using Letti.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Letti.Web.Helpers
{
    public class PoiSearchResponse
    {
        public List<PersonOfInterest> People { get; private set; }
        public bool ServiceCalled { get; set; }
        public PoiSearchResponse()
        {
            People = new List<PersonOfInterest>();
        }
    }
}
