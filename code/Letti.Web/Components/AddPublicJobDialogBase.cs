using Letti.Model;
using Letti.ServiceProxies.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Letti.Web.Components
{
    public class AddPublicJobDialogBase:DialogBase
    {
        [Inject]
        public IPersonService PersonService { get; set; }
        [Inject]
        public IOrganizationService OrganizationService { get; set; }
        [Parameter]
        public int MainPersonId { get; set; }
        public Model.PersonOrganizationRelationship PersonOrganizationRelationShip { get; set; } = new PersonOrganizationRelationship();
        public string SelectedOrganizationId { get; set; }
        public List<Organization> Organizations { get; set; } = new List<Organization>();
        public string SelectedRelationshipId { get; set; } = "1";
        public List<Entities.OrganizationPublicEmployeeType> OrganizationRelationships { get; set; }

        public override async Task Show()
        {
            ResetDialog();
            ShowDialog = true;
           
            var organizations = await OrganizationService.GetAll();
            if(organizations!=null && organizations.Any())
            {
                Organizations.AddRange(organizations);
                SelectedOrganizationId = Organizations.First().Id.ToString();
            }
            StateHasChanged();
        }

        protected override async Task HandleValidSubmit()
        {
          
            PersonOrganizationRelationShip.OrganizationId = int.Parse(SelectedOrganizationId);
            PersonOrganizationRelationShip.PersonOfInterestId = MainPersonId;
            PersonOrganizationRelationShip.OrganizationRelationshipTypeId = int.Parse(SelectedRelationshipId);
            await OrganizationService.EditPersonOrganizationRelationShip(PersonOrganizationRelationShip);
            ShowDialog = false;
            await CloseEventCallback.InvokeAsync(true);
            StateHasChanged();
        }

        protected override void ResetDialog()
        {
            Organizations.Clear();
            InitializeRelationships();
        }
        private void InitializeRelationships()
        {
            OrganizationRelationships = new List<Entities.OrganizationPublicEmployeeType>();
            OrganizationRelationships.Add(new Entities.OrganizationPublicEmployeeType { Id = 1, Name = "Director" });
            OrganizationRelationships.Add(new Entities.OrganizationPublicEmployeeType { Id = 2, Name = "Subdirector" });
            OrganizationRelationships.Add(new Entities.OrganizationPublicEmployeeType { Id = 3, Name = "Jefe de Área" });
        }
    }
}
