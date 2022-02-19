using Letti.Model;
using Letti.ServiceProxies.Contracts;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Letti.Web.Components
{
    public class AddPublicEmployeeDialogBase : DialogBase
    {
        [Inject]
        public IPersonService PersonService { get; set; }
        [Inject]
        public IOrganizationService OrganizationService { get; set; }
        [Parameter]
        public int OrganizationId { get; set; }
        public string SelectedRelationshipId { get; set; } = "1";
        public string JobTitle { get; set; }
        public List<Entities.OrganizationPublicEmployeeType> OrganizationRelationships { get; set; }
        public PersonOfInterest Person { get; set; } = new PersonOfInterest();
        public AddPublicEmployeeDialogBase()
        {
            InitializeRelationships();
        }
        private void InitializeRelationships()
        {
            OrganizationRelationships = new List<Entities.OrganizationPublicEmployeeType>();
            OrganizationRelationships.Add(new Entities.OrganizationPublicEmployeeType { Id = 1, Name = "Director" });
            OrganizationRelationships.Add(new Entities.OrganizationPublicEmployeeType { Id = 2, Name = "Subdirector" });
            OrganizationRelationships.Add(new Entities.OrganizationPublicEmployeeType { Id = 3, Name = "Jefe de Área" });
        }
        public async override Task Show()
        {
            ResetDialog();
            ShowDialog = true;
            StateHasChanged();
        }

        protected override async Task HandleValidSubmit()
        {
            var response = await PersonService.Create(Person);
            if (response != null)
            {
                int relationshipType = int.Parse(SelectedRelationshipId);
                Model.PersonOrganizationRelationship personOrganizationRelationShip = new PersonOrganizationRelationship();
                personOrganizationRelationShip.OrganizationId = OrganizationId;
                personOrganizationRelationShip.PersonOfInterestId = response.Id;
                personOrganizationRelationShip.OrganizationRelationshipTypeId = relationshipType;
                personOrganizationRelationShip.Description = JobTitle;
                await OrganizationService.EditPersonOrganizationRelationShip(personOrganizationRelationShip);
            }
            ShowDialog = false;
            await CloseEventCallback.InvokeAsync(true);
            StateHasChanged();
        }
        protected override void ResetDialog()
        {

        }
    }
}
