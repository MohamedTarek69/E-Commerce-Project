using E_Commerce.Shared;
using E_Commerce.Shared.CommonResult;
using E_Commerce.Shared.DTOs.ProductDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services_Abstraction.Interfaces
{
    public interface IProductServices
    {
        // Get All Products Return IEnumerable Of Products Data Which Will be
        // {Id , Name, Description , PictureUrl , Price , ProductBrand, ProductType} 
        Task<PaginatedResult<ProductDTO>> GetAllProductsAsync(ProductQueryParms queryParms);

        // Get Product By Id Return Product Data Which Will be
        // {Id , Name, Description , PictureUrl , Price , ProductBrand, ProductType} 
        Task<Result<ProductDTO >> GetProductByIdAsync(int id);

        // Get All Brands Return IEnumerable Of Brands Data Which Will be {Id , Name}
        Task<IEnumerable<BrandDTO>> GetAllBrandsAsync();

        // Get All Types Return IEnumerable Of Types Data Which Will be {Id , Name}
        Task<IEnumerable<TypeDTO>> GetAllTypesAsync();

    }
}
