using AngleSharp;
using AngleSharp.Io;
using Ardalis.Specification;
using Flurl;
using Flurl.Http;
using Markdig;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.JSInterop.Infrastructure;
using Newtonsoft.Json;
using OpenAI.Chat;
using PuppeteerSharp;
using Renci.SshNet.Messages;
using Senparc.CO2NET.Helpers.Serializers;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.Draft;
using Senparc.Weixin.MP.AdvancedAPIs.Draft.DraftJson;
using Senparc.Weixin.MP.AdvancedAPIs.GroupMessage;
using Senparc.Weixin.MP.Containers;
using sun.Basic.Domains;
using sun.Basic.Dtos;
using sun.Basic.Services;
using sun.Core.Services;
using sun.EntityFrameworkCore.Repository;
using sun.Infrastructure;
using sun.Infrastructure.Exceptions;
using sun.Infrastructure.Options;
using sun.Redis;
using System;
using System.ClientModel;
using System.Net;
using System.Text.RegularExpressions;
using SIConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
using StringUtils = sun.Infrastructure.Utils.StringExtensions;

namespace sun.Basic.Api.Controllers
{
    /// <summary>
    /// 公众号文章对接
    /// </summary>
    public class WeChatController(
        IWeChatBlogService wxBlogService,
        IWeChatConfigService wxConfigService,
        ILargeLanguageModelService llmService,
        IRedisService redisService,
        IWeChatUtlToBlogService blogService,
        ISystemPromptService spService,
        IFileService fileService,
        IOptionsSnapshot<StorageOptions> storageOptions,
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
            var appSecret = configuration.GetSection("WeChatOfficialAccounts:secret").Value;

            var token = await redisService.GetAsync<string>("WeChatToken");
            if (string.IsNullOrEmpty(token))
            {
                var result = await "https://api.weixin.qq.com/cgi-bin/token"
                    .SetQueryParams(new
                    {
                        grant_type = "client_credential",
                        appid = appid,
                        secret = appSecret
                    })
                    .GetJsonAsync<WeChatToken>();
                if(result.ErrorCode == 40164)
                {
                    throw new ErrorCodeException(-1, result.ErrorMessage);
                }

                await redisService.SetAsync("WeChatToken", result.AccessToken, TimeSpan.FromSeconds(result.ExpiresIn));
                // 将获取的token存入redis
                return result.AccessToken;
            }
            else
            {
                return token;
            }
        }

        /// <summary>
        /// 将url链接转换为纯内容文本
        /// </summary>
        /// <returns></returns>
        [HttpGet("urltotext")]
        public async Task<dynamic> UrlToTextAsync(string url)
        {
            var wxConfig = await wxConfigService.GetAsync(item => item.CreatedBy == CurrentUser.UserId && item.CookieType == Domains.CookieType.单次拉取Cookie);

            if(wxConfig is null)
            {
                return new ErrorCodeException(-1, "请通过浏览器F12获取Cookie进行设置");
            }
            try
            {
                var requester = new DefaultHttpRequester();
                // 设置必要的头信息
                requester.Headers["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/98.0.4758.102 Safari/537.36";
                requester.Headers["Cookie"] = wxConfig.Cookie; // 这里需要有效的微信 Cookie
                requester.Headers["Referer"] = "https://mp.weixin.qq.com/";


                var config = Configuration.Default.WithDefaultLoader(new LoaderOptions
                {
                    IsResourceLoadingEnabled = true
                }).With(requester);

                var address = url;
                var context = BrowsingContext.New(config);
                var document = await context.OpenAsync(address);
                // 获取完整的 HTML 内容
                var divElement = document.GetElementById("js_content");

                if (divElement != null)
                {
                    var htmlContent = divElement.OuterHtml;
                    var converter = new ReverseMarkdown.Converter();

                    // 将 HTML 转换为 Markdown
                    string markdown = converter.Convert(htmlContent);

                    // 这里要将markdown文章转换一下

                    var prompt = await spService.GetAsync(item => item.Code == "urltotext");
                    var content = $"{prompt.Content} {markdown}";
                    var result = await PostAsync(content, "gemini-2.5-pro-exp-03-25");

                    var blog = await blogService.GetAsync(item => item.SourceUrl == url);

                    var model = new WeChatUrlToBlog()
                    {
                        SourceUrl = url,
                        SourceUrlType = SourceUrlType.WeChat,
                        SourceContent = markdown,
                        GeminiContent = result,
                    };
                    if (blog is null) {
                        await blogService.InsertAsync(model);
                        return model.Id;
                    }
                    else
                    {
                        blog.GeminiContent = result;
                        blog.UpdatedAt = DateTime.Now;
                        await blogService.UpdateAsync(blog);
                    }

                    return blog.Id;
                }
            }catch(Exception e)
            {
                throw new ErrorCodeException(-1, e.Message);
            }
            
            return "";
        }

        /// <summary>
        /// 纯文本润色改写
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("texttoretext")]
        public async Task<dynamic> TextToReTextAsync(long id)
        {
            var blog = await blogService.GetAsync(item => item.Id == id);
            if (blog is null)
            {
                throw new ErrorCodeException(-1, "此Id数据不存在");
            }

            var prompt = await spService.GetAsync(item => item.Code == "texttoretext");
            var content = $"{prompt.Content} {blog.GeminiContent}";

            var result = await PostAsync(content, "gemini-2.5-pro-exp-03-25");

            string json = @"";
            json =  result.Replace("```json", "");
            json = json.Replace("```", "");

            if (!string.IsNullOrEmpty(json))
            {
                var model = JsonConvert.DeserializeObject<AIReWriteDto>(json);
                blog.ReWriteContent = model.Content;
                blog.UpdatedAt = DateTime.Now;
                blog.Title = model.Title;
                blog.Digest = model.Digest;
                await blogService.UpdateAsync(blog);

                return result;
            }

            return "";

        }

        /// <summary>
        /// 生成封面图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ErrorCodeException"></exception>
        [HttpGet("createCoverImage")]
        public async Task<dynamic> CreateCoverImageAsync(long id)
        {
            var blog = await blogService.GetAsync(item => item.Id == id);
            if (blog is null)
            {
                throw new ErrorCodeException(-1, "此Id数据不存在");
            }

            var prompt = await spService.GetAsync(item => item.Code == "coverimage");
            var content = $"{prompt.Content} {blog.Title}";
            var dsResult = await PostAsync(content, "deepseek-chat");

            Regex regex = new Regex(@"```html(.*?)```", RegexOptions.Singleline);
            Match match = regex.Match(dsResult);

            if (match.Success)
            {
                string html = match.Groups[1].Value.Trim();
                var base64 =  await CreateHtmlToImage(html,900, 383);

                var bytes = Convert.FromBase64String(base64);
                long stampId = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() * 1000000;

                var file = await fileService.UploadAsync(bytes, $"{stampId}.png");
                
                blog.CoverImageId = file.Id;
                blog.UpdatedAt = DateTime.Now;

                await blogService.UpdateAsync(blog);
            }
            return "";
        }

        /// <summary>
        /// 针对文本内容进行排版
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ErrorCodeException"></exception>
        [HttpGet("texttohtml")]
        public async Task<dynamic> ConvertAsync(long id)
        {
            var blog = await blogService.GetAsync(item => item.Id == id);
            if(blog is null)
            {
                throw new ErrorCodeException(-1, "此Id数据不存在");
            }
            var prompt = await spService.GetAsync(item => item.Code == "texttohtml");
            var content = $"{prompt.Content} {blog.ReWriteContent}";

            var dsResult = await PostAsync(content, "gemini-2.5-pro-exp-03-25", blog.ConvertContentToHtml);

            blog.ConvertContentToHtml = (!string.IsNullOrEmpty(blog.ConvertContentToHtml)) ? blog.ConvertContentToHtml + dsResult : dsResult;
            blog.UpdatedAt = DateTime.Now;
            await blogService.UpdateAsync(blog);

            return blog.ConvertContentToHtml;
        }

        /// <summary>
        /// 将html网页转换为适配微信公众号的格式
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("htmltowechathtml")]
        public async Task<dynamic> ConvertWeChatHtml(long id)
        {
            var blog = await blogService.GetAsync(item => item.Id == id);
            if (blog is null)
            {
                throw new ErrorCodeException(-1, "此Id数据不存在");
            }

            var prompt = await spService.GetAsync(item => item.Code == "htmltowechathtml");
            var content = $"{prompt.Content} {blog.ConvertContentToHtml}";
            var dsResult = await PostAsync(content, "deepseek-chat");

            blog.ConvertWeChatHtml = dsResult;
            blog.UpdatedAt = DateTime.Now;
            await blogService.UpdateAsync(blog);
            return dsResult;
        }

        /// <summary>
        /// 创建草稿
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ErrorCodeException"></exception>
        [HttpGet("createDraft")]
        public async Task<dynamic> GenDraftAsync(long id)
        {
            var blog = await blogService.GetAsync(item => item.Id == id);
            if (blog is null)
            {
                throw new ErrorCodeException(-1, "此Id数据不存在");
            }

            WeixinMediaDto mediaDto = null;

            if(blog.CoverImageId > 0)
            {
                var file = await fileService.GetByIdAsync(blog.CoverImageId);
                var basePath = Path.Combine(storageOptions.Value.Path, file.Path);
                mediaDto = await UploadFileAsync(basePath, UploadForeverMediaType.image);

                if (!string.IsNullOrEmpty(mediaDto.MediaId))
                {
                    blog.MediaId = mediaDto.MediaId;
                    blog.MediaUrl = mediaDto.MediaUrl;
                }
            } 

            var dto = new DraftModel
            {
                title = blog.Title,
                content = "<section><span leaf=\"\">" + blog.ConvertWeChatHtml + "</span></section><p style=\"display: none;\"><mp-style-type data-value=\"3\"></mp-style-type></p>",
                thumb_media_id = blog.MediaId,
                digest = (blog.Digest.Length> 120) ? blog.Digest.Substring(0,120) : blog.Digest,
                show_cover_pic = "1",
                need_open_comment = 1
            };

            var access_token = await GetToken();
            AddDraftResultJson result = await DraftApi.AddDraftAsync(access_token, 10000, dto);

            if (result.ErrorCodeValue == 0)
            {
                blog.UpdatedAt = DateTime.Now;
                await blogService.UpdateAsync(blog);
                return Ok();
            }
            else
            {
                throw new ErrorCodeException(-1, "创建草稿出现错误");
            }
        }
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpPost("upload")]
        public async Task<WeixinMediaDto> UploadFileAsync(string fileName, UploadForeverMediaType type = UploadForeverMediaType.image)
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var filePath = Path.Combine(baseDirectory, fileName);

            var accessToken = await this.GetToken();
            var imageResult = await MediaApi.UploadForeverMediaAsync(accessToken, filePath, type);

            var model = new WeixinMediaDto();

            if(imageResult.ErrorCodeValue == 0)
            {
                model.MediaId = imageResult.media_id;
                model.MediaUrl = imageResult.url;
            }

            return model;
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
        public async Task<string> PostAsync(string message, string modelName = "", string content = "", bool isJson = false)
        {
            var spec = Specifications<LargeLanguageModel>.Create();
            if(string.IsNullOrEmpty(modelName))
            {
                spec.Query.Where(item => item.IsDefault);
            } 
            else
            {
                spec.Query.Where(item => item.Name == modelName);
            }


            var model = await llmService.GetAsync(spec);

            var key = new ApiKeyCredential(key: model.ApiKey);

            var options = new OpenAI.OpenAIClientOptions();
            options.Endpoint = new System.Uri(model.BaseUrl);
            options.NetworkTimeout = TimeSpan.FromSeconds(60*10);

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

            if(!string.IsNullOrEmpty(content))
            {
                var systemMessage = new SystemChatMessage(content);
                list.Add(systemMessage);

                var userMessage = new UserChatMessage("上面还未生成完毕,请继续生成。");
                list.Add(userMessage);

            }
            //ChatCompletion completion = client.CompleteChat("你好啊");

            if(isJson)
            {
                var responseFormat = ChatResponseFormat.CreateJsonSchemaFormat(
                        jsonSchemaFormatName: "custom_response",
                        jsonSchema: BinaryData.FromBytes("""
                        {
                            "type": "object",
                            "properties": {
                                "title": { "type": "string" },
                                "content": { "type": "string" },
                                "digest": { "type": "string" }
                            },
                            "required": ["title", "content"],
                            "additionalProperties": false
                        }
                        """u8.ToArray()),
                        jsonSchemaIsStrict: true);


                ChatCompletionOptions chatOptions = new ChatCompletionOptions
                {
                    MaxOutputTokenCount = 8000,
                    ResponseFormat = responseFormat
                };

                var completion = client.CompleteChat(list);

                return completion.Value.Content[0].Text;
            }
            else
            {
                ChatCompletionOptions chatOptions = new ChatCompletionOptions
                {
                    MaxOutputTokenCount = 8000,
                };

                var completion = client.CompleteChat(list);

                return completion.Value.Content[0].Text;
            }
                
            //return "";
        }

        /// <summary>
        /// 将html转换为图片
        /// </summary>
        /// <returns></returns>
        [HttpGet("convertHtml2Imag")]
        public async Task<string> CreateHtmlToImage(string html, int width, int height)
        {
            var browserFetcher = new BrowserFetcher();
            await browserFetcher.DownloadAsync();
            await using var browser = await Puppeteer.LaunchAsync(
                new LaunchOptions { Headless = true });
            await using var page = await browser.NewPageAsync();
            await page.SetViewportAsync(new ViewPortOptions
            {
                Width = width,
                Height = height
            });

            // 加载在线连接
            //await page.GoToAsync("http://localhost:4000/b.html");

            // 直接加载html 字符串链接
            await page.SetContentAsync(html);
            var result = await page.GetContentAsync();
            return await page.ScreenshotBase64Async();
        }
    }
}
