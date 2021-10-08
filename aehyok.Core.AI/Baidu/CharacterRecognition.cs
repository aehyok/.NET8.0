using aehyok.Core.Config;
using aehyok.Core.Picture;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace aehyok.Core.AI.Baidu
{
    public class CharacterRecognition
    {
        private static string AppKey = ConfigurationManager.GetConfig("Baidu:AppKey");
        private static string AppSecret = ConfigurationManager.GetConfig("Baidu:AppSecret");

        /// <summary>
        /// 获取访问的Access_Token
        /// </summary>
        /// <returns></returns>
        public static string  GetAccessToken()
        {
            var client = new RestClient("https://aip.baidubce.com/oauth/2.0/token");

            var request = new RestRequest("", Method.GET);
            request.AddParameter("grant_type", "client_credentials");
            request.AddParameter("client_id", AppKey);
            request.AddParameter("client_secret", AppSecret);

            // execute the request
            IRestResponse response = client.Execute(request);
            //var content = response.Content; // raw content as string
            var content = JsonConvert.DeserializeObject<dynamic>(response.Content);
            var result = ((Newtonsoft.Json.Linq.JValue)content["access_token"]).Value.ToString();
            return result;
        }

        public static string GetContent(string access_token)
        {
            var client1 = new RestClient("https://aip.baidubce.com/rest/2.0/ocr/v1/accurate_basic");

            var request1 = new RestRequest("", Method.POST);
            request1.AddParameter("access_token", access_token);
            request1.AddParameter("detect_direction", false);
            request1.AddParameter("probability", false);

            var str = ImageUtils.ImgToBase64String("test.png");
            var result = ImageUtils.UrlEncode(str);

            request1.AddParameter("image", str);

            IRestResponse response1 = client1.Execute(request1);

            var content1 = JsonConvert.DeserializeObject<dynamic>(response1.Content);
            string text = string.Empty;
            Console.WriteLine(content1);

            foreach (var item in content1.words_result)
            {
                Console.WriteLine(item.words);
                text = text + item.words;
            }
            return text;
        }
    }
}
