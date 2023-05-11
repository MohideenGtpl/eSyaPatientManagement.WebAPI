using eSyaPatientManagement.DL.Entities;
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
    public class MasterRepository : IMasterRepository
    {
        private eSyaEnterpriseContext _context { get; set; }
        public MasterRepository(eSyaEnterpriseContext context)
        {
            _context = context;
        }

        public async Task<List<DO_IsdCodes>> GetISDCodes()
        {
            var pf = _context.GtEccncd
               .Where(w => w.ActiveStatus)
               .Select(s => new DO_IsdCodes
               {
                   IsdCode = s.Isdcode,
                   CountryCode = s.CountryCode,
                   CountryName = s.CountryName,
                   CurrencyCode = s.CurrencyCode,
                   MobileNumberPattern = s.MobileNumberPattern,
                   Nationality = s.Nationality
               })
               .ToListAsync();
            return await pf;
        }

        public async Task<List<DO_Place>> GetStateList(int isdCode)
        {
            var pf = _context.GtEccnpl
               .Where(w => w.ActiveStatus && w.PlaceType == PlaceTypeValues.State && w.Isdcode == isdCode)
               .Select(s => new DO_Place
               {
                   IsdCode = s.Isdcode,
                   PlaceId = s.PlaceId,
                   PlaceName = s.PlaceName
               })
               .ToListAsync();
            return await pf;
        }

        public async Task<List<DO_Place>> GetCityList(int isdCode, int stateCode)
        {
            var pf = _context.GtAddrin
                .Join(_context.GtEccnpl.Where(w => w.PlaceType == PlaceTypeValues.City && w.ActiveStatus),
                    a => new { a.CityCode },
                    c => new { CityCode = c.PlaceId },
                    (a, c) => new { a, c })
               .Where(w => w.a.ActiveStatus && w.a.StateCode == stateCode)
               .Select(s => new DO_Place
               {
                   IsdCode = s.a.Isdcode,
                   PlaceId = s.c.PlaceId,
                   PlaceName = s.c.PlaceName
               })
               .Distinct()
               .ToListAsync();
            return await pf;
        }

        public async Task<List<DO_AddressIN>> GetAreaList(int isdCode, int stateCode, int cityCode, string pincode)
        {
            var pf = _context.GtAddrin
               .Where(w => w.ActiveStatus
                    && w.Isdcode == isdCode
                    && w.StateCode == stateCode
                    && w.CityCode == cityCode
                    && (w.Pincode == pincode || pincode == null))
               .Select(s => new DO_AddressIN
               {
                   IsdCode = s.Isdcode,
                   AreaCode = s.AreaCode,
                   AreaName = s.AreaName,
                   StateCode = s.StateCode,
                   CityCode = s.CityCode,
                   District = s.District,
                   Pincode = s.Pincode
               })
               .ToListAsync();
            return await pf;
        }

        public async Task<List<DO_AddressIN>> GetCountryAreaDetailsByPincode(int isdCode, string pincode)
        {
            try
            {

                var ds = _context.GtAddrin
                    .Where(w => w.Isdcode == isdCode
                            && w.Pincode == pincode
                            && w.ActiveStatus)
                    .Select(r => new DO_AddressIN
                    {
                        AreaCode = r.AreaCode,
                        AreaName = r.AreaName,
                        StateCode = r.StateCode,
                        CityCode = r.CityCode,
                        Pincode = r.Pincode,
                    }).OrderBy(o => o.AreaName).ToListAsync();

                return await ds;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DO_EpisodeType>> GetEpisodeType()
        {
            var ep = await _context.GtEfbe01
                .Where(w => w.ActiveStatus)
                .Select(x => new DO_EpisodeType
                {
                    EpisodeId = x.EpisodeId,
                    EpisodeDesc = x.EpisodeDesc,
                }).OrderBy(o => o.Sequence).ToListAsync();

            return ep;
        }

        public async Task<List<DO_PatientType>> GetPatientType()
        {
            var pt = _context.GtEcapcd
               .Where(w => w.CodeType == CodeTypeValues.PatientType && w.ActiveStatus)
               .Select(s => new DO_PatientType
               {
                   PatientTypeID = s.ApplicationCode,
                   PatientTypeDesc = s.CodeDesc
               })
               .ToListAsync();
            return await pt;
        }

        public async Task<List<DO_PatientCategory>> GetPatientCategory(int patientTypeId)
        {
            var pt = _context.GtEapcms
                .Join(_context.GtEcapcd.Where(w => w.CodeType == CodeTypeValues.PatientCategory && w.ActiveStatus),
                    p => new { p.PatientCategoryId },
                    a => new { PatientCategoryId = a.ApplicationCode },
                    (p, a) => new { p, a })
                .GroupJoin(_context.GtEcapcd.Where(w => w.CodeType == CodeTypeValues.RateType && w.ActiveStatus),
                    pc => new { pc.p.RateType },
                    a => new { RateType = a.ApplicationCode },
                    (pc, a) => new { pc, a = a.FirstOrDefault() }).DefaultIfEmpty()
               .Where(w => w.pc.p.TreatmentAllowedOp
                    && w.pc.p.ActiveStatus)
               .Select(s => new DO_PatientCategory
               {
                   PatientCategoryID = s.pc.p.PatientCategoryId,
                   PatientCategoryDesc = s.pc.a.CodeDesc,
                   RateType = s.pc.p.RateType,
                   RateTypeDesc = s.a != null ? s.a.CodeDesc : "",
                   CareCardApplicable = s.pc.p.CareCardApplicable,
                   ServiceWiseRateType = s.pc.p.ServiceWiseRateType,
                   ValidateDocument = s.pc.p.ValidateDocument
               })
               .ToListAsync();
            return await pt;
        }

        public async Task<List<DO_BillSuspendType>> GetBillSuspendType()
        {
            var pt = _context.GtEcapcd
               .Where(w => w.CodeType == CodeTypeValues.BillSuspendType && w.ActiveStatus)
               .Select(s => new DO_BillSuspendType
               {
                   SuspendTypeID = s.ApplicationCode,
                   SuspendTypeDesc = s.CodeDesc,
                   SuspendTypeCode = s.ShortCode
               })
               .ToListAsync();
            return await pt;
        }

        public async Task<List<DO_ServiceCode>> GetServiceClassesByTypeID(int serviceType)
        {
            try
            {
                var result = _context.GtEssrcl
                .Join(_context.GtEssrgr,
                    c => c.ServiceGroupId,
                    g => g.ServiceGroupId,
                    (c, g) => new { c, g })
                    .Where(w => w.g.ServiceTypeId == serviceType && w.c.ActiveStatus && w.g.ActiveStatus)
                             .Select(x => new DO_ServiceCode
                             {
                                 ServiceClassId = x.c.ServiceClassId,
                                 ServiceClassDesc = x.c.ServiceClassDesc
                             }
                    ).ToListAsync();
                return await result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DO_DrugMaster>> GetDrugMaster()
        {
            try
            {
                var result = _context.GtEpdrcd
                    .Where(w => w.ActiveStatus)
                             .Select(x => new DO_DrugMaster
                             {
                                 DrugCode = x.DrugCode,
                                 DrugBrand = x.DrugBrand,
                                 DrugPrintDesc = x.DrugPrintDesc
                             }
                    ).ToListAsync();
                return await result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DO_ServiceCode>> GetServiceCodesByClassID(int serviceType, int serviceClass)
        {
            try
            {
                var result = _context.GtEssrms
                .Join(_context.GtEssrcl,
                m => m.ServiceClassId,
                c => c.ServiceClassId,
                (m, c) => new { m, c })
                .Join(_context.GtEssrgr,
                mc => mc.c.ServiceGroupId,
                g => g.ServiceGroupId,
                (mc, g) => new { mc, g })
                .Where(w => w.mc.m.ActiveStatus
                && w.mc.c.ActiveStatus
                && w.g.ActiveStatus
                && w.g.ServiceTypeId == serviceType
                && (w.mc.m.ServiceClassId == serviceClass || serviceClass == 0))
                             .Select(x => new DO_ServiceCode
                             {
                                 ServiceId = x.mc.m.ServiceId,
                                 ServiceDesc = x.mc.m.ServiceDesc,
                                 ServiceShortDesc = x.mc.m.ServiceShortDesc,
                             }
                    ).ToListAsync();
                return await result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DO_Doctors> GetDoctorByID(int doctorId)
        {
            try
            {
                var result = _context.GtEsdocd
                    .Where(w => w.ActiveStatus && w.DoctorId == doctorId)
                             .Select(x => new DO_Doctors
                             {
                                 DoctorId = x.DoctorId,
                                 DoctorName = x.DoctorName,
                                 Qualification = x.Qualification
                             }
                    ).FirstOrDefaultAsync();
                return await result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<DO_ApplicationCodes>> GetApplicationCodesByCodeTypeList(List<int> l_codeType)
        {
            try
            {
               
                    var ds = _context.GtEcapcd
                        .Where(w => w.ActiveStatus
                        && l_codeType.Contains(w.CodeType))
                        .Select(r => new DO_ApplicationCodes
                        {
                            CodeType = r.CodeType,
                            ApplicationCode = r.ApplicationCode,
                            CodeDesc = r.CodeDesc
                        }).OrderBy(o => o.CodeDesc).ToListAsync();

                    return await ds;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DO_CurrencyMaster>> GetActiveCurrencies()
        {
            var pf = _context.GtEccuco
               .Where(w => w.ActiveStatus)
               .Select(s => new DO_CurrencyMaster
               {
                   CurrencyCode = s.CurrencyCode,
                   CurrencyName = s.CurrencyName,
                   
               })
               .ToListAsync();
            return await pf;
        }
    }
}
