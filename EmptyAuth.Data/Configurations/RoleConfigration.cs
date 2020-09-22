using EmptyAuth.Data.Configurations.Helper;
using EmptyAuth.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyAuth.Data.Configurations
{
	public class RoleConfigration : DbEntityConfiguration<Role>
	{
		public override void Configure(EntityTypeBuilder<Role> entity)
		{
			entity.ToTable("Role").HasKey(x => new { x.Id });

		}
	}
}
