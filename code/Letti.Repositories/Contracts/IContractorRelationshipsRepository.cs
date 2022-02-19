using Letti.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Letti.Repositories.Contracts
{
    public interface IContractorRelationshipsRepository
    {
        Task<ICollection<PersonCompanyRelationship>> GetEmployeesForContractor(int contractorId);
        Task<ICollection<PersonCompanyRelationship>> GetContractorForPersonId(int personId);
        Task<PersonCompanyRelationship> Create(PersonCompanyRelationship cr);
        Task<PersonCompanyRelationship> Update(PersonCompanyRelationship cr);
    }
}

