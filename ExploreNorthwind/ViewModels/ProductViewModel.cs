using ExploreNorthwind.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExploreNorthwind.ViewModels
{
    public class ProductViewModel
    {
        public int ProductID { get; set; }
        [Required]
        [MinLength(2)]
        public string ProductName { get; set; }
        public string QuantityPerUnit { get; set; }
        [RegularExpression(@"^(0|-?\d{0,16}(\,\d{0,4})?)$", ErrorMessage = "Unit price must follow 0.00 pattern")]
        [Range(0, double.PositiveInfinity, ErrorMessage = "Unit price value must be positive")]
        public decimal UnitPrice { get; set; }
        [Range(0, short.MaxValue, ErrorMessage = "Units in stock value must be positive")]
        public short UnitsInStock { get; set; }
        [Range(0, short.MaxValue, ErrorMessage = "Units on order value must be positive")]
        public short UnitsOnOrder { get; set; }
        public short ReorderLevel { get; set; }
        public bool Discontinued { get; set; }
        public int CategoryID { get; set; }
        public int SupplierID { get; set; }

        public SelectList SuppliersList { get; set; }
        public SelectList CategoriesList { get; set; }

        public ProductViewModel()
        {
        }
        
        public ProductViewModel(List<Supplier> suppliersList, List<Category> categoryList)
        {
            this.SuppliersList = new SelectList(suppliersList, "SupplierID", "CompanyName");
            this.CategoriesList = new SelectList(categoryList, "CategoryID", "CategoryName");
        }

        public ProductViewModel(Product product, List<Supplier> suppliersList, List<Category> categoryList)
        {
            this.ProductID = product.ProductID;
            this.ProductName = product.ProductName;
            this.QuantityPerUnit = product.QuantityPerUnit;
            this.UnitPrice = product.UnitPrice;
            this.UnitsInStock = product.UnitsInStock;
            this.UnitsOnOrder = product.UnitsOnOrder;
            this.ReorderLevel = product.ReorderLevel;
            this.Discontinued = product.Discontinued;
            this.CategoryID = product.CategoryID;
            this.SupplierID = product.SupplierID;

            this.InitializeSelectLists(suppliersList, categoryList);
        }

        public void InitializeSelectLists(List<Supplier> suppliersList, List<Category> categoryList)
        {
            this.SuppliersList = new SelectList(suppliersList, "SupplierID", "CompanyName");
            this.CategoriesList = new SelectList(categoryList, "CategoryID", "CategoryName");
        }

        public Product GetProduct()
        {
            return new Product()
            {
                ProductID = this.ProductID,
                ProductName = this.ProductName,
                QuantityPerUnit = this.QuantityPerUnit,
                UnitPrice = this.UnitPrice,
                UnitsInStock = this.UnitsInStock,
                UnitsOnOrder = this.UnitsOnOrder,
                ReorderLevel = this.ReorderLevel,
                Discontinued = this.Discontinued,
                CategoryID = this.CategoryID,
                SupplierID = this.SupplierID
            };
        }
    }
}
