using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cliffy.Common;
using Cliffy.Data;
using Cliffy.Data.Blogger;

namespace Cliffy.Data.Tests
{
    /// <summary>
    /// Test accessing Blogger.Com.
    /// </summary>
	[TestClass]
    public class BloggerRepositoryTests
    {
        /// <summary>
        /// Get most recent blog posts.
        /// </summary>
		[TestMethod]
        [TestCategory("Blogger")]
        public void GetRecentPostsTest()
        {
            IBlogRepository repository = new BloggerRepository(null);

			BlogPosts posts = repository.GetPosts();
            Assert.IsNotNull(posts);
            Assert.AreNotEqual(0, posts.Count);
        }

        /// <summary>
        /// Get most blog posts by tag.
        /// </summary>
		[TestMethod]
        [TestCategory("Blogger")]
        public void GetPostsByTagTest()
        {
            IBlogRepository repository = new BloggerRepository(null);

			BlogPosts posts = repository.GetPosts(new string[] { "2010", "2009" });
            Assert.IsNotNull(posts);
            Assert.AreNotEqual(0, posts.Count);
        }

        /// <summary>
        /// Make sure we get the most popular tags from the collection of posts.
        /// </summary>
		[TestMethod]
        [TestCategory("Blogger")]
        public void GetTagsTest()
        {
			BlogPosts posts = new BlogPosts();

            for (int i = 1; i <= 5; i++)
            {
                BlogPost post = new BlogPost();
                post.Id = string.Concat("Post", i);

                switch (i)
                {
                    case 1:
                        post.Tags.Add("abcde");
                        post.Tags.Add("debbie");
                        break;

                    case 2:
                        post.Tags.Add("tomtom");
                        break;

                    case 3:
                        post.Tags.Add("debbie");
                        post.Tags.Add("abcde");
                        post.Tags.Add("tomtom");
                        break;

                    case 4:
                        post.Tags.Add("tomtom");
                        break;

                    case 5:
                        post.Tags.Add("bill");
                        break;
                }

                posts.Add(post);
            }

            List<string> tags = posts.Tags;
            Assert.AreEqual(4, tags.Count);
            Assert.AreEqual(tags[0], "tomtom");
            Assert.AreEqual(tags[1], "abcde");
            Assert.AreEqual(tags[2], "debbie");
            Assert.AreEqual(tags[3], "bill");
        }

        /// <summary>
        /// Make sure we get the archive dates from the collection of posts.
        /// </summary>
		[TestMethod]
        [TestCategory("Blogger")]
        public void GetArchivesTest()
        {
			BlogPosts posts = new BlogPosts();

            for (int i = 1; i <= 5; i++)
            {
                BlogPost post = new BlogPost();
                post.Id = string.Concat("Post", i);

                switch (i)
                {
                    case 1:
                        post.DatePublished = new DateTime(2011, 1, 10);
                        break;

                    case 2:
                        post.DatePublished = new DateTime(2011, 12, 10);
                        break;

                    case 3:
                        post.DatePublished = new DateTime(2011, 6, 10);
                        break;

                    case 4:
                        post.DatePublished = new DateTime(2012, 1, 10);
                        break;

                    case 5:
                        post.DatePublished = new DateTime(2011, 8, 10);
                        break;
                }

                posts.Add(post);
            }

            List<DateTime> archives = posts.Archives;
            Assert.AreEqual(archives[0], new DateTime(2012, 1, 1));
            Assert.AreEqual(archives[1], new DateTime(2011, 12, 1));
            Assert.AreEqual(archives[2], new DateTime(2011, 8, 1));
            Assert.AreEqual(archives[3], new DateTime(2011, 6, 1));
            Assert.AreEqual(archives[4], new DateTime(2011, 1, 1));
        }

        /// <summary>
        /// Get most blog post by it's id.
        /// </summary>
		[TestMethod]
        [TestCategory("Blogger")]
        public void GetPostByIdTest()
        {
            IBlogRepository repository = new BloggerRepository(null);

            BlogPost post = repository.GetPost("tag:blogger.com,1999:blog-14935203.post-3115974897893475339");
            Assert.IsNotNull(post);
        }

        /// <summary>
        /// Get the url to an archive page for Blogger.
        /// </summary>
		[TestMethod]
        [TestCategory("Blogger")]
        public void GetUriSourceForArchiveTest()
        {
            IBlogRepository repository = new BloggerRepository(null);

            DateTime archive = new DateTime(2011, 5, 10);
            string expected = "http://blog.cliffordcorner.com/2011_05_01_archive.html";
            string actual = repository.GetUriSource(archive);

            Assert.AreEqual(expected, actual);
        }
    }
}