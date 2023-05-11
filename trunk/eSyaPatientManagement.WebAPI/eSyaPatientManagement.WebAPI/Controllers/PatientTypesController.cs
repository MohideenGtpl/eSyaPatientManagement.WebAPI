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
    public class PatientTypesController : ControllerBase
    {
        private readonly IPatientTypesRepository _PatientTypesRepository;

        public PatientTypesController(IPatientTypesRepository PatientTypesRepository)
        {
            _PatientTypesRepository = PatientTypesRepository;
        }

        #region Patient Type & Category Link with Param

        [HttpGet]
        public async Task<IActionResult> GetAllPatientTypesforTreeView(int CodeType)
        {
            var rs = await _PatientTypesRepository.GetAllPatientTypesforTreeView(CodeType);
            return Ok(rs);
        }

        [HttpGet]
        public async Task<IActionResult> GetPatientCategoryInfo(int PatientTypeId, int PatientCategoryId)
        {
            var rs = await _PatientTypesRepository.GetPatientCategoryInfo(PatientTypeId, PatientCategoryId);
            return Ok(rs);
        }

        [HttpPost]
        public async Task<IActionResult> InsertPatientCategory(DO_PatientTypCategoryAttribute obj)
        {
            var rs = await _PatientTypesRepository.InsertPatientCategory(obj);
            return Ok(rs);
        }
        [HttpPost]
        public async Task<IActionResult> UpdatePatientCategory(DO_PatientTypCategoryAttribute obj)
        {
            var rs = await _PatientTypesRepository.UpdatePatientCategory(obj);
            return Ok(rs);
        }
        #endregion

        #region Patient Type & Category Business Link
        [HttpGet]
        public async Task<IActionResult> GetAllPatientCategoryBusinessLink(int businesskey)
        {
            var rs = await _PatientTypesRepository.GetAllPatientCategoryBusinessLink(businesskey);
            return Ok(rs);
        }

        [HttpPost]
        public async Task<IActionResult> InsertOrUpdatePatientCategoryBusinessLink(List<DO_PatientCategoryTypeBusinessLink> obj)
        {
            var rs = await _PatientTypesRepository.InsertOrUpdatePatientCategoryBusinessLink(obj);
            return Ok(rs);
        }
        #endregion

        #region Care Card Details

        #region Patient Type + Category – Care Card

        [HttpGet]
        public async Task<IActionResult> GetCareCardPattern(int businesskey, int PatientTypeId, int PatientCategoryId)
        {
            var rs = await _PatientTypesRepository.GetCareCardPattern(businesskey,PatientTypeId, PatientCategoryId);
            return Ok(rs);
        }

        [HttpPost]
        public async Task<IActionResult> InsertOrUpdateCareCardPattern(DO_CareCardPattern obj)
        {
            var rs = await _PatientTypesRepository.InsertOrUpdateCareCardPattern(obj);
            return Ok(rs);
        }

        [HttpGet]
        public IActionResult GetCardNumber()
        {
            var rs = _PatientTypesRepository.GetCardNumber();
            return Ok(rs);
        }
        #endregion

        #region Patient Type + Category – Document Details

        [HttpGet]
        public async Task<IActionResult> GetDocumentDetails(int businesskey, int PatientTypeId, int PatientCategoryId)
        {
            var rs = await _PatientTypesRepository.GetDocumentDetails(businesskey, PatientTypeId, PatientCategoryId);
            return Ok(rs);
        }

        [HttpPost]
        public async Task<IActionResult> InsertOrUpdateDocumentDetails(DO_DocumentDetails obj)
        {
            var rs = await _PatientTypesRepository.InsertOrUpdateDocumentDetails(obj);
            return Ok(rs);
        }

        #endregion

        #region Patient Type + – Service Type Wise – Rate Type

        [HttpGet]
        public async Task<IActionResult> GetPatientTypeCategoryServiceTypeLink(int businesskey, int PatientTypeId, int PatientCategoryId)
        {
            var rs = await _PatientTypesRepository.GetPatientTypeCategoryServiceTypeLink(businesskey, PatientTypeId, PatientCategoryId);
            return Ok(rs);
        }

        [HttpPost]
        public async Task<IActionResult> InsertOrUpdatePatientTypeCategoryServiceType(List<DO_PatientTypeCategoryServiceTypeLink> obj)
        {
            var rs = await _PatientTypesRepository.InsertOrUpdatePatientTypeCategoryServiceType(obj);
            return Ok(rs);
        }

        #endregion

        #region Patient Type + Category – Restricted Specialty
        [HttpGet]
        public async Task<IActionResult> GetPatientTypeCategorySpecialtyLink(int businesskey, int PatientTypeId, int PatientCategoryId)
        {
            var rs = await _PatientTypesRepository.GetPatientTypeCategorySpecialtyLink(businesskey, PatientTypeId, PatientCategoryId);
            return Ok(rs);
        }

        [HttpPost]
        public async Task<IActionResult> InsertOrUpdatePatientTypeCategorySpecialtyLink(List<DO_PatientTypeCategorySpecialtyLink> obj)
        {
            var rs = await _PatientTypesRepository.InsertOrUpdatePatientTypeCategorySpecialtyLink(obj);
            return Ok(rs);
        }
        #endregion

        #region Patient Type + Category – Dependent

        [HttpGet]
        public async Task<IActionResult> GetPatientTypeCategoryDependent(int businesskey, int PatientTypeId, int PatientCategoryId)
        {
            var rs = await _PatientTypesRepository.GetPatientTypeCategoryDependent(businesskey, PatientTypeId, PatientCategoryId);
            return Ok(rs);
        }

        [HttpPost]
        public async Task<IActionResult> InsertOrUpdatePatientTypeCategoryDependent(List<DO_PatientTypeCategoryDependent> obj)
        {
            var rs = await _PatientTypesRepository.InsertOrUpdatePatientTypeCategoryDependent(obj);
            return Ok(rs);
        }

        #endregion

        #endregion

        //#region  Care Card Rates

        //[HttpGet]
        //public async Task<IActionResult> GetPatientTypeCategoryCareCardRates(int businesskey)
        //{
        //    var rs = await _PatientTypesRepository.GetPatientTypeCategoryCareCardRates(businesskey);
        //    return Ok(rs);
        //}

        //[HttpPost]
        //public async Task<IActionResult> InsertOrUpdatePatientTypeCategoryCareCardRates(List<DO_PatientTypeCategoryCareCardRates> obj)
        //{
        //    var rs = await _PatientTypesRepository.InsertOrUpdatePatientTypeCategoryCareCardRates(obj);
        //    return Ok(rs);
        //}

        //#endregion
    }
}