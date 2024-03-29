﻿using ExploreNorthwindDataAccess.Models;
using ExploreNorthwindDataAccess.NorthwindDB;
using ExploreNorthwindDataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExploreNorthwindDataAccess.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private INorthwindContext Context { get; }
        public ProductsRepository(INorthwindContext northwindContext)
        {
            this.Context = northwindContext;
        }

        public IEnumerable<Product> Get(int maxCount)
        {
            var resultList = this.Get();
            if (maxCount > 0)
            {
                return resultList.Take(maxCount);
            }
            return resultList;
        }
        
        public IEnumerable<Product> Get()
        {
            var resultList = Context.Products.Include(w => w.Category).Include(w => w.Supplier).ToList();
            return resultList;
        }

        public Product GetById(int id)
        {
            return Context.Products.Where(w => w.ProductID == id).Include(w => w.Category).Include(w => w.Supplier).First();
        }

        public void Create(Product product)
        {
            Context.Products.Add(product);
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

        public void Delete(int id)
        {
            var item = GetById(id);
            if (item == null) return;

            Context.Products.Remove(item);
            Context.SaveChanges();
        }
    }
}
