using System;
using System.Collections.Generic;
using System.Text;

namespace eSyaPatientManagement.DO
{
    public class DO_PaymentReceipt
    {
        public int BusinessKey { get; set; }
        public int FinancialYear { get; set; }
        public int BookTypeId { get; set; }
        public int VoucherNumber { get; set; }
        public decimal VoucherKey { get; set; }
        public string VoucherType { get; set; }
        public string VoucherTypeDesc { get; set; }
        public DateTime VoucherDate { get; set; }
        public decimal VoucherAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal ConcessionAmount { get; set; }
        public decimal TotalNetAmount { get; set; }
        public decimal CollectedAmount { get; set; }
        public decimal CancelledAmount { get; set; }
        public decimal RefundAmount { get; set; }
        public decimal BillDocumentKey { get; set; }
        public string PaidorCollectedBy { get; set; }
        public string RefundReason { get; set; }
        public string Narration { get; set; }
        public decimal AdvanceAdjAmount { get; set; }
        public decimal? RefVoucherKey { get; set; }
        public DateTime? RefVoucherDate { get; set; }

        public string TransactionType { get; set; }
        public int PaymentModeID { get; set; }
        public string PaymentModeDesc { get; set; }
        public int PaymentModeCategoryID { get; set; }
        public string PaymentModeCategoryDesc { get; set; }
        public string CardType { get; set; }
        public string CardNumber { get; set; }
        public string CardExpiryDate { get; set; }
        public string ApprovalNumber { get; set; }
        public string ReferenceNumber { get; set; }
        public string MPReferenceNumber { get; set; }
        public DateTime? MPReferenceDate { get; set; }
        public string ChequeNumber { get; set; }
        public DateTime? ChequeDate { get; set; }
        public string DraweeBank { get; set; }

        public bool ActiveStatus { get; set; }
        public string FormID { get; set; }
        public int UserID { get; set; }
        public string TerminalID { get; set; }
    }
}
