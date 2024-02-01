using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 根据不同的接口，注册不同生命周期的接口服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <param name="lifetime"></param>
        /// <returns></returns>
        public static IServiceCollection AddServices<T>(this IServiceCollection services, ServiceLifetime lifetime)
        {
            var types = TypeFinders.TypeFinders.SearchTypes(typeof(T), TypeFinders.TypeFinders.TypeClassification.Interface);

            foreach (var type in types)
            {
                var interfaces = type.GetInterfaces().Where(t => !t.IsGenericType && t != typeof(T));

                foreach (var interfaceType in interfaces)
                {
                    services.AddService(interfaceType, type, lifetime);
                }
            }

            return services;
        }

        private static IServiceCollection AddService(this IServiceCollection services, Type interfaceType, Type implementationType, ServiceLifetime lifetime)
        {
            services.Add(new ServiceDescriptor(implementationType, implementationType, lifetime));
            services.Add(new ServiceDescriptor(interfaceType, implementationType, lifetime));

            return services;
        }
    }
}
