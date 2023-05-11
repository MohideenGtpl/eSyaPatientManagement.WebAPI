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
    public class SuspendOpBillController : ControllerBase
    {
        private readonly ISuspendOpBillingRepository _suspendOpBillingRepository;

        public SuspendOpBillController(ISuspendOpBillingRepository suspendOpBillingRepository)
        {
            _suspendOpBillingRepository = suspendOpBillingRepository;
        }

        [HttpPost]
        public async Task<IActionResult> SuspendOpBill(DO_PatientBillHeader obj_Bill)
        {
            var rs = await _suspendOpBillingRepository.SuspendOpBill(obj_Bill);
            return Ok(rs);
        }

        [HttpGet]
        public async Task<IActionResult> GetSuspendOpBillListForConfirmation(int businessKey, DateTime billFromDate, DateTime billTillDate)
        {
            var rs = await _suspendOpBillingRepository.GetSuspendOpBillListForConfirmation(businessKey, billFromDate, billTillDate);
            return Ok(rs);
        }

        [HttpGet]
        public async Task<IActionResult> GetSuspendOpBillHeader(int businessKey, long suspendBillKey)
        {
            var rs = await _suspendOpBillingRepository.GetSuspendOpBillHeader(businessKey, suspendBillKey);
            return Ok(rs);
        }

        [HttpGet]
        public async Task<IActionResult> GetSuspendOpBillDetails(int businessKey, long suspendBillKey)
        {
            var rs = await _suspendOpBillingRepository.GetSuspendOpBillDetails(businessKey, suspendBillKey);
            return Ok(rs);
        }
    }
}