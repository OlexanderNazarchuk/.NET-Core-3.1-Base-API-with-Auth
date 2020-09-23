using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyAuth.Data.Entities
{
	public class Organization
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public ICollection<User> Users { get; set; }
		public ICollection<Plant> Plants { get; set; }
	}
}
