using System;
using System.Collections.Generic;
using System.Text;
using Zorbek.Essentials.Utilities.Interfaces;

namespace Letti.Model
{
    public class Contract:IIdentificable
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }
        public int ContractorId { get; set; }
        public Contractor Contractor { get; set; }
        public string Concept { get; set; }
        public decimal Value { get; set; }
        public string Currency { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Number { get; set; }
        public string ContractType { get; set; }
        public string Bidding { get; set; }
        public string Url { get; set; }
        public string ContractObjectType { get; set; }
        public string Description { get; set; }
        public string Source { get; set; }
    }
}
