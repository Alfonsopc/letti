using Letti.Model;
using Letti.ServiceProxies.Contracts;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Letti.Web.Components;

namespace Letti.Web.Pages
{
    public class PersonDetailBase:ComponentBase
    {
        private int id;
        [Parameter]
        public string PersonId { get; set; }
        [Inject]
        public IPersonService PersonService { get; set; }
        [Inject] 
        public IContractorService ContractorService { get; set; }
        [Inject]
        public IOrganizationService OrganizationService { get; set; }
        public PersonOfInterest Person { get; set; } = new PersonOfInterest();
        protected AddRelationshipDialog AddRelationshipDialog { get; set; }
        protected DeletePersonalRelationship DeletePersonalRelationshipDialog { get; set; }
        protected AddPublicJobDialog AddPublicJobDialog { get; set; }
        public List<Entities.PersonalRelationship> PersonalRelationships { get; set; } = new List<Entities.PersonalRelationship>();
        public List<PersonCompanyRelationship> ContractorRelationships { get; set; } = new List<PersonCompanyRelationship>();

        public List<PersonOrganizationRelationship> OrganizationRelationships { get; set; } = new List<PersonOrganizationRelationship>();
        protected override async Task OnInitializedAsync()
        {
            id = int.Parse(PersonId);
            PersonOfInterest dataContractor = await PersonService.GetById(id);
            if (dataContractor != null)
            {
                Person = dataContractor;
            }
            var relationships = await PersonService.GetPersonalRelationShips(id);
            if(relationships!=null && relationships.Any())
            {
               MapPersonalRelationships(relationships);
            }
            var contractorRelationships = await ContractorService.GetContractorsByPoiId(id);
            if(contractorRelationships!=null && contractorRelationships.Any())
            {
                ContractorRelationships.AddRange(contractorRelationships);
            }
            var organizationRelationships = await OrganizationService.GetOrganizationsByPoiId(id);
            if(organizationRelationships!=null && organizationRelationships.Any())
            {
                OrganizationRelationships.AddRange(organizationRelationships);
            }
        }

        private void MapPersonalRelationships(ICollection<PersonalRelationship> personalRelationShips)
        {
            foreach(var relationship in personalRelationShips)
            {
                PersonalRelationships.Add(new Entities.PersonalRelationship(relationship));
            }
            
        }
        protected async void QuickAddRelationship()
        {
            await AddRelationshipDialog.Show();
        }
        protected async void QuickEditRelationship(Web.Entities.PersonalRelationship relationship)
        {
            AddRelationshipDialog.SecondaryPersonId = relationship.DisplayedPersonId;
            AddRelationshipDialog.EditRelationship = relationship.Relationship;
            AddRelationshipDialog.OriginalRelatinshiptId = relationship.Id;
            await AddRelationshipDialog.Show();
        }
        protected async void QuickAddPublicJob()
        {
            await AddPublicJobDialog.Show();
        }
        protected async Task DeleteRelationship(Web.Entities.PersonalRelationship relationship)
        {
            DeletePersonalRelationshipDialog.SecondaryPersonId = relationship.DisplayedPersonId;
            DeletePersonalRelationshipDialog.EditRelationship = relationship.Relationship;
            DeletePersonalRelationshipDialog.OriginalRelatinshiptId = relationship.Id;
            await DeletePersonalRelationshipDialog.Show();
        }
        public async Task AddRelationshipDialog_OnDialogClose()
        {
            PersonalRelationships.Clear();
            var relationships = await PersonService.GetPersonalRelationShips(id);
            if (relationships != null && relationships.Any())
            {
                MapPersonalRelationships(relationships);
            }
            StateHasChanged();
        }
        public async Task AddPublicJogDialog_OnDialogClose()
        {
            OrganizationRelationships.Clear();
            var organizationRelationships = await OrganizationService.GetOrganizationsByPoiId(id);
            if (organizationRelationships != null && organizationRelationships.Any())
            {
                OrganizationRelationships.AddRange(organizationRelationships);
            }
            StateHasChanged();
        }
    }
}
