using NUnit.Framework;
using RestSharp;
using System.Net;

namespace Betsson.OnlineWallets.ApiTests
{
    [TestFixture]
    public class BalanceTests
    {
        private RestClient _client;

        [SetUp]
        public void Setup()
        {
            _client = new RestClient("http://localhost:8080/onlinewallet/balance");
        }

        [Test]
        public async Task GetBalance_ShouldReturnOk()
        {
            // Arrange
            var request = new RestRequest("balance", Method.Get);

            // Act
            var response = await _client.ExecuteAsync(request);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.IsNotNull(response.Content);
        }
    }
}
