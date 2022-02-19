using Letti.Model;
using Letti.ServiceProxies.Contracts;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace Letti.Web.Pages
{
    public class OrganizationEditBase:ComponentBase
    {
        [Parameter]
        public string OrganizationId { get; set; }
        public Organization Organization { get; set; } = new Organization();
        [Inject]
        public IOrganizationService OrganizationService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        

        public List<string> Levels { get; set; } = new List<string>();
        public string SelectedLevelId { get; set; } = string.Empty;

        protected string Message = string.Empty;
        protected string StatusClass = string.Empty;
        protected bool Saved;
        protected override async Task OnInitializedAsync()
        {
            Saved = false;
            try
            {
               // Session session = await UserService.GetSession();
              
                if (!string.IsNullOrEmpty(OrganizationId))
                {
                    int organizationId = int.Parse(OrganizationId);
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
                    Organization = await OrganizationService.GetById(organizationId);
                 
                }
                else
                {
                    Organization = new Organization();
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

                if (string.IsNullOrEmpty(OrganizationId)) //new
                {
                    var response = await OrganizationService.Create(Organization);
                    if (response != null)
                    {
                        Organization = response;
                        StatusClass = "alert-success";
                        Message = "Dependencia agregada existosamente.";
                        Saved = true;
                    }
                    else
                    {
                        StatusClass = "alert-danger";
                        Message = "Algo falló al agregar la dependencia. Por favor intente de nuevo.";
                        Saved = false;
                    }
                }
                else
                {
                    await OrganizationService.Update(Organization);
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
            NavigationManager.NavigateTo("/organizations");
        }
    }
}
