using System;
using System.Collections.Generic;
using System.Linq;

namespace EmptyAuth.Data.Extensions
{
	public static class ActionClaimsHelper
	{
		public static IEnumerable<T> GetAll<T>()
		{
			return Enum.GetValues(typeof(T)).Cast<T>();
		}

	}
}
