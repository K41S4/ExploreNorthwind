using ExploreNorthwindAPI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.IO;

namespace ClientProxy
{
    [TestClass]
    public class ProductsTest
    {
        private ProductsClient clientProxy;
        public ProductsTest()
        {
            clientProxy = new ProductsClient(new System.Uri("http://localhost:56707/"), null); //44385
        }

        private object getJsonFromResponse(Stream stream)
        {
            var serializer = new JsonSerializer();

            using (var sr = new StreamReader(stream))
            using (var jsonTextReader = new JsonTextReader(sr))
            {
                return serializer.Deserialize(jsonTextReader);
            }
        }

        [TestMethod]
        public void GetAllProductsTest()
        {
            // Arrange
            // Act
            var response = clientProxy.GetAllProducts();

            // Assert
            var jsonObject = this.getJsonFromResponse(response.ContentStream) as dynamic;
            Assert.IsTrue(jsonObject.Count > 0);
        }

        [TestMethod]
        public void CreateProductTest()
        {
            // Arrange
            var productName = DateTime.Now.ToString();
            string json = JsonConvert.SerializeObject(new { ProductName = productName, SupplierID = 1, CategoryId = 1 });

            // Act
            clientProxy.CreateProduct(json, "application/json");

            // Assert
            var response = clientProxy.GetAllProducts();
            var jsonObject = this.getJsonFromResponse(response.ContentStream) as dynamic;
            Assert.AreEqual(productName, jsonObject.Last["productName"].ToString());
        }

        [TestMethod]
        public void UpdateProductTest()
        {
            // Arrange
            var response = clientProxy.GetAllProducts();

            var jsonObject = this.getJsonFromResponse(response.ContentStream) as dynamic;
            var productNewName = DateTime.Now.ToString();
            jsonObject.Last["productName"] = productNewName;

            int productId = int.Parse(jsonObject.Last["productID"].ToString());

            // Act
            clientProxy.UpdateProduct(productId, jsonObject.Last.ToString(), "application/json");

            // Assert
            response = clientProxy.GetAllProducts();
            jsonObject = this.getJsonFromResponse(response.ContentStream) as dynamic;
            Assert.AreEqual(productNewName, jsonObject.Last["productName"].ToString());
        }

        [TestMethod]
        public void DeleteProductTest()
        {
            // Arrange
            var response = clientProxy.GetAllProducts();
            var jsonObject = this.getJsonFromResponse(response.ContentStream) as dynamic;
            int productId = int.Parse(jsonObject.Last["productID"].ToString());

            // Act
            clientProxy.DeleteProduct(productId);

            // Assert
            response = clientProxy.GetAllProducts();
            jsonObject = this.getJsonFromResponse(response.ContentStream) as dynamic;
            Assert.AreNotEqual(productId, jsonObject.Last["productID"].ToString());
        }

    }
}
