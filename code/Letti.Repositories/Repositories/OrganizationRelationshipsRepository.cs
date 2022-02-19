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
    public class OrganizationRelationshipsRepository : GenericRepository<Model.PersonOrganizationRelationship, LettiContext>, IOrganizationRelationshipsRepository
    {
        public OrganizationRelationshipsRepository(LettiContext dbContext) : base(dbContext)
        {
        }
        public async Task<ICollection<PersonOrganizationRelationship>> GetOrganizationRelationshipsForPerson(int personId)
        {
            List<PersonOrganizationRelationship> response = await base._dbContext.PersonOrganizationRelationships
                .Include(c=>c.Organization).Include(c=>c.OrganizationRelationshipType).Where(c => c.PersonOfInterestId == personId).ToListAsync();
            return response;
        }

        public async Task<ICollection<PersonOrganizationRelationship>> GetRelationshipsForOrganization(int organizationId)
        {
            List<PersonOrganizationRelationship> response =await base._dbContext.PersonOrganizationRelationships
                .Include(c => c.PersonOfInterest).Include(c => c.OrganizationRelationshipType).Where(c => c.OrganizationId == organizationId).ToListAsync();
            return response;
        }


        public new async Task<PersonOrganizationRelationship> Create(PersonOrganizationRelationship or)
        {
            await base.Create(or);
            return or;
        }

        public new async Task<PersonOrganizationRelationship> Update(PersonOrganizationRelationship or)
        {
            await base.Update(or);
            return or;
        }
    }
}
