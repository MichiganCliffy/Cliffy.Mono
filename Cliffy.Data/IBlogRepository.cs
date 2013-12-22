using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cliffy.Common;

namespace Cliffy.Data
{
    /// <summary>
    /// Methods that a blog repository (source) should have. Ways to get posts out of a blog site (e.g. blogger.com).
    /// </summary>
    public interface IBlogRepository
    {
        /// <summary>
        /// Return all posts that match the tags (if provided). If no tags are provided, then return the most recent posts.
        /// </summary>
        /// <param name="tags">Optional parameter to specify the tags to use when loading the posts. If no tags are required, use null. This will return the most recent posts.</param>
        /// <returns>List of blog posts that match the criteria.</returns>
        BlogPosts GetPosts(string[] tags = null);

        /// <summary>
        /// Return a blog post that has the provided id.
        /// </summary>
        /// <param name="id">The id of the post to return.</param>
        /// <returns>The matching blog post. Null if it does not exist.</returns>
        BlogPost GetPost(string id);

        /// <summary>
        /// Return the appropriate source url for a tag. This will link back to a list of posts that have the given tag.
        /// </summary>
        /// <param name="tag">The tag to link back to.</param>
        /// <returns>the appropriate source url for a tag.</returns>
        string GetUriSource(string tag);

        /// <summary>
        /// Return the appropriate source url for an archive date. This will link back to a list of posts that were published on that date.
        /// </summary>
        /// <param name="archive">The archive date to link back to.</param>
        /// <returns>the appropriate source url for the date.</returns>
        string GetUriSource(DateTime archive);
    }
}