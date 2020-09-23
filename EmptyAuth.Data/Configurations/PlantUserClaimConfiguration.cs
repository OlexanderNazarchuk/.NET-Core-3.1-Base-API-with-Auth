using EmptyAuth.Data.Configurations.Helper;
using EmptyAuth.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmptyAuth.Data.Configurations
{
    public class PlantUserClaimConfiguration : DbEntityConfiguration<PlantUserClaim>
    {
        public override void Configure(EntityTypeBuilder<PlantUserClaim> entity)
        {
            entity.ToTable("PlantUserClaim").HasKey(e => e.Id);

            entity.HasOne(x => x.Plant).WithMany(x => x.PlantUserClaims).HasForeignKey(x => new { x.PlantId }).OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(x => x.User).WithMany(x => x.PlantUserClaims).HasForeignKey(x => new { x.UserId }).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
