using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Flurl.Http;
using Microsoft.Extensions.FileProviders;
using System.Collections.Generic;
using System.Threading;
using aehyok.Core.EntityFramework.MySql;
using aehyok.Core.EntityFramework.MySql.Models;
using System;
using aehyok.Core.EntityFramework.MySql.Data;
using System.Linq;

namespace aehyok.Core.WebApi.Controllers
{
    /// <summary>
    /// 极客
    /// </summary>
    public class GeekController : BaseApiController
    {
        private readonly IRepository<GeekArticle> _geekArticleRepository;

        private readonly IRepository<GeekProduct> _geekProductRepository;
        public GeekController(IRepository<GeekArticle> geekArticleRepository, IRepository<GeekProduct> geekProductRepository)
        {
            this._geekArticleRepository = geekArticleRepository;
            this._geekProductRepository = geekProductRepository;
        }

        //获取到本地的Json文件并且解析返回对应的json字符串
        public static string GetJsonFile(string filepath)
        {
            string json = string.Empty;
            using (FileStream fs = new FileStream(filepath, FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                using (StreamReader sr = new StreamReader(fs, Encoding.GetEncoding("GB2312")))
                {
                    json = sr.ReadToEnd().ToString();
                }
            }
            return json;
        }

        /// <summary>
        /// 获取课程列表
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public dynamic GetProductList()
        {
            var list  = this._geekProductRepository.GetQueryable().Select(item => new
            {
                item.Id,
                item.Title,
            });
            return list;
        }

        /// <summary>
        /// 获取课程详情
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<dynamic> GetProduct(string id)
        {
            return await this._geekProductRepository.GetAsync(id);
        }

        /// <summary>
        /// 获取文章详情
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<dynamic> GetArticle(string id)
        {
            return await this._geekArticleRepository.GetAsync(id);
        }

        /// <summary>
        /// 获取文章列表
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public dynamic GetArticleList(string productId)
        {
            MyDbContext context = new MyDbContext();
            var list =  this._geekArticleRepository.GetQueryable().Where(item => item.ProductId == productId).Select(item => new
            {
                item.Id,
                item.CreateTime,
                item.ProductId,
                item.Title
            });
            return list;
        }
        /// <summary>
        /// 获取
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<dynamic> SetArticle()
        {
            var directorypath = Directory.GetCurrentDirectory();
            string strFileName = directorypath + "\\json\\json.json";
            string jsonData = GetJsonFile(strFileName);
            var sourceobjects = JObject.Parse(jsonData);
            JArray jsonList = sourceobjects["list"] as JArray;

            List<string> list = new List<string>();
            var url = "https://time.geekbang.org/serv/v1/article";
            foreach (var item in jsonList)
            {
                try
                {
                    var itemId = (string)item["id"];

                    var body = new { id = itemId, include_neighbors = true, is_freelyread = true, reverse = false };
                    var response = await url
                        .WithHeaders(new
                        {
                            Host = "time.geekbang.org",
                            Connection = "keep-alive",
                            Accept = "application/json, text/plain, */*",
                            Content_Length = Encoding.UTF8.GetByteCount(JsonConvert.SerializeObject(body)),
                            User_Agent = "Mozilla/5.0 (iPhone; CPU iPhone OS 10_3_1 like Mac OS X) AppleWebKit/603.1.30 (KHTML, like Gecko) Version/10.0 Mobile/14E304 Safari/602.1",
                            Content_Type = "application/json",
                            Origin = "https://time.geekbang.org",
                            Sec_Fetch_Site = "same-origin",
                            Sec_Fetch_Mode = "cors",
                            Sec_Fetch_Dest = "empty",
                            Referer = "https://time.geekbang.org/column/article/252088",
                            Accept_Encoding = "gzip, deflate, br",
                            Accept_Language = "zh,en;q=0.9,zh-CN;q=0.8",
                            Cookie = "_ga=GA1.2.1062569751.1645080912; GCID=c235cb8-035898f-1a3c164-9361133; GRID=c235cb8-035898f-1a3c164-9361133; LF_ID=1645080909016-6410706-6626018; MEIQIA_TRACK_ID=25E8oT4sn4SPVW6qiGNSJGUcNXv; MEIQIA_VISIT_ID=25JknB92EtoTnVqa3AtKhCFeZUt; gksskpitn=16c5877d-8d1d-4277-b052-f3f75194c80a; _gid=GA1.2.2110285600.1645406172; GCESS=BgoEAAAAAAsCBgAEBAAvDQAGBE976HQHBG__r0MJAQEDBAroEmIFBAAAAAANAQEIAQMMAQEBCIM3KwAAAAAAAgQK6BJi; Hm_lvt_59c4ff31a9ee6263811b23eb921a5083=1645246026,1645246086,1645406172,1645406219; Hm_lvt_022f847c4e3acd44d4a2481d9187f1e6=1645406172,1645406219,1645418706,1645429445; sensorsdata2015jssdkcross=%7B%22distinct_id%22%3A%222832259%22%2C%22first_id%22%3A%2217f0676943de36-08f42fe9ce7825-576153e-3686400-17f0676943ee90%22%2C%22props%22%3A%7B%22%24latest_traffic_source_type%22%3A%22%E7%9B%B4%E6%8E%A5%E6%B5%81%E9%87%8F%22%2C%22%24latest_search_keyword%22%3A%22%E6%9C%AA%E5%8F%96%E5%88%B0%E5%80%BC_%E7%9B%B4%E6%8E%A5%E6%89%93%E5%BC%80%22%2C%22%24latest_referrer%22%3A%22%22%2C%22%24latest_landing_page%22%3A%22https%3A%2F%2Ftime.geekbang.org%2Fcolumn%2Fintro%2F100053801%3Ftab%3Dcatalog%22%7D%2C%22%24device_id%22%3A%2217f0676943de36-08f42fe9ce7825-576153e-3686400-17f0676943ee90%22%7D; gk_exp_uid=|1645430732227933407|a6a360f1aa84fd4964c155e53ddf8d70de0165ada9ba651000b3996d91f8b355; _gat=1; Hm_lpvt_59c4ff31a9ee6263811b23eb921a5083=1645431420; Hm_lpvt_022f847c4e3acd44d4a2481d9187f1e6=1645431420; SERVERID=3431a294a18c59fc8f5805662e2bd51e|1645431419|1645427948; gk_process_ev={%22referrer%22:%22https://time.geekbang.org/resource?m=0&d=9&c=9%22%2C%22utime%22:1645430733263%2C%22count%22:31%2C%22target%22:%22%22}"
                        }).PostJsonAsync(body)
                        .ReceiveJson();
                    //list.Add(response);
                    var jsonString = JsonConvert.SerializeObject(response, Formatting.Indented);
                    JObject obj = JsonConvert.DeserializeObject<JObject>(jsonString);

                    await this._geekArticleRepository.InsertAsync(new GeekArticle
                    {
                        Id = itemId,
                        Json = JsonConvert.SerializeObject(response, Formatting.Indented),
                        Title = (string)obj.GetValue("data")["article_title"],
                        AuthorName = (string)obj.GetValue("data")["author_name"],
                        CreateTime = DateTime.Now
                    }); ;
                    Thread.Sleep(1000 * 60 * 2);
                }catch
                {

                } 

            }   
            return list;
        }
    }
}
