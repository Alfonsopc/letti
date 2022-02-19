using Letti.Data;
using Letti.Model;
using Letti.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zorbek.Essentials.Core.Data.Repository;

namespace Letti.Repositories.Repositories
{
    public class ContractorScansRepository : GenericRepository<Model.ContractorScanning, LettiContext>, IContractorScansRepository
    {
        public ContractorScansRepository(LettiContext dbContext) : base(dbContext)
        {
        }
        public async Task<ContractorScanning> Create(int contractorId)
        {
            ContractorScanning contractorScanning = new ContractorScanning();
            contractorScanning.ContractorId = contractorId;
            contractorScanning.Timestamp = DateTime.UtcNow;
            await base.Create(contractorScanning);
            return contractorScanning;
        }

        public async Task<Contractor> GetNext()
        {
            ContractorScanning contractorScanning=await base._dbContext.ContractorScans.FirstOrDefaultAsync(c => c.LastSigerScan == null);
            if(contractorScanning==null)
            {
                return new Contractor();
            }
            Contractor contractor= await base._dbContext.Contractors.FirstOrDefaultAsync(c => c.Id == contractorScanning.ContractorId);
            return contractor;
        }

        public async Task MarkAsScanned(int contractorId)
        {
            ContractorScanning contractorScanning = await base._dbContext.ContractorScans.FirstOrDefaultAsync(c => c.ContractorId==contractorId);
            contractorScanning.LastSigerScan = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();
        }
    }
}
