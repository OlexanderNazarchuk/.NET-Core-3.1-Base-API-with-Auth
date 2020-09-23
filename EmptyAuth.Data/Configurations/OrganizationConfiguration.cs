using EmptyAuth.Data.Configurations.Helper;
using EmptyAuth.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmptyAuth.Data.Configurations
{
    public class OrganizationConfiguration : DbEntityConfiguration<Organization>
    {
        public override void Configure(EntityTypeBuilder<Organization> entity)
        {
            entity.ToTable("Organization").HasKey(e => e.Id);

        }
    }
}
