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
    [Route("api/contractorscans")]
    [ApiController]
    public class ContractorScansController : ControllerBase
    {
        private readonly IContractorScansRepository contractorScansRepository;
        public ContractorScansController(IContractorScansRepository contractorScansRepository)
        {
            this.contractorScansRepository = contractorScansRepository;
        }

        [HttpGet("next")]
        public async Task<IActionResult> Get()
        {
            try
            {
                Contractor contractor = await contractorScansRepository.GetNext();
                return Ok(contractor);
            }
            catch (SystemException exp)
            {
                return BadRequest();
            }
        }
        [HttpPut("{contractorid}")]
        public async Task<IActionResult> UpdateScan(int contractorid)
        {
            try
            {
                await contractorScansRepository.MarkAsScanned(contractorid);
                return Ok();
            }
            catch (SystemException exp)
            {
                return BadRequest();
            }
        }
    }
}
