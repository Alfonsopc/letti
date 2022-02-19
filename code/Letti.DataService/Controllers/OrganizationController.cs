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
    [Route("api/organizations")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private IOrganizationRepository organizationRepository;
        private IOrganizationRelationshipsRepository organizationRelationshipsRepository;
        public OrganizationController(IOrganizationRepository organizationRepository,IOrganizationRelationshipsRepository organizationRelationshipsRepository)
        {
            this.organizationRepository = organizationRepository;
            this.organizationRelationshipsRepository = organizationRelationshipsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                ICollection<Organization> organizations = await organizationRepository.GetAll();
                return Ok(organizations);
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
                Organization organization = await organizationRepository.GetById(id);
                return Ok(organization);
            }
            catch (SystemException exp)
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post(Model.Organization organization)
        {
            try
            {             
                Organization response=await organizationRepository.Create(organization);
                return Ok(response);
            }
            catch (SystemException exp)
            {
                return BadRequest(exp.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(Model.Organization organization)
        {
            try
            {
                Organization response = await organizationRepository.Update(organization);
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
                ICollection<PersonOrganizationRelationship> poiRelationships = await organizationRelationshipsRepository.GetOrganizationRelationshipsForPerson(id);
                return Ok(poiRelationships);
            }
            catch (SystemException exp)
            {
                return BadRequest();
            }
        }
        [HttpGet("employees/{id}")]
        public async Task<IActionResult> GetEmployeesByOrganizationId(int id)
        {
            try
            {
                ICollection<PersonOrganizationRelationship> poiRelationships = await organizationRelationshipsRepository.GetRelationshipsForOrganization(id);
                return Ok(poiRelationships);
            }
            catch (SystemException exp)
            {
                return BadRequest();
            }
        }
        [HttpPatch("personnelrelationships")]
        public async Task<IActionResult> AddRelationship([FromBody] PersonOrganizationRelationship relationShip)
        {
            try
            {
                PersonOrganizationRelationship response = null;
                if (relationShip.Id == 0)
                {
                    response = await organizationRelationshipsRepository.Create(relationShip);
                }
                else
                {
                    response = await organizationRelationshipsRepository.Update(relationShip);
                }
                return Ok(response);

            }
            catch (Exception exp)
            {
                return BadRequest();
            }
        }
    }
}
