using EmptyAuth.Data.Configurations.Helper;
using EmptyAuth.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmptyAuth.Data.Configurations
{
	public class UserRoleConfigration : DbEntityConfiguration<UserRole>
    {
        public override void Configure(EntityTypeBuilder<UserRole> entity)
        {
            entity.ToTable("UserRole").HasKey(e => new { e.UserId, e.RoleId });

            entity.HasOne(e => e.User).WithMany(e => e.Roles).HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Role).WithMany(e => e.Users).HasForeignKey(e => e.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
