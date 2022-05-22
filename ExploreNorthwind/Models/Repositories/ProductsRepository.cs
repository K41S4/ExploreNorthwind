using ExploreNorthwind.Models.NorthwindDB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExploreNorthwind.Models.Repositories
{
    public class ProductsRepository
    {
        private NorthwindContext Context { get; }
        public ProductsRepository(NorthwindContext northwindContext)
        {
            this.Context = northwindContext;
        }

        public List<Product> GetAll()
        {
            return Context.Products.Include(w => w.Category).Include(w => w.Supplier).ToList();
        }
    }
}
