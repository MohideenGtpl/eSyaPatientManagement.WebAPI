using eSyaPatientManagement.DL.Entities;
using eSyaPatientManagement.DO;
using eSyaPatientManagement.IF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSyaPatientManagement.DL.Repository
{
    public class PatientInfoRepository: IPatientInfoRepository
    {
        private eSyaEnterpriseContext _context { get; set; }
        public PatientInfoRepository(eSyaEnterpriseContext context)
        {
            _context = context;
        }
        public async Task<List<DO_PatientProfile>> GetSearchPatient(string searchText)
        {
            var pf = _context.GtEfoppr
               .Where(w => ((w.FirstName + ' ' + w.LastName).ToUpper().Contains(searchText.ToUpper())
                    || w.MobileNumber.Equals(searchText))
                    && w.ActiveStatus)
               .Select(s => new DO_PatientProfile
               {
                   UHID = s.RUhid,
                   ISDCode = s.Isdcode,
                   MobileNumber = s.MobileNumber,
                   FirstName = s.FirstName,
                   MiddleName = s.MiddleName,
                   LastName = s.LastName,
                   IsDOBApplicable = s.IsDobapplicable,
                   DateOfBirth = s.DateOfBirth,
                   Gender = s.Gender,
                   eMailID = s.EMailId
               })
               .ToListAsync();
            return await pf;
        }

        public async Task<List<DO_PatientProfile>> GetPatientInfoRegistrationByMobileNo(string mobileNumber)
        {
            var pf = _context.GtEfoppr
               .Where(w => w.MobileNumber == mobileNumber
                    && w.ActiveStatus)
               .Select(s => new DO_PatientProfile
               {
                   UHID = s.RUhid,
                   ISDCode = s.Isdcode,
                   MobileNumber = s.MobileNumber,
                   FirstName = s.FirstName,
                   MiddleName = s.MiddleName,
                   LastName = s.LastName,
                   IsDOBApplicable = s.IsDobapplicable,
                   DateOfBirth = s.DateOfBirth,
                   Gender = s.Gender,
                   eMailID = s.EMailId
               })
               .ToListAsync();

            return await pf;
        }

        public async Task<DO_PatientProfile> GetPatientProfileByUHID(long uhid)
        {
            var pf = _context.GtEfoppr
               .Where(w => w.RUhid == uhid
                    && w.ActiveStatus)
               .Select(s => new DO_PatientProfile
               {
                   UHID = s.RUhid,
                   ISDCode = s.Isdcode,
                   MobileNumber = s.MobileNumber,
                   FirstName = s.FirstName,
                   MiddleName = s.MiddleName,
                   LastName = s.LastName,
                   Gender = s.Gender,
                   IsDOBApplicable = s.IsDobapplicable,
                   DateOfBirth = s.DateOfBirth,
                   AgeYY = s.AgeYy,
                   AgeMM = s.AgeMm,
                   AgeDD = s.AgeDd,
                   eMailID = s.EMailId,
                   Nationality = s.NationalityId,
                   BloodGroup = s.BloodGroup,
                   CurrentPatientAddress = s.GtEfopa1.Where(w=> w.RUhid==uhid).Select(a => new DO_PatientAddress
                   {
                       AddressId = a.AddressId,
                       Address = a.Address,
                       AreaCode = a.AreaCode,
                       CityCode = a.CityCode,
                       StateCode=a.StateCode,
                       Pincode=a.Pincode 
                   }).OrderByDescending(o => o.AddressId).FirstOrDefault()
               })
               .FirstOrDefaultAsync();

            return await pf;
        }

        public async Task<DO_AppointmentDetails> getPatientbookingInfo(int businessKey,long appKey)
        {
            var pf = await _context.GtEopaph
                .Join(_context.GtEopapd,
                h => h.AppointmentKey,
                d=> d.AppointmentKey,
                (h,d) => new { h, d })
               .Where(w => w.h.BusinessKey==businessKey && w.d.BusinessKey==businessKey
                    && w.h.AppointmentKey == appKey)
               .Select(s => new DO_AppointmentDetails
               {
                   ClinicId=s.h.ClinicId,
                   ConsultationId=s.h.ConsultationId,
                   SpecialtyId=s.h.SpecialtyId,
                   DoctorId=s.h.DoctorId,

                   UHID=s.d.Uhid,
                   FirstName = s.d.PatientFirstName,
                   MiddleName = s.d.PatientMiddleName,
                   LastName = s.d.PatientLastName,
                   Gender = s.d.Gender,
                   MobileNumber = s.d.MobileNumber,
                   DateOfBirth = s.d.DateOfBirth ,
                   eMailID = s.d.EmailId,

                   
               })
               .FirstOrDefaultAsync();
            if(pf.UHID != null && pf.UHID > 0)
            {
                DO_PatientProfile _pat = await GetPatientProfileByUHID(Convert.ToInt64(pf.UHID));
                pf.CurrentPatientAddress = _pat.CurrentPatientAddress;
                pf.AgeYY = _pat.AgeYY;
                pf.AgeMM = _pat.AgeMM;
                pf.AgeDD = _pat.AgeDD;
                pf.ISDCode = _pat.ISDCode;
                pf.Nationality = _pat.Nationality;
                pf.BloodGroup = _pat.BloodGroup;
                //pf.CurrentPatientAddress = _context.GtEfopa1.Where(w => w.RUhid == pf.UHID).Select(a => new DO_PatientAddress
                //{
                //    AddressId = a.AddressId,
                //    Address = a.Address,
                //    AreaCode = a.AreaCode,
                //    CityCode = a.CityCode,
                //    StateCode = a.StateCode,
                //    Pincode = a.Pincode
                //}).OrderByDescending(o => o.AddressId).FirstOrDefault();
            }



            return pf;
        }

        public async Task<List<DO_PatientProfile>> GetPatientSearch(string searchText)
        {
            try
            {
                var pf = _context.GtEfoppr
               .Where(w => w.ActiveStatus && (
               ((w.FirstName + " " + w.LastName).ToUpper().Contains(searchText.ToUpper()))
                            || w.MobileNumber.Contains(searchText.ToUpper())
                            || w.EMailId.ToUpper().Contains(searchText.ToUpper())
                            || w.PatientId.Contains(searchText.ToUpper())
                            || w.RUhid.ToString().Contains(searchText.ToUpper())
                            ))
                      .Select(r => new DO_PatientProfile
               {
                   PatientID = r.PatientId,
                   FirstName = r.FirstName + " " + r.LastName,
                   LastName = r.LastName,
                   Gender = r.Gender,
                   DateOfBirth = r.DateOfBirth,
                   ISDCode = r.Isdcode,
                   MobileNumber = r.MobileNumber,
                   eMailID = r.EMailId,
                   UHID = r.RUhid


               })
               .ToListAsync();



                    return await pf;
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DO_PatientAppointmentDetail>> GetPatientByUHID(int businessKey, long uhid)
        {
            try
            {
                    var ds = await _context.GtEfoppr

                         .Where(w =>
                                     w.BusinessKey == businessKey
                                     && w.RUhid == uhid
                                     && w.ActiveStatus
                                     )
                         .AsNoTracking()
                         .Select(r => new DO_PatientAppointmentDetail
                         {
                             UHID = Convert.ToInt32(r.RUhid),
                             PatientName = r.FirstName + " " + r.LastName,
                             PatientFirstName = r.FirstName,
                             PatientLastName = r.LastName,
                             Gender = r.Gender,
                             DateOfBirth = r.DateOfBirth,
                             PatientMobileNumber = r.MobileNumber,
                             PatientEmailID = r.EMailId,
                             PatientID = r.PatientId
                         }).ToListAsync();
                if(ds.Count >0)
                {
                    var Age = Convert.ToInt32((DateTime.Today.Subtract(Convert.ToDateTime(ds[0].DateOfBirth)).TotalDays) / 365.25);
                    ds[0].Age = Age;
                }
                    

                    return ds;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
