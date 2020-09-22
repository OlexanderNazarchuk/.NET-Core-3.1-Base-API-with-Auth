using EmptyAuth.API.Middlewares;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmptyAuth.API.Extensions
{
	public static class HttpStatusCodeExceptionMiddlewareExtensions
	{
		public static IApplicationBuilder UseHttpStatusCodeExceptionMiddleware(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<HttpStatusCodeExceptionMiddleware>();
		}
	}
}
