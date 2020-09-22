using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmptyAuth.Core.Interfaces
{
	public interface IAuthService
	{
		Task<string> LoginAsync(string username, string password);
		Task RegisterAsync(string username, string password);
	}
}
