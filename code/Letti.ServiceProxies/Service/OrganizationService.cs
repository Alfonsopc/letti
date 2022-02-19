using Letti.Model;
using Letti.ServiceProxies.Contracts;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Letti.ServiceProxies.Service
{
    public class OrganizationService : BaseService, IOrganizationService
    {
        public OrganizationService(HttpClient httpClient) : base(httpClient)
        {

        }

        public async Task<Organization> Create(Organization organization)
        {
            var requestBody = JsonConvert.SerializeObject(organization);
            HttpContent contentPost = new StringContent(requestBody, Encoding.UTF8, "application/json");

            var requestUri = "organizations";
            Organization responseData = new Organization();
            HttpResponseMessage response = await _httpClient.PostAsync(requestUri, contentPost);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                responseData = JsonConvert.DeserializeObject<Organization>(json);
            }

            return responseData;
        }

        public async Task<ICollection<Organization>> GetAll()
        {
            var requestUri = "organizations";
            
            HttpResponseMessage response = await _httpClient.GetAsync(requestUri);
            ICollection<Organization> responseData = new List<Organization>();
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                responseData = JsonConvert.DeserializeObject<ICollection<Organization>>(json);
            }

            return responseData;
  
        }

        public async Task<Organization> GetById(int id)
        {
            var requestUri = $"organizations/{id}";
            Organization responseData = new Organization();
            HttpResponseMessage response = await _httpClient.GetAsync(requestUri);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                responseData = JsonConvert.DeserializeObject<Organization>(json);
            }

            return responseData;
        }

        public async Task<ICollection<PersonOrganizationRelationship>> GetOrganizationsByPoiId(int id)
        {
            var requestUri = $"organizations/poi/{id}";
            ICollection<PersonOrganizationRelationship> responseData = new List<PersonOrganizationRelationship>();
            HttpResponseMessage response = await _httpClient.GetAsync(requestUri);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                responseData = JsonConvert.DeserializeObject<ICollection<PersonOrganizationRelationship>>(json);
            }

            return responseData;
        }
        public async Task<ICollection<PersonOrganizationRelationship>> GetEmployeesByOrganizationId(int id)
        {
            var requestUri = $"organizations/employees/{id}";
            ICollection<PersonOrganizationRelationship> responseData = new List<PersonOrganizationRelationship>();
            HttpResponseMessage response = await _httpClient.GetAsync(requestUri);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                responseData = JsonConvert.DeserializeObject<ICollection<PersonOrganizationRelationship>>(json);
            }

            return responseData;
        }

        public async Task<Organization> Update(Organization organization)
        {
            var requestBody = JsonConvert.SerializeObject(organization);
            HttpContent contentPost = new StringContent(requestBody, Encoding.UTF8, "application/json");

            var requestUri = "organizations";
            Organization responseData = new Organization();
            HttpResponseMessage response = await _httpClient.PutAsync(requestUri, contentPost);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                responseData = JsonConvert.DeserializeObject<Organization>(json);
            }

            return responseData;
        }

        public async Task<PersonOrganizationRelationship> EditPersonOrganizationRelationShip(PersonOrganizationRelationship relationship)
        {
            var requestBody = JsonConvert.SerializeObject(relationship);
            HttpContent contentPost = new StringContent(requestBody, Encoding.UTF8, "application/json");

            var requestUri = "organizations/personnelrelationships";
            PersonOrganizationRelationship responseData = new PersonOrganizationRelationship();
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
                responseData = JsonConvert.DeserializeObject<PersonOrganizationRelationship>(json);
            }

            return responseData;
        }
    }
}
