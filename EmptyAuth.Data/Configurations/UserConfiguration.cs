using EmptyAuth.Data.Configurations.Helper;
using EmptyAuth.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmptyAuth.Data.Configurations
{
    public class UserConfiguration : DbEntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> entity)
        {
            entity.ToTable("User").HasKey(e => e.Id);

            entity.HasOne(x => x.Organization).WithMany(x => x.Users).HasForeignKey(x => new { x.OrganizationId }).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
