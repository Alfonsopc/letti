using System;
using System.Collections.Generic;
using System.Text;
using Zorbek.Essentials.Utilities.Interfaces;

namespace Letti.Model
{
    public class ContractorScanning:IIdentificable
    {
        public int Id { get; set; }
        public int ContractorId { get; set; }
        public Contractor Contractor { get; set; }
        public DateTime Timestamp { get; set; }
        public DateTime? LastSigerScan { get; set; }
    }
}
