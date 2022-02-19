using Letti.Model;
using Letti.ServiceProxies.Contracts;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Letti.Web.Entities;
using Microsoft.AspNetCore.Components.Web;
using System;
using Letti.Web.Components;

namespace Letti.Web.Pages
{
    public class OrganizationDetailBase:ComponentBase
    {
        private int id;
        [Parameter]
        public string OrganizationId { get; set; }
        [Inject]
        public IOrganizationService OrganizationService { get; set; }
        [Inject]
        public IContractsService ContractsService { get; set; }
       
        public Organization Organization { get; set; } = new Organization();
        public List<Contract> Contracts { get; set; } = new List<Contract>();
        public List<ContractsByContractorView> ContractsViews { get; set; } = new List<ContractsByContractorView>();
        
        protected  AddPublicEmployeeModal AddPublicEmployeeDialog { get; set; }
        public List<PersonOrganizationRelationship> OrganizationRelationships { get; set; } = new List<PersonOrganizationRelationship>();

        public ReportParameters ReportParameters { get; set; } = new ReportParameters();
        public List<Model.CaseToReview> Cases { get; set; } = new List<Model.CaseToReview>();
        protected override async Task OnInitializedAsync()
        {
            ReportParameters.ContractsEndDate = DateTime.Now;
            ReportParameters.ContractsInitialDate = new DateTime(ReportParameters.ContractsEndDate.Year, 1, 1);
           
            id = int.Parse(OrganizationId);
            Organization dataOganization = await OrganizationService.GetById(id);
            if(dataOganization!=null)
            {
                Organization = dataOganization;
            }
            var organizationRelationships = await OrganizationService.GetEmployeesByOrganizationId(id);
            if(organizationRelationships!=null && organizationRelationships.Any())
            {
                OrganizationRelationships.AddRange(organizationRelationships);
            }
            ICollection<Contract> contracts = await ContractsService.GetContractsByOrganizationId(id);
            if(contracts!=null && contracts.Any())
            {
                Contracts.AddRange(contracts);
                AnalyzeContracts();
            }
            //SetupDummyCases();
        }

        private void AnalyzeContracts()
        {
            ContractsViews.Clear();
            var contractsInPeriod = Contracts.Where(c => c.StartDate >= ReportParameters.ContractsInitialDate && c.StartDate <= ReportParameters.ContractsEndDate).ToList();
            var contractors = contractsInPeriod.Select(c => c.ContractorId).Distinct();
            List<ContractsByContractorView> tempContractsView = new List<ContractsByContractorView>();
            foreach(var contractor in contractors)
            {
                ContractsByContractorView byContractorView = new ContractsByContractorView();
                byContractorView.ContractorId = contractor;
                byContractorView.Contractor = contractsInPeriod.First(c => c.ContractorId == contractor).Contractor.OfficialName;
                byContractorView.TaxId = contractsInPeriod.First(c => c.ContractorId == contractor).Contractor.TaxId;
                byContractorView.Contracts = contractsInPeriod.Where(c => c.ContractorId == contractor).Count();
                byContractorView.Value = contractsInPeriod.Where(c => c.ContractorId == contractor).Sum(c => c.Value);
                tempContractsView.Add(byContractorView);
            }
            ContractsViews.AddRange(tempContractsView.OrderByDescending(c => c.Value));
        }

        protected async Task PeriodChanged()
        {
            AnalyzeContracts();

        }
        protected async void QuickAddRelationship()
        {
            await AddPublicEmployeeDialog.Show();
        }
        public async Task AddRelationshipDialog_OnDialogClose()
        {
            OrganizationRelationships.Clear();
            var organizationRelationships = await OrganizationService.GetEmployeesByOrganizationId(id);
            if (organizationRelationships != null && organizationRelationships.Any())
            {
                OrganizationRelationships.AddRange(organizationRelationships);
            }
            StateHasChanged();
        }
        /*
        private void SetupDummyCases()
        {
            CaseToReview case1 = new CaseToReview();
            case1.Id = 30;
            case1.CaseType = Model.Enums.CaseType.RecentCreation;
            case1.Organization = new Organization() { OrganizationName = "Secretaría de Desarrollo Económico" };
            case1.Contractor = new Contractor() { ComercialName = "Constructora Reyes", FoundedOn = DateTime.Today.AddDays(-30), TaxId = "RER9089-876",FiscalAddress= "Villa de las Duraznos 60, Villa fontana" };
            case1.CaseDate = DateTime.Today.AddDays(-7);
            Cases.Add(case1);

            CaseToReview case2 = new CaseToReview();
            case2.CaseType = Model.Enums.CaseType.Address;
            case2.Id = 45;
            case2.OrganizationId = 1;
            case2.Organization = new Organization() { OrganizationName = "Secretaría de Desarrollo Económico" };
            case2.Contractor = new Contractor() { ComercialName = "Constructora LOPEZ", FoundedOn = DateTime.Today.AddDays(-30), TaxId = "RER9089-TY7",FiscalAddress= "Villa de las flores 50, Villa fontana" };
            case2.CaseDate = DateTime.Today.AddDays(-7);
            case2.Contractors = new List<Contractor>();
            case2.Contractors.Add(new Contractor() { ComercialName = "Consultora Contable", FoundedOn = DateTime.Today.AddDays(-30), TaxId = "RER9089-HO9",FiscalAddress= "Villa de las flores 50, Villa fontana" });
            Cases.Add(case2);

            CaseToReview case3 = new CaseToReview();
            case3.Id = 54;
            case3.CaseType = Model.Enums.CaseType.Conflict;
            case3.Organization = new Organization() { OrganizationName = "Secretaría de Desarrollo Económico" };
            case3.Contractor = new Contractor() { ComercialName = "Constructora Reyes", FoundedOn = DateTime.Today.AddDays(-30), TaxId = "RER9089-876", FiscalAddress = "Villa de las Duraznos 60, Villa fontana" };
            case3.CaseDate = DateTime.Today.AddDays(-7);
            case3.SuspiciousRelationships = new List<SuspiciousRelationship>();
            SuspiciousRelationship relationship = new SuspiciousRelationship();
            relationship.OrganizationPoi = new PersonOfInterest() { FirstName = "Alfonso", LastName = "Paredes", SecondLastName = "Cervantes" };
            relationship.Job = "Director TI";
            relationship.Relationship = "Tío";
            relationship.ContractorPoi = new PersonOfInterest() { FirstName = "Erik", LastName = "Cervantes", SecondLastName = "Gómez" };
            relationship.Position = "Accionista";
            case3.SuspiciousRelationships.Add(relationship);
            Cases.Add(case3);
        }*/
    }
}
