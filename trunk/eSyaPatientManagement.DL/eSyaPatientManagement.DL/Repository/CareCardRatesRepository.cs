using eSyaPatientManagement.DL.Entities;
using eSyaPatientManagement.DO;
using eSyaPatientManagement.IF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSyaPatientManagement.DL.Repository
{
   public class CareCardRatesRepository: ICareCardRatesRepository
    {
        private eSyaEnterpriseContext _context { get; set; }
        public CareCardRatesRepository(eSyaEnterpriseContext context)
        {
            _context = context;
        }
        #region Care Card Rates

        public async Task<List<DO_PatientCategoryTypeBusinessLink>> GetPatientTypebyBusinesskey(int businesskey)
        {
            try
            {

                var _ptypes =await _context.GtEcptcb
                    .Where(x => x.BusinessKey == businesskey && x.ActiveStatus==true)
                    .Join(_context.GtEcapcd.Where(x => x.CodeType == CodeTypeValue.PatientType),
                    s => new { s.PatientTypeId },
                    b => new { PatientTypeId=b.ApplicationCode },
                    (s, b) => new { s, b })
                    .Select(d => new DO_PatientCategoryTypeBusinessLink
                    {
                        PatientTypeId = d.s.PatientTypeId,
                        PatientTypeDesc = d.b.CodeDesc,
                    }).ToListAsync();
                if (_ptypes.Count > 0)
                {
                    var res = _ptypes.GroupBy(x => x.PatientTypeId).Select(y => y.First()).Distinct();
                    return res.ToList();
                }
                else
                {
                    return _ptypes;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DO_PatientCategoryTypeBusinessLink>> GetPatientCategoriesbyBusinesskeyAndPatientType(int businesskey, int PatientTypeId)
        {
            try
            {

                var _pcategory =await _context.GtEcptcb
                    .Where(x => x.BusinessKey == businesskey && x.PatientTypeId==PatientTypeId && x.ActiveStatus == true )
                    .Join(_context.GtEcapcd.Where(x => x.CodeType == CodeTypeValue.PatientCategory),
                    s => new { s.PatientCategoryId },
                    b => new { PatientCategoryId = b.ApplicationCode },
                    (s, b) => new { s, b })
                    .Select(d => new DO_PatientCategoryTypeBusinessLink
                    {
                        PatientCategoryId = d.s.PatientCategoryId,
                        PatientCategoryDesc = d.b.CodeDesc,
                    }).ToListAsync();
                if (_pcategory.Count > 0)
                {
                    var res = _pcategory.GroupBy(x => x.PatientCategoryId).Select(y => y.First()).Distinct();
                    return res.ToList();
                }
                else
                {
                    return _pcategory;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DO_CareCardRates>> GetCareCardRates(int businesskey, int PatientTypeId, int PatientCategoryId)
        {
            try
            {
                
                var _rates = _context.GtEcptcr
                    .Where(x => x.BusinessKey == businesskey && x.PatientTypeId == PatientTypeId && x.PatientCategoryId == PatientCategoryId)
                     .Join(_context.GtEccuco.Where(y => y.ActiveStatus == true),
                    s => new { s.CurrencyCode },
                    b => new { b.CurrencyCode },
                    (s, b) => new { s, b })
                    .Select(d => new DO_CareCardRates
                    {
                        BusinessKey = d.s.BusinessKey,
                        PatientTypeId = d.s.PatientTypeId,
                        PatientCategoryId = d.s.PatientCategoryId,
                        CurrencyCode = d.s.CurrencyCode,
                        EffectiveFrom = d.s.EffectiveFrom,
                        Rate = d.s.Rate,
                        ValidforNoOfDays = d.s.ValidforNoOfDays,
                        EffectiveTill = d.s.EffectiveTill,
                        ActiveStatus = d.s.ActiveStatus,
                        CurrencyName=d.b.CurrencyName,
                    }).ToListAsync();
                return await _rates;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DO_ResponseParameter> InsertIntoCareCardRates(DO_CareCardRates obj)
        {
            var dbContext = _context.Database.BeginTransaction();
            try
            {

                GtEcptcr rates = _context.GtEcptcr.Where(x => x.PatientTypeId == obj.PatientTypeId
                                    && x.PatientCategoryId == obj.PatientCategoryId && x.BusinessKey == obj.BusinessKey && x.CurrencyCode==
                                    obj.CurrencyCode && x.EffectiveFrom.Date==obj.EffectiveFrom.Date).FirstOrDefault();
                if (rates == null)
                {
                   
                    var _rates = new GtEcptcr
                    {
                        BusinessKey = obj.BusinessKey,
                        PatientTypeId = obj.PatientTypeId,
                        PatientCategoryId = obj.PatientCategoryId,
                        CurrencyCode = obj.CurrencyCode,
                        EffectiveFrom = obj.EffectiveFrom,
                        Rate = obj.Rate,
                        ValidforNoOfDays = obj.ValidforNoOfDays,
                        EffectiveTill = obj.EffectiveTill,
                        ActiveStatus = obj.ActiveStatus,
                        FormId = obj.FormID,
                        CreatedBy = obj.UserID,
                        CreatedOn = System.DateTime.Now,
                        CreatedTerminal = obj.TerminalID
                    };
                    _context.GtEcptcr.Add(_rates);
                    await _context.SaveChangesAsync();
                    dbContext.Commit();
                    return new DO_ResponseParameter() { Status = true, Message = "Saved Successfully." };
                }
                else
                {
                    return new DO_ResponseParameter() { Status = false, Message = "Already Exists." };
                }

               
            }

            catch (Exception ex)
            {
                dbContext.Rollback();
                throw ex;
            }

        }

        public async Task<DO_ResponseParameter> UpdateIntoCareCardRates(DO_CareCardRates obj)
        {
            var dbContext = _context.Database.BeginTransaction();
            try
            {

                GtEcptcr rates = _context.GtEcptcr.Where(x => x.PatientTypeId == obj.PatientTypeId
                                    && x.PatientCategoryId == obj.PatientCategoryId && x.BusinessKey == obj.BusinessKey && x.CurrencyCode ==
                                    obj.CurrencyCode && x.EffectiveFrom.Date == obj.EffectiveFrom.Date).FirstOrDefault();
                if (rates == null)
                {

                    return new DO_ResponseParameter() { Status = false, Message = "Not Exists." };
                }
                else
                {
                    rates.Rate = obj.Rate;
                    rates.ValidforNoOfDays = obj.ValidforNoOfDays;
                    rates.EffectiveTill = obj.EffectiveTill;
                    rates.ActiveStatus = obj.ActiveStatus;
                    rates.ModifiedBy = obj.UserID;
                    rates.ModifiedOn = System.DateTime.Now;
                    rates.ModifiedTerminal = obj.TerminalID;
                }
                await _context.SaveChangesAsync();
                dbContext.Commit();
                return new DO_ResponseParameter() { Status = true, Message = "Updated Successfully." };
            }

            catch (Exception ex)
            {
                dbContext.Rollback();
                throw ex;
            }

        }


        public async Task<DO_ResponseParameter> InsertOrUpdateCareCardRates(DO_CareCardRates obj)
        {
            var dbContext = _context.Database.BeginTransaction();
            try
            {
                GtEcptcr _cardrates;

                var mw = _context.GtEcptcr.SingleOrDefault(c => c.PatientTypeId == obj.PatientTypeId && c.PatientCategoryId == obj.PatientCategoryId
                    && c.BusinessKey == obj.BusinessKey && c.CurrencyCode == obj.CurrencyCode && c.EffectiveTill == null);

                GtEcptcr mwChk = _context.GtEcptcr.SingleOrDefault(c => c.PatientTypeId == obj.PatientTypeId && c.PatientCategoryId == obj.PatientCategoryId
                && c.BusinessKey == obj.BusinessKey && c.CurrencyCode == obj.CurrencyCode && c.EffectiveFrom.Date == obj.EffectiveFrom.Date);

                if (mwChk != null)
                {
                    mwChk.Rate = obj.Rate;
                    mwChk.ValidforNoOfDays = obj.ValidforNoOfDays;
                    mwChk.ModifiedBy = obj.UserID;
                    mwChk.ModifiedOn = DateTime.Now;
                    mwChk.ModifiedTerminal = obj.TerminalID;
                }
                else
                {
                    _cardrates = new GtEcptcr();
                    _cardrates.BusinessKey = obj.BusinessKey;
                    _cardrates.PatientTypeId = obj.PatientTypeId;
                    _cardrates.PatientCategoryId = obj.PatientCategoryId;
                    _cardrates.CurrencyCode = obj.CurrencyCode;
                    _cardrates.EffectiveFrom = obj.EffectiveFrom;
                    _cardrates.Rate = obj.Rate;
                    _cardrates.ValidforNoOfDays = obj.ValidforNoOfDays;
                    if (mw != null)
                    {
                        mw.EffectiveTill = (obj.EffectiveFrom).AddDays(-1);
                        mw.ModifiedBy = obj.UserID;
                        mw.ModifiedOn = DateTime.Now;
                        mw.ModifiedTerminal = obj.TerminalID;
                    }
                    _cardrates.FormId = obj.FormID;
                    _cardrates.ActiveStatus = obj.ActiveStatus;
                    _cardrates.CreatedBy = obj.UserID;
                    _cardrates.CreatedOn = DateTime.Now;
                    _cardrates.CreatedTerminal = obj.TerminalID;
                    _context.GtEcptcr.Add(_cardrates);
                }
                await _context.SaveChangesAsync();
                dbContext.Commit();
                return new DO_ResponseParameter() { Status = true, Message = "Saved Successfully." };
            }

            catch (Exception ex)
            {
                dbContext.Rollback();
                throw ex;
            }

        }

        public async Task<List<DO_CareCardRates>> GetPreviousCareCardRates(int businesskey, int PatientTypeId, int PatientCategoryId)
        {
            try
            {

                var _rates = _context.GtEcptcr
                    .Where(x => x.BusinessKey == businesskey && x.PatientTypeId == PatientTypeId && x.PatientCategoryId == PatientCategoryId && x.EffectiveTill!=null)
                     .Join(_context.GtEccuco.Where(y => y.ActiveStatus == true),
                    s => new { s.CurrencyCode },
                    b => new { b.CurrencyCode },
                    (s, b) => new { s, b })
                    .Select(d => new DO_CareCardRates
                    {
                        BusinessKey = d.s.BusinessKey,
                        PatientTypeId = d.s.PatientTypeId,
                        PatientCategoryId = d.s.PatientCategoryId,
                        CurrencyCode = d.s.CurrencyCode,
                        EffectiveFrom = d.s.EffectiveFrom,
                        Rate = d.s.Rate,
                        ValidforNoOfDays = d.s.ValidforNoOfDays,
                        EffectiveTill = d.s.EffectiveTill,
                        ActiveStatus = d.s.ActiveStatus,
                        CurrencyName = d.b.CurrencyName,
                    }).ToListAsync();
                return await _rates;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
