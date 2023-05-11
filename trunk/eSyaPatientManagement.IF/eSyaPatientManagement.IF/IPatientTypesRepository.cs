using eSyaPatientManagement.DO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eSyaPatientManagement.IF
{
   public interface IPatientTypesRepository
    {
        #region Patient Type & Category Link with Param
        Task<DO_PatientAttributes> GetAllPatientTypesforTreeView(int CodeType);

        Task<DO_PatientTypCategoryAttribute> GetPatientCategoryInfo(int PatientTypeId, int PatientCategoryId);

        Task<DO_ResponseParameter> InsertPatientCategory(DO_PatientTypCategoryAttribute obj);

        Task<DO_ResponseParameter> UpdatePatientCategory(DO_PatientTypCategoryAttribute obj);
        #endregion

        #region Patient Type & Category Business Link
        Task<List<DO_PatientCategoryTypeBusinessLink>> GetAllPatientCategoryBusinessLink(int businesskey);

        Task<DO_ResponseParameter> InsertOrUpdatePatientCategoryBusinessLink(List<DO_PatientCategoryTypeBusinessLink> obj);
        #endregion

        #region Care Card Details

        #region Patient Type + Category – Care Card
        Task<DO_CareCardPattern> GetCareCardPattern(int businesskey, int PatientTypeId, int PatientCategoryId);

        Task<DO_ResponseParameter> InsertOrUpdateCareCardPattern(DO_CareCardPattern obj);

        DO_CareCardPattern GetCardNumber();

        #endregion

        #region Patient Type + Category – Document Details

        Task<List<DO_DocumentDetails>> GetDocumentDetails(int businesskey, int PatientTypeId, int PatientCategoryId);

        Task<DO_ResponseParameter> InsertOrUpdateDocumentDetails(DO_DocumentDetails obj);

        #endregion

        #region Patient Type + – Service Type Wise – Rate Type

        Task<List<DO_PatientTypeCategoryServiceTypeLink>> GetPatientTypeCategoryServiceTypeLink(int businesskey, int PatientTypeId, int PatientCategoryId);

        Task<DO_ResponseParameter> InsertOrUpdatePatientTypeCategoryServiceType(List<DO_PatientTypeCategoryServiceTypeLink> obj);
        #endregion

        #region Patient Type + Category – Restricted Specialty

        Task<List<DO_PatientTypeCategorySpecialtyLink>> GetPatientTypeCategorySpecialtyLink(int businesskey, int PatientTypeId, int PatientCategoryId);

        Task<DO_ResponseParameter> InsertOrUpdatePatientTypeCategorySpecialtyLink(List<DO_PatientTypeCategorySpecialtyLink> obj);
        #endregion

        #region Patient Type + Category – Dependent

        Task<List<DO_PatientTypeCategoryDependent>> GetPatientTypeCategoryDependent(int businesskey, int PatientTypeId, int PatientCategoryId);

        Task<DO_ResponseParameter> InsertOrUpdatePatientTypeCategoryDependent(List<DO_PatientTypeCategoryDependent> obj);
        #endregion

        #endregion

        //#region  Care Card Rates

        //Task<List<DO_PatientTypeCategoryCareCardRates>> GetPatientTypeCategoryCareCardRates(int businesskey);

        //Task<DO_ResponseParameter> InsertOrUpdatePatientTypeCategoryCareCardRates(List<DO_PatientTypeCategoryCareCardRates> obj);
        //#endregion
    }
}
