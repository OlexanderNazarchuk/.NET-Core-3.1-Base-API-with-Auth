using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmptyAuth.API.Extensions;
using EmptyAuth.API.Filters;
using EmptyAuth.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EmptyAuth.API
{
	public class Startup
	{

		private readonly IWebHostEnvironment _env;
		public IConfiguration Configuration { get; }

		public Startup(IWebHostEnvironment env)
		{
			_env = env;

			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
				.AddEnvironmentVariables();

			Configuration = builder.Build();
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{

			//services.AddApplicationInsightsTelemetry();
			services.AddHttpClient();

			services
				.AddControllers(opt=> opt.Filters.Add(typeof(ModelStateValidationFilter)))
				.SetCompatibilityVersion(CompatibilityVersion.Latest);


			services.AddVersionedApiExplorer(options =>
			{
				options.GroupNameFormat = "'v'VVV";
				options.DefaultApiVersion = new ApiVersion(1, 0);
				options.SubstituteApiVersionInUrl = false;
				options.AssumeDefaultVersionWhenUnspecified = true;
			});
			services.AddApiVersioning(options =>
			{
				options.ReportApiVersions = false;
				options.AssumeDefaultVersionWhenUnspecified = true;
				options.ApiVersionReader = new HeaderApiVersionReader("api-version");
				options.DefaultApiVersion = new ApiVersion(1, 0);
			});


			var appSettings = Configuration.GetSection("ConnectionStrings");
			services.ConfigureServices(appSettings["SQLConnection"]);

			services.Configure<GzipCompressionProviderOptions>(options => options.Level = System.IO.Compression.CompressionLevel.Optimal);
			services.AddResponseCompression();

			var projectName = GetType().Assembly.GetName().Name;
			services.ConfigureSwagger(projectName);
			services.ConfigureAuth(Configuration);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IServiceProvider serviceProvider, IApiVersionDescriptionProvider provider)
		{
			if (_env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.InitializeServices();

			app.UseHttpsRedirection();
			app.UseHsts();

			app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

			app.UseApiVersioning();

			app.UseSwagger();
			app.UseSwaggerUI(options =>
			{
				options.RoutePrefix = String.Empty;
				//Build a swagger endpoint for each discovered API version  
				foreach (var description in provider.ApiVersionDescriptions)
				{
					options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToLowerInvariant());
				}
			});

			app.UseRouting();

			app.UseHttpStatusCodeExceptionMiddleware();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
