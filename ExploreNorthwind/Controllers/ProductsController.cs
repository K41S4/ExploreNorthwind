using ExploreNorthwind.Models;
using ExploreNorthwind.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ExploreNorthwind.Controllers
{
    public class ProductsController : Controller
    {
        private ProductsRepository productsRepo { get; }
        public ProductsController(ProductsRepository productsRepo)
        {
            this.productsRepo = productsRepo;
        }

        public IActionResult Index()
        {
            List<Product> products = productsRepo.GetAll();
            return View(products);
        }
    }
}
