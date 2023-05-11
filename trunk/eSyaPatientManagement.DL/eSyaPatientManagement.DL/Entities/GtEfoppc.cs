using System;
using System.Collections.Generic;

namespace eSyaPatientManagement.DL.Entities
{
    public partial class GtEfoppc
    {
        public int BusinessKey { get; set; }
        public long RUhid { get; set; }
        public long Opnumber { get; set; }
        public int PatientType { get; set; }
        public int PatientCategory { get; set; }
        public int RatePlan { get; set; }
        public bool ActiveStatus { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedTerminal { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedTerminal { get; set; }

        public virtual GtEfopvd GtEfopvd { get; set; }
    }
}
