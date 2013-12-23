using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.XPath;
using System.Text;
using Cliffy.Common;
using Cliffy.Common.Caching;
using Cliffy.Data;

namespace Cliffy.Web.CliffordCorner
{
    /// <summary>
    /// Utility method for taking a page definition and building out the appropriate page.
    /// </summary>
    public class PageBuilder
    {
        public class Constants
        {
            public const string MetaTitlePrefix = "The Clifford Corner => ";
        }

        private List<PageDefinition> mPages;
        private IBlogRepository mBlog;
        private IPhotographRepository mPhotographs;

        public PageBuilder(List<PageDefinition> pages, IBlogRepository blog, IPhotographRepository photographs)
        {
            mPages = pages;
            mBlog = blog;
            mPhotographs = photographs;
        }

        public IPage Build(PageDefinition page, int pageNo = 0, int pageLength = 70)
        {
            IPage output = null;

            switch (page.PageType)
            {
                case PageType.BlogPosts:
                case PageType.Home:
                    output = BlogPosts(page);
                    break;

                case PageType.FlickrGroup:
                    output = PhotoGroup(page, pageNo, pageLength);
                    break;

                case PageType.FlickrGroupTag:
                    output = PhotoGroupTag(page, pageNo, pageLength);
                    break;

                case PageType.FlickrSet:
                    output = PhotoSet(page);
                    break;

                case PageType.FlickrSetTag:
                    output = PhotoSetTag(page);
                    break;

                case PageType.HtmlFragment:
                    output = HtmlFragment(page);
                    break;

                case PageType.Redirect:
                    output = Redirect(page);
                    break;
            }

            return output;
        }

        public IPage Build(PageDefinition page, string photoId, int pageNo = 0, int pageLength = 70)
        {
            IPage output = null;

            switch (page.PageType)
            {
                case PageType.FlickrSet:
                case PageType.FlickrSetTag:
                    output = PhotoSetPhoto(page, photoId);
                    break;

                case PageType.FlickrGroup:
                case PageType.FlickrGroupTag:
                    output = PhotoGroupPhoto(page, photoId, pageNo, pageLength);
                    break;
            }

            return output;
        }

        public IPage HttpError404(string url)
        {
            PageError model = new PageError();
            model.Navigation = Navigation(null);

            if (HttpContext.Current != null)
            {
                HttpRequest request = HttpContext.Current.Request;

                // If the url is relative ('NotFound' route) then replace with Requested path
                string requestedUrl = request.Url.OriginalString.Contains(url) & request.Url.OriginalString != url ? request.Url.OriginalString : url;
                model.RequestedUrl = requestedUrl;

                // Dont get the user stuck in a 'retry loop' by 
                // allowing the Referrer to be the same as the Request 
                string referingUrl = request.UrlReferrer != null &&
                    request.UrlReferrer.OriginalString != requestedUrl ?
                    request.UrlReferrer.OriginalString : null;
                model.ReferingUrl = referingUrl;
            }

            return model;
        }

        public string MetaTitle(PageDefinition page)
        {
            return string.Concat(Constants.MetaTitlePrefix, page.MetaDescription);
        }

        public string MetaDescription(PageDefinition page)
        {
            if (!string.IsNullOrWhiteSpace(page.MetaDescription))
            {
                return page.MetaDescription;
            }
            else
            {
                // TODO: assign some default meta description
                return "lorum ipsum";
            }
        }

        public string MetaKeywords(PageDefinition page)
        {
            StringBuilder output = new StringBuilder("Clifford Corner, Clifford Family, San Francisco, Ann Arbor");

            if (page.IsTravelLog)
            {
                output.Append(", Travel Log, Travellog");
            }

            return output.ToString();
        }

        public string MetaKeywordsUpdate(IPage page, Photographs photographs)
        {
            StringBuilder output = new StringBuilder(page.MetaKeywords);

            if ((photographs != null) && (photographs.Tags != null) && (photographs.Tags.Count > 0))
            {
                foreach (string tag in photographs.Tags)
                {
                    if (output.Length > 0)
                    {
                        output.Append(", ");
                    }

                    output.Append(tag.ToLower());
                }
            }

            return output.ToString();
        }

        public string MetaKeywordsUpdate(IPage page, Photograph photograph)
        {
            StringBuilder output = new StringBuilder(page.MetaKeywords);

            if ((photograph != null) && (photograph.Tags != null) && (photograph.Tags.Count > 0))
            {
                foreach (string tag in photograph.Tags)
                {
                    if (output.Length > 0)
                    {
                        output.Append(", ");
                    }

                    output.Append(tag.ToLower());
                }
            }

            return output.ToString();
        }

        public string MetaKeywordsUpdate(IPage page, BlogPosts posts)
        {
            StringBuilder output = new StringBuilder(page.MetaKeywords);

            if ((posts != null) && (posts.Tags != null) && (posts.Tags.Count > 0))
            {
                foreach (string tag in posts.Tags)
                {
                    if (output.Length > 0)
                    {
                        output.Append(", ");
                    }

                    output.Append(tag.ToLower());
                }
            }

            return output.ToString();
        }

        public PageLinks GlobalNavigation()
        {
            var output = new PageLinks();

            if (mPages != null)
            {
                var pages = mPages.Where(x => string.IsNullOrWhiteSpace(x.ParentId)).OrderBy(x => x.SortOrder).ToList();
                foreach (var page in pages)
                {
                    output.Add(page);
                }
            }

            return output;
        }

        public Dictionary<NavigationType, PageLinks> Navigation(PageDefinition page)
        {
            Dictionary<NavigationType, PageLinks> output = new Dictionary<NavigationType, PageLinks>();
            output.Add(NavigationType.Global, GlobalNavigation());

            if (page != null)
            {
                if ((page.PageLinks != null) && (page.PageLinks.Count > 0))
                {
                    output.Add(NavigationType.Sub, page.PageLinks);

                    if (page.IsTravelLog)
                    {
                        PageDefinition overview;

                        // make sure we add a link for the parent photoset
                        if (string.IsNullOrWhiteSpace(page.ParentId))
                        {
                            if (!output[NavigationType.Sub].Any(x => x.Id == page.Id))
                            {
                                overview = (PageDefinition)page.Clone();
                                //overview.Title = "Overview";
                                output[NavigationType.Sub].Insert(0, overview);
                            }
                        }
                        else if (mPages != null)
                        {
                            PageDefinition parent = mPages.FirstOrDefault(x => x.Id == page.ParentId);
                            if ((parent != null) && (!output[NavigationType.Sub].Any(x => x.Id == parent.Id)))
                            {
                                overview = (PageDefinition)parent.Clone();
                                //overview.Title = "Overview";
                                output[NavigationType.Sub].Insert(0, overview);
                            }
                        }
                    }
                }
            }

            return output;
        }

        public PageHtmlFragment HtmlFragment(PageDefinition page)
        {
            PageHtmlFragment output = null;

            if (page.PageType == PageType.HtmlFragment)
            {
                output = new PageHtmlFragment();
                page.CopyTo(output);

                output.MetaTitle = MetaTitle(page);
                output.MetaDescription = MetaDescription(page);
                output.MetaKeywords = MetaKeywords(page);
                output.Navigation = Navigation(page);

                string path = page.PageSettings["path"];
                if (HttpContext.Current != null)
                {
                    path = HttpContext.Current.Server.MapPath(path);
                }
                else
                {
                    path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
                }

                if (File.Exists(path))
                {
                    output.Content = File.ReadAllText(path);
                }
            }

            return output;
        }

        public PageBlogPosts BlogPosts(PageDefinition page)
        {
            PageBlogPosts output = null;

            if ((page.PageType == PageType.BlogPosts) ||
                (page.PageType == PageType.Home))
            {
                output = new PageBlogPosts();
                page.CopyTo(output);

                output.MetaTitle = MetaTitle(page);
                output.MetaDescription = MetaDescription(page);
                output.MetaKeywords = MetaKeywords(page);
                output.Navigation = Navigation(page);

                List<string> labels = new List<string>();
                if (!string.IsNullOrWhiteSpace(page.PageSettings["label"]))
                {
                    string label = page.PageSettings["label"];
                    output.Tag = HttpUtility.UrlDecode(label);
                    labels.Add(label);
                }

                BlogPosts posts = mBlog.GetPosts(labels.ToArray());

                output.Posts = posts.OrderByDescending(x => x.DatePublished).ToList();

                PageLinks tagCloud = TagCloud(page, posts);
                if (tagCloud != null)
                {
                    output.Navigation.Add(NavigationType.PopularTags, tagCloud);
                }

                PageLinks archives = ArchivedPosts(posts);
                if (archives != null)
                {
                    output.Navigation.Add(NavigationType.ArchivedPosts, archives);
                }

                output.MetaKeywords = MetaKeywordsUpdate(output, posts);
            }

            return output;
        }

        public PageRedirect Redirect(PageDefinition page)
        {
            PageRedirect output = null;

            if (page.PageType == PageType.Redirect)
            {
                output = new PageRedirect();
                page.CopyTo(output);

                output.MetaTitle = MetaTitle(page);
                output.MetaDescription = MetaDescription(page);
                output.MetaKeywords = MetaKeywords(page);
                output.Navigation = Navigation(page);
                output.RedirectTo = page.PageSettings["uri"];
            }

            return output;
        }

        public PagePhotoSet PhotoSet(PageDefinition page)
        {
            PagePhotoSet output = null;

            if (page.PageType == PageType.FlickrSet)
            {
                output = new PagePhotoSet();
                page.CopyTo(output);

                output.MetaTitle = MetaTitle(page);
                output.MetaDescription = MetaDescription(page);
                output.MetaKeywords = MetaKeywords(page);
                output.Navigation = Navigation(page);

                Photographs photographs = mPhotographs.GetAlbum(new string[] { }, page.PageSettings["setId"]);
                output.Spotlight = photographs.Default;
                output.Photographs = new List<Photograph>(photographs);
                output.Content = photographs.Description;

                output.MetaKeywords = MetaKeywordsUpdate(output, photographs);
            }

            return output;
        }

        public PagePhotoSetTag PhotoSetTag(PageDefinition page)
        {
            PagePhotoSetTag output = null;

            if (page.PageType == PageType.FlickrSetTag)
            {
                output = new PagePhotoSetTag();
                page.CopyTo(output);

                output.MetaTitle = MetaTitle(page);
                output.MetaDescription = MetaDescription(page);
                output.MetaKeywords = MetaKeywords(page);
                output.Navigation = Navigation(page);

                if (!string.IsNullOrWhiteSpace(page.PageSettings["content"]))
                {
                    output.Content = page.PageSettings["content"];
                }

                output.Tag = page.PageSettings["tag"];

                Photographs photographs = mPhotographs.GetAlbum(new string[] { output.Tag }, page.PageSettings["setId"]);
                output.Photographs = photographs;

                output.MetaKeywords = MetaKeywordsUpdate(output, photographs);
            }

            return output;
        }

        public PagePhotoSetPhoto PhotoSetPhoto(PageDefinition page, string photoId)
        {
            PagePhotoSetPhoto output = null;

            if ((page.PageType == PageType.FlickrSetTag) ||
                (page.PageType == PageType.FlickrSet))
            {
                output = new PagePhotoSetPhoto();
                page.CopyTo(output);

                output.Id += string.Concat("|", photoId);
                output.MetaTitle = MetaTitle(page);
                output.MetaDescription = MetaDescription(page);
                output.MetaKeywords = MetaKeywords(page);
                output.Navigation = Navigation(page);

                List<string> tags = new List<string>();
                if (page.PageType == PageType.FlickrSetTag)
                {
                    string tag = page.PageSettings["tag"];
                    if (!string.IsNullOrWhiteSpace(tag))
                    {
                        tags.Add(tag);
                        output.Tag = tag;
                    }
                }

                Photographs photographs = mPhotographs.GetAlbum(tags.ToArray(), page.PageSettings["setId"]);
                output.Photograph = photographs.FirstOrDefault(x => string.Compare(x.UniqueId, photoId, true) == 0);

                output.MetaKeywords = MetaKeywordsUpdate(output, photographs);
            }

            return output;
        }

        public PagePhotoGroup PhotoGroup(PageDefinition page, int pageNo = 0, int pageLength = 70)
        {
            PagePhotoGroup output = null;

            if (page.PageType == PageType.FlickrGroup)
            {
                output = new PagePhotoGroup();
                page.CopyTo(output);

                if (pageNo > 1)
                {
                    output.Id = string.Concat(output.Id, "|page", pageNo);
                }

                output.MetaTitle = MetaTitle(page);
                output.MetaDescription = MetaDescription(page);
                output.MetaKeywords = MetaKeywords(page);
                output.Navigation = Navigation(page);

                Photographs photographs = mPhotographs.GetPhotographs(new string[] { }, pageLength, pageNo);
                output.Photographs = photographs;

                PageLinks tagCloud = TagCloud(page, photographs);
                if (tagCloud != null)
                {
                    output.Navigation.Add(NavigationType.PopularTags, tagCloud);
                }

                PageLinks paging = Paging(page, photographs, pageLength);
                if (paging != null)
                {
                    output.Navigation.Add(NavigationType.Paging, paging);
                }

                output.MetaKeywords = MetaKeywordsUpdate(output, photographs);
            }

            return output;
        }

        public PagePhotoGroupTag PhotoGroupTag(PageDefinition page, int pageNo = 0, int pageLength = 70)
        {
            PagePhotoGroupTag output = null;

            if (page.PageType == PageType.FlickrGroupTag)
            {
                output = new PagePhotoGroupTag();
                page.CopyTo(output);

                if (pageNo > 1)
                {
                    output.Id = string.Concat(output.Id, "|page", pageNo);
                }

                output.MetaTitle = MetaTitle(page);
                output.MetaDescription = MetaDescription(page);
                output.MetaKeywords = MetaKeywords(page);
                output.Navigation = Navigation(page);

                output.Tag = page.PageSettings["tag"];

                if (pageNo > 0)
                {
                    pageNo--;
                }

                Photographs photographs = mPhotographs.GetPhotographs(new string[] { output.Tag }, pageLength, pageNo);
                output.Photographs = photographs;

                PageLinks tagCloud = TagCloud(page, photographs);
                if (tagCloud != null)
                {
                    output.Navigation.Add(NavigationType.PopularTags, tagCloud);
                }

                PageLinks paging = Paging(page, photographs, pageLength);
                if (paging != null)
                {
                    output.Navigation.Add(NavigationType.Paging, paging);
                }

                output.MetaKeywords = MetaKeywordsUpdate(output, photographs);
            }

            return output;
        }

        public PagePhotoGroupPhoto PhotoGroupPhoto(PageDefinition page, string photoId, int pageNo = 0, int pageLength = 70)
        {
            PagePhotoGroupPhoto output = null;

            if ((page.PageType == PageType.FlickrGroup) ||
                (page.PageType == PageType.FlickrGroupTag))
            {
                output = new PagePhotoGroupPhoto();
                page.CopyTo(output);

                output.Id += string.Concat("|", photoId);
                output.MetaTitle = MetaTitle(page);
                output.MetaDescription = MetaDescription(page);
                output.MetaKeywords = MetaKeywords(page);
                output.Navigation = Navigation(page);

                List<string> tags = new List<string>();
                if (!string.IsNullOrWhiteSpace(page.PageSettings["tag"]))
                {
                    tags.Add(page.PageSettings["tag"]);
                }

                output.Photograph = mPhotographs.GetPhotograph(photoId);

                PageLinks imageTags = TagCloud(page, output.Photograph);
                if (imageTags != null)
                {
                    output.Navigation.Add(NavigationType.ImageTags, imageTags);
                }

                Photographs photographs = mPhotographs.GetPhotographs(tags.ToArray(), pageLength, pageNo);
                PageLinks popularTags = TagCloud(page, photographs, "Popular Tags");
                if (popularTags != null)
                {
                    output.Navigation.Add(NavigationType.PopularTags, popularTags);
                }

                PageLinks recentPosts = RecentPosts();
                if (recentPosts != null)
                {
                    output.Navigation.Add(NavigationType.RecentPosts, recentPosts);
                }

                output.MetaKeywords = MetaKeywordsUpdate(output, output.Photograph);
            }

            return output;
        }

        private PageLinks Paging(PageDefinition page, Photographs photographs, int pageLength)
        {
            PageLinks output = new PageLinks();
            output.Title = "Pages";

            if (photographs.Total > pageLength)
            {
                int pageCount = Convert.ToInt32(photographs.Total / pageLength);
                if (photographs.Total % photographs.Count != 0)
                {
                    pageCount += 1;
                }

                for (int i = 1; i <= pageCount; i++)
                {
                    var link = (PageDefinition)page.Clone();
                    link.Title = i.ToString();
                    if (i > 1)
                    {
                        link.Id = string.Concat(page.Id, "|page", i);
                    }

                    link.SortOrder = output.Count;
                    output.Add(link);
                }
            }

            return output;
        }

        private PageLinks RecentPosts()
        {
            PageLinks output = new PageLinks();
            output.Title = "Recent Posts";

            if (mBlog != null)
            {
                List<BlogPost> posts = mBlog.GetPosts().Take(5).ToList();

                foreach (BlogPost post in posts)
                {
                    var link = new PageDefinition()
                    {
                        MetaDescription = post.Title,
                        Id = string.Concat("blog|", post.Id),
                        IsTravelLog = false,
                        ParentId = "blog",
                        SortOrder = output.Count,
                        Title = post.Title
                    };

                    output.Add(link);
                }
            }

            return output;
        }

        private PageLinks ArchivedPosts(BlogPosts blog)
        {
            PageLinks output = new PageLinks();
            output.Title = "Archives";

            if (blog != null)
            {
                List<DateTime> archives = blog.Archives;
                foreach (DateTime archive in archives)
                {
                    var link = new PageDefinition()
                    {
                        MetaDescription = string.Format("{0:MMMM} {0:yyyy}", archive.Date),
                        Id = mBlog.GetUriSource(archive),
                        IsTravelLog = false,
                        ParentId = string.Empty,
                        SortOrder = output.Count,
                        Title = string.Format("{0:MMMM} {0:yyyy}", archive.Date)
                    };

                    output.Add(link);
                }
            }

            return output;
        }

        private PageLinks TagCloud(PageDefinition page, BlogPosts posts, string title = "Top Labels")
        {
            PageLinks output = new PageLinks()
            {
                Title = title
            };

            List<string> tags = posts.Tags.Take(10).ToList();
            if (tags.Count > 0)
            {
                foreach (string tag in tags)
                {
                    var link = (PageDefinition)page.Clone();
                    link.Id = mBlog.GetUriSource(tag);
                    link.SortOrder = output.Count;
                    link.Title = tag;

                    output.Add(link);
                }
            }

            return output;
        }

        private PageLinks TagCloud(PageDefinition page, Photographs photographs, string title = "Popular Tags")
        {
            PageLinks output = new PageLinks()
            {
                Title = title
            };

            List<string> tags = photographs.Tags.Take(10).ToList();
            if (tags.Count > 0)
            {
                foreach (string tag in tags)
                {
                    string id = string.Concat(page.Id, "|", tag);
                    if (!string.IsNullOrWhiteSpace(page.ParentId))
                    {
                        id = string.Concat(page.ParentId, "|", tag);
                    }

                    var link = (PageDefinition)page.Clone();
                    link.Id = id;
                    link.SortOrder = output.Count;
                    link.Title = tag;

                    if ((page.PageType == PageType.FlickrGroup) ||
                        (page.PageType == PageType.FlickrGroupTag))
                    {
                        link.PageType = PageType.FlickrGroupTag;
                    }
                    else if ((page.PageType == PageType.FlickrSet) ||
                        (page.PageType == PageType.FlickrSetTag))
                    {
                        link.PageType = PageType.FlickrSetTag;
                    }

                    output.Add(link);
                }
            }

            return output;
        }

        private PageLinks TagCloud(PageDefinition page, Photograph photograph, int count = 5)
        {
            PageLinks output = null;

            if (photograph != null)
            {
                var tags = new List<string>();
                if (photograph.Tags != null && photograph.Tags.Count > 0)
                {
                    tags.AddRange(photograph.Tags.Take(count));
                }

                if (tags.Count > 0)
                {
                    output = new PageLinks()
                    {
                        Title = "Image Tags"
                    };

                    foreach (string tag in tags)
                    {
                        var link = (PageDefinition)page.Clone();
                        link.Id = string.Concat(page.Id, "|", tag);
                        link.SortOrder = output.Count;
                        link.Title = tag;

                        if ((page.PageType == PageType.FlickrGroup) ||
                            (page.PageType == PageType.FlickrGroupTag))
                        {
                            link.PageType = PageType.FlickrGroupTag;
                        }
                        else if ((page.PageType == PageType.FlickrSet) ||
                            (page.PageType == PageType.FlickrSetTag))
                        {
                            link.PageType = PageType.FlickrSetTag;
                        }

                        output.Add(link);
                    }
                }
            }

            return output;
        }

        private ThumbnailLink Thumbnail(PageDefinition page, Photograph photograph)
        {
            ThumbnailLink output = new ThumbnailLink()
            {
                Id = photograph.UniqueId,
                IsTravelLog = page.IsTravelLog,
                IsVideo = string.Compare(photograph.Media, "video", true) == 0,
                ParentId = page.Id,
                MetaDescription = photograph.Description,
                Title = photograph.Title,
                Uri = photograph.UriSizes.First(x => x.Size == ImageSize.Thumbnail).Uri
            };

            if (string.IsNullOrWhiteSpace(output.MetaDescription))
            {
                output.MetaDescription = output.Title;
            }

            return output;
        }
    }
}