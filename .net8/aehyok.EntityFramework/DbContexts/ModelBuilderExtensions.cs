using aehyok.Core.Mapping;
using aehyok.Infrastructure.TypeFinders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.EntityFramework.DbContexts
{
    public static class ModelBuilderExtensions
    {
        /// <summary>
        /// 将EFCore 所有实体通过反射注册到EFCore中（要继承初始化接口IEntity）
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="builder"></param>
        /// <param name="modelTypePredicate"></param>
        public static void RegisterFromAssembly<TEntity>(this ModelBuilder builder, Func<Type, bool> modelTypePredicate)
        {
            var registedTypes = new HashSet<Type>();

            var mappingTypes = TypeFinders.SearchTypes(typeof(IMappingConfiguration), TypeFinders.TypeClassification.Interface).Where(a => !a.IsAbstract && !a.IsInterface);

            // 映射关系
            foreach (var type in mappingTypes)
            {
                var mapping = (IMappingConfiguration)Activator.CreateInstance(type);
                mapping.ApplyConfiguration(builder);

                var entityType = type.GetTypeInfo().ImplementedInterfaces.First().GetGenericArguments().Single();
                registedTypes.Add(entityType);
            }

            var types = TypeFinders.SearchTypes(typeof(TEntity), TypeFinders.TypeClassification.Interface).Where(a => !a.IsAbstract && a.IsClass).Where(modelTypePredicate).ToList();

            // 映射实体
            foreach (var type in types)
            {
                builder.Entity(type).HasNoDiscriminator();
            }
        }
    }
}
