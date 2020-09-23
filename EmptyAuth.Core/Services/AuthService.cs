using EmptyAuth.Common.Configurations;
using EmptyAuth.Core.Helpers;
using EmptyAuth.Core.Interfaces;
using EmptyAuth.Data.Entities;
using EmptyAuth.Models.AuthModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EmptyAuth.Core.Services
{
	public class AuthService: IAuthService
	{

		private readonly TokenAuthConfiguration _configuration;
		private readonly SignInManager<User> _signInManager;
		private readonly UserManager<User> _userManager;
		private readonly RoleManager<Role> _roleManager;

		public AuthService(
			TokenAuthConfiguration configuration,
			SignInManager<User> signInManager,
			UserManager<User> userManager,
			RoleManager<Role> roleManager
			)
		{
			_configuration = configuration;
			_signInManager = signInManager;
			_userManager = userManager;
			_roleManager = roleManager;
		}

		public async Task<string> LoginAsync(string username, string password)
		{
			var appUser = await _userManager.FindByEmailAsync(username);
			if (appUser != null)
			{
				var loginResult = await _signInManager.PasswordSignInAsync(username, password, false, false);
				if (loginResult.Succeeded)
				{
					var claims = await _userManager.GetClaimsAsync(appUser);
					var userDto = new UserDto 
					{ 
						Id = appUser.Id, 
						Name = appUser.UserName, 
						Email = appUser.Email,
						Claims = claims, OrganizationId = (int)appUser.OrganizationId 
					};
					var token = await BuildTokenHelper.CreateJwtAsync(userDto, _configuration.Issuer, _configuration.Audience, _configuration.SigningCredentials, _configuration.Expiration);
					return token;
				}
			}
			return null;
		}

		public async Task CreateAsync(string username, string password, int organizationId)
		{
			var appUser = await _userManager.FindByEmailAsync(username);
			if (appUser == null)
			{
				appUser = new User { UserName = username, Email = username, OrganizationId = organizationId };
				var result = await _userManager.CreateAsync(appUser, password);
				if (result.Succeeded)
				{
					var role = await _roleManager.FindByNameAsync(Common.Enums.Role.Admin.ToString());
					var claims = await _roleManager.GetClaimsAsync(role);
					await _userManager.AddClaimsAsync(appUser, claims);
					return;
				}
			}

			throw new Exception();
		}
	}
}
