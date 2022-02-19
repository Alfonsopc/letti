using Letti.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Letti.Repositories.Contracts
{
    public interface IOrganizationRepository
    {
        Task<ICollection<Organization>> GetAll();
        Task<Organization> GetById(int id);      
        Task<Organization> GetByName(string name);
        Task<Organization> Create(Organization organization);
        Task<Organization> Update(Organization organization);
    }
}
