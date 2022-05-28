using ExploreNorthwind.Models.NorthwindDB;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExploreNorthwind.Models.Repositories
{
    public class SuppliersRepository
    {
        private NorthwindContext Context { get; }
        public SuppliersRepository(NorthwindContext northwindContext)
        {
            this.Context = northwindContext;
        }

        public List<Supplier> GetAll()
        {
            return Context.Suppliers.ToList();
        }
    }
}
