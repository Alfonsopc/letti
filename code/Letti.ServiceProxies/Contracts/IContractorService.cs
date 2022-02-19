using Letti.Model;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Letti.ServiceProxies.Contracts
{
    public interface IContractorService
    {
        HttpClient HttpClient { get; }
        Task<Contractor> Create(Contractor contractor);
        Task<Contractor> Update(Contractor contractor);
        Task<ICollection<Contractor>> GetAll();
        Task<Contractor> GetById(int id);
        Task<ICollection<PersonCompanyRelationship>> GetContractorsByPoiId(int id);
        Task<ICollection<PersonCompanyRelationship>> GetEmployeesByContractorId(int id);
        Task<PersonCompanyRelationship> AddRelationship(PersonCompanyRelationship relationship);
    }
}
