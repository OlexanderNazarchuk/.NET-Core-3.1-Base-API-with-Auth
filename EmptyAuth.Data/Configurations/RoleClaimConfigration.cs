using EmptyAuth.Data.Configurations.Helper;
using EmptyAuth.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmptyAuth.Data.Configurations
{
	public class RoleClaimConfigration : DbEntityConfiguration<RoleClaim>
	{
		public override void Configure(EntityTypeBuilder<RoleClaim> entity)
		{
			entity.ToTable("RoleClaim").HasKey(x => new { x.Id });

			entity.HasOne(x => x.Role).WithMany(x => x.Claims).HasForeignKey(x => new { x.RoleId }).OnDelete(DeleteBehavior.Cascade);

		}
	}
}

