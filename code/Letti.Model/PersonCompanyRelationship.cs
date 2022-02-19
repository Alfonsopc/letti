using System;
using System.Collections.Generic;
using System.Text;
using Zorbek.Essentials.Utilities.Interfaces;

namespace Letti.Model
{
    public class PersonCompanyRelationship:IIdentificable
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public Contractor Company { get; set; }
        public int PersonOfInterestId { get; set; }
        public PersonOfInterest PersonOfInterest { get; set; }
        public int ContractorRelationshipTypeId { get; set; }
        public ContractorRelationshipType ContractorRelationshipType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; }
    }
}
