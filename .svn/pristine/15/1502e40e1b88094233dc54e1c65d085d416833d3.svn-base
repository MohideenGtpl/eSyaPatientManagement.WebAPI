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
    public class OpClinicDetailsRepository : IOpClinicDetailsRepository
    {
        private eSyaEnterpriseContext _context { get; set; }
        public OpClinicDetailsRepository(eSyaEnterpriseContext context)
        {
            _context = context;
        }

        public async Task<List<DO_AppointmentBooked>> GetAppointmentBookedByMobile(string mobileNumber)
        {
            var ap = await _context.GtEopaph
                .Join(_context.GtEopapd,
                     a => new { a.BusinessKey, a.AppointmentKey },
                     p => new { p.BusinessKey, p.AppointmentKey },
                     (a, p) => new { a, p })
               .Where(w => w.a.AppointmentDate.Date == DateTime.Now.Date 
                        && w.p.MobileNumber == mobileNumber 
                        && w.a.AppointmentStatus == StatusVariables.Appointment.Booked 
                        && w.a.ActiveStatus)
               .Select(r => new DO_AppointmentBooked
               {
                   AppointmentKey = (long)r.a.AppointmentKey,
                   PatientFirstName = r.p.PatientFirstName,
                   PatientLastName = r.p.PatientLastName,
                   UHID = r.p.Uhid,
                   ClinicId=r.a.ClinicId,
                   ConsultationId=r.a.ConsultationId,
                   SpecialtyId=r.a.SpecialtyId,
                   DoctorId=r.a.DoctorId 
                                     
               }).ToListAsync();

            return ap;
        }

        public async Task<List<DO_PatientPreRegistered>> GetPreRegistrationByMobile(string mobileNumber)
        {
            var pr = await _context.GtEopprr
               .Where(w => w.MobileNumber == mobileNumber
                        && w.RUhid == null
                        && w.ActiveStatus)
               .Select(r => new DO_PatientPreRegistered
               {
                   PRKey = (long)r.Prkey,
                   PatientFirstName = r.PatientFirstName,
                   PatientLastName = r.PatientLastName,
                   Gender = r.Gender,
                   AgeYY = r.AgeYy,
                   AgeMM = r.AgeMm,
                   AgeDD = r.AgeDd,
                   DateOfBirth = r.DateOfBirth,
                   MaritalStatus = r.MaritalStatus,
                   ISDCode = r.Isdcode,
                   MobileNumber = r.MobileNumber,
                   EmailID = r.EmailId,
                   Nationality = r.Nationality,
                   ReferredBy = r.ReferredBy,
                   PatientMiddleName=r.PatientMiddleName,
                   Address=r.Address,
                   City=r.CityCode,
                   State = r.StateCode,
                   AreaCode=r.AreaCode,
                   Pincode=r.Pincode 
               }).ToListAsync();

            return pr;
        }

        public async Task<List<DO_PatientProfile>> GetRegisteredPatientByMobileNo(string mobileNumber)
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

        public async Task<List<DO_ClinicType>> GetClinicTypes(int businessKey)
        {

            var cc = await _context.GtEsopcl
                .Join(_context.GtEcapcd,
                    c => c.ClinicId,
                    a => a.ApplicationCode,
                    (c, a) => new { c, a})
                .Join(_context.GtEcapcd,
                    cl => cl.c.ConsultationId,
                    a => a.ApplicationCode,
                    (cl, a) => new { cl, a })
                .Where(w => w.cl.c.BusinessKey == businessKey
                   && w.cl.c.ActiveStatus)
                .Select(x => new DO_ClinicType
                {
                    ClinicType = x.cl.c.ClinicId,
                    ClinicTypeCode = x.cl.a.ShortCode,
                    ClinicTypeDesc = x.cl.a.CodeDesc,
                    ConsultationType = x.cl.c.ConsultationId,
                    ConsultationTypeCode = x.a.ShortCode,
                    ConsultationTypeDesc = x.a.CodeDesc,
                })
                .OrderBy(o => o.ClinicTypeDesc)
                .ThenBy(o => o.ConsultationTypeDesc)
                .ToListAsync();

            return cc;
        }

        public async Task<List<DO_Specialty>> GetSpecialty(int businessKey)
        {
            var sp = await _context.GtEsspcd
                .Join(_context.GtEsspbl,
                    s => s.SpecialtyId,
                    b => b.SpecialtyId,
                    (s, b) => new { s, b })
                .Where(w => w.b.BusinessKey == businessKey
                    && _context.GtEssppa.Any(a => a.BusinessKey == w.b.BusinessKey && a.SpecialtyId == w.b.SpecialtyId
                                    && a.ParameterId == AppParameter.Specialty_AllowConsulation
                                    && a.ParmAction && a.ActiveStatus)
                    && w.s.ActiveStatus && w.b.ActiveStatus)
                .OrderBy(o => o.s.SpecialtyDesc)
                .Select(x => new DO_Specialty
                {
                    SpecialtyId = x.s.SpecialtyId,
                    SpecialtyDesc = x.s.SpecialtyDesc,
                }).Distinct().ToListAsync();

            return sp;
        }

        public async Task<List<DO_DoctorClinicSchedule>> GetDoctorScheduleListByClinicTypeSpecialtyDate(int businessKey,
           int clinicTypeId, int consultationTypeId,
           int specialtyId, DateTime scheduleDate)
        {

            try
            {
                var wk = scheduleDate.DayOfWeek.ToString();
                var wk_No = CommonFunction.GetWeekOfMonth(scheduleDate);

                var l_ds_1 = await _context.GtEsdocd
                    .GroupJoin(_context.GtEsdos1.Where(w => w.BusinessKey == businessKey
                                && w.DayOfWeek.ToUpper() == wk.ToUpper()
                                && w.ConsultationId == consultationTypeId
                                && w.ClinicId == clinicTypeId
                                && w.SpecialtyId == specialtyId
                                && ((wk_No == 1 && w.Week1) || (wk_No == 2 && w.Week2)
                                    || (wk_No == 3 && w.Week3) || (wk_No == 4 && w.Week4)
                                    || (wk_No == 5 && w.Week5) || (wk_No == 6 && w.Week5))
                                && w.ActiveStatus),
                        d => d.DoctorId,
                        s => s.DoctorId,
                        (d, s) => new { d, s }).DefaultIfEmpty()
                    .SelectMany(d => d.s.DefaultIfEmpty(), (d, s) => new { d.d, s })
                    .GroupJoin(_context.GtEsdold.Where(w =>
                                scheduleDate.Date >= w.OnLeaveFrom.Date
                                && scheduleDate.Date <= w.OnLeaveTill.Date
                                && w.ActiveStatus),
                        ds => ds.d.DoctorId,
                        l => l.DoctorId,
                        (ds, l) => new { ds, l = l.FirstOrDefault() }).DefaultIfEmpty()
                    .Where(w => w.ds.d.ActiveStatus
                                && !_context.GtEsdos2.Any(r => r.BusinessKey == businessKey
                                       && r.ConsultationId == consultationTypeId
                                       && r.ClinicId == clinicTypeId
                                       && r.SpecialtyId == specialtyId
                                       && r.DoctorId == w.ds.d.DoctorId
                                       && r.ScheduleDate.Date == scheduleDate.Date
                                       && r.ActiveStatus))
                    .AsNoTracking()
                    .Select(x => new DO_DoctorClinicSchedule
                    {
                        DoctorId = x.ds.d.DoctorId,
                        DoctorName = x.ds.d.DoctorName,
                        DoctorRemarks = x.ds.d.DoctorRemarks,
                        DayOfWeek = x.ds.s != null ? x.ds.s.DayOfWeek : "",
                        ScheduleDate = scheduleDate,
                        NumberofPatients = x.ds.s != null ? x.ds.s.NoOfPatients : 0,
                        FromTime = x.ds.s != null ? x.ds.s.ScheduleFromTime : new TimeSpan(9, 00, 00),
                        ToTime = x.ds.s != null ? x.ds.s.ScheduleToTime : new TimeSpan(18, 00, 00),
                        IsAvailable = x.ds.s != null ? true : false,
                        IsOnLeave = x.l != null ? x.l.ActiveStatus : false
                    }).ToListAsync();


                var l_ds_2 = await _context.GtEsdocd
                   .GroupJoin(_context.GtEsdos2.Where(w => w.BusinessKey == businessKey
                               && w.ConsultationId == consultationTypeId
                               && w.ClinicId == clinicTypeId
                               && w.SpecialtyId == specialtyId
                               && w.ScheduleDate.Date == scheduleDate.Date
                               && w.ActiveStatus),
                       d => d.DoctorId,
                       s => s.DoctorId,
                       (d, s) => new { d, s }).DefaultIfEmpty()
                   .SelectMany(d => d.s.DefaultIfEmpty(), (d, s) => new { d.d, s })
                   .GroupJoin(_context.GtEsdold.Where(w =>
                               scheduleDate.Date >= w.OnLeaveFrom.Date
                               && scheduleDate.Date <= w.OnLeaveTill.Date
                               && w.ActiveStatus),
                       ds => ds.d.DoctorId,
                       l => l.DoctorId,
                       (ds, l) => new { ds, l = l.FirstOrDefault() }).DefaultIfEmpty()
                   .Where(w => w.ds.d.ActiveStatus)
                   .AsNoTracking()
                   .Select(x => new DO_DoctorClinicSchedule
                   {
                       DoctorId = x.ds.d.DoctorId,
                       DoctorName = x.ds.d.DoctorName,
                       DoctorRemarks = x.ds.d.DoctorRemarks,
                       DayOfWeek = "",
                       ScheduleDate = scheduleDate,
                       NumberofPatients = x.ds.s != null ? x.ds.s.NoOfPatients : 0,
                       FromTime = x.ds.s != null ? x.ds.s.ScheduleFromTime : new TimeSpan(9, 00, 00),
                       ToTime = x.ds.s != null ? x.ds.s.ScheduleToTime : new TimeSpan(18, 00, 00),
                       IsAvailable = x.ds.s != null ? true : false,
                       IsOnLeave = x.l != null ? x.l.ActiveStatus : false
                   }).ToListAsync();

                var l_ds = l_ds_1.Union(l_ds_2);

                return l_ds.Where(w => w.IsAvailable).ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DO_Doctors>> GetAllDoctorList(int businessKey)
        {
            var dm = await _context.GtEsdocd
                .Join(_context.GtEsdobl,
                    d => d.DoctorId,
                    b => b.DoctorId,
                    (d, b) => new { d, b })
                .Where(w => w.b.BusinessKey == businessKey
                    && w.d.ActiveStatus && w.b.ActiveStatus)
                .OrderBy(o => o.d.DoctorName)
                .Select(x => new DO_Doctors
                {
                    DoctorId = x.d.DoctorId,
                    DoctorName = x.d.DoctorName,
                }).ToListAsync();

            return dm;
        }


    }
}
