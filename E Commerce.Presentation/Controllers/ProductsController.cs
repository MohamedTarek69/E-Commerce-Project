using E_Commerce.Services_Abstraction.Interfaces;
using E_Commerce.Shared;
using E_Commerce.Shared.DTOs.ProductDTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductServices _productServices;

        public ProductsController(IProductServices productServices)
        {
            _productServices=productServices;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedResult<ProductDTO>>> GetProducts([FromQuery]ProductQueryParms queryParms)
        {
            var Products = await _productServices.GetAllProductsAsync(queryParms);
            return Ok(Products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id) 
        {
            var Product = await _productServices.GetProductByIdAsync(id);
            return Ok(Product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<BrandDTO>>> GetAllBrands() 
        {
            var Brands = await _productServices.GetAllBrandsAsync();
            return Ok(Brands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<TypeDTO>>> GetAllTypes()
        {
            var Types = await _productServices.GetAllTypesAsync();
            return Ok(Types);
        }

    }
}
