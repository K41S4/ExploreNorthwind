using ExploreNorthwind.ConfigurationOptions;
using ExploreNorthwind.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;

namespace ExploreNorthwind.Controllers
{
    public class ProductsController : Controller
    {
        private ProductsRepository productsRepo { get; }
        private ExploreNorthwindOptions Options { get; set; }
        public ProductsController(ProductsRepository productsRepo, IOptionsSnapshot<ExploreNorthwindOptions> options)
        {
            this.productsRepo = productsRepo;
            this.Options = options.Value;
        }

        public IActionResult Index()
        {
            var products = productsRepo.Get(Options.ProductsMaxCount);
            return View(products);
        }
    }
}
