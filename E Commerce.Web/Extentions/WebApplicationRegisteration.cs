using E_Commerce.Domain.Contracts;
using E_Commerce.Persistence.Data.DbContexts;
using E_Commerce.Persistence.IdentityData.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Web.Extentions
{
    public static class WebApplicationRegisteration
    {
        public static async Task<WebApplication> MigrateDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateAsyncScope();
            var DbContextService = scope.ServiceProvider.GetRequiredService<StoreDbContext>();
            var pendingMigrations = await DbContextService.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
            {
                await DbContextService.Database.MigrateAsync();
            }
            return app;
        }

        public static async Task<WebApplication> SeedDataAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateAsyncScope();
            var DataIntializerService = scope.ServiceProvider.GetRequiredKeyedService<IDataIntializer>("Default");
            await DataIntializerService.IntializeAsync();
            return app;
        }
        public static async Task<WebApplication> SeedIdentityDataAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateAsyncScope();
            var DataIntializerService = scope.ServiceProvider.GetRequiredKeyedService<IDataIntializer>("Identity");
            await DataIntializerService.IntializeAsync();
            return app;
        }

        public static async Task<WebApplication> MigrateIdentityDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateAsyncScope();
            var DbContextService = scope.ServiceProvider.GetRequiredService<StoreIdentityDbContext>();
            var pendingMigrations = await DbContextService.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
            {
                await DbContextService.Database.MigrateAsync();
            }
            return app;
        }
    }
}
