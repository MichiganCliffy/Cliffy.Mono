
namespace Cliffy.Web.CliffordCorner
{
    /// <summary>
    /// Identifies the type of page that is being rendered.
    /// </summary>
    public enum PageType
    {
        /// <summary>
        /// Should not get this, it means we have an unsupported page type that we don't know how to wire up.
        /// </summary>
        Unknown,

        /// <summary>
        /// The landing page for a Flickr Photoset. This contains links to tags within the set, blog posts, and html fragments.
        /// </summary>
        FlickrSet,

        /// <summary>
        /// A subset of photographs for a Flickr Photoset, all with a common tag.
        /// </summary>
        FlickrSetTag,

        /// <summary>
        /// A page that contains a block of HTML, specified by a path to an html file.
        /// </summary>
        HtmlFragment,

        /// <summary>
        /// Redirection to another app or page outside of cliffordcorner.
        /// </summary>
        Redirect,

        /// <summary>
        /// A group of blog posts grouped by a common blog label (tag).
        /// </summary>
        BlogPosts,

        /// <summary>
        /// A set of photographs from a Flickr Group.
        /// </summary>
        FlickrGroup,

        /// <summary>
        /// A subset of photographs from a Flickr Group, all with a common tag.
        /// </summary>
        FlickrGroupTag,

        /// <summary>
        /// Defines the home page.
        /// </summary>
        Home
    }
}