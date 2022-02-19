using System;
using System.Collections.Generic;
using System.Text;

namespace Letti.Model
{
    public class Property
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public PersonOfInterest Owner { get; set; }
        public string Description { get; set; }
        public string Ownership { get; set; }
        public string Record { get; set; }
        public string Source { get; set; }
        public string Colony { get; set; }
        public string OwnerType { get; set; }
        public string CadastralCode { get; set; }
        public string Certificate { get; set; }
        public DateTime ScrappedTimestamp { get; set; }
    }
}
