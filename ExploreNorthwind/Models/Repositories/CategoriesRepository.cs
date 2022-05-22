using ExploreNorthwind.Models.NorthwindDB;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExploreNorthwind.Models.Repositories
{
    public class CategoriesRepository
    {
        private NorthwindContext Context { get; }
        public CategoriesRepository(NorthwindContext northwindContext)
        {
            this.Context = northwindContext;
        }

        public List<Category> GetAll()
        {
            return Context.Categories.ToList();
        }
    }
}
