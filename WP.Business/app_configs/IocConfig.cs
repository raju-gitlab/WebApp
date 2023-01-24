
using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Dependencies;
using WP.Business.Classes;
using WP.Business.Interfaces;
using WP.Repository.Classes;
using WP.Repository.Interfaces;

namespace Business.WebApp.API.DependencyResolve
{
    public class IocConfigz
    {
        public IDependencyResolver SolveDependency()
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<AuthBusiness>().As<IAuthBusiness>();
            builder.RegisterType<AuthRepository>().As<IAuthRepository>();
            var container = builder.Build();
            return new AutofacWebApiDependencyResolver(container);
        }
    }
}
