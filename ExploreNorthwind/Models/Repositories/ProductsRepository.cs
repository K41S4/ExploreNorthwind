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
        
        public Product GetById(int id)
        {
            return Context.Products.Where(w => w.ProductID == id).Include(w => w.Category).Include(w => w.Supplier).First();
        }

        public void Create(Product product)
        {
            Context.Add(product);
            Context.SaveChanges();
        }
        
        public void Update(Product product)
        {
            var oldItem = GetById(product.ProductID);

            if (oldItem == null) return;

            oldItem.ProductName = product.ProductName;
            oldItem.QuantityPerUnit = product.QuantityPerUnit;
            oldItem.ReorderLevel = product.ReorderLevel;
            oldItem.UnitPrice = product.UnitPrice;
            oldItem.UnitsInStock = product.UnitsInStock;
            oldItem.UnitsOnOrder = product.UnitsOnOrder;
            oldItem.SupplierID = product.SupplierID;
            oldItem.CategoryID = product.CategoryID;
            oldItem.Discontinued = product.Discontinued;

            Context.SaveChanges();
        }
    }
}
