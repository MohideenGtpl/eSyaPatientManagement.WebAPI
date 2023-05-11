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
   public class PatientTypesRepository: IPatientTypesRepository
    {
        private eSyaEnterpriseContext _context { get; set; }
        public PatientTypesRepository(eSyaEnterpriseContext context)
        {
            _context = context;
        }

        #region Patient Type & Category Link with Param
        public async Task<DO_PatientAttributes> GetAllPatientTypesforTreeView(int CodeType)
        {
            try
            {
                
                    DO_PatientAttributes obj = new DO_PatientAttributes();
                    obj.l_PatientType = await _context.GtEcapcd.Where(x => x.CodeType == CodeType)
                                   .Select(s => new DO_PatientTypeAttribute()
                                   {
                                       PatientTypeId = s.ApplicationCode,
                                       Description = s.ApplicationCode + " - " + s.CodeDesc,
                                       ActiveStatus = s.ActiveStatus
                                   }).ToListAsync();
                     obj.l_PatienTypeCategory = await _context.GtEcptch.Join(_context.GtEcapcd,
                         x => x.PatientCategoryId,
                         y => y.ApplicationCode,
                        (x, y) => new DO_PatientTypCategoryAttribute
                        {
                            PatientTypeId = x.PatientTypeId,
                            PatientCategoryId = x.PatientCategoryId,
                            GenerateInstantBill = x.GenerateInstantBill,
                            GenerateOpenBill = x.GenerateOpenBill,
                            Description = x.PatientCategoryId.ToString() + " - " + y.CodeDesc,
                            ActiveStatus = x.ActiveStatus
                        }).ToListAsync();
                    return obj;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DO_PatientTypCategoryAttribute> GetPatientCategoryInfo(int PatientTypeId, int PatientCategoryId)
        {
            try
            {
                
                    var pa_categories = _context.GtEcptch
                        .Where(x => x.PatientTypeId == PatientTypeId && x.PatientCategoryId == PatientCategoryId)
                        .Select(pc => new DO_PatientTypCategoryAttribute
                        {
                            PatientTypeId = pc.PatientTypeId,
                            PatientCategoryId = pc.PatientCategoryId,
                            GenerateInstantBill = pc.GenerateInstantBill,
                            GenerateOpenBill = pc.GenerateOpenBill,
                            ActiveStatus = pc.ActiveStatus,
                            l_ptypeparams = _context.GtEcpapc.Where(h=>h.PatientTypeId==PatientTypeId
                            && h.PatientCategoryId==PatientCategoryId).Select(p => new DO_eSyaParameter
                            {
                                ParameterID = p.ParameterId,
                                ParmAction = p.ParmAction,
                            }).ToList()
                        }).FirstOrDefaultAsync();
                    return await pa_categories;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DO_ResponseParameter> InsertPatientCategory(DO_PatientTypCategoryAttribute obj)
        {
            var dbContext = _context.Database.BeginTransaction();
            try
            {

                GtEcptch _pcat = _context.GtEcptch.Where(x => x.PatientTypeId == obj.PatientTypeId && x.PatientCategoryId == obj.PatientCategoryId).FirstOrDefault();
                if (_pcat == null)
                {
                    _pcat = new GtEcptch
                    {
                        PatientTypeId = obj.PatientTypeId,
                        PatientCategoryId = obj.PatientCategoryId,
                        GenerateInstantBill = obj.GenerateInstantBill,
                        GenerateOpenBill = obj.GenerateOpenBill,
                        ActiveStatus = obj.ActiveStatus,
                        FormId = obj.FormID,
                        CreatedBy = obj.UserID,
                        CreatedOn = System.DateTime.Now,
                        CreatedTerminal = obj.TerminalID
                    };
                    _context.GtEcptch.Add(_pcat);

                    foreach (DO_eSyaParameter ip in obj.l_ptypeparams)
                    {
                        var _parm = new GtEcpapc
                        {
                            PatientTypeId = obj.PatientTypeId,
                            PatientCategoryId = obj.PatientCategoryId,
                            ParameterId = ip.ParameterID,
                            ParmPerc = 0,
                            ParmDesc = null,
                            ParmValue = 0,
                            ParmAction = ip.ParmAction,
                            ActiveStatus = ip.ActiveStatus,
                            FormId = obj.FormID,
                            CreatedBy = obj.UserID,
                            CreatedOn = System.DateTime.Now,
                            CreatedTerminal = obj.TerminalID,
                        };
                        _context.GtEcpapc.Add(_parm);
                    }

                    await _context.SaveChangesAsync();
                    dbContext.Commit();
                    return new DO_ResponseParameter() { Status = true, Message = "Patient Category Linked Successfully." };
                }
                else
                {
                    return new DO_ResponseParameter() { Status = false, Message = "Patient Category already Exists." };
                }
            }

            catch (Exception ex)
            {
                dbContext.Rollback();
                throw ex;

            }
                 
        }

        public async Task<DO_ResponseParameter> UpdatePatientCategory(DO_PatientTypCategoryAttribute obj)
        {

            var dbContext = _context.Database.BeginTransaction();
            try
                    {
                        GtEcptch pat_cat = _context.GtEcptch.Where(x => x.PatientTypeId == obj.PatientTypeId && x.PatientCategoryId == obj.PatientCategoryId).FirstOrDefault();
                        pat_cat.GenerateInstantBill = obj.GenerateInstantBill;
                        pat_cat.GenerateOpenBill = obj.GenerateOpenBill;
                        pat_cat.ActiveStatus = obj.ActiveStatus;
                        pat_cat.ModifiedBy = obj.UserID;
                        pat_cat.ModifiedOn = System.DateTime.Now;
                        pat_cat.ModifiedTerminal = obj.TerminalID;

                        foreach (DO_eSyaParameter ip in obj.l_ptypeparams)
                        {
                            GtEcpapc sPar = _context.GtEcpapc.Where(x => x.PatientTypeId == obj.PatientTypeId && x.PatientCategoryId==obj.PatientCategoryId && x.ParameterId == ip.ParameterID).FirstOrDefault();
                            if (sPar != null)
                            {
                                sPar.ParmPerc = 0;
                                sPar.ParmDesc = null;
                                sPar.ParmValue = 0;
                                sPar.ParmAction = ip.ParmAction;
                                sPar.ActiveStatus = obj.ActiveStatus;
                                sPar.ModifiedBy = obj.UserID;
                                sPar.ModifiedOn = System.DateTime.Now;
                                sPar.ModifiedTerminal = obj.TerminalID;
                            }
                            else
                            {
                                var parms = new GtEcpapc
                                {
                                    PatientTypeId = obj.PatientTypeId,
                                    PatientCategoryId=obj.PatientCategoryId,
                                    ParameterId = ip.ParameterID,
                                    //ParamAction = ip.ParmAction,
                                    ParmPerc = 0,
                                    ParmDesc = null,
                                    ParmValue = 0,
                                    ParmAction = ip.ParmAction,
                                    ActiveStatus = ip.ActiveStatus,
                                    FormId = obj.FormID,
                                    CreatedBy = obj.UserID,
                                    CreatedOn = System.DateTime.Now,
                                    CreatedTerminal = obj.TerminalID,
                                };
                              _context.GtEcpapc.Add(parms);
                            }
                        }

                        await _context.SaveChangesAsync();
                        dbContext.Commit();

                        return new DO_ResponseParameter() { Status = true, Message = "Patient Category Linked Updated Successfully." };
                    }
                    catch (Exception ex)
                    {
                        dbContext.Rollback();
                        throw ex;
                    }
        }
        #endregion

        #region Patient Type & Category Business Link

        public async Task<List<DO_PatientCategoryTypeBusinessLink>> GetAllPatientCategoryBusinessLink(int businesskey)
        {
            try
            {
                var pa_link = _context.GtEcptch
                         .Join(_context.GtEcapcd.Where(w => w.CodeType == CodeTypeValue.PatientType),
                          pl => new { pl.PatientTypeId },
                          pt => new { PatientTypeId = pt.ApplicationCode },
                         (pl, pt) => new { pl, pt })
                         .Join(_context.GtEcapcd.Where(w => w.CodeType == CodeTypeValue.PatientCategory),
                         pll =>new { pll.pl.PatientCategoryId },
                         pc => new { PatientCategoryId = pc.ApplicationCode },
                         (pll, pc) => new { pll, pc })
                         .GroupJoin(_context.GtEcptcb.Where(m=>m.BusinessKey==businesskey),
                          plink =>new { plink.pll.pl.PatientTypeId, plink.pll.pl.PatientCategoryId },
                          r => new { r.PatientTypeId, r.PatientCategoryId },
                         (plink, r) => new { plink, r=r.FirstOrDefault() })
                         .Where(w => w.plink.pll.pl.ActiveStatus)
                          .Select(l => new DO_PatientCategoryTypeBusinessLink
                          {
                              PatientTypeId =l.plink.pll.pl.PatientTypeId,
                              PatientTypeDesc=l.plink.pll.pt.CodeDesc,
                              PatientCategoryId=l.plink.pll.pl.PatientCategoryId,
                              PatientCategoryDesc =l.plink.pc.CodeDesc,
                              ActiveStatus=l.r!=null?l.r.ActiveStatus:false,
                              BusinessKey= l.r != null ? l.r.BusinessKey : businesskey,
                              RateType = l.r != null ? l.r.RateType : 0,

                          }).ToListAsync();
                return await pa_link;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DO_ResponseParameter> InsertOrUpdatePatientCategoryBusinessLink(List<DO_PatientCategoryTypeBusinessLink> obj)
        {
            var dbContext = _context.Database.BeginTransaction();
            try
            {
                foreach (var pt in obj)
                {
                    GtEcptcb pt_link = _context.GtEcptcb.Where(x => x.PatientTypeId == pt.PatientTypeId
                                    && x.PatientCategoryId == pt.PatientCategoryId && x.BusinessKey == pt.BusinessKey).FirstOrDefault();
                    if (pt_link == null)
                    {
                        var plink = new GtEcptcb
                        {
                            BusinessKey = pt.BusinessKey,
                            PatientTypeId = pt.PatientTypeId,
                            PatientCategoryId = pt.PatientCategoryId,
                            RateType=pt.RateType,
                            ActiveStatus = pt.ActiveStatus,
                            FormId = pt.FormID,
                            CreatedBy = pt.UserID,
                            CreatedOn = System.DateTime.Now,
                            CreatedTerminal = pt.TerminalID
                        };
                        _context.GtEcptcb.Add(plink);
                    }
                    else
                    {
                        pt_link.RateType = pt.RateType;
                        pt_link.ActiveStatus = pt.ActiveStatus;
                        pt_link.ModifiedBy = pt.UserID;
                        pt_link.ModifiedOn = System.DateTime.Now;
                        pt_link.ModifiedTerminal = pt.TerminalID;
                    }
                    await _context.SaveChangesAsync();
                }
                dbContext.Commit();
                return new DO_ResponseParameter() { Status = true, Message = "Saved Successfully." };
            }

            catch (Exception ex)
            {
                dbContext.Rollback();
                throw ex;
            }
              
        }
        #endregion

        #region Care Card Details

        #region Patient Type + Category – Care Card
        public async Task<DO_CareCardPattern> GetCareCardPattern(int businesskey,int PatientTypeId, int PatientCategoryId)
        {
            try
            {

                var card_pattern = _context.GtEcptcc
                    .Where(x =>x.BusinessKey==businesskey && x.PatientTypeId == PatientTypeId && x.PatientCategoryId == PatientCategoryId)
                    .Select(c => new DO_CareCardPattern
                    {
                        BusinessKey=c.BusinessKey,
                        PatientTypeId = c.PatientTypeId,
                        PatientCategoryId = c.PatientCategoryId,
                        CareCardNumber = c.CareCardNumber,
                        CareCardIdpattern = c.CareCardIdpattern,
                        ActiveStatus = c.ActiveStatus,
                    }).FirstOrDefaultAsync();
                return await card_pattern;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DO_ResponseParameter> InsertOrUpdateCareCardPattern(DO_CareCardPattern obj)
        {
            var dbContext = _context.Database.BeginTransaction();
            try
            {

                GtEcptcc card = _context.GtEcptcc.Where(x => x.PatientTypeId == obj.PatientTypeId
                                    && x.PatientCategoryId == obj.PatientCategoryId && x.BusinessKey == obj.BusinessKey
                                    && x.CareCardNumber == obj.CareCardNumber).FirstOrDefault();
                if (card == null)
                {
                    int maxcardno = _context.GtEcptcc.Select(d => d.CareCardNumber).DefaultIfEmpty().Max();
                    int cardnumber = maxcardno + 1;

                    var _card = new GtEcptcc
                    {
                        BusinessKey = obj.BusinessKey,
                        PatientTypeId = obj.PatientTypeId,
                        PatientCategoryId = obj.PatientCategoryId,
                        CareCardNumber = cardnumber,
                        CareCardIdpattern=obj.CareCardIdpattern,
                        ActiveStatus = obj.ActiveStatus,
                        FormId = obj.FormID,
                        CreatedBy = obj.UserID,
                        CreatedOn = System.DateTime.Now,
                        CreatedTerminal = obj.TerminalID
                    };
                    _context.GtEcptcc.Add(_card);
                }
                else
                {
                    card.CareCardIdpattern = obj.CareCardIdpattern;
                    card.ActiveStatus = obj.ActiveStatus;
                    card.ModifiedBy = obj.UserID;
                    card.ModifiedOn = System.DateTime.Now;
                    card.ModifiedTerminal = obj.TerminalID;
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

        public DO_CareCardPattern GetCardNumber()
        {
            try
            {
                int maxcardno = _context.GtEcptcc.Select(d => d.CareCardNumber).DefaultIfEmpty().Max();
                int cardnumber = maxcardno + 1;
                DO_CareCardPattern obj = new DO_CareCardPattern();
                obj.CareCardNumber = cardnumber;
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Patient Type + Category – Document Details
        public async Task<List<DO_DocumentDetails>> GetDocumentDetails(int businesskey, int PatientTypeId,int PatientCategoryId)
        {
            try
            {

                var d_details = _context.GtEcptcd
                    .Where(x => x.BusinessKey == businesskey && x.PatientTypeId == PatientTypeId && x.PatientCategoryId == PatientCategoryId)
                    .Select(d => new DO_DocumentDetails
                    {
                        BusinessKey = d.BusinessKey,
                        PatientTypeId = d.PatientTypeId,
                        PatientCategoryId = d.PatientCategoryId,
                        DocumentId = d.DocumentId,
                        DocumentDesc = d.DocumentDesc,
                        ActiveStatus = d.ActiveStatus
                    }).ToListAsync();
                return await d_details;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DO_ResponseParameter> InsertOrUpdateDocumentDetails(DO_DocumentDetails obj)
        {
            var dbContext = _context.Database.BeginTransaction();
            try
            {

                GtEcptcd doc = _context.GtEcptcd.Where(x => x.PatientTypeId == obj.PatientTypeId
                                    && x.PatientCategoryId == obj.PatientCategoryId && x.BusinessKey == obj.BusinessKey).FirstOrDefault();
                if (doc == null)
                {
                    int maxdocId = _context.GtEcptcd.Select(d => d.DocumentId).DefaultIfEmpty().Max();
                    int documentId = maxdocId + 1;

                    var _doc = new GtEcptcd
                    {
                        BusinessKey = obj.BusinessKey,
                        PatientTypeId = obj.PatientTypeId,
                        PatientCategoryId = obj.PatientCategoryId,
                        DocumentId = documentId,
                        DocumentDesc = obj.DocumentDesc,
                        ActiveStatus = obj.ActiveStatus,
                        FormId = obj.FormID,
                        CreatedBy = obj.UserID,
                        CreatedOn = System.DateTime.Now,
                        CreatedTerminal = obj.TerminalID
                    };
                    _context.GtEcptcd.Add(_doc);
                }
                else
                {
                    doc.DocumentDesc = obj.DocumentDesc;
                    doc.ActiveStatus = obj.ActiveStatus;
                    doc.ModifiedBy = obj.UserID;
                    doc.ModifiedOn = System.DateTime.Now;
                    doc.ModifiedTerminal = obj.TerminalID;
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
        #endregion

        #region Patient Type + – Service Type Wise – Rate Type
        public async Task<List<DO_PatientTypeCategoryServiceTypeLink>> GetPatientTypeCategoryServiceTypeLink(int businesskey, int PatientTypeId, int PatientCategoryId)
        {
            try
            {
                var service_type = _context.GtEssrty
                         .GroupJoin(_context.GtEcptsr,
                          s => new { s.ServiceTypeId },
                          r => new { ServiceTypeId = r.ServiceType },
                         (s, r) => new
                         {s,r = r.Where(k => k.BusinessKey == businesskey && k.PatientTypeId == PatientTypeId && k.PatientCategoryId == PatientCategoryId).FirstOrDefault()})
                         .Where(w => w.s.ActiveStatus)
                          .Select(st => new DO_PatientTypeCategoryServiceTypeLink
                          {
                              ServiceType = st.s.ServiceTypeId,
                              ServiceTypeDesc = st.s.ServiceTypeDesc,
                              ActiveStatus = st.r != null ? st.r.ActiveStatus : false,
                              BusinessKey = st.r != null ? st.r.BusinessKey : businesskey,
                              PatientTypeId = st.r != null ? st.r.PatientTypeId : PatientTypeId,
                              PatientCategoryId = st.r != null ? st.r.PatientCategoryId : PatientCategoryId,
                              RateType = st.r != null ? st.r.RateType : 0,

                          }).ToListAsync();

                return await service_type;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DO_ResponseParameter> InsertOrUpdatePatientTypeCategoryServiceType(List<DO_PatientTypeCategoryServiceTypeLink> obj)
        {
            var dbContext = _context.Database.BeginTransaction();
            try
            {
                foreach (var s in obj)
                {
                    GtEcptsr st_link = _context.GtEcptsr.Where(x => x.PatientTypeId == s.PatientTypeId
                                    && x.PatientCategoryId == s.PatientCategoryId && x.BusinessKey == s.BusinessKey && x.ServiceType==s.ServiceType).FirstOrDefault();
                    if (st_link == null)
                    {
                        var slink = new GtEcptsr
                        {
                            BusinessKey = s.BusinessKey,
                            PatientTypeId = s.PatientTypeId,
                            PatientCategoryId = s.PatientCategoryId,
                            ServiceType=s.ServiceType,
                            RateType = s.RateType,
                            ActiveStatus = s.ActiveStatus,
                            FormId = s.FormID,
                            CreatedBy = s.UserID,
                            CreatedOn = System.DateTime.Now,
                            CreatedTerminal = s.TerminalID
                        };
                        _context.GtEcptsr.Add(slink);
                    }
                    else
                    {
                        st_link.RateType = s.RateType;
                        st_link.ActiveStatus = s.ActiveStatus;
                        st_link.ModifiedBy = s.UserID;
                        st_link.ModifiedOn = System.DateTime.Now;
                        st_link.ModifiedTerminal = s.TerminalID;
                    }
                    await _context.SaveChangesAsync();
                }
                dbContext.Commit();
                return new DO_ResponseParameter() { Status = true, Message = "Saved Successfully." };
            }

            catch (Exception ex)
            {
                dbContext.Rollback();
                throw ex;
            }

        }
        #endregion

        #region Patient Type + Category – Restricted Specialty
        public async Task<List<DO_PatientTypeCategorySpecialtyLink>> GetPatientTypeCategorySpecialtyLink(int businesskey, int PatientTypeId, int PatientCategoryId)
        {
            try
            {
                var specialty = _context.GtEsspcd
                         .GroupJoin(_context.GtEcptsp,
                          s => new { s.SpecialtyId },
                          r => new { r.SpecialtyId },
                         (s, r) => new
                         { s, r = r.Where(k =>k.PatientTypeId == PatientTypeId && k.PatientCategoryId == PatientCategoryId).FirstOrDefault() })
                         .Where(w => w.s.ActiveStatus)
                          .Select(st => new DO_PatientTypeCategorySpecialtyLink
                          {
                              SpecialtyId = st.s.SpecialtyId,
                              ServiceTypeDesc = st.s.SpecialtyDesc,
                              ActiveStatus = st.r != null ? st.r.ActiveStatus : false,
                              //BusinessKey = st.r != null ? st.r.BusinessKey : businesskey,
                              PatientTypeId = st.r != null ? st.r.PatientTypeId : PatientTypeId,
                              PatientCategoryId = st.r != null ? st.r.PatientCategoryId : PatientCategoryId,
                          }).ToListAsync();

                return await specialty;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DO_ResponseParameter> InsertOrUpdatePatientTypeCategorySpecialtyLink(List<DO_PatientTypeCategorySpecialtyLink> obj)
        {
            var dbContext = _context.Database.BeginTransaction();
            try
            {
                foreach (var s in obj)
                {
                    GtEcptsp sp_link = _context.GtEcptsp.Where(x => x.PatientTypeId == s.PatientTypeId
                                    && x.PatientCategoryId == s.PatientCategoryId && x.SpecialtyId == s.SpecialtyId).FirstOrDefault();
                    if (sp_link == null)
                    {
                        var slink = new GtEcptsp
                        {
                            PatientTypeId = s.PatientTypeId,
                            PatientCategoryId = s.PatientCategoryId,
                            SpecialtyId = s.SpecialtyId,
                            ActiveStatus = s.ActiveStatus,
                            FormId = s.FormID,
                            CreatedBy = s.UserID,
                            CreatedOn = System.DateTime.Now,
                            CreatedTerminal = s.TerminalID
                        };
                        _context.GtEcptsp.Add(slink);
                    }
                    else
                    {
                        sp_link.ActiveStatus = s.ActiveStatus;
                        sp_link.ModifiedBy = s.UserID;
                        sp_link.ModifiedOn = System.DateTime.Now;
                        sp_link.ModifiedTerminal = s.TerminalID;
                    }
                    await _context.SaveChangesAsync();
                }
                dbContext.Commit();
                return new DO_ResponseParameter() { Status = true, Message = "Saved Successfully." };
            }

            catch (Exception ex)
            {
                dbContext.Rollback();
                throw ex;
            }

        }
        #endregion

        #region Patient Type + Category – Dependent
        public async Task<List<DO_PatientTypeCategoryDependent>> GetPatientTypeCategoryDependent(int businesskey, int PatientTypeId, int PatientCategoryId)
        {
            try
            {
                var specialty = _context.GtEcapcd.Where(w => w.CodeType == CodeTypeValue.Relationship)
                         .GroupJoin(_context.GtEcptdp,
                          s => new { s.ApplicationCode },
                          r => new { ApplicationCode=r.Relationship },
                         (s, r) => new
                         { s, r = r.Where(k =>k.BusinessKey==businesskey && k.PatientTypeId == PatientTypeId && k.PatientCategoryId == PatientCategoryId).FirstOrDefault() })
                         .Where(w => w.s.ActiveStatus)
                          .Select(st => new DO_PatientTypeCategoryDependent
                          {
                              Relationship = st.s.ApplicationCode,
                              RelationShipDesc = st.s.CodeDesc,
                              ActiveStatus = st.r != null ? st.r.ActiveStatus : false,
                              BusinessKey = st.r != null ? st.r.BusinessKey : businesskey,
                              PatientTypeId = st.r != null ? st.r.PatientTypeId : PatientTypeId,
                              PatientCategoryId = st.r != null ? st.r.PatientCategoryId : PatientCategoryId,

                          }).ToListAsync();

                return await specialty;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DO_ResponseParameter> InsertOrUpdatePatientTypeCategoryDependent(List<DO_PatientTypeCategoryDependent> obj)
        {
            var dbContext = _context.Database.BeginTransaction();
            try
            {
                foreach (var s in obj)
                {
                    GtEcptdp r_link = _context.GtEcptdp.Where(x =>x.BusinessKey==s.BusinessKey && x.PatientTypeId == s.PatientTypeId
                                    && x.PatientCategoryId == s.PatientCategoryId && x.Relationship == s.Relationship).FirstOrDefault();
                    if (r_link == null)
                    {
                        var rlink = new GtEcptdp
                        {
                            BusinessKey=s.BusinessKey,
                            PatientTypeId = s.PatientTypeId,
                            PatientCategoryId = s.PatientCategoryId,
                            Relationship = s.Relationship,
                            ActiveStatus = s.ActiveStatus,
                            FormId = s.FormID,
                            CreatedBy = s.UserID,
                            CreatedOn = System.DateTime.Now,
                            CreatedTerminal = s.TerminalID
                        };
                        _context.GtEcptdp.Add(rlink);
                    }
                    else
                    {
                        r_link.ActiveStatus = s.ActiveStatus;
                        r_link.ModifiedBy = s.UserID;
                        r_link.ModifiedOn = System.DateTime.Now;
                        r_link.ModifiedTerminal = s.TerminalID;
                    }
                    await _context.SaveChangesAsync();
                }
                dbContext.Commit();
                return new DO_ResponseParameter() { Status = true, Message = "Saved Successfully." };
            }

            catch (Exception ex)
            {
                dbContext.Rollback();
                throw ex;
            }

        }
        #endregion

        

        #endregion

        //#region  Care Card Rates
        //public async Task<List<DO_PatientTypeCategoryCareCardRates>> GetPatientTypeCategoryCareCardRates(int businesskey)
        //{
        //    try
        //    {
        //        var pa_link = _context.GtEcptcb
        //                 .Join(_context.GtEcapcd.Where(w => w.CodeType == CodeTypeValue.PatientType),
        //                  pl => new { pl.PatientTypeId },
        //                  pt => new { PatientTypeId = pt.ApplicationCode },
        //                 (pl, pt) => new { pl, pt })
        //                 .Join(_context.GtEcapcd.Where(w => w.CodeType == CodeTypeValue.PatientCategory),
        //                 pll => new { pll.pl.PatientCategoryId },
        //                 pc => new { PatientCategoryId = pc.ApplicationCode },
        //                 (pll, pc) => new { pll, pc })
        //                 .GroupJoin(_context.GtEcptcr.Where(m => m.BusinessKey == businesskey),
        //                  plink => new { plink.pll.pl.PatientTypeId, plink.pll.pl.PatientCategoryId },
        //                  r => new { r.PatientTypeId, r.PatientCategoryId },
        //                 (plink, r) => new { plink, r = r.FirstOrDefault() })
        //                    .Where(w => w.plink.pll.pl.ActiveStatus)
        //                  .Select(l => new DO_PatientTypeCategoryCareCardRates
        //                  {
        //                      PatientTypeId = l.plink.pll.pl.PatientTypeId,
        //                      PatientTypeDesc = l.plink.pll.pt.CodeDesc,
        //                      PatientCategoryId = l.plink.pll.pl.PatientCategoryId,
        //                      PatientCategoryDesc = l.plink.pc.CodeDesc,
        //                      ActiveStatus = l.r != null ? l.r.ActiveStatus : false,
        //                      BusinessKey = l.r != null ? l.r.BusinessKey : businesskey,
        //                      Rate = l.r != null ? l.r.Rate : 0,
        //                      CurrencyCode = l.r != null ? l.r.CurrencyCode : string.Empty,
        //                      ValidforNoOfDays = l.r != null ? l.r.ValidforNoOfDays : 0,
        //                      EffectiveFrom = l.r != null ? l.r.EffectiveFrom : DateTime.Now,
        //                      EffectiveTill = l.r != null ? l.r.EffectiveTill : null,

        //                  }).ToListAsync();

        //        return await pa_link;


        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public async Task<DO_ResponseParameter> InsertOrUpdatePatientTypeCategoryCareCardRates(List<DO_PatientTypeCategoryCareCardRates> obj)
        //{
        //    var dbContext = _context.Database.BeginTransaction();
        //    try
        //    {
        //        foreach (var pt in obj)
        //        {
        //            GtEcptcr rate = _context.GtEcptcr.Where(x => x.PatientTypeId == pt.PatientTypeId
        //                            && x.PatientCategoryId == pt.PatientCategoryId && x.BusinessKey == pt.BusinessKey).FirstOrDefault();
        //            if (rate == null)
        //            {
        //                var r = new GtEcptcr
        //                {
        //                    BusinessKey = pt.BusinessKey,
        //                    PatientTypeId = pt.PatientTypeId,
        //                    PatientCategoryId = pt.PatientCategoryId,
        //                    Rate = pt.Rate,
        //                    CurrencyCode=pt.CurrencyCode,
        //                    EffectiveFrom=pt.EffectiveFrom,
        //                    EffectiveTill=pt.EffectiveTill,
        //                    ValidforNoOfDays=pt.ValidforNoOfDays,
        //                    ActiveStatus = pt.ActiveStatus,
        //                    FormId = pt.FormID,
        //                    CreatedBy = pt.UserID,
        //                    CreatedOn = System.DateTime.Now,
        //                    CreatedTerminal = pt.TerminalID
        //                };
        //                _context.GtEcptcr.Add(r);
        //            }
        //            else
        //            {
        //                rate.Rate = pt.Rate;
        //                rate.CurrencyCode = pt.CurrencyCode;
        //                rate.EffectiveFrom = pt.EffectiveFrom;
        //                rate.EffectiveTill = pt.EffectiveTill;
        //                rate.ValidforNoOfDays = pt.ValidforNoOfDays;
        //                rate.ActiveStatus = pt.ActiveStatus;
        //                rate.ModifiedBy = pt.UserID;
        //                rate.ModifiedOn = System.DateTime.Now;
        //                rate.ModifiedTerminal = pt.TerminalID;
        //            }
        //            await _context.SaveChangesAsync();
        //        }
        //        dbContext.Commit();
        //        return new DO_ResponseParameter() { Status = true, Message = "Saved Successfully." };
        //    }

        //    catch (Exception ex)
        //    {
        //        dbContext.Rollback();
        //        throw ex;
        //    }

        //}
        
        //#endregion
    }
}
