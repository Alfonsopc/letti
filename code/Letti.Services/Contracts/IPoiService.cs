using Letti.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Letti.Services.Contracts
{
    public interface IPoiService
    {
        Task Delete(int poiId);
        Task<ICollection<PersonOfInterest>> Search(string filter);
    }
}
