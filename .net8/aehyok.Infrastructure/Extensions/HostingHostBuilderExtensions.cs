using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Infrastructure.Extensions
{
    public static class HostingHostBuilderExtensions
    {
        /// <summary>
        /// 初始化 Host，加载配置文件
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="moduleKey"></param>
        /// <returns></returns>
        public static IHostBuilder InitHostAndConfig(this IHostBuilder builder, string moduleKey)
        {
            Thread.CurrentThread.Name = moduleKey;

            // 例如 aehyok.NCDP 最开始代码中没有使用到，是不会加载到内存中的，所以需要手动加载
            Directory.GetFiles(AppContext.BaseDirectory, "aehyok.*.dll").Select(AssemblyLoadContext.Default.LoadFromAssemblyPath).ToList();

            builder.ConfigureAppConfiguration((context, options) =>
            {
                //options.AddJsonFile("../etc/appsettings.json", optional: true, reloadOnChange: true);
                options.AddJsonFile(Path.Combine(AppContext.BaseDirectory, "../../../../../../etc/appsettings.json"), true, true);
            });

            return builder;
        }
    }
}
