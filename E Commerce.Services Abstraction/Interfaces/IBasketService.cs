using E_Commerce.Shared.DTOs.BasketDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services_Abstraction.Interfaces
{
    public interface IBasketService
    {
        Task<BasketDTO> GetBasketAsync(string basketId);
        Task<BasketDTO> CreateOrUpdateBasketAsync(BasketDTO basket);
        Task<bool> DeleteBasketAsync(string basketId);
    }
}
