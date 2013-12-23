using System.Collections.Generic;
using System.Configuration;
using System;
using System.Web.Mvc;
using Cliffy.Common.Caching;
using Cliffy.Data;
using Cliffy.Web.CliffordCorner.ActionFilters;

namespace Cliffy.Web.CliffordCorner.Controllers
{
    [FlickrCacheWiring]
    public abstract class BaseController : Controller
    {
        protected ICache mCache;
        protected IBlogRepository mBlog;
        protected IPhotographRepository mPhotographs;
        public bool IsMobileRequest { get; set; }

        public List<PageDefinition> Pages { get; set; }

        public BaseController(IBlogRepository blog, IPhotographRepository photographs, ICache cache)
        {
            mCache = cache;
            mBlog = blog;
            mPhotographs = photographs;

            //var config = new ConfigReader(mCache);
            //Pages = config.Read();
            Pages = (List<PageDefinition>)ConfigurationManager.GetSection("content");
        }
    }
}