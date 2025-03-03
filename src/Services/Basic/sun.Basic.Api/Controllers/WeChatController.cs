using Flurl;
using Flurl.Http;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using sun.Basic.Dtos;
using sun.Basic.Services;
using sun.Infrastructure;

namespace sun.Basic.Api.Controllers
{
    /// <summary>
    /// 公众号文章对接
    /// </summary>
    /// <param name="weChatBlogService"></param>
    public class WeChatController(IWeChatBlogService weChatBlogService, IConfiguration configuration) : BasicControllerBase
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
            return result;
        }
        /// <summary>
        /// 测试接口
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public async Task<List<AppMsgEx>> CreateBlogList()
        {
            var result = await "https://mp.weixin.qq.com/cgi-bin/appmsgpublish"
                .SetQueryParams(new 
                {
                    sub = "list",
                    begin = 15,
                    count = 5,
                    query = "",
                    fakeid = "MzkzNDQxOTU2MQ%3D%3D",
                    type = "101_1",
                    free_publish_type = 1,
                    sub_action = "list_ex",
                    token = 696618492,
                    lang = "zh_CN",
                    f = "json",
                    ajax = 1,
                })
                .WithHeaders(new
                {
                    Cookie = "appmsglist_action_2394354338=card; ua_id=EDAMOP6D094xkKlDAAAAAJScC2-ZoI93QzEi5w7or3E=; wxuin=21377171225362; mm_lang=zh_CN; RK=uVsUWLJ3a6; ptcz=e0fe95a82503ab89e2b919d2193906f4a19ea3a36d862e122b104fc3f94d675e; pac_uid=0_15Fb86bQZnCKr; suid=user_0_15Fb86bQZnCKr; _qimei_uuid42=18b040f063510053c5a6d22710ac61def545aefef5; _qimei_fingerprint=061ad7ab545fa395b7a58aac9a716f9b; _qimei_q36=; _qimei_h38=fdede269c5a6d22710ac61de02000008718b04; pgv_pvid=9297857591; mp_1fdc14ede649e0330d3bf90740df79f9_mixpanel=%7B%22distinct_id%22%3A%20%2270c45f5ca1da0a016faf9680b852e3fb%22%2C%22%24device_id%22%3A%20%22192c780360e1f1-0bd3031f4c01d9-26011951-1fa400-192c780360e1f2%22%2C%22%24initial_referrer%22%3A%20%22%24direct%22%2C%22%24initial_referring_domain%22%3A%20%22%24direct%22%2C%22%24user_id%22%3A%20%2270c45f5ca1da0a016faf9680b852e3fb%22%2C%22%24search_engine%22%3A%20%22google%22%2C%22__mps%22%3A%20%7B%7D%2C%22__mpso%22%3A%20%7B%7D%2C%22__mpus%22%3A%20%7B%7D%2C%22__mpa%22%3A%20%7B%7D%2C%22__mpu%22%3A%20%7B%7D%2C%22__mpr%22%3A%20%5B%5D%2C%22__mpap%22%3A%20%5B%5D%7D; _ga=GA1.1.1386640540.1736907294; _ga_MRRHVE420B=GS1.1.1736907294.1.1.1736907295.0.0.0; mmad_session=e5e926d32cb7213da0a91cd8da634a846b1cdbc6a741285fa117d0e6d67a0a68c0d57d633c06603d56d47c99b727ba3629cdba5d78140253592c9a9d6b02fae1453e98b61339a62b77b88eb30524c2fd33f18109b6d3bdee343cd5477e08ad0611de1c56c245721266e7088080fefde3; pgv_info=ssid=s5593969149; ts_uid=2836866276; rewardsn=; wxtokenkey=777; poc_sid=HA68tWejWVHhGM1H5FGgBkMskhyyj6KQV8lq4YMH; mp_token=1804375939; sig=h0106d3034344e5237b37b21d935714c2b06ecf1e3a88535616c161efa17dde60a28a793453720852b4; _clck=2394354338|1|ftp|0; uuid=24be235f1b6922579f7fb1cb88b0c6bd; rand_info=CAESIL+T9TOD4rHODfXrrEmeaBNv2NPsJBEacNiD4TZurlfo; slave_bizuin=2394354338; data_bizuin=2394354338; bizuin=2394354338; data_ticket=e0/9NVE3F75Vb4SGGlQyvwiNLDjo6PFyF/7WMzy6RfKzi7vvr58bR3UgtfsV2tHP; slave_sid=Uk1TQ1FhSjV5T1N6YlljUV9mU2VZQ0FMRGNWbHAzcUVZWW1QWHFCbnp4dGpKSkJGUVdyTGpYbl9TUXQxdGIzcFlwa2pWemJPZzJIeDl6eGM3Nk1seE9OeGJRRGlxYlc2d015V2sxQkV1YWtDUHNMUncyd3FrMWY5b3l1RnQ0NkpOZU95U1gwRHFXampia0hV; slave_user=gh_bd3fa87c5431; xid=27855178a895ba91639c5f66a4cd505c; _clsk=1x99xe8|1740381597049|3|1|mp.weixin.qq.com/weheat-agent/payload/record"
                })
                .GetJsonAsync<WeChatPublishData>();

            var p = JsonConvert.DeserializeObject<PublishPage>(result.publish_page);

            var pp = new List<AppMsgEx>();
            foreach (var item in p.publish_list)
            {
                var json = JsonConvert.DeserializeObject<PublishInfo>(item.publish_info);
                pp.Add(json!.appmsgex[0]);

                var model = json!.appmsgex[0];
                var exist = await weChatBlogService.ExistsAsync(a => a.AId == model.aid);

                if(!exist)
                {
                   await weChatBlogService.InsertAsync(new Domains.WeChatBlog()
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
            return pp;
        }
    }
}
