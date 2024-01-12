using aehyok.Infrastructure;
using CSRedis;
using System.Runtime.CompilerServices;

namespace aehyok.Redis
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
