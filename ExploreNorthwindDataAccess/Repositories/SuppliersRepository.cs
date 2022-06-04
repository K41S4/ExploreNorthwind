using ExploreNorthwindDataAccess.Models;
using ExploreNorthwindDataAccess.NorthwindDB;
using ExploreNorthwindDataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExploreNorthwindDataAccess.Repositories
{
    public class SuppliersRepository: ISuppliersRepository
    {
        private INorthwindContext Context { get; }
        public SuppliersRepository(INorthwindContext northwindContext)
        {
            this.Context = northwindContext;
        }

        public List<Supplier> GetAll()
        {
            return Context.Suppliers.ToList();
        }
    }
}
