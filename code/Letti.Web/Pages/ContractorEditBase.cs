using Letti.Model;
using Letti.ServiceProxies.Contracts;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Letti.Web.Pages
{
    public class ContractorEditBase:ComponentBase
    {
        [Parameter]
        public string ContractorId { get; set; }
        public Contractor Contractor { get; set; } = new Contractor();
        [Inject]
        public IContractorService ContractorService { get; set; }
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

                if (!string.IsNullOrEmpty(ContractorId))
                {
                    int contractorId = int.Parse(ContractorId);
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
                    Contractor = await ContractorService.GetById(contractorId);

                }
                else
                {
                    Contractor = new Contractor();
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

                if (string.IsNullOrEmpty(ContractorId)) //new
                {
                    var response = await ContractorService.Create(Contractor);
                    if (response != null)
                    {
                        Contractor = response;
                        StatusClass = "alert-success";
                        Message = "Proveedor agregado existosamente.";
                        Saved = true;
                    }
                    else
                    {
                        StatusClass = "alert-danger";
                        Message = "Algo falló al agregar el proveedor. Por favor intente de nuevo.";
                        Saved = false;
                    }
                }
                else
                {
                    await ContractorService.Update(Contractor);
                    StatusClass = "alert-success";
                    Message = "Proveedor actualizada exitosamente";
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
            NavigationManager.NavigateTo("/contractors");
        }
    }
}
