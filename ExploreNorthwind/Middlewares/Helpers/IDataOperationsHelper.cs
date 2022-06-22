using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace ExploreNorthwind.Middlewares.Helpers
{
    public interface IDataOperationsHelper
    {
        public string CacheStoragePath { get; set; }
        public int MaxCacheCount { get; set; }
        public int ExpirationTimeSeconds { get; set; }
        Task<bool> WriteCachedFileToResponse(HttpContext context, string cacheKey);
        Task WritePostDataToCachedFile(HttpContext context, IMemoryCache memoryCache);
        void WriteToFile(string cacheKey, byte[] bytes);
        string GetCacheKey(string requestQuery, string idParameter);
        void AddToCache(string cacheKey, IMemoryCache _memoryCache);
    }
}
