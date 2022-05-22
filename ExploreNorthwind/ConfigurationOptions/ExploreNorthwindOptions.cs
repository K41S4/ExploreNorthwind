using System;

namespace ExploreNorthwind.ConfigurationOptions
{
    public class ExploreNorthwindOptions
    {
        public const string ExploreNorthwindOptionsName = "ExploreNorthwindOptions";
        public int ProductsMaxCount { get; set; }
        public string NorthwindConnectionString { get; set; }
    }
}
