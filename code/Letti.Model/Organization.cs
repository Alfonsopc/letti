using System;
using System.Collections.Generic;
using System.Text;
using Zorbek.Essentials.Utilities.Interfaces;

namespace Letti.Model
{
    public class Organization:IIdentificable
    {
        public int Id { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationType { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}
