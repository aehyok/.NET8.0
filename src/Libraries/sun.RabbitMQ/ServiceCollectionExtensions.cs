using sun.Infrastructure.TypeFinders;
using sun.RabbitMQ.EventBus;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace sun.RabbitMQ
{
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// 读取RabbitMQ配置，以及注入RabbitMQ的发布者和订阅者
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddRabbitMQ(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitOptions>(configuration.GetSection("RabbitMQ"));

            services.AddSingleton<IConnection, Connection>();
            services.AddSingleton<IEventPublisher, EventPublisher>();
            services.AddSingleton<IEventSubscriber, EventSubscriber>();

            return services;
        }

        /// <summary>
        /// 初始化RabbitMQ事件订阅
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder AddRabbitMQEventBus(this IApplicationBuilder app)
        {
            var subscriber = app.ApplicationServices.GetRequiredService<IEventSubscriber>();
            TypeFinders.SearchTypes(typeof(IEventHandler<>), TypeFinders.TypeClassification.GenericInterface).ForEach(item =>
            {
                var eventType = item.GetInterfaces().Where(item => item.IsGenericType).SingleOrDefault().GetGenericArguments().SingleOrDefault();
                subscriber.Subscribe(eventType, item);
            });

            return app;
        }
    }
}
