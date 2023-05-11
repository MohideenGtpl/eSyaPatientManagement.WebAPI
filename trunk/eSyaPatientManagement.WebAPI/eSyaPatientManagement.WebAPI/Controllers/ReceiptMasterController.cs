using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eSyaPatientManagement.IF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eSyaPatientManagement.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReceiptMasterController : ControllerBase
    {
        private readonly IReceiptMasterRepository _receiptMasterRepository;

        public ReceiptMasterController(IReceiptMasterRepository receiptMasterRepository)
        {
            _receiptMasterRepository = receiptMasterRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetPaymentMode()
        {
            var rs = await _receiptMasterRepository.GetPaymentMode();
            return Ok(rs);
        }

        [HttpGet]
        public async Task<IActionResult> GetPaymentModeCategoryById(int paymentModeId)
        {
            var rs = await _receiptMasterRepository.GetPaymentModeCategoryById(paymentModeId);
            return Ok(rs);
        }
    }
}