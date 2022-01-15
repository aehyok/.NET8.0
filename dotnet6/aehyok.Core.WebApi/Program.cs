using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aehyok.Core.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
#if DEBUG
                    webBuilder.UseStartup<Startup>().UseNLog();//.UseUrls("http://*:5000;");
#else
                    webBuilder.UseStartup<Startup>().UseNLog().UseUrls("http://*:5000;");
#endif
                }).UseServiceProviderFactory(new AutofacServiceProviderFactory());
    }
}
