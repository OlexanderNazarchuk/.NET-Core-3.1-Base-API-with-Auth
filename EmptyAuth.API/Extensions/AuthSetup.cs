using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyAuth.Common.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace EmptyAuth.API.Extensions
{
    public static class AuthSetup
    {
        public static void ConfigureAuth(this IServiceCollection services, IConfiguration configuration)
        {
            if (bool.Parse(configuration["Authentication:JwtBearer:IsEnabled"]))
            {
                var tokenAuthConfig = new TokenAuthConfiguration
                {
                    SecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Authentication:JwtBearer:SecurityKey"])),
                    Issuer = configuration["Authentication:JwtBearer:Issuer"],
                    Audience = configuration["Authentication:JwtBearer:Audience"]
                };
                tokenAuthConfig.SigningCredentials = new SigningCredentials(tokenAuthConfig.SecurityKey, SecurityAlgorithms.HmacSha256);
                tokenAuthConfig.Expiration = TimeSpan.FromDays(int.Parse(configuration["Authentication:JwtBearer:DaysValid"]));
                services.AddSingleton(tokenAuthConfig);

                JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

                services.AddAuthentication(options => {
                    options.DefaultAuthenticateScheme = "JwtBearer";
                    options.DefaultChallengeScheme = "JwtBearer";
                }).AddJwtBearer("JwtBearer", options => {
                    options.Audience = tokenAuthConfig.Audience;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = tokenAuthConfig.SecurityKey,
                        ValidateIssuer = true,
                        ValidIssuer = tokenAuthConfig.Issuer,
                        ValidateAudience = true,
                        ValidAudience = tokenAuthConfig.Audience,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });
            }
        }
    }
}
