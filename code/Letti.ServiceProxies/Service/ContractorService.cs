using Letti.Model;
using Letti.ServiceProxies.Contracts;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Letti.ServiceProxies.Service
{
    public class ContractorService : BaseService, IContractorService
    {
        public ContractorService(HttpClient httpClient) : base(httpClient)
        {

        }

        public async Task<PersonCompanyRelationship> AddRelationship(PersonCompanyRelationship relationship)
        {
            var requestBody = JsonConvert.SerializeObject(relationship);
            HttpContent contentPost = new StringContent(requestBody, Encoding.UTF8, "application/json");

            var requestUri = "contractors/relationships";
            PersonCompanyRelationship responseData = new PersonCompanyRelationship();
            var method = "PATCH";
            var httpVerb = new HttpMethod(method);
            var httpRequestMessage =
                new HttpRequestMessage(httpVerb, requestUri)
                {
                    Content = contentPost
                };
            HttpResponseMessage response = await _httpClient.SendAsync(httpRequestMessage);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                responseData = JsonConvert.DeserializeObject<PersonCompanyRelationship>(json);
            }

            return responseData;
        }

        public async Task<Contractor> Create(Contractor contractor)
        {

            var requestBody = JsonConvert.SerializeObject(contractor);
            HttpContent contentPost = new StringContent(requestBody, Encoding.UTF8, "application/json");

            var requestUri = "contractors";
            Contractor responseData = new Contractor();
            HttpResponseMessage response = await _httpClient.PostAsync(requestUri, contentPost);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                responseData = JsonConvert.DeserializeObject<Contractor>(json);
            }

            return responseData;
        }

        public async Task<ICollection<Contractor>> GetAll()
        {
            var requestUri = "contractors";

            HttpResponseMessage response = await _httpClient.GetAsync(requestUri);
            ICollection<Contractor> responseData = new List<Contractor>();
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                responseData = JsonConvert.DeserializeObject<ICollection<Contractor>>(json);
            }

            return responseData;
        }

        public async Task<Contractor> GetById(int id)
        {
            var requestUri = $"contractors/{id}";
            Contractor responseData = new Contractor();
            HttpResponseMessage response = await _httpClient.GetAsync(requestUri);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                responseData = JsonConvert.DeserializeObject<Contractor>(json);
            }

            return responseData;
        }

        public async Task<ICollection<PersonCompanyRelationship>> GetContractorsByPoiId(int id)
        {
            var requestUri = $"contractors/poi/{id}";
            ICollection<PersonCompanyRelationship> responseData = new List<PersonCompanyRelationship>();
            HttpResponseMessage response = await _httpClient.GetAsync(requestUri);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                responseData = JsonConvert.DeserializeObject<ICollection<PersonCompanyRelationship>>(json);
            }

            return responseData;
        }

        public async Task<ICollection<PersonCompanyRelationship>> GetEmployeesByContractorId(int id)
        {
            var requestUri = $"contractors/employees/{id}";
            ICollection<PersonCompanyRelationship> responseData = new List<PersonCompanyRelationship>();
            HttpResponseMessage response = await _httpClient.GetAsync(requestUri);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                responseData = JsonConvert.DeserializeObject<ICollection<PersonCompanyRelationship>>(json);
            }

            return responseData;
        }

        public async Task<Contractor> Update(Contractor contractor)
        {
            var requestBody = JsonConvert.SerializeObject(contractor);
            HttpContent contentPost = new StringContent(requestBody, Encoding.UTF8, "application/json");

            var requestUri = "contractors";
            Contractor responseData = new Contractor();
            HttpResponseMessage response = await _httpClient.PutAsync(requestUri, contentPost);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                responseData = JsonConvert.DeserializeObject<Contractor>(json);
            }

            return responseData;
        }
    }
}
