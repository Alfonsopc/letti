using Letti.Model;
using Letti.Repositories.Contracts;
using Letti.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Letti.Services.Services
{
    public class ContractsService : IContractsService
    {
        private IOrganizationRepository organizationRepository;
        private IContractorRepository contractorRepository;
        private IContractRepository contractRepository;
        public ContractsService(IContractRepository contractRepository,IOrganizationRepository organizationRepository,IContractorRepository contractorRepository)
        {
            this.contractRepository = contractRepository;
            this.contractorRepository = contractorRepository;
            this.organizationRepository = organizationRepository;
        }
        public async Task<Contract> Create(Contract contract)
        {
            contract.Id = 0;
            Organization organization = await organizationRepository.GetByName(contract.Organization.OrganizationName);
            contract.Organization = null;
            contract.OrganizationId = organization.Id;
            //Contractor contractor = await contractorRepository.GetByTaxId(contract.Contractor.TaxId);
            Contractor contractor = await contractorRepository.GetByName(contract.Contractor.OfficialName);
            if (contractor == null)
            {
                contract.Contractor.Id = 0;
                contractor = await contractorRepository.Create(contract.Contractor);
            }
            contract.Contractor = null;
            contract.ContractorId = contractor.Id;
            Contract response = await contractRepository.Create(contract);
            return response;
        }

        public async Task<ICollection<Contract>> GetContractsByContractorId(int contractorId)
        {
            ICollection<Contract> contracts = await contractRepository.GetByContractor(contractorId);
            return contracts;
        }

        public async Task<ICollection<Contract>> GetContractsByOrganizationId(int organizationId)
        {
            ICollection<Contract> contracts = await contractRepository.GetByOrganization(organizationId);
            return contracts;
        }
    }
}
