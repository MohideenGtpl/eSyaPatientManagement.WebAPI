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
    public class DoctorDeskRepository : IDoctorDeskRepository
    {
        private eSyaEnterpriseContext _context { get; set; }
        public DoctorDeskRepository(eSyaEnterpriseContext context)
        {
            _context = context;
        }

        public async Task<List<DO_PatientAppointmentDetail>> GetAppointmentDetailByDate(int businessKey, int doctorId, DateTime startDate, DateTime endDate)
        {
            try
            {
                    var ds = await _context.GtEopaph
                      .Join(_context.GtEopapd,
                          h => new { h.BusinessKey, h.AppointmentKey },
                          d => new { d.BusinessKey, d.AppointmentKey },
                          (h, d) => new { h, d })
                          .Join(_context.GtEfopvd,
                          hd => new { hd.d.BusinessKey, hd.h.AppointmentKey},
                          v => new { v.BusinessKey, v.AppointmentKey},
                          (hd,v) => new { hd, v })
                         .Where(w =>
                                     w.hd.h.BusinessKey == businessKey
                                     && (w.hd.h.DoctorId == doctorId || doctorId == -1)
                                     && w.hd.h.AppointmentDate.Date >= startDate.Date
                                     && w.hd.h.AppointmentDate.Date <= endDate.Date
                                     && (w.hd.h.AppointmentStatus == "RG")
                                     && !w.hd.h.UnScheduleWorkOrder
                                     && w.hd.h.ActiveStatus && w.hd.d.ActiveStatus
                                     )
                         .AsNoTracking()
                         .Select(r => new DO_PatientAppointmentDetail
                         {
                             AppointmentKey = r.hd.h.AppointmentKey,
                             UHID = r.hd.d.Uhid,
                             AppointmentDate = r.hd.h.AppointmentDate,
                             AppointmentFromTime = r.hd.h.AppointmentFromTime,
                             Duration = r.hd.h.Duration,
                             StartDate = r.hd.h.AppointmentDate.Date.Add(r.hd.h.AppointmentFromTime),
                             EndDate = r.hd.h.AppointmentDate.Date.Add(r.hd.h.AppointmentFromTime).AddMinutes(r.hd.h.Duration),
                             PatientName = r.hd.d.PatientFirstName + " " + r.hd.d.PatientMiddleName + " " + r.hd.d.PatientLastName,
                             PatientFirstName = r.hd.d.PatientFirstName,
                             PatientLastName = r.hd.d.PatientLastName,
                             Gender = r.hd.d.Gender,
                             DateOfBirth = r.hd.d.DateOfBirth,
                             PatientMobileNumber = r.hd.d.MobileNumber,
                             PatientEmailID = r.hd.d.EmailId,
                             EpisodeType = r.hd.h.EpisodeType,
                             IsSponsored = r.hd.d.IsSponsored,
                             AppointmentStatus = r.hd.h.AppointmentStatus,
                             RequestChannel = r.hd.h.RequestChannel,
                             PaymentReceived = r.hd.h.PaymentReceived,
                             VisitType = r.hd.h.VisitType,
                             PatientID = _context.GtEfoppr.Where(w => w.RUhid == r.hd.d.Uhid).Select(s => s.PatientId).FirstOrDefault(),
                             DoctorName= _context.GtEsdocd.Where(w => w.DoctorId == r.hd.h.DoctorId).Select(s => s.DoctorName).FirstOrDefault(),
                             Opnumber=r.v.Opnumber

                         }).ToListAsync();

                    return ds;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<DO_PatientAppointmentDetail>> GetAppointmentDetailByUHID(long businessKey, long uhid)
        {
            try
            {
                    var ds = await _context.GtEopaph
                      .Join(_context.GtEopapd,
                          h => new { h.BusinessKey, h.AppointmentKey },
                          d => new { d.BusinessKey, d.AppointmentKey },
                          (h, d) => new { h, d })
                         .Where(w =>
                                     w.h.BusinessKey == businessKey
                                     && w.d.Uhid == uhid
                                     && w.h.AppointmentStatus != "CN"
                                     && !w.h.UnScheduleWorkOrder
                                     && w.h.ActiveStatus && w.d.ActiveStatus
                                     )
                         .AsNoTracking()
                         .Select(r => new DO_PatientAppointmentDetail
                         {
                             AppointmentKey = r.h.AppointmentKey,
                             UHID = r.d.Uhid,
                             AppointmentDate = r.h.AppointmentDate,
                             AppointmentFromTime = r.h.AppointmentFromTime,
                             Duration = r.h.Duration,
                             StartDate = r.h.AppointmentDate.Date.Add(r.h.AppointmentFromTime),
                             EndDate = r.h.AppointmentDate.Date.Add(r.h.AppointmentFromTime).AddMinutes(r.h.Duration),
                             PatientName = r.d.PatientFirstName + " " + r.d.PatientMiddleName + " " + r.d.PatientLastName,
                             PatientFirstName = r.d.PatientFirstName,
                             PatientLastName = r.d.PatientLastName,
                             Gender = r.d.Gender,
                             DateOfBirth = r.d.DateOfBirth,
                             PatientMobileNumber = r.d.MobileNumber,
                             PatientEmailID = r.d.EmailId,
                             EpisodeType = r.h.EpisodeType,
                             IsSponsored = r.d.IsSponsored,
                             AppointmentStatus = r.h.AppointmentStatus

                         }).OrderByDescending(o => o.AppointmentDate).ThenByDescending(o => o.AppointmentKey).ToListAsync();

                    return ds;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<DO_DoctorSchedule>> GetDoctorScheduleList(int businessKey, int doctorId)
        {

                try
                {
                    var dc_ms = _context.GtEsdos1
                        .Join(_context.GtEcapcd.Where(x => x.CodeType == CodeTypeValue.ConsultationType),
                        d => new { d.ConsultationId },
                        a => new { ConsultationId = a.ApplicationCode },
                        (d, a) => new { d, a })
                        .Join(_context.GtEsspcd,
                        dd => new { dd.d.SpecialtyId },
                        aa => new { aa.SpecialtyId },
                        (dd, aa) => new { dd, aa })
                        .Join(_context.GtEcapcd.Where(x => x.CodeType == CodeTypeValue.Clinic),
                        ddd => new { ddd.dd.d.ClinicId },
                        aaa => new { ClinicId = aaa.ApplicationCode },
                        (ddd, aaa) => new { ddd, aaa })
                        .Where(w => w.ddd.dd.d.BusinessKey == businessKey && w.ddd.dd.d.DoctorId == doctorId && w.ddd.dd.d.ActiveStatus)
                        .AsNoTracking()
                        .Select(x => new DO_DoctorSchedule
                        {
                            BusinessKey = x.ddd.dd.d.BusinessKey,
                            DoctorId = x.ddd.dd.d.DoctorId,
                            ClinicID = x.ddd.dd.d.ClinicId,
                            ClinicDesc = x.aaa.CodeDesc,
                            ConsultationID = x.ddd.dd.d.ConsultationId,
                            ConsultationType = x.ddd.dd.a.CodeDesc,
                            SerialNo = x.ddd.dd.d.SerialNo,
                            SpecialtyID = x.ddd.dd.d.SpecialtyId,
                            SpecialtyDesc = x.ddd.aa.SpecialtyDesc,
                            DayOfWeek = x.ddd.dd.d.DayOfWeek,
                            NoOfPatients = x.ddd.dd.d.NoOfPatients,
                            ScheduleFromTime = x.ddd.dd.d.ScheduleFromTime,
                            ScheduleToTime = x.ddd.dd.d.ScheduleToTime,
                            Week1 = x.ddd.dd.d.Week1,
                            Week2 = x.ddd.dd.d.Week2,
                            Week3 = x.ddd.dd.d.Week3,
                            Week4 = x.ddd.dd.d.Week4,
                            Week5 = x.ddd.dd.d.Week5,
                            RoomNo = x.ddd.dd.d.RoomNo,
                            ActiveStatus = x.ddd.dd.d.ActiveStatus

                        })//.OrderBy(x => new { x.SpecialtyDesc, x.ClinicDesc, x.ConsultationType, x.SerialNo })
                        .ToListAsync();

                    return await dc_ms;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            
        }
        public async Task<long> GetLastOPNumber (long businessKey,long uhid)
        {
            try
            {
                var _opnum = _context.GtEfopvd
                    .Where(x => x.BusinessKey == businessKey && x.RUhid == uhid)
                    .Select(s => s.Opnumber).MaxAsync();
              

                return await _opnum;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
