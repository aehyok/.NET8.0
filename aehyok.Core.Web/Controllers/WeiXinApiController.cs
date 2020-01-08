using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.MP.Entities.Menu;

namespace aehyok.Core.Web.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class WeiXinApiController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private static string appId = string.Empty; //WebConfigurationManager.AppSettings["WeiXinAppId"];         //微信开发者ID
        private static string appSecret = string.Empty;// WebConfigurationManager.AppSettings["WeiXinAppSecret"];  //微信开发者密码 

        /// <summary>
        /// 构造函数初始化
        /// </summary>
        /// <param name="configuration"></param>
        public WeiXinApiController(IConfiguration configuration)
        {
            this._configuration = configuration;
            appId = this._configuration.GetSection("WeiXin:AppId").Value;
            appSecret = this._configuration.GetSection("WeiXin:AppSecret").Value;
        }

        /// <summary>
        /// 获取微信AccessToken值(AccessToken每次只暂存2小时需要进行程序处理)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public dynamic GetAccessToken()
        {
            var accessToken = AccessTokenContainer.TryGetAccessToken(appId, appSecret);   //盛派已处理AccessToken的生命周期
            //string url = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=" + appId + "&secret=" + appSecret;
            //var client = new RestClient(url);

            //var request = new RestRequest("", Method.GET);

            //IRestResponse response = client.Execute(request);
            //var content = JsonConvert.DeserializeObject<dynamic>(response.Content);
            //token = content["access_token"].ToString();
            return accessToken;
        }

        /// <summary>
        /// 发送邮件测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public dynamic SendMail()
        {
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.163.com";//使用163的SMTP服务器发送邮件
            client.UseDefaultCredentials = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new System.Net.NetworkCredential("aehyok", "Hk1997mc1999");//163的SMTP服务器需要用163邮箱的用户名和密码作认证，如果没有需要去163申请个,
            MailMessage message = new MailMessage();
            message.From = new MailAddress("aehyok@163.com");//这里需要注意，163似乎有规定发信人的邮箱地址必须是163的
            message.To.Add("455043818@qq.com");//将邮件发送给QQ邮箱
            var url = "https://www.aehyok.com";
            message.Subject = "邮箱发送消息测试";
            message.Body = url;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.Priority = MailPriority.High;
            message.IsBodyHtml = true;
            client.Send(message);
            return "发送成功！";
        }

        /// <summary>
        /// 给用户发送模板消息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public dynamic SendTemplate()
        {
            var accessToken = GetAccessToken();
            string url = "https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=" + accessToken;
            var first = "沈丘物流园工程设计初稿讨论";
            var keyword1 = "2019年12月30日 09：30";
            var keyword2 = "郑州金水区祭城路街道楷林中心7座703";
            var keyword3 = "李建林";
            var remark = "如有问题可联系负责人 魏杭 13888888888888";

            JObject jObject = new JObject();
            jObject.Add("first", JToken.FromObject(new
            {
                value = first,
                color = "#173177"
            }));

            jObject.Add("keyword1", JToken.FromObject(new
            {
                value = keyword1,
                color = "#173177"
            }));
            jObject.Add("keyword2", JToken.FromObject(new
            {
                value = keyword2,
                color = "#173177"
            }));
            jObject.Add("keyword3", JToken.FromObject(new
            {
                value = keyword3,
                color = "red"
            }));
            jObject.Add("remark", JToken.FromObject(new
            {
                value = remark,
                color = "#173177"
            }));


            JObject caseObject = new JObject();
            var openId = "obnc91UBIDTiUje0WimQodu_Tk74";// WebConfigurationManager.AppSettings["OpenId"];
            caseObject.Add("touser", openId);//"ofO7kwcA--0lUpAAXKyyhWYhIvRQ");  //微信号
            caseObject.Add("template_id", "SLqPFnyObAYbLIc1wMjuApA1Cj6KkS6uEPhUqIbeW1o"); //模板ID
            var pdfUrl = "http://aehyok.com"; //WebConfigurationManager.AppSettings["PdfUrl"].ToString();
            caseObject.Add("url", pdfUrl);
            caseObject.Add("topcolor", "#FF0000");
            caseObject.Add("data", jObject);
            caseObject.Merge(jObject);

            var casejson = caseObject.ToString();


            var client = new RestClient(url);

            var request = new RestRequest("", Method.POST);

            request.AddParameter("application/json", caseObject, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            var content = JsonConvert.DeserializeObject<dynamic>(response.Content);
            return Ok(content);
        }

        /// <summary>
        /// 给用户发送模板消息（小程序）
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public dynamic SendMiniTemplate()
        {
            var accessToken = GetAccessToken();
            string url = "https://api.weixin.qq.com/cgi-bin/message/subscribe/send?access_token=" + accessToken;
            var first = "沈丘物流园工程设计初稿讨论";
            var date2 = DateTime.Now.ToString("f");
            var character_string1 = "YH1231231";
            var phrase3 = "成功";
            var thing4 = "失败理由";
            var thing5 = "沈丘物流园工程设计初稿讨论";

            JObject jObject = new JObject();
            //jObject.Add("first", JToken.FromObject(new
            //{
            //    value = first,
            //    color = "#173177"
            //}));

            jObject.Add("date2", JToken.FromObject(new
            {
                value = date2,
                color = "#173177"
            }));
            jObject.Add("character_string1", JToken.FromObject(new
            {
                value = character_string1,
                color = "#173177"
            }));
            jObject.Add("phrase3", JToken.FromObject(new
            {
                value = phrase3,
                color = "red"
            }));
            jObject.Add("thing4", JToken.FromObject(new
            {
                value = thing4,
                color = "#173177"
            }));

            jObject.Add("thing5", JToken.FromObject(new
            {
                value = thing5,
                color = "#173177"
            }));
            JObject caseObject = new JObject();
            var openId = "omBbY5U_eLR12PBoo3cutdTBYyFs";// WebConfigurationManager.AppSettings["OpenId"];
            caseObject.Add("touser", openId);//"ofO7kwcA--0lUpAAXKyyhWYhIvRQ");  //微信号
            caseObject.Add("template_id", "SJKejbRVTcjk5zb5eUIfYBacicIfLobCnkBmNR1H1js"); //模板ID

            caseObject.Add("topcolor", "#FF0000");
            caseObject.Add("data", jObject);
            caseObject.Add("page", "pages/bind/bind");
            caseObject.Merge(jObject);

            var casejson = caseObject.ToString();


            var client = new RestClient(url);

            var request = new RestRequest("", Method.POST);

            request.AddParameter("application/json", caseObject, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            var content = JsonConvert.DeserializeObject<dynamic>(response.Content);
            return Ok(content);
        }


        /// <summary>
        /// 创建自定义菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public dynamic CreateMenuList()
        {
            ButtonGroup bg = new ButtonGroup();

            //单击
            bg.button.Add(new SingleClickButton()
            {
                name = "智能检索",
                key = "OneClick",
            });

            //二级菜单
            var subButton = new SubButton()
            {
                name = "我的"
            };
            subButton.sub_button.Add(new SingleViewButton()
            {
                name = "身份绑定",
                url="http://aehyok.com"
            });
            subButton.sub_button.Add(new SingleClickButton()
            {
                key = "SubClickRoot_News",
                name = "我的任务"
            });
            subButton.sub_button.Add(new SingleClickButton()
            {
                key = "SubClickRoot_Music",
                name = "我的会议"
            });
            subButton.sub_button.Add(new SingleViewButton()
            {
                url = "http://weixin.senparc.com",
                name = "个人信息"
            });
            bg.button.Add(subButton);

            var accessToken = AccessTokenContainer.TryGetAccessToken(appId, appSecret);
            var result = CommonApi.CreateMenu(accessToken, bg);
            return Ok(result);
        }
    }
}