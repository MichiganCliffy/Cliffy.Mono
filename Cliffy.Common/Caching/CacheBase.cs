using System;
using System.Collections.Generic;
using System.Text;

namespace Cliffy.Common.Caching
{
    public abstract class CacheBase : ICache
    {
        private int mDefaultDuration = 15;

        /// <summary>
        /// Retrieves an object from the cache.
        /// </summary>
        /// <param name="key">The key that was used to store the object.</param>
        /// <returns>The object that is in cache. This should return null if the object does not exist.</returns>
        protected abstract object GetObject(string key);

        public CacheBase() { }

        public CacheBase(int defaultDuration)
        {
            this.mDefaultDuration = defaultDuration;
        }

        #region ICache Members
        /// <summary>
        /// The default duration, in minutes, to store something in cache.
        /// </summary>
        public int DefaultDuration
        {
            get { return this.mDefaultDuration; }
            set { this.mDefaultDuration = value; }
        }

        /// <summary>
        /// All of the keys currently used to store items in the cache.
        /// </summary>
        public abstract List<string> Keys { get; }

        /// <summary>
        /// Checks to see if the item is in the cache.
        /// </summary>
        /// <param name="key">The key that was used to store the object.</param>
        /// <returns>True if the object is in the cache, false otherwise.</returns>
        public virtual bool IsInCache(string key)
        {
            object item = this.GetObject(key);
            if (item != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Retrieves an object from the cache.
        /// </summary>
        /// <typeparam name="T">The type of object that is expected.</typeparam>
        /// <param name="key">The key that was used to store the object.</param>
        /// <param name="value">The object that was stored in the cache.</param>
        /// <returns>True if the data was stored in the cache, false otherwise.</returns>
        public virtual bool GetFromCache<T>(string key, out T value)
        {
            value = default(T);

            if (this.IsInCache(key))
            {
                object output = this.GetObject(key);

                if (output != null)
                {
                    if (output is T)
                    {
                        value = (T)output;
                        return true;
                    }
                    else
                    {
                        this.RemoveFromCache(key);
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Save an object into the cache using the default cache duration.
        /// </summary>
        /// <typeparam name="T">The type of object to store.</typeparam>
        /// <param name="key">The key to use to store the object.</param>
        /// <param name="value">The object to store.</param>
        public virtual void PutInCache<T>(string key, T value)
        {
            this.PutInCache<T>(key, this.mDefaultDuration, value);
        }

        /// <summary>
        /// Save an object into the cache.
        /// </summary>
        /// <typeparam name="T">The type of object to store.</typeparam>
        /// <param name="key">The key to use to store the object.</param>
        /// <param name="duration">How long (in minutes) to store the item in cache.</param>
        /// <param name="value">The object to store.</param>
        public abstract void PutInCache<T>(string key, int duration, T value);

        /// <summary>
        /// Removes a value from the cache.
        /// </summary>
        /// <param name="key">The key that was used to store the object.</param>
        public abstract void RemoveFromCache(string key);

        /// <summary>
        /// Removes a collection of items from the cache.
        /// </summary>
        /// <param name="keys">Collection of keys to remove.</param>
        public virtual void RemoveFromCache(List<string> keys)
        {
            foreach (string key in keys)
            {
                this.RemoveFromCache(key);
            }
        }

        /// <summary>
        /// Removes all items from the cache.
        /// </summary>
        public virtual void ClearCache()
        {
            List<string> keys = this.Keys;
            foreach (string key in keys)
            {
                this.RemoveFromCache(key);
            }
        }
        #endregion
    }
}