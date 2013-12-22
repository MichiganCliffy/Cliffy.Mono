using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Xml;
using Cliffy.Common;
using Cliffy.Common.Logging;
using Cliffy.Common.Caching;

namespace Cliffy.Data.Blogger
{
    /// <summary>
    /// This manages all access to the blogger.com blog repository.
    /// </summary>
    public class BloggerRepository : IBlogRepository
    {
		private readonly ICache mCache;
		private readonly LogWriter mLogger;
		private readonly string mBlogUrl = "http://blog.cliffordcorner.com/";
		private readonly int mCacheDuration = 120;

        public BloggerRepository(ICache cache)
        {
			mCache = cache;
			mLogger = new LogWriter ();

            if (!string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["BloggerUrl"]))
            {
                this.mBlogUrl = ConfigurationManager.AppSettings["BloggerUrl"];
            }

            if (!string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["BloggerCacheDuration"]))
            {
                int duration = int.MinValue;
                if (int.TryParse(ConfigurationManager.AppSettings["BloggerCacheDuration"], out duration))
                {
                    this.mCacheDuration = duration;
                }
            }
        }

		public BloggerRepository(ICache cache, string blogUrl)
        {
			mCache = cache;
			mLogger = new LogWriter ();
            mBlogUrl = blogUrl;

            if (!string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["BloggerCacheDuration"]))
            {
                int duration = int.MinValue;
                if (int.TryParse(ConfigurationManager.AppSettings["BloggerCacheDuration"], out duration))
                {
                    this.mCacheDuration = duration;
                }
            }
        }

        #region IBlogRepository Members

		public BlogPosts GetPosts(string[] tags = null)
        {
			BlogPosts output = new BlogPosts();

            List<BlogPost> posts = null;
            if ((tags != null) && (tags.Length > 0))
            {
                foreach (string tag in tags)
                {
                    posts = this.GetPostsFromCache(tag);
                    if (posts == null)
                    {
                        posts = this.GetPostsFromBlogger(tag);
                    }

                    if ((posts != null) && (posts.Count > 0))
                    {
                        foreach (BlogPost post in posts)
                        {
                            if (!output.Any(x => x.Id == post.Id))
                            {
                                output.Add(post);
                            }
                        }
                    }
                }
            }
            else
            {
                posts = this.GetPostsFromCache("");
                if (posts == null)
                {
                    posts = this.GetPostsFromBlogger("");
                }
            }

            if (posts != null)
            {
                List<BlogPost> sorted = posts.OrderByDescending(x => x.DateUpdated).ThenByDescending(x => x.DatePublished).ToList();
                foreach (BlogPost post in sorted)
                {
                    output.Add(post);
                }
            }

            return output;
        }

        public BlogPost GetPost(string id)
        {
            if (id.Contains("post-"))
            {
                id = id.Substring(id.LastIndexOf("post-") + 5);
            }

            StringBuilder url = new StringBuilder(this.mBlogUrl);
            url.Append("feeds/posts/default/");
            url.Append(id);
            url.Append("?alt=rss");

            return this.GetPostFromUrl(url.ToString());
        }

        public string GetUriSource(string tag)
        {
            if (!string.IsNullOrWhiteSpace(tag))
            {
                return string.Format("{0}search/label/{1}", this.mBlogUrl, tag);
            }
            else
            {
                return this.mBlogUrl;
            }
        }

        public string GetUriSource(DateTime archive)
        {
            if (archive.CompareTo(DateTime.MinValue) != 0)
            {
                return string.Format("{0}{1}_{2:00}_01_archive.html", this.mBlogUrl, archive.Year, archive.Month);
            }
            else
            {
                return this.mBlogUrl;
            }
        }

        #endregion

        private string GetPostsKey(string tag = "")
        {
            StringBuilder output = new StringBuilder("BloggerPosts");

            if (!string.IsNullOrWhiteSpace(tag))
            {
                output.AppendFormat("|{0}", tag);
            }

            return output.ToString();
        }

        private List<BlogPost> GetPostsFromCache(string tag)
        {
            List<BlogPost> output = null;

            if (this.mCache != null)
            {
                string key = this.GetPostsKey(tag);
                if (this.mCache.IsInCache(key))
                {
                    this.mCache.GetFromCache<List<BlogPost>>(key, out output);
                }
            }

            return output;
        }

        private List<BlogPost> GetPostsFromBlogger(string tag)
        {
            StringBuilder url = new StringBuilder(this.mBlogUrl);
            url.Append("feeds/posts/default?alt=rss");

            if (!string.IsNullOrWhiteSpace(tag))
            {
                url.AppendFormat("&category={0}", tag);
            }

            List<BlogPost> output = this.GetPostsFromUrl(url.ToString());
            
            if ((output != null) && (output.Count > 0) && (this.mCache != null))
            {
                string key = this.GetPostsKey(tag);
                this.mCache.PutInCache<List<BlogPost>>(key, this.mCacheDuration, output);
            }

            return output;
        }

        private BlogPost GetPostFromUrl(string url)
        {
            BlogPost output = null;

            XmlReader reader = XmlReader.Create(url);
            Rss20ItemFormatter rssItem = new Rss20ItemFormatter();
            if (rssItem.CanRead(reader))
            {
                rssItem.ReadFrom(reader);
				output = rssItem.Item.ToBlogPost ();
            }
            reader.Close();

            return output;
		}

		private List<BlogPost> GetPostsFromUrl(string url)
        {
            List<BlogPost> output = new List<BlogPost>();

            try
            {
                XmlReader reader = XmlReader.Create(url);
                Rss20FeedFormatter rss = new Rss20FeedFormatter();
                if (rss.CanRead(reader))
                {
                    rss.ReadFrom(reader);
                    foreach (SyndicationItem item in rss.Feed.Items)
                    {
						BlogPost post = item.ToBlogPost();

                        if (!output.Any(x => x.Id == item.Id))
                        {
                            output.Add(post);
                        }
                    }
                }
                reader.Close();
            }
            catch (Exception x)
            {
                mLogger.Log(string.Concat(x.Message, "->", x.StackTrace), LogType.ErrorLog);
            }

            return output;
        }
    }
}