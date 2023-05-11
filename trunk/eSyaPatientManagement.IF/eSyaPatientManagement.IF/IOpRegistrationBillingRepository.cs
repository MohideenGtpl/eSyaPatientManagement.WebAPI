using eSyaPatientManagement.DO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eSyaPatientManagement.IF
{
    public interface IOpRegistrationBillingRepository
    {
        Task<DO_ResponseParameter> InsertOPRegistrationVisit(DO_OPRegistrationVisit obj);
    }
}
