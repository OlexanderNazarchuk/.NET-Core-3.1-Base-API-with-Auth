using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using EmptyAuth.API.Filters;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace EmptyAuth.API.Extensions
{
	public static class SwaggerSetup
	{
		public static void ConfigureSwagger(this IServiceCollection services, string projectName)
		{
			services.AddSwaggerGen(
				options =>
				{
					// Resolve the temporary IApiVersionDescriptionProvider service  
					var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

					// Add a swagger document for each discovered API version  
					foreach (var description in provider.ApiVersionDescriptions)
					{

						options.SwaggerDoc(description.GroupName, new OpenApiInfo()
						{
							Title = "EmptyAuth API",
							Version = $"v{description.ApiVersion.ToString()}",
							Description = "API Documentation - EmptyAuth",
							Contact = new OpenApiContact() { Name = "Aleksandr Nazarchuk", Url = new Uri("https://www.linkedin.com/in/aleksander-nazarchuk-4867aa139/") },
						});
						options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
						{
							Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
											Enter 'Bearer' [space] and then your token in the text input below.
											\r\n\r\nExample: 'Bearer 12345abcdef'",
							Name = "Authorization",
							In = ParameterLocation.Header,
							Type = SecuritySchemeType.ApiKey,
							Scheme = "Bearer"
						});

						options.AddSecurityRequirement(new OpenApiSecurityRequirement()
						  {
							{
							  new OpenApiSecurityScheme
							  {
								Reference = new OpenApiReference
								  {
									Type = ReferenceType.SecurityScheme,
									Id = "Bearer"
								  },
								  Scheme = "oauth2",
								  Name = "Bearer",
								  In = ParameterLocation.Header,

								},
								new List<string>()
							  }
							});

					}

					// Add a custom filter for setting the default values  
					options.OperationFilter<SwaggerDefaultOperationFilter>();
				});
		}
	}
}
