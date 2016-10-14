using System;
using System.Diagnostics;
using System.Reflection;
using System.Web.Http;
using App.Waes.ScalableWeb.Application;
using App.Waes.ScalableWeb.Infrastructure;
using App.Waes.ScalableWeb.WebApi.Server.Middleware;
using Autofac;
using Autofac.Integration.WebApi;
using Owin;

namespace App.Waes.ScalableWeb.WebApi.Server.Start
{
    public class WebApiStartup : IDisposable
    {
        private IContainer container;

        public void Dispose()
        {
            container?.Dispose();
        }

        public void Start(string url, IAppBuilder app)
        {
            Trace.TraceInformation($"Internet Server: Starting - URL: {url}");
            Configuration(app);
            Trace.TraceInformation("Internet Server: Running.");
        }

        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterModule(new IocInfrastructureModule());
            builder.RegisterModule(new IocApplicationModule());
            builder.RegisterType<LogMiddleware>();
            container = builder.Build();
            app.UseAutofacMiddleware(container);

            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            app.UseWebApi(config);
        }
    }
}