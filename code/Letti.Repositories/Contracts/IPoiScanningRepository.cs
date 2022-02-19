using Letti.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Letti.Repositories.Contracts
{
    public interface IPoiScanningRepository
    {
        Task<PersonOfInterest> GetNext();
        Task MarkAsScanned(int poiId);
        Task<PoiScanning> Create(int poiId);
    }
}
