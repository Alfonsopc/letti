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
    public class PersonalRelationshipRepository : GenericRepository<Model.PersonalRelationship, LettiContext>,  IPersonalRelationshipsRepository
    {
        public PersonalRelationshipRepository(LettiContext dbContext) : base(dbContext)
        {
        }
        public new async Task<PersonalRelationship> Create(PersonalRelationship pr)
        {
            
            PersonalRelationshipType type = await _dbContext.PersonalRelationshipTypes.FirstAsync(c => c.Id == pr.PersonalRelationshipType.Id);
            pr.PersonalRelationshipType = type;
            await base.Create(pr);
            return pr;
            
        }

        public new async Task<PersonalRelationship> Update(PersonalRelationship pr)
        {
            pr.PersonalRelationshipTypeId = pr.PersonalRelationshipType.Id;
            await base.Update(pr);
          //  await _dbContext.SaveChangesAsync();
            return pr;
        }
        public async Task Delete(PersonalRelationship pr)
        {
            await base.Delete(pr.Id);
            return;
        }

        public async Task<ICollection<PersonalRelationship>> GetRelationshipsForPerson(int personId)
        {
            List<PersonalRelationship> relationships = new List<PersonalRelationship>();
            ICollection<PersonalRelationship> mainRelationships=await base._dbContext.PersonalRelationShips
                .Include(c=>c.SecondaryPerson).Include(c=>c.PersonalRelationshipType).Where(c => c.MainPersonId == personId).ToListAsync();
            relationships.AddRange(mainRelationships);
            ICollection<PersonalRelationship> secondaryRelationships = await base._dbContext.PersonalRelationShips
                .Include(c => c.MainPerson).Include(c => c.PersonalRelationshipType).Where(c => c.SecondaryPersonId == personId).ToListAsync();
            relationships.AddRange(secondaryRelationships);
            return relationships;
        }

        
    }
}
