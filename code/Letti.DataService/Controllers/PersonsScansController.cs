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
    [Route("api/personscans")]
    [ApiController]
    public class PersonsScansController : ControllerBase
    {
        private readonly IPoiScanningRepository poiScanningRepository;
        public PersonsScansController(IPoiScanningRepository poiScanningRepository)
        {
            this.poiScanningRepository = poiScanningRepository;
        }
        [HttpGet("next")]
        public async Task<IActionResult> Get()
        {
            try
            {
                PersonOfInterest contractor = await poiScanningRepository.GetNext();
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
                await poiScanningRepository.MarkAsScanned(contractorid);
                return Ok();
            }
            catch (SystemException exp)
            {
                return BadRequest();
            }
        }
    }
}
