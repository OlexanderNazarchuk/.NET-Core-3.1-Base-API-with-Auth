using EmptyAuth.Data.Configurations.Helper;
using EmptyAuth.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmptyAuth.Data.Configurations
{
    public class PlantConfiguration : DbEntityConfiguration<Plant>
    {
        public override void Configure(EntityTypeBuilder<Plant> entity)
        {
            entity.ToTable("Plant").HasKey(e => e.Id);

            entity.HasOne(x => x.Organization).WithMany(x => x.Plants).HasForeignKey(x => new { x.OrganizationId }).OnDelete(DeleteBehavior.Cascade);


        }
    }
}
