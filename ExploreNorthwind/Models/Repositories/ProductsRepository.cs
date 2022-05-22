using ExploreNorthwind.ConfigurationOptions;
using ExploreNorthwind.Models.NorthwindDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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

        public IEnumerable<Product> Get(int maxCount)
        {
            var resultList = Context.Products.Include(w => w.Category).Include(w => w.Supplier).ToList();
            if (maxCount > 0)
            {
                return resultList.Take(maxCount);
            }
            return resultList;
        }
    }
}
