using Letti.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Letti.Services.Contracts
{
    public interface ICasesService
    {
        Task Review(Contract caseToReview);
        Task Review(PersonalRelationship relationShip);
        Task Review(PersonCompanyRelationship relationship);
        Task Review(PersonOrganizationRelationship relationship);
        Task<ICollection<CaseToReview>> GetPublicCases(int organizationId);
        Task<ICollection<CaseToReview>> GetPrivateCases(int organizationId, int userId);
    }
}
