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
    public class WithdrawEndpoint : BaseEndpoint
    {
        private const string WithdrawEndpointUrl = "/onlinewallet/withdraw";

        // Method to withdraw money
        public async Task WithdrawAsync(decimal amount)
        {
            var request = new RestRequest(WithdrawEndpointUrl, Method.Post);
            request.AddJsonBody(new { amount });
            await _client.ExecuteAsync(request);
        }
    }
}
