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
    public class BillingTransactionRepository : IBillingTransactionRepository
    {
        public async Task<DO_ResponseParameter> InsertBillingTransaction(object context, DO_PatientBillHeader obj)
        {
            try
            {
                var db = (eSyaEnterpriseContext)context;
                obj.FinancialYear = (int)db.GtEcclco.Where(w => DateTime.Now.Date >= w.FromDate.Date
                        && DateTime.Now.Date <= w.TillDate.Date).First().FinancialYear;

                var dc_bl = await db.GtDncn02
                  .Where(w => w.BusinessKey == obj.BusinessKey && w.FinancialYear == obj.FinancialYear
                      && w.DocumentId == DocumentIdValues.OpBilling_Combined_id).FirstOrDefaultAsync();
                dc_bl.CurrentDocNumber = dc_bl.CurrentDocNumber + 1;
                dc_bl.CurrentDocDate = DateTime.Now;
                obj.BillDocumentKey = long.Parse(obj.FinancialYear.ToString().Substring(2, 2) 
                    + obj.BusinessKey.ToString().PadLeft(2, '0')
                    + dc_bl.DocumentId.ToString().PadLeft(3, '0')
                    + dc_bl.CurrentDocNumber.ToString());

                GtEfpbhd bh = new GtEfpbhd
                {
                    BusinessKey = obj.BusinessKey,
                    FinancialYear = obj.FinancialYear,
                    DocumentId = dc_bl.DocumentId,
                    DocumentNumber = dc_bl.CurrentDocNumber,
                    DocumentDate = DateTime.Now,
                    BillType = obj.BillType,
                    BillDocumentKey = obj.BillDocumentKey,
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
                    IsFinancePosted = false,
                    SmartCardSubmission = false,
                    BillStatus = "O",
                    PrintBillKey = obj.BillDocumentKey.ToString(),
                    ActiveStatus = true,
                    FormId = obj.FormID,
                    CreatedBy = obj.UserID,
                    CreatedOn = System.DateTime.Now,
                    CreatedTerminal = obj.TerminalID
                };
                db.GtEfpbhd.Add(bh);

                short sNo = 1;
                foreach (var s in obj.l_PatientBillDetails)
                {
                    GtEfpbdt bd = new GtEfpbdt
                    {
                        BusinessKey = bh.BusinessKey,
                        BillDocumentKey = bh.BillDocumentKey,
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
                    db.GtEfpbdt.Add(bd);
                }

                var p_collectedAmount = db.GtEfprdt.Where(w => w.BusinessKey == obj.BusinessKey && w.BillDocumentKey == obj.BillDocumentKey 
                        && w.VoucherType == "R" && w.ActiveStatus).Sum(w => w.VoucherAmount);

                var p_RefundAmount = db.GtEfprdt.Where(w => w.BusinessKey == obj.BusinessKey && w.BillDocumentKey == obj.BillDocumentKey
                        && w.VoucherType == "P" && w.ActiveStatus).Sum(w => w.VoucherAmount);

                GtEfpbps bs = new GtEfpbps
                {
                    BusinessKey = obj.BusinessKey,
                    BillDocumentKey = obj.BillDocumentKey,
                    CollectedAmount = p_collectedAmount,
                    AdvanceAmount = 0,
                    DuesSettledAmount = 0,
                    CancellationAmount = 0,
                    RefundAmount = p_RefundAmount,
                    SettlConcession = 0,
                    PostSettlConcession = 0,
                    ActiveStatus = true,
                    FormId = obj.FormID,
                    CreatedBy = obj.UserID,
                    CreatedOn = DateTime.Now,
                    CreatedTerminal = obj.TerminalID
                };
                db.GtEfpbps.Add(bs);

                short sNo_ledger = 1;
                foreach (var s in obj.l_SubLedgerDetails)
                {
                    GtEfpbsl sl_Patient = new GtEfpbsl
                    {
                        BusinessKey = obj.BusinessKey,
                        BillDocumentKey = obj.BillDocumentKey,
                        SerialNumber = sNo_ledger++,
                        SubledgerType = s.SubledgerType,
                        SubledgerId = s.SubledgerCode,
                        PayerPercentage = s.PayerPercentage,
                        PayableAmount = s.PayableAmount,
                        Crnamount = 0,
                        AdvanceAdjAmount = s.AdvanceAdjAmount,
                        ReceivedAmount = s.ReceivedAmount,
                        ActiveStatus = true,
                        FormId = obj.FormID,
                        CreatedBy = obj.UserID,
                        CreatedOn = DateTime.Now,
                        CreatedTerminal = obj.TerminalID
                    };
                    db.GtEfpbsl.Add(sl_Patient);
                }

                db.SaveChanges();

                return new DO_ResponseParameter() { Status = true, Key = obj.BillDocumentKey };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
