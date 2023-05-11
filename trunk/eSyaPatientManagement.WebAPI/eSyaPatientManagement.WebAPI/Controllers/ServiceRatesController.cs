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
    public class ServiceRatesController : ControllerBase
    {
        private readonly IServiceRatesRepository _serviceRatesRepository;

        public ServiceRatesController(IServiceRatesRepository serviceRatesRepository)
        {
            _serviceRatesRepository = serviceRatesRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetOpConsultationServiceRate(
            int businessKey,
            string episodeId,
            int clinicId, int consultationId,
            int specialtyId, int doctorId,
            int rateType, string currencyCode, long uhid)
        {
            var rs = await _serviceRatesRepository.GetOpConsultationServiceRate(
            businessKey, episodeId,
            clinicId, consultationId,
            specialtyId, doctorId,
            rateType, currencyCode, uhid);
            return Ok(rs);
        }

        [HttpGet]
        public async Task<IActionResult> GetServiceList(int businessKey)
        {
            var rs = await _serviceRatesRepository.GetServiceList(businessKey);
            return Ok(rs);
        }

        [HttpGet]
        public async Task<IActionResult> GetServiceRatesForOpBilling(int businessKey, int serviceId, int rateType, string currencyCode)
        {
            var rs = await _serviceRatesRepository.GetServiceRatesForOpBilling(businessKey, serviceId, rateType, currencyCode);
            return Ok(rs);
        }
    }
}