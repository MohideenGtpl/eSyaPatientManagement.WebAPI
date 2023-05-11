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
    public class CareCardRatesController : ControllerBase
    {
        private readonly ICareCardRatesRepository _careCardRatesRepository;

        public CareCardRatesController(ICareCardRatesRepository careCardRatesRepository)
        {
            _careCardRatesRepository = careCardRatesRepository;
        }

        #region Care Card Rates

        [HttpGet]
        public async Task<IActionResult> GetPatientTypebyBusinesskey(int businesskey)
        {
            var rs = await _careCardRatesRepository.GetPatientTypebyBusinesskey(businesskey);
            return Ok(rs);
        }

        [HttpGet]
        public async Task<IActionResult> GetPatientCategoriesbyBusinesskeyAndPatientType(int businesskey, int PatientTypeId)
        {
            var rs = await _careCardRatesRepository.GetPatientCategoriesbyBusinesskeyAndPatientType(businesskey, PatientTypeId);
            return Ok(rs);
        }

        [HttpGet]
        public async Task<IActionResult> GetCareCardRates(int businesskey, int PatientTypeId, int PatientCategoryId)
        {
            var rs = await _careCardRatesRepository.GetCareCardRates(businesskey, PatientTypeId, PatientCategoryId);
            return Ok(rs);
        }
        [HttpPost]
        public async Task<IActionResult> InsertIntoCareCardRates(DO_CareCardRates obj)
        {
            var rs = await _careCardRatesRepository.InsertIntoCareCardRates(obj);
            return Ok(rs);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateIntoCareCardRates(DO_CareCardRates obj)
        {
            var rs = await _careCardRatesRepository.UpdateIntoCareCardRates(obj);
            return Ok(rs);
        }

        [HttpPost]
        public async Task<IActionResult> InsertOrUpdateCareCardRates(DO_CareCardRates obj)
        {
            var rs = await _careCardRatesRepository.InsertOrUpdateCareCardRates(obj);
            return Ok(rs);
        }

        [HttpGet]
        public async Task<IActionResult> GetPreviousCareCardRates(int businesskey, int PatientTypeId, int PatientCategoryId)
        {
            var rs = await _careCardRatesRepository.GetPreviousCareCardRates(businesskey, PatientTypeId, PatientCategoryId);
            return Ok(rs);
        }
        #endregion
    }
}