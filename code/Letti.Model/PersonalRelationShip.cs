using System;
using System.Collections.Generic;
using System.Text;
using Zorbek.Essentials.Utilities.Interfaces;

namespace Letti.Model
{
    public class PersonalRelationship:IIdentificable
    {
        public int Id { get; set; }
        public int MainPersonId { get; set; }
        public PersonOfInterest MainPerson { get; set; }
        public int SecondaryPersonId { get; set; }
        public PersonOfInterest SecondaryPerson { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int PersonalRelationshipTypeId { get; set; }
        public PersonalRelationshipType PersonalRelationshipType { get; set; }
        public int? UserId { get; set; }
        public int CreatedBy { get; set; }
        public bool IsOficial { get; set; }
    }
}
