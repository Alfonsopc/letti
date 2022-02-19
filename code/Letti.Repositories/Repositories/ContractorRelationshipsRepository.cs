using Letti.Data;
using Letti.Model;
using Letti.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zorbek.Essentials.Core.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace Letti.Repositories.Repositories
{
    public class ContractorRelationshipsRepository : GenericRepository<Model.PersonCompanyRelationship, LettiContext>, IContractorRelationshipsRepository
    {
        public ContractorRelationshipsRepository(LettiContext dbContext) : base(dbContext)
        {
        }
        public async Task<ICollection<PersonCompanyRelationship>> GetEmployeesForContractor(int contractorId)
        {
            List<PersonCompanyRelationship> response = await base._dbContext.PersonCompanyRelationships
                .Include(c => c.PersonOfInterest).Include(c => c.ContractorRelationshipType).Where(c => c.CompanyId == contractorId).ToListAsync();
            return response;
        }

        public new async Task<PersonCompanyRelationship> Create(PersonCompanyRelationship cr)
        {
            await base.Create(cr);
            return cr;
        }

        public new async Task<PersonCompanyRelationship> Update(PersonCompanyRelationship cr)
        {
            await base.Update(cr);
            return cr;
        }

        public async Task<ICollection<PersonCompanyRelationship>> GetContractorForPersonId(int personId)
        {
            List<PersonCompanyRelationship> response = await base._dbContext.PersonCompanyRelationships
                .Include(c=>c.Company).Include(c=>c.ContractorRelationshipType).Where(c => c.PersonOfInterestId == personId).ToListAsync();
            return response;
        }
    }
}
