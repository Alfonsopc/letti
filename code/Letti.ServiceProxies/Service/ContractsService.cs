using Letti.Model;
using Letti.ServiceProxies.Contracts;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Letti.ServiceProxies.Service
{
    public class ContractsService : BaseService, IContractsService
    {
        public ContractsService(HttpClient httpClient) : base(httpClient)
        {

        }
        public async Task<ICollection<Contract>> GetContractsByContractorId(int contractorId)
        {
            var requestUri = $"contracts/contractor/{contractorId}";
            ICollection<Contract> responseData = new List<Contract>();
            HttpResponseMessage response = await _httpClient.GetAsync(requestUri);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                responseData = JsonConvert.DeserializeObject<ICollection<Contract>>(json);
            }

            return responseData;
        }

        public async Task<ICollection<Contract>> GetContractsByOrganizationId(int organizationId)
        {
            var requestUri = $"contracts/organization/{organizationId}";
            ICollection<Contract> responseData = new List<Contract>();
            HttpResponseMessage response = await _httpClient.GetAsync(requestUri);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                responseData = JsonConvert.DeserializeObject<ICollection<Contract>>(json);
            }

            return responseData;
        }
        public async Task<Contract> Create(Contract contract)
        {
            var requestBody = JsonConvert.SerializeObject(contract);
            HttpContent contentPost = new StringContent(requestBody, Encoding.UTF8, "application/json");

            var requestUri = "contracts";
            Contract responseData = new Contract();
            HttpResponseMessage response = await _httpClient.PostAsync(requestUri, contentPost);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                responseData = JsonConvert.DeserializeObject<Contract>(json);
            }

            return responseData;
        }
    }
}
