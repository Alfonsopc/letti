using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Letti.Web.Entities
{
    public class ContractsByContractorView
    {
        public string Contractor { get; set; }
        public int ContractorId { get; set; }
        public string TaxId { get; set; }
        public bool Flagged { get; set; }
        public int Contracts { get; set; }
        public decimal Value { get; set; }
        public string Currency { get; set; }

    }
}
