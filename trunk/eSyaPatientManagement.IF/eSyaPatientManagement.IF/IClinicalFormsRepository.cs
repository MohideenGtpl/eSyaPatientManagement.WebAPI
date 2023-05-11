using eSyaPatientManagement.DO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eSyaPatientManagement.IF
{
    public interface IClinicalFormsRepository
    {
        Task<List<DO_ClinicalInformation>> GetClinicalInformation(int businessKey, long UHID, long vNumber, string clType);
        Task<DO_ResponseParameter> InsertIntoClinicalInformation(DO_ClinicalInformation obj);
        Task<List<DO_ClinicalInformation>> GetInformationValueView(int businessKey, long UHID, long vNumber, string clType);
        Task<DO_ResponseParameter> InsertPatientClinicalInformation(DO_ClinicalInformation obj);
        Task<DO_ResponseParameter> UpdatePatientClinicalInformation(DO_ClinicalInformation obj);
        Task<List<DO_ClinicalInformation>> GetClinicalInformationValueByTransaction(int businessKey, long UHID, long vNumber, int transactionID);
        Task<DO_ResponseParameter> DeletePatientClinicalInformation(DO_ClinicalInformation obj);
    }
}
