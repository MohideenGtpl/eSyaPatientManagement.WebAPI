using System;
using System.Collections.Generic;
using System.Text;

namespace eSyaPatientManagement.DO
{
   public class DO_PatientCategoryTypeBusinessLink
    {
        public int BusinessKey { get; set; }
        public int PatientTypeId { get; set; }
        public int PatientCategoryId { get; set; }
        public int RateType { get; set; }
        public bool ActiveStatus { get; set; }
        public string FormID { get; set; }
        public int UserID { get; set; }
        public string TerminalID { get; set; }
        public string PatientTypeDesc { get; set; }
        public string PatientCategoryDesc { get; set; }
    }
}
