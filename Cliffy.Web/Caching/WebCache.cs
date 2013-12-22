using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Caching;
using Cliffy.Common.Caching;

namespace Cliffy.Web.Caching
{
    /// <summary>
    /// Helper class to store items in the HttpCache.
    /// </summary>
    public class WebCache : CacheBase, ICache
    {
        /// <summary>
        /// All of the keys currently used to store items in the cache.
        /// </summary>
        public override List<string> Keys
        {
            get
            {
                List<string> output = new List<string>();
                int count = HttpContext.Current.Cache.Count;
                if (count > 0)
                {
                    IDictionaryEnumerator enumerator = HttpContext.Current.Cache.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        if (enumerator.Key is string)
                        {
                            string key = (string)enumerator.Key;
                            output.Add(key);
                        }
                    }
                }

                return output;
            }
        }

        public WebCache() : base() { }
        public WebCache(int duration) : base(duration) { }

        /// <summary>
        /// Retrieves an object from the Http (web) cache.
        /// </summary>
        /// <param name="key">The key that was used to store the object.</param>
        /// <returns>The object that is in cache. This should return null if the object does not exist.</returns>
        protected override object GetObject(string key)
        {
            HttpContext context = HttpContext.Current;
            return context.Cache[key];
        }

        /// <summary>
        /// Save an object into the cache.
        /// </summary>
        /// <typeparam name="T">The type of object to store.</typeparam>
        /// <param name="key">The key to use to store the object.</param>
        /// <param name="duration">How long (in minutes) to store the item in cache.</param>
        /// <param name="value">The object to store.</param>
        public override void PutInCache<T>(string key, int duration, T value)
        {
            HttpContext context = HttpContext.Current;
            context.Cache.Add(key, value, null, DateTime.Now.AddMinutes(duration), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
        }

        /// <summary>
        /// Removes a value from the cache.
        /// </summary>
        /// <param name="key">The key that was used to store the object.</param>
        public override void RemoveFromCache(string key)
        {
            HttpContext context = HttpContext.Current;
            context.Cache.Remove(key);
        }
    }
}