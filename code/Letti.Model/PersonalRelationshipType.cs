using System;
using System.Collections.Generic;
using System.Text;

namespace Letti.Model
{
    public class PersonalRelationshipType
    {
        public int Id { get; set; }
        public string RelationshipType { get; set; }
        public int Weight { get; set; }
        public string InverseRelationshipType { get; set; }
    }
}
