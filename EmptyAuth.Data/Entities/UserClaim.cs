using Microsoft.AspNetCore.Identity;
using System;

namespace EmptyAuth.Data.Entities
{
	public class UserClaim : IdentityUserClaim<int>
	{
		public User User { get; set; }
	}
}
