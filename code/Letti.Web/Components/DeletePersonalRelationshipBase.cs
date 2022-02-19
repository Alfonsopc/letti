using Letti.Model;
using Letti.ServiceProxies.Contracts;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Letti.Web.Components
{
    public class DeletePersonalRelationshipBase: DialogBase
    {
        [Inject]
        public IPersonService PersonService { get; set; }
        [Parameter]
        public int MainPersonId { get; set; }

        public int SecondaryPersonId { get; set; }
        public string EditRelationship { get; set; }
        public string SelectedRelationship { get; set; }
        public List<Entities.PersonalRelationshipType> PersonalRelationships { get; set; }
        public PersonOfInterest Person { get; set; } = new PersonOfInterest();
        public int OriginalRelatinshiptId;
        private int OriginalRelationshipType;

        public DeletePersonalRelationshipBase()
        {
            OriginalRelationshipType = 0;
            InitializePersonalRelationships();
        }
        private void InitializePersonalRelationships()
        {
            PersonalRelationships = new List<Entities.PersonalRelationshipType>();
            PersonalRelationships.Add(new Entities.PersonalRelationshipType { Id = 1, Name = "Conyuge" });
            PersonalRelationships.Add(new Entities.PersonalRelationshipType { Id = 2, Name = "Hijo" });
            PersonalRelationships.Add(new Entities.PersonalRelationshipType { Id = 3, Name = "Primo" });
            PersonalRelationships.Add(new Entities.PersonalRelationshipType { Id = 4, Name = "Hermano" });
            PersonalRelationships.Add(new Entities.PersonalRelationshipType { Id = 5, Name = "Compadrazgo" });
            PersonalRelationships.Add(new Entities.PersonalRelationshipType { Id = 6, Name = "Padre" });
            PersonalRelationships.Add(new Entities.PersonalRelationshipType { Id = 7, Name = "Amistad" });
        }
        public async override Task Show()
        {
            ResetDialog();
            if (SecondaryPersonId > 0)
            {
                Model.PersonOfInterest dataPerson = await PersonService.GetById(SecondaryPersonId);
                Person.Id = SecondaryPersonId;
                Person.FirstName = dataPerson.FirstName;
                Person.LastName = dataPerson.LastName;
                Person.SecondLastName = dataPerson.SecondLastName;
                if (!string.IsNullOrEmpty(EditRelationship))
                {
                    var relationshipt = PersonalRelationships.First(c => c.Name == EditRelationship);
                    SelectedRelationship = relationshipt.Name;
                }
            }
            ShowDialog = true;
            StateHasChanged();
        }

        protected override async Task HandleValidSubmit()
        {

            await PersonService.Delete(Person.Id);
                
           
            ShowDialog = false;
            SecondaryPersonId = 0;
            await CloseEventCallback.InvokeAsync(true);
            StateHasChanged();
        }
        protected override void ResetDialog()
        {

        }
    }
}
