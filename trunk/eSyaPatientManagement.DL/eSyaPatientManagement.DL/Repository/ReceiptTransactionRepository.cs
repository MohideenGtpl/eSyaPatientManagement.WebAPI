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
    public class ReceiptTransactionRepository: IReceiptTransactionRepository
    {
        public async Task<DO_ResponseParameter> InsertPatientReceipt(object context, DO_PaymentReceipt obj)
        {

            try
            {
                var db = (eSyaEnterpriseContext) context;

                obj.FinancialYear = (int)db.GtEcclco.Where(w => DateTime.Now.Date >= w.FromDate.Date
                     && DateTime.Now.Date <= w.TillDate.Date).First().FinancialYear;

                obj.VoucherDate = DateTime.Now;

                int paymentId = db.GtEcpyid.Where(w => w.PaymentModeId == obj.PaymentModeID
                                   && w.PaymentModeCategoryId == obj.PaymentModeCategoryID
                                   && w.ActiveStatus).First().PaymentId;

                int voucherId = db.GtEcvyid.Where(w => w.TransactionType == obj.TransactionType).First().VoucherId;

                var vch = await db.GtDnvcdt.Where(w => w.BusinessKey == obj.BusinessKey && w.FinancialYear == obj.FinancialYear
                                    && w.PaymentId == paymentId && w.VoucherId == voucherId && w.VoucherType == obj.VoucherType).FirstAsync();

                vch.CurrentVoucherNumber++;
                vch.CurrentVoucherDate = DateTime.Now;
                obj.VoucherDate = System.DateTime.Now;
                obj.VoucherKey = decimal.Parse(obj.FinancialYear.ToString().Substring(2, 2) +
                    obj.BusinessKey.ToString() +
                    paymentId.ToString().PadLeft(2, '0') +
                    voucherId.ToString().PadLeft(2, '0') +
                    vch.CurrentVoucherNumber);

                GtEfprdt obj_PR = new GtEfprdt
                {
                    BusinessKey = obj.BusinessKey,
                    FinancialYear = obj.FinancialYear,
                    BookTypeId = Convert.ToInt32(paymentId.ToString().PadLeft(2, '0') + voucherId.ToString().PadLeft(2, '0') + (obj.VoucherType == "R"?"0":"1")),
                    VoucherNumber = vch.CurrentVoucherNumber,
                    VoucherKey = obj.VoucherKey,
                    VoucherType = obj.VoucherType,
                    VoucherDate = obj.VoucherDate,
                    VoucherAmount = obj.VoucherAmount,
                    CollectedAmount = obj.CollectedAmount,
                    RefundAmount = obj.RefundAmount,
                    CancelledAmount = obj.CancelledAmount,
                    Narration = obj.Narration,
                    BillDocumentKey = obj.BillDocumentKey,
                    ActiveStatus = true,
                    FormId = obj.FormID,
                    CreatedBy = obj.UserID,
                    CreatedOn = DateTime.Now,
                    CreatedTerminal = Environment.MachineName,
                };
                db.GtEfprdt.Add(obj_PR);

                GtEfprpm gt_PM = new GtEfprpm
                {
                    BusinessKey = obj_PR.BusinessKey,
                    VoucherKey = obj.VoucherKey,
                    PaymentMode = obj.PaymentModeID,
                    CardType = obj.CardType,
                    CardNumber = obj.CardNumber,
                    CardExpiryDate = obj.CardExpiryDate,
                    ApprovalNumber = obj.ApprovalNumber,
                    ReferenceNumber = obj.ReferenceNumber,
                    MpreferenceNumber = obj.MPReferenceNumber,
                    MpreferenceDate = obj.MPReferenceDate,
                    ChequeNumber = obj.ChequeNumber,
                    ChequeDate = obj.ChequeDate,
                    DraweeBank = obj.DraweeBank,

                    ActiveStatus = true,
                    CreatedBy = obj.UserID,
                    CreatedOn = DateTime.Now,
                    CreatedTerminal = Environment.MachineName
                };
                db.GtEfprpm.Add(gt_PM);

                var ps = await db.GtEfpbps.Where(w => w.BusinessKey == obj.BusinessKey && w.BillDocumentKey == obj.BillDocumentKey).FirstOrDefaultAsync();
                if(ps != null)
                {
                    if (ps.CreatedOn.Date == obj.VoucherDate.Date)
                        ps.CollectedAmount = ps.CollectedAmount + obj.VoucherAmount;
                    else
                        ps.DuesSettledAmount = ps.DuesSettledAmount + obj.VoucherAmount;

                    ps.ModifiedBy = obj.UserID;
                    ps.ModifiedOn = DateTime.Now;
                    ps.ModifiedTerminal = Environment.MachineName;
                }

                await db.SaveChangesAsync();

                return new DO_ResponseParameter { Status = true };

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
    }
}
