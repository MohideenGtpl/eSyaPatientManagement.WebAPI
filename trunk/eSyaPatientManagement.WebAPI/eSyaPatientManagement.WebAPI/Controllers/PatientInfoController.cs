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
    public class PatientInfoController : ControllerBase
    {
        private readonly IPatientInfoRepository _patientInfoRepository;

        public PatientInfoController(IPatientInfoRepository patientInfoRepository)
        {
            _patientInfoRepository = patientInfoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetSearchPatient(string searchText)
        {
            var rs = await _patientInfoRepository.GetSearchPatient(searchText);
            return Ok(rs);
        }

        [HttpGet]
        public async Task<IActionResult> GetPatientInfoRegistrationByMobileNo(string mobileNumber)
        {
            var rs = await _patientInfoRepository.GetPatientInfoRegistrationByMobileNo(mobileNumber);
            return Ok(rs);
        }

        [HttpGet]
        public async Task<IActionResult> GetPatientProfileByUHID(long uhid)
        {
            var rs = await _patientInfoRepository.GetPatientProfileByUHID(uhid);
            return Ok(rs);
        }

        [HttpGet]
        public async Task<IActionResult> getPatientbookingInfo(int businessKey, long appKey)
        {
            var rs = await _patientInfoRepository.getPatientbookingInfo(businessKey,appKey);
            return Ok(rs);
        }

        [HttpGet]
        public async Task<IActionResult> GetPatientSearch(string searchText)
        {
            var rs = await _patientInfoRepository.GetPatientSearch(searchText);
            return Ok(rs);
        }
    }
}