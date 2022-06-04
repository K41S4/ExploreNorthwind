using ExploreNorthwind.ConfigurationOptions;
using ExploreNorthwind.Controllers;
using ExploreNorthwind.Models;
using ExploreNorthwindDataAccess.Models;
using ExploreNorthwindDataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace ExploreNorthwind.UnitTests.Controllers
{
    [TestClass]
    public class ProductsControllerTests
    {
        private Mock<ICategoriesRepository> _categoriesRepoMock;
        private Mock<IProductsRepository> _productsRepoMock;
        private Mock<ISuppliersRepository> _suppliersRepoMock;
        private Mock<IOptionsSnapshot<ExploreNorthwindOptions>> _optionsMock;
        private ProductsController _productsController;

        [TestInitialize]
        public void Initialize()
        {
            _categoriesRepoMock = new Mock<ICategoriesRepository>();
            _productsRepoMock = new Mock<IProductsRepository>();
            _suppliersRepoMock = new Mock<ISuppliersRepository>();
            _optionsMock = new Mock<IOptionsSnapshot<ExploreNorthwindOptions>>();
            _optionsMock.Setup(w => w.Value).Returns(new ExploreNorthwindOptions());

            var categories = new List<Category>()
            {
                new Category() { CategoryID = 1, CategoryName = "categoryName", Description = "description" }
            };
            _categoriesRepoMock.Setup(w => w.GetAll()).Returns(categories);
            var suppliers = new List<Supplier>()
            {
                new Supplier() { SupplierID = 1, CompanyName = "companyName" }
            };
            _suppliersRepoMock.Setup(w => w.GetAll()).Returns(suppliers);

            _productsController = new ProductsController(_productsRepoMock.Object, _suppliersRepoMock.Object, _categoriesRepoMock.Object, _optionsMock.Object);
        }

        [TestMethod]
        public void Index_ReturnsViewResult_WithAppropriateProductsCount()
        {
            // Arrange
            var products = new List<Product>()
            {
                new Product() { ProductID = 1,  CategoryID = 1, Discontinued = true, ProductName = "testProdName1", QuantityPerUnit = "testQuantity", SupplierID = 1, ReorderLevel = 1, UnitPrice = 1, UnitsInStock = 1, UnitsOnOrder = 1, Category = new Category() { CategoryName = "CategoryName1" }, Supplier = new Supplier() { CompanyName = "CompanyName1" } },
                new Product() { ProductID = 2,  CategoryID = 1, Discontinued = true, ProductName = "testProdName2", QuantityPerUnit = "testQuantity", SupplierID = 1, ReorderLevel = 1, UnitPrice = 1, UnitsInStock = 1, UnitsOnOrder = 1, Category = new Category() { CategoryName = "CategoryName2" }, Supplier = new Supplier() { CompanyName = "CompanyName2" } },
                new Product() { ProductID = 3,  CategoryID = 1, Discontinued = true, ProductName = "testProdName3", QuantityPerUnit = "testQuantity", SupplierID = 1, ReorderLevel = 1, UnitPrice = 1, UnitsInStock = 1, UnitsOnOrder = 1, Category = new Category() { CategoryName = "CategoryName3" }, Supplier = new Supplier() { CompanyName = "CompanyName3" } }
            };
            _productsRepoMock.Setup(w => w.Get(It.IsAny<int>())).Returns(products);

            var productCountTest = 2;
            var options = new ExploreNorthwindOptions() { ProductsMaxCount = productCountTest };
            _optionsMock.Setup(w => w.Value).Returns(options);
            _productsController = new ProductsController(_productsRepoMock.Object, _suppliersRepoMock.Object, _categoriesRepoMock.Object, _optionsMock.Object);

            // Act
            var result = _productsController.Index();

            // Assert
            var resultList = (List<ProductDTO>)((ViewResult)result).Model;
            _productsRepoMock.Verify(w => w.Get(It.Is<int>(w => w == productCountTest)), Times.Once);
        }

        [TestMethod]
        public void CreateGet_ReturnsViewResult_WithCorrectModelType()
        {
            // Arrange

            // Act
            var result = _productsController.Create();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var resultList = (ProductDTO)((ViewResult)result).Model;
            _categoriesRepoMock.Verify(w => w.GetAll(), Times.Once);
            _suppliersRepoMock.Verify(w => w.GetAll(), Times.Once);
        }
        
        [TestMethod]
        public void CreatePost_ValidationPass_CallsUpdateAndRedirectsToIndex()
        {
            // Arrange
            var product = new ProductDTO()
            {
                ProductID = 1,
                CategoryID = 1,
                Discontinued = true,
                ProductName = "testProdName1",
                QuantityPerUnit = "testQuantity",
                SupplierID = 1,
                ReorderLevel = 1,
                UnitPrice = 1,
                UnitsInStock = 1,
                UnitsOnOrder = 1,
                CategoryName = "CategoryName1",
                CompanyName = "CompanyName1"
            };

            // Act
            var result = _productsController.Create(product);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            Assert.AreEqual("Index", (result as RedirectToActionResult).ActionName);
            _categoriesRepoMock.Verify(w => w.GetAll(), Times.Once);
            _suppliersRepoMock.Verify(w => w.GetAll(), Times.Once);
            _productsRepoMock.Verify(w => w.Create(It.IsAny<Product>()));
        }

        [TestMethod]
        public void CreatePost_ValidationFail_DoesNotCallCreate()
        {
            // Arrange
            var product = new ProductDTO()
            {
                ProductID = 1,
                CategoryID = 1,
                Discontinued = true,
                ProductName = null,
                QuantityPerUnit = "testQuantity",
                SupplierID = 1,
                ReorderLevel = 1,
                UnitPrice = 1,
                UnitsInStock = 1,
                UnitsOnOrder = 1,
                CategoryName = "CategoryName1",
                CompanyName = "CompanyName1"
            };
            _productsController.ModelState.AddModelError("ProductName", "Required");

            // Act
            var result = _productsController.Create(product);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsInstanceOfType((result as ViewResult).Model, typeof(ProductDTO));
            _categoriesRepoMock.Verify(w => w.GetAll(), Times.Once);
            _suppliersRepoMock.Verify(w => w.GetAll(), Times.Once);
            _productsRepoMock.Verify(w => w.Create(It.IsAny<Product>()), Times.Never);
        }

        [TestMethod]
        public void UpdateGet_ReturnsViewResult_WithCorrectModel()
        {
            // Arrange
            var product = new Product()
            {
                ProductID = 1,  
                CategoryID = 1, 
                Discontinued = true, 
                ProductName = "testProdName1", 
                QuantityPerUnit = "testQuantity", 
                SupplierID = 1, 
                ReorderLevel = 1, 
                UnitPrice = 1, 
                UnitsInStock = 1, 
                UnitsOnOrder = 1, 
                Category = new Category() { CategoryName = "CategoryName1" }, 
                Supplier = new Supplier() { CompanyName = "CompanyName1" } 
            };
            var testProductId = 1;
            _productsRepoMock.Setup(w => w.GetById(testProductId)).Returns(product).Verifiable();

            // Act
            var result = _productsController.Update(testProductId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var resultViewResult = result as ViewResult;
            Assert.IsInstanceOfType(resultViewResult.Model, typeof(ProductDTO));
            Assert.AreEqual(testProductId, (resultViewResult.Model as ProductDTO).ProductID);
            _categoriesRepoMock.Verify(w => w.GetAll(), Times.Once);
            _suppliersRepoMock.Verify(w => w.GetAll(), Times.Once);
            _productsRepoMock.Verify();
        }

        [TestMethod]
        public void UpdatePost_ValidationPass_CallsUpdateAndRedirectsToIndex()
        {
            // Arrange
            var product = new ProductDTO()
            {
                ProductID = 1,
                CategoryID = 1,
                Discontinued = true,
                ProductName = "testProdName1",
                QuantityPerUnit = "testQuantity",
                SupplierID = 1,
                ReorderLevel = 1,
                UnitPrice = 1,
                UnitsInStock = 1,
                UnitsOnOrder = 1,
                CategoryName = "CategoryName1",
                CompanyName = "CompanyName1"
            };

            // Act
            var result = _productsController.Update(product);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            Assert.AreEqual("Index", (result as RedirectToActionResult).ActionName);
            _categoriesRepoMock.Verify(w => w.GetAll(), Times.Once);
            _suppliersRepoMock.Verify(w => w.GetAll(), Times.Once);
            _productsRepoMock.Verify(w => w.Update(It.IsAny<Product>()));
        }

        [TestMethod]
        public void UpdatePost_ValidationFail_DoesNotCallUpdate()
        {
            // Arrange
            var product = new ProductDTO()
            {
                ProductID = 1,
                CategoryID = 1,
                Discontinued = true,
                ProductName = null,
                QuantityPerUnit = "testQuantity",
                SupplierID = 1,
                ReorderLevel = 1,
                UnitPrice = 1,
                UnitsInStock = 1,
                UnitsOnOrder = 1,
                CategoryName = "CategoryName1",
                CompanyName = "CompanyName1"
            };
            _productsController.ModelState.AddModelError("ProductName", "Required");

            // Act
            var result = _productsController.Update(product);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsInstanceOfType((result as ViewResult).Model, typeof(ProductDTO));
            _categoriesRepoMock.Verify(w => w.GetAll(), Times.Once);
            _suppliersRepoMock.Verify(w => w.GetAll(), Times.Once);
            _productsRepoMock.Verify(w => w.Update(It.IsAny<Product>()), Times.Never);
        }
    }
}
