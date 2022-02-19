using Letti.Model;
using Letti.ServiceProxies.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Letti.Web.Helpers
{
    public class PoiSearchHelper
    {
        private string lastSearchString;
        private bool searching;
        private List<PersonOfInterest> allData;
        private IPersonService personService;
        private List<PersonOfInterest> searchResults;
        public PoiSearchHelper(List<PersonOfInterest> allData,IPersonService personService)
        {
            searchResults = new List<PersonOfInterest>();
            this.allData = allData;
            this.personService = personService;
            searching = false;
        }
        public async Task<PoiSearchResponse> Search(string name)
        {
            PoiSearchResponse response = new PoiSearchResponse();
            
            
            response.ServiceCalled = false;
            if (searching)
            {
                return response;
            }
            if (!string.IsNullOrEmpty(lastSearchString) && name.Contains(lastSearchString))
            {
                response.People.AddRange(searchResults.Where(c => c.FullName.Contains(name)));
                return response;
            }
            searching = true;
            lastSearchString = name;
            searchResults.Clear();
            ICollection<PersonOfInterest> serviceSearchResults = await personService.GetAll();
            searchResults.AddRange(serviceSearchResults);
            response.People.AddRange(serviceSearchResults);
            response.ServiceCalled = true;
            searching = false;
           
            return response;
        }
        public async Task<PoiSearchResponse> SearchLocal(string name)
        {
            PoiSearchResponse response = new PoiSearchResponse();


            response.ServiceCalled = false;
            if (searching)
            {
                return response;
            }
            if (!string.IsNullOrEmpty(lastSearchString) && name.Contains(lastSearchString))
            {
                response.People.AddRange(searchResults.Where(c => c.FullName.ToLower().Contains(name)));
                return response;
            }
            searching = true;
            lastSearchString = name;
            searchResults.Clear();
            ICollection<PersonOfInterest> serviceSearchResults = allData.Where(c => c.FullName.ToLower().Contains(name)).ToList();
            searchResults.AddRange(serviceSearchResults);
            response.People.AddRange(serviceSearchResults);
            response.ServiceCalled = true;
            searching = false;

            return response;
        }
    }
}
