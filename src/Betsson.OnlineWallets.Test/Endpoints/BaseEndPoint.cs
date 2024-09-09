using Betsson.OnlineWallets.Tests.Helpers;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Betsson.OnlineWallets.ApiTestsE2E.Endpoints
{
    public abstract class BaseEndpoint
    {
        protected readonly RestClient _client;

        public BaseEndpoint()
        {
            _client = ApiClient.GetClient();
        }
    }
}
