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

        public List<ProductDTO> Get()
        {
            var dataProducts = productsRepo.Get(options.ProductsMaxCount);
            var dtoProducts = new List<ProductDTO>();
            foreach (var item in dataProducts)
            {
                dtoProducts.Add(new ProductDTO(item));
            }
            return dtoProducts;
        }
    }
}
