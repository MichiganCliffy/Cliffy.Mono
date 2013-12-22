using System;
using System.Collections;
using System.Configuration;
using System.Diagnostics;
using Cliffy.Common;
using Cliffy.Common.Caching;
using Cliffy.Common.Logging;
using FlickrNet;

namespace Cliffy.Data.Flickr
{
    public abstract class FlickrAdapterBase
    {
		protected readonly FlickrCredentials mCredentials;
		protected readonly ICache mCache;

		private FlickrAdapterBase(ICache cache) {
			this.mCache = cache;
		}

		public FlickrAdapterBase(ICache cache, FlickrCredentials credentials) : this(cache)
        {
            this.mCredentials = credentials;
        }
    }
}