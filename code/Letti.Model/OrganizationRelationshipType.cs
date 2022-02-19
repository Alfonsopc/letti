using System;
using System.Collections.Generic;
using System.Text;

namespace Letti.Model
{
    public class OrganizationRelationshipType
    {
        public int Id { get; set; }
        public string RelationshipType { get; set; }
        public int Weight { get; set; }
    }
}
