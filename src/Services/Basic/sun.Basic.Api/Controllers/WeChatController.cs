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
using Microsoft.IdentityModel.Tokens;
using Microsoft.JSInterop.Infrastructure;
using Newtonsoft.Json;
using OpenAI.Chat;
using PuppeteerSharp;
using Renci.SshNet.Messages;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.Draft;
using Senparc.Weixin.MP.AdvancedAPIs.Draft.DraftJson;
using Senparc.Weixin.MP.AdvancedAPIs.GroupMessage;
using Senparc.Weixin.MP.Containers;
using sun.Basic.Domains;
using sun.Basic.Dtos;
using sun.Basic.Services;
using sun.EntityFrameworkCore.Repository;
using sun.Infrastructure;
using sun.Infrastructure.Exceptions;
using sun.Redis;
using System;
using System.ClientModel;
using System.Net;
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
        [HttpGet("wechat/urltotext")]
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

            var prompt = await spService.GetAsync(item => item.Code == "urltotext");
            var content = $"{prompt.Content} {blog.GeminiContent}";

            var result = await PostAsync(content, "gemini-2.5-pro-exp-03-25");

            blog.ReWriteContent = result;
            blog.UpdatedAt = DateTime.Now;

            await blogService.UpdateAsync(blog);

            return result;
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

            var dsResult = await PostAsync(content, "deepseek-chat", blog.ConvertContentToHtml);

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
        /// 针对原始网页文章进行重新排版
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ErrorCodeException"></exception>
        [HttpGet("wechat/convertReWriteHtml")]
        public async Task<dynamic> ConvertReWriteAsync(long id)
        {
            var blog = await blogService.GetAsync(item => item.Id == id);
            if (blog is null)
            {
                throw new ErrorCodeException(-1, "此Id数据不存在");
            }
            var ds = $"你是一名专业的网页设计师和前端开发专家，对现代 Web 设计趋势和最佳实践有深入理解，尤其擅长创造具有极高审美价值的用户界面。你的设计作品不仅功能完备，而且在视觉上令人惊叹，能够给用户带来强烈的\"Aha-moment\"体验。\r\n\r\n请根据最后提供的内容，设计一个**美观、现代、易读**的\"中文\"可视化网页。请充分发挥你的专业判断，选择最能体现内容精髓的设计风格、配色方案、排版和布局。\r\n\r\n**设计目标：**\r\n\r\n*   **视觉吸引力：** 创造一个在视觉上令人印象深刻的网页，能够立即吸引用户的注意力，并激发他们的阅读兴趣。\r\n*   **可读性：** 确保内容清晰易读，无论在桌面端还是移动端，都能提供舒适的阅读体验。\r\n*   **信息传达：** 以一种既美观又高效的方式呈现信息，突出关键内容，引导用户理解核心思想。\r\n*   **情感共鸣:** 通过设计激发与内容主题相关的情感（例如，对于励志内容，激发积极向上的情绪；对于严肃内容，营造庄重、专业的氛围）。\r\n\r\n**设计指导（请灵活运用，而非严格遵循）：**\r\n\r\n*   **整体风格：** 可以考虑杂志风格、出版物风格，或者其他你认为合适的现代 Web 设计风格。目标是创造一个既有信息量，又有视觉吸引力的页面，就像一本精心设计的数字杂志或一篇深度报道。\r\n*   **Hero 模块（可选，但强烈建议）：** 如果你认为合适，可以设计一个引人注目的 Hero 模块。它可以包含大标题、副标题、一段引人入胜的引言，以及一张高质量的背景图片或插图。\r\n*   **排版：**\r\n    *   精心选择字体组合（衬线和无衬线），以提升中文阅读体验。\r\n    *   利用不同的字号、字重、颜色和样式，创建清晰的视觉层次结构。\r\n    *   可以考虑使用一些精致的排版细节（如首字下沉、悬挂标点）来提升整体质感。\r\n    *   Font-Awesome中有很多图标，选合适的点缀增加趣味性。\r\n*   **配色方案：**\r\n    *   选择一套既和谐又具有视觉冲击力的配色方案。\r\n    *   考虑使用高对比度的颜色组合来突出重要元素。\r\n    *   可以探索渐变、阴影等效果来增加视觉深度。\r\n*   **布局：**\r\n    *   使用基于网格的布局系统来组织页面元素。\r\n    *   充分利用负空间（留白），创造视觉平衡和呼吸感。\r\n    *   可以考虑使用卡片、分割线、图标等视觉元素来分隔和组织内容。\r\n*   **调性：**整体风格精致, 营造一种高级感。\r\n*   **数据可视化：** \r\n    *   设计一个或多个数据可视化元素，展示关键概念和它们之间的关系。\r\n    *   可以考虑使用思想导图、概念关系图、时间线或主题聚类展示等方式。\r\n    *   确保可视化设计既美观又有洞察性，帮助用户更直观地理解整体框架。\r\n    *   \r\n\r\n**技术规范：**\r\n\r\n*   使用 HTML5、Font Awesome、和最基本的CSS。\r\n    *   Font Awesome: [https://lf6-cdn-tos.bytecdntp.com/cdn/expire-100-M/font-awesome/6.0.0/css/all.min.css](https://lf6-cdn-tos.bytecdntp.com/cdn/expire-100-M/font-awesome/6.0.0/css/all.min.css)\r\n    *   Tailwind CSS: [https://lf3-cdn-tos.bytecdntp.com/cdn/expire-1-M/tailwindcss/2.2.19/tailwind.min.css](https://lf3-cdn-tos.bytecdntp.com/cdn/expire-1-M/tailwindcss/2.2.19/tailwind.min.css)\r\n    *   非中文字体: [https://fonts.googleapis.com/css2?family=Noto+Serif+SC:wght@400;500;600;700&family=Noto+Sans+SC:wght@300;400;500;700&display=swap](https://fonts.googleapis.com/css2?family=Noto+Serif+SC:wght@400;500;600;700&family=Noto+Sans+SC:wght@300;400;500;700&display=swap)\r\n    *   `font-family: Tahoma,Arial,Roboto,\"Droid Sans\",\"Helvetica Neue\",\"Droid Sans Fallback\",\"Heiti SC\",\"Hiragino Sans GB\",Simsun,sans-self;`\r\n    *   Mermaid: [https://lf3-cdn-tos.bytecdntp.com/cdn/expire-1-M/mermaid/8.14.0/mermaid.min.js](https://lf3-cdn-tos.bytecdntp.com/cdn/expire-1-M/mermaid/8.14.0/mermaid.min.js)\r\n*   \r\n*   代码结构清晰、语义化，包含适当的注释。\r\n*   实现完整的响应式，必须在所有设备上（手机、平板、桌面）完美展示。\r\n\r\n\r\n*  \r\n\r\n**输出要求：**\r\n\r\n*   提供一个完整、可运行的单一 HTML 文件，其中包含所有必要的 CSS不要使用JavaScript。\r\n*   确保代码符合 W3C 标准，没有错误或警告。   \r\n\r\n 记住这里有最重要的一个点：请直接返回给我最终的Html网页内容即可,其他内容无需进行返回。 \r\n\r\n请你像一个真正的设计师一样思考，充分发挥你的专业技能和创造力，打造一个令人惊艳的网页！\r\n\r\n待处理内容：{{{blog.ReWriteContent}}}";
            var dsResult = await PostAsync(ds, "deepseek-chat", blog.ConvertContentToHtml);

            blog.ConvertContentToHtml = (!string.IsNullOrEmpty(blog.ConvertContentToHtml)) ? blog.ConvertContentToHtml + dsResult : dsResult;

            blog.UpdatedAt = DateTime.Now;
            await blogService.UpdateAsync(blog);
            

            return blog.ConvertContentToHtml;
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

            var file = "20241101190046.jpg";
            // var media = await UploadFileAsync(file, UploadForeverMediaType.image);

            var dto = new DraftModel
            {
                title = "ceshi11",
                content = "<section><span leaf=\"\">"+ blog.ConvertWeChatHtml +"</span></section><p style=\"display: none;\"><mp-style-type data-value=\"3\"></mp-style-type></p>",
                thumb_media_id = "WT6sJmnkf0Wc51KJ8L2SX8stqQGNwGosBXMs_SeHXKhrObYnQl6BnkzYhYoKvvs4",  //media.MediaId,
                digest = "ceshi zhaiyao",
                show_cover_pic = "1",
                need_open_comment = 1
            };

            var access_token = await GetToken();
            var newsResult = await DraftApi.AddDraftAsync(access_token, 10000, dto);
            return newsResult;
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
        public async Task<dynamic> PostAsync(string message, string modelName = "", string content = "")
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
            ChatCompletionOptions chatOptions = new ChatCompletionOptions { MaxOutputTokenCount = 8000 };
            var completion = client.CompleteChat(list);

            return completion.Value.Content[0].Text;
            //return "";
        }

        /// <summary>
        /// 将html转换为图片
        /// </summary>
        /// <returns></returns>
        [HttpGet("convertHtml2Imag")]
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
