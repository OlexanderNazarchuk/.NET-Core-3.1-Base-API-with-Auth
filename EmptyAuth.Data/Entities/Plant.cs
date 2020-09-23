using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyAuth.Data.Entities
{
	public class Plant
	{
		public int Id { get; set; }
		public int OrganizationId { get; set; }

		public string Name { get; set; }

		public Organization Organization { get; set; }
		public ICollection<PlantUserClaim> PlantUserClaims { get; set; }
	}
}
