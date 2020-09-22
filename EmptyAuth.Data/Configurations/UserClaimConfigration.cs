using EmptyAuth.Data.Configurations.Helper;
using EmptyAuth.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmptyAuth.Data.Configurations
{
	public class UserClaimConfigration : DbEntityConfiguration<UserClaim>
	{
		public override void Configure(EntityTypeBuilder<UserClaim> entity)
		{
			entity.ToTable("UserClaim").HasKey(x => new { x.Id });

			entity.HasOne(x => x.User).WithMany(x => x.Claims).HasForeignKey(x => new { x.UserId }).OnDelete(DeleteBehavior.Cascade);

		}
	}
}
