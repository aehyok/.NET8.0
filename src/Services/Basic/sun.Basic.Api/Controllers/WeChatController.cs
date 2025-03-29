using AngleSharp;
using AngleSharp.Io;
using Flurl;
using Flurl.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using OpenAI.Chat;
using PuppeteerSharp;
using Renci.SshNet.Messages;
using sun.Basic.Dtos;
using sun.Basic.Services;
using sun.Infrastructure;
using sun.Infrastructure.Exceptions;
using System;
using System.ClientModel;
using System.Net;
using SIConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace sun.Basic.Api.Controllers
{
    /// <summary>
    /// 公众号文章对接
    /// </summary>
    public class WeChatController(
        IWeChatBlogService wxBlogService,
        IWeChatConfigService wxConfigService,
        ILargeLanguageModelService llmService,
        SIConfiguration configuration) : BasicControllerBase
    {
        /// <summary>
        /// 获取公众号token
        /// </summary>
        /// <returns></returns>
        [HttpGet("token")]
        public async Task<dynamic> GetToken()
        {
            var appid = configuration.GetSection("WeChatOfficialAccounts:appid").Value;
            var secret = configuration.GetSection("WeChatOfficialAccounts:secret").Value;
            var result = await "https://api.weixin.qq.com/cgi-bin/token"
                .SetQueryParams(new
                {
                    grant_type="client_credential",
                    appid = appid,
                    secret= secret
                })
                .GetJsonAsync<WcChatToken>();

            // 将获取的token存入redis
            return result;
        }

        /// <summary>
        /// 转换html网页
        /// </summary>
        /// <returns></returns>
        [HttpGet("html")]
        public async Task<dynamic> GetHtml()
        {
            var config = Configuration.Default.WithDefaultLoader();
            var address = "https://mp.weixin.qq.com/s/QDf6S2hCz5DT2De1vYNlvw";
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(address);
            // 获取完整的 HTML 内容
            var divElement = document.GetElementById("js_content");
            
            if(divElement != null)
            {
                var htmlContent = divElement.OuterHtml;
                var converter = new ReverseMarkdown.Converter();

                // 将 HTML 转换为 Markdown
                string markdown = converter.Convert(htmlContent);
                return markdown;
            }
            return "";
        }

        /// <summary>
        /// 测试接口
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public async Task<StatusCodeResult> CreateBlogList()
        {
            //第几个开始
            var begin = 0;

            // 每次请求多少条记录
            var count = 5;

            var totalCount = 0;

            var getList = async (int begin, int count) =>
            {
                var config = await wxConfigService.GetAsync(item => item.CreatedBy == CurrentUser.UserId);

                if (config is null)
                {
                    throw new ErrorCodeException(-1, "请先配置微信公众号参数");
                }

                var result = await "https://mp.weixin.qq.com/cgi-bin/appmsgpublish"
                    .SetQueryParams(new
                    {
                        sub = "list",
                        begin = begin,
                        count = count,
                        query = "",
                        fakeid = config.FakeId,
                        type = "101_1",
                        free_publish_type = 1,
                        sub_action = "list_ex",
                        token = config.Token,
                        lang = "zh_CN",
                        f = "json",
                        ajax = 1,
                    })
                    .WithHeaders(new
                    {
                        Cookie = config.Cookie
                    })
                    .GetJsonAsync<WeChatPublishData>();

                if (result.base_resp.ret == 200003)
                {
                    throw new ErrorCodeException(-1, "微信公众号的token和Cookie过期，请重新获取");
                }
                var p = JsonConvert.DeserializeObject<PublishPage>(result.publish_page);


                //总数量
                totalCount = p.total_count;
                var pp = new List<AppMsgEx>();
                foreach (var item in p.publish_list)
                {
                    var json = JsonConvert.DeserializeObject<PublishInfo>(item.publish_info);
                    pp.Add(json!.appmsgex[0]);

                    var model = json!.appmsgex[0];
                    var exist = await wxBlogService.ExistsAsync(a => a.AId == model.aid);

                    if (!exist)
                    {
                        await wxBlogService.InsertAsync(new Domains.WeChatBlog()
                        {
                            AId = model.aid,
                            Title = model.title,
                            Cover = model.cover,
                            Link = model.link,
                            Create_Time = model.create_time,
                            Update_Time = model.update_time,
                            Digest = model.digest,
                            AuthorName = model.author_name,
                            ItemShowType = model.item_show_type,
                        });
                    }
                }
            };

            for(var index = 0; index <= totalCount; index += count)
            {
                await getList(index, count);
            }

            return Ok();
        }

        /// <summary>
        /// 测试AI
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("stream")]
        public async Task<dynamic> GetAsync(string message)
        {
            var model = await llmService.GetAsync(item => item.IsDefault);

            Response.Headers.Append("Content-Type", "text/event-stream");
            Response.Headers.Append("Cache-Control", "no-cache");
            Response.Headers.Append("Connection", "keep-alive");
            var key = new ApiKeyCredential(key: model.ApiKey);

            var options = new OpenAI.OpenAIClientOptions();
            options.Endpoint = new System.Uri(model.BaseUrl);

            var cancellationToken = HttpContext.RequestAborted;

            ChatClient client = new(model: model.Name, key, options);

            List<ChatMessage> messages =
            [
                new UserChatMessage(message),
            ];

            //ChatCompletion completion = client.CompleteChat("你好啊");

            var completion = client.CompleteChatStreamingAsync(messages, cancellationToken: cancellationToken);

            await foreach (StreamingChatCompletionUpdate completionUpdate in completion.WithCancellation(cancellationToken))
            {
                if (completionUpdate.ContentUpdate.Count > 0)
                {
                    var text = completionUpdate.ContentUpdate[0].Text;
                    await Response.WriteAsync($"{text}", cancellationToken);
                    await Response.Body.FlushAsync(cancellationToken);
                }
            }
            return "";
        }

        /// <summary>
        /// 测试AI
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("chat")]
        public async Task<dynamic> PostAsync(string message)
        {
            var model = await llmService.GetAsync(item => item.IsDefault);

            var key = new ApiKeyCredential(key: model.ApiKey);

            var options = new OpenAI.OpenAIClientOptions();
            options.Endpoint = new System.Uri(model.BaseUrl);

            var cancellationToken = HttpContext.RequestAborted;

            ChatClient client = new(model: model.Name, key, options);

            List<ChatMessage> list = new List<ChatMessage>();

            if (!string.IsNullOrEmpty(model.SystemPrompt))
            {
                var systemMessage = new SystemChatMessage(model.SystemPrompt);
                list.Add(systemMessage);
            }
            
            if(string.IsNullOrEmpty(message))
            {
                throw new ErrorCodeException(-1, "数据不能为空");
            }
            else
            {
                var userMessage = new UserChatMessage(message);
                list.Add(userMessage);
            }

            //ChatCompletion completion = client.CompleteChat("你好啊");

            var completion = client.CompleteChat(list);

            return completion.Value.Content[0].Text;
            //return "";
        }

        /// <summary>
        /// 将html转换为图片
        /// </summary>
        /// <returns></returns>
        [HttpGet("convert")]
        public async Task CreateHtmlToImage()
        {
            var browserFetcher = new BrowserFetcher();
            await browserFetcher.DownloadAsync();
            await using var browser = await Puppeteer.LaunchAsync(
                new LaunchOptions { Headless = true });
            await using var page = await browser.NewPageAsync();
            await page.SetViewportAsync(new ViewPortOptions
            {
                Width = 500,
                Height = 725
            });

            // 加载在线连接
            //await page.GoToAsync("http://localhost:4000/b.html");

            // 直接加载html 字符串链接
            await page.SetContentAsync("<div>My Receipt</div>");
            var result = await page.GetContentAsync();


            var outputFile = "tlp.png";
            await page.ScreenshotAsync(outputFile);
        }
    }
}
