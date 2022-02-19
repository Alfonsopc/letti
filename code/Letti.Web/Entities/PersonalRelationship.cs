using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Letti.Web.Entities
{
    public class PersonalRelationship
    {
        public string Name { get; set; }
        public string Relationship { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public int DisplayedPersonId { get; set; }
        public int Id { get; set; }
     
        public PersonalRelationship()
        {
            
        }
        public PersonalRelationship(Model.PersonalRelationship relationship)
        {
            Id = relationship.Id;
            if(relationship.SecondaryPerson!=null)
            {
                Name = $"{ relationship.SecondaryPerson.FirstName} {relationship.SecondaryPerson.LastName} {relationship.SecondaryPerson.SecondLastName}";
                Relationship = relationship.PersonalRelationshipType.RelationshipType;
                DisplayedPersonId = relationship.SecondaryPersonId;
            }
            if(relationship.MainPerson!=null)
            {
                Name = $"{ relationship.MainPerson.FirstName} {relationship.MainPerson.LastName} {relationship.MainPerson.SecondLastName}";
                Relationship = relationship.PersonalRelationshipType.InverseRelationshipType;
                DisplayedPersonId = relationship.MainPersonId;
            }
            Start = relationship.StartDate;
            End = relationship.EndDate;
        }
    }
}
