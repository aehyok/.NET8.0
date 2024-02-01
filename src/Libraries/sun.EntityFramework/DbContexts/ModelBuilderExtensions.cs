using sun.EntityFrameworkCore.Mapping;
using sun.Infrastructure;
using sun.Infrastructure.TypeFinders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace sun.EntityFrameworkCore.DbContexts
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

        /// <summary>
        /// 查询时添加全局过滤器
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <param name="builder"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static ModelBuilder ApplyGlobalFilterAsDeleted<TInterface>(this ModelBuilder builder, Expression<Func<TInterface, bool>> expression)
        {
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                if (entityType.ClrType.GetInterface(typeof(TInterface).Name) != null)
                {
                    var newParam = Expression.Parameter(entityType.ClrType);
                    var newBody = ReplacingExpressionVisitor.Replace(expression.Parameters.Single(), newParam, expression.Body);
                    builder.Entity(entityType.ClrType).HasQueryFilter(Expression.Lambda(newBody, newParam));
                }
            }

            return builder;
        }

        /// <summary>
        /// 设置级联删除为禁止
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static ModelBuilder SetDeleteBehaviorToRestrict(this ModelBuilder builder)
        {
            var foreignKeys = builder.Model.GetEntityTypes().SelectMany(a => a.GetForeignKeys()).Where(a => a.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var foreignKey in foreignKeys)
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }

            return builder;
        }

        /// <summary>
        /// Migrations 时生成表、字段注释，因 LoxSmoke.DocXml 无法添加多个 Xml , 所以目前暂时无法 读取实体基类注释
        /// </summary>
        /// <param name="builder"></param>
        public static void AddEntityComments(this ModelBuilder builder)
        {
            //Debugger.Launch();
            var coreAssemblyName = Assembly.GetExecutingAssembly().GetName().Name;

            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var typeAssemblyName = entityType.ClrType.Assembly.GetName().Name;

                var typeComment = DocsHelper.GetTypeComments(entityType.ClrType.Assembly.GetName().Name, entityType.ClrType);
                builder.Entity(entityType.ClrType).HasComment(typeComment);

                //builder.Entity(entityType.ClrType).ToTable( a => a.HasComment(typeComment));

                foreach (var property in entityType.GetProperties())
                {
                    string memberComment;

                    if (property.PropertyInfo?.DeclaringType?.Assembly == Assembly.GetExecutingAssembly())
                    {
                        memberComment = DocsHelper.GetMemberComments(coreAssemblyName, property.PropertyInfo);
                    }
                    else
                    {
                        memberComment = DocsHelper.GetMemberComments(typeAssemblyName, property.PropertyInfo);
                    }

                    property.SetComment(memberComment);
                }
            }
        }
    }
}
