using Letti.Model;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Letti.ServiceProxies.Contracts
{
    public interface IPersonService
    {
        HttpClient HttpClient { get; }
        Task<PersonOfInterest> Create(PersonOfInterest person);
        Task<PersonOfInterest> Update(PersonOfInterest person);
        Task<ICollection<PersonOfInterest>> GetAll();
        Task<ICollection<PersonOfInterest>> Search(string filter);
        Task<PersonOfInterest> GetById(int id);
        Task<ICollection<PersonalRelationship>> GetPersonalRelationShips(int id);
        Task<PersonalRelationship> EditPersonalRelationShip(PersonalRelationship relationship);
        Task<Property> AddProperty(Property property);
        Task<ICollection<Property>> GetProperties(int poiId);
        Task Delete(int poi);
    }
}
