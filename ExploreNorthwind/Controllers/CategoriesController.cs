using ExploreNorthwind.Models;
using ExploreNorthwind.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ExploreNorthwind.Controllers
{
    public class CategoriesController : Controller
    {
        private CategoriesRepository categoriesRepo { get; }
        public CategoriesController(CategoriesRepository categoriesRepo)
        {
            this.categoriesRepo = categoriesRepo;
        }

        public IActionResult Index()
        {
            List<Category> categories = categoriesRepo.GetAll();
            return View(categories);
        }
    }
}
