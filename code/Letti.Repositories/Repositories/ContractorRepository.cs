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
    public class ContractorRepository : GenericRepository<Model.Contractor, LettiContext>, IContractorRepository
    {
        public ContractorRepository(LettiContext dbContext) : base(dbContext)
        {
        }
        

        public async Task<ICollection<Contractor>> GetAll()
        {
           List<Contractor> contractors= await base.GetList().ToListAsync();
            return contractors;
        }

        public async Task<Contractor> GetById(int id)
        {
            Contractor contractor= await base.Read(id);
            return contractor;
        }

        public async Task<Contractor> GetByNumber(string number)
        {
            throw new NotImplementedException();
        }

        public new async Task<Contractor> Create(Contractor contractor)
        {
            await base.Create(contractor);
            return contractor;
        }

        public new async Task<Contractor> Update(Contractor contractor)
        {
            await base.Update(contractor);
            return contractor;
        }

        public async Task<Contractor> GetByTaxId(string taxid)
        {
            Contractor contractor = await _dbContext.Contractors.FirstOrDefaultAsync(c => c.TaxId == taxid);
            return contractor;
        }

        public async Task<Contractor> GetByName(string name)
        {
            Contractor contractor = await _dbContext.Contractors.FirstOrDefaultAsync(c => c.OfficialName == name);
            return contractor;
        }
    }
}
