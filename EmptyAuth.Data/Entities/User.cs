using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace EmptyAuth.Data.Entities
{
	public class User : IdentityUser<int>
	{
		public ICollection<UserRole> Roles { get; set; }
		public ICollection<UserClaim> Claims { get; set; }
		public ICollection<UserLogin> Logins { get; set; }
		public ICollection<UserToken> Tokens { get; set; }
	}
}
