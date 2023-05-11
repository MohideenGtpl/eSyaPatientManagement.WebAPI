using System;
using System.Collections.Generic;

namespace eSyaPatientManagement.DL.Entities
{
    public partial class GtEapcms
    {
        public int PatientTypeId { get; set; }
        public int PatientCategoryId { get; set; }
        public bool TreatmentAllowedOp { get; set; }
        public bool TreatmentAllowedIp { get; set; }
        public bool IsSpecialtyLink { get; set; }
        public bool CareCardApplicable { get; set; }
        public string ServiceWiseRateType { get; set; }
        public int RateType { get; set; }
        public bool ValidateDocument { get; set; }
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
