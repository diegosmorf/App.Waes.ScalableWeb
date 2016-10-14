using App.Waes.ScalableWeb.Application.Contract;
using Autofac;
using NUnit.Framework;

namespace App.Waes.ScalableWeb.UnitTest.Tests
{
    public class CryptoHandlerTest : UnitTestBaseDefinitions
    {
        [Test]
        public void Execute_Encode_Then_ValidateEncoded()
        {
            // Arrange
            const string content = "Chuck Norris";
            const string expectedEncodedContent = "Q2h1Y2sgTm9ycmlz";

            var handler = Container.Resolve<ICryptographyHandler>();

            // Act
            var encodedContent = handler.Encode(content);

            // Assert
            Assert.AreEqual(expectedEncodedContent, encodedContent);
        }

        [Test]
        public void Execute_EncodeAndDecode_Then_PlainTextKeepSame()
        {
            // Arrange
            const string content = "Chuck Norris";

            var handler = Container.Resolve<ICryptographyHandler>();

            // Act
            var encodedContent = handler.Encode(content);
            var decodedContent = handler.Decode(encodedContent);


            // Assert
            Assert.AreEqual(content, decodedContent);
        }

        [Test]
        public void Execute_EncodeAndDecodeJson_Then_PlainTextKeepSame()
        {
            // Arrange
            const string content = @"{""menu"": {
                                          ""id"": ""file"",
                                          ""value"": ""File"",
                                          ""popup"": {
                                                        ""menuitem"": [
                                                          {""value"": ""New"", ""onclick"": ""CreateNewDoc()""},
                                              {""value"": ""Open"", ""onclick"": ""OpenDoc()""},
                                              {""value"": ""Close"", ""onclick"": ""CloseDoc()""}
                                            ]
                                          }
                                        }}";

            var handler = Container.Resolve<ICryptographyHandler>();

            // Act
            var encodedContent = handler.Encode(content);
            var decodedContent = handler.Decode(encodedContent);


            // Assert
            Assert.AreEqual(content, decodedContent);
        }

        [Test]
        public void Execute_EncodeAndDecodeUTF8_Then_PlainTextKeepSame()
        {
            // Arrange
            const string content = "'!@#$%¨&*()´´``{}[]~~^^çÇ?/:;>.<,º";

            var handler = Container.Resolve<ICryptographyHandler>();

            // Act
            var encodedContent = handler.Encode(content);
            var decodedContent = handler.Decode(encodedContent);


            // Assert
            Assert.AreEqual(content, decodedContent);
        }
    }
}