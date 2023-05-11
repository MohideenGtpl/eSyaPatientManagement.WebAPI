using System;
using System.Collections.Generic;
using System.Text;

namespace eSyaPatientManagement.DO
{
    public class DO_PatientFODeskList
    {
       public  List<DO_AppointmentBooked> L_Appointment { get; set; }
        public List<DO_PatientPreRegistered> L_PreRegistered { get; set; }
        public List<DO_PatientProfile> L_RegisteredPatient { get; set; }
    }
   public class DO_OPRegistrationVisit
    {
        public bool IsNewRegn { get; set; }
        public long UHID { get; set; }
        public DateTime VisitDate { get; set; }
        public int SerialNumber { get; set; }
        public int BusinessKey { get; set; }
        public string RegistrationType { get; set; }
        public string PatientClass { get; set; }
        public string VisitType { get; set; }
        public string VisitClass { get; set; }
        public string VisitCategory { get; set; }
        public decimal VDKey { get; set; }
        public int ClinicId { get; set; }
        public int ConsultationId { get; set; }
        public int PatientType { get; set; }
        public int PatientCategory { get; set; }
        public int RatePlan { get; set; }
        public long? AppointmentKey { get; set; }
        public bool IsVIP { get; set; }
        public bool IsMLC { get; set; }
        public bool IsRecommendedByStaff { get; set; }
        public string RecommenderName { get; set; }
        public bool IsStaff { get; set; }
        public bool IsStaffDependent { get; set; }
        public bool IsStudent { get; set; }
        public int SourceOfReference { get; set; }
        public int SubSOR { get; set; }
        public string BillStatus { get; set; }

        public DO_PatientProfile PatientProfile { get; set; }

        public DO_PatientPassport PatientPassport { get; set; }

        public DO_PatientNextToKIN PatientNextToKIN { get; set; }

        public DO_ConsultationInfo ConsultationInfo { get; set; }

        public List<DO_OPPayer> L_OPPayer { get; set; }

        public DO_PatientBillHeader O_PatientBill { get; set; }

        public List<DO_PaymentReceipt> O_PaymentReceipt { get; set; }

        public long? Prkey { get; set; }
        public string TokenKey { get; set; }
        public bool ActiveStatus { get; set; }
        public string FormID { get; set; }
        public int UserID { get; set; }
        public string TerminalID { get; set; }
    }

    public class DO_PatientPassport
    {
        public bool IsPPScanned { get; set; }
        public string PassportNumber { get; set; }
        public DateTime DateOfIssue { get; set; }
        public string PlaceOfIssue { get; set; }
        public DateTime PassportExpiresOn { get; set; }
        public int VisaType { get; set; }
        public DateTime VisaIssueDate { get; set; }
        public DateTime VisaExpiryDate { get; set; }
    }

    public class DO_PatientNextToKIN
    {
        public string KINName { get; set; }
        public int KINRelationship { get; set; }
        public int ISDCode { get; set; }
        public string KINMobileNumber { get; set; }
        public string KINContactAddress { get; set; }
    }

    public class DO_OPPayer
    {
        public int Payer { get; set; }
        public bool IsPrimaryPayer { get; set; }
        public int RatePlan { get; set; }
        public int SchemePlan { get; set; }
        public string MemberID { get; set; }
        public decimal CoPayPerc { get; set; }
    }

    public class DO_ConsultationInfo
    {
        public int ClinicID { get; set; }
        public int ConsultationID { get; set; }
        public int SpecialtyID { get; set; }
        public int DoctorID { get; set; }
        public string Episode { get; set; }
        public string Complaint { get; set; }
        public string CaseType { get; set; }
        public bool IsDiabetic { get; set; }
        public bool IsHypertensive { get; set; }
    }


}

