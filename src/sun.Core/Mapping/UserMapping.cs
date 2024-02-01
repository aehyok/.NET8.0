using sun.Core.Domains;
using sun.EntityFrameworkCore.Mapping;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace sun.Basic.Mapping
{
    public class UserMapping: MapBase<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            // builder.HasMany(a => a.Roles) 指示一个用户下可以有多个角色（一对多的关系）
            builder.HasMany(a => a.Roles).WithMany(a => a.Users)
                .UsingEntity<UserRole>();
        }
    }
}
