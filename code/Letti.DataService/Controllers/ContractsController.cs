using Letti.Model;
using Letti.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Letti.DataService.Controllers
{
    [Route("api/contracts")]
    [ApiController]
    public class ContractsController : ControllerBase
    {
        private IContractsService contractsService;
        public ContractsController(IContractsService contractsService)
        {
            this.contractsService = contractsService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Contract contract)
        {
            try
            {

                Contract response = await contractsService.Create(contract);
                return Ok(response);
            }
            catch (SystemException exp)
            {
                return BadRequest(exp.Message);
            }
        }

        [HttpGet("organization/{id}")]
        public async Task<IActionResult> GetContractsForOrganizationById(int id)
        {
            try
            {
                ICollection<Contract> contracts = await contractsService.GetContractsByOrganizationId(id);
                return Ok(contracts);
            }
            catch (SystemException exp)
            {
                return BadRequest();
            }
        }

        [HttpGet("contractor/{id}")]
        public async Task<IActionResult> GetContractsForContractorById(int id)
        {
            try
            {
                ICollection<Contract> contracts = await contractsService.GetContractsByContractorId(id);
                return Ok(contracts);
            }
            catch (SystemException exp)
            {
                return BadRequest();
            }
        }
    }
}
