using eSyaPatientManagement.DO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eSyaPatientManagement.IF
{
    public interface IServiceRatesRepository
    {
        Task<List<DO_ServiceRates>> GetOpConsultationServiceRate(
            int businessKey, string episodeId,
            int clinicId, int consultationId,
            int specialtyId, int doctorId,
            int rateType, string currencyCode, long uhid);

        Task<List<DO_ServiceRates>> GetServiceList(int businessKey);

        Task<DO_ServiceRates> GetServiceRatesForOpBilling(int businessKey, int serviceId, int rateType, string currencyCode);
    }
}
