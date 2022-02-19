using Letti.Model;
using Letti.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Letti.DataService.Controllers
{
    [Route("api/contractors")]
    [ApiController]
    public class ContractorController : ControllerBase
    {
        private IContractorRepository contractorRepository;
        private IContractorRelationshipsRepository relationshipsRepository;
        public ContractorController(IContractorRepository contractorRepository,IContractorRelationshipsRepository relationshipsRepository)
        {
            this.contractorRepository = contractorRepository;
            this.relationshipsRepository = relationshipsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                ICollection<Contractor> contractors = await contractorRepository.GetAll();
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
                Contractor contractor = await contractorRepository.GetById(id);
                return Ok(contractor);
            }
            catch (SystemException exp)
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post(Model.Contractor contractor)
        {
            try
            {
               
                Contractor response = await contractorRepository.Create(contractor);
                return Ok(response);
            }
            catch (SystemException exp)
            {
                return BadRequest(exp.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(Model.Contractor contractor)
        {
            try
            {
                
                Contractor response = await contractorRepository.Update(contractor);
                return Ok(response);
            }
            catch (SystemException exp)
            {
                return BadRequest(exp.Message);
            }
        }

        [HttpGet("poi/{id}")]
        public async Task<IActionResult> GetRelationshipsByPoiId(int id)
        {
            try
            {
                ICollection<PersonCompanyRelationship> poiRelationships = await relationshipsRepository.GetContractorForPersonId(id);
                return Ok(poiRelationships);
            }
            catch (SystemException exp)
            {
                return BadRequest();
            }
        }
        [HttpGet("employees/{id}")]
        public async Task<IActionResult> GetRelationshipsByContractorId(int id)
        {
            try
            {
                ICollection<PersonCompanyRelationship> poiRelationships = await relationshipsRepository.GetEmployeesForContractor(id);
                return Ok(poiRelationships);
            }
            catch (SystemException exp)
            {
                return BadRequest();
            }
        }

        [HttpPatch("relationships")]
        public async Task<IActionResult> AddRelationship([FromBody] PersonCompanyRelationship relationShip)
        {
            try
            {
                PersonCompanyRelationship response = null;
                if (relationShip.Id == 0)
                {
                    response = await relationshipsRepository.Create(relationShip);
                }
                else
                {
                    response = await relationshipsRepository.Update(relationShip);
                }
                return Ok(response);

            }
            catch (SystemException exp)
            {
                return BadRequest();
            }
        }
    }
}
