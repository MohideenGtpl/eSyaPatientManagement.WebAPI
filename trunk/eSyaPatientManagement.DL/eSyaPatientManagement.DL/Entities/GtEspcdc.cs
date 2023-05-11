using System;
using System.Collections.Generic;

namespace eSyaPatientManagement.DL.Entities
{
    public partial class GtEspcdc
    {
        public int BusinessKey { get; set; }
        public int PatientCategory { get; set; }
        public string ServiceFor { get; set; }
        public int ServiceClass { get; set; }
        public string DiscountRule { get; set; }
        public decimal DiscountPercent { get; set; }
        public bool ActiveStatus { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedTerminal { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedTerminal { get; set; }
    }
}
