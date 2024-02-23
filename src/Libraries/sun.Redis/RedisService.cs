using sun.Infrastructure;
using CSRedis;

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

        public async Task<Dictionary<string,string>> ScanAsync()
        {
           List<string> list = new List<string>();
           var result = await RedisHelper.ScanAsync(20, "*");
            list.AddRange(result.Items);

            var values = await RedisHelper.MGetAsync(list.ToArray());

            var resultDictionary = list.Zip(values, (key, value) => new { key, value })
                                            .ToDictionary(item => item.key, item => item.value);

            return resultDictionary;
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
