using EmptyAuth.Common.Enums;
using EmptyAuth.Common.Enums.Claims;
using EmptyAuth.Data.Extensions;
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
				foreach (var item in ActionClaimsHelper.GetAll<Role>())
				{
					var role = new Entities.Role { Name = item.ToString(), NormalizedName = item.ToString().ToUpper(), Claims = new List<Entities.RoleClaim>() };

					switch (role.Name)
					{
						case nameof(Role.Admin):
							foreach (var claim in (Organization[])Enum.GetValues(typeof(Organization)))
								role.Claims.Add(new Entities.RoleClaim { ClaimType = claim.GetType().Name, ClaimValue = claim.ToString() });
							break;
						case nameof(Role.User):
							role.Claims.Add(new Entities.RoleClaim { ClaimType = Organization.Read.GetType().Name, ClaimValue = Organization.Read.ToString() });
							role.Claims.Add(new Entities.RoleClaim { ClaimType = Organization.Update.GetType().Name, ClaimValue = Organization.Update.ToString() });
							break;
						case nameof(Role.Viewer):
							role.Claims.Add(new Entities.RoleClaim { ClaimType = Organization.Read.GetType().Name, ClaimValue = Organization.Read.ToString() });
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
