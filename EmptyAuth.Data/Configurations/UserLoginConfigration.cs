using EmptyAuth.Data.Configurations.Helper;
using EmptyAuth.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmptyAuth.Data.Configurations
{
	public class UserLoginConfigration : DbEntityConfiguration<UserLogin>
    {
        public override void Configure(EntityTypeBuilder<UserLogin> entity)
        {
            entity.ToTable("UserLogin").HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasOne(x => x.User).WithMany(x => x.Logins).HasForeignKey(x => new { x.UserId }).OnDelete(DeleteBehavior.Cascade);
        }
    }
}