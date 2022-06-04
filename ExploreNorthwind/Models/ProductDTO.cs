using ExploreNorthwindDataAccess.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExploreNorthwind.Models
{
    public class ProductDTO
    {
        public int ProductID { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Product name length should be at least 2")]
        public string ProductName { get; set; }
        public string QuantityPerUnit { get; set; }
        [RegularExpression(@"^(0|-?\d{0,16}(\,\d{0,2})?)$", ErrorMessage = "Unit price must follow 0,00 pattern")]
        [Range(0, double.PositiveInfinity, ErrorMessage = "Unit price value must be positive")]
        public decimal UnitPrice { get; set; }
        [Range(0, short.MaxValue, ErrorMessage = "Units in stock value must be positive")]
        public short UnitsInStock { get; set; }
        [Range(0, short.MaxValue, ErrorMessage = "Units on order value must be positive")]
        public short UnitsOnOrder { get; set; }
        public short ReorderLevel { get; set; }
        public bool Discontinued { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int SupplierID { get; set; }
        public string CompanyName { get; set; }
        public SelectList SuppliersList { get; set; }
        public SelectList CategoriesList { get; set; }

        public ProductDTO()
        { }

        public ProductDTO(List<SupplierDTO> suppliersList, List<CategoryDTO> categoryList)
        {
            this.InitializeSelectLists(suppliersList, categoryList);
        }

        public ProductDTO(Product product, List<SupplierDTO> suppliersList, List<CategoryDTO> categoryList): this(product)
        {
            this.InitializeSelectLists(suppliersList, categoryList);
        }

        public ProductDTO(Product product)
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
            this.CategoryName = product.Category.CategoryName;
            this.SupplierID = product.SupplierID;
            this.CompanyName = product.Supplier.CompanyName;
        }

        public void InitializeSelectLists(List<SupplierDTO> suppliersList, List<CategoryDTO> categoryList)
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
