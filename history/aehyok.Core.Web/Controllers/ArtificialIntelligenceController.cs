using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aehyok.Core.AI.Baidu;
using aehyok.Core.Picture;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RestSharp;

namespace aehyok.Core.Web.Controllers
{
    /// <summary>
    /// 人工智能控制器
    /// </summary>
    public class ArtificialIntelligenceController : Controller
    {
        private const string _cacheKey = "Baidu_Access_Token";
        private readonly IMemoryCache _memoryCache;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="memoryCache"></param>
        public ArtificialIntelligenceController(IMemoryCache memoryCache) 
        {
            this._memoryCache = memoryCache;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            if(!this._memoryCache.TryGetValue<string>(_cacheKey, out string value))
            {
                value = CharacterRecognition.GetAccessToken();

                MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions();
                cacheOptions.AbsoluteExpiration = DateTime.Now.AddDays(1);
                this._memoryCache.Set<string>(_cacheKey, value, cacheOptions);
            }
            var text=CharacterRecognition.GetContent(value);

            return View("Index",text);
        }
    }
}