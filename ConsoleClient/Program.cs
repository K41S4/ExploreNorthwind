using ConsoleClient.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ConsoleClient
{
    class Program
    {
        static HttpClient client = new HttpClient();

        static void ShowProducts(List<Product> products)
        {
            Console.WriteLine("Products:");
            foreach (var item in products)
            {
                Console.WriteLine($"Id: {item.ProductID}\tName: {item.ProductName}");
            }
        }

        static void ShowCategories(List<Category> categories)
        {
            Console.WriteLine("Categories:");
            foreach (var item in categories)
            {
                Console.WriteLine($"Id: {item.CategoryID}\tName: {item.CategoryName}\tDescription: {item.Description}\t");
            }
        }

        static async Task<T> GetEntitiesAsync<T>(string path)
        {
            T product = default(T);
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadFromJsonAsync<T>();
            }
            return product;
        }

        static void Main()
        {
            RunAsync().GetAwaiter().GetResult();
        }

        static async Task RunAsync()
        {
            client.BaseAddress = new Uri("http://localhost:56707/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                List<Product> products = await GetEntitiesAsync<List<Product>>("api/ProductsAPI/Get");
                ShowProducts(products);

                List<Category> categories = await GetEntitiesAsync<List<Category>>("api/CategoriesAPI/Get");
                ShowCategories(categories);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}
