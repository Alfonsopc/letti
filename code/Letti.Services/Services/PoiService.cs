using Letti.Repositories.Contracts;
using Letti.Services.Contracts;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Letti.Model;
using System.Text.RegularExpressions;

namespace Letti.Services.Services
{
    public class PoiService:IPoiService
    {
        IPersonOfInterestRepository poiRepository;
        IPersonalRelationshipsRepository relationshipsRepository;
        public PoiService(IPersonOfInterestRepository poiRepository,IPersonalRelationshipsRepository relationshipsRepository)
        {
            this.poiRepository = poiRepository;
            this.relationshipsRepository = relationshipsRepository;
        }

        public async Task Delete(int poiId)
        {
            var relationships=await relationshipsRepository.GetRelationshipsForPerson(poiId);
            if(relationships!=null && relationships.Any())
            {
                foreach(var relationship in relationships)
                {
                    await relationshipsRepository.Delete(relationship);
                }
            }
            await poiRepository.Delete(poiId);
        }

        public async Task<ICollection<Model.PersonOfInterest>> Search(string filter)
        {
            List<PersonOfInterest> response = new List<PersonOfInterest>();
            if (string.IsNullOrEmpty(filter))
            {
                return response;
            }
            int id = 0;
            string[] parameters = filter.Split(' ');
            if (parameters.Length == 1)
            {
                
                if (!ContainsNumber(filter))
                {
                    var simpleQuery = await poiRepository.SearchInName(filter);
                    response.AddRange(simpleQuery);
                }
                else
                {
                    var listByEmployeeNumber = await poiRepository.SearchInCURP(filter);
                    if(listByEmployeeNumber!=null && listByEmployeeNumber.Any())
                    {
                        response.AddRange(listByEmployeeNumber);
                    }
                    
                }
               
            }
            else
            {
                var simpleQuery = await poiRepository.SearchInName(filter);
                response.AddRange(simpleQuery);
            }
            return response;
        }

        private bool ContainsNumber(string input)
        {
            Regex regex = new Regex("[0-9]");

            return regex.IsMatch(input);
        }
    }
}
