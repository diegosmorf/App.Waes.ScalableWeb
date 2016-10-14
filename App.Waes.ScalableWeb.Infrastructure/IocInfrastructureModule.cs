using App.Waes.ScalableWeb.Infrastructure.Service;
using Autofac;

namespace App.Waes.ScalableWeb.Infrastructure
{
    /// <summary>
    ///     Registers types for this application and performs infrastructure configuration.
    /// </summary>
    public class IocInfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(RepositoryInMemory<>)).AsImplementedInterfaces().SingleInstance();
            builder.RegisterGeneric(typeof(PersistenceServiceInMemory<>)).AsImplementedInterfaces().SingleInstance();
            builder.RegisterGeneric(typeof(QueryServiceInMemory<>)).AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<CryptographyHandler>().AsImplementedInterfaces();
            builder.RegisterType<BinaryDocumentComparisonStrategy>().AsImplementedInterfaces();
            base.Load(builder);
        }
    }
}