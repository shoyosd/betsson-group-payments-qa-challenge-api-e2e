using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Betsson.OnlineWallets.Tests.Helpers
{
    public static class ApiClient
    {
        private static readonly string BaseUrl = "http://localhost:8080";
        private static RestClient _client;

        public static RestClient GetClient()
        {
            if (_client == null)
            {
                _client = new RestClient(BaseUrl);
            }
            return _client;
        }
    }
}
