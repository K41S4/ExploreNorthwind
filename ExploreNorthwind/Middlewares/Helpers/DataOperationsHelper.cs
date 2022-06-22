using ExploreNorthwind.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ExploreNorthwind.Middlewares.Helpers
{
    public class DataOperationsHelper: IDataOperationsHelper
    {
        public string CacheStoragePath { get; set; }
        public int MaxCacheCount { get; set; }
        public int ExpirationTimeSeconds { get; set; }

        private CancellationTokenSource _resetCacheToken = new();
        private int _cacheCounter = 0;

        public async Task<bool> WriteCachedFileToResponse(HttpContext context, string cacheKey)
        {
            var fileInfo = new FileInfo(CacheStoragePath + cacheKey);
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

        public async Task WritePostDataToCachedFile(HttpContext context, IMemoryCache memoryCache)
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
            this.AddToCache(cacheKey, memoryCache);
        }

        public void WriteToFile(string cacheKey, byte[] bytes)
        {
            if (bytes != null)
            {
                File.WriteAllBytes(CacheStoragePath + cacheKey, bytes);
            }
        }

        public void AddToCache(string cacheKey, IMemoryCache _memoryCache)
        {
            if (_cacheCounter >= MaxCacheCount)
            {
                _cacheCounter = 0;
                _resetCacheToken.Cancel();
                _resetCacheToken.Dispose();
                _resetCacheToken = new CancellationTokenSource();
            }

            bool outResult;
            if (_memoryCache.TryGetValue(cacheKey, out outResult)) return;
            
            using var entry = _memoryCache.CreateEntry(cacheKey);
            entry.SetOptions(new MemoryCacheEntryOptions()
            {
                SlidingExpiration = TimeSpan.FromSeconds(ExpirationTimeSeconds),
            });
            entry.Value = true;
            entry.AddExpirationToken(new CancellationChangeToken(_resetCacheToken.Token));
            _cacheCounter++;
        }

        public string GetCacheKey(string requestQuery, string idParameter)
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
