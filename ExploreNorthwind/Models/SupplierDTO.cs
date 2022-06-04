using ExploreNorthwindDataAccess.Models;
using System;

namespace ExploreNorthwind.Models
{
    public class SupplierDTO
    {
        public int SupplierID { get; set; }
        public string CompanyName { get; set; }
        public SupplierDTO(Supplier supplier)
        {
            SupplierID = supplier.SupplierID;
            CompanyName = supplier.CompanyName;
        }
    }
}
