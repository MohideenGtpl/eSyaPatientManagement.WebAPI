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
    public class MasterController : ControllerBase
    {
        private readonly IMasterRepository _masterRepository;

        public MasterController(IMasterRepository masterRepository)
        {
            _masterRepository = masterRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetISDCodes()
        {
            var rs = await _masterRepository.GetISDCodes();
            return Ok(rs);
        }

        [HttpGet]
        public async Task<IActionResult> GetStateList(int isdCode)
        {
            var rs = await _masterRepository.GetStateList(isdCode);
            return Ok(rs);
        }

        [HttpGet]
        public async Task<IActionResult> GetCityList(int isdCode, int stateCode)
        {
            var rs = await _masterRepository.GetCityList(isdCode, stateCode);
            return Ok(rs);
        }

        [HttpGet]
        public async Task<IActionResult> GetAreaList(int isdCode, int stateCode, int cityCode, string pincode)
        {
            var rs = await _masterRepository.GetAreaList(isdCode, stateCode, cityCode, pincode);
            return Ok(rs);
        }

        [HttpGet]
        public async Task<IActionResult> GetCountryAreaDetailsByPincode(int isdCode, string pincode)
        {
            var activities = await _masterRepository.GetCountryAreaDetailsByPincode(isdCode, pincode);
            return Ok(activities);
        }

        [HttpGet]
        public async Task<IActionResult> GetEpisodeType()
        {
            var rs = await _masterRepository.GetEpisodeType();
            return Ok(rs);
        }

        [HttpGet]
        public async Task<IActionResult> GetPatientType()
        {
            var rs = await _masterRepository.GetPatientType();
            return Ok(rs);
        }

        [HttpGet]
        public async Task<IActionResult> GetPatientCategory(int patientTypeId)
        {
            var rs = await _masterRepository.GetPatientCategory(patientTypeId);
            return Ok(rs);
        }

        [HttpGet]
        public async Task<IActionResult> GetBillSuspendType()
        {
            var rs = await _masterRepository.GetBillSuspendType();
            return Ok(rs);
        }

        [HttpGet]
        public async Task<IActionResult> GetServiceClassesByTypeID(int serviceType)
        {
            var rs = await _masterRepository.GetServiceClassesByTypeID(serviceType);
            return Ok(rs);
        }

        [HttpGet]
        public async Task<IActionResult> GetServiceCodesByClassID(int serviceType, int serviceClass)
        {
            var rs = await _masterRepository.GetServiceCodesByClassID(serviceType,serviceClass);
            return Ok(rs);
        }

        [HttpGet]
        public async Task<IActionResult> GetDrugMaster()
        {
            var rs = await _masterRepository.GetDrugMaster();
            return Ok(rs);
        }

        [HttpGet]
        public async Task<IActionResult> GetDoctorByID(int doctorId)
        {
            var rs = await _masterRepository.GetDoctorByID(doctorId);
            return Ok(rs);
        }

        [HttpPost]
        public async Task<IActionResult> GetApplicationCodesByCodeTypeList(List<int> l_codeType)
        {
            var rs = await _masterRepository.GetApplicationCodesByCodeTypeList(l_codeType);
            return Ok(rs);
        }

        [HttpGet]
        public async Task<IActionResult> GetActiveCurrencies()
        {
            var rs = await _masterRepository.GetActiveCurrencies();
            return Ok(rs);
        }
    }
}