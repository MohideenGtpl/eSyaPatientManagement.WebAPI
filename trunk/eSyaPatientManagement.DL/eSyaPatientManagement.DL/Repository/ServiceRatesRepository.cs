using eSyaPatientManagement.DL.Entities;
using eSyaPatientManagement.DL.Utility;
using eSyaPatientManagement.DO;
using eSyaPatientManagement.DO.StaticVariables;
using eSyaPatientManagement.IF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSyaPatientManagement.DL.Repository
{
    public class ServiceRatesRepository : IServiceRatesRepository
    {
        private eSyaEnterpriseContext _context { get; set; }
        public ServiceRatesRepository(eSyaEnterpriseContext context)
        {
            _context = context;
        }

        public async Task<List<DO_ServiceRates>> GetOpConsultationServiceRate(
            int businessKey, string episodeId,
            int clinicId, int consultationId,
            int specialtyId, int doctorId,
            int rateType, string currencyCode, long uhid)
        {

            try
            {
                List<DO_ServiceRates> L_serviceRate = new List<DO_ServiceRates>();

                var regnType = "N";
                if (uhid > 0)
                    regnType = "R";
                var regnType_Service = await _context.GtEforvl.Where(w => w.RegnType == regnType && w.ActiveStatus).FirstOrDefaultAsync();

                if (regnType == "R")
                {
                    var regnValidty = await _context.GtEfoppp.Where(w => w.BusinessKey == businessKey && w.RUhid == uhid && w.ActiveStatus).FirstOrDefaultAsync();
                    if (regnValidty != null && regnValidty.RegistrationChargesValidTill.HasValue)
                        if (regnValidty.RegistrationChargesValidTill.Value.Date < System.DateTime.Now.Date)
                        {
                            regnType_Service = await _context.GtEforvl.Where(w => w.RegnType == "N" && w.ActiveStatus).FirstOrDefaultAsync();
                        }
                }

                if (regnType_Service != null)
                {
                    var regn_Rate = await _context.GtEssrbr
                         .Join(_context.GtEssrms,
                            r => r.ServiceId,
                            s => s.ServiceId,
                            (r, s) => new { r, s })
                        .Where(w => w.r.BusinessKey == businessKey && w.r.ServiceId == regnType_Service.ServiceId
                           && w.r.RateType == rateType && w.r.CurrencyCode == currencyCode && DateTime.Now.Date >= w.r.EffectiveDate.Date
                           && DateTime.Now.Date <= (w.r.EffectiveTill ?? DateTime.Now).Date
                           && w.r.ActiveStatus).FirstOrDefaultAsync();

                    L_serviceRate.Add(new DO_ServiceRates
                    {
                        ServiceClassId = regn_Rate.s.ServiceClassId,
                        ServiceId = regn_Rate.s.ServiceId,
                        ServiceDesc = regn_Rate.s.ServiceDesc,
                        ServiceRule = "F",
                        ServiceRate = regn_Rate.r.OpbaseRate
                    });
                }

                if (episodeId != "P" && episodeId != "O")
                {
                    var sr = await _context.GtEsclsl
                        .Join(_context.GtEssrms,
                            c => c.ServiceId,
                            s => s.ServiceId,
                            (c, s) => new { c, s })
                        .Where(w => w.c.BusinessKey == businessKey
                            && w.c.ClinicId == clinicId && w.c.ConsultationId == consultationId
                            && w.c.ActiveStatus && w.s.ActiveStatus)
                        .FirstOrDefaultAsync();


                    var doctor_sr = await _context.GtEscdst.Where(w => w.BusinessKey == businessKey
                                && w.ServiceId == sr.c.ServiceId && w.DoctorId == doctorId
                                && w.CurrencyCode == currencyCode
                                && DateTime.Now.Date >= w.EffectiveDate.Date
                                && DateTime.Now.Date <= (w.EffectiveTill ?? DateTime.Now).Date
                                && w.ActiveStatus).FirstOrDefaultAsync();
                    if (doctor_sr != null)
                    {
                        L_serviceRate.Add(new DO_ServiceRates
                        {
                            ServiceClassId = sr.s.ServiceClassId,
                            ServiceId = sr.c.ServiceId,
                            ServiceDesc = sr.s.ServiceDesc,
                            ServiceRule = "F",
                            ServiceRate = doctor_sr.Tariff
                        });
                    }
                    else
                    {
                        var specialty_sr = await _context.GtEscsst.Where(w => w.BusinessKey == businessKey
                               && w.ServiceId == sr.c.ServiceId && w.SpecialtyId == specialtyId
                               && w.CurrencyCode == currencyCode
                               && DateTime.Now.Date >= w.EffectiveDate.Date
                               && DateTime.Now.Date <= (w.EffectiveTill ?? DateTime.Now).Date
                               && w.ActiveStatus).FirstOrDefaultAsync();
                        if (doctor_sr != null)
                        {
                            L_serviceRate.Add(new DO_ServiceRates
                            {
                                ServiceClassId = sr.s.ServiceClassId,
                                ServiceId = sr.c.ServiceId,
                                ServiceDesc = sr.s.ServiceDesc,
                                ServiceRule = "F",
                                ServiceRate = doctor_sr.Tariff
                            });
                        }
                        else
                        {
                            var clinic_sr = await _context.GtEsclst.Where(w => w.BusinessKey == businessKey
                                    && w.ClinicId == clinicId && w.ConsultationId == consultationId
                                    && w.ServiceId == sr.c.ServiceId
                                    && w.CurrencyCode == currencyCode
                                    && w.RateType == rateType
                                    && DateTime.Now.Date >= w.EffectiveDate.Date
                                    && DateTime.Now.Date <= (w.EffectiveTill ?? DateTime.Now).Date
                                    && w.ActiveStatus).FirstOrDefaultAsync();
                            if (clinic_sr != null)
                            {
                                L_serviceRate.Add(new DO_ServiceRates
                                {
                                    ServiceClassId = sr.s.ServiceClassId,
                                    ServiceId = sr.c.ServiceId,
                                    ServiceDesc = sr.s.ServiceDesc,
                                    ServiceRule = "F",
                                    ServiceRate = clinic_sr.Tariff
                                });
                            }
                        }
                    }
                }

                return L_serviceRate;
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

        public async Task<List<DO_ServiceRates>> GetServiceList(int businessKey)
        {

            var sr = await _context.GtEssrms
                .Join(_context.GtEssrbl,
                    s => new { s.ServiceId },
                    b => new { b.ServiceId },
                    (s, b) => new { s, b })
                .Join(_context.GtEssrcl,
                    sb => new { sb.s.ServiceClassId },
                    c => new { c.ServiceClassId },
                    (sb, c) => new { sb, c })
               .Join(_context.GtEssrgr,
                    sbc => new { sbc.c.ServiceGroupId },
                    g => new { g.ServiceGroupId },
                    (sbc, g) => new { sbc, g })
                .Where(w => w.sbc.sb.b.BusinessKey == businessKey
                        && _context.GtEspasm.Any(a => a.ServiceId == w.sbc.sb.b.ServiceId
                                    && a.ParameterId == AppParameter.Service_ApplicableToOpd
                                    && a.ParmAction && a.ActiveStatus)
                        && w.sbc.sb.s.ActiveStatus && w.sbc.sb.b.ActiveStatus
                        && w.sbc.c.ActiveStatus && w.g.ActiveStatus)
                .Select(r => new DO_ServiceRates
                {
                    ServiceTypeId = r.g.ServiceTypeId,
                    ServiceId = r.sbc.sb.s.ServiceId,
                    ServiceDesc = r.sbc.sb.s.ServiceDesc,
                    ServiceProviderType = _context.GtEspasm.Any(w => w.ServiceId == r.sbc.sb.s.ServiceId
                         && w.ParameterId == AppParameter.Service_DoctorProvider
                         && w.ParmAction && w.ActiveStatus) ? "D" : "N"
                })
                .ToListAsync();

            return sr;
        }

        public async Task<DO_ServiceRates> GetServiceRatesForOpBilling(int businessKey, int serviceId, int rateType, string currencyCode)
        {
            var sr = await _context.GtEssrms
                .Join(_context.GtEssrbl,
                    s => new { s.ServiceId },
                    b => new { b.ServiceId },
                    (s, b) => new { s, b })
                .Join(_context.GtEssrbr
                    .Where(w => w.RateType == rateType && w.CurrencyCode == currencyCode
                        && w.ActiveStatus
                        && DateTime.Now.Date >= w.EffectiveDate.Date
                        && DateTime.Now.Date <= (w.EffectiveTill ?? DateTime.Now.Date).Date),
                    sb => new { sb.b.BusinessKey, sb.b.ServiceId },
                    r => new { r.BusinessKey, r.ServiceId },
                    (sb, r) => new { sb, r })
                .Where(w => w.sb.b.BusinessKey == businessKey && w.sb.s.ServiceId == serviceId
                        && w.sb.s.ActiveStatus && w.sb.b.ActiveStatus && w.r.ActiveStatus)
                .Select(r => new DO_ServiceRates
                {
                    ServiceId = r.sb.s.ServiceId,
                    ServiceDesc = r.sb.s.ServiceDesc,
                    ServiceRule = r.r.ServiceRule,
                    ServiceDiscount = 0,
                    ServiceRate = r.r.OpbaseRate
                })
                .FirstOrDefaultAsync();

            return sr;
        }

        public async Task<List<DO_ServiceRates>> GetOpRegisterationRate(
           int businessKey,
           string regType,
           int uhid)
        {
            var reg_rate = new List<DO_ServiceRates>();
            var ser = new GtEforvl();
            if (uhid == 0)
            {
                ser = _context.GtEforvl.Where(w => w.RegnType == "N" && w.ActiveStatus).FirstOrDefault();
            }
            else
            {
                var reg_status = await _context.GtEfoppp.Where(w => w.BusinessKey == businessKey && w.RUhid == uhid).FirstOrDefaultAsync();
                if (reg_status.RegistrationChargesValidTill > System.DateTime.Now)
                {
                    ser = _context.GtEforvl.Where(w => w.RegnType == "R" && w.ActiveStatus


                    ).FirstOrDefault();
                }
            }
            if (ser != null)
            {
                var sr = await _context.GtEssrbr
                               .Join(_context.GtEssrms,
                                   r => r.ServiceId,
                                   s => s.ServiceId,
                                   (r, s) => new { r, s })
                               .Where(w => w.r.BusinessKey == businessKey
                                   && w.r.ServiceId == ser.ServiceId
                                   && w.r.ActiveStatus && w.s.ActiveStatus)
                               .FirstOrDefaultAsync();
                if (sr != null)
                {
                    reg_rate.Add(new DO_ServiceRates
                    {
                        ServiceId = ser.ServiceId,
                        ServiceDesc = sr.s.ServiceDesc,
                        ServiceRule = "F",
                        ServiceRate = sr.r.OpbaseRate
                    });
                }
            }


            return reg_rate;
        }
    }
}
