using Letti.Model;
using Letti.ServiceProxies.Contracts;
using Letti.Web.Entities;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;


namespace Letti.Web.Pages
{
    public class ContractorDetailBase:ComponentBase
    {
        private int id;
        [Parameter]
        public string ContractorId { get; set; }
        [Inject]
        public IContractorService ContractorService { get; set; }
        [Inject]
        public IContractsService ContractsService { get; set; }
        public List<Contract> Contracts { get; set; } = new List<Contract>();
        public List<ContractsByOrganizationView> ContractsViews { get; set; } = new List<ContractsByOrganizationView>();
        public ReportParameters ReportParameters { get; set; } = new ReportParameters();
        public Contractor Contractor { get; set; } = new Contractor();
        protected override async Task OnInitializedAsync()
        {
            ReportParameters.ContractsEndDate = DateTime.Now;
            ReportParameters.ContractsInitialDate = new DateTime(ReportParameters.ContractsEndDate.Year, 1, 1);
            id = int.Parse(ContractorId);
            Contractor dataContractor = await ContractorService.GetById(id);
            if (dataContractor != null)
            {
                Contractor = dataContractor;
            }
            ICollection<Contract> contracts = await ContractsService.GetContractsByContractorId(id);
            if (contracts != null && contracts.Any())
            {
                Contracts.AddRange(contracts);
                AnalyzeContracts();
            }
        }

        private void AnalyzeContracts()
        {
            ContractsViews.Clear();
            var contractsInPeriod = Contracts.Where(c => c.StartDate >= ReportParameters.ContractsInitialDate && c.StartDate <= ReportParameters.ContractsEndDate).ToList();
            var organizations = contractsInPeriod.Select(c => c.OrganizationId).Distinct();
            foreach (var organization in organizations)
            {
                ContractsByOrganizationView byOrganizationView = new ContractsByOrganizationView();
                byOrganizationView.OrganizationId = organization;
                byOrganizationView.Organization = contractsInPeriod.First(c => c.OrganizationId == organization).Organization.OrganizationName;              
                byOrganizationView.Contracts = contractsInPeriod.Where(c => c.OrganizationId == organization).Count();
                byOrganizationView.Value = contractsInPeriod.Where(c => c.OrganizationId == organization).Sum(c => c.Value);
                ContractsViews.Add(byOrganizationView);
            }
        }
    }
}
