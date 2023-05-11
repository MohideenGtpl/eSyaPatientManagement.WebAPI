using System;
using System.Collections.Generic;

namespace eSyaPatientManagement.DL.Entities
{
    public partial class GtEfpbsh
    {
        public int BusinessKey { get; set; }
        public int FinancialYear { get; set; }
        public int DocumentId { get; set; }
        public int DocumentNumber { get; set; }
        public DateTime DocumentDate { get; set; }
        public long SuspendBillKey { get; set; }
        public string BillType { get; set; }
        public long RUhid { get; set; }
        public long Opnumber { get; set; }
        public int? PackageId { get; set; }
        public string TransCurrencyCode { get; set; }
        public string LocalCurrencyCode { get; set; }
        public decimal ExchangeRate { get; set; }
        public string ConcessionOn { get; set; }
        public decimal TotalBillAmount { get; set; }
        public decimal TotalDiscountAmount { get; set; }
        public decimal TotalConcessionAmount { get; set; }
        public decimal RoundOff { get; set; }
        public decimal NetBillAmount { get; set; }
        public decimal DiscountPatient { get; set; }
        public decimal DiscountInsurance { get; set; }
        public string Narration { get; set; }
        public string Status { get; set; }
        public int SuspendType { get; set; }
        public bool ActiveStatus { get; set; }
        public string FormId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedTerminal { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedTerminal { get; set; }
    }
}
