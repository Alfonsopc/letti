using Letti.Model;
using Letti.ServiceProxies.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Letti.ServiceProxies.Service
{
    public class PersonScansService : BaseService, IPersonScansService
    {
        public PersonScansService(HttpClient httpClient) : base(httpClient)
        {

        }
        public async Task<PersonOfInterest> GetNext()
        {
            var requestUri = "personscans/next";

            HttpResponseMessage response = await _httpClient.GetAsync(requestUri);
            PersonOfInterest responseData = new PersonOfInterest();
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                responseData = JsonConvert.DeserializeObject<PersonOfInterest>(json);
            }

            return responseData;
        }

        public async Task MarkAsScanned(int poiId)
        {
            HttpContent contentPost = new StringContent(string.Empty, Encoding.UTF8, "application/json");

            var requestUri = $"personscans/{poiId}";

            HttpResponseMessage response = await _httpClient.PutAsync(requestUri, contentPost);
            if (response.IsSuccessStatusCode)
            {
                return;
            }

            return;
        }
    }
}
