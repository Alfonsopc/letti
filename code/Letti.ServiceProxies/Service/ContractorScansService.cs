using Letti.Model;
using Letti.ServiceProxies.Contracts;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Letti.ServiceProxies.Service
{
    public class ContractorScansService:BaseService, IContractorScansService
    {
        public ContractorScansService(HttpClient httpClient) : base(httpClient)
        {

        }

        public async Task<Contractor> GetNext()
        {
            var requestUri = "contractorscans/next";

            HttpResponseMessage response = await _httpClient.GetAsync(requestUri);
            Contractor responseData = new Contractor();
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                responseData = JsonConvert.DeserializeObject<Contractor>(json);
            }

            return responseData;
        }

        public async Task MarkAsScanned(int contractorId)
        {
          
            HttpContent contentPost = new StringContent(string.Empty, Encoding.UTF8, "application/json");

            var requestUri = $"contractorscans/{contractorId}";
           
            HttpResponseMessage response = await _httpClient.PutAsync(requestUri, contentPost);
            if (response.IsSuccessStatusCode)
            {
                return;
            }

            return ;
        }
    }
}
