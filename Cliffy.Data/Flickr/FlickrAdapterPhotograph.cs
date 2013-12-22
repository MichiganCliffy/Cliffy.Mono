using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using FlickrNet;
using Cliffy.Common;
using Cliffy.Common.Caching;

namespace Cliffy.Data.Flickr
{
	public class FlickrAdapterPhotograph : FlickrAdapterBase
    {
		public FlickrAdapterPhotograph(ICache cache, FlickrCredentials credentials) : base(cache, credentials) { }

		public Photograph ToPhotograph(Photo photo)
        {
			var output = new Photograph {
				Title = photo.Title,
				UriSource = photo.WebUrl,
				DateSaved = photo.DateUploaded,
				Description = photo.Description,
				Media = photo.Media,
				Photographer = photo.OwnerName,
				UriSizes = new List<PhotographUri>(),
				Tags = new List<string>()
			};

			output.UriSizes.Add(
                new PhotographUri { Size = ImageSize.Thumbnail, Uri = photo.SquareThumbnailUrl }
                );
			output.UriSizes.Add(
                new PhotographUri { Size = ImageSize.Small, Uri = photo.SmallUrl }
                );
			output.UriSizes.Add(
                new PhotographUri { Size = ImageSize.Medium, Uri = photo.MediumUrl }
                );
			output.UriSizes.Add(
                new PhotographUri { Size = ImageSize.Large, Uri = photo.LargeUrl }
                );
			output.UriSizes.Add(
                new PhotographUri { Size = ImageSize.Original, Uri = photo.OriginalUrl }
                );

            foreach (string tag in photo.Tags)
            {
				if (!output.Tags.Contains(tag))
                {
					output.Tags.Add(tag);
                }
            }

			return output;
        }

		public Photograph ToPhotograph(PhotoInfo photo)
		{
			var output = new Photograph {
				Title = photo.Title,
				UriSource = photo.WebUrl,
				DateSaved = photo.DateUploaded,
				Description = photo.Description,
				Media = photo.Media.ToString(),
				Photographer = photo.OwnerRealName,
				UriSizes = new List<PhotographUri>(),
				Tags = new List<string>()
			};

			output.UriSizes.Add(
				new PhotographUri { Size = ImageSize.Thumbnail, Uri = photo.SquareThumbnailUrl }
			);
			output.UriSizes.Add(
				new PhotographUri { Size = ImageSize.Small, Uri = photo.SmallUrl }
			);
			output.UriSizes.Add(
				new PhotographUri { Size = ImageSize.Medium, Uri = photo.MediumUrl }
			);
			output.UriSizes.Add(
				new PhotographUri { Size = ImageSize.Large, Uri = photo.LargeUrl }
			);
			output.UriSizes.Add(
				new PhotographUri { Size = ImageSize.Original, Uri = photo.OriginalUrl }
			);

			foreach (PhotoInfoTag tag in photo.Tags)
			{
				if (!output.Tags.Contains(tag.TagText))
				{
					output.Tags.Add(tag.TagText);
				}
			}

			return output;
		}

		public Photograph GetPhotograph(string photoId, string secretId)
        {
			var flickr = new FlickrNet.Flickr(this.mCredentials.ApiKey);
			PhotoInfo photo = flickr.PhotosGetInfo(photoId, secretId);
			return ToPhotograph (photo);
        }
    }
}