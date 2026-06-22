using E_Commerce.Presentation.Attributes;
using E_Commerce.Services_Abstraction.Interfaces;
using E_Commerce.Shared;
using E_Commerce.Shared.DTOs.ProductDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Controllers
{
    public class ProductsController : ApiBaseController
    {
        private readonly IProductServices _productServices;

        public ProductsController(IProductServices productServices)
        {
            _productServices = productServices;
        }

        [HttpGet]
        [RedisCache]
        public async Task<ActionResult<PaginatedResult<ProductDTO>>> GetProducts([FromQuery]ProductQueryParms queryParms)
        {
            var Products = await _productServices.GetAllProductsAsync(queryParms);
            return Ok(Products);
        }

        [HttpGet("{id}")]
        [RedisCache]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id) 
        {
            //throw new Exception();
            var Product = await _productServices.GetProductByIdAsync(id);
            return HandleResult<ProductDTO>(Product);
        }

        [HttpGet("brands")]
        [RedisCache]
        public async Task<ActionResult<IEnumerable<BrandDTO>>> GetAllBrands() 
        {
            var Brands = await _productServices.GetAllBrandsAsync();
            return Ok(Brands);
        }

        [HttpGet("types")]
        [RedisCache]
        public async Task<ActionResult<IEnumerable<TypeDTO>>> GetAllTypes()
        {
            var Types = await _productServices.GetAllTypesAsync();
            return Ok(Types);
        }

    }
}
