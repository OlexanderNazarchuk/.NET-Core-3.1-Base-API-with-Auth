using EmptyAuth.Data.Enums;
using EmptyAuth.Data.Enums.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace EmptyAuth.Data.Seeders
{
	public static class RoleSeeder
	{
		public static void Seed(AppDbContext context)
		{
			if (!context.Roles.Any())
			{
				var roles = new List<Entities.Role>();
				foreach (var item in (Role[])Enum.GetValues(typeof(Role)))
				{
					var role = new Entities.Role { Name = item.ToString(), NormalizedName = item.ToString().ToUpper(), Claims = new List<Entities.RoleClaim>() };

					switch (role.Name)
					{
						case nameof(Role.Admin):
							foreach (var claim in (Organization[])Enum.GetValues(typeof(Organization)))
								role.Claims.Add(new Entities.RoleClaim { ClaimType = claim.GetType().Name, ClaimValue = claim.ToString() });
							foreach (var claim in (Plant[])Enum.GetValues(typeof(Plant)))
								role.Claims.Add(new Entities.RoleClaim { ClaimType = claim.GetType().Name, ClaimValue = claim.ToString() });
							break;
						case nameof(Role.OrganizationOwner):
							foreach (var claim in (Organization[])Enum.GetValues(typeof(Organization)))
								role.Claims.Add(new Entities.RoleClaim { ClaimType = claim.GetType().Name, ClaimValue = claim.ToString() });
							foreach (var claim in (Plant[])Enum.GetValues(typeof(Plant)))
								role.Claims.Add(new Entities.RoleClaim { ClaimType = claim.GetType().Name, ClaimValue = claim.ToString() });
							break;
						case nameof(Role.PlantOwner):
							foreach (var claim in (Plant[])Enum.GetValues(typeof(Plant)))
								role.Claims.Add(new Entities.RoleClaim { ClaimType = claim.GetType().Name, ClaimValue = claim.ToString() });
							break;
					}
					roles.Add(role);
				}
				context.Roles.AddRange(roles);
				context.SaveChanges();
			}
		}
	}
}
