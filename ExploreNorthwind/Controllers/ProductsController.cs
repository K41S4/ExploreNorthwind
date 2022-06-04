using ExploreNorthwind.ConfigurationOptions;
using ExploreNorthwind.Models;
using ExploreNorthwindDataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExploreNorthwind.Controllers
{
    public class ProductsController : Controller
    {
        private IProductsRepository productsRepo { get; }
        private ICategoriesRepository categoriesRepo { get; }
        private ISuppliersRepository suppliersRepo { get; }
        private ExploreNorthwindOptions options { get; set; }
        public ProductsController(IProductsRepository productsRepo, ISuppliersRepository suppliersRepo, ICategoriesRepository categoriesRepo, IOptionsSnapshot<ExploreNorthwindOptions> options)
        {
            this.productsRepo = productsRepo;
            this.suppliersRepo = suppliersRepo;
            this.categoriesRepo = categoriesRepo;
            this.options = options.Value;
        }

        public IActionResult Index()
        {
            var dataProducts = productsRepo.Get(options.ProductsMaxCount);
            var dtoProducts = new List<ProductDTO>();
            foreach (var item in dataProducts)
            {
                dtoProducts.Add(new ProductDTO(item));
            }
            return View(dtoProducts);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var productView = new ProductDTO(GetAllSuppliers(), GetAllCategories());

            return View(productView);
        }

        [HttpPost]
        public IActionResult Create(ProductDTO product)
        {
            product.InitializeSelectLists(GetAllSuppliers(), GetAllCategories());
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
            var productView = new ProductDTO(product, GetAllSuppliers(), GetAllCategories());
            return View(productView);
        }
        
        [HttpPost]
        public IActionResult Update(ProductDTO product)
        {
            product.InitializeSelectLists(GetAllSuppliers(), GetAllCategories());
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            var dataProduct = product.GetProduct();
            productsRepo.Update(dataProduct);
            return RedirectToAction(nameof(Index));
        }

        private List<CategoryDTO> GetAllCategories()
        {
            var dataCategories = categoriesRepo.GetAll();
            var dtoCategories = new List<CategoryDTO>();
            foreach (var item in dataCategories)
            {
                dtoCategories.Add(new CategoryDTO(item));
            }
            return dtoCategories;
        }

        private List<SupplierDTO> GetAllSuppliers()
        {
            var dataSuppliers = suppliersRepo.GetAll();
            var dtoSuppliers = new List<SupplierDTO>();
            foreach (var item in dataSuppliers)
            {
                dtoSuppliers.Add(new SupplierDTO(item));
            }
            return dtoSuppliers;
        }
    }
}
