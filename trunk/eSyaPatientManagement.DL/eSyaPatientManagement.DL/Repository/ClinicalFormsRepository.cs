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
    public class ClinicalFormsRepository : IClinicalFormsRepository
    {
        private eSyaEnterpriseContext _context { get; set; }
        public ClinicalFormsRepository(eSyaEnterpriseContext context)
        {
            _context = context;
        }

        public async Task<List<DO_ClinicalInformation>> GetClinicalInformation(int businessKey, long UHID, long vNumber, string clType)
        {

            var cl = await _context.GtOpclin
                    .Where(w => w.BusinessKey == businessKey && w.Uhid == UHID && (w.Vnumber == vNumber || w.Vnumber == 0)
                       && clType.Contains(w.Cltype) && w.ActiveStatus)
                    .Select(x => new DO_ClinicalInformation
                    {
                        CLControlID = x.ClcontrolId,
                        CLType = x.Cltype,
                        Value = x.Value,
                        ValueType = x.ValueType
                    }).ToListAsync();


            return cl;

        }

        public async Task<DO_ResponseParameter> InsertIntoClinicalInformation(DO_ClinicalInformation obj)
        {
            var dbContext = _context.Database.BeginTransaction();
            try
            {
                foreach (var c in obj.l_ControlValue)
                {
                    var exist = _context.GtOpclin.Where(w => w.BusinessKey == obj.BusinessKey && w.Uhid == obj.UHID && w.Vnumber == obj.VisitNumber && w.ClcontrolId == c.CLControlID).FirstOrDefault();
                    if (exist != null)
                    {
                        if (c.Value == null)
                        {
                            exist.ActiveStatus = false;
                        }
                        else
                        {
                            exist.ValueType = c.ValueType;
                            exist.Value = c.Value;
                            exist.ActiveStatus = true;
                            exist.ModifiedBy = obj.UserID;
                            exist.ModifiedOn = System.DateTime.Now;
                            exist.ModifiedTerminal = obj.TerminalID;
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(c.Value))
                        {
                            GtOpclin cl = new GtOpclin
                            {
                                BusinessKey = obj.BusinessKey,
                                Uhid = obj.UHID,
                                Vnumber = obj.VisitNumber,
                                TransactionId = 0,
                                TransactionDate = obj.TransactionDate,
                                Cltype = c.CLType,
                                ClcontrolId = c.CLControlID,
                                ValueType = c.ValueType,
                                Value = c.Value,
                                ActiveStatus = true,
                                FormId = obj.FormID,
                                CreatedBy = obj.UserID,
                                CreatedOn = System.DateTime.Now,
                                CreatedTerminal = obj.TerminalID
                            };
                            _context.GtOpclin.Add(cl);

                        }
                    }
                }
                await _context.SaveChangesAsync();
                dbContext.Commit();

                return new DO_ResponseParameter() { Status = true, Message = "Saved Successfully." };
            }
            catch (DbUpdateException ex)
            {
                dbContext.Rollback();
                throw ex;
            }
            catch (Exception ex)
            {
                dbContext.Rollback();
                throw ex;
            }
        }

        public async Task<List<DO_ClinicalInformation>> GetInformationValueView(int businessKey, long UHID, long vNumber, string clType)
        {

            var ip = await _context.GtOpclin
                    .Where(w => w.BusinessKey == businessKey && w.Uhid == UHID && (w.Vnumber == vNumber || vNumber == -1)
                       && w.Cltype == clType && w.ActiveStatus)
                    .Select(x => new DO_ClinicalInformation
                    {
                        TransactionID = x.TransactionId,
                        TransactionDate = x.TransactionDate,
                        VisitNumber = Convert.ToInt32(x.Vnumber)

                    }).Distinct().ToListAsync();

            var cl = ip.Select(x => new DO_ClinicalInformation
            {
                TransactionID = x.TransactionID,
                TransactionDate = x.TransactionDate,
                VisitNumber = x.VisitNumber,
                ChartNumber = x.ChartNumber,
                l_ControlValue = _context.GtOpclin.Where(w => w.BusinessKey == businessKey && w.Uhid == UHID && w.Vnumber == x.VisitNumber
                                                        && w.Cltype == clType && w.TransactionId == x.TransactionID && w.ActiveStatus)
                                    .Select(c => new DO_ControlValue
                                    {
                                        CLControlID = c.ClcontrolId,
                                        CLType = c.Cltype,
                                        Value = c.Value,

                                    }).ToList()
            }).OrderBy(o => o.TransactionDate).ToList();


            return cl;

        }

        public async Task<DO_ResponseParameter> InsertPatientClinicalInformation(DO_ClinicalInformation obj)
        {
            var dbContext = _context.Database.BeginTransaction();
            try
            {
                var sl = _context.GtOpclin.Where(w => w.BusinessKey == obj.BusinessKey && w.Uhid == obj.UHID && w.Vnumber == obj.VisitNumber).Select(s => s.TransactionId).DefaultIfEmpty(0).Max() + 1;

                int chartNumber = 0;


                foreach (var c in obj.l_ControlValue)
                {
                    if (!string.IsNullOrEmpty(c.Value))
                    {
                        GtOpclin cl = new GtOpclin
                        {
                            BusinessKey = obj.BusinessKey,
                            Uhid = obj.UHID,
                            Vnumber = obj.VisitNumber,
                            TransactionId = sl,
                            TransactionDate = obj.TransactionDate,
                            Cltype = c.CLType,
                            ClcontrolId = c.CLControlID,
                            ValueType = c.ValueType,
                            Value = c.Value,
                            ChartNumber = chartNumber,
                            ActiveStatus = true,
                            FormId = obj.FormID,
                            CreatedBy = obj.UserID,
                            CreatedOn = System.DateTime.Now,
                            CreatedTerminal = obj.TerminalID
                        };
                        _context.GtOpclin.Add(cl);
                    }
                }

                await _context.SaveChangesAsync();
                dbContext.Commit();
                return new DO_ResponseParameter() { Status = true, Message = "Saved Successfully." };
            }
            catch (DbUpdateException ex)
            {
                dbContext.Rollback();
                throw ex;
            }
            catch (Exception ex)
            {
                dbContext.Rollback();
                throw ex;
            }



        }

        public async Task<DO_ResponseParameter> UpdatePatientClinicalInformation(DO_ClinicalInformation obj)
        {
            var dbContext = _context.Database.BeginTransaction();
            try
            {

                foreach (var c in obj.l_ControlValue)
                {
                    if (!string.IsNullOrEmpty(c.Value))
                    {
                        var cl = _context.GtOpclin.Where(w => w.BusinessKey == obj.BusinessKey
                                    && w.Uhid == obj.UHID && w.Vnumber == obj.VisitNumber
                                    && w.TransactionId == obj.TransactionID
                                    && w.ClcontrolId == c.CLControlID).FirstOrDefault();
                        if (cl == null)
                        {
                            cl = new GtOpclin
                            {
                                BusinessKey = obj.BusinessKey,
                                Uhid = obj.UHID,
                                Vnumber = obj.VisitNumber,
                                TransactionId = obj.TransactionID,
                                TransactionDate = obj.TransactionDate,
                                Cltype = c.CLType,
                                ClcontrolId = c.CLControlID,
                                ValueType = c.ValueType,
                                Value = c.Value,
                                ActiveStatus = true,
                                FormId = obj.FormID,
                                CreatedBy = obj.UserID,
                                CreatedOn = System.DateTime.Now,
                                CreatedTerminal = obj.TerminalID
                            };
                            _context.GtOpclin.Add(cl);
                        }
                        else
                        {
                            cl.ValueType = c.ValueType;
                            cl.Value = c.Value;
                            cl.ActiveStatus = obj.ActiveStatus;
                            cl.ModifiedBy = obj.UserID;
                            cl.ModifiedOn = System.DateTime.Now;
                            cl.ModifiedTerminal = obj.TerminalID;
                        }
                    }
                }

                await _context.SaveChangesAsync();
                dbContext.Commit();
                return new DO_ResponseParameter() { Status = true, Message = "Saved Successfully." };
            }
            catch (DbUpdateException ex)
            {
                dbContext.Rollback();
                throw ex;
            }
            catch (Exception ex)
            {
                dbContext.Rollback();
                throw ex;
            }

        }

        public async Task<List<DO_ClinicalInformation>> GetClinicalInformationValueByTransaction(int businessKey, long UHID, long vNumber, int transactionID)
        {
            var r = await _context.GtOpclin
                    .Where(w => w.BusinessKey == businessKey && w.Uhid == UHID && w.Vnumber == vNumber
                       && w.TransactionId == transactionID
                       && w.ActiveStatus)
                    .Select(x => new DO_ClinicalInformation
                    {
                        TransactionID = x.TransactionId,
                        TransactionDate = x.TransactionDate,
                        ChartNumber = x.ChartNumber,
                        CLControlID = x.ClcontrolId,
                        ValueType = x.ValueType,
                        Value = x.Value
                    }).OrderBy(o => o.TransactionDate).ToListAsync();

            return r;

        }

        public async Task<DO_ResponseParameter> DeletePatientClinicalInformation(DO_ClinicalInformation obj)
        {
            var dbContext = _context.Database.BeginTransaction();
            try
            {
                var cl = _context.GtOpclin.Where(w => w.BusinessKey == obj.BusinessKey
                            && w.Uhid == obj.UHID && w.Vnumber == obj.VisitNumber
                            && w.TransactionId == obj.TransactionID
                            && w.Cltype == obj.CLType).ToList();
                foreach (var c in cl)
                {
                    c.ActiveStatus = false;
                    c.ModifiedBy = obj.UserID;
                    c.ModifiedOn = System.DateTime.Now;
                    c.ModifiedTerminal = obj.TerminalID;
                }


                await _context.SaveChangesAsync();
                dbContext.Commit();
                return new DO_ResponseParameter() { Status = true, Message = "Deleted Successfully." };
            }
            catch (DbUpdateException ex)
            {
                dbContext.Rollback();
                throw ex;
            }
            catch (Exception ex)
            {
                dbContext.Rollback();
                throw ex;
            }
        }



    }
}
