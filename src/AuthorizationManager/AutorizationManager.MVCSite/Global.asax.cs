using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AuthorizationManager.Core.FromThinktectureIdentityServer.EntityFramework.DbContexts;
using AuthorizationManager.Domain.Services;
using AuthorizationManager.Infraestructure.ServicesEF;
using Autofac;
using Autofac.Integration.Mvc;

namespace AutorizationManager.MVCSite
{
    public class AutorizationManagerMvcSite : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();

            // Register your MVC controllers.
            builder.RegisterControllers(typeof(AutorizationManagerMvcSite).Assembly);

            // OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

            // Register types
            builder.RegisterType<ClientConfigurationDbContext>().AsSelf().WithParameter("connectionString", "IdSvr3Config").InstancePerRequest();
            builder.RegisterType<ClientService>().As<IClientService>().InstancePerRequest();


            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));


            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
