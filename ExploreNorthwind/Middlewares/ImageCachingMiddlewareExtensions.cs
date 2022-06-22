using Microsoft.AspNetCore.Builder;
using System;

namespace ExploreNorthwind.Middlewares
{
    public static class ImageCachingMiddlewareExtensions
    {
        public static IApplicationBuilder UseImageCaching(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ImageCachingMiddleware>();
        }
    }
}
