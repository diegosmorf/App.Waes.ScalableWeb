using App.Waes.ScalableWeb.Application;
using App.Waes.ScalableWeb.Infrastructure;
using Autofac;

namespace App.Waes.ScalableWeb.UnitTest
{
    public class IocIntegrationTestModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new IocApplicationModule());
            builder.RegisterModule(new IocInfrastructureModule());

            base.Load(builder);
        }
    }
}