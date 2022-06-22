using ExploreNorthwind.ConfigurationOptions;
using ExploreNorthwind.Constants;
using ExploreNorthwind.Middlewares.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ExploreNorthwind.Middlewares
{
    public class ImageCachingMiddleware
    {
        private IMemoryCache _memoryCache;
        private readonly RequestDelegate _next;
        private IDataOperationsHelper _dataOperationsHelper;

        public ImageCachingMiddleware(RequestDelegate next, IMemoryCache cache, IDataOperationsHelper dataOperationsHelper)
        {
            _next = next;
            _memoryCache = cache;
            _dataOperationsHelper = dataOperationsHelper;
        }

        public async Task InvokeAsync(HttpContext context, IOptionsSnapshot<ExploreNorthwindOptions> options)
        {
            var optionsValue = options.Value;
            _dataOperationsHelper.MaxCacheCount = optionsValue.MaxCacheCount;
            _dataOperationsHelper.CacheStoragePath = optionsValue.CacheStoragePath;
            _dataOperationsHelper.ExpirationTimeSeconds = optionsValue.ExpirationTime;

            // Return if cached
            var cacheKey = _dataOperationsHelper.GetCacheKey(context.Request.Path, context.Request.Query["categoryId"]);
            var a = _memoryCache.Get(cacheKey);
            bool outResult;
            if (_memoryCache.TryGetValue(cacheKey, out outResult))
            {
                var isSucceeded = await _dataOperationsHelper.WriteCachedFileToResponse(context, cacheKey);
                if (isSucceeded) return;
            }

            var originalResponseStream = context.Response.Body;
            var buffer = new MemoryStream();
            context.Response.Body = buffer;

            await _next.Invoke(context);

            context.Response.Body.Position = 0;
            await buffer.CopyToAsync(originalResponseStream);
            context.Response.Body = originalResponseStream;

            // Cache response
            if (context.Response.ContentType == ExploreNotrhwindConstants.ImageContentType)
            {
                _dataOperationsHelper.WriteToFile(cacheKey, buffer.ToArray());
                _dataOperationsHelper.AddToCache(cacheKey, _memoryCache);
            }

            // Update or cache POST request image
            if (context.Request.Method == "POST" 
                && context.Request.Path.Equals(ExploreNotrhwindConstants.PostPicturePath) 
                && context.Response.StatusCode == 302)
            {
                await _dataOperationsHelper.WritePostDataToCachedFile(context, _memoryCache);
            }
        }
    }
}