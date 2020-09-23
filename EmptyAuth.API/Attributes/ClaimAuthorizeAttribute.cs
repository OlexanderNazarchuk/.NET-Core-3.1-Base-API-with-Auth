using EmptyAuth.Common.Enums.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
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


			using (StreamReader reader = new StreamReader(context.HttpContext.Request.Body))
			{
				string text = reader.ReadToEnd();
			}
			var hasClaim = context.HttpContext.User
				.Claims.Any(c => c.Type == _claim.Type && c.Value == _claim.Value);
			if (!hasClaim)
			{
				context.Result = new ForbidResult();
			}
		}
	}
}
