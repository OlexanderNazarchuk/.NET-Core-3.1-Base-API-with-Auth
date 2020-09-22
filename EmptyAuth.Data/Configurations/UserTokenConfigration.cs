using EmptyAuth.Data.Configurations.Helper;
using EmptyAuth.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmptyAuth.Data.Configurations
{
	public class UserTokenConfigration : DbEntityConfiguration<UserToken>
    {
        public override void Configure(EntityTypeBuilder<UserToken> entity)
        {
            entity.ToTable("UserToken").HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.HasOne(e => e.User).WithMany(e => e.Tokens).HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

