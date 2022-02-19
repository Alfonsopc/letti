using System;
using System.Collections.Generic;
using System.Text;
using Zorbek.Essentials.Utilities.Interfaces;

namespace Letti.Model
{
    public class PoiScanning:IIdentificable
    {
        public int Id { get; set; }
        public int PoiId { get; set; }
        public PersonOfInterest Person { get; set; }
        public DateTime Timestamp { get; set; }
        public DateTime? LastPropertyRegistryScan { get; set; }
    }
}
