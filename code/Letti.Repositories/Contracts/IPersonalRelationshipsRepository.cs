using Letti.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Letti.Repositories.Contracts
{
    public interface IPersonalRelationshipsRepository
    {
        Task<ICollection<PersonalRelationship>> GetRelationshipsForPerson(int personId);
        Task<PersonalRelationship> Create(PersonalRelationship relationship);
        Task<PersonalRelationship> Update(PersonalRelationship relationship);
        Task Delete(PersonalRelationship relationship);
    }
}
