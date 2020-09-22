using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyAuth.Data.Entities
{
	public class UserToken : IdentityUserToken<int>
	{
		public User User { get; set; }
	}
}
