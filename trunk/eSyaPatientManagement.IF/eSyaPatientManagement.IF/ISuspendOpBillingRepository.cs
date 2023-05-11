using eSyaPatientManagement.DO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eSyaPatientManagement.IF
{
    public interface ISuspendOpBillingRepository
    {
        Task<DO_ResponseParameter> SuspendOpBill(DO_PatientBillHeader obj);

        Task<List<DO_OpBillDetail>> GetSuspendOpBillListForConfirmation(int businessKey, DateTime billFromDate, DateTime billTillDate);

        Task<DO_PatientBillHeader> GetSuspendOpBillHeader(int businessKey, long suspendBillKey);
        Task<List<DO_PatientBillDetails>> GetSuspendOpBillDetails(int businessKey, long suspendBillKey);
    }
}
