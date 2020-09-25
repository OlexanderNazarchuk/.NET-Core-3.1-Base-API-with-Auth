using EmptyAuth.Core.Interfaces;
using EmptyAuth.Core.Services;
using EmptyAuth.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EmptyAuth.Core
{
	public static class Startup
	{
		public static void ConfigureServices(this IServiceCollection services, string connectionString)
		{
			services.ConfigureData(connectionString);

			services.AddTransient<IAuthService, AuthService>();
			services.AddTransient<IPlantService, PlantService>();
		}
		public static void InitializeServices(this IApplicationBuilder app)
		{
			app.InitializeDatabase();
		}
	}
}
