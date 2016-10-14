using App.Waes.ScalableWeb.UnitTest;
using Autofac;
using NUnit.Framework;

namespace App.Waes.ScalableWeb.IntegrationTest.Tests
{
    [TestFixture]
    public class IntegrationTestBaseDefinitions
    {
        [SetUp]
        public void RunBeforeTest()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new IocIntegrationTestModule());
            Container = builder.Build();
        }


        [TearDown]
        public void RunAfterTest()
        {
            Container.Dispose();
        }

        public IContainer Container { get; set; }
    }
}