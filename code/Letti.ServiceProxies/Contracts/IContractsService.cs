using Letti.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Letti.ServiceProxies.Contracts
{
    public interface IContractsService
    {
        Task<ICollection<Contract>> GetContractsByContractorId(int contractorId);
        Task<ICollection<Contract>> GetContractsByOrganizationId(int organizationId);
        Task<Contract> Create(Contract contract);
    }
}
