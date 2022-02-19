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
    public class PoiScanningRepository : GenericRepository<Model.PoiScanning, LettiContext>, IPoiScanningRepository
    {
        public PoiScanningRepository(LettiContext dbContext) : base(dbContext)
        {
        }
        public async Task<PoiScanning> Create(int poiId)
        {
            PoiScanning poiScanning = new PoiScanning();
            poiScanning.PoiId = poiId;
            poiScanning.Timestamp = DateTime.UtcNow;
            await base.Create(poiScanning);
            return poiScanning;
        }

        public async Task<PersonOfInterest> GetNext()
        {
            PoiScanning poiScanning = await base._dbContext.PersonScans.FirstOrDefaultAsync(c => c.LastPropertyRegistryScan == null);
            if (poiScanning == null)
            {
                return new PersonOfInterest();
            }
            PersonOfInterest poi = await base._dbContext.PersonOfInterests.FirstOrDefaultAsync(c => c.Id == poiScanning.PoiId);
            return poi;
        }

        public async Task MarkAsScanned(int poiId)
        {
            PoiScanning poiScanning = await base._dbContext.PersonScans.FirstOrDefaultAsync(c => c.PoiId == poiId);
            poiScanning.LastPropertyRegistryScan = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();
        }
    }
}
