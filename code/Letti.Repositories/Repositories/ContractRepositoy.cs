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
    public class ContractRepositoy:GenericRepository<Model.Contract, LettiContext>, IContractRepository
    {
        public ContractRepositoy(LettiContext dbContext) : base(dbContext)
        {
        }

        public async Task<ICollection<Contract>> GetAll()
        {
            List<Contract> contracts = await base.GetList().ToListAsync();
            return contracts;
        }

        public async Task<ICollection<Contract>> GetByContractor(int contractorId)
        {
            ICollection<Contract> contracts = await _dbContext.Contracts.
                Include(c=>c.Organization).Include(c=>c.Contractor).Where(c => c.ContractorId == contractorId).ToListAsync();
            return contracts;
        }

        public async Task<Contract> GetById(int id)
        {
            Contract contract = await base.Read(id);
            return contract;
        }

        public async Task<Contract> GetByNumber(string number)
        {
            Contract contract = await _dbContext.Contracts.
                Include(c => c.Organization).Include(c => c.Contractor).FirstOrDefaultAsync(c => c.Number == number);
            return contract;
        }

        public new async Task<Contract> Create(Contract contract)
        {
            await base.Create(contract);
            return contract;
        }

        public new async Task<Contract> Update(Contract contract)
        {
            await base.Update(contract);
            return contract;
        }

        public async Task<ICollection<Contract>> GetByOrganization(int organizationId)
        {
            ICollection<Contract> contracts = await _dbContext.Contracts.
                Include(c => c.Organization).Include(c => c.Contractor).Where(c => c.OrganizationId == organizationId).ToListAsync();
            return contracts;
        }
    }
}
