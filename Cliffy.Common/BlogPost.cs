using System;
using System.Collections.Generic;

namespace Cliffy.Common
{
    public class BlogPost
    {
        private string mId = string.Empty;
        private DateTime mDatePublished = DateTime.Now;
        private DateTime mDateUpdated = DateTime.Now;
        private string mTitle = string.Empty;
        private string mDescription = string.Empty;
        private string mUriSource = string.Empty;
        private List<string> mTags = new List<string>();
        private string mAuthor = string.Empty;

        public string Id
        {
            get { return this.mId; }
            set { this.mId = value; }
        }

        public DateTime DatePublished
        {
            get { return this.mDatePublished; }
            set { this.mDatePublished = value; }
        }

        public DateTime DateUpdated
        {
            get { return this.mDateUpdated; }
            set { this.mDateUpdated = value; }
        }

        public string Title
        {
            get { return this.mTitle; }
            set { this.mTitle = value; }
        }

        public string Description
        {
            get { return this.mDescription; }
            set { this.mDescription = value; }
        }

        public string UriSource
        {
            get { return this.mUriSource; }
            set { this.mUriSource = value; }
        }

        public string Author
        {
            get { return this.mAuthor; }
            set { this.mAuthor = value; }
        }

        public List<string> Tags
        {
            get { return this.mTags; }
            set { this.mTags = value; }
        }

        public BlogPost() { }
    }
}