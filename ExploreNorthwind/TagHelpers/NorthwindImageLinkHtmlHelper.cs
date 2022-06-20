using ExploreNorthwind.Constants;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace ExploreNorthwind.TagHelpers
{
    public static class NorthwindImageLinkHtmlHelper
    {
        public static HtmlString NorthwindImageLink(this IHtmlHelper helper, string imageId, string text)
        {
            var imageLink = String.Format(ExploreNotrhwindConstants.ImagePath, imageId);

            var resultTag = String.Format("<a _target='blank' href='{0}'>{1}</a>", imageLink, text);

            return new HtmlString(resultTag);
        }
    }
}
