using ExploreNorthwind.Models;
using ExploreNorthwindDataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ExploreNorthwind.ControllersAPI
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private ICategoriesRepository categoriesRepo { get; }
        public CategoriesController(ICategoriesRepository categoriesRepo)
        {
            this.categoriesRepo = categoriesRepo;
        }

        [HttpGet]
        public List<CategoryDTO> GetAllCategories()
        {
            var dataCategories = categoriesRepo.GetAll();
            var dtoCategories = new List<CategoryDTO>();
            foreach (var item in dataCategories)
            {
                dtoCategories.Add(new CategoryDTO(item));
            }
            return dtoCategories;
        }

        [HttpGet("{id:int}/Picture")]
        public byte[] GetPicture(int id)
        {
            var picture = categoriesRepo.GetPictureByCategoryId(id);
            return picture;
        }

        [HttpPut("{id:int}/Picture")]
        public IActionResult UpdatePicture(int id, [FromBody] byte[] picture)
        {
            categoriesRepo.AddPicture(id, picture);
            return Ok();
        }
    }
}
