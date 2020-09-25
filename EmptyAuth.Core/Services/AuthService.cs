using EmptyAuth.Common.Configurations;
using EmptyAuth.Core.Helpers;
using EmptyAuth.Core.Interfaces;
using EmptyAuth.Data;
using EmptyAuth.Data.Entities;
using EmptyAuth.Models.AuthModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
		private readonly AppDbContext _context;

		public AuthService(
			AppDbContext context,
			TokenAuthConfiguration configuration,
			SignInManager<User> signInManager,
			UserManager<User> userManager,
			RoleManager<Role> roleManager
			)
		{
			_context = context;
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
					var organizationClaims = await _context.UserClaims.Where(x => x.UserId == appUser.Id)
						.Select(x=> x.ClaimValue)
						.ToListAsync();

					var plantClaims = await _context.Organizations.Select(x => new OrganizationAuthDto
					{
						Id = x.Id,
						Plants = x.Plants.Select(x2 => new PlantAuthDto
						{
							Id = x2.Id,
							Claims = x2.PlantUserClaims.Where(x=>x.UserId==appUser.Id).Select(x3 => x3.ClaimValue).ToList(),
						}).ToList(),
					})
					.FirstOrDefaultAsync(x => x.Id == appUser.OrganizationId);
					var userDto = new UserDto 
					{ 
						Id = appUser.Id, 
						Name = appUser.UserName, 
						Email = appUser.Email,
						Organization = new OrganizationAuthDto()
						{
							Id = (int)appUser.OrganizationId,
							Claims = organizationClaims,
							Plants = plantClaims.Plants,
						}
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
