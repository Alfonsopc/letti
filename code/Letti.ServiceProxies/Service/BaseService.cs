using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Letti.ServiceProxies.Service
{
    public class BaseService
    {
        protected readonly HttpClient _httpClient;

        public HttpClient HttpClient
        {
            get
            {
                return _httpClient;
            }
        }
        public BaseService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
    }
}
