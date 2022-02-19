using Letti.Model;
using Letti.ServiceProxies.Service;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ManualImporter.Controller
{
    public class ContractController
    {
        //string api = "http://localhost:55926/api/";
        string api = "http://XXXX.cloudapp.net:8020/LettiAPI/api/";
        private HttpClient httpClient;
        private ContractsService contractsService;
        private OrganizationService organizationService;
        public ContractController()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(api);
            contractsService = new ContractsService(httpClient);
            organizationService = new OrganizationService(httpClient);
        }
        public async Task Create(Contract contract)
        {
            try
            {
                await contractsService.Create(contract);
            }
            catch(Exception exp)
            {
                string message = exp.Message;
            }
        }
        public async Task<ICollection<Organization>> GetOrganizations()
        {
            var response = await organizationService.GetAll();
            return response;
        }

    }
}
