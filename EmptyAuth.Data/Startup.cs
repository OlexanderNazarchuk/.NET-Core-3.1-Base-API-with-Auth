using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Identity;
using EmptyAuth.Data.Entities;
using EmptyAuth.Data.Seeders;

namespace EmptyAuth.Data
{
	public static class Startup
	{
		public static void ConfigureData(this IServiceCollection service, string connectionString)
		{
			service.AddDbContext<AppDbContext>(x => x.UseSqlServer(connectionString));

			service.AddIdentity<User, Role>(options =>
			{
				// Password settings
				options.Password.RequiredLength = 8;
				options.Password.RequiredUniqueChars = 0;
				options.Password.RequireLowercase = true;
				options.Password.RequireDigit = true;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireUppercase = true;
			})
				.AddDefaultTokenProviders()
				.AddEntityFrameworkStores<AppDbContext>();
		}
		public static void InitializeDatabase(this IApplicationBuilder app)
		{
			using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
			{

				scope.ServiceProvider.GetRequiredService<AppDbContext>().Database.Migrate();

				var context = scope.ServiceProvider.GetService<AppDbContext>();
				//SEEDRS
				RoleSeeder.Seed(context);
			}
		}
	}
}
