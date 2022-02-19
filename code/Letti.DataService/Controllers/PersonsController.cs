using Letti.Model;
using Letti.Repositories.Contracts;
using Letti.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Letti.DataService.Controllers
{
    [Route("api/persons")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonOfInterestRepository personRepository;
        private readonly IPersonalRelationshipsRepository personalRelationshipsRepository;
        private readonly IPoiService poiService;
        public PersonsController(IPoiService poiService, IPersonOfInterestRepository personRepository,IPersonalRelationshipsRepository personalRelationshipsRepository)
        {
            this.poiService = poiService;
            this.personRepository = personRepository;
            this.personalRelationshipsRepository = personalRelationshipsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                ICollection<PersonOfInterest> contractors = await personRepository.GetAll();
                return Ok(contractors);
            }
            catch (SystemException exp)
            {
                return BadRequest();
            }

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                PersonOfInterest person = await personRepository.GetById(id);
                return Ok(person);
            }
            catch (SystemException exp)
            {
                return BadRequest();
            }
        }
        [HttpGet("search/{filter}")]
        public async Task<IActionResult> Search(string filter)
        {
            try
            {
                ICollection<PersonOfInterest> persons = await poiService.Search(filter);
                return Ok(persons);
            }
            catch (SystemException exp)
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post(PersonOfInterest person)
        {
            try
            {
               
                PersonOfInterest response = await personRepository.Create(person);
                return Ok(response);
            }
            catch (SystemException exp)
            {
                return BadRequest(exp.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(PersonOfInterest person)
        {
            try
            {
                //Model.PersonOfInterest person = value.ToObject<Model.PersonOfInterest>();
                PersonOfInterest response = await personRepository.Update(person);
                return Ok(response);
            }
            catch (SystemException exp)
            {
                return BadRequest(exp.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await poiService.Delete(id);
                return Ok();
            }
            catch (SystemException exp)
            {
                return BadRequest();
            }
        }
        [HttpGet("relationships/{id}")]
        public async Task<IActionResult> GetRelationshipsById(int id)
        {
            try
            {
                ICollection<PersonalRelationship> personalRelationships = await personalRelationshipsRepository.GetRelationshipsForPerson(id);
                return Ok(personalRelationships);
            }
            catch (SystemException exp)
            {
                return BadRequest();
            }
        }
        [HttpPatch("personalrelationships")]
        public async Task<IActionResult> AddRelationship([FromBody]PersonalRelationship relationShip)
        {
            try
            {
                PersonalRelationship response = null;
                if (relationShip.Id==0)
                {
                   response= await personalRelationshipsRepository.Create(relationShip);
                }
                else
                {
                   response = await personalRelationshipsRepository.Update(relationShip);
                }
                return Ok(response);
               
            }
            catch (SystemException exp)
            {
                return BadRequest();
            }
        }

        [HttpPatch("properties")]
        public async Task<IActionResult> AddProperty([FromBody] Property property)
        {
            try
            {
                Property response = await personRepository.AddProperty(property);
                return Ok(response);
            }
            catch (SystemException exp)
            {
                return BadRequest();
            }
        }

        [HttpGet("properties/{id}")]
        public async Task<IActionResult> GetProperties(int id)
        {
            try
            {
                ICollection<Property> properties = await personRepository.GetProperties(id);
                return Ok(properties);
            }
            catch (SystemException exp)
            {
                return BadRequest();
            }
        }
    }
}
