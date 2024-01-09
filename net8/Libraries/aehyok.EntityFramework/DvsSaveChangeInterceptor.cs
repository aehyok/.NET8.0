using aehyok.EntityFrameworkCore.Entities;
using aehyok.Infrastructure;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.EntityFrameworkCore
{
    /// <summary>
    /// EFCore 操作拦截器（新增、删除、修改）
    /// </summary>
    public class DvsSaveChangeInterceptor: SaveChangesInterceptor
    {
        private readonly IServiceScopeFactory scopeFactory;


        public DvsSaveChangeInterceptor(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        /// <summary>
        /// 同步保存之前的处理逻辑
        /// </summary>
        /// <param name="eventData"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            TrackerHandler(eventData);
            return base.SavingChanges(eventData, result);
        }

        /// <summary>
        /// 异步保存之前的处理逻辑
        /// </summary>
        /// <param name="eventData"></param>
        /// <param name="result"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            TrackerHandler(eventData);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        /// <summary>
        /// 同步保存之后的处理逻辑
        /// </summary>
        /// <param name="eventData"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
        {
            return base.SavedChanges(eventData, result);    
        }

        /// <summary>
        /// 异步保存之后的处理逻辑
        /// </summary>
        /// <param name="eventData"></param>
        /// <param name="result"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            return await base.SavedChangesAsync(eventData, result, cancellationToken);
        }

        private void TrackerHandler(DbContextEventData eventData)
        {
            var tracker = eventData.Context.ChangeTracker;
            tracker.DetectChanges();

            SoftDeleteHandler(tracker);
            AuditHandler(tracker);
        }
        /// <summary>
        /// 处理软删除
        /// </summary>
        /// <param name="changeTracker"></param>
        private void SoftDeleteHandler(ChangeTracker changeTracker)
        {
            var entities = changeTracker.Entries().Where(a => a.Entity is ISoftDelete && a.State == Microsoft.EntityFrameworkCore.EntityState.Deleted);

            if (entities.Any())
            {
                foreach (var entry in entities)
                {
                    ISoftDelete entity = entry.Entity as ISoftDelete;

                    entity.IsDeleted = true;
                    entry.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
            }
        }

        /// <summary>
        /// 处理数据审计
        /// </summary>
        /// <param name="changeTracker"></param>
        private void AuditHandler(ChangeTracker changeTracker)
        {
            var entities = changeTracker.Entries().Where(a => a.Entity is IAuditedEntity && a.State > Microsoft.EntityFrameworkCore.EntityState.Unchanged);

            if (entities.Any())
            {
                using var scope = this.scopeFactory.CreateScope();
                //{
                var currentUser = scope.ServiceProvider.GetService<ICurrentUser>();

                    foreach (var entry in entities)
                    {
                        IAuditedEntity entity = entry.Entity as IAuditedEntity;

                        switch (entry.State)
                        {
                            case Microsoft.EntityFrameworkCore.EntityState.Added:
                                entity.UpdatedAt = DateTime.Now;
                                entity.CreatedAt = DateTime.Now;
                                if (currentUser.IsAuthenticated)
                                {
                                    entity.CreatedBy = currentUser.UserId;
                                    entity.UpdatedBy = currentUser.UserId;
                                }
                                break;

                            case Microsoft.EntityFrameworkCore.EntityState.Modified:
                            case Microsoft.EntityFrameworkCore.EntityState.Deleted:
                                entity.UpdatedAt = DateTime.Now;
                                if (currentUser.IsAuthenticated)
                                {
                                    entity.UpdatedBy = currentUser.UserId;
                                }
                                break;
                        }
                    }
                //}
            }
        }
    }
}
