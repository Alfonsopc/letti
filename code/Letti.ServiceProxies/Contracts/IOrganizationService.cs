using Letti.Model;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Letti.ServiceProxies.Contracts
{
    public interface IOrganizationService
    {
        HttpClient HttpClient { get; }
        Task<Organization> Create(Organization organization);
        Task<Organization> Update(Organization organization);
        Task<ICollection<Organization>> GetAll();
        Task<Organization> GetById(int id);
        Task<ICollection<PersonOrganizationRelationship>> GetOrganizationsByPoiId(int id);
        Task<ICollection<PersonOrganizationRelationship>> GetEmployeesByOrganizationId(int id);
        Task<PersonOrganizationRelationship> EditPersonOrganizationRelationShip(PersonOrganizationRelationship relationship);
    }
}
