using Letti.Data;
using Letti.Model;
using Letti.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zorbek.Essentials.Core.Data.Repository;


namespace Letti.Repositories.Repositories
{
    public class OrganizationRepository : GenericRepository<Model.Organization, LettiContext>, IOrganizationRepository
    {
        public OrganizationRepository(LettiContext dbContext) : base(dbContext)
        {
        }

        public async Task<ICollection<Organization>> GetAll()
        {
            List<Organization> organizations = await base.GetList().OrderBy(c=>c.OrganizationName).ToListAsync();
            return organizations;
        }

        public async Task<Organization> GetById(int id)
        {
            Organization organization = await base.Read(id);
            return organization;
        }

        

        public new async Task<Organization> Create(Organization organization)
        {
            await base.Create(organization);
            return organization;
        }

        public new async Task<Organization> Update(Organization organization)
        {
            await base.Update(organization);
            return organization;
        }

        public async Task<Organization> GetByName(string name)
        {
            Organization organization = await _dbContext.Organizations.FirstOrDefaultAsync(c => c.OrganizationName == name);
            return organization;
        }
    }
}
