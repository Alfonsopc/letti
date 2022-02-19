using Letti.Model;
using Letti.ServiceProxies.Contracts;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace Letti.Web.Pages
{
    public class PersonEditBase : ComponentBase
    {
        [Parameter]
        public string PersonId { get; set; }
        public PersonOfInterest Person { get; set; } = new PersonOfInterest();
        [Inject]
        public IPersonService PersonService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
      

        protected string Message = string.Empty;
        protected string StatusClass = string.Empty;
        protected bool Saved;
        protected override async Task OnInitializedAsync()
        {
            Saved = false;
            try
            {
                // Session session = await UserService.GetSession();

                if (!string.IsNullOrEmpty(PersonId))
                {
                    int personId = int.Parse(PersonId);
                    //OrganizationService.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session.AccessToken);
                    /* if (!AreaService.HttpClient.DefaultRequestHeaders.Contains("PlantId"))
                     {
                         AreaService.HttpClient.DefaultRequestHeaders.Add("PlantId", plant.Name);
                     }*/

                    /*OrganizationService.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session.AccessToken);
                    if (!MachineService.HttpClient.DefaultRequestHeaders.Contains("PlantId"))
                    {
                        MachineService.HttpClient.DefaultRequestHeaders.Add("PlantId", plant.Name);
                    }*/
                    Person = await PersonService.GetById(personId);

                }
                else
                {
                    Person = new PersonOfInterest();
                }
            }
            catch (Exception exp)
            {
                string message = exp.Message;
            }
        }


        protected async Task HandleValidSubmit()
        {
            Saved = false;
            try
            {
                //Session session = await UserService.GetSession();

                //OrganizationService.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session.AccessToken);

                if (string.IsNullOrEmpty(PersonId)) //new
                {
                    var response = await PersonService.Create(Person);
                    if (response != null)
                    {
                        Person = response;
                        StatusClass = "alert-success";
                        Message = "Persona agregada existosamente.";
                        Saved = true;
                    }
                    else
                    {
                        StatusClass = "alert-danger";
                        Message = "Algo falló al agregar a la persona. Por favor intente de nuevo.";
                        Saved = false;
                    }
                }
                else
                {
                    await PersonService.Update(Person);
                    StatusClass = "alert-success";
                    Message = "Dependencia actualizada exitosamente";
                    Saved = true;
                }
            }
            catch (Exception exp)
            {
                string message = exp.Message;
            }

        }

        protected void NavigateToOverview()
        {
            NavigationManager.NavigateTo("/persons");
        }
    }
}
