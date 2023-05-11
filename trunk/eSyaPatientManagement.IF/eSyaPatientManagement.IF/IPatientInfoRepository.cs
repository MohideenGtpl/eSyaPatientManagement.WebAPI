using eSyaPatientManagement.DO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eSyaPatientManagement.IF
{
    public interface IPatientInfoRepository
    {
        Task<List<DO_PatientProfile>> GetSearchPatient(string searchText);

        Task<List<DO_PatientProfile>> GetPatientInfoRegistrationByMobileNo(string mobileNumber);

        Task<DO_PatientProfile> GetPatientProfileByUHID(long uhid);

        Task<DO_AppointmentDetails> getPatientbookingInfo(int businessKey, long appKey);

        Task<List<DO_PatientProfile>> GetPatientSearch(string searchText);

        Task<List<DO_PatientAppointmentDetail>> GetPatientByUHID(int businessKey, long uhid);
    }
}
