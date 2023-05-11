using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eSyaPatientManagement.DO;
using eSyaPatientManagement.IF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eSyaPatientManagement.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OpRegistrationBillingController : ControllerBase
    {
        private readonly IOpRegistrationBillingRepository _opRegistrationBillingRepository;

        public OpRegistrationBillingController(IOpRegistrationBillingRepository opRegistrationBillingRepository)
        {
            _opRegistrationBillingRepository = opRegistrationBillingRepository;
        }

        [HttpPost]
        public async Task<IActionResult> InsertOPRegistrationVisit(DO_OPRegistrationVisit obj)
        {
            var rs = await _opRegistrationBillingRepository.InsertOPRegistrationVisit(obj);
            return Ok(rs);
        }

       
    }
}