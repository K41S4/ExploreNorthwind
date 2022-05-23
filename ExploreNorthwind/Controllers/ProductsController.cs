using ExploreNorthwind.ConfigurationOptions;
using ExploreNorthwind.Models;
using ExploreNorthwind.Models.Repositories;
using ExploreNorthwind.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System;
using System.Linq;

namespace ExploreNorthwind.Controllers
{
    public class ProductsController : Controller
    {
        private ProductsRepository productsRepo { get; }
        private CategoriesRepository categoriesRepo { get; }
        private SuppliersRepository suppliersRepo { get; }
        private ExploreNorthwindOptions Options { get; set; }
        public ProductsController(ProductsRepository productsRepo, SuppliersRepository suppliersRepo, CategoriesRepository categoriesRepo, IOptionsSnapshot<ExploreNorthwindOptions> options)
        {
            this.productsRepo = productsRepo;
            this.suppliersRepo = suppliersRepo;
            this.categoriesRepo = categoriesRepo;
            this.Options = options.Value;
        }

        public IActionResult Index()
        {
            var products = productsRepo.Get(Options.ProductsMaxCount);
            return View(products);
        }

        public IActionResult Create(ProductViewModel product)
        {
            product.InitializeSelectLists(suppliersRepo.GetAll(), categoriesRepo.GetAll());
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            var dataProduct = product.GetProduct(); 
            productsRepo.Create(dataProduct);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Update(int productId)
        {
            var product = productsRepo.GetById(productId);
            if (product == null) RedirectToAction(nameof(Index));
            var productView = new ProductViewModel(product, suppliersRepo.GetAll(), categoriesRepo.GetAll());
            return View(productView);
        }
        
        [HttpPost]
        public IActionResult Update(ProductViewModel product)
        {
            product.InitializeSelectLists(suppliersRepo.GetAll(), categoriesRepo.GetAll());
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            var dataProduct = product.GetProduct();
            productsRepo.Update(dataProduct);
            return RedirectToAction(nameof(Index));
        }
    }
}
