using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cliffy.Common
{
    /// <summary>
    /// Defines a list or collection of photographs.
    /// </summary>
    public class Photographs : List<Photograph>
    {
        private List<PhotographTag> mTags = new List<PhotographTag>();

        public List<string> Tags
        {
            get
            {
                List<PhotographTag> tags = this.mTags.OrderByDescending(x => x.Count).ThenBy(x => x.Tag).ToList();
                return tags.ConvertAll<string>(x => x.Tag);
            }
        }

        /// <summary>
        /// A title to associate with the collection.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// A description to associate with the collection.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The default photograph to use when identifying this collection of photographs.
        /// </summary>
        public Photograph Default { get; set; }

        /// <summary>
        /// The total number available - we might not return all and this is needed for paging.
        /// </summary>
        public long Total { get; set; }

        public new void Add(Photograph item)
        {
            this.AddTags(item);
            base.Add(item);
        }

        public void AddTags(IEnumerable<PhotographTag> tags)
        {
            if ((mTags != null) && (mTags.Count > 0))
            {
                mTags.Clear();
            }
            else if (mTags == null)
            {
                mTags = new List<PhotographTag>();
            }

            mTags.AddRange(tags);
        }

        public new void AddRange(IEnumerable<Photograph> items)
        {
            foreach (Photograph item in items)
            {
                this.AddTags(item);
            }

            base.AddRange(items);
        }

		public void AddTags(Photograph item)
        {
            if (item.Tags != null)
            {
                foreach (string tag in item.Tags)
                {
                    PhotographTag photoTag = this.mTags.FirstOrDefault(x => string.Compare(x.Tag, tag, true) == 0);
                    if (photoTag != null)
                    {
                        photoTag.Count++;
                    }
                    else
                    {
                        photoTag = new PhotographTag()
                        {
                            Tag = tag,
                            Count = 1
                        };

                        this.mTags.Add(photoTag);
                    }
                }
            }
        }
    }
}