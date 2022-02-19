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
    public class AddRelationshipDialogBase:DialogBase,IDisposable
    {
        [Inject]        
        public IPersonService PersonService { get; set; }
        [Parameter]
        public int MainPersonId { get; set; }
       
        public int SecondaryPersonId { get; set; }
        public string EditRelationship { get; set; }
        public string SelectedRelationshipId { get; set; } = "1";
        public List<Entities.PersonalRelationshipType> PersonalRelationships { get; set; }
        public PersonOfInterest Person { get; set; } = new PersonOfInterest();

        public int OriginalRelatinshiptId;
        private int OriginalRelationshipType;
        protected EditContext EditContext;
        protected List<PersonOfInterest> Candidates = new List<PersonOfInterest>();
        private bool searching;
        private string lastSearchString = string.Empty;
        private List<PersonOfInterest> searchResults = new List<PersonOfInterest>();
        public AddRelationshipDialogBase()
        {
            OriginalRelationshipType = 0;
            InitializePersonalRelationships();
            EditContext = new EditContext(Person);
            EditContext.OnFieldChanged += EditContext_OnFieldChanged;
            
        }

        private async void EditContext_OnFieldChanged(object sender, FieldChangedEventArgs e)
        {
            string modified=e.FieldIdentifier.FieldName;
            if(modified=="LastName")
            {
                ICollection<PersonOfInterest> candidates = await PersonService.Search(Person.LastName);
                if (candidates != null && candidates.Any())
                {
                    searchResults.Clear();
                    Candidates.Clear();
                    searchResults.AddRange(candidates);
                    Candidates.AddRange(candidates);
                }
            }
            if(modified== "SecondLastName")
            {
                string name = Person.LastName.ToLower()+" "+Person.SecondLastName;
                Candidates.Clear();
                Candidates.AddRange(searchResults.Where(c => c.FullName.ToLower().Contains(name)));            
            }
            StateHasChanged();
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
            PersonalRelationships.Add(new Entities.PersonalRelationshipType { Id = 8, Name = "Yerno/Nuera" });
        }
        public async override Task Show()
        {
            ResetDialog();
            if(SecondaryPersonId>0)
            {
                Model.PersonOfInterest dataPerson= await PersonService.GetById(SecondaryPersonId);
                Person.Id = SecondaryPersonId;
                Person.FirstName = dataPerson.FirstName;
                Person.LastName = dataPerson.LastName;
                Person.SecondLastName = dataPerson.SecondLastName;
                if (!string.IsNullOrEmpty(EditRelationship))
                {
                    var relationshipt = PersonalRelationships.First(c => c.Name == EditRelationship);
                    SelectedRelationshipId = relationshipt.Id.ToString();
                    OriginalRelationshipType = relationshipt.Id;
                }
            }
            ShowDialog = true;
            StateHasChanged();
        }

        protected override async Task HandleValidSubmit()
        {
            if (Person.Id == 0)
            {
                var response = await PersonService.Create(Person);
                if (response != null)
                {
                    int relationshipType = int.Parse(SelectedRelationshipId);
                    Model.PersonalRelationship personalRelationShip = new PersonalRelationship();
                    personalRelationShip.MainPersonId = MainPersonId;
                    personalRelationShip.SecondaryPersonId = response.Id;
                    personalRelationShip.PersonalRelationshipTypeId = relationshipType;
                    personalRelationShip.PersonalRelationshipType = new PersonalRelationshipType() { Id = relationshipType };
                    await PersonService.EditPersonalRelationShip(personalRelationShip);
                }
            }
            else
            {
                var response = await PersonService.Update(Person);
                if (response != null)
                {
                    int relationshipType = int.Parse(SelectedRelationshipId);
                    if(relationshipType!=OriginalRelationshipType)
                    {
                        Model.PersonalRelationship personalRelationShip = new PersonalRelationship();
                        personalRelationShip.Id = OriginalRelatinshiptId;
                        personalRelationShip.MainPersonId = MainPersonId;
                        personalRelationShip.SecondaryPersonId = response.Id;
                        personalRelationShip.PersonalRelationshipType = new PersonalRelationshipType() { Id = relationshipType };
                        await PersonService.EditPersonalRelationShip(personalRelationShip);
                    }
                   
                }
            }
            ShowDialog = false;
            SecondaryPersonId = 0;
            await CloseEventCallback.InvokeAsync(true);
            StateHasChanged();
        }

        protected override void ResetDialog()
        {
            searchResults.Clear();
            Candidates.Clear();
            lastSearchString = string.Empty;
        }
        public void Dispose()
        {
            EditContext.OnFieldChanged -= EditContext_OnFieldChanged;
        }
        //protected async void KeyUp(KeyboardEventArgs e, string memberName)
        //{
        //    var property = Person.GetType().GetProperty(memberName);
        //    var value = property.GetValue(Person);

        //    // Requires some tweaks to prevent unwanted stuff...
        //    if (e.Key!=null)
        //    {
        //        if (e.Key.Length == 1)
        //        {
        //            if(e.Code=="Quote")
        //            {

        //            }
        //            else
        //            {
        //                property.SetValue(Person, value + e.Key);
        //            }

        //        }
        //        if(e.Key=="Backspace")
        //        {
        //            string tmp = value.ToString();
        //            tmp = tmp.Substring(0, tmp.Length - 1);
        //            property.SetValue(Person, tmp);
        //        }

        //        var id = EditContext.Field(memberName);


        //        EditContext.NotifyFieldChanged(id);
        //        property = Person.GetType().GetProperty(memberName);

        //        string lastName = property.GetValue(Person).ToString().ToLower();
        //        if (lastName.Length > 3)
        //        {   
        //            if(searching)
        //            {
        //                return;
        //            }
        //            string name = Person.LastName.ToLower();
        //            if(memberName=="SecondLastName")
        //            {
        //                name += " " + lastName.ToLower();
        //            }
        //            if (!string.IsNullOrEmpty(lastSearchString) && name.Contains(lastSearchString))
        //            {
        //                Candidates.Clear();
        //                Candidates.AddRange(searchResults.Where(c => c.FullName.ToLower().Contains(name)));
        //                return ;
        //            }
        //            searching = true;
        //            lastSearchString = name;
        //            searchResults.Clear();

        //            ICollection<PersonOfInterest> candidates = await PersonService.Search(name);
        //            if (candidates != null && candidates.Any())
        //            {
        //                searchResults.AddRange(candidates);
        //                Candidates.AddRange(candidates);
        //            }
        //            searching = false;
        //        }
        //    }
        //}
    }
}
