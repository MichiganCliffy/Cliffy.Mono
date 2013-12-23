using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Cliffy.Common;
using Cliffy.Common.Caching;
using Cliffy.Data;
using Cliffy.Web;

namespace Cliffy.Web.CliffordCorner.Controllers
{
    public class ServicesController : BaseController
    {
        private const int PageLength = 18;

        public class AlbumWrapper
        {
            public List<Photograph> Photographs { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }
        }

        public ServicesController(IBlogRepository blog, IPhotographRepository photographs, ICache cache) : base(blog, photographs, cache)
        {
        }

        #region Albums
        public JsonResult Album(string album)
        {
            var definition = Pages.FirstOrDefault(x => String.Compare(x.Id, album, StringComparison.OrdinalIgnoreCase) == 0);

            var output = new AlbumWrapper();
            
            var builder = new PageBuilder(Pages, mBlog, mPhotographs);
            var page = builder.Build(definition);
            
            var photoSet = page as PagePhotoSet;
            if (photoSet != null)
            {
                output.Title = photoSet.Title;
                output.Content = photoSet.Content;
                output.Photographs = photoSet.Photographs;
            }

            return Json(output, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AlbumPage(string album, string tag)
        {
            var home = Pages.FirstOrDefault(x => String.Compare(x.Id, album, StringComparison.OrdinalIgnoreCase) == 0);

            var pageId = string.Concat(album, "|", tag);
            var definition = home.PageLinks.FirstOrDefault(x => String.Compare(x.Id, pageId, StringComparison.OrdinalIgnoreCase) == 0);

            var output = new AlbumWrapper();

            var builder = new PageBuilder(Pages, mBlog, mPhotographs);
            var page = builder.Build(definition);

            var photoSet = page as PagePhotoSetTag;
            if (photoSet != null)
            {
                output.Title = photoSet.Title;
                output.Content = photoSet.Content;
                output.Photographs = photoSet.Photographs;
            }
            else
            {
                var htmlFrag = page as PageHtmlFragment;
                if (htmlFrag != null)
                {
                    output.Title = htmlFrag.Title;
                    output.Content = htmlFrag.Content;
                }
                else
                {
                    var blogPosts = page as PageBlogPosts;
                    if (blogPosts != null)
                    {
                        output.Title = blogPosts.Title;
                        //output.Content = blogPosts.c
                    }
                }
            }

            return Json(output, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AlbumPhotograph(string album, string page, string photoId, string secret)
        {
            var id = string.Concat(photoId, "/", secret);
            var pageId = string.Concat(album, "|", page);

            var home = Pages.FirstOrDefault(x => x.PageType == PageType.FlickrSet && String.Compare(x.Id, album, StringComparison.OrdinalIgnoreCase) == 0);
            var definition = home.PageLinks.FirstOrDefault(x => x.Id == pageId);
            if (definition == null)
            {
                definition = home;
            }

            var builder = new PageBuilder(Pages, mBlog, mPhotographs);
            var model = builder.Build(definition, id);

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Photographs
        public JsonResult Photographs(int pageNo = 0)
        {
            var model = mPhotographs.GetPhotographs(new string[] { }, PageLength, pageNo);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PhotographsByTag(string tag, int pageNo = 0)
        {
            var model = mPhotographs.GetPhotographs(new string[] { tag.ToLower() }, PageLength, pageNo);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Photograph(string photoId, string secret, string tag = null)
        {
            var id = string.Concat(photoId, "/", secret);
            var model = mPhotographs.GetPhotograph(id);

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}