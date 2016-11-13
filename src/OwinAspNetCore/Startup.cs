using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Integration.Owin;
using Autofac.Integration.WebApi;
using EFDAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using Owin;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using System.Web.OData.Query;

namespace OwinAspNetCore
{
    public class Startup
    {
        public IContainer AutofacContainer { get; private set; }
        public IConfigurationRoot Configuration { get; }


        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddScoped<GreenBoxEntities>(_ => new GreenBoxEntities(Configuration.GetConnectionString("DefaultConnection")));


            ContainerBuilder autofacContainerBuilder = new ContainerBuilder();
            autofacContainerBuilder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            autofacContainerBuilder.RegisterType<SomeDependency>().As<ISomeDependency>();
            autofacContainerBuilder.Populate(services);
            AutofacContainer = autofacContainerBuilder.Build();

            return new AutofacServiceProvider(AutofacContainer);
        }

        public void Configure(IApplicationBuilder aspNetCoreApp, IApplicationLifetime appLifetime)
        {
            //aspNetCoreApp.UseMvc();

            aspNetCoreApp.UseOwinApp(owinApp =>
            {
                owinApp.Use<ExtendAspNetCoreAutofacLifetimeToOwinMiddleware>();
                // Use ExtendAspNetCoreAutofacLifetimeToOwinMiddleware instead of owinApp.UseAutofacMiddleware(AutofacContainer); because that middleware will create autofac lifetime scope from scratch,
                // But our middleware will get lifetime scope from asp net core http context object, and after that, signalr & asp.net web api odata and other owin middlewares can use that lifetime scope.

                // owinApp.UseWebApi(); asp.net web api / odata / web hooks

                HttpConfiguration webApiConfig = new HttpConfiguration();

                webApiConfig.DependencyResolver = new AutofacWebApiDependencyResolver(AutofacContainer);

                ODataModelBuilder odataMetadataBuilder = new ODataConventionModelBuilder();
                
                odataMetadataBuilder.Register<dvd>(name: "Dvds", key: a => a.id);
                odataMetadataBuilder.Register<dvd_partaker>(name: "Dvd_partakers", key: a => a.id);
                odataMetadataBuilder.Register<aspect>(name: "Aspects", key: a => a.code);
                odataMetadataBuilder.Register<capacity>(name: "Capacities", key: a => a.code);
                odataMetadataBuilder.Register<studio>(name: "Studios", key: a => a.id);
                odataMetadataBuilder.Register<partaker>(name: "Partakers", key: a => a.id);
                odataMetadataBuilder.Register<genre>(name: "Genres", key: a => a.code);
                odataMetadataBuilder.Register<rating>(name: "Ratings", key: a => a.code);
                odataMetadataBuilder.Register<status>(name: "Statuses", key: a => a.code);

                webApiConfig.MapODataServiceRoute(
                    routeName: "odata",
                    routePrefix: "odata",
                    model: odataMetadataBuilder.GetEdmModel());

                owinApp.UseAutofacWebApi(webApiConfig);
                owinApp.UseWebApi(webApiConfig);

                //owinApp.MapSignalR();

                owinApp.Use<SampleOwinMiddleware>();
            });

            aspNetCoreApp.UseMiddleware<SampleAspNetCoreMiddleware>();

            appLifetime.ApplicationStopped.Register(() => AutofacContainer.Dispose());
        }
    }

    public class ExtendAspNetCoreAutofacLifetimeToOwinMiddleware : OwinMiddleware
    {
        public ExtendAspNetCoreAutofacLifetimeToOwinMiddleware(OwinMiddleware next)
            : base(next)
        {

        }

        static ExtendAspNetCoreAutofacLifetimeToOwinMiddleware()
        {
            Type autofacConstantsType = typeof(OwinContextExtensions).Assembly.GetType("Autofac.Integration.Owin.Constants");

            FieldInfo owinLifetimeScopeKeyField = autofacConstantsType.GetField("OwinLifetimeScopeKey", BindingFlags.Static | BindingFlags.NonPublic);

            owinLifetimeScopeKey = (string)owinLifetimeScopeKeyField.GetValue(null);
        }

        private static readonly string owinLifetimeScopeKey;

        public async override Task Invoke(IOwinContext context)
        {
            // You've access to asp.net core http context in your owin middlewares, asp.net web api odata controllers, signalr hubs, etc.

            HttpContext aspNetCoreContext = (HttpContext)context.Environment["Microsoft.AspNetCore.Http.HttpContext"];

            // do what ever you want using context.Request & context.Response

            ILifetimeScope autofacScope = aspNetCoreContext.RequestServices.GetService<ILifetimeScope>();

            context.Set(owinLifetimeScopeKey, autofacScope);

            await Next.Invoke(context);
        }
    }

    public class SampleOwinMiddleware : OwinMiddleware
    {
        public SampleOwinMiddleware(OwinMiddleware next)
            : base(next)
        {

        }

        public async override Task Invoke(IOwinContext context)
        {
            // You've access to asp.net core http context in your owin middlewares, asp.net web api odata controllers, signalr hubs, etc.

            HttpContext aspNetCoreContext = (HttpContext)context.Environment["Microsoft.AspNetCore.Http.HttpContext"];

            // do what ever you want using context.Request & context.Response

            await Next.Invoke(context);
        }
    }

    public class SampleAspNetCoreMiddleware
    {
        private readonly RequestDelegate Next;

        public SampleAspNetCoreMiddleware(RequestDelegate next)
        {
            Next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // do what ever you want using context.Request & context.Response
            await Next.Invoke(context);
        }
    }
}
