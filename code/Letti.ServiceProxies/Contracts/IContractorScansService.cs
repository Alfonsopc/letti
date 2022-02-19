using Letti.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Letti.ServiceProxies.Contracts
{
    public interface IContractorScansService
    {
        HttpClient HttpClient { get; }
        Task<Contractor> GetNext();
        Task MarkAsScanned(int contractorId);
    }
}
