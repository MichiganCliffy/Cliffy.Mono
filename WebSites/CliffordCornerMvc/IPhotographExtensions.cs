using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cliffy.Web
{
    public static class IPhotographExtensions
    {
        public static string GetImageUri(this IPhotograph source, ImageSize size)
        {
            var image = source.UriSizes.FirstOrDefault(x => x.Size == size);
            if (image != null)
            {
                return image.Uri;
            }

            return string.Empty;
        }
    }
}