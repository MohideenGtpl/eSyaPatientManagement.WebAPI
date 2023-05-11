using System;
using System.Collections.Generic;
using System.Text;

namespace eSyaPatientManagement.DO
{
    public class DO_PatientProfile
    {
        public long UHID { get; set; }
        public string PatientID { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int BusinessKey { get; set; }
        public int? Nationality { get; set; }
        public string NationalID { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public bool IsDOBApplicable { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int AgeYY { get; set; }
        public int AgeMM { get; set; }
        public int AgeDD { get; set; }
        public string BloodGroup { get; set; }
        public int ISDCode { get; set; }
        public string MobileNumber { get; set; }
        public string eMailID { get; set; }
        public string PatientStatus { get; set; }
        public int RecordStatus { get; set; }
        public DateTime? RegistrationValidTill { get; set; }
        public DO_PatientAddress CurrentPatientAddress { get; set; }

        public List<DO_PatientAddress> L_PatientAddress { get; set; }
    }

    public class DO_PatientAddress
    {
        public int AddressId { get; set; }
        public string AddressType { get; set; }
        public string Address { get; set; }
        public int AreaCode { get; set; }
        public int CityCode { get; set; }

        public int StateCode { get; set; }
        public string Pincode { get; set; }
        //public string AddressLine1 { get; set; }
        //public string AddressLine2 { get; set; }
        //public string AddressLine3 { get; set; }
    }
}
