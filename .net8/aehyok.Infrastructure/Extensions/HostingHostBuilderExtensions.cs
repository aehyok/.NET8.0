using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Infrastructure.Extensions
{
    public static class HostingHostBuilderExtensions
    {
        /// <summary>
        /// 初始化 Host
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="moduleKey"></param>
        /// <returns></returns>
        public static IHostBuilder InitHost(this IHostBuilder builder, string moduleKey)
        {
            Thread.CurrentThread.Name = moduleKey;

            return builder;
        }
    }
}
