using ExploreNorthwind.Constants;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;

namespace ExploreNorthwind.TagHelpers
{
    public class NorthwindImageLinkTagHelper: TagHelper
    {
        public string ImageId { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";

            var imageLink = String.Format(ExploreNotrhwindConstants.ImagePath, ImageId);
            output.Attributes.SetAttribute("href", imageLink);
            output.Attributes.SetAttribute("target", "_blank");
        }
    }
}
