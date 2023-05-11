﻿using System;
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
    public class OpBillingController : ControllerBase
    {
        private readonly IOpBillingRepository _opBillingRepository;

        public OpBillingController(IOpBillingRepository opBillingRepository)
        {
            _opBillingRepository = opBillingRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetOpDoctorPrescribedServices(int businessKey, long uhid, long opNumber, int rateType, string currencyCode)
        {
            var rs = await _opBillingRepository.GetOpDoctorPrescribedServices(businessKey, uhid, opNumber, rateType, currencyCode);
            return Ok(rs);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOpBill(DO_PatientBillHeader obj_Bill)
        {
            List<DO_PaymentReceipt> obj_PR = obj_Bill.l_PaymentReceipt;
            var rs = await _opBillingRepository.CreateOpBill(obj_Bill, obj_PR);
            return Ok(rs);
        }

        [HttpGet]
        public async Task<IActionResult> GetOpBillList(int businessKey, DateTime billFromDate, DateTime billTillDate)
        {
            var rs = await _opBillingRepository.GetOpBillList(businessKey, billFromDate, billTillDate);
            return Ok(rs);
        }
        [HttpGet]
        public async Task<IActionResult> GetOpBillListbySearchCriteria(int businessKey, DateTime billFromDate, DateTime billTillDate, long? uhid, string patientname)
        {
            var rs = await _opBillingRepository.GetOpBillListbySearchCriteria(businessKey, billFromDate, billTillDate, uhid, patientname);
            return Ok(rs);
        }
    }
}