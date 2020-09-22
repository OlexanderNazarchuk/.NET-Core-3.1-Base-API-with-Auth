using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmptyAuth.API.Middlewares
{
	public class HttpStatusCodeExceptionMiddleware
	{
		private readonly RequestDelegate next;

		public HttpStatusCodeExceptionMiddleware(RequestDelegate next)
		{
			this.next = next;
		}

		public async Task Invoke(HttpContext context /* other dependencies */)
		{
			try
			{
				await next(context);
			}
			//catch (HttpStatusCodeException ex)
			//{
			//	await HandleExceptionAsync(context, ex);
			//}
			catch (Exception exceptionObj)
			{
				//await HandleExceptionAsync(context, exceptionObj);
			}
		}
	}
}
