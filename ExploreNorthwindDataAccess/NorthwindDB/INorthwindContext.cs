using ExploreNorthwindDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace ExploreNorthwindDataAccess.NorthwindDB
{
    public interface INorthwindContext
    {
        DbSet<Category> Categories { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<Supplier> Suppliers { get; set; }
        int SaveChanges();
    }
}
