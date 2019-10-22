using aehyok.Core.Config;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace aehyok.Core.AutoMapper
{
    public static class AutoMapperExtension
    {
        public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services)
        {
            string assemblies = ConfigurationManager.GetConfig("Assembly:Mapper");

            if (!string.IsNullOrEmpty(assemblies))
            {
                var profiles = new List<Type>();

                var parentType = typeof(Profile);

                foreach (var item in assemblies.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    var types = Assembly.Load(item).GetTypes()
                        .Where(i => i.BaseType != null && i.BaseType.Name == parentType.Name);

                    if (types.Count() != 0 || types.Any())
                        profiles.AddRange(types);
                }

                if (profiles.Count() != 0 || profiles.Any())
                    services.AddAutoMapper(profiles.ToArray());
            }

            return services;
        }
    }
}
