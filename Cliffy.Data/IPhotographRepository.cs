using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cliffy.Common;

namespace Cliffy.Data
{
    /// <summary>
    /// Defines the methods that a Photograph Manager (e.g. Flickr) object should use.
    /// </summary>
    public interface IPhotographRepository
    {
        /// <summary>
        /// Retreive a group of photographs (e.g. photo album) from the source repository.
        /// </summary>
        /// <param name="tags">Tags to filter the results by. Use an empty array to return all.</param>
        /// <param name="setId">A set, or grouping, identifier. This assumes that the source repository has a way to group photographs into sets or albums.</param>
        /// <returns>The photographs that match the critera.</returns>
        Photographs GetAlbum(string[] tags, string setId);

        /// <summary>
        /// Retreive a list of photographs from the source repository.
        /// </summary>
        /// <param name="tags">Tags to filter the results by. Use an empty array to return all.</param>
        /// <param name="count">The maximum number of photographs to return.</param>
        /// <param name="page">For paging, the starting point for this list.</param>
        /// <returns>The photographs that match the critera.</returns>
        Photographs GetPhotographs(string[] tags, int count = 25, int page = 0);

        /// <summary>
        /// Retrieve a photograph by it's identifier.
        /// </summary>
        /// <param name="id">The photograph identifier.</param>
        /// <returns>The photograph.</returns>
        Photograph GetPhotograph(string id);
    }
}