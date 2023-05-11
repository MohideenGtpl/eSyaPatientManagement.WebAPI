using System;
using System.Collections.Generic;

namespace eSyaPatientManagement.DL.Entities
{
    public partial class GtEcapms
    {
        public int BusinessKey { get; set; }
        public string ApprovalType { get; set; }
        public long KeyReference { get; set; }
        public int SerialNumber { get; set; }
        public int TaskId { get; set; }
        public int ApprovalLevelStage { get; set; }
        public int ApproverPriority { get; set; }
        public int UserRole { get; set; }
        public decimal ApprovalRangeFrom { get; set; }
        public decimal ApprovalRangeTo { get; set; }
        public string ApprovalStatus { get; set; }
        public int? ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public int? RefSerialNumber { get; set; }
        public DateTime? ReassignedDate { get; set; }
        public bool ActiveStatus { get; set; }
        public string FormId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedTerminal { get; set; }
    }
}
