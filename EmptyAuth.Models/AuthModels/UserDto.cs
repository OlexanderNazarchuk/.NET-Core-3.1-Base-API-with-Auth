using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace EmptyAuth.Models.AuthModels
{
	public class UserDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public int OrganizationId { get; set; }
		public IEnumerable<Claim> Claims { get; set; }
	}
}
