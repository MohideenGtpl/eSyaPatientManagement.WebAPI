using System;
using System.Collections.Generic;
using System.Text;

namespace eSyaPatientManagement.DO
{
   public class DO_AppointmentBooked
    {
        public long AppointmentKey { get; set; }
        public long? UHID { get; set; }
        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
        public int ClinicId { get; set; }
        public int ConsultationId { get; set; }
        public int SpecialtyId { get; set; }
        public int DoctorId { get; set; }
    }

    public class DO_PatientPreRegistered
    {
        public long PRKey { get; set; }
        public string PatientFirstName { get; set; }
        public string PatientMiddleName { get; set; }
        public string PatientLastName { get; set; }
        public string Gender { get; set; }
        public int AgeYY { get; set; }
        public int AgeMM { get; set; }
        public int AgeDD { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string MaritalStatus { get; set; }
        public int ISDCode { get; set; }
        public string MobileNumber { get; set; }
        public string EmailID { get; set; }
        public int Nationality { get; set; }
        public int ReferredBy { get; set; }
        public string Address { get; set; }
        public int City { get; set; }
        public int State { get; set; }
        public string Pincode { get; set; }
        public int? AreaCode { get; set; }

    }
}
