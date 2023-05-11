using eSyaPatientManagement.DL.Entities;
using eSyaPatientManagement.DO;
using eSyaPatientManagement.DO.StaticVariables;
using eSyaPatientManagement.IF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSyaPatientManagement.DL.Repository
{
    public class OpRegistrationBillingRepository : IOpRegistrationBillingRepository
    {
        private eSyaEnterpriseContext _context { get; set; }
        private IBillingTransactionRepository _billingTransactionRepository;
        private IReceiptTransactionRepository _receiptTransactionRepository;
        public OpRegistrationBillingRepository(eSyaEnterpriseContext context, IBillingTransactionRepository billingTransactionRepository, IReceiptTransactionRepository receiptTransactionRepository)
        {
            _context = context;
            _billingTransactionRepository = billingTransactionRepository;
            _receiptTransactionRepository = receiptTransactionRepository;
        }
        public async Task<DO_ResponseParameter> InsertOPRegistrationVisit(DO_OPRegistrationVisit obj)
        {

            var dbContext = _context.Database.BeginTransaction();

            try
            {
                if (obj.AppointmentKey != null)
                {
                    var op_app_exist = _context.GtEfopvd.Where(w => w.AppointmentKey == obj.AppointmentKey).Count();
                    if (op_app_exist > 0)
                    {
                        return new DO_ResponseParameter { Status = false, Message = "Appointment has been already registered" };
                    }
                }

                if (obj.UHID > 0)
                    obj.IsNewRegn = false;
                else
                    obj.IsNewRegn = true;

                bool warning = false;
                string warningMessage = "";

                var financialYear = _context.GtEcclco.Where(w => w.BusinessKey == obj.BusinessKey
                                              && DateTime.Now.Date >= w.FromDate.Date
                                              && DateTime.Now.Date <= w.TillDate.Date)
                                  .First().FinancialYear;

              


                GtEfoppr pp;
                if (obj.IsNewRegn)
                {
                    var dc_pm = _context.GtDncn01
                     .Where(w => w.BusinessKey == obj.BusinessKey && w.DocumentId == DocumentIdValues.UHID_id).FirstOrDefault();
                    dc_pm.CurrentDocNumber = dc_pm.CurrentDocNumber + 1;
                    dc_pm.CurrentDocDate = DateTime.Now;

                    var patient_id = DateTime.Now.ToString("yMM") + (dc_pm.CurrentDocNumber).ToString().PadLeft(4, '0');

                    pp = new GtEfoppr
                    {
                        RUhid = dc_pm.CurrentDocNumber,
                        SUhid = dc_pm.CurrentDocNumber,
                        BusinessKey = obj.BusinessKey,
                        NationalityId = obj.PatientProfile.Nationality,
                        Title = obj.PatientProfile.Title,
                        FirstName = obj.PatientProfile.FirstName,
                        MiddleName = obj.PatientProfile.MiddleName,
                        LastName = obj.PatientProfile.LastName,
                        Gender = obj.PatientProfile.Gender,
                        IsDobapplicable = obj.PatientProfile.IsDOBApplicable,
                        DateOfBirth = obj.PatientProfile.DateOfBirth,
                        AgeYy = obj.PatientProfile.AgeYY,
                        AgeMm = obj.PatientProfile.AgeMM,
                        AgeDd = obj.PatientProfile.AgeDD,
                        BloodGroup = obj.PatientProfile.BloodGroup,
                        Isdcode = obj.PatientProfile.ISDCode,
                        MobileNumber = obj.PatientProfile.MobileNumber,
                        EMailId = obj.PatientProfile.eMailID,
                        PatientStatus = obj.PatientProfile.PatientStatus,
                        RecordStatus = obj.PatientProfile.RecordStatus,
                        BillStatus = "O",
                        PatientId = patient_id,
                        ActiveStatus = true,
                        CreatedBy = obj.UserID,
                        CreatedOn = DateTime.Now,
                        CreatedTerminal = obj.TerminalID
                    };

                    _context.GtEfoppr.Add(pp);
                }
                else
                {
                    pp = await _context.GtEfoppr.Where(w => w.RUhid == obj.UHID).FirstAsync();

                    pp.NationalityId = obj.PatientProfile.Nationality;
                    pp.Title = obj.PatientProfile.Title;
                    pp.FirstName = obj.PatientProfile.FirstName;
                    pp.MiddleName = obj.PatientProfile.MiddleName;
                    pp.LastName = obj.PatientProfile.LastName;
                    pp.Gender = obj.PatientProfile.Gender;
                    pp.IsDobapplicable = obj.PatientProfile.IsDOBApplicable;
                    pp.DateOfBirth = obj.PatientProfile.DateOfBirth;
                    pp.AgeYy = obj.PatientProfile.AgeYY;
                    pp.AgeMm = obj.PatientProfile.AgeMM;
                    pp.AgeDd = obj.PatientProfile.AgeDD;
                    pp.BloodGroup = obj.PatientProfile.BloodGroup;
                    pp.Isdcode = obj.PatientProfile.ISDCode;
                    pp.MobileNumber = obj.PatientProfile.MobileNumber;
                    pp.EMailId = obj.PatientProfile.eMailID;
                    pp.ModifiedBy = obj.UserID;
                    pp.ModifiedOn = DateTime.Now;
                    pp.ModifiedTerminal = obj.TerminalID;
                }

                if (obj.Prkey != null)
                {
                    var pr = await _context.GtEopprr.Where(w => w.BusinessKey == obj.BusinessKey && w.Prkey == obj.Prkey).FirstOrDefaultAsync();
                    if (pr != null)
                        pr.RUhid = pp.RUhid;
                }

                if (obj.PatientProfile.L_PatientAddress != null)
                {
                    foreach (var a in obj.PatientProfile.L_PatientAddress)
                    {
                        var cd = _context.GtEfopa1.Where(w => w.RUhid == pp.RUhid && w.AddressId == a.AddressId).FirstOrDefault();
                        if (cd == null)
                        {
                            cd = new GtEfopa1
                            {
                                RUhid = pp.RUhid,
                                AddressId = _context.GtEfopa1.Where(w => w.RUhid == pp.RUhid).Select(s => s.AddressId).DefaultIfEmpty(0).Max() + 1,
                                Address = a.Address,
                                AreaCode = a.AreaCode,
                                CityCode = a.CityCode,
                                StateCode = a.StateCode,
                                Pincode = a.Pincode,
                                ActiveStatus = true,
                                CreatedBy = obj.UserID,
                                CreatedOn = DateTime.Now,
                                CreatedTerminal = obj.TerminalID
                            };
                            _context.GtEfopa1.Add(cd);
                        }
                        else
                        {
                            // cd .AddressId = _context.GtEfopa1.Where(w => w.RUhid == pp.RUhid).Select(s=>s.AddressId).DefaultIfEmpty(0).Max()+1;
                            cd.Address = a.Address;
                            cd.AreaCode = a.AreaCode;
                            cd.CityCode = a.CityCode;
                            cd.StateCode = a.StateCode;
                            cd.Pincode = a.Pincode;
                            cd.ModifiedBy = obj.UserID;
                            cd.ModifiedOn = DateTime.Now;
                            cd.ModifiedTerminal = obj.TerminalID;
                        }
                    }
                }

                if (obj.AppointmentKey != null)
                {
                    var app_h = _context.GtEopaph.Where(w => w.AppointmentKey == obj.AppointmentKey).FirstOrDefault();
                    app_h.AppointmentStatus = StatusVariables.Appointment.Registered;

                    var app_d = _context.GtEopapd.Where(w => w.AppointmentKey == obj.AppointmentKey).FirstOrDefault();
                    app_d.Uhid = (long)pp.RUhid;
                }
                else
                {
                    if (obj.ConsultationInfo != null)
                    {
                        DO_PatientAppointmentDetail obj_ap = new DO_PatientAppointmentDetail
                        {
                            BusinessKey = obj.BusinessKey,
                            AppointmentDate = DateTime.Now,
                            AppointmentFromTime = DateTime.Now.TimeOfDay,
                            UHID = pp.RUhid,
                            ClinicId = obj.ConsultationInfo.ClinicID,
                            ConsultationId = obj.ConsultationInfo.ConsultationID,
                            SpecialtyID = obj.ConsultationInfo.SpecialtyID,
                            DoctorID = obj.ConsultationInfo.DoctorID,
                            ReasonforAppointment = "Walk-In",
                            VisitType = obj.VisitType,
                            EpisodeType = obj.ConsultationInfo.Episode,
                            PatientFirstName = obj.PatientProfile.FirstName,
                            PatientMiddleName = obj.PatientProfile.MiddleName,
                            PatientLastName = obj.PatientProfile.LastName,
                            Gender = obj.PatientProfile.Gender,
                            DateOfBirth = obj.PatientProfile.DateOfBirth,
                            PatientMobileNumber = obj.PatientProfile.MobileNumber,
                            PatientEmailID = obj.PatientProfile.eMailID,
                            ReferredBy = 0,
                            FormID = obj.FormID,
                            UserID = obj.UserID,
                            TerminalID = obj.TerminalID
                        };

                        var obj_ap_resp = await InsertIntoPatientAppointmentDetail(_context, obj_ap);
                        obj.AppointmentKey = obj_ap_resp.Key;
                        var app_h = _context.GtEopaph.Where(w => w.AppointmentKey == obj.AppointmentKey).FirstOrDefault();
                        app_h.AppointmentStatus = StatusVariables.Appointment.Registered;
                    }
                }

                int visitSerialNo = 1;
                if (!obj.IsNewRegn)
                    visitSerialNo = _context.GtEfopvd.Where(w => w.RUhid == pp.RUhid).Select(s => s.NoOfVisit).DefaultIfEmpty(0).Max() + 1;

                var dc_op = _context.GtDncn01
                  .Where(w => w.BusinessKey == obj.BusinessKey && w.FinancialYear == financialYear
                      && w.DocumentId == DocumentIdValues.OP_Number_id).FirstOrDefault();
                dc_op.CurrentDocNumber = dc_op.CurrentDocNumber + 1;
                dc_op.CurrentDocDate = DateTime.Now;

                var opNumber = Convert.ToInt64(DateTime.Now.ToString("yy") + dc_op.CurrentDocNumber.ToString().PadLeft(7, '0'));

                GtEfopvd vd = new GtEfopvd
                {
                    BusinessKey = obj.BusinessKey,
                    RUhid = pp.RUhid,
                    Opnumber = opNumber,
                    VisitDate = DateTime.Now,
                    NoOfVisit = visitSerialNo,
                    RegistrationType = obj.RegistrationType,
                    PatientClass = "H",
                    VisitType = obj.VisitType,
                    AppointmentKey = obj.AppointmentKey ?? 0,
                    IsVip = obj.IsVIP,
                    IsMlc = obj.IsMLC,
                    Prkey = obj.Prkey,
                    TokenKey = obj.TokenKey,
                    ActiveStatus = true,
                    CreatedBy = obj.UserID,
                    CreatedOn = DateTime.Now,
                    CreatedTerminal = obj.TerminalID
                };
                _context.GtEfopvd.Add(vd);

                GtEfoppc vd_pc = new GtEfoppc
                {
                    BusinessKey = obj.BusinessKey,
                    RUhid = pp.RUhid,
                    Opnumber = opNumber,
                    PatientType = obj.PatientType,
                    PatientCategory = obj.PatientCategory,
                    RatePlan = obj.RatePlan,
                    ActiveStatus = true,
                    CreatedBy = obj.UserID,
                    CreatedOn = DateTime.Now,
                    CreatedTerminal = obj.TerminalID
                };
                _context.GtEfoppc.Add(vd_pc);

                GtPtrgci cc = new GtPtrgci();
                if (obj.ConsultationInfo != null)
                {
                    cc = new GtPtrgci
                    {
                        BusinessKey = obj.BusinessKey,
                        RUhid = pp.RUhid,
                        Opnumber = opNumber,
                        SerialNumber = 1,
                        ClinicId = obj.ClinicId,
                        SpecialtyId = obj.ConsultationInfo.SpecialtyID,
                        DoctorId = obj.ConsultationInfo.DoctorID,
                        Episode = obj.ConsultationInfo.Episode,
                        CaseType = "C",
                        IsDiabetic = false,
                        IsHypertensive = false,
                        ActiveStatus = true,
                        CreatedBy = obj.UserID,
                        CreatedOn = DateTime.Now,
                        CreatedTerminal = obj.TerminalID
                    };
                    var visitDetail = _context.GtPtrgci.Add(cc);
                }

                if (obj.PatientPassport != null)
                {
                    GtEfoppd pd = new GtEfoppd
                    {
                        BusinessKey = obj.BusinessKey,
                        RUhid = pp.RUhid,
                        Opnumber = opNumber,
                        IsPpscanned = obj.PatientPassport.IsPPScanned,
                        PassportNumber = obj.PatientPassport.PassportNumber,
                        DateOfIssue = obj.PatientPassport.DateOfIssue,
                        PlaceOfIssue = obj.PatientPassport.PlaceOfIssue,
                        PassportExpiresOn = obj.PatientPassport.PassportExpiresOn,
                        VisaType = obj.PatientPassport.VisaType,
                        VisaIssueDate = obj.PatientPassport.VisaIssueDate,
                        VisaExpiryDate = obj.PatientPassport.VisaExpiryDate,
                        ActiveStatus = true,
                        CreatedBy = obj.UserID,
                        CreatedOn = DateTime.Now,
                        CreatedTerminal = obj.TerminalID
                    };
                    _context.GtEfoppd.Add(pd);
                }

                if (obj.PatientNextToKIN != null)
                {
                    GtEfopnk nk = new GtEfopnk
                    {
                        BusinessKey = obj.BusinessKey,
                        RUhid = pp.RUhid,
                        Opnumber = opNumber,
                        Kinname = obj.PatientNextToKIN.KINName,
                        Kinrelationship = obj.PatientNextToKIN.KINRelationship,
                        Isdcode = obj.PatientNextToKIN.ISDCode,
                        KinmobileNumber = obj.PatientNextToKIN.KINMobileNumber,
                        KincontactAddress = obj.PatientNextToKIN.KINContactAddress,
                        ActiveStatus = true,
                        CreatedBy = obj.UserID,
                        CreatedOn = DateTime.Now,
                        CreatedTerminal = obj.TerminalID
                    };
                    _context.GtEfopnk.Add(nk);
                }

                if (obj.L_OPPayer != null)
                {
                    foreach (var p in obj.L_OPPayer)
                    {
                        GtEfoppi pi = new GtEfoppi
                        {
                            BusinessKey = obj.BusinessKey,
                            RUhid = pp.RUhid,
                            Opnumber = opNumber,
                            SerialNumber = obj.SerialNumber,
                            Payer = p.Payer,
                            IsPrimaryPayer = p.IsPrimaryPayer,
                            RatePlan = p.RatePlan,
                            SchemePlan = p.SchemePlan,
                            MemberId = p.MemberID,
                            CoPayPerc = p.CoPayPerc,
                            ActiveStatus = true,
                            CreatedBy = obj.UserID,
                            CreatedOn = DateTime.Now,
                            CreatedTerminal = obj.TerminalID
                        };
                        _context.GtEfoppi.Add(pi);
                    }
                }

               

                long billDocumentKey = 0;
                if (obj.O_PatientBill != null)
                {
                    obj.O_PatientBill.UHID = pp.RUhid;
                    obj.O_PatientBill.OPNumber = opNumber;
                    obj.O_PatientBill.BillType = VoucherTransactionType.Consultation_Billing;
                    obj.O_PatientBill.BusinessKey = obj.BusinessKey;
                    obj.O_PatientBill.FormID = obj.FormID;
                    obj.O_PatientBill.TerminalID = obj.TerminalID;
                    obj.O_PatientBill.UserID = obj.UserID;

                    var billingResponse = await _billingTransactionRepository.InsertBillingTransaction(_context, obj.O_PatientBill);

                    if (cc != null)
                    {
                        cc.BillDocumentKey = billingResponse.Key;
                        billDocumentKey = billingResponse.Key;
                    }

                    foreach (DO_PaymentReceipt r in obj.O_PatientBill.l_PaymentReceipt)
                    {
                        r.BusinessKey = obj.BusinessKey;
                        r.BillDocumentKey = billingResponse.Key;
                        r.TransactionType = VoucherTransactionType.Consultation_Billing;

                        await _receiptTransactionRepository.InsertPatientReceipt(_context, r);
                    }
                }

                await _context.SaveChangesAsync();
                dbContext.Commit();
                return new DO_ResponseParameter { Key = billDocumentKey, Warning = warning, WarningMessage = warningMessage, Status = true };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DO_ResponseParameter> InsertIntoPatientAppointmentDetail(eSyaEnterpriseContext _context, DO_PatientAppointmentDetail obj)
        {

            try
            {
                var endTimeSlot = obj.AppointmentFromTime.Add(new TimeSpan(0, obj.Duration, 0));

                var financialYear = _context.GtEcclco.Where(w =>
                                                    w.BusinessKey == obj.BusinessKey
                                                && DateTime.Now.Date >= w.FromDate.Date
                                                && DateTime.Now.Date <= w.TillDate.Date)
                                    .First().FinancialYear;
                obj.FinancialYear = (int)financialYear;

                var dc_ap = await _context.GtDncn01
                                .Where(w => w.BusinessKey == obj.BusinessKey
                                    && w.FinancialYear == financialYear
                                    && w.DocumentId == DocumentIdValues.op_AppointmentId).FirstOrDefaultAsync();
                dc_ap.CurrentDocNumber = dc_ap.CurrentDocNumber + 1;
                dc_ap.CurrentDocDate = DateTime.Now;
                await _context.SaveChangesAsync();

                obj.DocumentID = dc_ap.DocumentId;
                obj.DocumentNumber = dc_ap.CurrentDocNumber;

                var appointmentKey = long.Parse(obj.FinancialYear.ToString().Substring(2, 2) +
                    obj.BusinessKey.ToString().PadLeft(2, '0') +
                    dc_ap.DocumentId.ToString().PadLeft(3, '0') +
                    obj.DocumentNumber.ToString());

                obj.AppointmentKey = appointmentKey;

                var timeSlotDuration = await _context.GtEsdocd.Where(w => w.DoctorId == obj.DoctorID).Select(s => s.TimeSlotInMintues).FirstOrDefaultAsync();
                obj.Duration = timeSlotDuration;

                string appType = "CA";
                if (obj.IsSponsored)
                    appType = "SA";

                var rp = await GetAppointmentQueueToken(_context, obj);
                if (!rp.Status)
                    return rp;
                var qTokenKey = obj.QueueTokenKey;

                var qs_apTk = new GtEopapq
                {
                    BusinessKey = obj.BusinessKey,
                    TokenDate = obj.AppointmentDate.Date,
                    QueueTokenKey = qTokenKey,
                    AppointmentFromTime = obj.AppointmentFromTime,
                    AppointmentKey = obj.AppointmentKey,
                    SequeueNumber = 1,
                    PatientType = appType,
                    SpecialtyId = obj.SpecialtyID,
                    DoctorId = obj.DoctorID,
                    Uhid = obj.UHID,
                    TokenStatus = StatusVariables.Appointment.Booked,
                    ActiveStatus = true,
                    FormId = obj.FormID,
                    CreatedBy = obj.UserID,
                    CreatedOn = DateTime.Now,
                    CreatedTerminal = obj.TerminalID
                };
                _context.GtEopapq.Add(qs_apTk);
                await _context.SaveChangesAsync();

                var app_hd = new GtEopaph
                {
                    BusinessKey = obj.BusinessKey,
                    FinancialYear = obj.FinancialYear,
                    DocumentId = obj.DocumentID,
                    DocumentNumber = obj.DocumentNumber,
                    AppointmentKey = obj.AppointmentKey,
                    ClinicId = obj.ClinicId,
                    ConsultationId = obj.ConsultationId,
                    SpecialtyId = obj.SpecialtyID,
                    DoctorId = obj.DoctorID,
                    AppointmentDate = obj.AppointmentDate,
                    AppointmentFromTime = obj.AppointmentFromTime,
                    Duration = obj.Duration,
                    AppointmentStatus = StatusVariables.Appointment.Booked,
                    ReasonforAppointment = obj.ReasonforAppointment,
                    QueueTokenKey = qTokenKey,
                    VisitType = obj.VisitType,
                    EpisodeType = obj.EpisodeType,
                    ReferredBy = obj.ReferredBy,
                    UnScheduleWorkOrder = false,
                    ActiveStatus = true,
                    FormId = obj.FormID,
                    CreatedBy = obj.UserID,
                    CreatedOn = DateTime.Now,
                    CreatedTerminal = obj.TerminalID
                };
                _context.GtEopaph.Add(app_hd);
                await _context.SaveChangesAsync();

                var app_dt = new GtEopapd
                {
                    BusinessKey = obj.BusinessKey,
                    AppointmentKey = obj.AppointmentKey,
                    Uhid = obj.UHID,
                    PatientFirstName = obj.PatientFirstName,
                    PatientMiddleName = obj.PatientMiddleName,
                    PatientLastName = obj.PatientLastName,
                    Gender = obj.Gender,
                    DateOfBirth = obj.DateOfBirth,
                    MobileNumber = obj.PatientMobileNumber,
                    SecondaryMobileNumber = obj.SecondaryMobileNumber,
                    EmailId = obj.PatientEmailID,
                    IsSponsored = obj.IsSponsored,
                    CustomerId = obj.CustomerID,
                    ActiveStatus = true,
                    FormId = obj.FormID,
                    CreatedBy = obj.UserID,
                    CreatedOn = DateTime.Now,
                    CreatedTerminal = obj.TerminalID
                };
                _context.GtEopapd.Add(app_dt);

                await _context.SaveChangesAsync();
                return new DO_ResponseParameter { Status = true, Key = obj.AppointmentKey };
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<DO_ResponseParameter> GetAppointmentQueueToken(eSyaEnterpriseContext db, DO_PatientAppointmentDetail obj)
        {

            var doctorCode = obj.DoctorID.ToString();

            TimeSpan totalDuration = new TimeSpan();
            var appointmentSchedule = await GetDoctorScheduleByID(obj.BusinessKey, obj.DoctorID, obj.AppointmentDate);
            foreach (var s in appointmentSchedule)
            {
                if (s.FromTime <= obj.AppointmentFromTime && s.ToTime >= obj.AppointmentFromTime)
                {
                    totalDuration += obj.AppointmentFromTime.Subtract((TimeSpan)s.FromTime);
                    break;
                }
                else
                    totalDuration += ((TimeSpan)s.ToTime).Subtract((TimeSpan)s.FromTime);
            }

            var totalIntervalinMinutes = totalDuration.Days * 24 * 60 +
                                         totalDuration.Hours * 60 +
                                         totalDuration.Minutes;
            var slotQueue = Math.Ceiling(totalIntervalinMinutes / Convert.ToDecimal(obj.Duration)) + 1;

            while (true)
            {
                var qTokenKey = doctorCode + "-" + slotQueue.ToString().PadLeft(2, '0');

                var q_exists = await db.GtEopapq.Where(w => w.BusinessKey == obj.BusinessKey && w.DoctorId == obj.DoctorID
                    && w.TokenDate.Date == obj.AppointmentDate.Date
                    && w.QueueTokenKey == qTokenKey
                    ).FirstOrDefaultAsync();
                if (q_exists != null)
                {
                    slotQueue++;
                    continue;
                }
                else
                {
                    obj.QueueTokenKey = qTokenKey;
                    break;
                }
            }

            return new DO_ResponseParameter
            {
                Status = true
            };
        }

        public async Task<List<DO_DoctorClinicSchedule>> GetDoctorScheduleByID(int businessKey,
                 int doctorID, DateTime fromDate)
        {

            List<DO_DoctorClinicSchedule> l_sc = new List<DO_DoctorClinicSchedule>();
            var wk = fromDate.DayOfWeek.ToString();
            var wk_No = GetWeekOfMonth(fromDate);

            var l_ds_1 = await _context.GtEsdocd
                    .GroupJoin(_context.GtEsdos1.Where(w => w.DayOfWeek.ToUpper() == wk.ToUpper()
                            && ((wk_No == 1 && w.Week1) || (wk_No == 2 && w.Week2)
                                || (wk_No == 3 && w.Week3) || (wk_No == 4 && w.Week4)
                                || (wk_No == 5 && w.Week5) || (wk_No == 6 && w.Week5))
                            && w.ActiveStatus),
                        d => d.DoctorId,
                        s => s.DoctorId,
                        (d, s) => new { d, s }).DefaultIfEmpty()
                    .SelectMany(d => d.s.DefaultIfEmpty(), (d, s) => new { d.d, s })
                    .GroupJoin(_context.GtEsdold.Where(w =>
                            w.ActiveStatus),
                        ds => ds.d.DoctorId,
                        l => l.DoctorId,
                        (ds, l) => new { ds, l = l.FirstOrDefault() }).DefaultIfEmpty()
                    .Where(w => w.ds.d.ActiveStatus && w.ds.d.DoctorId == doctorID
                            && !_context.GtEsdos2.Any(r => r.BusinessKey == businessKey
                                   && r.DoctorId == doctorID
                                   && r.ScheduleDate.Date == fromDate.Date
                                   && r.ActiveStatus))
                    .AsNoTracking()
                    .Select(x => new DO_DoctorClinicSchedule
                    {
                        DoctorId = x.ds.d.DoctorId,
                        DoctorName = x.ds.d.DoctorName,
                        DoctorRemarks = x.ds.d.DoctorRemarks,
                        DayOfWeek = x.ds.s != null ? x.ds.s.DayOfWeek : "",
                        ScheduleDate = fromDate,
                        NumberofPatients = x.ds.s != null ? x.ds.s.NoOfPatients : 0,
                        FromTime = x.ds.s != null ? x.ds.s.ScheduleFromTime : new TimeSpan(9, 00, 00),
                        ToTime = x.ds.s != null ? x.ds.s.ScheduleToTime : new TimeSpan(18, 00, 00),
                        IsAvailable = x.ds.s != null ? true : false,
                        IsOnLeave = x.l != null ? x.l.ActiveStatus : false
                    }).OrderBy(o => o.FromTime).ToListAsync();


            var l_ds_2 = await _context.GtEsdocd
                  .GroupJoin(_context.GtEsdos2.Where(w => w.BusinessKey == businessKey
                          && w.ScheduleDate.Date == fromDate.Date
                          && w.ActiveStatus),
                      d => d.DoctorId,
                      s => s.DoctorId,
                      (d, s) => new { d, s = s.FirstOrDefault() }).DefaultIfEmpty()
                  .GroupJoin(_context.GtEsdold.Where(w =>
                          w.ActiveStatus),
                      ds => ds.d.DoctorId,
                      l => l.DoctorId,
                      (ds, l) => new { ds, l = l.FirstOrDefault() }).DefaultIfEmpty()
              .Where(w => w.ds.d.ActiveStatus && w.ds.d.DoctorId == doctorID)
              .AsNoTracking()
              .Select(x => new DO_DoctorClinicSchedule
              {
                  DoctorId = x.ds.d.DoctorId,
                  DoctorName = x.ds.d.DoctorName,
                  DoctorRemarks = x.ds.d.DoctorRemarks,
                  DayOfWeek = "",
                  ScheduleDate = fromDate,
                  NumberofPatients = x.ds.s != null ? x.ds.s.NoOfPatients : 0,
                  FromTime = x.ds.s != null ? x.ds.s.ScheduleFromTime : new TimeSpan(9, 00, 00),
                  ToTime = x.ds.s != null ? x.ds.s.ScheduleToTime : new TimeSpan(18, 00, 00),
                  IsAvailable = x.ds.s != null ? true : false,
                  IsOnLeave = x.l != null ? x.l.ActiveStatus : false
              }).OrderBy(o => o.FromTime).ToListAsync();

            l_sc = l_ds_1.Union(l_ds_2).ToList();

            return l_sc;
        }
        public static int GetWeekOfMonth(DateTime date)
        {
            DateTime beginningOfMonth = new DateTime(date.Year, date.Month, 1);
            while (date.Date.AddDays(1).DayOfWeek != CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek)
                date = date.AddDays(1);
            return (int)Math.Truncate((double)date.Subtract(beginningOfMonth).TotalDays / 7f) + 1;
        }

    }
}
