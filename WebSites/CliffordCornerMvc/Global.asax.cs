using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Cliffy.Web;
using Cliffy.Web.CliffordCorner;
using Cliffy.Web.CliffordCorner.Controllers;
using Cliffy.Web.CliffordCorner.Installers;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace CliffordCornerMvc
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private static IWindsorContainer mContainer;

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "RecentThumbnailsService",
                "services/ThumbnailsRecent",
                new { controller = "Default", action="ThumbnailsRecent" }
                );

            routes.MapRoute(
                "ThumbnailsByTagService",
                "services/ThumbnailsByTag",
                new { controller = "Default", action = "ThumbnailsByTag" }
                );

            routes.MapRoute(
                "WebAPI",
                "services/{action}",
                new { controller = "Services" }
                );

            routes.MapRoute(
                "AfricaFix",
                "travellogs/africa/default.asp",
                new { controller = "Default", action = "Album", album = "africa" }
            );

            routes.MapRoute(
                "TravelLog", // Route name
                "TravelLog/{album}", // URL with parameters
                new { controller = "Default", action = "Album" } // Parameter defaults
            );

            routes.MapRoute(
                "TravelLogs", // Route name
                "TravelLogs/{album}", // URL with parameters
                new { controller = "Default", action = "Album" } // Parameter defaults
            );

            routes.MapRoute(
                "TravelLogPost", // Route name
                "TravelLog/{album}/post/{page}", // URL with parameters
                new { controller = "Default", action = "AlbumPage" } // Parameter defaults
            );

            routes.MapRoute(
                "TravelLogPosts", // Route name
                "TravelLog/{album}/posts", // URL with parameters
                new { controller = "Default", action = "AlbumPage", page = "posts" } // Parameter defaults
            );

            routes.MapRoute(
                "TravelLogUniquePosts", // Route name
                "TravelLog/{album}/posts/{page}", // URL with parameters
                new { controller = "Default", action = "AlbumPage" } // Parameter defaults
            );

            routes.MapRoute(
                "TravelLogNote", // Route name
                "TravelLog/{album}/notes/{page}", // URL with parameters
                new { controller = "Default", action = "AlbumPage" } // Parameter defaults
            );

            routes.MapRoute(
                "TravelLogPhotographs", // Route name
                "TravelLog/{album}/photographs/{page}", // URL with parameters
                new { controller = "Default", action = "AlbumPage" } // Parameter defaults
            );

            routes.MapRoute(
                "TravelLogLegacyPhotograph", // Route name
                "TravelLog/{album}/photograph/{photoId}/{secret}", // URL with parameters
                new { controller = "Default", action = "AlbumPhotograph" } // Parameter defaults
            );

            routes.MapRoute(
                "TravelLogPhotograph", // Route name
                "TravelLog/{album}/{page}/photograph/{photoId}/{secret}", // URL with parameters
                new { controller = "Default", action = "AlbumPhotograph" } // Parameter defaults
            );

            routes.MapRoute(
                "TravelLogVideo", // Route name
                "TravelLog/{album}/{page}/video/{photoId}/{secret}", // URL with parameters
                new { controller = "Default", action = "AlbumPhotograph" } // Parameter defaults
            );

            routes.MapRoute(
                "Photograph", // Route name
                "Photograph/{photoId}/{secret}/", // URL with parameters
                new { controller = "Default", action = "Photograph" } // Parameter defaults
            );

            routes.MapRoute(
                "Video", // Route name
                "Video/{photoId}/{secret}/", // URL with parameters
                new { controller = "Default", action = "Photograph" } // Parameter defaults
            );

            routes.MapRoute(
                "PhotographFromTaggedGroup", // Route name
                "Photographs/{tag}/photograph/{secret}/{photoId}", // URL with parameters
                new { controller = "Default", action = "Photograph" }
            );

            routes.MapRoute(
                "VideoFromTaggedGroup", // Route name
                "Photographs/{tag}/video/{secret}/{photoId}", // URL with parameters
                new { controller = "Default", action = "Photograph" }
            );

            routes.MapRoute(
                "PhotographsPaged", // Route name
                "Photographs/Page{pageNo}", // URL with parameters
                new { controller = "Default", action = "Photographs" }
            );

            routes.MapRoute(
                "Photographs", // Route name
                "Photographs/{tag}", // URL with parameters
                new { controller = "Default", action = "PhotographsByTag" }
            );

            routes.MapRoute(
                "PhotographsTagged", // Route name
                "Photographs/{tag}/page{pageNo}", // URL with parameters
                new { controller = "Default", action = "PhotographsByTag", tag = UrlParameter.Optional, pageNo = UrlParameter.Optional }
            );

            routes.MapRoute(
                "Administration", // Route name
                "admin/{action}", // URL with parameters
                new { controller = "Admin", action = "Index" } // Parameter defaults
            );

            routes.MapRoute(
                "ContentEngine", // Route name
                "{album}", // URL with parameters
                new { controller = "Default", action = "Album" } // Parameter defaults
            );

            routes.MapRoute(
                "ContentEnginePost", // Route name
                "{album}/post/{page}", // URL with parameters
                new { controller = "Default", action = "AlbumPage" } // Parameter defaults
            );

            routes.MapRoute(
                "ContentEnginePosts", // Route name
                "{album}/posts", // URL with parameters
                new { controller = "Default", action = "AlbumPage", page = "posts" } // Parameter defaults
            );

            routes.MapRoute(
                "ContentEngineUniquePosts", // Route name
                "{album}/posts/{page}", // URL with parameters
                new { controller = "Default", action = "AlbumPage" } // Parameter defaults
            );

            routes.MapRoute(
                "ContentEngineNote", // Route name
                "{album}/notes/{page}", // URL with parameters
                new { controller = "Default", action = "AlbumPage" } // Parameter defaults
            );

            routes.MapRoute(
                "ContentEnginePhotographs", // Route name
                "{album}/photographs/{page}", // URL with parameters
                new { controller = "Default", action = "AlbumPage" } // Parameter defaults
            );

            routes.MapRoute(
                "ContentEnginePhotograph", // Route name
                "{album}/{page}/photograph/{photoId}/{secret}", // URL with parameters
                new { controller = "Default", action = "AlbumPhotograph" } // Parameter defaults
            );

            routes.MapRoute(
                "ContentEngineVideo", // Route name
                "{album}/{page}/video/{id}/{secret}", // URL with parameters
                new { controller = "Default", action = "AlbumPhotograph" } // Parameter defaults
            );

            routes.MapRoute(
                "Default", // Route name
                "", // URL with parameters
                new { controller = "Default", action = "Index" } // Parameter defaults
            );

            routes.MapRoute(
                "NotFound",
                "{*url}",
                new { controller = "Default", action = "Http404" }
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);

            //ViewEngines.Engines.Add(new ViewEngine());
            BootstrapContainer();
        }

        protected void Application_End()
        {
            mContainer.Dispose();
        }

        /// <summary>
        /// This is the DI magic that allows us to use interfaces everywhere.
        /// </summary>
        private static void BootstrapContainer()
        {
            mContainer = new WindsorContainer().Install(FromAssembly.Containing<ControllersInstaller>());
            var controllerFactory = new WindsorControllerFactory(mContainer.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }
    }
}