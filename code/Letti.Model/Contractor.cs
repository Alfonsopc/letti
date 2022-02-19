using System;
using System.Collections.Generic;
using System.Text;
using Zorbek.Essentials.Utilities.Interfaces;

namespace Letti.Model
{
    public class Contractor:IIdentificable
    {
        public int Id { get; set; }
        public string ComercialName { get; set; }
        public string OfficialName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string TaxId { get; set; }
        public string FiscalAddress { get; set; }
        public DateTime FoundedOn { get; set; }
        public string Record { get; set; }
    }
}
