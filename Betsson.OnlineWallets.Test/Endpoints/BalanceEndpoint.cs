using Betsson.OnlineWallets.Tests.Helpers;
using RestSharp;
using System.Threading.Tasks;
using Betsson.OnlineWallets.Tests.Models;
using Betsson.OnlineWallets.Tests.Endpoints;
using Betsson.OnlineWallets.ApiTestsE2E.Endpoints;

namespace Betsson.OnlineWallets.Tests.Endpoints
{
    public class BalanceEndpoint : BaseEndpoint
    {
        private const string BalanceEndpointUrl = "/onlinewallet/balance";

        // Method to get the balance
        public async Task<RestResponse<BalanceResponse>> GetBalanceAsync()
        {
            var request = new RestRequest(BalanceEndpointUrl, Method.Get);
            var response = await _client.ExecuteAsync<BalanceResponse>(request);
            return response;
        }

        // Method to get only the balance amount
        public async Task<decimal> GetBalanceAmountAsync()
        {
            var response = await GetBalanceAsync();
            return response.Data.Amount;
        }
    }
}
