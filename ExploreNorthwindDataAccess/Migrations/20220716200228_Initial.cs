﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace ExploreNorthwindDataAccess.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Categories",
            //    columns: table => new
            //    {
            //        CategoryID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Picture = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Categories", x => x.CategoryID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Suppliers",
            //    columns: table => new
            //    {
            //        SupplierID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Suppliers", x => x.SupplierID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Products",
            //    columns: table => new
            //    {
            //        ProductID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        ProductName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        QuantityPerUnit = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
            //        UnitsInStock = table.Column<short>(type: "smallint", nullable: false),
            //        UnitsOnOrder = table.Column<short>(type: "smallint", nullable: false),
            //        ReorderLevel = table.Column<short>(type: "smallint", nullable: false),
            //        Discontinued = table.Column<bool>(type: "bit", nullable: false),
            //        CategoryID = table.Column<int>(type: "int", nullable: false),
            //        SupplierID = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Products", x => x.ProductID);
            //        table.ForeignKey(
            //            name: "FK_Products_Categories_CategoryID",
            //            column: x => x.CategoryID,
            //            principalTable: "Categories",
            //            principalColumn: "CategoryID",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_Products_Suppliers_SupplierID",
            //            column: x => x.SupplierID,
            //            principalTable: "Suppliers",
            //            principalColumn: "SupplierID",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Products_CategoryID",
            //    table: "Products",
            //    column: "CategoryID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Products_SupplierID",
            //    table: "Products",
            //    column: "SupplierID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Suppliers");
        }
    }
}
