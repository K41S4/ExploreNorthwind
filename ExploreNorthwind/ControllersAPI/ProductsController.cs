using ExploreNorthwind.ConfigurationOptions;
using ExploreNorthwind.Models;
using ExploreNorthwindDataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace ExploreNorthwind.ControllersAPI
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private IProductsRepository productsRepo { get; }
        private ExploreNorthwindOptions options { get; set; }
        public ProductsController(IProductsRepository productsRepo, IOptionsSnapshot<ExploreNorthwindOptions> options)
        {
            this.productsRepo = productsRepo;
            this.options = options.Value;
        }

        [HttpGet]
        public List<ProductDTO> GetAllProducts()
        {
            var dataProducts = productsRepo.Get(options.ProductsMaxCount);
            var dtoProducts = new List<ProductDTO>();
            foreach (var item in dataProducts)
            {
                dtoProducts.Add(new ProductDTO(item));
            }
            return dtoProducts;
        }

        [HttpPost]
        public IActionResult CreateProduct(ProductDTO product)
        {
            var dataProduct = product.GetProduct();
            productsRepo.Create(dataProduct);
            return Ok(product);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateProduct(int id, [FromBody] ProductDTO product)
        {
            product.ProductID = id;
            var dataProduct = product.GetProduct();
            productsRepo.Update(dataProduct);
            return Ok(product);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteProduct(int id)
        {
            productsRepo.Delete(id);
            return Ok();
        }
    }
}
