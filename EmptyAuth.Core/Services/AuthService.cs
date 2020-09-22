using EmptyAuth.Common.Configurations;
using EmptyAuth.Core.Helpers;
using EmptyAuth.Core.Interfaces;
using EmptyAuth.Data.Entities;
using EmptyAuth.Models.AuthModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
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

		public async Task<string> Login(string username, string password)
		{
			var appUser = await _userManager.FindByEmailAsync(username);
			if (appUser != null)
			{
				var loginResult = await _signInManager.PasswordSignInAsync(username, password, false, false);
				if (loginResult.Succeeded)
				{
					var userDto = new UserDto { Id = appUser.Id, Name = appUser.UserName };
					var token = await BuildTokenHelper.CreateJwtAsync(userDto, _configuration.Issuer, _configuration.Audience, _configuration.SigningCredentials, _configuration.Expiration);
					return token;
				}
			}
			throw new Exception();
		}

		public async Task Register(string username, string password)
		{
			var appUser = await _userManager.FindByEmailAsync(username);
			if (appUser == null)
			{
				appUser = new User { UserName = username, Email = username };
				var result = await _userManager.CreateAsync(appUser, password);
				if (result.Succeeded)
				{
					var role = await _roleManager.FindByNameAsync("OrganizationOwner");
					var claims = await _roleManager.GetClaimsAsync(role);
					await _userManager.AddClaimsAsync(appUser, claims);
					return;
				}
			}

			throw new Exception();
		}
	}
}
