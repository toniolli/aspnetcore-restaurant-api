using Microsoft.AspNetCore.Identity;

namespace Infra.Identity
{
    public static class SeedIdentity
    {
        public const string AdminRole = "Admin";
       
        public static async Task SeedRolesAsync(
            RoleManager<IdentityRole> roleManager)
        {
            string[] roles =
            {
                AdminRole
               
            };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(
                        new IdentityRole(role));
                }
            }
        }

        public static async Task SeedAdminAsync(
            UserManager<ApplicationUser> userManager)
        {
            string adminUserName = "admin";
            string adminEmail = "admin@sistema.local";
            string adminPassword = "NovaSenha123@";

            var admin =
                await userManager.FindByNameAsync(
                    adminUserName);

            admin ??=
                await userManager.FindByEmailAsync(
                    adminEmail);

            if (admin == null)
            {
                var newAdmin = new ApplicationUser
                {
                    UserName = adminUserName,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result =
                    await userManager.CreateAsync(
                        newAdmin,
                        adminPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(
                        newAdmin,
                        AdminRole);
                }
            }
        }   
    }
}