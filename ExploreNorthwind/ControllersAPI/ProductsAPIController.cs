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
    [Route("api/[controller]/[action]")]
    public class ProductsAPIController : ControllerBase
    {
        private IProductsRepository productsRepo { get; }
        private ExploreNorthwindOptions options { get; set; }
        public ProductsAPIController(IProductsRepository productsRepo, IOptionsSnapshot<ExploreNorthwindOptions> options)
        {
            this.productsRepo = productsRepo;
            this.options = options.Value;
        }

        [HttpGet]
        public List<ProductDTO> Product()
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
        public IActionResult Product(ProductDTO product)
        {
            var dataProduct = product.GetProduct();
            productsRepo.Create(dataProduct);
            return Ok(product);
        }

        [HttpPut]
        public IActionResult Product(int id, [FromBody]ProductDTO product)
        {
            product.ProductID = id;
            var dataProduct = product.GetProduct();
            productsRepo.Update(dataProduct);
            return Ok(product);
        }

        [HttpDelete]
        public IActionResult Product(int id)
        {
            productsRepo.Delete(id);
            return Ok();
        }
    }
}
