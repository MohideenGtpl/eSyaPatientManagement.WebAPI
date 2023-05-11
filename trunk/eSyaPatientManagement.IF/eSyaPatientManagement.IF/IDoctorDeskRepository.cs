using eSyaPatientManagement.DO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eSyaPatientManagement.IF
{
    public interface IDoctorDeskRepository
    {
        Task<List<DO_PatientAppointmentDetail>> GetAppointmentDetailByDate(int businessKey, int doctorId, DateTime startDate, DateTime endDate);
        Task<List<DO_PatientAppointmentDetail>> GetAppointmentDetailByUHID(long businessKey, long uhid);
        Task<List<DO_DoctorSchedule>> GetDoctorScheduleList(int businessKey, int doctorId);
        Task<long> GetLastOPNumber(long businessKey, long uhid);
    }
}
