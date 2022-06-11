using Microsoft.AspNetCore.Builder;
using System;

namespace ExploreNorthwind.Middlewares
{
    public static class ImageCachingMiddlewareExtensions
    {
        public static IApplicationBuilder UseImageCaching(
            this IApplicationBuilder builder, ImageCachingParameters parameters)
        {
            return builder.UseMiddleware<ImageCachingMiddleware>(parameters);
        }
    }
}
