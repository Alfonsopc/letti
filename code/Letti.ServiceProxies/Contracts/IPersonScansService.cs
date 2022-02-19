using Letti.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Letti.ServiceProxies.Contracts
{
    public interface IPersonScansService
    {
        HttpClient HttpClient { get; }
        Task<PersonOfInterest> GetNext();
        Task MarkAsScanned(int poiId);
    }
}
