using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Letti.Web.Entities
{
    public class ContractsByOrganizationView
    {
        public string Organization { get; set; }
        public int OrganizationId { get; set; }
        public int Contracts { get; set; }
        public decimal Value { get; set; }
        public string Currency { get; set; }
    }
}
