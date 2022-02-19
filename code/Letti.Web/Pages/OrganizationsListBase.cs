using Letti.Model;
using Letti.ServiceProxies.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Letti.Web.Entities;

namespace Letti.Web.Pages
{
    public class OrganizationsListBase:ComponentBase
    {
        [Inject]
        public IOrganizationService OrganizationService { get; set; }
        public List<Organization> Organizations { get; set; }
        public string SearchText { get; set; }
        public List<Organization> DataOrganizations { get; set; }
        public MetaData MetaData { get; set; } = new MetaData();
        private EntityParameters organizationParameters = new EntityParameters();
        private bool isSearching;
        protected override async Task OnInitializedAsync()
        {
            ICollection<Organization> organizations = await OrganizationService.GetAll();
            Organizations = new List<Organization>();
            DataOrganizations = new List<Organization>(organizations);
            isSearching = false;
            GetOrganizations();
        }
        protected async Task KeyPressed(KeyboardEventArgs args)
        {
            if (SearchText.Length > 3)
            {
                Organizations.Clear();
                organizationParameters.PageNumber = 1;
                isSearching = true;
                GetFilteredOrganizations();
            }
            if (SearchText.Length == 0)
            {
                isSearching = false;
                GetOrganizations();
            }

        }
        protected async Task SelectedPage(int page)
        {
            organizationParameters.PageNumber = page;
            if(isSearching)
            {
                GetFilteredOrganizations();
            }
            else
            {
                GetOrganizations();
            }
           
        }
        private void GetOrganizations()
        {
            Organizations.Clear();
            var pagingResponse = GetOrganizations(organizationParameters);
            MetaData = pagingResponse.MetaData;
            Organizations.AddRange(pagingResponse);
        }
        private void GetFilteredOrganizations()
        {
            var pagingResponse = GetFilteredOrganizations(organizationParameters);
            MetaData = pagingResponse.MetaData;
            Organizations.AddRange(pagingResponse);
        }
        protected PagedList<Organization> GetOrganizations(EntityParameters organizationParameters)
        {
            return PagedList<Organization>.ToPagedList(DataOrganizations, organizationParameters.PageNumber, organizationParameters.PageSize);
        }
        protected PagedList<Organization> GetFilteredOrganizations(EntityParameters organizationParameters)
        {
            string filter = SearchText.ToLower();
            var filtered = DataOrganizations.Where(c => c.OrganizationName.ToLower().Contains(filter));
            return PagedList<Organization>.ToPagedList(filtered, organizationParameters.PageNumber, organizationParameters.PageSize);
        }
    }
}
