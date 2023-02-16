using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;
using System.Web.Http;
using WP.Business.Classes;
using WP.Business.Classes.Misc;
using WP.Business.Interfaces;
using WP.Business.Interfaces.Misc;
using WP.Repository.Classes;
using WP.Repository.Classes.Misc;
using WP.Repository.Interfaces;
using WP.Repository.Interfaces.Misc;

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
            builder.RegisterType<PagesBusiness>().As<IPagesBusiness>();
            builder.RegisterType<PagesRepository>().As<IPagesRepository>();
            builder.RegisterType<PostsBusiness>().As<IPostsBusiness>();
            builder.RegisterType<PostsRepository>().As<IPostsRepository>();
            builder.RegisterType<MiscBusiness>().As<IMiscBusiness>();
            builder.RegisterType<MiscRepository>().As<IMiscRepository>();
            var container = builder.Build();
            var resoolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resoolver;
        }
    }
}