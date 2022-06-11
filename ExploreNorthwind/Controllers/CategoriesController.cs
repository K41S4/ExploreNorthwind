using ExploreNorthwind.Constants;
using ExploreNorthwind.Models;
using ExploreNorthwindDataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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
        
        [HttpGet]
        public FileContentResult Picture(int categoryId)
        {
            var categoryPicture = categoriesRepo.GetPictureByCategoryId(categoryId);
            if (categoryPicture == null) return File(Encoding.ASCII.GetBytes("No picture here!"), "text/plain");
            return File(categoryPicture, ExploreNotrhwindConstants.ImageContentType);
        }

        [HttpGet]
        public IActionResult EditPicture(int categoryId)
        {
            var category = categoriesRepo.GetById(categoryId);
            var categoryDTO = new CategoryDTO(category);
            return View(categoryDTO);
        }

        [HttpPost]
        public IActionResult EditPicture(CategoryDTO category)
        {
            if (category.Picture == null) return View(category);

            byte[] data;
            using (var stream = new MemoryStream()) { 
                category.Picture.CopyTo(stream);
                data = stream.ToArray();
            }
            categoriesRepo.AddPicture(category.CategoryID, data);
            return RedirectToAction("Index");
        }

    }
}
