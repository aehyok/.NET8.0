using Microsoft.Extensions.Hosting;
using Serilog;
using System.Runtime.CompilerServices;

namespace sun.Serilog
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 初始化Serilog日志
        /// </summary>
        /// <param name="builder"></param>
        public static void UseLog(this IHostBuilder builder)
        {
            builder.UseSerilog((context, services, config) =>
            {
                config.ReadFrom.Configuration(context.Configuration);

                // 在日志中记录请求的客户端 IP 地址
                config.Enrich.WithIpAddress(services);

                // 在日志中记录产生该日志的服务 WorkerId
                config.Enrich.WithWorker();

                // 在日志中记录当前登录用户的 UserTokenId
                config.Enrich.WithToken(services);
            });
        }
    }
}
