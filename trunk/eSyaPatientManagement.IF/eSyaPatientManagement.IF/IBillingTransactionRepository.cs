using eSyaPatientManagement.DO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eSyaPatientManagement.IF
{
    public interface IBillingTransactionRepository
    {
        Task<DO_ResponseParameter> InsertBillingTransaction(object context, DO_PatientBillHeader obj);
    }
}
