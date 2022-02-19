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
    public class PersonService:BaseService,IPersonService
    {
        public PersonService(HttpClient httpClient) : base(httpClient)
        {

        }

        public async Task<PersonOfInterest> Create(PersonOfInterest person)
        {
            var requestBody = JsonConvert.SerializeObject(person);
            HttpContent contentPost = new StringContent(requestBody, Encoding.UTF8, "application/json");

            var requestUri = "persons";
            PersonOfInterest responseData = new PersonOfInterest();
            HttpResponseMessage response = await _httpClient.PostAsync(requestUri, contentPost);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                responseData = JsonConvert.DeserializeObject<PersonOfInterest>(json);
            }

            return responseData;
        }

        public async Task<ICollection<PersonOfInterest>> GetAll()
        {
            var requestUri = "persons";

            HttpResponseMessage response = await _httpClient.GetAsync(requestUri);
            ICollection<PersonOfInterest> responseData = new List<PersonOfInterest>();
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                responseData = JsonConvert.DeserializeObject<ICollection<PersonOfInterest>>(json);
            }

            return responseData;
        }

        public async Task<PersonOfInterest> GetById(int id)
        {
            var requestUri = $"persons/{id}";
            PersonOfInterest responseData = new PersonOfInterest();
            HttpResponseMessage response = await _httpClient.GetAsync(requestUri);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                responseData = JsonConvert.DeserializeObject<PersonOfInterest>(json);
            }

            return responseData;
        }
        public async Task Delete(int id)
        {
            var requestUri = $"persons/{id}";
         
            HttpResponseMessage response = await _httpClient.DeleteAsync(requestUri);
            
            return ;
        }

        public async Task<ICollection<PersonalRelationship>> GetPersonalRelationShips(int id)
        {
            var requestUri = $"persons/relationships/{id}";
            ICollection<PersonalRelationship> responseData =null;
            HttpResponseMessage response = await _httpClient.GetAsync(requestUri);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                responseData = JsonConvert.DeserializeObject<ICollection<PersonalRelationship>>(json);
            }

            return responseData;
        }

        public async Task<PersonOfInterest> Update(PersonOfInterest person)
        {
            var requestBody = JsonConvert.SerializeObject(person);
            HttpContent contentPost = new StringContent(requestBody, Encoding.UTF8, "application/json");

            var requestUri = "persons";
            PersonOfInterest responseData = new PersonOfInterest();
            HttpResponseMessage response = await _httpClient.PutAsync(requestUri, contentPost);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                responseData = JsonConvert.DeserializeObject<PersonOfInterest>(json);
            }

            return responseData;
        }
        public async Task<PersonalRelationship> EditPersonalRelationShip(PersonalRelationship relationship)
        {
            var requestBody = JsonConvert.SerializeObject(relationship);
            HttpContent contentPost = new StringContent(requestBody, Encoding.UTF8, "application/json");

            var requestUri = "persons/personalrelationships";
            PersonalRelationship responseData = new PersonalRelationship();
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
                responseData = JsonConvert.DeserializeObject<PersonalRelationship>(json);
            }

            return responseData;
        }

        public async Task<ICollection<PersonOfInterest>> Search(string filter)
        {
            var requestUri = $"persons/search/{filter}";

            HttpResponseMessage response = await _httpClient.GetAsync(requestUri);
            ICollection<PersonOfInterest> responseData = new List<PersonOfInterest>();
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                responseData = JsonConvert.DeserializeObject<ICollection<PersonOfInterest>>(json);
            }

            return responseData;
        }

        public async Task<Property> AddProperty(Property property)
        {
            var requestBody = JsonConvert.SerializeObject(property);
            HttpContent contentPost = new StringContent(requestBody, Encoding.UTF8, "application/json");

            var requestUri = "persons/properties";
            Property responseData = new Property();
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
                responseData = JsonConvert.DeserializeObject<Property>(json);
            }

            return responseData;
        }

        public async Task<ICollection<Property>> GetProperties(int poiId)
        {
            var requestUri = $"persons/properties";

            HttpResponseMessage response = await _httpClient.GetAsync(requestUri);
            ICollection<Property> responseData = new List<Property>();
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                responseData = JsonConvert.DeserializeObject<ICollection<Property>>(json);
            }

            return responseData;
        }
    }
}
