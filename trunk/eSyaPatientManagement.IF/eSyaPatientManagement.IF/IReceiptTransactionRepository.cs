using eSyaPatientManagement.DO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eSyaPatientManagement.IF
{
    public interface IReceiptTransactionRepository
    {
        Task<DO_ResponseParameter> InsertPatientReceipt(object context, DO_PaymentReceipt obj);
    }
}
