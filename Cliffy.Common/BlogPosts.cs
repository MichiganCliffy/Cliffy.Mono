using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cliffy.Common
{
    /// <summary>
    /// Defines a list or collection of blog posts.
    /// </summary>
    public class BlogPosts : List<BlogPost>
    {
        private List<BlogTag> mTags = new List<BlogTag>();
        private List<DateTime> mArchives = new List<DateTime>();

        public List<string> Tags
        {
            get
            {
                List<BlogTag> tags = this.mTags.OrderByDescending(x => x.Count).ThenBy(x => x.Tag).ToList();
                return tags.ConvertAll<string>(x => x.Tag);
            }
        }

        public List<DateTime> Archives
        {
            get
            {
                return this.mArchives.OrderByDescending(x => x).ToList();
            }
        }

        public new void Add(BlogPost item)
        {
            this.AddTags(item);
            this.AddArchive(item);
            base.Add(item);
        }

        public new void AddRange(IEnumerable<BlogPost> items)
        {
            foreach (BlogPost item in items)
            {
                this.AddTags(item);
                this.AddArchive(item);
            }

            base.AddRange(items);
        }

        private void AddArchive(BlogPost item)
        {
            if (item.DatePublished.CompareTo(DateTime.MinValue) != 0)
            {
                DateTime archive = new DateTime(item.DatePublished.Year, item.DatePublished.Month, 1);
                if (!this.mArchives.Any(x => x.CompareTo(archive) == 0))
                {
                    this.mArchives.Add(archive);
                }
            }
        }

        private void AddTags(BlogPost item)
        {
            if (item.Tags != null)
            {
                foreach (string tag in item.Tags)
                {
                    BlogTag blogTag = this.mTags.FirstOrDefault(x => string.Compare(x.Tag, tag, true) == 0);
                    if (blogTag != null)
                    {
                        blogTag.Count++;
                    }
                    else
                    {
                        blogTag = new BlogTag()
                        {
                            Tag = tag,
                            Count = 1
                        };

                        this.mTags.Add(blogTag);
                    }
                }
            }
        }
    }
}