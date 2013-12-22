using System;
using System.Collections.Generic;
using System.Text;

namespace Cliffy.Common.Caching
{
    /// <summary>
    /// Defines a contract that all cache implementations have to fulfill.
    /// </summary>
    public interface ICache
    {
        /// <summary>
        /// The default duration, in minutes, to store something in cache.
        /// </summary>
        int DefaultDuration { get; set; }

        /// <summary>
        /// All of the keys currently used to store items in the cache.
        /// </summary>
        List<string> Keys { get; }

        /// <summary>
        /// Checks to see if the item is in the cache.
        /// </summary>
        /// <param name="key">The key that was used to store the object.</param>
        /// <returns>True if the object is in the cache, false otherwise.</returns>
        bool IsInCache(string key);

        /// <summary>
        /// Retrieves an object from the cache.
        /// </summary>
        /// <typeparam name="T">The type of object that is expected.</typeparam>
        /// <param name="key">The key that was used to store the object.</param>
        /// <param name="value">The object that was stored in the cache.</param>
        /// <returns>True if the data was stored in the cache, false otherwise.</returns>
        bool GetFromCache<T>(string key, out T value);

        /// <summary>
        /// Save an object into the cache using the default cache duration.
        /// </summary>
        /// <typeparam name="T">The type of object to store.</typeparam>
        /// <param name="key">The key to use to store the object.</param>
        /// <param name="value">The object to store.</param>
        void PutInCache<T>(string key, T value);

        /// <summary>
        /// Save an object into the cache.
        /// </summary>
        /// <typeparam name="T">The type of object to store.</typeparam>
        /// <param name="key">The key to use to store the object.</param>
        /// <param name="duration">How long (in minutes) to store the item in cache.</param>
        /// <param name="value">The object to store.</param>
        void PutInCache<T>(string key, int duration, T value);

        /// <summary>
        /// Removes a value from the cache.
        /// </summary>
        /// <param name="key">The key that was used to store the object.</param>
        void RemoveFromCache(string key);

        /// <summary>
        /// Removes a collection of items from the cache.
        /// </summary>
        /// <param name="keys">Collection of keys to remove.</param>
        void RemoveFromCache(List<string> keys);

        /// <summary>
        /// Removes all items from the cache.
        /// </summary>
        void ClearCache();
    }
}