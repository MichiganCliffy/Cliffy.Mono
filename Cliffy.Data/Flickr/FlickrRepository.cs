using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Cliffy.Common;
using Cliffy.Common.Caching;

namespace Cliffy.Data.Flickr
{
    /// <summary>
    /// This manages all access to the Flickr photograph repository.
    /// </summary>
    public class FlickrRepository : IPhotographRepository
    {
		private readonly FlickrCredentials mCredentials = null;
		private readonly ICache mCache;
		private readonly string mGroupId = string.Empty;
		private readonly int mCacheDuration = 120;

        public FlickrRepository(ICache cache)
        {
            mCache = cache;

			string groupId = ConfigurationManager.AppSettings ["groupId"];
			if (!string.IsNullOrWhiteSpace(groupId))
            {
				this.mGroupId = groupId;
            }

			string cacheDuration = ConfigurationManager.AppSettings ["FlickrCacheDuration"];
			if (!string.IsNullOrWhiteSpace(cacheDuration))
            {
                int duration = int.MinValue;
				if (int.TryParse(cacheDuration, out duration))
                {
                    this.mCacheDuration = duration;
                }
            }

			this.mCredentials = new FlickrCredentials {
				ApiKey = ConfigurationManager.AppSettings["apiKey"],
				Email = ConfigurationManager.AppSettings["email"],
				Password = ConfigurationManager.AppSettings["password"],
				Secret = ConfigurationManager.AppSettings["secret"],
				UserID = ConfigurationManager.AppSettings["userID"]
			};
        }

        #region IPhotographMgr Members

        public Photographs GetAlbum(string[] tags, string setId)
        {
			FlickrAdapterSet adapter = new FlickrAdapterSet(mCache, mCredentials);
			return adapter.ToPhotographs(setId, tags);
        }

        public Photographs GetPhotographs(string[] tags, int count = 25, int page = 0)
        {
			FlickrAdapterGroup adapter = new FlickrAdapterGroup(mCache, mCredentials, count);
			return adapter.ToPhotographs (this.mGroupId, tags, page);
        }

        public Photograph GetPhotograph(string id)
        {
			Photograph output = null;

            string[] flickrId = id.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            if (flickrId.Length == 2)
            {
				FlickrAdapterPhotograph photoAdapter = new FlickrAdapterPhotograph (mCache, mCredentials);
				output = photoAdapter.GetPhotograph(flickrId[0], flickrId[1]);
            }

            return output;
        }
        #endregion
    }
}