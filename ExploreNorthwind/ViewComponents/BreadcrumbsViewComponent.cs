using ExploreNorthwind.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace ExploreNorthwind.ViewComponents
{
    public class BreadcrumbsViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var result = new BreadcrumbsViewModel();

            JObject obj = JObject.Parse(File.ReadAllText("actionNames.json"));

            var section = HttpContext.Request.RouteValues["controller"].ToString();
            var subsection = HttpContext.Request.RouteValues["action"].ToString();

            result.SectionAction = section;
            result.SectionName = (string)obj[section] ?? section;
            if (subsection != "Index")
            {
                result.SubsectionAction = subsection;
                result.SubsectionName = (string)obj[subsection] ?? subsection;
            }

            return View(result);
        }
    }
}
