using System;
using System.Collections.Generic;
using System.Text;

namespace eSyaPatientManagement.DO
{
    public class DO_PatientOpVisitDetails
    {
        public long UHID { get; set; }
        public long OpNumber { get; set; }
        public int SerialNumber { get; set; }
        public DateTime VisitDate { get; set; }
        public string PatientName { get; set; }
        public string Gender { get; set; }
        public string Age { get; set; }
        public int PatientType { get; set; }
        public string PatientTypeDesc { get; set; }
        public int PatientCategory { get; set; }
        public string PatientCategoryDesc { get; set; }
        public int RatePlan { get; set; }
        public string RatePlanDesc { get; set; }
        public int SpecialtyId { get; set; }
        public string SpecialtyDesc { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
    }
}
