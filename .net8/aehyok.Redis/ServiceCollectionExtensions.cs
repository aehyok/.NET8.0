using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSRedis;
using Microsoft.Extensions.Configuration;

namespace aehyok.Redis
{
    public class RedisOption
    {
        public string ConnectionString { get; set; }
        public bool UseKeyEventNotify { get; set; } = false;
    }

    public static partial  class ServiceCollectionExtensions
    {

        public static IApplicationBuilder AddRedis(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.AddRedis(options =>
            {
                options.ConnectionString = configuration.GetConnectionString("Redis");
            });

            return app;
        }

        /// <summary>
        /// 初始化RabbitMQ事件订阅
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder AddRedis(this IApplicationBuilder app, Action<RedisOption> configure)
        {
            RedisOption options = new RedisOption();
            configure(options);
            string redisConnectionString = options.ConnectionString;
            if (string.IsNullOrWhiteSpace(redisConnectionString))
            {
                throw new Exception("Redis连接字符串不能为空");
            }
            var csredis = new CSRedisClient(redisConnectionString);
            RedisHelper.Initialization(csredis);

            return app;
        }
    }
}
