using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Cliffy.Web.CliffordCorner.Installers
{
    /// <summary>
    /// Provides Castle Windsor with the location of our controllers, so that it may create and inject dependencies into them.
    /// </summary>
    public class ControllersInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromAssemblyNamed("Cliffy.Web")
                                      .BasedOn<IController>()
                                      .LifestyleTransient());
        }
    }
}
