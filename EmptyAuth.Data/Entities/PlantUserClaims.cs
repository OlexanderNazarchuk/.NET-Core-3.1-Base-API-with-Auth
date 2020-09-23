using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyAuth.Data.Entities
{
	public class PlantUserClaim
	{
		public int Id { get; set; }
		public int PlantId { get; set; }
		public int UserId { get; set; }
		public string ClaimType { get; set; }
		public string ClaimValue { get; set; }

		public User User { get; set; }
		public Plant Plant { get; set; }
	}
}
