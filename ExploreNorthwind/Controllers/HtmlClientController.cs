using ExploreNorthwind.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ExploreNorthwind.Controllers
{
    public class HtmlClientController : Controller
    {
        public IActionResult Index()
        {
            var model = new HtmlClientViewModel()
            {
                GetProductsUrl = "api/Products",
                GetCategoriesUrl = "api/Categories"
            };

            return View(model);
        }
    }
}
