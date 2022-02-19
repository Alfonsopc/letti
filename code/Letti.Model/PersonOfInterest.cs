using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Zorbek.Essentials.Utilities.Interfaces;

namespace Letti.Model
{
    public class PersonOfInterest:IIdentificable
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SecondLastName { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public bool Active { get; set; }
        public bool IsOficial { get; set; }
        public string RFC { get; set; }
        public string CURP { get; set; }
        [JsonIgnore]
        public string FullName => $"{LastName} {SecondLastName} {FirstName}";
    }
}
