using Betsson.OnlineWallets.Tests.Helpers;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Betsson.OnlineWallets.Tests.Endpoints
{
    public class WithdrawEndpoint
    {
        private readonly RestClient _client;

        public WithdrawEndpoint()
        {
            _client = ApiClient.GetClient();
        }

        // Método para hacer un retiro
        public async Task WithdrawAsync(decimal amount)
        {
            var request = new RestRequest("/onlinewallet/withdraw", Method.Post);
            request.AddJsonBody(new { amount });
            await _client.PostAsync(request);
        }
    }
}
