using System;

namespace ExploreNorthwind.Middlewares
{
    public class ImageCachingParameters
    {
        public string CacheStoragePath { get; set; }
        public int MaxCacheCount { get; set; }
        public int ExpirationTime { get; set; }
    }
}
