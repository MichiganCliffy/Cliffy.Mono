using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cliffy.Common;
using Cliffy.Data;
using Cliffy.Data.Mongo;

namespace Cliffy.Data.Tests
{
	[TestClass]
    public class MongoPhotographRepositoryTests
    {
        [TestMethod]
        [TestCategory("MongoDb")]
		public void GetPhotographTest()
        {
			string id = "8849855146/cb4a41f094";
            var repo = new MongoPhotographRepository();
            var photo = repo.GetPhotograph(id);
            Assert.IsNotNull(photo);
            Assert.AreEqual(id, photo.UniqueId);
        }

        [TestMethod]
        [TestCategory("MongoDb")]
        public void GetAlbumNullTagsTest()
        {
			string setId = "72157624473398430";
            var repo = new MongoPhotographRepository();
            var album = repo.GetAlbum(null, setId);
            Assert.IsNotNull(album);
            Assert.AreEqual("2010 Sabbatical", album.Title);
            Assert.AreEqual(300, album.Total);
            Assert.AreEqual(300, album.Count);
        }

        [TestMethod]
        [TestCategory("MongoDb")]
        public void GetAlbumEmptyTagsTest()
        {
			string setId = "72157624473398430";
            var tags = new List<string>();
            var repo = new MongoPhotographRepository();
            var album = repo.GetAlbum(tags.ToArray(), setId);
            Assert.IsNotNull(album);
            Assert.AreEqual("2010 Sabbatical", album.Title);
            Assert.AreEqual(300, album.Total);
            Assert.AreEqual(300, album.Count);
        }

        [TestMethod]
        [TestCategory("MongoDb")]
        public void GetAlbumSingleTagTest()
        {
			string setId = "72157624473398430";
            var tags = new List<string>();
            tags.Add("hvar");

            var repo = new MongoPhotographRepository();
            var album = repo.GetAlbum(tags.ToArray(), setId);
            Assert.IsNotNull(album);
            Assert.AreEqual("2010 Sabbatical", album.Title);
            Assert.AreEqual(300, album.Total);
            Assert.AreEqual(15, album.Count);
        }

        [TestMethod]
        [TestCategory("MongoDb")]
        public void GetAlbumMultiTagTest()
        {
			string setId = "72157624473398430";
            var tags = new List<string>();
            tags.Add("hvar");
            tags.Add("santorini");

            var repo = new MongoPhotographRepository();
            var album = repo.GetAlbum(tags.ToArray(), setId);
            Assert.IsNotNull(album);
            Assert.AreEqual("2010 Sabbatical", album.Title);
            Assert.AreEqual(300, album.Total);
            Assert.AreEqual(39, album.Count);
        }

        [TestMethod]
        [TestCategory("MongoDb")]
        public void GetPhotographsNullTagsNoPagingTest()
        {
            var repo = new MongoPhotographRepository();
            var album = repo.GetPhotographs(null);
            Assert.IsNotNull(album);
            Assert.AreEqual(25, album.Count);
        }

        [TestMethod]
        [TestCategory("MongoDb")]
        public void GetPhotographsNullTagsWithPagingTest()
        {
            var repo = new MongoPhotographRepository();
            var firstPage = repo.GetPhotographs(null);
            Assert.IsNotNull(firstPage);
            Assert.AreEqual(25, firstPage.Count);

            var secondPage = repo.GetPhotographs(null, 25, 1);
            Assert.IsNotNull(secondPage);
            Assert.AreEqual(25, secondPage.Count);
            foreach (var item in secondPage)
            {
                Assert.IsFalse(firstPage.Any(x => x.UniqueId == item.UniqueId));
            }

            var bothPages = repo.GetPhotographs(null, 50);
            Assert.AreEqual(bothPages[24].UniqueId, firstPage[24].UniqueId);
            Assert.AreEqual(bothPages[25].UniqueId, secondPage[0].UniqueId);
        }

        [TestMethod]
        [TestCategory("MongoDb")]
        public void GetPhotographsSingleTagNoPagingTest()
        {
            var tags = new string[] { "2011" };
            var repo = new MongoPhotographRepository();
            var page = repo.GetPhotographs(tags, count: 1000);

            Console.WriteLine(page.Count);
            Console.WriteLine(page.Total);

            Assert.AreEqual(page.Count, page.Total);
        }
    }
}
