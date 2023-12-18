using aehyok.Core;
using CSRedis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Redis
{
    public class RedisService: IRedisService, IScopedDependency
    {
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

        public Task<bool> SetAsync(string key, object value, TimeSpan expire, RedisExistence? exists = null)
        {
            return RedisHelper.SetAsync(key, value, expire, exists);
        }

        public Task<bool> SetAsync(string key, object value, int expireSeconds = -1, RedisExistence? exists = null)
        {
            return RedisHelper.SetAsync(key, value, expireSeconds, exists);
        }
    }
}
