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
    public class SuspendOpBillingRepository : ISuspendOpBillingRepository
    {
        private eSyaEnterpriseContext _context { get; set; }
        public SuspendOpBillingRepository(eSyaEnterpriseContext context)
        {
            _context = context;
        }

        public async Task<DO_ResponseParameter> SuspendOpBill(DO_PatientBillHeader obj)
        {

            try
            {
                var dbContext = _context.Database.BeginTransaction();

                obj.FinancialYear = (int)_context.GtEcclco.Where(w => DateTime.Now.Date >= w.FromDate.Date
                        && DateTime.Now.Date <= w.TillDate.Date).First().FinancialYear;

                var dc_bl = await _context.GtDncn02
                  .Where(w => w.BusinessKey == obj.BusinessKey && w.FinancialYear == obj.FinancialYear
                      && w.DocumentId == DocumentIdValues.Suspend_OpBilling_id).FirstOrDefaultAsync();
                dc_bl.CurrentDocNumber = dc_bl.CurrentDocNumber + 1;
                dc_bl.CurrentDocDate = DateTime.Now;

                obj.BillDocumentKey = long.Parse(obj.FinancialYear.ToString().Substring(2, 2)
                    + obj.BusinessKey.ToString().PadLeft(2, '0')
                    + dc_bl.DocumentId.ToString().PadLeft(3, '0')
                    + dc_bl.CurrentDocNumber.ToString());

                obj.BillType = VoucherTransactionType.Op_Billing;



                GtEfpbsh bh = new GtEfpbsh
                {
                    BusinessKey = obj.BusinessKey,
                    FinancialYear = obj.FinancialYear,
                    DocumentId = dc_bl.DocumentId,
                    DocumentNumber = dc_bl.CurrentDocNumber,
                    DocumentDate = DateTime.Now,
                    BillType = obj.BillType,
                    SuspendBillKey = obj.BillDocumentKey,
                    RUhid = obj.UHID,
                    Opnumber = obj.OPNumber,
                    PackageId = obj.PackageId,
                    TransCurrencyCode = obj.TransCurrencyCode,
                    LocalCurrencyCode = obj.LocalCurrencyCode,
                    ExchangeRate = obj.ExchangeRate,
                    ConcessionOn = obj.ConcessionOn,
                    TotalBillAmount = obj.TotalBillAmount,
                    TotalDiscountAmount = obj.TotalDiscountAmount,
                    TotalConcessionAmount = obj.TotalConcessionAmount,
                    RoundOff = obj.RoundOff,
                    NetBillAmount = obj.NetBillAmount,
                    DiscountPatient = 0,
                    DiscountInsurance = 0,
                    Narration = obj.Narration,
                    Status = obj.SuspendTypeCode == "L" ? "L" : "F",
                    SuspendType = obj.SuspendTypeId,
                    ActiveStatus = true,
                    FormId = obj.FormID,
                    CreatedBy = obj.UserID,
                    CreatedOn = DateTime.Now,
                    CreatedTerminal = obj.TerminalID
                };
                _context.GtEfpbsh.Add(bh);

                short sNo = 1;
                foreach (var s in obj.l_PatientBillDetails)
                {
                    GtEfpbsd bd = new GtEfpbsd
                    {
                        BusinessKey = bh.BusinessKey,
                        SuspendBillKey = bh.SuspendBillKey,
                        SerialNumber = sNo++,
                        ServiceDate = DateTime.Now,
                        ServiceTypeId = s.ServiceTypeId,
                        ServiceId = s.ServiceId,
                        ServiceProviderType = s.ServiceProviderType,
                        ServiceProviderId = s.ServiceProviderId,
                        ServiceRule = s.ServiceRule,
                        BaseRate = s.BaseRate,
                        Quantity = s.Quantity,
                        Rate = s.Rate,
                        DiscountAmount = s.DiscountAmount,
                        ConcessionAmount = s.ConcessionAmount,
                        TotalAmount = s.TotalAmount,
                        PayableByPatient = s.PayableByPatient,
                        PayableByInsurance = s.PayableByInsurance,
                        ChargableToPatient = s.ChargableToPatient,
                        ActiveStatus = true,
                        FormId = obj.FormID,
                        CreatedBy = obj.UserID,
                        CreatedOn = DateTime.Now,
                        CreatedTerminal = obj.TerminalID
                    };
                    _context.GtEfpbsd.Add(bd);
                }

                if(obj.SuspendTypeCode == Suspend.TaskCode.bill_Concession_Code)
                {
                    var s_task = await _context.GtEcfmap.Where(w => w.BusinessKey == obj.BusinessKey
                            // && w.FormId == obj.FormID
                             && w.TaskId == Suspend.TaskId.bill_Concession_id
                             && w.ApprovalLevelStage == 1 
                             && (w.ApprovalRangeFrom == 0 || obj.TotalConcessionAmount >= w.ApprovalRangeFrom)
                             && (w.ApprovalRangeTo == 0 || obj.TotalConcessionAmount <= w.ApprovalRangeTo)
                             && w.ActiveStatus)
                         .OrderBy(o => o.ApproverPriority)
                         .FirstOrDefaultAsync();

                    if(s_task != null)
                    {
                        GtEcapms b_task = new GtEcapms
                        {
                            BusinessKey = obj.BusinessKey,
                            ApprovalType = obj.SuspendTypeCode,
                            KeyReference = obj.BillDocumentKey,
                            SerialNumber = 1,
                            TaskId = s_task.TaskId,
                            ApprovalLevelStage = s_task.ApprovalLevelStage,
                            ApproverPriority = s_task.ApproverPriority,
                            UserRole = s_task.UserRole,
                            ApprovalRangeFrom = s_task.ApprovalRangeFrom,
                            ApprovalRangeTo = s_task.ApprovalRangeTo,
                            ApprovalStatus = Suspend.ApprovalStatus.bill_ForApproval,
                            ActiveStatus = true,
                            FormId = obj.FormID,
                            CreatedBy = obj.UserID,
                            CreatedOn = DateTime.Now,
                            CreatedTerminal = obj.TerminalID
                        };

                        _context.GtEcapms.Add(b_task);

                    }

                }

                bool warning = false;
                string warningMessage = "";

                await _context.SaveChangesAsync();
                dbContext.Commit();
                return new DO_ResponseParameter { Status = true, Key = obj.BillDocumentKey, Warning = warning, WarningMessage = warningMessage, };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DO_OpBillDetail>> GetSuspendOpBillListForConfirmation(int businessKey, DateTime billFromDate, DateTime billTillDate)
        {
            var op_bill = await _context.GtEfpbsh
               .Join(_context.GtEfoppr,
                   b => new { b.BusinessKey, b.RUhid },
                   p => new { p.BusinessKey, p.RUhid },
                   (b, p) => new { b, p })
              //.AsNoTracking()
              .Where(w => w.b.BusinessKey == businessKey
                    && w.b.Status != "A"
                    && w.b.DocumentDate.Date >= billFromDate.Date
                    && w.b.DocumentDate.Date <= billTillDate.Date
                   && w.b.ActiveStatus)
               .Select(s => new DO_OpBillDetail
               {
                   BillDocumentKey = s.b.SuspendBillKey,
                   DocumentDate = s.b.DocumentDate,
                   UHID = s.b.RUhid,
                   PatientName = s.p.FirstName +" " +s.p.LastName,
                   OPNumber = s.b.Opnumber,
                   BillType = s.b.BillType,
                   TransCurrencyCode = s.b.TransCurrencyCode,
                   TotalBillAmount = s.b.TotalBillAmount,
                   TotalDiscountAmount = s.b.TotalDiscountAmount,
                   TotalConcessionAmount = s.b.TotalConcessionAmount,
                   NetBillAmount = s.b.NetBillAmount,
                   SuspendStatus = s.b.Status
               }).ToListAsync();

            return op_bill;
        }

        public async Task<DO_PatientBillHeader> GetSuspendOpBillHeader(int businessKey, long suspendBillKey)
        {


            var s_bill = await _context.GtEfpbsh
              .Where(w => w.BusinessKey == businessKey
                   && w.ActiveStatus)
               .Select(r => new DO_PatientBillHeader
               {
                  DocumentDate = r.DocumentDate,
                  TotalBillAmount = r.TotalBillAmount,
                  TotalConcessionAmount = r.TotalConcessionAmount,
                  TotalDiscountAmount = r.TotalDiscountAmount,
                  NetBillAmount = r.NetBillAmount,
                  Narration = r.Narration,
                  SuspendStatus = r.Status
               }).FirstOrDefaultAsync();

            return s_bill;
        }

        public async Task<List<DO_PatientBillDetails>> GetSuspendOpBillDetails(int businessKey, long suspendBillKey)
        {
           

            var s_bill = await _context.GtEfpbsd
               .Join(_context.GtEssrms,
                   b => new { b.ServiceId },
                   s => new { s.ServiceId },
                   (b, s) => new { b, s })
              .Where(w => w.b.BusinessKey == businessKey
                    && w.b.SuspendBillKey == suspendBillKey
                   && w.b.ActiveStatus)
               .Select(r => new DO_PatientBillDetails
               {
                   ServiceId = r.b.ServiceId,
                   ServiceDesc = r.s.ServiceDesc,
                   ServiceProviderType = r.b.ServiceProviderType,
                   ServiceProviderId = r.b.ServiceProviderId,
                   ServiceRule = r.b.ServiceRule,
                   BaseRate = r.b.Rate,
                   Quantity = r.b.Quantity,
                   Rate = r.b.Rate,
                   DiscountAmount = r.b.DiscountAmount,
                   TotalAmount = r.b.TotalAmount,
               }).ToListAsync();

            return s_bill;
        }
    }
}
