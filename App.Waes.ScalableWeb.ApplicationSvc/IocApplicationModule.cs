using App.Waes.ScalableWeb.Application.Service;
using Autofac;

namespace App.Waes.ScalableWeb.Application
{
    public class IocApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //var types = AppDomain
            //    .CurrentDomain
            //    .GetAssemblies()
            //    .SelectMany(n => n.GetTypes())
            //    .Where(n => n.FullName.StartsWith("App.Waes.ScalableWeb.Application"))
            //    .OrderBy(n => n.FullName)
            //    .Distinct()
            //    .ToList();

            //types.ForEach(n => { builder.RegisterType(n).AsImplementedInterfaces(); });
            builder.RegisterType<DocumentContentServiceApplication>().AsImplementedInterfaces();
            base.Load(builder);
        }
    }
}