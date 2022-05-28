﻿using Microsoft.EntityFrameworkCore;

namespace ExploreNorthwind.Models.NorthwindDB
{
    public class NorthwindContext: DbContext
    {
        public NorthwindContext(DbContextOptions<NorthwindContext> options) : base(options)
        { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

    }
}
