using eSyaPatientManagement.DO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eSyaPatientManagement.IF
{
   public interface ICareCardRatesRepository
    {
        #region Care Card Rates
        Task<List<DO_PatientCategoryTypeBusinessLink>> GetPatientTypebyBusinesskey(int businesskey);

        Task<List<DO_PatientCategoryTypeBusinessLink>> GetPatientCategoriesbyBusinesskeyAndPatientType(int businesskey, int PatientTypeId);

        Task<List<DO_CareCardRates>> GetCareCardRates(int businesskey, int PatientTypeId, int PatientCategoryId);

        Task<DO_ResponseParameter> InsertIntoCareCardRates(DO_CareCardRates obj);

        Task<DO_ResponseParameter> UpdateIntoCareCardRates(DO_CareCardRates obj);

        Task<DO_ResponseParameter> InsertOrUpdateCareCardRates(DO_CareCardRates obj);

        Task<List<DO_CareCardRates>> GetPreviousCareCardRates(int businesskey, int PatientTypeId, int PatientCategoryId);
        #endregion

    }
}
