using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObjects;

namespace Presentation.Controllers
{
    [ApiController]
    [Route(template: "api/[Controller]")] // BaseUrl/api/Products

    public class ProductsController(IServiceManager _serviceManager) : ControllerBase
    {
        // Get All Products
        //GET BaseUrl/api/Products
        [HttpGet]

        public async Task<ActionResult<IEnumerable<ProductDTo>>> GetAllProducts()
        {
            var products = await _serviceManager.ProductService.GetAllBrandAsyc();

            return Ok(products);

        }

        // Get Product By Id
        [HttpGet("{id:int}")]
        //GET BaseUrl/api/Products/10
        public async Task<ActionResult<ProductDTo>> GetProduct(int id)
        {
            var product = await _serviceManager.ProductService.GetProductByIdAsync(id);
            return Ok(product);
        }




        // Get All Types
        [HttpGet("types")]
        //GET BaseUrl/api/Products/types
        public async Task<ActionResult<IEnumerable<TypeDTo>>> GetTypes()
        {
            var Types = await _serviceManager.ProductService.GetAllTypesAsync();
            return Ok(Types);

        }


        // Get All Brands
        //GET BaseUrl/api/Products/brand

        [HttpGet("brands")]

        public async Task<ActionResult<IEnumerable<BrandDTo>>> GetBrands()
        {
            var Brands = await _serviceManager.ProductService.GetAllBrandAsyc();
            return Ok(Brands);
        }

    }
}
