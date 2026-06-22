using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Entities.ProductModule;
using E_Commerce.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Data.DataSeed
{
    public class DataIntializer : IDataIntializer
    {
        private readonly StoreDbContext _dbContext;

        public DataIntializer(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task IntializeAsync()
        {
            try
            {
                var hasproducts = await _dbContext.Products.AnyAsync();
                var hasTypes = await _dbContext.ProductTypes.AnyAsync();
                var hasBrands = await _dbContext.ProductBrands.AnyAsync();

                if (hasproducts && hasTypes && hasBrands) return;

                if (!hasBrands)
                {
                    await SeedDataFromJsonAsync<ProductBrand, int>("brands.json", _dbContext.ProductBrands);
                }
                if (!hasTypes)
                {
                    await SeedDataFromJsonAsync<ProductType, int>("types.json", _dbContext.ProductTypes);
                }
                await _dbContext.SaveChangesAsync();
                if (!hasproducts)
                {
                    await SeedDataFromJsonAsync<Product, int>("products.json", _dbContext.Products);
                }
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred during data seeding: {ex.Message}");
            }
        }

        private async Task SeedDataFromJsonAsync<TEntity, Tkey>(string FileName, DbSet<TEntity> dbset) where TEntity : BaseEntity<Tkey>
        {
            //D:\Course .NET\08 ASP .Net API\API Project\E Commerce Project\E Commerce.Persistence\Data\DataSeed\JsonFiles\brands.json
            var FilePath = @"..\E Commerce.Persistence\Data\DataSeed\JsonFiles\" + FileName;
            if (!File.Exists(FilePath)) throw new FileNotFoundException($"File {FileName} is not Found");

            try
            {
                using var dataStreams = File.OpenRead(FilePath);
                var data = JsonSerializer.Deserialize<List<TEntity>>(dataStreams, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                if (data is not null)
                {
                    await dbset.AddRangeAsync(data);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while seeding data from {FileName}: {ex.Message}");
            }

        }
    }
}
