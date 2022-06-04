using ExploreNorthwind.Controllers;
using ExploreNorthwind.Models;
using ExploreNorthwindDataAccess.Models;
using ExploreNorthwindDataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace ExploreNorthwind.UnitTests.Controllers
{
    [TestClass]
    public class CategoriesControllerTest
    {
        private Mock<ICategoriesRepository> _categoriesRepo;
        private CategoriesController _categoriesController;

        [TestInitialize]
        public void Initialize()
        {
            _categoriesRepo = new Mock<ICategoriesRepository>();
            _categoriesController = new CategoriesController(_categoriesRepo.Object);
        }

        [TestMethod]
        public void Index_ReturnsViewResult_WithAllCategories()
        {
            // Arrange
            var categories = new List<Category>()
            {
                new Category() { CategoryID = 1, CategoryName = "testName", Description = "testDescription"}
            };
            _categoriesRepo.Setup(w => w.GetAll()).Returns(categories);

            // Act
            var result = _categoriesController.Index();

            // Assert
            var resultList = (List<CategoryDTO>)((ViewResult)result).Model;
            Assert.AreEqual(categories.Count, resultList.Count);
            _categoriesRepo.Verify(w => w.GetAll(), Times.Once);
        }
    }
}
