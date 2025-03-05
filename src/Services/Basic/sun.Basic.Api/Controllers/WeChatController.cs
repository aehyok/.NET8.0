using AngleSharp;
using Flurl;
using Flurl.Http;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using sun.Basic.Dtos;
using sun.Basic.Services;
using sun.Infrastructure;
using sun.Infrastructure.Exceptions;
using System;
using System.Net;
using SIConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace sun.Basic.Api.Controllers
{
    /// <summary>
    /// 公众号文章对接
    /// </summary>
    /// <param name="weChatBlogService"></param>
    public class WeChatController(IWeChatBlogService wxBlogService, IWeChatConfigService wxConfigService,  SIConfiguration configuration) : BasicControllerBase
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
    }
}
