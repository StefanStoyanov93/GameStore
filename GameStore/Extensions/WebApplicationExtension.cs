using GameStore.Data.Data;
using GameStore.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Extensions
{
    public static class WebApplicationExtension
    {
        public static WebApplication SeedData(this WebApplication app)
        {
            var scoped = app.Services.CreateScope();
            var serviceProvider = scoped.ServiceProvider;

            MigrateDatabase(serviceProvider);

            CreateAndSeedDataManager(serviceProvider);

            return app;
        }

        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services.GetRequiredService<GameStoreDbContext>();

            data.Database.Migrate();
        }

        private static void CreateAndSeedDataManager(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync("DataManager"))
                    {
                        return;
                    }

                    var role = new IdentityRole { Name = "DataManager" };

                    await roleManager.CreateAsync(role);

                    const string dmanagerEmail = "datamanager@abv.com";
                    const string dmanagerPassword = "dmanager101";

                    var user = new User
                    {
                        Email = dmanagerEmail,
                        UserName = dmanagerEmail,
                        CountryId = 4,
                        BirthDate = DateTime.Now
                    };

                    await userManager.CreateAsync(user, dmanagerPassword);

                    await userManager.AddToRoleAsync(user, role.Name);
                })
                .GetAwaiter()
                .GetResult();
        }
    }
}
