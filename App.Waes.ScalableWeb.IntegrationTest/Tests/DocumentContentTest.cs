using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using App.Waes.ScalableWeb.Application.Contract;
using App.Waes.ScalableWeb.Domain.Model;
using App.Waes.ScalableWeb.WebApi.Server.Start;
using Autofac;
using Microsoft.Owin.Testing;
using NUnit.Framework;

namespace App.Waes.ScalableWeb.IntegrationTest.Tests
{
    [TestFixture]
    public class DocumentContentTest : IntegrationTestBaseDefinitions
    {
        [Test]
        public async Task Execute_RightLeftWithTwoDifferentContentSameSize_Then_ResultIsNotEqual()
        {
            // Arrange
            const int expectedSize = 12;
            const int id = 1;
            const int expectedDiffsCount = 1;
            const int expectedStartPosition = 6;
            const int expectedDiffSize = 2;
            const string contentLeft = "ChuckAAorris";
            const string contentRight = "Chuck__orris";
            const string url = @"http://localhost:9001/v1/diff/";
            DocumentComparisonResult result;

            // Act
            var cryptoHandler = Container.Resolve<ICryptographyHandler>();

            using (var server = TestServer.Create<WebApiStartup>())
            {
                using (var client = new HttpClient(server.Handler))
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.BaseAddress = new Uri(url);
                    await client.PostAsJsonAsync($"{id}/right", cryptoHandler.Encode(contentRight));
                    await client.PostAsJsonAsync($"{id}/left", cryptoHandler.Encode(contentLeft));
                    var response = await client.GetAsync($"{id}");
                    result = await response.Content.ReadAsAsync<DocumentComparisonResult>();
                }
            }


            // Assert
            Assert.AreEqual(eDocumentComparisonSize.IsNotEqual, result.ComparisonSize);
            Assert.AreEqual(expectedSize, result.Size);
            Assert.AreEqual(expectedDiffsCount, result.Diffs.Count);
            Assert.AreEqual(expectedStartPosition, result.Diffs[0].StartPosition);
            Assert.AreEqual(expectedDiffSize, result.Diffs[0].Size);

            await Task.FromResult(0);
        }
    }
}