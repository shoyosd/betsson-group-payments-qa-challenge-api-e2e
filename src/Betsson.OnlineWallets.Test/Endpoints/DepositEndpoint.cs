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
    public class DepositEndpoint : BaseEndpoint
    {
        public async Task<RestResponse> DepositAsync(decimal amount)
        {
            var request = new RestRequest(ApiTestData.DepositEndpointUrl, Method.Post);
            request.AddJsonBody(new { amount });
            var response = await _client.ExecuteAsync(request);
            return response;
        }
    }
}
