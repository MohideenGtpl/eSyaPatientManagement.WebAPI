using System;
using System.Collections.Generic;
using System.Text;

namespace eSyaPatientManagement.DO
{
    public class DO_PatientBillHeader
    {
        public int BusinessKey { get; set; }
        public int FinancialYear { get; set; }
        public int DocumentID { get; set; }
        public int DocumentNumber { get; set; }
        public DateTime DocumentDate { get; set; }
        public string BillType { get; set; }
        public long BillDocumentKey { get; set; }
        public long UHID { get; set; }
        public string PatientID { get; set; }
        public string PatientName { get; set; }
        public string PrimaryMemberName { get; set; }
        public long OPNumber { get; set; }
        public int? PackageId { get; set; }
        public string TransCurrencyCode { get; set; }
        public string LocalCurrencyCode { get; set; }
        public decimal ExchangeRate { get; set; }
        public string ConcessionOn { get; set; }
        public decimal TotalBillAmount { get; set; }
        public decimal TotalDiscountAmount { get; set; }
        public decimal DiscountPatient { get; set; }
        public decimal DiscountInsurance { get; set; }
        public decimal DiscountEmployee { get; set; }
        public decimal TotalConcessionAmount { get; set; }
        public decimal RoundOff { get; set; }
        public decimal NetBillAmount { get; set; }
        public string Narration { get; set; }
        public int? BillFinalBy { get; set; }
        public DateTime? BillFinalOn { get; set; }
        public bool IsFinancePosted { get; set; }
        public DateTime? FinancePostedDate { get; set; }
        public string FormID { get; set; }
        public int UserID { get; set; }
        public string TerminalID { get; set; }
        public int SuspendTypeId { get; set; }
        public string SuspendTypeCode { get; set; }
        public string SuspendStatus { get; set; }

        public List<DO_PatientBillDetails> l_PatientBillDetails { get; set; }
        public List<DO_PatientBillLedger> l_SubLedgerDetails { get; set; }
        public List<DO_PaymentReceipt> l_PaymentReceipt { get; set; }

        public long BookingKey { get; set; }

    }

    public class DO_PatientBillDetails
    {
        public int BusinessKey { get; set; }
        public long BillDocumentKey { get; set; }
        public int SerialNumber { get; set; }
        public int ServiceDate { get; set; }
        public int ServiceTypeId { get; set; }
        public int ServiceId { get; set; }
        public string ServiceDesc { get; set; }
        public string ServiceName { get; set; }
        //public int? DrugCode { get; set; }
        //public string DrugDescription { get; set; }
        //public decimal? PreReceiptKey { get; set; }
        //public decimal? PreReceiptSerialNumber { get; set; }
        //public string PreBatchNumber { get; set; }
        //public string BatchNumber { get; set; }
        //public string ExpiryDate { get; set; }
        //public decimal? ReceiptKey { get; set; }
        //public decimal? ReceiptSerialNumber { get; set; }
        public string ServiceProviderType { get; set; }
        public int? ServiceProviderId { get; set; }
        public string ServiceRule { get; set; }
        public decimal BaseRate { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal ConcessionAmount { get; set; }
        public decimal TotalAmount { get; set; }

        public decimal PayableByPatient { get; set; }
        public decimal PayableByInsurance { get; set; }
        public bool ChargableToPatient { get; set; }
    }

    public class DO_PatientBillSummary
    {
        public int BusinessKey { get; set; }
        public long BillDocumentKey { get; set; }
        public decimal CollectedAmount { get; set; }
        public decimal AdvanceAmount { get; set; }
        public decimal DuesSettledAmount { get; set; }
        public decimal CancellationAmount { get; set; }
        public decimal RefundAmount { get; set; }
        public decimal SettlConcession { get; set; }
        public decimal PostSettlConcession { get; set; }
    }

    public class DO_PatientBillLedger
    {
        public int BusinessKey { get; set; }
        public long BillDocumentKey { get; set; }
        public int SerialNumber { get; set; }
        public string SubledgerType { get; set; }
        public int? SubledgerCode { get; set; }
        public string PayerScheme { get; set; }
        public string MemberId { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public DateTime? AcknowledgeDate { get; set; }
        public decimal PayerPercentage { get; set; }
        public decimal PayableAmount { get; set; }
        public decimal CRNAmount { get; set; }
        public decimal AdvanceAdjAmount { get; set; }
        public decimal ReceivedAmount { get; set; }
    }
}
