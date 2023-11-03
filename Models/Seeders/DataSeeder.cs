namespace ThesisOct2023.Models.Seeders
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using ThesisOct2023.Models; 
    using Microsoft.AspNetCore.Identity;
    using System;
    using ThesisOct2023.Data;

    public class DataSeeder
    {
        public static void SeedAdminUser(IServiceProvider serviceProvider)
        {
            var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            dbContext.Database.Migrate(); // Ensure the database is created and up to date

            if (!dbContext.Users.Any(u => u.UserName == "admin"))
            {
                var adminUser = new IdentityUser
                {
                    UserName = "admin",
                    // Set other properties as needed
                };

                var result = userManager.CreateAsync(adminUser, "YourAdminPassword").Result;

                if (result.Succeeded)
                {
                    // Assign the admin role to the admin user (if you have a custom role)
                    userManager.AddToRoleAsync(adminUser, "Admin").Wait();
                }
            }
        }
    }

}
