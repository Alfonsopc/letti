using Letti.Model;
using Letti.ServiceProxies.Contracts;
using Letti.Web.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;


namespace Letti.Web.Pages
{
    public class PersonListBase : ComponentBase
    {
        [Inject]
        public IPersonService PersonService { get; set; }
        public List<PersonOfInterest> AllPersons { get; set; }
        public List<PersonOfInterest> Persons { get; set; }
        private PoiSearchHelper searchHelper;
        
        public string SearchText { get; set; }
       
        protected override async Task OnInitializedAsync()
        {
            ICollection<PersonOfInterest> persons = await PersonService.GetAll();
            Persons = new List<PersonOfInterest>();
            AllPersons = new List<PersonOfInterest>(persons);
            searchHelper = new PoiSearchHelper(AllPersons, PersonService);
        }
       

        protected async Task KeyPressed(KeyboardEventArgs args)
        {
            if (SearchText.Length > 3)
            {
                PoiSearchResponse response = await searchHelper.SearchLocal(SearchText.ToLower());
                Persons.Clear();
                Persons.AddRange(response.People);
            }
          
        }
    }
}
