using ExploreNorthwind.Models;
using ExploreNorthwindDataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ExploreNorthwind.ControllersAPI
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CategoriesAPIController : Controller
    {
        private ICategoriesRepository categoriesRepo { get; }
        public CategoriesAPIController(ICategoriesRepository categoriesRepo)
        {
            this.categoriesRepo = categoriesRepo;
        }

        [HttpGet]
        public List<CategoryDTO> Category()
        {
            var dataCategories = categoriesRepo.GetAll();
            var dtoCategories = new List<CategoryDTO>();
            foreach (var item in dataCategories)
            {
                dtoCategories.Add(new CategoryDTO(item));
            }
            return dtoCategories;
        }

        [HttpGet]
        public byte[] Picture(int id)
        {
            var picture = categoriesRepo.GetPictureByCategoryId(id);
            return picture;
        }

        [HttpPut]
        public IActionResult Picture(int id, byte[] picture)
        {
            categoriesRepo.AddPicture(id, picture);
            return Ok();
        }
    }
}
