using sun.Infrastructure;
using CSRedis;
using sun.Infrastructure.Models;
using System.Dynamic;

namespace sun.Redis
{
    public class RedisService: IRedisService, IScopedDependency
    {
        public async Task<long> DeleteAsync(string key)
        {
            return await RedisHelper.DelAsync(key);
        }

        public Task<T> GetAsync<T>(string key)
        {
            return RedisHelper.GetAsync<T>(key);
        }

        public bool PingAsync()
        {
            return RedisHelper.Ping();
        }

        public async Task<dynamic> ScanAsync(PagedQueryModelBase model)
        {
            List<string> list = new List<string>();

            //根沐model.Keyword进行模糊匹配
            var scanResult = await RedisHelper.ScanAsync(model.Page, $"*{model.Keyword}*", model.Limit);
            list.AddRange(scanResult.Items);

            var values = await RedisHelper.MGetAsync(list.ToArray());

            var resultDictionary = list.Zip(values, (key, value) => new { key, value })
                                            .ToDictionary(item => item.key, item => item.value);
            dynamic result = new ExpandoObject();
            result.Items = resultDictionary;
            result.Cursor = scanResult.Cursor;  // 下一次获取要通过这个Cursor获取下一页的keys
           return result;
        }

        public Task<bool> SetAsync(string key, object value)
        {
            return RedisHelper.SetAsync(key, value);
        }

        public async Task<bool> SetAsync(string key, object value, TimeSpan expire, RedisExistence? exists = null)
        {
            return await RedisHelper.SetAsync(key, value, expire, exists);
        }

        public Task<bool> SetAsync(string key, object value, int expireSeconds = -1, RedisExistence? exists = null)
        {
            return RedisHelper.SetAsync(key, value, expireSeconds, exists);
        }
    }
}
