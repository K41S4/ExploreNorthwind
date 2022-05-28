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
            var dataCategories = categoriesRepo.GetAll();
            var dtoCategories = new List<CategoryDTO>();
            foreach (var item in dataCategories)
            {
                dtoCategories.Add(new CategoryDTO(item));
            }
            return View(dtoCategories);
        }
    }
}
