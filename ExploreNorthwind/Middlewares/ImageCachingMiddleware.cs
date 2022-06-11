using ExploreNorthwind.Constants;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ExploreNorthwind.Middlewares
{
    public class ImageCachingMiddleware
    {
        private readonly RequestDelegate _next;
        private Queue<string> _cache;
        private string _cacheStoragePath;
        private int _maxCacheCount;
        private int _expirationTimeSeconds;
        private DateTime _startTime;

        public ImageCachingMiddleware(RequestDelegate next, ImageCachingParameters parameters)
        {
            _startTime = DateTime.Now;
            _cache = new Queue<string>();
            _next = next;

            _maxCacheCount = parameters.MaxCacheCount;
            _cacheStoragePath = parameters.CacheStoragePath;
            _expirationTimeSeconds = parameters.ExpirationTime;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Clear cache if time X passed since last request
            var difference = DateTime.Now.Subtract(_startTime);
            if (difference.TotalSeconds > _expirationTimeSeconds) _cache.Clear();
            _startTime = DateTime.Now;

            // Return if cached
            var cacheKey = this.GetCacheKey(context.Request.Path, context.Request.Query["categoryId"]);
            if (_cache.Contains(cacheKey))
            {
                var isSucceeded = await this.WriteCachedFileToResponse(context, cacheKey);
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
                this.WriteToFile(cacheKey, buffer.ToArray());
            }

            // Update or cache POST request image
            if (context.Request.Method == "POST" 
                && context.Request.Path.Equals(ExploreNotrhwindConstants.PostPicturePath) 
                && context.Response.StatusCode == 302)
            {
                await this.WritePostDataToCachedFile(context);
            }
        }

        private async Task<bool> WriteCachedFileToResponse(HttpContext context, string cacheKey)
        {
            var fileInfo = new FileInfo(_cacheStoragePath + cacheKey);
            var fileBytes = new byte[fileInfo.Length];
            using (FileStream fs = fileInfo.OpenRead())
            {
                fs.Read(fileBytes, 0, fileBytes.Length);
            }

            if (fileBytes == null) return false;

            context.Response.ContentType = ExploreNotrhwindConstants.ImageContentType;
            await context.Response.Body.WriteAsync(fileBytes);

            return true;
        }

        private async Task WritePostDataToCachedFile(HttpContext context)
        {
            var form = await context.Request.ReadFormAsync();
            var file = form.Files[0];

            var bytes = new byte[file.Length];
            using (MemoryStream ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                bytes = ms.ToArray();
            }

            var cacheKey = this.GetCacheKey(form["CategoryID"]);
            this.WriteToFile(cacheKey, bytes);
        }

        private void WriteToFile(string cacheKey, byte[] bytes)
        {
            if (bytes != null)
            {
                File.WriteAllBytes(_cacheStoragePath + cacheKey, bytes);
                this.AddToCache(cacheKey);
            }
        }

        private void AddToCache(string cacheKey)
        {
            if (_cache.Count >= _maxCacheCount)
            {
                var dequeued = _cache.Dequeue();

                var fileInfo = new FileInfo(_cacheStoragePath + dequeued);
                fileInfo.Delete();
            }
            if (!_cache.Contains(cacheKey)) _cache.Enqueue(cacheKey);
        }

        private string GetCacheKey(string requestQuery, string idParameter)
        {
            if (requestQuery.Equals(ExploreNotrhwindConstants.GetPicturePath) && idParameter != null) return this.GetCacheKey(idParameter);

            return requestQuery.Replace("/", String.Empty);
        }

        private string GetCacheKey(string idParameter)
        {
            return "images" + idParameter;
        }
    }
}