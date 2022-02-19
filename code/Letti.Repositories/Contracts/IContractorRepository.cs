using Letti.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Letti.Repositories.Contracts
{
    public interface IContractorRepository
    {
        Task<ICollection<Contractor>> GetAll();
        Task<Contractor> GetById(int id);
        Task<Contractor> GetByTaxId(string taxid);
        Task<Contractor> GetByName(string name);
        Task<Contractor> GetByNumber(string number);
        Task<Contractor> Create(Contractor contract);
        Task<Contractor> Update(Contractor contract);
    }
}
