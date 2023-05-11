using System;
using System.Collections.Generic;

namespace eSyaPatientManagement.DL.Entities
{
    public partial class GtEsspct
    {
        public int BusinessKey { get; set; }
        public int SpecialtyId { get; set; }
        public int ClinicId { get; set; }
        public int RateType { get; set; }
        public int ClinicType { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public string ServiceRule { get; set; }
        public decimal ConsultationTariff { get; set; }
        public decimal TeleConsultationTariff { get; set; }
        public decimal RevisitConsultationTariff { get; set; }
        public int RevisitRule { get; set; }
        public int SecRevisitRule { get; set; }
        public decimal SecRevisitConsultationTariff { get; set; }
        public DateTime? EffectiveTill { get; set; }
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
