using Betsson.OnlineWallets.Tests.Helpers;
using RestSharp;
using System.Threading.Tasks;
using Betsson.OnlineWallets.Tests.Models;
using Betsson.OnlineWallets.Tests.Endpoints;

namespace Betsson.OnlineWallets.Tests.Endpoints
{
    public class BalanceEndpoint
    {
        private readonly RestClient _client;

        public BalanceEndpoint()
        {
            _client = ApiClient.GetClient();
        }

        // Método para obtener el balance
        public async Task<BalanceResponse> GetBalanceAsync()
        {
            var request = new RestRequest("/onlinewallet/balance", Method.Get);
            var response = await _client.GetAsync<BalanceResponse>(request);
            return response;
        }
    }
}
