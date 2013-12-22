using Cliffy.Common.Caching;
using Cliffy.Web.CliffordCorner.ActionFilters;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.RelyingParty;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;

namespace Cliffy.Web.CliffordCorner.Controllers
{
    public class AdminController : BaseController
    {
        private IAccountRepository mUsers;

        public AdminController(IBlogRepository blog, IPhotographRepository photographs, ICache cache, IAccountRepository users) : base(blog, photographs, cache) 
        {
            mUsers = users;
        }

        [AdminAuthCheck]
        public ActionResult Index()
        {
            PageAdminCache model = new PageAdminCache();
            model.CachedItems = LoadCache();
            LoadModel(model, "cache");

            return View("cache", model);
        }

        public ActionResult Login()
        {
            PageAdmin model = new PageAdmin();
            LoadModel(model, string.Empty);
            return View(model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect(FormsAuthentication.DefaultUrl);
        }

        [AdminAuthCheck]
        public ActionResult Cache()
        {
            PageAdminCache model = new PageAdminCache();
            model.CachedItems = LoadCache();
            LoadModel(model, "cache");

            return View(model);
        }

        [AdminAuthCheck]
        public ActionResult CacheRemove(string key)
        {
            key = Server.UrlDecode(key);

            if (String.Compare(key, "Flickr", StringComparison.OrdinalIgnoreCase) == 0 &&
                mPhotographs is FlickrWrapper.FlickrRepository)
            {
                FlickrNet.Cache.FlushCache();
            }
            else
            {
                mCache.RemoveFromCache(key);
            }

            return Redirect("/admin/cache");
        }

        [AdminAuthCheck]
        public ActionResult CacheClear()
        {
            mCache.ClearCache();
            return Redirect("/admin/cache");
        }

        private void LoadModel(PageBase model, string option)
        {
            Response.Expires = 0;
            Response.Cache.SetNoStore();
            Response.AppendHeader("Pragma", "no-cache");

            model.Navigation = BuildNavigation();
        }

        private Dictionary<NavigationType, PageLinks> BuildNavigation()
        {
            var output = new Dictionary<NavigationType, PageLinks>();

            var builder = new PageBuilder(Pages, mBlog, mPhotographs);
            output.Add(NavigationType.Global, builder.GlobalNavigation());
            output.Add(NavigationType.Sub, new PageLinks());

            return output;
        }

        private List<string> LoadCache()
        {
            var output = new List<string>();

            if (mPhotographs is FlickrWrapper.FlickrRepository)
            {
                output.Add("Flickr");
            }

            var keys = mCache.Keys;
            foreach (var key in keys)
            {
                if (key.IndexOf("ViewCacheEntry", StringComparison.OrdinalIgnoreCase) < 0)
                {
                    output.Add(key);
                }
            }

            return output;
        }

        [ValidateInput(false)]
        public ActionResult Authenticate(string returnUrl)
        {
            PageAdmin model = new PageAdmin();
            LoadModel(model, string.Empty);

            var openId = new OpenIdRelyingParty();
            var response = openId.GetResponse();
            if (response == null)
            {
                string account = Request.Form["openid_identifier"];

                if (!mUsers.HasAccess(account))
                {
                    ViewData["Message"] = string.Format("{0} does not have admin rights on this site", account);
                    return View("Login", model);
                }
                else
                {
                    // Stage 2: user submitting Identifier
                    Identifier id;
                    if (Identifier.TryParse(account, out id))
                    {
                        try
                        {
                            return openId.CreateRequest(account).RedirectingResponse.AsActionResult();
                        }
                        catch (ProtocolException ex)
                        {
                            ViewData["Message"] = ex.Message;
                            return View("Login", model);
                        }
                    }
                    else
                    {
                        ViewData["Message"] = "Invalid identifier";
                        return View("Login", model);
                    }
                }
            }
            else
            {
                // Stage 3: OpenID Provider sending assertion response
                switch (response.Status)
                {
                    case AuthenticationStatus.Authenticated:
                        Session["FriendlyIdentifier"] = response.FriendlyIdentifierForDisplay;
                        FormsAuthentication.SetAuthCookie(response.ClaimedIdentifier, false);
                        if (!string.IsNullOrEmpty(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Admin");
                        }

                    case AuthenticationStatus.Canceled:
                        ViewData["Message"] = "Canceled at provider";
                        return View("Login", model);

                    case AuthenticationStatus.Failed:
                        ViewData["Message"] = response.Exception.Message;
                        return View("Login", model);
                }
            }

            return View(model);
        }
    }
}