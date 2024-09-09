using Betsson.OnlineWallets.ApiTestsE2E.Endpoints;
using Betsson.OnlineWallets.ApiTestsE2E.TestData;
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
        // Method to withdraw money
        public async Task<RestResponse> WithdrawAsync(decimal amount)
        {
            var request = new RestRequest(ApiTestData.WithdrawEndpointUrl, Method.Post);
            request.AddJsonBody(new { amount });
            var response = await _client.ExecuteAsync(request);
            return response;  // Ensure the method returns the response
        }
    }
}
