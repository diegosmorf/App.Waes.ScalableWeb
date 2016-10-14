using System.Linq;
using App.Waes.ScalableWeb.Application.Contract;
using App.Waes.ScalableWeb.Core.Repository;
using App.Waes.ScalableWeb.Domain.Model;
using Autofac;
using NUnit.Framework;

namespace App.Waes.ScalableWeb.UnitTest.Tests
{
    [TestFixture]
    public class DocumentContentTest : UnitTestBaseDefinitions
    {
        [Test]
        public void Execute_LeftContent_Then_LeftContentPersisted()
        {
            // Arrange
            const int expectedNumberOfEntities = 1;
            const int id = 1;
            const string content = "Chuck Norris";

            var serviceApp = Container.Resolve<IDocumentContentServiceApplication>();
            var repository = Container.Resolve<IRepository<DocumentContent>>();
            var cryptoHandler = Container.Resolve<ICryptographyHandler>();

            // Act
            serviceApp.Left(id, cryptoHandler.Encode(content));
            var documentContent = repository.All().Single();

            // Assert
            Assert.AreEqual(expectedNumberOfEntities, repository.All().Count());
            Assert.AreEqual(id, documentContent.Id);
            Assert.AreEqual(content, documentContent.Left);
        }

        [Test]
        public void Execute_RightContent_Then_RightContentPersisted()
        {
            // Arrange
            const int expectedNumberOfEntities = 1;
            const int id = 1;
            const string content = "Sylvester Stallone";

            var serviceApp = Container.Resolve<IDocumentContentServiceApplication>();
            var repository = Container.Resolve<IRepository<DocumentContent>>();
            var cryptoHandler = Container.Resolve<ICryptographyHandler>();

            // Act
            serviceApp.Right(id, cryptoHandler.Encode(content));
            var documentContent = repository.All().Single();

            // Assert
            Assert.AreEqual(expectedNumberOfEntities, repository.All().Count());
            Assert.AreEqual(id, documentContent.Id);
            Assert.AreEqual(content, documentContent.Right);
        }


        [Test]
        public void Execute_RightLeftWithDifferentContent_Then_ResultIsNotEqual()
        {
            // Arrange
            const int expectedNumberOfEntities = 1;
            const int expectedSize = 0;
            const int id = 1;
            const string contentLeft = "Chuck Norris";
            const string contentRight = "123 Chuck 123 Norris 123";

            var serviceApp = Container.Resolve<IDocumentContentServiceApplication>();
            var repository = Container.Resolve<IRepository<DocumentContent>>();
            var cryptoHandler = Container.Resolve<ICryptographyHandler>();

            // Act
            serviceApp.Left(id, cryptoHandler.Encode(contentLeft));
            serviceApp.Right(id, cryptoHandler.Encode(contentRight));
            var result = serviceApp.Diff(id);

            var documentContent = repository.All().Single();

            // Assert
            Assert.AreEqual(expectedNumberOfEntities, repository.All().Count());
            Assert.AreEqual(id, documentContent.Id);
            Assert.AreEqual(eDocumentComparisonSize.IsNotEqual, result.ComparisonSize);
            Assert.AreEqual(expectedSize, result.Size);
        }


        [Test]
        public void Execute_RightLeftWithDifferentContentSameSize_Then_ResultIsNotEqual()
        {
            // Arrange
            const int expectedNumberOfEntities = 1;
            const int expectedSize = 12;
            const int id = 1;
            const int expectedDiffsCount = 1;
            const int expectedStartPosition = 6;
            const int expectedDiffSize = 2;
            const string contentLeft = "Chuck Norris";
            const string contentRight = "Chuck__orris";

            var serviceApp = Container.Resolve<IDocumentContentServiceApplication>();
            var repository = Container.Resolve<IRepository<DocumentContent>>();
            var cryptoHandler = Container.Resolve<ICryptographyHandler>();

            // Act
            serviceApp.Left(id, cryptoHandler.Encode(contentLeft));
            serviceApp.Right(id, cryptoHandler.Encode(contentRight));
            var result = serviceApp.Diff(id);

            var documentContent = repository.All().Single();

            // Assert
            Assert.AreEqual(expectedNumberOfEntities, repository.All().Count());
            Assert.AreEqual(id, documentContent.Id);
            Assert.AreEqual(eDocumentComparisonSize.IsNotEqual, result.ComparisonSize);
            Assert.AreEqual(expectedSize, result.Size);
            Assert.AreEqual(expectedDiffsCount, result.Diffs.Count);
            Assert.AreEqual(expectedStartPosition, result.Diffs[0].StartPosition);
            Assert.AreEqual(expectedDiffSize, result.Diffs[0].Size);
        }

        [Test]
        public void Execute_RightLeftWithSameContent_Then_ResultIsEqual()
        {
            // Arrange
            const int expectedNumberOfEntities = 1;
            const int expectedSize = 12;
            const int id = 1;
            const string contentLeft = "Chuck Norris";
            const string contentRight = "Chuck Norris";

            var serviceApp = Container.Resolve<IDocumentContentServiceApplication>();
            var repository = Container.Resolve<IRepository<DocumentContent>>();
            var cryptoHandler = Container.Resolve<ICryptographyHandler>();

            // Act
            serviceApp.Left(id, cryptoHandler.Encode(contentLeft));
            serviceApp.Right(id, cryptoHandler.Encode(contentRight));
            var result = serviceApp.Diff(id);

            var documentContent = repository.All().Single();

            // Assert
            Assert.AreEqual(expectedNumberOfEntities, repository.All().Count());
            Assert.AreEqual(id, documentContent.Id);
            Assert.AreEqual(eDocumentComparisonSize.IsEqual, result.ComparisonSize);
            Assert.AreEqual(expectedSize, result.Size);
        }

        [Test]
        public void Execute_RightLeftWithTwoDifferentContentSameSize_Then_ResultIsNotEqual()
        {
            // Arrange
            const int expectedNumberOfEntities = 1;
            const int expectedSize = 12;
            const int id = 1;
            const int expectedDiffsCount = 3;
            const int expectedStartPosition = 2;
            const int expectedDiffSize = 2;
            const string contentLeft = "C##ck Norr$$";
            const string contentRight = "Chuck__orris";

            var serviceApp = Container.Resolve<IDocumentContentServiceApplication>();
            var repository = Container.Resolve<IRepository<DocumentContent>>();
            var cryptoHandler = Container.Resolve<ICryptographyHandler>();

            // Act
            serviceApp.Left(id, cryptoHandler.Encode(contentLeft));
            serviceApp.Right(id, cryptoHandler.Encode(contentRight));
            var result = serviceApp.Diff(id);

            var documentContent = repository.All().Single();

            // Assert
            Assert.AreEqual(expectedNumberOfEntities, repository.All().Count());
            Assert.AreEqual(id, documentContent.Id);
            Assert.AreEqual(eDocumentComparisonSize.IsNotEqual, result.ComparisonSize);
            Assert.AreEqual(expectedSize, result.Size);
            Assert.AreEqual(expectedDiffsCount, result.Diffs.Count);
            Assert.AreEqual(expectedStartPosition, result.Diffs[0].StartPosition);
            Assert.AreEqual(expectedDiffSize, result.Diffs[0].Size);
            Assert.AreEqual(expectedDiffSize, result.Diffs[1].Size);
            Assert.AreEqual(expectedDiffSize, result.Diffs[2].Size);
        }
    }
}