using Microsoft.AspNetCore.Identity;
using System;

namespace EmptyAuth.Data.Entities
{
	public class UserRole : IdentityUserRole<int>
	{
		public User User { get; set; }
		public Role Role { get; set; }

	}
}
