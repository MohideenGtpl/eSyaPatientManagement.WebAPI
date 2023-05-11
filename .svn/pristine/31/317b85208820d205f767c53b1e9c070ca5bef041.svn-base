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
    public class DoctorDeskController : ControllerBase
    {
        private readonly IDoctorDeskRepository _doctorDeskRepository;

        public DoctorDeskController(IDoctorDeskRepository doctorDeskRepository)
        {
            _doctorDeskRepository = doctorDeskRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAppointmentDetailByDate(int businessKey, int doctorId, DateTime startDate, DateTime endDate)
        {
            var rs = await _doctorDeskRepository.GetAppointmentDetailByDate(businessKey,doctorId,startDate,endDate);
            return Ok(rs);
        }

        [HttpGet]
        public async Task<IActionResult> GetAppointmentDetailByUHID(long businessKey, long uhid)
        {
            var rs = await _doctorDeskRepository.GetAppointmentDetailByUHID(businessKey,uhid);
            return Ok(rs);
        }


        [HttpGet]
        public async Task<IActionResult> GetDoctorScheduleList(int businessKey, int doctorId)
        {
            var rs = await _doctorDeskRepository.GetDoctorScheduleList(businessKey, doctorId);
            return Ok(rs);
        }

        [HttpGet]
        public async Task<IActionResult> GetLastOPNumber(long businessKey, long uhid)
        {
            var rs = await _doctorDeskRepository.GetLastOPNumber(businessKey, uhid);
            return Ok(rs);
        }
    }
}