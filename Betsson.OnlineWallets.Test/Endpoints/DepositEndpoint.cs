using Betsson.OnlineWallets.Tests.Helpers;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Betsson.OnlineWallets.Tests.Endpoints
{
    public class DepositEndpoint
    {
        private readonly RestClient _client;

        public DepositEndpoint()
        {
            _client = ApiClient.GetClient();
        }

        // Método para hacer un depósito
        public async Task DepositAsync(decimal amount)
        {
            var request = new RestRequest("/onlinewallet/deposit", Method.Post);
            request.AddJsonBody(new { amount });
            await _client.PostAsync(request);
        }
    }
}
