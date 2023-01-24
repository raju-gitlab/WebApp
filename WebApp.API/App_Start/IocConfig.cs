
using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;
using System.Web.Http;
using WP.Business.Classes;
using WP.Business.Interfaces;
using WP.Repository.Classes;
using WP.Repository.Interfaces;

namespace WebApp.API
{
    public class IocConfig
    {
        public static void SolveDependency()
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<AuthBusiness>().As<IAuthBusiness>();
            builder.RegisterType<AuthRepository>().As<IAuthRepository>();
            var container = builder.Build();
            var resoolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resoolver;
        }
    }
}
