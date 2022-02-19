using Letti.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Letti.Repositories.Contracts
{
    public interface IOrganizationRelationshipsRepository
    {
        Task<ICollection<PersonOrganizationRelationship>> GetRelationshipsForOrganization(int organizationId);
        Task<ICollection<PersonOrganizationRelationship>> GetOrganizationRelationshipsForPerson(int personId);
        Task<PersonOrganizationRelationship> Create(PersonOrganizationRelationship organization);
        Task<PersonOrganizationRelationship> Update(PersonOrganizationRelationship organization);
    }
}
