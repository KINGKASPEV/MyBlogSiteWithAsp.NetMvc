using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Data
{
	public class AuthDbContext : IdentityDbContext
	{
		public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			//  seed Roles (User, Admin, SuperAdmin)

			var adminRoleId = "4584b312-1d49-4b92-b3a9-1b6213bd5e59";
			var superAdminRoleId = "fe367fc0-8505-467e-a512-3ae0e5fffc06";
			var userRoleId = "761b853f-be84-4415-8e2a-54571844b0d8\r\n";

			var roles = new List<IdentityRole>
			 {
				 new IdentityRole
				 {
					 Name = "Admin",
					 NormalizedName = "Admin",
					 Id = adminRoleId,
					 ConcurrencyStamp = adminRoleId
				 },
				 new IdentityRole
				 {
					 Name= "SuperAdmin",
					 NormalizedName = "SuperAdmin",
					 Id = superAdminRoleId,
					 ConcurrencyStamp = superAdminRoleId
				 },
				 new IdentityRole
				 {
					 Name = "User",
					 NormalizedName = "User",
					 Id =userRoleId,
					 ConcurrencyStamp = userRoleId
				 }

			 };

			builder.Entity<IdentityRole>().HasData(roles);

			// seed SuperAdminUser
			var superAdminId = "d37cb911-1ee8-4b0b-b701-0af8f6e45d0e";
			var superAdminUser = new IdentityUser
			{
				UserName = "superadminkingsley@bloggie.com",
				Email = "superadminkingsley@bloggie.com",
				NormalizedEmail = "superadminkingsley@bloggie.com".ToUpper(),
				NormalizedUserName = "superadminkingsley@bloggie.com".ToUpper(),
				Id = superAdminId
			};

			superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
				.HashPassword(superAdminUser, "Superadminkingsley@123");

			builder.Entity<IdentityUser>().HasData(superAdminUser);

			//Add All roles to SuperAdminUser
			var superAdminRoles = new List<IdentityUserRole<string>>
			{
				new IdentityUserRole<string>
				{
					RoleId = adminRoleId,
					UserId = superAdminId,
				},
				new IdentityUserRole<string>
				{
					RoleId = superAdminRoleId,
					UserId = superAdminId,
				},
				new IdentityUserRole<string>
				{
					RoleId = userRoleId,
					UserId = superAdminId,
				}
			};

			builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
		}
	}
}
