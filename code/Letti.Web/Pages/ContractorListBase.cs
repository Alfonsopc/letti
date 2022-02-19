using Letti.Model;
using Letti.ServiceProxies.Contracts;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Components.Web;
using Letti.Web.Entities;

namespace Letti.Web.Pages
{
    public class ContractorListBase:ComponentBase
    {
        [Inject]
        public IContractorService ContractorService { get; set; }
        public string SearchText { get; set; }
        public List<Contractor> Contractors { get; set; }
        public List<Contractor> DataContractors { get; set; }
        public MetaData MetaData { get; set; } = new MetaData();
        private EntityParameters contractorParameters = new EntityParameters();
        private bool isSearching;
        protected override async Task OnInitializedAsync()
        {
            ICollection<Contractor> contractors = await ContractorService.GetAll();           
            DataContractors = new List<Contractor>(contractors.OrderBy(c=>c.OfficialName));
            Contractors = new List<Contractor>();
            isSearching = false;
            GetContractors();
           
        }
        protected async Task KeyPressed(KeyboardEventArgs args)
        {
            if (SearchText.Length > 3)
            {
                isSearching = true;
                Contractors.Clear();
                contractorParameters.PageNumber = 1;
                GetFilteredContractors();
              
            }
            if(SearchText.Length==0)
            {
                isSearching = false;
                GetContractors();
            }

        }

        protected async Task SelectedPage(int page)
        {
            contractorParameters.PageNumber = page;
            if(isSearching)
            {
                GetFilteredContractors();
            }
            else
            {
                GetContractors();
            }
            
        }
        private void GetContractors()
        {
            Contractors.Clear();
            var pagingResponse = GetContractors(contractorParameters);
            MetaData = pagingResponse.MetaData;
            Contractors.AddRange(pagingResponse);
        }
        private void GetFilteredContractors()
        {
            var pagingResponse = GetFilteredContractors(contractorParameters);
            MetaData = pagingResponse.MetaData;
            Contractors.AddRange(pagingResponse);
        }
        protected PagedList<Contractor> GetContractors(EntityParameters contractorParameters)
        {
            return PagedList<Contractor>.ToPagedList(DataContractors, contractorParameters.PageNumber, contractorParameters.PageSize);
        }
        protected PagedList<Contractor> GetFilteredContractors(EntityParameters contractorParameters)
        {
            string filter = SearchText.ToLower();
            var filtered = DataContractors.Where(c => c.OfficialName.ToLower().Contains(filter));
            return PagedList<Contractor>.ToPagedList(filtered, contractorParameters.PageNumber, contractorParameters.PageSize);
        }
    }
}
