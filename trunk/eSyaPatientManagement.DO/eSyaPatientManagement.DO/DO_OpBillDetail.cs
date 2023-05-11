using System;
using System.Collections.Generic;
using System.Text;

namespace eSyaPatientManagement.DO
{
    public class DO_OpBillDetail
    {

        public long BillDocumentKey { get; set; }
        public DateTime DocumentDate { get; set; }
        public string BillType { get; set; }
        public long UHID { get; set; }
        public string PatientID { get; set; }
        public string PatientName { get; set; }
        public string PrimaryMemberName { get; set; }
        public long OPNumber { get; set; }
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
        public string SuspendStatus { get; set; }
    }

    public class DO_OpBillServices
    {

        public long BillDocumentKey { get; set; }
        public int ServiceTypeId { get; set; }
        public int ServiceId { get; set; }
        public string ServiceDesc { get; set; }
        public string ServiceProviderType { get; set; }
        public int? ServiceProviderId { get; set; }
        public string ServiceProviderName { get; set; }
        public string ServiceRule { get; set; }
        public decimal BaseRate { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
