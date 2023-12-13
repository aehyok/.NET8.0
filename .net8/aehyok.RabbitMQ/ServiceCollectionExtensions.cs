using aehyok.Infrastructure.TypeFinders;
using aehyok.RabbitMQ.EventBus;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.RabbitMQ
{
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// 读取RabbitMQ配置
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

        public static IApplicationBuilder AddRabbitMQEventBus(this IApplicationBuilder app)
        {
            var subscriber = app.ApplicationServices.GetRequiredService<IEventSubscriber>();
            
            //var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(item => item.FullName.StartsWith("aehyok.")).ToList();
            //try
            //{
            //    foreach (var assembly in assemblies)
            //    {
            //        foreach (var handleType in assembly.GetTypes())
            //        { 
            //            if (IsAssignableToGenericInterface(handleType, typeof(IEventHandler<>)))
            //            {
            //                var eventType = handleType.GetInterfaces().Where(item => item.IsGenericType).SingleOrDefault().GetGenericArguments().SingleOrDefault();
            //                subscriber.Subscribe(eventType, handleType);
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}

            TypeFinders.SearchTypes(typeof(IEventHandler<>), TypeFinders.TypeClassification.GenericInterface).ForEach(item =>
            {
                var eventType = item.GetInterfaces().Where(item => item.IsGenericType).SingleOrDefault().GetGenericArguments().SingleOrDefault();
                subscriber.Subscribe(eventType, item);
            });

            return app;
        }

        public static bool IsAssignableToGenericInterface(Type givenType, Type genericInterfaceType)
        {
            var interfaceTypes = givenType.GetInterfaces();
            foreach (var it in interfaceTypes)
            {
                if (it.IsGenericType && it.GetGenericTypeDefinition() == genericInterfaceType)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
