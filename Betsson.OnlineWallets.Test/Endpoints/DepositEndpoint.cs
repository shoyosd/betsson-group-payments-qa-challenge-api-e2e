using Betsson.OnlineWallets.ApiTestsE2E.Endpoints;
using Betsson.OnlineWallets.Tests.Helpers;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Betsson.OnlineWallets.Tests.Endpoints
{
    public class DepositEndpoint : BaseEndpoint
    {
        private const string DepositEndpointUrl = "/onlinewallet/deposit";

        // Method to make a deposit
        public async Task DepositAsync(decimal amount)
        {
            var request = new RestRequest(DepositEndpointUrl, Method.Post);
            request.AddJsonBody(new { amount });
            await _client.ExecuteAsync(request);
        }
    }
}
