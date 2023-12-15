using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using aehyok.Infrastructure.TypeFinders;
namespace aehyok.Schedules
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 初始化定时任务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCronTask(this IServiceCollection services)
        {
            var cronType = typeof(CronScheduleService);

            foreach (var type in TypeFinders.SearchTypes(cronType, TypeFinders.TypeClassification.Class))
            {
                services.Add(new ServiceDescriptor(typeof(IHostedService), type, ServiceLifetime.Singleton));
            }
            return services;
        }
    }
}
