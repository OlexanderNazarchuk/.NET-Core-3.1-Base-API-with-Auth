using EmptyAuth.Models;
using EmptyAuth.Models.AuthModels;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EmptyAuth.API.Extensions
{
	public static  class RequestControllerExtension
	{
		public static AuthRequest<TModel> GetRequestData<TModel>(this HttpContext context, TModel de)
		{

			var permissionsClaim = context.User
				.Claims.FirstOrDefault(c => c.Type == "Permissions").Value;
			var org = JsonConvert.DeserializeObject<OrganizationAuthDto>(permissionsClaim);

			var userId = context.User.Claims.FirstOrDefault(c => c.Type == "nameid");

			return new AuthRequest<TModel>() { Permission = org, Model = de, UserId = int.Parse(userId.Value) };
		}
		public static AuthRequestBase GetRequestData(this HttpContext context)
		{

			var permissionsClaim = context.User
				.Claims.FirstOrDefault(c => c.Type == "Permissions").Value;
			var org = JsonConvert.DeserializeObject<OrganizationAuthDto>(permissionsClaim);
			var userId = context.User.Claims.FirstOrDefault(c => c.Type == "nameid");

			return new AuthRequestBase() { Permission = org, UserId = int.Parse(userId.Value)};
		}
	}
}
