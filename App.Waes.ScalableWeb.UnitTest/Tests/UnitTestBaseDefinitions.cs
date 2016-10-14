using Autofac;
using NUnit.Framework;

namespace App.Waes.ScalableWeb.UnitTest.Tests
{
    [TestFixture]
    public class UnitTestBaseDefinitions
    {
        [SetUp]
        public void RunBeforeTest()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new IocUnitTestModule());
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