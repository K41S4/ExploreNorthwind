using System;
using System.Collections.Generic;

namespace ExploreNorthwind.Models
{
    public class Supplier
    {
        public int SupplierID { get; set; }
        public string CompanyName { get; set; }
        public List<Product> Products { get; set; }
    }
}
