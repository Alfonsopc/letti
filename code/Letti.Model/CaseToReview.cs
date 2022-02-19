using System;
using System.Collections.Generic;
using System.Text;

namespace Letti.Model
{
    public class CaseToReview
    {
        public int Id { get; set; }
        public Enums.CaseType CaseType { get; set; }
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }
        public int ContractorId { get; set; }
        public Contractor Contractor { get; set; }
        public DateTime Timestamp { get; set; }
        public DateTime CaseDate { get; set; }
        public List<Contractor> Contractors { get; set; }
        public List<SuspiciousRelationship> SuspiciousRelationships { get; set; }
        public int? UserId { get; set; }
    }
}
