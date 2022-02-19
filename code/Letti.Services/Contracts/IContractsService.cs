using Letti.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Letti.Services.Contracts
{
    public interface IContractsService
    {
        Task<Contract> Create(Contract contract);
        Task<ICollection<Contract>> GetContractsByContractorId(int contractorId);
        Task<ICollection<Contract>> GetContractsByOrganizationId(int organizationId);
    }
}
