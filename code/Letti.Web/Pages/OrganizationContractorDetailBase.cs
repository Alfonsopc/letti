using Letti.Model;
using Letti.ServiceProxies.Contracts;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Letti.Web.Entities;
using Letti.Web.Components;

namespace Letti.Web.Pages
{
    public class OrganizationContractorDetailBase : ComponentBase
    {
        private int organizationId;
        private int contractorId;
        [Parameter]
        public string OrganizationId { get; set; }
        [Inject]
        public IContractsService ContractsService { get; set; }
        [Inject]
        public IOrganizationService OrganizationService { get; set; }
        [Inject]
        public IContractorService ContractorService { get; set; }

        public List<Contract> Contracts { get; set; } = new List<Contract>();
        protected ContractView ContractView { get; set; }
        protected override async Task OnInitializedAsync()
        {
            string[] parameters = OrganizationId.Split("_");
            organizationId = int.Parse(parameters[0]);
            contractorId = int.Parse(parameters[1]);
            var contracts = await ContractsService.GetContractsByOrganizationId(organizationId);
            if (contracts != null && contracts.Any())
            {
                Contracts.AddRange(contracts.Where(c => c.ContractorId == contractorId));
            }
           


        }
        protected async void ViewContract(Contract contract)
        {
            ContractView.Contract = contract;
            await ContractView.Show();
        }
        public async Task OnDialogClose()
        {

        }
    }
}
