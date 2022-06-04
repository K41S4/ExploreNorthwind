using ExploreNorthwind.Models;
using ExploreNorthwindDataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ExploreNorthwind.Controllers
{
    public class CategoriesController : Controller
    {
        private ICategoriesRepository categoriesRepo { get; }
        public CategoriesController(ICategoriesRepository categoriesRepo)
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
