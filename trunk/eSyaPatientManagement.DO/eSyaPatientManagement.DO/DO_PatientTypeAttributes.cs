using System;
using System.Collections.Generic;
using System.Text;

namespace eSyaPatientManagement.DO
{
    public class DO_PatientTypCategoryAttribute
    {
        public int PatientTypeId { get; set; }
        public int PatientCategoryId { get; set; }
        public bool GenerateInstantBill { get; set; }
        public bool GenerateOpenBill { get; set; }
        public bool ActiveStatus { get; set; }
        public string FormID { get; set; }
        public int UserID { get; set; }
        public string TerminalID { get; set; }
        public string Description { get; set; }
        public List<DO_eSyaParameter> l_ptypeparams { get; set; }
    }
    public class DO_PatientAttributes
    {
        public List<DO_PatientTypeAttribute> l_PatientType { get; set; }
        public List<DO_PatientTypCategoryAttribute> l_PatienTypeCategory { get; set; }
    }
    public class DO_PatientTypeAttribute
    {
        public int PatientTypeId { get; set; }
        public string Description { get; set; }
        public bool ActiveStatus { get; set; }
    }
    public class DO_ApplicationCodes
    {
        public int ApplicationCode { get; set; }
        public int CodeType { get; set; }
        public string CodeDesc { get; set; }
    }
}
