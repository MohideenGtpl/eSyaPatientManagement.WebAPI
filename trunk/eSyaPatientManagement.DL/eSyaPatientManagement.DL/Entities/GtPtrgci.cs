using System;
using System.Collections.Generic;

namespace eSyaPatientManagement.DL.Entities
{
    public partial class GtPtrgci
    {
        public int BusinessKey { get; set; }
        public long RUhid { get; set; }
        public long Opnumber { get; set; }
        public int SerialNumber { get; set; }
        public int ClinicId { get; set; }
        public int ConsultationId { get; set; }
        public int SpecialtyId { get; set; }
        public int DoctorId { get; set; }
        public string Episode { get; set; }
        public string Complaint { get; set; }
        public string CaseType { get; set; }
        public bool IsDiabetic { get; set; }
        public bool IsHypertensive { get; set; }
        public int? CrossRefDoctor { get; set; }
        public int? CrossRefSpecialty { get; set; }
        public long? BillDocumentKey { get; set; }
        public bool ActiveStatus { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedTerminal { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedTerminal { get; set; }
    }
}
