using System;
using System.Collections.Generic;

namespace eSyaPatientManagement.DL.Entities
{
    public partial class GtMaptrg
    {
        public int Isdcode { get; set; }
        public string MobileNumber { get; set; }
        public int MemberId { get; set; }
        public int RelationShip { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public bool IsDobapplicable { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int AgeYy { get; set; }
        public int AgeMm { get; set; }
        public int AgeDd { get; set; }
        public string EMailId { get; set; }
        public int ReferredSource { get; set; }
        public string Password { get; set; }
        public int? SecurityQuestionId { get; set; }
        public string SecurityAnswer { get; set; }
        public string FcmregistrationToken { get; set; }
        public DateTime? FcmcreatedOn { get; set; }
        public int? CustomerCode { get; set; }
        public int? AddressTag { get; set; }
        public string RegSource { get; set; }
        public long? RUhid { get; set; }
        public bool IsChatApplicable { get; set; }
        public bool IsGuideApplicable { get; set; }
        public string AppDeviceId { get; set; }
        public bool AppSignIn { get; set; }
        public DateTime? LastAppSignInDate { get; set; }
        public DateTime? LastAppSignOutDate { get; set; }
        public bool ActiveStatus { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedTerminal { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedTerminal { get; set; }
    }
}
