using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.BasketModule;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;

        public BasketRepository(IConnectionMultiplexer connection)
        {
            _database = connection.GetDatabase();
        }

        public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan TimeToLive = default)
        {
            var BasketAsJson = JsonSerializer.Serialize(basket);
            var IsCreatedOrUpdated = await _database.StringSetAsync(basket.Id, BasketAsJson,
                (TimeToLive == default) ? TimeSpan.FromDays(7) : TimeToLive);
            if (IsCreatedOrUpdated)
            {
                return await GetBasketAsync(basket.Id);
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> DeleteBasketAsync(string basketId) => await _database.KeyDeleteAsync(basketId);

        public async Task<CustomerBasket?> GetBasketAsync(string basketId)
        {
            var Basket = await _database.StringGetAsync(basketId);

            if (string.IsNullOrEmpty(Basket))
            {
                return null;
            }
            else
            {
                return JsonSerializer.Deserialize<CustomerBasket>(Basket!);
            }
        }
    }
}
