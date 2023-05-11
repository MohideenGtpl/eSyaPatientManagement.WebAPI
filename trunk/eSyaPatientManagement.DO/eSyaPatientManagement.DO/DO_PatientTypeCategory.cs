using System;
using System.Collections.Generic;
using System.Text;

namespace eSyaPatientManagement.DO
{
    public class DO_PatientType
    {
        public int PatientTypeID { get; set; }
        public string PatientTypeDesc { get; set; }
    }
    public class DO_PatientCategory
    {
        public int PatientCategoryID { get; set; }
        public string PatientCategoryDesc { get; set; }
        public bool TreatmentAllowedOP { get; set; }
        public bool IsSpecialtyLink { get; set; }
        public bool CareCardApplicable { get; set; }
        public string ServiceWiseRateType { get; set; }
        public int RateType { get; set; }
        public string RateTypeDesc { get; set; }
        public bool ValidateDocument { get; set; }
    }
}
