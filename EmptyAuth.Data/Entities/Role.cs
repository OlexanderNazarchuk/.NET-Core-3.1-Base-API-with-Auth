using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace EmptyAuth.Data.Entities
{
	public class Role : IdentityRole<int>
	{
		public ICollection<UserRole> Users { get; set; }
		public ICollection<RoleClaim> Claims { get; set; }
	}
}
