using ExploreNorthwindDataAccess.Models;
using ExploreNorthwindDataAccess.NorthwindDB;
using ExploreNorthwindDataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExploreNorthwindDataAccess.Repositories
{
    public class CategoriesRepository: ICategoriesRepository
    {
        private INorthwindContext Context { get; }
        public CategoriesRepository(INorthwindContext northwindContext)
        {
            this.Context = northwindContext;
        }

        public List<Category> GetAll()
        {
            return Context.Categories.ToList();
        }
    }
}
