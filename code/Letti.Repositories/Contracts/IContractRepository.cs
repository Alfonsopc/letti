using Letti.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Letti.Repositories.Contracts
{
    public interface IContractRepository
    {
        Task<ICollection<Contract>> GetAll();
        Task<Contract> GetById(int id);
        Task<Contract> GetByNumber(string number);
        Task<ICollection<Contract>> GetByContractor(int contractorId);
        Task<ICollection<Contract>> GetByOrganization(int organizationId);
        Task<Contract> Create(Contract contract);
        Task<Contract> Update(Contract contract);
    }
}
