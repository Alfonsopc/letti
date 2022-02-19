using Letti.Model;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Letti.Repositories.Contracts
{
    public interface IContractorScansRepository
    {
        Task<Contractor> GetNext();
        Task MarkAsScanned(int contractorId);
        Task<ContractorScanning> Create(int contractorId);
    }
}
