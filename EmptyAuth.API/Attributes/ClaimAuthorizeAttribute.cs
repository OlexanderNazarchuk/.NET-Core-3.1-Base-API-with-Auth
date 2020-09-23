using EmptyAuth.Common.Enums.Claims;
using EmptyAuth.Models.AuthModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace EmptyAuth.API.Attributes
{
	public class ClaimAuthorizeAttribute : TypeFilterAttribute
	{
		public ClaimAuthorizeAttribute(params Organization[] claims) : base(typeof(ClaimRequirementFilter))
		{
			foreach (var claim in claims)
				Arguments = new object[] { new Claim(typeof(Organization).Name, claim.ToString()) };
		}
	}

	public class ClaimRequirementFilter : IAuthorizationFilter
	{
		private readonly Claim _claim;

		public ClaimRequirementFilter(Claim claim)
		{
			_claim = claim;
		}

		public void OnAuthorization(AuthorizationFilterContext context)
		{
			var permissionsClaim = context.HttpContext.User
				.Claims.FirstOrDefault(c => c.Type == "Permissions").Value;
			var org = JsonConvert.DeserializeObject<OrganizationDto>(permissionsClaim);

			var hasClaim = org.Claims.Any(x => x == _claim.Type+"."+_claim.Value);
			if (!hasClaim)
			{
				context.Result = new ForbidResult();
			}
		}
	}
}
