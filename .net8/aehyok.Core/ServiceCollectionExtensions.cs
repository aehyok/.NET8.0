using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core
{
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// 设置开发环境项目启动后的默认启动路由
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseSetStartDefaultRoute(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                //app.Use(async (context, next) =>
                //{
                //    if (context.Request.Path.Value.Equals("/"))
                //    {
                //        context.Response.Redirect("/docs/index.html");
                //        return;
                //    }
                //    await next(context);
                //});
            }
            return app;
        }
    }
}
