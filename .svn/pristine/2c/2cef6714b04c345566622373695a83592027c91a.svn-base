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
    public class ClinicalFormsController : ControllerBase
    {
        private readonly IClinicalFormsRepository _clinicalFormsRepository;
        private readonly IPatientInfoRepository _patientInfoRepository;
        private readonly IMasterRepository _masterRepository;

        public ClinicalFormsController(IClinicalFormsRepository clinicalFormsRepository, IPatientInfoRepository patientInfoRepository, IMasterRepository masterRepository)
        {
            _clinicalFormsRepository = clinicalFormsRepository;
            _patientInfoRepository = patientInfoRepository;
            _masterRepository = masterRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetClinicalInformation(int businessKey, long UHID, long vNumber, string clType)
        {
            var rs = await _clinicalFormsRepository.GetClinicalInformation(businessKey,UHID,vNumber,clType);
            return Ok(rs);
        }

        [HttpGet]
        public async Task<IActionResult> GetPatientByUHID(int businessKey, long uhid)
        {
            var rs = await _patientInfoRepository.GetPatientByUHID(businessKey, uhid);
            return Ok(rs);
        }

        [HttpPost]
        public async Task<IActionResult> InsertIntoClinicalInformation(DO_ClinicalInformation obj)
        {
            var rs = await _clinicalFormsRepository.InsertIntoClinicalInformation(obj);
            return Ok(rs);
        }

        [HttpGet]
        public async Task<IActionResult> GetInformationValueView(int businessKey, long UHID, long vNumber, string clType)
        {
            var rs = await _clinicalFormsRepository.GetInformationValueView(businessKey, UHID, vNumber, clType);
            return Ok(rs);
        }

        [HttpPost]
        public async Task<IActionResult> InsertPatientClinicalInformation(DO_ClinicalInformation obj)
        {
            var rs = await _clinicalFormsRepository.InsertPatientClinicalInformation(obj);
            return Ok(rs);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePatientClinicalInformation(DO_ClinicalInformation obj)
        {
            var rs = await _clinicalFormsRepository.UpdatePatientClinicalInformation(obj);
            return Ok(rs);
        }

        [HttpGet]
        public async Task<IActionResult> GetClinicalInformationValueByTransaction(int businessKey, long UHID, long vNumber, int transactionID)
        {
            var rs = await _clinicalFormsRepository.GetClinicalInformationValueByTransaction(businessKey, UHID, vNumber, transactionID);
            return Ok(rs);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePatientClinicalInformation(DO_ClinicalInformation obj)
        {
            var rs = await _clinicalFormsRepository.DeletePatientClinicalInformation(obj);
            return Ok(rs);
        }
    }
}