using EmptyAuth.Data.Entities;
using EmptyAuth.Models.AuthModels;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EmptyAuth.Core.Helpers
{
	public static class BuildTokenHelper
	{
		internal static async Task<string> CreateJwtAsync(
				UserDto user,
				string issuer,
				string authority,
				SigningCredentials symSec,
				TimeSpan daysValid)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var claims = await CreateClaimsIdentities(user);

			// Create JWToken
			var token = tokenHandler.CreateJwtSecurityToken(issuer: issuer,
				audience: authority,
				subject: claims,
				notBefore: DateTime.UtcNow,
				expires: DateTime.UtcNow.Add(daysValid),
				signingCredentials: symSec);

			return tokenHandler.WriteToken(token);
		}

		private static Task<ClaimsIdentity> CreateClaimsIdentities(UserDto user)
		{

			ClaimsIdentity claimsIdentity = new ClaimsIdentity();

			claimsIdentity.AddClaims(user.Claims);

			claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, user.Name));
			claimsIdentity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
			claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
			claimsIdentity.AddClaim(new Claim("OrganizationId", user.OrganizationId.ToString()));

			return Task.FromResult(claimsIdentity);
		}

	}
}
