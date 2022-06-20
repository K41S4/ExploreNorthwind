using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExploreNorthwind.Models
{
    public class BreadcrumbsViewModel
    {
        public string SectionAction { get; set; }
        public string SectionName { get; set; }
        public string SubsectionAction { get; set; }
        public string SubsectionName { get; set; }
    }
}
