﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Cliffy.Common.Caching;
using Cliffy.Web;
using Cliffy.Web.Caching;
using Cliffy.Web.Blogger;
using Cliffy.Web.FlickrWrapper;
using Cliffy.Web.OpenId;
using Cliffy.Web.MongoWrapper;

namespace Cliffy.Web.CliffordCorner.Installers
{
    public class RepositoryInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container
                .Register(
                    Component.For<ICache>()
                             .ImplementedBy<WebCache>()
                             .LifestylePerWebRequest())
                .Register(
                    Component.For<IPhotographRepository>()
                             .ImplementedBy<MongoPhotographRepository>()
                             .LifestylePerWebRequest())
                .Register(
                    Component.For<IBlogRepository>()
                             .ImplementedBy<BloggerRepository>()
                             .LifestylePerWebRequest())
                .Register(
                    Component.For<IAccountRepository>()
                             .ImplementedBy<OpenIdAccountsFromXmlFile>()
                             .LifestylePerWebRequest());
        }
    }
}