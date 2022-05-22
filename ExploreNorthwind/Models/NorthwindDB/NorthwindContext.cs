using ExploreNorthwind.ConfigurationOptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExploreNorthwind.Models.NorthwindDB
{
    public class NorthwindContext: DbContext
    {
        private ExploreNorthwindOptions Options { get; set; }

        public NorthwindContext(IOptionsSnapshot<ExploreNorthwindOptions> options) : base()
        {
            this.Options = options.Value;
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Options.NorthwindConnectionString);
        }
    }
}
