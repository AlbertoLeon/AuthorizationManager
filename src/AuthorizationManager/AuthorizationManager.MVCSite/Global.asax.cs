using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AuthorizationManager.Core.FromThinktectureIdentityServer.EntityFramework.DbContexts;
using AuthorizationManager.Domain.Services;
using AuthorizationManager.Infraestructure.ServicesEF;
using AuthorizationManager.MVCSite;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using AuthorizationManager.MVCSite.App_Start;

namespace AuthorizationManager.MVCSite
{
    public class AuthorizationManagerMvcSite : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();
            // AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            


            // Register your MVC controllers.
            builder.RegisterControllers(typeof(AuthorizationManagerMvcSite).Assembly);

            builder.RegisterApiControllers(typeof (AuthorizationManagerMvcSite).Assembly);

            // OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();

            //// OPTIONAL: Register web abstractions like HttpContextBase.
            //builder.RegisterModule<AutofacWebTypesModule>();

            //// OPTIONAL: Enable property injection in view pages.
            //builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

            // Register types
            builder.RegisterType<ClientConfigurationDbContext>().AsSelf().WithParameter("connectionString", "IdSvr3Config").InstancePerRequest();
            builder.RegisterType<ClientService>().As<IClientService>().InstancePerRequest();

            builder.RegisterType<ScopeConfigurationDbContext>().AsSelf().WithParameter("connectionString", "IdSvr3Config").InstancePerRequest();
            builder.RegisterType<ScopeService>().As<IScopeService>().InstancePerRequest();

            
            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            
            
            

            
        }
    }
}
