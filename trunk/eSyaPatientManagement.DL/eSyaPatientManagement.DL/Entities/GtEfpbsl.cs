using System;
using System.Collections.Generic;

namespace eSyaPatientManagement.DL.Entities
{
    public partial class GtEfpbsl
    {
        public int BusinessKey { get; set; }
        public long BillDocumentKey { get; set; }
        public int SerialNumber { get; set; }
        public string SubledgerType { get; set; }
        public int? SubledgerId { get; set; }
        public string PayerScheme { get; set; }
        public string MemberId { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public DateTime? AcknowledgeDate { get; set; }
        public decimal PayerPercentage { get; set; }
        public decimal PayableAmount { get; set; }
        public decimal Crnamount { get; set; }
        public decimal Drnamount { get; set; }
        public decimal AdvanceAdjAmount { get; set; }
        public decimal ReceivedAmount { get; set; }
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
