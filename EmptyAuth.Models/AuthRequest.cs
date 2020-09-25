using EmptyAuth.Models.AuthModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyAuth.Models
{
	public class AuthRequestBase
	{
		public OrganizationAuthDto Permission { get; set; }
		public int UserId { get; set; }
	}
	public class AuthRequest<TModel>: AuthRequestBase
	{
		public TModel Model { get; set; }
	}
}
