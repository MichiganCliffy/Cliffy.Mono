using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using FlickrNet;
using Cliffy.Common;
using Cliffy.Common.Caching;

namespace Cliffy.Data.Flickr
{
    public class FlickrAdapterGroup : FlickrAdapterBase
    {
		private readonly int mNumPerPage;

		public FlickrAdapterGroup(ICache cache, FlickrCredentials credentials, int numPerPage) : base(cache, credentials)
		{
			mNumPerPage = numPerPage;
		}

		public Photographs ToPhotographs(string groupId, int pageNo)
        {
			return ToPhotographs(groupId, new string[] { }, pageNo);
        }

		public Photographs ToPhotographs(string groupId, string groupTag, int pageNo)
        {
            if (!string.IsNullOrWhiteSpace(groupTag))
            {
				return ToPhotographs(groupId, new string[] { groupTag }, pageNo);
            }
            else
            {
				return ToPhotographs(groupId, new string[] { }, pageNo);
            }
        }

		public Photographs ToPhotographs(string groupId, string[] groupTags, int pageNo)
        {
			Photographs output = new Photographs ();

            StringBuilder tagString = new StringBuilder();
            if ((groupTags != null) && (groupTags.Length > 0))
            {
                foreach (string groupTag in groupTags)
                {
                    if (tagString.Length > 0) tagString.Append(" ");
                    tagString.Append(groupTag);
                }
            }

			var conn = new FlickrNet.Flickr(this.mCredentials.ApiKey);
			PhotoCollection photos = conn.GroupsPoolsGetPhotos(groupId, tagString.ToString(), mCredentials.UserID, PhotoSearchExtras.Tags | PhotoSearchExtras.DateUploaded | PhotoSearchExtras.Media, pageNo, this.mNumPerPage);
            //FlickrNet.Photos photos = conn.GroupPoolGetPhotos(groupID, tagString.ToString(), this.UserID, PhotoSearchExtras.All, this.mNumPerPage, this.mCurrentPage);

            if (photos != null)
            {
                foreach (Photo photo in photos)
                {
					FlickrAdapterPhotograph photoAdapter = new FlickrAdapterPhotograph (mCache, mCredentials);
					output.Add (photoAdapter.ToPhotograph (photo));
                }

				output.Total = photos.Total;
            }

			return output;
        }
    }
}