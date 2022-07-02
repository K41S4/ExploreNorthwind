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

        public List<CategoryDTO> Get()
        {
            var dataCategories = categoriesRepo.GetAll();
            var dtoCategories = new List<CategoryDTO>();
            foreach (var item in dataCategories)
            {
                dtoCategories.Add(new CategoryDTO(item));
            }
            return dtoCategories;
        }
    }
}
