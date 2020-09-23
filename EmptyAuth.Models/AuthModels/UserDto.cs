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
		public OrganizationDto Organization { get; set; }
	}

	public class OrganizationDto
	{
		public int Id { get; set; }
		public IList<PlantDto> Plants { get; set; }
		public IList<string> Claims { get; set; }

	}

	public class PlantDto
	{
		public int Id { get; set; }
		public IList<string> Claims { get; set; }
		
	}
}
