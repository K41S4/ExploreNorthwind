using ExploreNorthwindDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ExploreNorthwindDataAccess.NorthwindDB
{
    public class NorthwindContext : IdentityDbContext<AppUser, AppRole, int>, INorthwindContext
    {
        public NorthwindContext(DbContextOptions<NorthwindContext> options) : base(options)
        { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

    }
}
