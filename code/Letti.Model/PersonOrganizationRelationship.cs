using System;
using System.Collections.Generic;
using System.Text;
using Zorbek.Essentials.Utilities.Interfaces;

namespace Letti.Model
{
    public class PersonOrganizationRelationship:IIdentificable
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }
        public int PersonOfInterestId { get; set; }
        public PersonOfInterest PersonOfInterest { get; set; }
        public int OrganizationRelationshipTypeId { get; set; }
        public OrganizationRelationshipType OrganizationRelationshipType { get; set; }
        public string Job { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; }
    }
}
