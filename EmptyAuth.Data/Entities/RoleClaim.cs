using Microsoft.AspNetCore.Identity;
using System;

namespace EmptyAuth.Data.Entities
{
	public class RoleClaim : IdentityRoleClaim<int>
	{
		public Role Role { get; set; }
	}
}
