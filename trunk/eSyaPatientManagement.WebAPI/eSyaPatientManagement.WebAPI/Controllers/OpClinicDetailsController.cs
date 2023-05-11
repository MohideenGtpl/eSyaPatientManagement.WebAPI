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
    public class OpClinicDetailsController : ControllerBase
    {
        private readonly IOpClinicDetailsRepository _opClinicDetailsRepository;

        public OpClinicDetailsController(IOpClinicDetailsRepository opClinicDetailsRepository)
        {
            _opClinicDetailsRepository = opClinicDetailsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetPatientFODeskListByMobile(string mobileNumber)
        {
            var ap = await _opClinicDetailsRepository.GetAppointmentBookedByMobile(mobileNumber);
            var pr = await _opClinicDetailsRepository.GetPreRegistrationByMobile(mobileNumber);
            var pm = await _opClinicDetailsRepository.GetRegisteredPatientByMobileNo(mobileNumber);
            return Ok(new DO_PatientFODeskList{ L_Appointment = ap, L_PreRegistered = pr, L_RegisteredPatient = pm });
        }

        [HttpGet]
        public async Task<IActionResult> GetClinicTypes(int businessKey)
        {
            var rs = await _opClinicDetailsRepository.GetClinicTypes(businessKey);
            return Ok(rs);
        }

        [HttpGet]
        public async Task<IActionResult> GetSpecialty(int businessKey)
        {
            var rs = await _opClinicDetailsRepository.GetSpecialty(businessKey);
            return Ok(rs);
        }

        [HttpGet]
        public async Task<IActionResult> GetDoctorScheduleListByClinicTypeSpecialtyDate(int businessKey,
           int clinicTypeId, int consultationTypeId,
           int specialtyId, DateTime scheduleDate)
        {
            var rs = await _opClinicDetailsRepository.GetDoctorScheduleListByClinicTypeSpecialtyDate(businessKey,
           clinicTypeId, consultationTypeId,
           specialtyId, scheduleDate);
            return Ok(rs);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDoctorList(int businessKey)
        {
            var rs = await _opClinicDetailsRepository.GetAllDoctorList(businessKey);
            return Ok(rs);
        }

    }
}