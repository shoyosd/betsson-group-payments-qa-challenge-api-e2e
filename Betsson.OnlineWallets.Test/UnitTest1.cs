using NUnit.Framework;
using RestSharp;
using System.Net;

namespace Betsson.OnlineWallets.Tests
{
    [TestFixture]
    public class BalanceTests
    {
        private RestClient _client;

        [SetUp]
        public void Setup()
        {
            // Inicializamos el cliente antes de cada prueba
            _client = new RestClient("http://localhost:8080/onlinewallet/");
        }

        [TearDown]
        public void TearDown()
        {
            // Liberamos los recursos después de cada prueba
            _client.Dispose();
        }

        [Test]
        public async Task GetBalance_ShouldReturnOk()
        {
            var request = new RestRequest("balance", Method.Get);
            var response = await _client.ExecuteAsync(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            //Assert.IsNotNull(response.Content);
        }
    }
}
