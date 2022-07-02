using Microsoft.AspNetCore.Mvc;
using System;

namespace ExploreNorthwind.Controllers
{
    public class HtmlClientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
