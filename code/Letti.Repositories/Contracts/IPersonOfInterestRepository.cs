using Letti.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Letti.Repositories.Contracts
{
    public interface IPersonOfInterestRepository
    {
        Task<ICollection<PersonOfInterest>> GetAll();
        Task<ICollection<PersonOfInterest>> SearchInName(string filter);
        Task<ICollection<PersonOfInterest>> SearchInCURP(string filter);
        Task<ICollection<PersonOfInterest>> SearchInRFC(string filter);
        Task<PersonOfInterest> GetById(int id);
        Task<PersonOfInterest> GetByNumber(string number);
        Task<PersonOfInterest> Create(PersonOfInterest contract);
        Task<PersonOfInterest> Update(PersonOfInterest contract);
        Task Delete(int personOfInterestId);
        Task<Property> AddProperty(Property property);
        Task<ICollection<Property>> GetProperties(int poiId);
    }
}
