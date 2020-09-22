using EmptyAuth.Data.Configurations;
using EmptyAuth.Data.Entities;
using EmptyAuth.Data.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyAuth.Data
{
    public class AppDbContext : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public AppDbContext(DbContextOptions options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //AUTH
            builder.AddConfiguration(new UserConfiguration());
            builder.AddConfiguration(new UserRoleConfigration());
            builder.AddConfiguration(new UserClaimConfigration());
            builder.AddConfiguration(new UserTokenConfigration());
            builder.AddConfiguration(new UserLoginConfigration());
            builder.AddConfiguration(new RoleConfigration());
            builder.AddConfiguration(new RoleClaimConfigration());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserClaim> UserClaims { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
        public DbSet<UserLogin> UserLogins { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleClaim> RoleClaims { get; set; }
    }
}
