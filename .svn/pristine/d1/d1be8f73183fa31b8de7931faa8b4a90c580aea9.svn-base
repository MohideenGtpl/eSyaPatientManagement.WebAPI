using eSyaPatientManagement.DO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eSyaPatientManagement.IF
{
    public interface IOpClinicDetailsRepository
    {
        Task<List<DO_AppointmentBooked>> GetAppointmentBookedByMobile(string mobileNumber);
        Task<List<DO_PatientPreRegistered>> GetPreRegistrationByMobile(string mobileNumber);
        Task<List<DO_PatientProfile>> GetRegisteredPatientByMobileNo(string mobileNumber);
        Task<List<DO_ClinicType>> GetClinicTypes(int businessKey);
        Task<List<DO_Specialty>> GetSpecialty(int businessKey);
        Task<List<DO_DoctorClinicSchedule>> GetDoctorScheduleListByClinicTypeSpecialtyDate(int businessKey,
           int clinicTypeId, int consultationTypeId,
           int specialtyId, DateTime scheduleDate);

        Task<List<DO_Doctors>> GetAllDoctorList(int businessKey);

    };
}
