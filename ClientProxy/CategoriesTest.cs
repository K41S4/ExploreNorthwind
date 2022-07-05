using ConcertAPI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace ClientProxy
{
    [TestClass]
    public class CategoriesTest
    {
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
            var clientProxy = new CategoriesAPIClient(new System.Uri("http://localhost:56707/"), null); //44385
            var response = clientProxy.GetAllCategories();
            var jsonObject = this.getJsonFromResponse(response.ContentStream);
        }
    }
}
