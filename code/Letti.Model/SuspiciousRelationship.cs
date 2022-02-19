using System;
using System.Collections.Generic;
using System.Text;

namespace Letti.Model
{
    public class SuspiciousRelationship
    {
        public int Id { get; set; }
        public int OrganizationPoiId { get; set; }
        public PersonOfInterest OrganizationPoi { get; set; }
        public string Job { get; set; }
        public int ContractorPoiId { get; set; }
        public PersonOfInterest ContractorPoi { get; set; }
        public string Position { get; set; }
        public string Relationship { get; set; }
        public CaseToReview CaseToReview { get; set; }
    }
}
