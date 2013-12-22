using Cliffy.Common.Caching;
using Cliffy.Web.CliffordCorner.ActionFilters;
using Cliffy.Web.CliffordCorner.ViewMappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Cliffy.Web.CliffordCorner.Controllers
{
    [MobileCheck]
    public class DefaultController : BaseController
    {
        private string mRedirect = string.Empty;

        public DefaultController(IBlogRepository blog, IPhotographRepository photographs, ICache cache) : base(blog, photographs, cache)
        {
        }

        public ActionResult Http404()
        {
            var url = HttpContext.Request.Url.ToString();
            if (url.Contains("+"))
            {
                Response.StatusCode = (int)HttpStatusCode.Moved;
                Response.Redirect(url.Replace('+', '-'));
                return new EmptyResult();
            }
            else if (url.Contains("~"))
            {
                Response.StatusCode = (int)HttpStatusCode.Moved;
                Response.Redirect(url.Replace("~/", string.Empty));
                return new EmptyResult();
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;

                PageBuilder builder = new PageBuilder(Pages, mBlog, mPhotographs);
                IPage model = builder.HttpError404(url);
                return View(model);
            }
        }

        public ActionResult Index()
        {
            var page = Pages.FirstOrDefault(x => x.PageType == PageType.Home);
            if (page == null)
            {
                page = new PageDefinition
                {
                    Id = "Home",
                    PageType = PageType.Home
                };
            }

            var builder = new PageBuilder(Pages, mBlog, mPhotographs);
            var model = builder.Build(page);

            return MapToView(model);
        }

        #region Photographs
        public ActionResult Photographs(int pageNo = 0)
        {
            var definition = Pages.FirstOrDefault(x => x.PageType == PageType.FlickrGroup);
            if (definition != null)
            {
                var builder = new PageBuilder(Pages, mBlog, mPhotographs);
                var model = builder.Build(definition, pageNo);

                return MapToView(model);
            }

            return Http404();
        }

        public ActionResult PhotographsByTag(string tag, int pageNo = 0)
        {
            var definition = Pages.FirstOrDefault(x => x.PageType == PageType.FlickrGroup);
            if (definition != null)
            {
                if (!string.IsNullOrWhiteSpace(tag))
                {
                    var parentId = definition.Id;
                    var id = string.Concat(definition.Id, "|", tag);

                    definition = (PageDefinition)definition.Clone();
                    definition.Id = id;
                    definition.ParentId = parentId;
                    definition.PageType = PageType.FlickrGroupTag;
                    definition.AddPageSetting("tag", tag);
                }

                var builder = new PageBuilder(Pages, mBlog, mPhotographs);
                var model = builder.Build(definition, pageNo);

                return MapToView(model);
            }

            return Http404();
        }

        public ActionResult Photograph(string photoId, string secret, string tag = null)
        {
            var id = string.Concat(photoId, "/", secret);

            var group = Pages.FirstOrDefault(x => x.PageType == PageType.FlickrGroup);
            if (group != null)
            {
                var builder = new PageBuilder(Pages, mBlog, mPhotographs);
                var model = builder.Build(group, id);

                if (model != null)
                {
                    return MapToView(model);
                }
            }

            return Http404();
        }
        #endregion

        #region Albums
        public ActionResult Album(string album)
        {
            var definition = Pages.FirstOrDefault(x => String.Compare(x.Id, album, StringComparison.OrdinalIgnoreCase) == 0);

            var builder = new PageBuilder(Pages, mBlog, mPhotographs);
            var model = builder.Build(definition);

            return MapToView(model);
        }

        public ActionResult AlbumPage(string album, string page)
        {
            var home = Pages.FirstOrDefault(x => String.Compare(x.Id, album, StringComparison.OrdinalIgnoreCase) == 0);

            var pageId = string.Concat(album, "|", page);
            var definition = home.PageLinks.FirstOrDefault(x => String.Compare(x.Id, pageId, StringComparison.OrdinalIgnoreCase) == 0);

            var builder = new PageBuilder(Pages, mBlog, mPhotographs);
            var model = builder.Build(definition);

            return MapToView(model);
        }

        public ActionResult AlbumPhotograph(string album, string page, string photoId, string secret)
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

            return MapToView(model);
        }
        #endregion

        private IViewMapper GetViewMapper()
        {
            IViewMapper output = null;
            mRedirect = string.Empty;

            if (IsMobileRequest)
            {
                output = new MobileViewMapper();
            }
            else
            {
                output = new DesktopViewMapper();
            }

            output.Redirect += SetRedirect;
            return output;
        }

        private void SetRedirect(object sender, string redirectTo)
        {
            mRedirect = redirectTo;
        }

        private ActionResult MapToView(IPage model)
        {
            var mapper = GetViewMapper();
            string view = mapper.MapView(model);
            if (!string.IsNullOrWhiteSpace(mRedirect))
            {
                PageRedirect redirect = (PageRedirect)model;
                return Redirect(redirect.RedirectTo);
            }
            else
            {
                return View(view, model);
            }
        }

        #region Services
        public ActionResult ThumbnailsRecent(int count = 10, string baseUrl = null)
        {
            List<IPhotograph> photographs = mPhotographs.GetPhotographs(null).Take(count).ToList();

            List<IPhotograph> model = new List<IPhotograph>();
            foreach (IPhotograph photograph in photographs)
            {
                IPhotograph clone = (IPhotograph)photograph.Clone();
                clone.UriSource = Url.ThumbnailLink(clone, baseUrl);
                model.Add(clone);
            }

            return PartialView("Thumbnails", model);
        }

        public ActionResult ThumbnailsByTag(string tag, int count = 10, string baseUrl = null)
        {
            List<IPhotograph> photographs = mPhotographs.GetPhotographs(new string[] { tag }).Take(count).ToList();

            List<IPhotograph> model = new List<IPhotograph>();
            foreach (IPhotograph photograph in photographs)
            {
                IPhotograph clone = (IPhotograph)photograph.Clone();
                clone.UriSource = Url.ThumbnailLink(clone, baseUrl);
                model.Add(clone);
            }

            return PartialView("Thumbnails", model);
        }
        #endregion
    }
}