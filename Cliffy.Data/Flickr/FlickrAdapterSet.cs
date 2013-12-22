using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using FlickrNet;
using Cliffy.Common;
using Cliffy.Common.Caching;

namespace Cliffy.Data.Flickr
{
    /// <summary>
    /// Methods for Returning all images associated with a PhotoSet.
    /// </summary>
    public class FlickrAdapterSet : FlickrAdapterBase
    {
		public FlickrAdapterSet(ICache cache, FlickrCredentials credentials) : base(cache, credentials) { }

		public Photographs ToPhotographs(string photosetId, string[] tags)
        {
			var conn = new FlickrNet.Flickr(this.mCredentials.ApiKey);
            Photoset setInfo = conn.PhotosetsGetInfo(photosetId);

			FlickrAdapterPhotograph photoAdapter = new FlickrAdapterPhotograph (mCache, mCredentials);

			Photographs output = new Photographs {
				Title = setInfo.Title,
				Description = setInfo.Description,
				Default = photoAdapter.GetPhotograph(setInfo.PrimaryPhotoId, setInfo.Secret)
			};

			this.LoadPhotos(photosetId, tags, output);

			return output;
        }

		private void LoadPhotos(string photosetId, string[] tags, Photographs target)
        {
            PhotosetPhotoCollection photos = this.GetPhotos(photosetId);
            foreach (Photo photo in photos)
            {
                foreach (string tag in photo.Tags)
                {
					if (!target.Tags.Contains(tag))
                    {
						target.Tags.Add(tag);
                    }
                }

				if (this.IncludePhoto(photo, photo.Tags, tags))
                {
					FlickrAdapterPhotograph photoAdapter = new FlickrAdapterPhotograph (mCache, mCredentials);
					target.Add (photoAdapter.ToPhotograph (photo));
                }
            }
        }

        private PhotosetPhotoCollection GetPhotos(string id)
        {
            PhotosetPhotoCollection output = null;

            string key = string.Concat("FlickrSet|", id);

            if ((this.mCache != null) && (this.mCache.IsInCache(key)))
            {
                this.mCache.GetFromCache<PhotosetPhotoCollection>(key, out output);
            }

            if (output == null)
            {
				var conn = new FlickrNet.Flickr(this.mCredentials.ApiKey);
                output = conn.PhotosetsGetPhotos(id, PhotoSearchExtras.Media | PhotoSearchExtras.ThumbnailUrl | PhotoSearchExtras.Tags | PhotoSearchExtras.DateUploaded | PhotoSearchExtras.OwnerName, PrivacyFilter.None);

                if (this.mCache != null)
                {
                    this.mCache.PutInCache<PhotosetPhotoCollection>(key, output);
                }
            }

            return output;
        }

		private bool IncludePhoto(Photo photo, IList<string> photoTags, string[] setTags)
        {
            bool output = true;

			if (setTags.Length > 0)
            {
				if (!photoTags.Any (x => setTags.Contains (x)))
                {
                    output = false;
                }
            }

            return output;
        }
    }
}