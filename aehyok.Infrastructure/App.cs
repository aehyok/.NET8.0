using aehyok.Infrastructure.Options;
using aehyok.Infrastructure.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Infrastructure
{
    /// <summary>
    /// 统一的静态方法
    /// </summary>
    public class App
    {
        private static IServiceProvider ServiceProvider;

        /// <summary>
        /// 初始化IServiceProvider用于获取配置
        /// </summary>
        /// <param name="serviceProvider"></param>
        public static void Init(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public static TOptions Options<TOptions>() where TOptions : class, new()
        {
            if(ServiceProvider is null)
            {
                throw new Exception("使用前请先使用 App.Init() 方法初始化");
            }

            using var scope = ServiceProvider.CreateScope();
            //IOptionsSnapshot 可以获取到最新的配置
            return scope.ServiceProvider.GetService<IOptionsSnapshot<TOptions>>().Value;
        }

        /// <summary>
        /// 获取系统临时文件夹路径
        /// </summary>
        /// <returns></returns>
        public static string GetTempPath()
        {
            var tempPath = Options<StorageOptions>().TempPath;
            if (tempPath.IsNullOrEmpty())
            {
                tempPath = Path.Combine(AppContext.BaseDirectory, "temp");
            }

            Directory.CreateDirectory(tempPath);

            return tempPath;
        }
    }
}
