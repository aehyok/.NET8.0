using aehyok.Core.Domains;
using aehyok.EntityFrameworkCore.Mapping;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace aehyok.Basic.Mapping
{
    public class UserMapping: MapBase<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.HasMany(a => a.Roles).WithMany(a => a.Users)
                .UsingEntity<UserRole>(a => a.HasOne(c => c.Role).WithMany(c => c.UserRoles).HasForeignKey(c => c.RoleId),
                a => a.HasOne(c => c.User).WithMany(c => c.UserRoles).HasForeignKey(c => c.UserId));
        }
    }
}
