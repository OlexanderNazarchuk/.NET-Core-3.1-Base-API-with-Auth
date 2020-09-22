using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;
using static Microsoft.AspNetCore.Mvc.Versioning.ApiVersionMapping;

namespace EmptyAuth.API.Filters
{
	public class SwaggerDefaultOperationFilter : IOperationFilter
	{
		/// <summary>  
		/// Applies the filter to the specified operation using the given context.  
		/// </summary>  
		/// <param name="operation">The operation to apply the filter to.</param>  
		/// <param name="context">The current operation filter context.</param>  
		public void Apply(OpenApiOperation operation, OperationFilterContext context)
		{
			var apiDescription = context.ApiDescription;
			var apiVersion = apiDescription.GetApiVersion();
			var model = apiDescription.ActionDescriptor.GetApiVersionModel(Explicit | Implicit);

			operation.Deprecated = model.DeprecatedApiVersions.Contains(apiVersion);

			if (operation.Parameters == null)
			{
				return;
			}

			foreach (var parameter in operation.Parameters)
			{
				var description = apiDescription.ParameterDescriptions
					.First(p => p.Name == parameter.Name);

				if (parameter.Description == null)
				{
					parameter.Description = description.ModelMetadata?.Description;
				}


				parameter.Required |= description.IsRequired;
			}
		}
	}
}
