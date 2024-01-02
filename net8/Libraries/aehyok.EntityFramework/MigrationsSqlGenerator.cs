using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Update;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure.Internal;
using Pomelo.EntityFrameworkCore.MySql.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.EntityFrameworkCore
{
    /// <summary>
    /// EF Core 数据库迁移时，过滤外键
    /// </summary>
    public class MigrationsSqlGenerator : MySqlMigrationsSqlGenerator
    {
#pragma warning disable EF1001 // Internal EF Core API usage.
        public MigrationsSqlGenerator(MigrationsSqlGeneratorDependencies dependencies,ICommandBatchPreparer commandBatchPreparer, IMySqlOptions options) : base(dependencies, commandBatchPreparer, options)
#pragma warning restore EF1001 // Internal EF Core API usage.
        {
        }

        protected override void Generate(CreateTableOperation operation, IModel model, MigrationCommandListBuilder builder, bool terminate = true)
        {
            operation.ForeignKeys.RemoveAll(a => true);
            base.Generate(operation, model, builder, terminate);
        }

        protected override void Generate(AlterTableOperation operation, IModel model, MigrationCommandListBuilder builder)
        {
            // AlertTable 取消外键

            base.Generate(operation, model, builder);
        }
    }
}
