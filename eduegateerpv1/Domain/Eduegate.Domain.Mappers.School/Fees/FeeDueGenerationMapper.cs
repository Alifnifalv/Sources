using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Mappers.School.Accounts;
using Eduegate.Domain.Repository;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Extensions;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.School.Fees;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.VisualBasic;
using System.Globalization;

namespace Eduegate.Domain.Mappers.School.Fees
{
    public class FeeDueGenerationMapper : DTOEntityDynamicMapper
    {
        public static FeeDueGenerationMapper Mapper(CallContext context)
        {
            var mapper = new FeeDueGenerationMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<StudentFeeDueDTO>(entity);
        }
        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            StudentFeeDueDTO feeDueDTO = new StudentFeeDueDTO();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                #region old code start : commented old code, bsc difficult to find debug and old method
                //feeDueDTO = (from stFee in dbContext.StudentFeeDues.AsEnumerable()
                //             join FeeTypeMap in dbContext.FeeDueFeeTypeMaps on stFee.StudentFeeDueIID equals FeeTypeMap.StudentFeeDueID
                //             join FeeMonthlySplit in dbContext.FeeDueMonthlySplits on FeeTypeMap.FeeDueFeeTypeMapsIID equals FeeMonthlySplit.FeeDueFeeTypeMapsID into tmpMonthlySplit
                //             from FeeMonthlySplit in tmpMonthlySplit.DefaultIfEmpty()
                //             where (stFee.StudentFeeDueIID == IID)
                //             orderby stFee.InvoiceDate ascending
                //             group stFee by new { stFee.StudentFeeDueIID, stFee.AcademicYear, stFee.SchoolID, stFee.StudentId, stFee.Student, stFee.DueDate, stFee.ClassId, stFee.SectionID, stFee.Class, stFee.InvoiceNo, stFee.InvoiceDate, stFee.CollectionStatus, stFee.FeeDueFeeTypeMaps, stFee.AccountPostingDate, stFee.IsAccountPost } into stFeeGroup
                //             select new StudentFeeDueDTO()
                //             {
                //                 ClassId = stFeeGroup.Key.ClassId,
                //                 SectionId = stFeeGroup.Key.SectionID,
                //                 SchoolID = stFeeGroup.Key.SchoolID,
                //                 SectionName = stFeeGroup.Key.Student.Section.SectionName,
                //                 Class = new KeyValueDTO() { Key = Convert.ToString(stFeeGroup.Key.ClassId), Value = stFeeGroup.Key.Class.ClassDescription },
                //                 AcademicYear = new KeyValueDTO() { Key = Convert.ToString(stFeeGroup.Key.AcademicYear.AcademicYearID), Value = stFeeGroup.Key.AcademicYear.Description + '(' + stFeeGroup.Key.AcademicYear.AcademicYearCode + ')' },
                //                 StudentId = stFeeGroup.Key.StudentId,
                //                 StudentName = stFeeGroup.Key.Student.AdmissionNumber + '-' + stFeeGroup.Key.Student.FirstName + ' ' + stFeeGroup.Key.Student.MiddleName + ' ' + stFeeGroup.Key.Student.LastName,
                //                 InvoiceNo = stFeeGroup.Key.InvoiceNo,
                //                 InvoiceDate = stFeeGroup.Key.InvoiceDate,
                //                 DueDate = stFeeGroup.Key.DueDate,
                //                 //AccountPostingDate = stFeeGroup.Key.AccountPostingDate,
                //                 StudentFeeDueIID = stFeeGroup.Key.StudentFeeDueIID,
                //                 CollectionStatus = stFeeGroup.Key.CollectionStatus,
                //                 CollectionStatusEdit = stFeeGroup.Key.CollectionStatus,
                //                 IsAccountPost = stFeeGroup.Key.IsAccountPost.Value,
                //                 IsAccountPostEdit = stFeeGroup.Key.IsAccountPost.Value,
                //                 FeeFineMap = (from ft in stFeeGroup.Key.FeeDueFeeTypeMaps//.AsEnumerable()//.AsQueryable()
                //                               where ft.StudentFeeDueID == stFeeGroup.Key.StudentFeeDueIID && ft.FineMasterID != null
                //                               select new FeeDueFeeFineMapDTO()
                //                               {
                //                                   FineMasterID = ft.FineMasterID,
                //                                   FineName = ft.FineMaster.FineName,
                //                                   FineMasterStudentMapID = ft.FineMasterStudentMapID,
                //                                   StudentFeeDueID = ft.StudentFeeDueID,
                //                                   FeeDueFeeTypeMapsID = ft.FeeDueFeeTypeMapsIID,
                //                                   Amount = ft.Amount,
                //                               }).ToList(),
                //                 FeeDueFeeTypeMap = (from ft in stFeeGroup.Key.FeeDueFeeTypeMaps//.AsEnumerable().AsQueryable()
                //                                     where (ft.StudentFeeDueID == stFeeGroup.Key.StudentFeeDueIID && ft.FeeMasterID != null)
                //                                     group ft by new { ft.Status, ft.Amount, ft.TaxAmount, ft.FeePeriodID, ft.TaxPercentage, ft.StudentFeeDueID, ft.FeeDueFeeTypeMapsIID, ft.FeeStructureFeeMapID, ft.FeeMaster, ft.FeePeriod, ft.FeeDueMonthlySplits } into ftGroup
                //                                     select new FeeDueFeeTypeMapDTO()
                //                                     {
                //                                         IsRowSelected = true,
                //                                         FeeCollectionStatus = ftGroup.Key.Status,
                //                                         Amount = ftGroup.Key.Amount,

                //                                         InvoiceNo = stFeeGroup.Key.InvoiceNo,
                //                                         FeePeriodID = ftGroup.Key.FeePeriodID.HasValue ? ftGroup.Key.FeePeriodID : (int?)null,

                //                                         StudentFeeDueID = ftGroup.Key.StudentFeeDueID,
                //                                         FeeStructureFeeMapID = ftGroup.Key.FeeStructureFeeMapID,

                //                                         FeeCycleID = ftGroup.Key.FeeMaster != null ? ftGroup.Key.FeeMaster.FeeCycleID : (byte?)null,

                //                                         FeeDueFeeTypeMapsIID = ftGroup.Key.FeeDueFeeTypeMapsIID,
                //                                         InvoiceDate = stFeeGroup.Key.InvoiceDate,
                //                                         IsFeePeriodDisabled = (ftGroup.Key.FeeMaster != null && ftGroup.Key.FeeMaster.FeeCycleID.HasValue) ? ftGroup.Key.FeeMaster.FeeCycleID.Value != 3 : true,
                //                                         FeeMaster = ftGroup.Key.FeeMaster != null ? new KeyValueDTO() { Key = Convert.ToString(ftGroup.Key.FeeMaster.FeeMasterID), Value = ftGroup.Key.FeeMaster.Description } : new KeyValueDTO(),
                //                                         FeePeriod = ftGroup.Key.FeePeriodID.HasValue ? new KeyValueDTO()
                //                                         {
                //                                             Key = ftGroup.Key.FeePeriodID.HasValue ? Convert.ToString(ftGroup.Key.FeePeriodID) : null,
                //                                             Value = !ftGroup.Key.FeePeriodID.HasValue ? null : ftGroup.Key.FeePeriod.Description + " ( " + ftGroup.Key.FeePeriod.PeriodFrom.ToString("dd/MMM/yyy") + '-'
                //                                                     + ftGroup.Key.FeePeriod.PeriodTo.ToString("dd/MMM/yyy") + " ) "
                //                                         } : new KeyValueDTO(),

                //                                         FeeDueMonthlySplit = (from mf in ftGroup.Key.FeeDueMonthlySplits//.AsEnumerable()
                //                                                               select new FeeDueMonthlySplitDTO()
                //                                                               {
                //                                                                   FeeStructureMontlySplitMapID = (long)(mf.FeeStructureMontlySplitMapID.HasValue ? mf.FeeStructureMontlySplitMapID : 0),
                //                                                                   FeeCollectionStatus = mf.Status,
                //                                                                   FeeDueMonthlySplitIID = mf.FeeDueMonthlySplitIID,
                //                                                                   FeeDueFeeTypeMapsID = mf.FeeDueFeeTypeMapsID,
                //                                                                   MonthID = mf.MonthID != 0 ? int.Parse(Convert.ToString(mf.MonthID)) : 0,
                //                                                                   Year = mf.Year.HasValue ? int.Parse(Convert.ToString(mf.Year)) : 0,
                //                                                                   FeePeriodID = ftGroup.Key.FeePeriodID.HasValue ? ftGroup.Key.FeePeriodID.Value : (int?)null,
                //                                                                   Amount = mf.Amount.HasValue ? mf.Amount : (decimal?)null,
                //                                                                   TaxAmount = mf.TaxAmount.HasValue ? mf.TaxAmount : (decimal?)null,
                //                                                                   TaxPercentage = mf.TaxPercentage.HasValue ? mf.TaxPercentage : (decimal?)null
                //                                                               }).ToList(),
                //                                     }).ToList(),
                //             }).FirstOrDefault();
                #endregion old code

                #region new get code
                var feeDue = dbContext.StudentFeeDues.Where(x => x.StudentFeeDueIID == IID)
                    .Include(i => i.Student).ThenInclude(i => i.Section)
                    .Include(i => i.Class)
                    .Include(i => i.AcademicYear)
                    .Include(i => i.FeeDueFeeTypeMaps).ThenInclude(i => i.FineMaster)
                    .Include(i => i.FeeDueFeeTypeMaps).ThenInclude(i => i.FeePeriod)
                    .Include(i => i.FeeDueFeeTypeMaps).ThenInclude(i => i.FeeMaster)
                    .Include(i => i.FeeDueFeeTypeMaps).ThenInclude(i => i.FeeDueMonthlySplits)
                    .AsNoTracking()
                    .FirstOrDefault();

                feeDueDTO = new StudentFeeDueDTO()
                {
                    ClassId = feeDue?.ClassId,
                    SectionId = feeDue?.SectionID,
                    SchoolID = feeDue?.SchoolID,
                    SectionName = feeDue.Student?.Section?.SectionName,
                    Class = new KeyValueDTO() { Key = Convert.ToString(feeDue?.ClassId), Value = feeDue.Class?.ClassDescription },
                    AcademicYear = new KeyValueDTO() { Key = Convert.ToString(feeDue.AcademicYear?.AcademicYearID), Value = feeDue.AcademicYear?.Description + '(' + feeDue.AcademicYear?.AcademicYearCode + ')' },
                    StudentId = feeDue?.StudentId,
                    StudentName = feeDue.Student?.AdmissionNumber + '-' + feeDue.Student?.FirstName + ' ' + feeDue.Student?.MiddleName + ' ' + feeDue.Student?.LastName,
                    InvoiceNo = feeDue?.InvoiceNo,
                    InvoiceDate = feeDue?.InvoiceDate,
                    DueDate = feeDue?.DueDate,
                    //AccountPostingDate = feeDue.AccountPostingDate,
                    StudentFeeDueIID = feeDue.StudentFeeDueIID,
                    CollectionStatus = feeDue.CollectionStatus,
                    CollectionStatusEdit = feeDue.CollectionStatus,
                    IsAccountPost = feeDue.IsAccountPost.Value,
                    IsAccountPostEdit = feeDue.IsAccountPost.Value,
                    Remarks = feeDue.Remarks,
                };

                var fineMaps = feeDue.FeeDueFeeTypeMaps.Where(f => f.FineMasterID != null).ToList();

                if (fineMaps.Count > 0)
                {
                    foreach (var fine in fineMaps)
                    {
                        feeDueDTO.FeeFineMap.Add(new FeeDueFeeFineMapDTO()
                        {
                            FineMasterID = fine?.FineMasterID,
                            FineName = fine.FineMaster?.FineName,
                            FineMasterStudentMapID = fine.FineMasterStudentMapID,
                            StudentFeeDueID = fine.StudentFeeDueID,
                            FeeDueFeeTypeMapsID = fine.FeeDueFeeTypeMapsIID,
                            Amount = fine?.Amount,
                        });
                    }
                }

                var feeDueFeeType = feeDue.FeeDueFeeTypeMaps.Where(m => m.FeeMasterID != null).ToList();

                if (feeDueFeeType.Count > 0)
                {
                    foreach (var feeType in feeDueFeeType)
                    {
                        feeDueDTO.FeeDueFeeTypeMap.Add(new FeeDueFeeTypeMapDTO()
                        {
                            IsRowSelected = true,
                            FeeCollectionStatus = feeType.Status,
                            Amount = feeType.Amount,

                            InvoiceNo = feeDue.InvoiceNo,
                            FeePeriodID = feeType.FeePeriodID.HasValue ? feeType.FeePeriodID : (int?)null,

                            StudentFeeDueID = feeType.StudentFeeDueID,
                            FeeStructureFeeMapID = feeType.FeeStructureFeeMapID,

                            FeeCycleID = feeType.FeeMaster != null ? feeType.FeeMaster.FeeCycleID : (byte?)null,

                            FeeDueFeeTypeMapsIID = feeType.FeeDueFeeTypeMapsIID,
                            InvoiceDate = feeDue.InvoiceDate,
                            IsFeePeriodDisabled = (feeType.FeeMaster != null && feeType.FeeMaster.FeeCycleID.HasValue) ? feeType.FeeMaster.FeeCycleID.Value != 3 : true,
                            FeeMaster = feeType.FeeMaster != null ? new KeyValueDTO() { Key = Convert.ToString(feeType.FeeMaster.FeeMasterID), Value = feeType.FeeMaster.Description } : new KeyValueDTO(),
                            FeePeriod = feeType.FeePeriodID.HasValue ? new KeyValueDTO()
                            {
                                Key = feeType.FeePeriodID.HasValue ? Convert.ToString(feeType.FeePeriodID) : null,
                                Value = !feeType.FeePeriodID.HasValue ? null : feeType.FeePeriod.Description + " ( " + feeType.FeePeriod.PeriodFrom.ToString("dd/MMM/yyy") + '-'
                                                                     + feeType.FeePeriod.PeriodTo.ToString("dd/MMM/yyy") + " ) "
                            } : new KeyValueDTO(),
                        });

                        if (feeType.FeeDueMonthlySplits.Count > 0)
                        {
                            foreach (var monthsplit in feeType.FeeDueMonthlySplits)
                            {
                                feeDueDTO.FeeDueFeeTypeMap.FirstOrDefault().FeeDueMonthlySplit.Add(new FeeDueMonthlySplitDTO()
                                {
                                    FeeStructureMontlySplitMapID = (long)(monthsplit.FeeStructureMontlySplitMapID.HasValue ? monthsplit.FeeStructureMontlySplitMapID : 0),
                                    FeeCollectionStatus = monthsplit.Status,
                                    FeeDueMonthlySplitIID = monthsplit.FeeDueMonthlySplitIID,
                                    FeeDueFeeTypeMapsID = monthsplit.FeeDueFeeTypeMapsID,
                                    MonthID = monthsplit.MonthID != 0 ? int.Parse(Convert.ToString(monthsplit.MonthID)) : 0,
                                    Year = monthsplit.Year.HasValue ? int.Parse(Convert.ToString(monthsplit.Year)) : 0,
                                    FeePeriodID = feeType.FeePeriodID.HasValue ? feeType.FeePeriodID.Value : (int?)null,
                                    Amount = monthsplit.Amount.HasValue ? monthsplit.Amount : (decimal?)null,
                                    TaxAmount = monthsplit.TaxAmount.HasValue ? monthsplit.TaxAmount : (decimal?)null,
                                    TaxPercentage = monthsplit.TaxPercentage.HasValue ? monthsplit.TaxPercentage : (decimal?)null
                                });
                            }
                        }
                    }
                }
                #endregion end new code
            }

            return ToDTOString(feeDueDTO);
        }


        public List<StudentFeeDueDTO> FillFeeDue(int classId, long studentId)
        {
            List<StudentFeeDueDTO> _sRetData = new List<StudentFeeDueDTO>();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var dFnd = dbContext.StudentFeeDues
                            .Where(n => n.CollectionStatus != true && n.StudentId == studentId && n.IsCancelled != true)
                            .Include(w => w.FeeDueFeeTypeMaps)
                            .AsNoTracking().ToList();

                if (dFnd.Any())
                {
                    dFnd.All(w => { _sRetData.Add(GetStudentFeeDue(dbContext, w)); return true; });
                }

            }
            return _sRetData;


            List<StudentFeeDueDTO> feeDueDTOList = new List<StudentFeeDueDTO>();
            List<FeeDueFeeTypeMapDTO> feeDueFeeTypeMapDTOList = new List<FeeDueFeeTypeMapDTO>();
            List<FeeDueMonthlySplitDTO> feeDueMonthlySplitsList = new List<FeeDueMonthlySplitDTO>();
            using (var dbContext = new dbEduegateSchoolContext())
            {




                var feeDueDTO = (from stFee in dbContext.StudentFeeDues
                                 join FeeTypeMap in dbContext.FeeDueFeeTypeMaps on stFee.StudentFeeDueIID equals FeeTypeMap.StudentFeeDueID
                                 join FeeMonthlySplit in dbContext.FeeDueMonthlySplits on FeeTypeMap.FeeDueFeeTypeMapsIID equals FeeMonthlySplit.FeeDueFeeTypeMapsID into tmpMonthlySplit
                                 from FeeMonthlySplit in tmpMonthlySplit.DefaultIfEmpty()
                                 where (stFee.StudentId == studentId && stFee.CollectionStatus == false)
                                 orderby stFee.InvoiceDate ascending
                                 select stFee
                               ).Include(i => i.FeeDueFeeTypeMaps).ThenInclude(i => i.FeePeriod)
                                .Include(i => i.FeeDueFeeTypeMaps).ThenInclude(i => i.FeeDueMonthlySplits)
                                .ToList();

                //group stFee by new { stFee.StudentFeeDueIID, stFee.StudentId, stFee.ClassId, stFee.InvoiceNo, stFee.InvoiceDate, stFee.FeeDueFeeTypeMaps } into stFeeGroup

                feeDueDTOList = feeDueDTO.Select(stFeeGroup => new StudentFeeDueDTO()
                {
                    StudentFeeDueIID = stFeeGroup.StudentFeeDueIID,
                    StudentId = stFeeGroup.StudentId,
                    ClassId = stFeeGroup.ClassId,
                    InvoiceNo = stFeeGroup.InvoiceNo,
                    InvoiceDate = stFeeGroup.InvoiceDate,
                    IsExternal = stFeeGroup.FeeDueFeeTypeMaps.FirstOrDefault().FeeMaster.IsExternal,
                    FeeDueFeeTypeMap = (from ft in stFeeGroup.FeeDueFeeTypeMaps
                                        join cnf in dbContext.CreditNoteFeeTypeMaps on  //.Where(x => x.SchoolCreditNote.StudentID == stFeeGroup.StudentId && x.Status == false) on
                                        ft.FeeMasterID equals cnf.FeeMasterID
                                        into tmpCreditNoteFeeType
                                        from cnf in tmpCreditNoteFeeType.DefaultIfEmpty()
                                        where ft.Status == false && ft.FeeMasterID != null
                                        //&& cnf.SchoolCreditNote.StudentID == stFeeGroup.StudentId && cnf.Status == false
                                        group ft by new
                                        {
                                            crdtAmt = cnf == null ? 0 : cnf.Amount
                                        ?? 0,
                                            ft.Amount,
                                            ft.TaxAmount,
                                            ft.FeePeriodID,
                                            ft.TaxPercentage,
                                            ft.StudentFeeDueID,
                                            ft.FeeDueFeeTypeMapsIID,
                                            ft.FeeStructureFeeMapID,
                                            ft.FeeMaster,
                                            ft.FeePeriod,
                                            ft.FeeMaster.IsExternal,
                                            ft.FeeDueMonthlySplits
                                        } into ftGroup
                                        select new FeeDueFeeTypeMapDTO()
                                        {
                                            Amount = ftGroup.Key.Amount - (ftGroup.Key.crdtAmt),
                                            CreditNoteAmount = ftGroup.Key.crdtAmt,
                                            TaxAmount = ftGroup.Key.TaxAmount,
                                            InvoiceNo = stFeeGroup.InvoiceNo,
                                            FeePeriodID = ftGroup.Key.FeePeriodID,
                                            TaxPercentage = ftGroup.Key.TaxPercentage,
                                            StudentFeeDueID = ftGroup.Key.StudentFeeDueID,
                                            FeeStructureFeeMapID = ftGroup.Key.FeeStructureFeeMapID,
                                            //ClassFeeMasterID = ftGroup.Key.ClassFeeMasterID,
                                            FeeCycleID = ftGroup.Key.FeeMaster.FeeCycleID,
                                            //FeeMasterClassMapID = ftGroup.Key.FeeMasterClassMapID,
                                            FeeDueFeeTypeMapsIID = ftGroup.Key.FeeDueFeeTypeMapsIID,
                                            InvoiceDate = stFeeGroup.InvoiceDate,
                                            IsExternal = ftGroup.Key.IsExternal,
                                            IsFeePeriodDisabled = ftGroup.Key.FeeMaster.FeeCycleID.HasValue ? ftGroup.Key.FeeMaster.FeeCycleID.Value != 3 : true,
                                            FeeMaster = ftGroup.Key.FeeMaster != null ? new KeyValueDTO()
                                            {
                                                Key = ftGroup.Key.FeeMaster != null ? Convert.ToString(ftGroup.Key.FeeMaster.FeeMasterID) : null,
                                                Value = ftGroup.Key.FeeMaster != null ? ftGroup.Key.FeeMaster.Description : null
                                            } : new KeyValueDTO(),
                                            FeePeriod = ftGroup.Key.FeePeriodID.HasValue ? new KeyValueDTO()
                                            {
                                                Key = ftGroup.Key.FeePeriodID.HasValue ? Convert.ToString(ftGroup.Key.FeePeriodID) : null,
                                                Value = !ftGroup.Key.FeePeriodID.HasValue ? null : ftGroup.Key.FeePeriod.Description + " ( " + ftGroup.Key.FeePeriod.PeriodFrom.ToString("dd/MMM/yyy") + '-'
                                                        + ftGroup.Key.FeePeriod.PeriodTo.ToString("dd/MMM/yyy") + " ) "
                                            } : new KeyValueDTO(),

                                            //FeeDueMonthlySplit = (from mf in ftGroup.Key.FeeDueMonthlySplits
                                            //                      join crno in dbContext.SchoolCreditNotes on
                                            //                      stFeeGroup.StudentId equals crno.StudentID
                                            //                      into tmpcrNot
                                            //                      from crno in tmpcrNot
                                            //                      //.Where(cr => cr.StudentID == stFeeGroup.StudentId
                                            //                      //&& cr.CreditNoteDate.Value.Month == mf.MonthID
                                            //                      //&& cr.CreditNoteDate.Value.Year == mf.Year && cr.Status == false)
                                            //                      .DefaultIfEmpty()
                                            //                      join cnf in dbContext.CreditNoteFeeTypeMaps on
                                            //                      //(crno == null ? 0 : crno.SchoolCreditNoteIID) equals cnf.SchoolCreditNoteID
                                            //                      crno.SchoolCreditNoteIID equals cnf.SchoolCreditNoteID
                                            //                      into tmpCreditNoteFeeType
                                            //                      from cnf in tmpCreditNoteFeeType
                                            //                          //.Where(ft => ft.FeeMasterID == mf.FeeDueFeeTypeMap.FeeMasterID && ft.Status == false)
                                            //                          //.DefaultIfEmpty()
                                            //                      where mf.Status == false && crno != null && crno.StudentID == studentId && crno.CreditNoteDate.Value.Month == mf.MonthID
                                            //                     && crno.CreditNoteDate.Value.Year == mf.Year && crno.Status == false &&
                                            //                     cnf != null && cnf.FeeMasterID == mf.FeeDueFeeTypeMap.FeeMasterID && cnf.Status == false
                                            //                      select new FeeDueMonthlySplitDTO()
                                            //                      {
                                            //                          FeeStructureMontlySplitMapID = mf.FeeStructureMontlySplitMapID.HasValue ? mf.FeeStructureMontlySplitMapID.Value : 0,
                                            //                          FeeDueMonthlySplitIID = mf.FeeDueMonthlySplitIID,
                                            //                          FeeDueFeeTypeMapsID = mf.FeeDueFeeTypeMapsID,
                                            //                          MonthID = mf.MonthID != 0 ? int.Parse(Convert.ToString(mf.MonthID)) : 0,
                                            //                          Amount = mf.Amount.HasValue ? mf.Amount : (decimal?)null,
                                            //                          CreditNoteAmount = (cnf != null ? cnf.Amount : 0),
                                            //                          Balance = mf.Amount.Value - (cnf != null ? cnf.Amount : 0),
                                            //                          TaxAmount = mf.TaxAmount.HasValue ? mf.TaxAmount : (decimal?)null,
                                            //                          TaxPercentage = mf.TaxPercentage.HasValue ? mf.TaxPercentage : (decimal?)null
                                            //                      }).ToList(),
                                        }).ToList(),

                }).ToList();


            }

            return feeDueDTOList;
        }


        public List<StudentFeeDueDTO> GetFeeCollected(long studentId)
        {
            DateTime currentDate = System.DateTime.Now;
            DateTime preYearDate = DateTime.Now.AddYears(-1);

            List<StudentFeeDueDTO> feeDueDTOList = new List<StudentFeeDueDTO>();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var feeDueDTO = (from stFee in dbContext.StudentFeeDues
                                 join ctFee in dbContext.FeeCollections on stFee.StudentId equals ctFee.StudentID
                                 join FeeTypeMap in dbContext.FeeDueFeeTypeMaps on stFee.StudentFeeDueIID equals FeeTypeMap.StudentFeeDueID
                                 join FeeMonthlySplit in dbContext.FeeDueMonthlySplits on FeeTypeMap.FeeDueFeeTypeMapsIID equals FeeMonthlySplit.FeeDueFeeTypeMapsID into tmpMonthlySplit
                                 from FeeMonthlySplit in tmpMonthlySplit.DefaultIfEmpty()
                                 where (stFee.StudentId == studentId && (ctFee.CollectionDate != null
                                 /*(ctFee.CollectionDate.Value.Date) <= (EntityFunctions.TruncateTime(currentDate.Date)) && (ctFee.CollectionDate.Value.Date) >= (EntityFunctions.TruncateTime(preYearDate.Date))*/))
                                 orderby ctFee.CollectionDate descending
                                 select stFee).AsNoTracking().ToList();

                feeDueDTOList = feeDueDTO.Select(stFeeGroup => new StudentFeeDueDTO()
                {
                    StudentFeeDueIID = stFeeGroup.StudentFeeDueIID,
                    StudentId = stFeeGroup.StudentId,
                    ClassId = stFeeGroup.ClassId,
                    //InvoiceNo = stFeeGroup.Key.FeeReceiptNo,
                    //InvoiceDate = stFeeGroup.Key.CollectionDate,
                    InvoiceNo = stFeeGroup.InvoiceNo,
                    InvoiceDate = stFeeGroup.InvoiceDate,

                    FeeDueFeeTypeMap = (from ft in stFeeGroup.FeeDueFeeTypeMaps//.AsEnumerable().AsQueryable()
                                        where ft.Status == true && ft.FeeMasterID != null
                                        group ft by new { ft.Amount, ft.TaxAmount, ft.FeePeriodID, ft.TaxPercentage, ft.StudentFeeDueID, ft.FeeDueFeeTypeMapsIID, ft.FeeStructureFeeMapID, ft.FeeMaster, ft.FeePeriod, ft.FeeDueMonthlySplits } into ftGroup
                                        select new FeeDueFeeTypeMapDTO()
                                        {
                                            Amount = ftGroup.Key.Amount,
                                            TaxAmount = ftGroup.Key.TaxAmount,
                                            InvoiceNo = stFeeGroup.InvoiceNo,
                                            FeePeriodID = ftGroup.Key.FeePeriodID,
                                            TaxPercentage = ftGroup.Key.TaxPercentage,
                                            StudentFeeDueID = ftGroup.Key.StudentFeeDueID,
                                            FeeStructureFeeMapID = ftGroup.Key.FeeStructureFeeMapID,
                                            FeeCycleID = ftGroup.Key.FeeMaster.FeeCycleID,
                                            FeeDueFeeTypeMapsIID = ftGroup.Key.FeeDueFeeTypeMapsIID,
                                            IsFeePeriodDisabled = ftGroup.Key.FeeMaster.FeeCycleID.HasValue ? ftGroup.Key.FeeMaster.FeeCycleID.Value != 3 : true,
                                            FeeMaster = ftGroup.Key.FeeMaster != null ? new KeyValueDTO()
                                            {
                                                Key = ftGroup.Key.FeeMaster != null ? Convert.ToString(ftGroup.Key.FeeMaster.FeeMasterID) : null,
                                                Value = ftGroup.Key.FeeMaster != null ? ftGroup.Key.FeeMaster.Description : null
                                            } : new KeyValueDTO(),
                                            FeePeriod = ftGroup.Key.FeePeriodID.HasValue ? new KeyValueDTO()
                                            {
                                                Key = ftGroup.Key.FeePeriodID.HasValue ? Convert.ToString(ftGroup.Key.FeePeriodID) : null,
                                                Value = !ftGroup.Key.FeePeriodID.HasValue ? null : ftGroup.Key.FeePeriod.Description + " ( " + ftGroup.Key.FeePeriod.PeriodFrom.ToString("dd/MMM/yyy") + '-'
                                                        + ftGroup.Key.FeePeriod.PeriodTo.ToString("dd/MMM/yyy") + " ) "
                                            } : new KeyValueDTO(),

                                            FeeDueMonthlySplit = (from mf in ftGroup.Key.FeeDueMonthlySplits
                                                                  where mf.Status == true
                                                                  select new FeeDueMonthlySplitDTO()
                                                                  {
                                                                      //FeeStructureMontlySplitMapID = mf.FeeStructureMontlySplitMapID.Value,
                                                                      FeeDueMonthlySplitIID = mf.FeeDueMonthlySplitIID,
                                                                      FeeDueFeeTypeMapsID = mf.FeeDueFeeTypeMapsID,
                                                                      MonthID = mf.MonthID != 0 ? int.Parse(Convert.ToString(mf.MonthID)) : 0,
                                                                      Year = mf.Year.HasValue ? mf.Year.Value : 0,
                                                                      FeePeriodID = mf.FeePeriodID,
                                                                      Amount = mf.Amount.HasValue ? mf.Amount : (decimal?)null,
                                                                      TaxAmount = mf.TaxAmount.HasValue ? mf.TaxAmount : (decimal?)null,
                                                                      TaxPercentage = mf.TaxPercentage.HasValue ? mf.TaxPercentage : (decimal?)null
                                                                  }).ToList(),
                                        }).ToList(),

                }).ToList();
            }
            return feeDueDTOList;
        }

        public List<FeeCollectionDTO> GetStudentFeeCollection(long _mStudentID)
        {
            List<FeeCollectionDTO> _sRetData = new List<FeeCollectionDTO>();
            using (var _sContext = new dbEduegateSchoolContext())
            {
                int _sAcademicYearId = _sContext.Students.Where(w => w.StudentIID == _mStudentID).AsNoTracking().FirstOrDefault().AcademicYearID ?? 0;

                var dFnd = _sContext.FeeCollections.Where(n => n.StudentID == _mStudentID && n.AcadamicYearID == _sAcademicYearId)
                            .Include(w => w.FeeCollectionFeeTypeMaps)
                            .AsNoTracking().ToList();

                if (dFnd.Any())
                {
                    dFnd.All(w => { _sRetData.Add(GetStudentFeeCollection(_sContext, w)); return true; });
                }
            }
            return _sRetData;
        }


        private FeeCollectionDTO GetStudentFeeCollection(dbEduegateSchoolContext _sContext, FeeCollection _sFeeColl)
        {
            FeeCollectionDTO _sRetData = new FeeCollectionDTO()
            {
                AcadamicYearID = _sFeeColl.AcadamicYearID,
                AcademicYear = null,
                AdmissionNo = string.Empty,
                Amount = _sFeeColl.Amount,
                CashierEmployee = null,
                CashierID = _sFeeColl.CashierID,
                ClassFeeMaster = string.Empty,
                ClassFeeMasterId = _sFeeColl.ClassFeeMasterId,
                ClassID = _sFeeColl.ClassID,
                ClassName = string.Empty,
                CollectionDate = _sFeeColl.CollectionDate,
                CreatedBy = _sFeeColl.CreatedBy,
                CreatedDate = _sFeeColl.CreatedDate,
                Description = string.Empty,
                DiscountAmount = _sFeeColl.DiscountAmount,
                FeeCollectionIID = _sFeeColl.FeeCollectionIID,
                FeeReceiptNo = _sFeeColl.FeeReceiptNo,
                FeeTypes = GetFeeCollectionTypeMap(_sContext, _sFeeColl),
                IsPaid = _sFeeColl.IsPaid,
                PaidAmount = _sFeeColl.PaidAmount,
                FineAmount = _sFeeColl.FineAmount,
                Remarks = _sFeeColl.Remarks,
                SchoolID = _sFeeColl.SchoolID,
                SectionID = _sFeeColl.SectionID,
                SectionName = string.Empty,
                StudentID = _sFeeColl.StudentID,
                StudentName = string.Empty,
            };

            return _sRetData;
        }


        private List<FeeCollectionFeeTypeDTO> GetFeeCollectionTypeMap(dbEduegateSchoolContext _sContext, FeeCollection _sFeeColl)
        {
            List<FeeCollectionFeeTypeDTO> _sRetData = new List<FeeCollectionFeeTypeDTO>();
            _sFeeColl.FeeCollectionFeeTypeMaps.ToList().All(w => { _sRetData.Add(GetFeeCollectionTypeMap(_sContext, _sFeeColl, w)); return true; });
            return _sRetData;
        }



        private FeeCollectionFeeTypeDTO GetFeeCollectionTypeMap(dbEduegateSchoolContext _sContext, FeeCollection _sFeeColl, FeeCollectionFeeTypeMap _sFeeCollMap)
        {
            var dFeeMaster = _sContext.FeeMasters.Where(w => w.FeeMasterID == (_sFeeCollMap.FeeMasterID ?? 0));
            var dFeePeriod = _sContext.FeePeriods.Where(w => w.FeePeriodID == (_sFeeCollMap.FeePeriodID ?? 0));
            var dMontlySplit = GetFeeCollectionMonthlySplit(_sContext, _sFeeCollMap);
            decimal _sCreditNoteAmount = 0;
            //if (dCreditNotes.Any(w => w.FeeMasterID == _sFeeDueMap.FeeMasterID))
            //{
            //    if (dMontlySplit.Any())
            //    {
            //        dMontlySplit.All(w => { SetCreditNoteMonthly(_sContext, w, dCreditNotes.FindAll(x => x.FeeMasterID == _sFeeDueMap.FeeMasterID)); return true; });
            //        _sCreditNoteAmount = dMontlySplit.Any(w => (w.CreditNoteAmount ?? 0) != 0) ? dMontlySplit.Sum(w => (w.CreditNoteAmount ?? 0)) : 0;
            //    }
            //    else
            //    {
            //        _sCreditNoteAmount = dCreditNotes.Sum(w => w.Amount ?? 0);
            //    }
            //}
            long _sCreditNoteFeeTypeMapID = 0;// dCreditNotes.Any() ? dCreditNotes.FirstOrDefault().CreditNoteFeeTypeMapIID : 0;
            FeeCollectionFeeTypeDTO _sRetData = new FeeCollectionFeeTypeDTO()
            {
                Amount = (_sFeeCollMap.Amount ?? 0),
                CreditNoteAmount = _sFeeCollMap.CreditNoteAmount,
                Balance = _sFeeCollMap.Balance,
                CreditNoteFeeTypeMapID = _sCreditNoteFeeTypeMapID,
                //InvoiceDate = _sFeeDue.InvoiceDate,
                //InvoiceNo = _sFeeDue.InvoiceNo,
                CollectedAmount = _sFeeCollMap.Balance ?? 0,
                CreatedBy = _sFeeCollMap.CreatedBy,
                CreatedDate = _sFeeCollMap.CreatedDate,
                FeeCycleID = 0,
                FeeMaster = dFeeMaster.FirstOrDefault().Description,// dFeeMaster.Any() ? new KeyValueDTO() { Key = dFeeMaster.FirstOrDefault().FeeMasterID.ToString(), Value = dFeeMaster.FirstOrDefault().Description } : new KeyValueDTO(),
                FeeMasterID = _sFeeCollMap.FeeMasterID,
                FeePeriod = dFeePeriod.Any() ? dFeePeriod.FirstOrDefault().Description : "",//dFeePeriod.Any() ? new KeyValueDTO() { Key = dFeePeriod.FirstOrDefault().FeePeriodID.ToString(), Value = dFeePeriod.FirstOrDefault().Description } : new KeyValueDTO(),
                FeePeriodID = _sFeeCollMap.FeePeriodID,
                FeeStructureFeeMapID = 0,
                StudentFeeDueID = 0,//_sFeeCollMap.StudentFeeDueID,
                TaxAmount = _sFeeCollMap.TaxAmount,
                TaxPercentage = _sFeeCollMap.TaxPercentage,
                UpdatedBy = _sFeeCollMap.UpdatedBy,
                UpdatedDate = _sFeeCollMap.UpdatedDate,
                IsFeePeriodDisabled = false,
                RefundAmount = 0,
                MontlySplitMaps = dMontlySplit,
                FeeCollectionFeeTypeMapsIID = _sFeeCollMap.FeeCollectionFeeTypeMapsIID,
                FEECollectionID = _sFeeCollMap.FeeCollectionID,
                FeeDueFeeTypeMapsID = _sFeeCollMap.FeeDueFeeTypeMapsID,
            };
            return _sRetData;

        }

        private List<FeeCollectionMonthlySplitDTO> GetFeeCollectionMonthlySplit(dbEduegateSchoolContext _sContext, FeeCollectionFeeTypeMap _sFeeCollMap)
        {
            List<FeeCollectionMonthlySplitDTO> _sRetData = new List<FeeCollectionMonthlySplitDTO>();
            if ((_sFeeCollMap.FeePeriodID ?? 0) == 0)
                return _sRetData;
            var dFnd = from n in _sContext.FeeCollectionMonthlySplits.Where(w => w.FeeCollectionFeeTypeMapId == _sFeeCollMap.FeeDueFeeTypeMapsID)
                       select new FeeCollectionMonthlySplitDTO
                       {
                           FeeCollectionMonthlySplitIID = n.FeeCollectionMonthlySplitIID,
                           FeeDueMonthlySplitID = n.FeeDueMonthlySplitID ?? 0,
                           FeeCollectionFeeTypeMapId = n.FeeCollectionFeeTypeMapId,
                           MonthID = n.MonthID,
                           Amount = n.Amount ?? 0,
                           Year = n.Year,
                           CreditNoteAmount = n.CreditNoteAmount,
                           Balance = n.Amount ?? 0,
                           TaxAmount = n.TaxAmount ?? 0,
                           TaxPercentage = n.TaxPercentage ?? 0,
                           RefundAmount = 0,
                       };
            if (dFnd.Any())
                _sRetData.AddRange(dFnd);
            return _sRetData;
        }


        //public List<FeeDueFeeTypeMapDTO> FillFeeDueForSettlement(long studentId)
        //{
        //    List<FeeDueFeeTypeMapDTO> feeDueDTO = new List<FeeDueFeeTypeMapDTO>();

        //    using (var dbContext = new dbEduegateSchoolContext())
        //    {


        //        feeDueDTO = (from stFee in dbContext.FeeDueFeeTypeMaps.AsEnumerable()
        //                     where (stFee.StudentFeeDue.StudentId == studentId &&
        //                     stFee.StudentFeeDue.CollectionStatus == false && stFee.Status == false)
        //                     orderby stFee.StudentFeeDue.InvoiceDate ascending
        //                     group stFee by new
        //                     {
        //                         stFee.StudentFeeDueID,
        //                         stFee.StudentFeeDue.InvoiceNo,
        //                         stFee.StudentFeeDue.InvoiceDate,
        //                         stFee.Amount,
        //                         stFee.CollectedAmount,
        //                         stFee.ClassFeeMasterID,
        //                         stFee.FeePeriodID,
        //                         stFee.FeeMasterClassMapID,
        //                         stFee.FeeMaster,
        //                         stFee.FeeDueFeeTypeMapsIID,
        //                         stFee.FeePeriod,
        //                         stFee.FeeDueMonthlySplits,
        //                         stFee.Status

        //                     } into ftGroup
        //                     select new FeeDueFeeTypeMapDTO()
        //                     {
        //                         CollectedAmount = ftGroup.Key.CollectedAmount,
        //                         FeeCollectionStatus = ftGroup.Key.Status,
        //                         Amount = ftGroup.Key.Amount,
        //                         InvoiceNo = ftGroup.Key.InvoiceNo,
        //                         FeePeriodID = ftGroup.Key.FeePeriodID,
        //                         StudentFeeDueID = ftGroup.Key.StudentFeeDueID,
        //                         ClassFeeMasterID = ftGroup.Key.FeeMaster.FeeCycleID,
        //                         FeeMasterClassMapID = ftGroup.Key.FeeMasterClassMapID,
        //                         FeeDueFeeTypeMapsIID = ftGroup.Key.FeeDueFeeTypeMapsIID,
        //                         InvoiceDate = ftGroup.Key.InvoiceDate,
        //                         FeeMasterID = ftGroup.Key.FeeMaster != null ? ftGroup.Key.FeeMaster.FeeMasterID : (int?)null,
        //                         IsRefundable = ftGroup.Key.FeeMaster != null ? ftGroup.Key.FeeMaster.FeeType.IsRefundable.Value : false,
        //                         IsFeePeriodDisabled = ftGroup.Key.FeeMaster.FeeCycleID.HasValue ? ftGroup.Key.FeeMaster.FeeCycleID.Value != 3 : true,
        //                         FeeMaster = ftGroup.Key.FeeMaster != null ? new KeyValueDTO()
        //                         {
        //                             Key = ftGroup.Key.FeeMaster != null ? Convert.ToString(ftGroup.Key.FeeMaster.FeeMasterID) : null,
        //                             Value = ftGroup.Key.FeeMaster != null ? ftGroup.Key.FeeMaster.Description : null
        //                         } : new KeyValueDTO(),
        //                         FeePeriod = ftGroup.Key.FeePeriodID.HasValue ? new KeyValueDTO()
        //                         {
        //                             Key = ftGroup.Key.FeePeriodID.HasValue ? Convert.ToString(ftGroup.Key.FeePeriodID) : null,
        //                             Value = !ftGroup.Key.FeePeriodID.HasValue ? null : ftGroup.Key.FeePeriod.Description + " ( " + ftGroup.Key.FeePeriod.PeriodFrom.ToString("dd/MMM/yyy") + '-'
        //                                                              + ftGroup.Key.FeePeriod.PeriodTo.ToString("dd/MMM/yyy") + " ) "
        //                         } : new KeyValueDTO(),

        //                         FeeDueMonthlySplit = (from mf in ftGroup.Key.FeeDueMonthlySplits//.AsEnumerable()
        //                                               where mf.Status == false
        //                                               select new FeeDueMonthlySplitDTO()
        //                                               {
        //                                                   FeeDueMonthlySplitIID = mf.FeeDueMonthlySplitIID,
        //                                                   FeeDueFeeTypeMapsID = mf.FeeDueFeeTypeMapsID,
        //                                                   MonthID = mf.MonthID != 0 ? int.Parse(Convert.ToString(mf.MonthID)) : 0,
        //                                                   Amount = mf.Amount.HasValue ? mf.Amount : (decimal?)null,
        //                                                   FeeCollectionStatus = mf.Status

        //                                               }).ToList()
        //                     }).ToList();

        //        var IIDs = feeDueDTO.Select(a => a.FeeMasterID).ToList();
        //        var feeMasters = dbContext.FeeMasters.Where(x => x.FeeType.IsRefundable == true &&
        //          !IIDs.Contains(x.FeeMasterID)).ToList();

        //        if (feeMasters.IsNotNull())
        //        {
        //            foreach (var feeMst in feeMasters)
        //            {
        //                var feeDueRefundableDTO = new FeeDueFeeTypeMapDTO()
        //                {
        //                    IsRefundable = true,
        //                    Amount = feeMst.Amount,
        //                    FeeMaster = new KeyValueDTO()
        //                    {
        //                        Key = feeMst.FeeMasterID.ToString(),
        //                        Value = feeMst.Description
        //                    },
        //                    FeeMasterID = feeMst != null ? feeMst.FeeMasterID : (int?)null,
        //                    CollectedAmount = 0,
        //                    FeeCollectionStatus = false,

        //                };
        //                feeDueDTO.Add(feeDueRefundableDTO);
        //            }
        //        }


        //    }

        //    return feeDueDTO;
        //}


        private List<FeeCollectionFeeTypeDTO> GetFeeCollectionTypeMapForSettlement(dbEduegateSchoolContext _sContext, FeeCollection _sFeeColl)
        {
            List<FeeCollectionFeeTypeDTO> _sRetData = new List<FeeCollectionFeeTypeDTO>();
            _sFeeColl.FeeCollectionFeeTypeMaps.ToList().Where(x => x.FeeMaster.FeeType.IsRefundable == true).All(w => { _sRetData.Add(GetFeeCollectionTypeMap(_sContext, _sFeeColl, w)); return true; });
            return _sRetData;
        }
        public List<FeeDueFeeTypeMapDTO> FillFeeDueForSettlement(long studentId, int academicId)
        {

            // var feeDueDTO = new List<FeeDueFeeTypeMapDTO>();
            var feeCollectedDTO = new List<FeeDueFeeTypeMapDTO>();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                feeCollectedDTO = (from stFee in dbContext.FeeDueFeeTypeMaps
                                   join feemas in dbContext.FeeMasters on stFee.FeeMasterID equals feemas.FeeMasterID
                                   join feeCollect in dbContext.FeeCollectionFeeTypeMaps on stFee.FeeDueFeeTypeMapsIID equals feeCollect.FeeDueFeeTypeMapsID
                                   where (stFee.FeeMaster != null && stFee.FeeMasterID == feeCollect.FeeMasterID && stFee.StudentFeeDue.StudentId == studentId
                                   && stFee.StudentFeeDue.AcadamicYearID == academicId
                                   && stFee.Status == true && feemas.FeeType.IsRefundable == true)
                                   orderby stFee.StudentFeeDue.InvoiceDate ascending
                                   group stFee by new
                                   {
                                       stFee.StudentFeeDueID,
                                       stFee.StudentFeeDue.InvoiceNo,
                                       stFee.StudentFeeDue.InvoiceDate,
                                       stFee.Amount,
                                       stFee.CollectedAmount,
                                       stFee.ClassFeeMasterID,
                                       stFee.FeePeriodID,
                                       stFee.FeeMasterClassMapID,
                                       stFee.FeeMaster,
                                       stFee.FeeDueFeeTypeMapsIID,
                                       stFee.FeePeriod,
                                       stFee.FeeDueMonthlySplits,
                                       stFee.Status,
                                       feeCollect.CreditNoteAmount,
                                       feeCollect.FeeCollectionFeeTypeMapsIID,
                                       feeCollect.FeeCollectionMonthlySplits

                                   } into ftGroup
                                   select new FeeDueFeeTypeMapDTO()
                                   {
                                       CreditNoteAmount = ftGroup.Key.CreditNoteAmount.HasValue ? ftGroup.Key.CreditNoteAmount : (decimal?)null,
                                       FeeCollectionFeeTypeMapsID = ftGroup.Key.FeeCollectionFeeTypeMapsIID != null ? ftGroup.Key.FeeCollectionFeeTypeMapsIID : (long?)null,
                                       CollectedAmount = ftGroup.Key.CollectedAmount,
                                       FeeCollectionStatus = ftGroup.Key.Status,
                                       Amount = ftGroup.Key.Amount,
                                       InvoiceNo = ftGroup.Key.InvoiceNo,
                                       FeePeriodID = ftGroup.Key.FeePeriodID,
                                       StudentFeeDueID = ftGroup.Key.StudentFeeDueID,
                                       //ClassFeeMasterID = ftGroup.Key.FeeMaster.FeeCycleID,
                                       //FeeMasterClassMapID = ftGroup.Key.FeeMasterClassMapID,
                                       FeeDueFeeTypeMapsIID = ftGroup.Key.FeeDueFeeTypeMapsIID,
                                       InvoiceDate = ftGroup.Key.InvoiceDate,
                                       FeeMasterID = ftGroup.Key.FeeMaster != null ? ftGroup.Key.FeeMaster.FeeMasterID : (int?)null,
                                       IsRefundable = ftGroup.Key.FeeMaster != null ? ftGroup.Key.FeeMaster.FeeType.IsRefundable.Value : false,
                                       IsFeePeriodDisabled = ftGroup.Key.FeeMaster.FeeCycleID.HasValue ? ftGroup.Key.FeeMaster.FeeCycleID.Value != 3 : true,
                                       FeeMaster = ftGroup.Key.FeeMaster != null ? new KeyValueDTO()
                                       {
                                           Key = ftGroup.Key.FeeMaster != null ? Convert.ToString(ftGroup.Key.FeeMaster.FeeMasterID) : null,
                                           Value = ftGroup.Key.FeeMaster != null ? ftGroup.Key.FeeMaster.Description : null
                                       } : new KeyValueDTO(),
                                       FeePeriod = ftGroup.Key.FeePeriodID.HasValue ? new KeyValueDTO()
                                       {
                                           Key = ftGroup.Key.FeePeriodID.HasValue ? Convert.ToString(ftGroup.Key.FeePeriodID) : null,
                                           Value = !ftGroup.Key.FeePeriodID.HasValue ? null : ftGroup.Key.FeePeriod.Description + " ( " + ftGroup.Key.FeePeriod.PeriodFrom.ToString("dd/MMM/yyy") + '-'
                                                                            + ftGroup.Key.FeePeriod.PeriodTo.ToString("dd/MMM/yyy") + " ) "
                                       } : new KeyValueDTO(),

                                       //FeeDueMonthlySplit = (from mf in ftGroup.Key.FeeDueMonthlySplits//.AsEnumerable()
                                       //                      join fcollm in ftGroup.Key.FeeCollectionMonthlySplits on mf.FeeDueMonthlySplitIID equals fcollm.FeeDueMonthlySplitID
                                       //                      where mf.Status == true
                                       //                      select new FeeDueMonthlySplitDTO()
                                       //                      {
                                       //                          FeeCollectionMonthlySplitID = fcollm.FeeDueMonthlySplitID,

                                       //                          FeeDueMonthlySplitIID = mf.FeeDueMonthlySplitIID,
                                       //                          FeeDueFeeTypeMapsID = mf.FeeDueFeeTypeMapsID,
                                       //                          MonthID = mf.MonthID != 0 ? int.Parse(Convert.ToString(mf.MonthID)) : 0,
                                       //                          Amount = mf.Amount.HasValue ? mf.Amount : (decimal?)null,
                                       //                          FeeCollectionStatus = mf.Status,
                                       //                          CreditNoteAmount = fcollm.CreditNoteAmount,
                                       //                          RefundAmount = fcollm.Balance,

                                       //                      }).ToList()
                                   }).AsNoTracking().ToList();
            }

            return feeCollectedDTO;
        }

        public List<FeeDuePaymentDTO> FillFeePaymentDetails(long loginId)
        {
            //List<FeeDuePaymentDTO> feeDueDTO = new List<FeeDuePaymentDTO>();
            List<FeeDuePaymentDTO> feeDueDTOList = new List<FeeDuePaymentDTO>();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var feeDueDTO = (from stFee in dbContext.StudentFeeDues
                                     //join FeeTypeMap in dbContext.FeeDueFeeTypeMaps on stFee.StudentFeeDueIID equals FeeTypeMap.StudentFeeDueID
                                     //join FeeMonthlySplit in dbContext.FeeDueMonthlySplits on FeeTypeMap.FeeDueFeeTypeMapsIID equals FeeMonthlySplit.FeeDueFeeTypeMapsID into tmpMonthlySplit
                                     //from FeeMonthlySplit in tmpMonthlySplit.DefaultIfEmpty()
                                 join s in dbContext.Students on stFee.StudentId equals s.StudentIID
                                 join p in dbContext.Parents on s.ParentID equals p.ParentIID
                                 where (p.LoginID == loginId && stFee.CollectionStatus == false && stFee.IsCancelled != true)
                                 orderby stFee.StudentId ascending
                                 select stFee
                                 ).AsNoTracking().ToList();

                feeDueDTOList = feeDueDTO.Select(stFeeGroup => new FeeDuePaymentDTO()
                {
                    StudentID = stFeeGroup.StudentId.Value,
                    StudentName = stFeeGroup.Student.FirstName + " " + stFeeGroup.Student.MiddleName + " " + stFeeGroup.Student.LastName,
                    FeeDueFeeTypeMap = (from ft in dbContext.FeeDueFeeTypeMaps
                                        where ft.Status == false && ft.FeeMasterID != null && ft.StudentFeeDue.StudentId == stFeeGroup.StudentId.Value
                                        // group ft by new { ft.Amount, ft.TaxAmount, ft.FeePeriodID, ft.TaxPercentage, 
                                        //ft.FeeDueFeeTypeMapsIID, ft.ClassFeeMasterID, ft.FeeMasterClassMapID, ft.FeeMaster,
                                        // ft.FeePeriod, ft.FeeDueMonthlySplits } into ftGroup
                                        select new FeeDueFeeTypeMapDTO()
                                        {
                                            Amount = ft.Amount,
                                            InvoiceNo = ft.StudentFeeDue.InvoiceNo,
                                            InvoiceDate = ft.StudentFeeDue.InvoiceDate,
                                            FeePeriodID = ft.FeePeriodID,
                                            StudentFeeDueID = ft.StudentFeeDueID,
                                            FeeStructureFeeMapID = ft.FeeStructureFeeMapID,
                                            FeeCycleID = ft.FeeMaster.FeeCycleID,
                                            FeeDueFeeTypeMapsIID = ft.FeeDueFeeTypeMapsIID,
                                            IsExternal = ft.FeeMaster.IsExternal == true ? true : false,
                                            IsFeePeriodDisabled = ft.FeeMaster.FeeCycleID.HasValue ? ft.FeeMaster.FeeCycleID.Value != 3 : true,
                                            FeeMaster = /*ft.FeeMaster != null ? */new KeyValueDTO()
                                            {
                                                Key = ft.FeeMaster.FeeMasterID.ToString(),
                                                Value = ft.FeeMaster.Description
                                            },// : new KeyValueDTO(),

                                            FeePeriod = /*ft.FeePeriodID.HasValue ? */new KeyValueDTO()
                                            {
                                                Key = ft.FeePeriodID.Value.ToString(),
                                                Value = ft.FeePeriod.Description //+ " ( " + ft.FeePeriod.PeriodFrom.ToString("dd/mmm/yyy") + '-'
                                                                                 // + ft.FeePeriod.PeriodTo.ToString("dd/mmm/yyy") + " ) "
                                            }, //: new KeyValueDTO(),
                                            FeeDueMonthlySplit = (from mf in ft.FeeDueMonthlySplits//.AsEnumerable()
                                                                  join crno in dbContext.SchoolCreditNotes on
                                                                  stFeeGroup.StudentId equals crno.StudentID
                                                                  into tmpcrNot
                                                                  from crno in tmpcrNot.Where(cr => cr.StudentID == stFeeGroup.StudentId
                                                                  && cr.CreditNoteDate.Value.Month == mf.MonthID
                                                                  && cr.CreditNoteDate.Value.Year == mf.Year && cr.Status == false).DefaultIfEmpty()
                                                                  join cnf in dbContext.CreditNoteFeeTypeMaps on
                                                                  (crno == null ? 0 : crno.SchoolCreditNoteIID) equals cnf.SchoolCreditNoteID
                                                                  into tmpCreditNoteFeeType
                                                                  from cnf in tmpCreditNoteFeeType.Where(mf => ft.FeeMaster.FeeMasterID == ft.FeeMasterID && ft.Status == false).DefaultIfEmpty()
                                                                  where mf.Status == false
                                                                  select new FeeDueMonthlySplitDTO()
                                                                  {
                                                                      FeeStructureMontlySplitMapID = mf.FeeStructureMontlySplitMapID.HasValue ? mf.FeeStructureMontlySplitMapID.Value : 0,
                                                                      FeeDueMonthlySplitIID = mf.FeeDueMonthlySplitIID,
                                                                      FeeDueFeeTypeMapsID = mf.FeeDueFeeTypeMapsID,
                                                                      MonthID = mf.MonthID != 0 ? mf.MonthID : 0,
                                                                      Amount = mf.Amount.HasValue ? mf.Amount : (decimal?)null,
                                                                      CreditNoteAmount = (cnf != null ? cnf.Amount : 0),
                                                                      Balance = mf.Amount.Value - (cnf != null ? cnf.Amount : 0),
                                                                      TaxAmount = mf.TaxAmount.HasValue ? mf.TaxAmount : (decimal?)null,
                                                                      TaxPercentage = mf.TaxPercentage.HasValue ? mf.TaxPercentage : (decimal?)null
                                                                  }).ToList(),
                                        }).ToList(),
                }).ToList();

          
            }

            return feeDueDTOList;
        }

        public List<StudentFeeDueDTO> FillFineDue(int classId, long studentId)
        {
            List<StudentFeeDueDTO> feeDueDTO = new List<StudentFeeDueDTO>();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                feeDueDTO = (from stFee in dbContext.StudentFeeDues
                             join FeeTypeMap in dbContext.FeeDueFeeTypeMaps on stFee.StudentFeeDueIID equals FeeTypeMap.StudentFeeDueID
                             join fine in dbContext.FineMasterStudentMaps on stFee.StudentId equals fine.StudentId
                             where (stFee.StudentId == studentId && stFee.CollectionStatus == false && stFee.IsCancelled != true)
                             orderby stFee.InvoiceDate ascending
                             group stFee by new { stFee.StudentFeeDueIID, stFee.StudentId, stFee.ClassId, stFee.InvoiceNo, stFee.InvoiceDate, stFee.FeeDueFeeTypeMaps } into stFeeGroup
                             select new StudentFeeDueDTO()
                             {
                                 StudentFeeDueIID = stFeeGroup.Key.StudentFeeDueIID,
                                 StudentId = stFeeGroup.Key.StudentId,
                                 ClassId = stFeeGroup.Key.ClassId,
                                 InvoiceNo = stFeeGroup.Key.InvoiceNo,
                                 InvoiceDate = stFeeGroup.Key.InvoiceDate,
                                 FeeFineMap = (from ft in stFeeGroup.Key.FeeDueFeeTypeMaps
                                               where ft.Status == false && ft.FineMasterID != null

                                               select new FeeDueFeeFineMapDTO()
                                               {
                                                   InvoiceNo = stFeeGroup.Key.InvoiceNo,
                                                   InvoiceDate = stFeeGroup.Key.InvoiceDate,
                                                   FineMasterID = ft.FineMasterID,
                                                   FineName = ft.FineMaster.FineName,
                                                   FineMasterStudentMapID = ft.FineMasterStudentMapID,
                                                   Amount = ft.Amount,
                                                   FineMapDate = ft.FineMasterStudentMap.FineMapDate
                                               }).ToList(),

                             }).AsNoTracking().ToList();
            }
            return feeDueDTO;
        }

        public decimal PendingFeesByParent(long parentID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var studentDetails = dbContext.Students.Where(s => s.ParentID == parentID && s.IsActive == true).AsNoTracking().ToList();

                decimal studentsDue = 0;

                if (studentDetails != null && studentDetails.Count > 0)
                {
                    studentDetails.ForEach(s =>
                    {
                        studentsDue += PendingFeesByStudent(s.StudentIID);
                    });
                }

                return studentsDue;
            }
        }

        public decimal PendingFeesByStudent(long studentID)
        {
            List<StudentFeeDueDTO> studentFeeDueDTOList = new List<StudentFeeDueDTO>();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var dFnd = dbContext.StudentFeeDues
                            .Where(n => n.CollectionStatus != true && n.StudentId == studentID && n.IsCancelled != true)
                            .Include(w => w.FeeDueFeeTypeMaps)
                            .AsNoTracking().ToList();

                if (dFnd.Any())
                {
                    dFnd.All(w => { studentFeeDueDTOList.Add(GetStudentFeeDue(dbContext, w)); return true; });
                }

                return studentFeeDueDTOList.Count() > 0 ? studentFeeDueDTOList.Sum(x => x.FeeDueFeeTypeMap.Sum(t => t.Amount - (t.CollectedAmount != null ? t.CollectedAmount : 0) - (t.CreditNoteAmount != null ? t.CreditNoteAmount : 0))).Value : 0;
            }
        }

        public List<StudentFeeDueDTO> FillPendingFeesForBank(int classId, long studentId)
        {

            return GetStudentFeeDue(studentId, classId);
        }

        public List<StudentFeeDueDTO> FillPendingFees(int classId, long studentId)
        {

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var feeDueDTO = (from stFee in dbContext.StudentFeeDues
                                 join FeeTypeMap in dbContext.FeeDueFeeTypeMaps on stFee.StudentFeeDueIID
                                 equals FeeTypeMap.StudentFeeDueID
                                 where (stFee.StudentId == studentId
                                 && stFee.CollectionStatus == false && stFee.IsCancelled != true)
                                 orderby (FeeTypeMap.FeePeriodID ?? 0) ascending // stFee.InvoiceDate ascending
                                 select new StudentFeeDueDTO()
                                 {
                                     StudentFeeDueIID = stFee.StudentFeeDueIID,
                                     InvoiceAmount = stFee.FeeDueFeeTypeMaps.Select(xt => (xt.Amount.Value)).Sum(),
                                     CollectedAmount = stFee.FeeDueFeeTypeMaps.Select(xt => ((xt.CollectedAmount ?? 0))).Sum(),
                                     InvoiceNo = stFee.InvoiceNo,
                                     InvoiceDate = stFee.InvoiceDate,
                                     FeePeriodId = FeeTypeMap.FeePeriodID,
                                     FeeMasterID = FeeTypeMap.FeeMasterID,
                                     IsExternal = stFee.FeeDueFeeTypeMaps.Select(fm => fm.FeeMaster.IsExternal).FirstOrDefault(),
                                 }).AsNoTracking();

                var detailDTO = feeDueDTO.ToList().Select(stFee => new StudentFeeDueDTO()
                {
                    StudentFeeDueIID = stFee.StudentFeeDueIID,
                    InvoiceAmount = stFee.InvoiceAmount,
                    InvoiceNo = stFee.InvoiceNo,
                    InvoiceDate = stFee.InvoiceDate,
                    FeePeriodId = stFee.FeePeriodId,
                    FeeMasterID = stFee.FeeMasterID,
                    IsExternal = stFee.IsExternal,
                    CollectedAmount = GetTotalCollectedAmount(dbContext, stFee.FeeMasterID, stFee.FeePeriodId, stFee.StudentFeeDueIID),
                    CreditNoteAmount = GetTotalCreditNote(dbContext, studentId, stFee.FeeMasterID, stFee.FeePeriodId, stFee.StudentFeeDueIID),
                    Remarks = GetFeeDueRemarks(dbContext, stFee.StudentFeeDueIID)
                });

                return detailDTO.ToList();
            }

        }
        public decimal? GetTotalCollectedAmount(dbEduegateSchoolContext _sContext, int? feeMasterID, int? feePeriodID, long studentFeeDueIID)
        {

            MutualRepository mutualRepository = new MutualRepository();
            var draftFeeCollectionStatus = mutualRepository.GetSettingData("FEECOLLECTIONSTATUSID_DRAFT").SettingValue ?? "1";
            int draftStatusID = int.Parse(draftFeeCollectionStatus);

            var amount = (from n in _sContext.FeeCollectionFeeTypeMaps
                          join m in _sContext.FeeCollections on n.FeeCollectionID equals m.FeeCollectionIID
                          join fd in _sContext.FeeDueFeeTypeMaps on n.FeeDueFeeTypeMapsID equals fd.FeeDueFeeTypeMapsIID
                          where fd.StudentFeeDueID == studentFeeDueIID && (m.FeeCollectionStatusID ?? 2) != draftStatusID
                          && m.IsCancelled!=true
                          && n.FeeMasterID == feeMasterID && (feePeriodID == null || n.FeePeriodID == feePeriodID)
                          select n.Amount).Sum();

            return amount;
        }

        public decimal? GetTotalCreditNote(dbEduegateSchoolContext _sContext, long? studentID, int? feeMasterID, int? feePeriodID, long studentFeeDueIID)
        {
            List<CreditNoteFeeTypeMap> _sRetData = new List<CreditNoteFeeTypeMap>();
            var crAmount = (from n in _sContext.SchoolCreditNotes
                            join m in _sContext.CreditNoteFeeTypeMaps on n.SchoolCreditNoteIID equals m.SchoolCreditNoteID
                            join fd in _sContext.FeeDueFeeTypeMaps on m.FeeDueFeeTypeMapsID equals fd.FeeDueFeeTypeMapsIID
                            where n.StudentID == studentID && fd.StudentFeeDueID == studentFeeDueIID
                            && n.IsCancelled != true
                            && m.FeeMasterID == feeMasterID && (feePeriodID == null || m.PeriodID == feePeriodID)
                            select m.Amount).Sum();

            return crAmount;
        }
        public List<KeyValueDTO> GetInvoiceForCreditNote(int classId, long studentId, int? feeMasterID, int? feePeriodID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var feeDueDTO = (from stFee in dbContext.StudentFeeDues
                                 join FeeTypeMap in dbContext.FeeDueFeeTypeMaps on stFee.StudentFeeDueIID
                                 equals FeeTypeMap.StudentFeeDueID
                                 where (stFee.StudentId == studentId && stFee.IsCancelled != true && stFee.CollectionStatus != true
                                 && ((feeMasterID == 0 || feeMasterID == null) || FeeTypeMap.FeeMasterID == feeMasterID)
                                    && ((feePeriodID == 0 || feePeriodID == null) || FeeTypeMap.FeePeriodID == feePeriodID))
                                 orderby (FeeTypeMap.FeePeriodID ?? 0) ascending
                                 select new KeyValueDTO()
                                 {
                                     Key = stFee.StudentFeeDueIID.ToString(),
                                     Value = stFee.InvoiceNo
                                 }).AsNoTracking();

                return feeDueDTO.ToList();
            }

        }
        //public static decimal? GetTotalDueAmount(long? studentID)
        //{
        //    decimal? balance = 0, collectedAmount = 0;
        //    decimal? creditnoteAmount = 0;

        //    using (var dbContext = new dbEduegateSchoolContext())
        //    {
        //        feeDueDTO = (from stFee in dbContext.StudentFeeDues
        //                     join FeeTypeMap in dbContext.FeeDueFeeTypeMaps on stFee.StudentFeeDueIID equals FeeTypeMap.StudentFeeDueID
                            
        //                     where (stFee.StudentId == studentID && stFee.CollectionStatus == false && stFee.IsCancelled != true)
        //                     orderby stFee.InvoiceDate ascending
        //                     group stFee by new { stFee.StudentFeeDueIID, stFee.StudentId, stFee.ClassId, stFee.InvoiceNo, stFee.InvoiceDate, stFee.FeeDueFeeTypeMaps } into stFeeGroup
        //                     select new StudentFeeDueDTO()
        //                     {
        //                         StudentFeeDueIID = stFeeGroup.Key.StudentFeeDueIID,
        //                         StudentId = stFeeGroup.Key.StudentId,
        //                         ClassId = stFeeGroup.Key.ClassId,
        //                         InvoiceNo = stFeeGroup.Key.InvoiceNo,
        //                         InvoiceDate = stFeeGroup.Key.InvoiceDate,
        //                         FeeFineMap = (from ft in stFeeGroup.Key.FeeDueFeeTypeMaps
        //                                       where ft.Status == false && ft.FineMasterID != null

        //                                       select new FeeDueFeeFineMapDTO()
        //                                       {
        //                                           InvoiceNo = stFeeGroup.Key.InvoiceNo,
        //                                           InvoiceDate = stFeeGroup.Key.InvoiceDate,
        //                                           FineMasterID = ft.FineMasterID,
        //                                           FineName = ft.FineMaster.FineName,
        //                                           FineMasterStudentMapID = ft.FineMasterStudentMapID,
        //                                           Amount = ft.Amount,
        //                                           FineMapDate = ft.FineMasterStudentMap.FineMapDate
        //                                       }).ToList(),

        //                     }).AsNoTracking().ToList();
        //    }
        //    return feeDueDTO;
        

        //    if (feeDueMonthlySplitIID.HasValue)
        //    {
        //        var feeCollectionMonthlySplits = dbContext.FeeCollectionMonthlySplits.Where(x => x.FeeDueMonthlySplitID == feeDueMonthlySplitIID
        //                                         && x.FeeCollectionFeeTypeMap.FeeCollection.FeeCollectionStatusID != draftStatusID
        //                                         && x.FeeCollectionFeeTypeMap.FeeCollection.IsCancelled != true)
        //                                        .Include(x => x.FeeCollectionFeeTypeMap)
        //                                        .ThenInclude(x => x.FeeCollection)
        //                                        .AsNoTracking().ToList();

        //        collectedAmount = feeCollectionMonthlySplits.Sum(x => x.Amount) ?? 0;
        //        var feecreditnoteMonthlySplits = dbContext.CreditNoteFeeTypeMaps.Where(x => x.FeeDueMonthlySplitID == feeDueMonthlySplitIID && x.SchoolCreditNote.IsCancelled != true).Include(x => x.SchoolCreditNote).AsNoTracking().ToList();
        //        creditnoteAmount = feecreditnoteMonthlySplits.Sum(x => x.Amount) ?? 0;
        //    }
        //    else
        //    {
        //        var feeCollectionFeeDueMaps = dbContext.FeeCollectionFeeTypeMaps.Where(x => x.FeeDueFeeTypeMapsID == feeDueFeeTypeMapsID && x.FeeCollection.FeeCollectionStatusID != draftStatusID && x.FeeCollection.IsCancelled != true)
        //                                     .Include(x => x.FeeCollection).AsNoTracking().ToList();

        //        collectedAmount = feeCollectionFeeDueMaps.Sum(x => x.Amount) ?? 0;
        //        var feecreditnoteMonthlySplits = dbContext.CreditNoteFeeTypeMaps.Where(x => x.FeeDueFeeTypeMapsID == feeDueFeeTypeMapsID && x.SchoolCreditNote.IsCancelled != true).Include(x => x.SchoolCreditNote).AsNoTracking().ToList();
        //        creditnoteAmount = feecreditnoteMonthlySplits.Sum(x => x.Amount) ?? 0;
        //    }
        //    balance = dueAmount - collectedAmount - creditnoteAmount;
        //    return balance;

        //}

        public static decimal? GetFeeDueMonthlyCollectedAmount(dbEduegateSchoolContext dbContext, long? feeDueFeeTypeMapsID, long? feeDueMonthlySplitIID, int draftStatusID, decimal? dueAmount)
        {
            decimal? balance = 0, collectedAmount = 0;
            decimal? creditnoteAmount = 0;
            if (feeDueMonthlySplitIID.HasValue)
            {
                var feeCollectionMonthlySplits = dbContext.FeeCollectionMonthlySplits.Where(x => x.FeeDueMonthlySplitID == feeDueMonthlySplitIID
                                                 && x.FeeCollectionFeeTypeMap.FeeCollection.FeeCollectionStatusID != draftStatusID
                                                 && x.FeeCollectionFeeTypeMap.FeeCollection.IsCancelled!=true)
                                                .Include(x => x.FeeCollectionFeeTypeMap)
                                                .ThenInclude(x => x.FeeCollection)
                                                .AsNoTracking().ToList();

                collectedAmount = feeCollectionMonthlySplits.Sum(x => x.Amount) ?? 0;
                var feecreditnoteMonthlySplits = dbContext.CreditNoteFeeTypeMaps.Where(x => x.FeeDueMonthlySplitID == feeDueMonthlySplitIID && x.SchoolCreditNote.IsCancelled!=true).Include(x=>x.SchoolCreditNote).AsNoTracking().ToList();
                creditnoteAmount = feecreditnoteMonthlySplits.Sum(x => x.Amount) ?? 0;
            }
            else
            {
                var feeCollectionFeeDueMaps = dbContext.FeeCollectionFeeTypeMaps.Where(x => x.FeeDueFeeTypeMapsID == feeDueFeeTypeMapsID && x.FeeCollection.FeeCollectionStatusID != draftStatusID && x.FeeCollection.IsCancelled != true)
                                             .Include(x => x.FeeCollection).AsNoTracking().ToList();

                collectedAmount = feeCollectionFeeDueMaps.Sum(x => x.Amount) ?? 0;
                var feecreditnoteMonthlySplits = dbContext.CreditNoteFeeTypeMaps.Where(x => x.FeeDueFeeTypeMapsID == feeDueFeeTypeMapsID && x.SchoolCreditNote.IsCancelled != true).Include(x => x.SchoolCreditNote).AsNoTracking().ToList();
                creditnoteAmount = feecreditnoteMonthlySplits.Sum(x => x.Amount) ?? 0;
            }
            balance = dueAmount - collectedAmount - creditnoteAmount;
            return balance;

        }
        public  List<FeeDueMonthlySplitDTO> GetFeeDueMonthlyDetails(long studentFeeDueID, int? feeMasterID, int? feePeriodID)
        {
            var lstMonthlySplit = new List<FeeDueMonthlySplitDTO>();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                MutualRepository mutualRepository = new MutualRepository();
                var draftFeeCollectionStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("FEECOLLECTIONSTATUSID_DRAFT", "1");
                int draftStatusID = int.Parse(draftFeeCollectionStatus);
                lstMonthlySplit = (from n in dbContext.FeeDueMonthlySplits
                                   join FeeTypeMap in dbContext.FeeDueFeeTypeMaps on n.FeeDueFeeTypeMapsID
                                   equals FeeTypeMap.FeeDueFeeTypeMapsIID
                                   where FeeTypeMap.StudentFeeDueID == studentFeeDueID
                                   && FeeTypeMap.StudentFeeDue.IsCancelled!=true
                                   && n.Status == false
                                   && FeeTypeMap.FeeMasterID == feeMasterID && (feePeriodID == null || FeeTypeMap.FeePeriodID == feePeriodID)
                                   orderby n.Year, n.MonthID
                                   select new FeeDueMonthlySplitDTO
                                   {
                                       FeeStructureMontlySplitMapID = n.FeeStructureMontlySplitMapID ?? 0,
                                       FeeDueMonthlySplitIID = n.FeeDueMonthlySplitIID,
                                       FeeDueFeeTypeMapsID = n.FeeDueFeeTypeMapsID,
                                       MonthID = n.MonthID,
                                       //TODO: Refactoring
                                       Amount = GetFeeDueMonthlyCollectedAmount(dbContext, n.FeeDueFeeTypeMapsID, n.FeeDueMonthlySplitIID, draftStatusID, n.Amount),
                                       Year = n.Year ?? 0,
                                       CreditNoteAmount = 0,
                                       Balance = n.Amount ?? 0,
                                       CreditNoteFeeTypeMapID = 0,
                                       TaxAmount = n.TaxAmount ?? 0,
                                       TaxPercentage = n.TaxPercentage ?? 0,
                                       FeeMasterID = FeeTypeMap.FeeMasterID ?? 0,
                                       FeePeriodID = n.FeePeriodID ?? 0,
                                       StudentId = FeeTypeMap.StudentFeeDue.StudentId ?? 0,
                                   }).AsNoTracking().ToList();

                var studentId = lstMonthlySplit.FirstOrDefault()?.StudentId;              

                if (lstMonthlySplit.Count() == 0)
                {
                    lstMonthlySplit = (from n in dbContext.FeeDueFeeTypeMaps
                                       where n.StudentFeeDueID == studentFeeDueID
                                       select new FeeDueMonthlySplitDTO
                                       {
                                           FeeStructureMontlySplitMapID = 0,
                                           FeeDueMonthlySplitIID = 0,
                                           FeeDueFeeTypeMapsID = n.FeeDueFeeTypeMapsIID,
                                           MonthID = 0,
                                           Amount = GetFeeDueMonthlyCollectedAmount(dbContext, n.FeeDueFeeTypeMapsIID, null, draftStatusID, n.Amount),
                                           Year = 0,
                                           CreditNoteAmount = 0,
                                           Balance = n.Amount ?? 0,
                                           CreditNoteFeeTypeMapID = 0
                                       }).ToList();
                }

                return lstMonthlySplit;
            }
        }


        #region Code Created By Sudish => 2021-056-01 => Get Remarks from Fee Due Generated
        public string GetFeeDueRemarks(dbEduegateSchoolContext _sContext, long StudentFeeDueIID)
        {
            string _sRetData = string.Empty; ;
            /*Regular*/
            var dFnd1 = (from n in _sContext.FeeDueFeeTypeMaps
                         join m in _sContext.FeeMasters on n.FeeMasterID equals m.FeeMasterID
                         join k in _sContext.FeePeriods on n.FeePeriodID equals k.FeePeriodID
                         where n.StudentFeeDueID == StudentFeeDueIID
                         select new { Data = string.Concat(m.Description, "=>", k.Description) }).Distinct().AsNoTracking();

            if (dFnd1.Any())
            {
                return String.Join(",", dFnd1.Select(w => w.Data.ToString()));
            }
            /*One Time*/
            var dFnd2 = (from n in _sContext.FeeDueFeeTypeMaps
                         join m in _sContext.FeeMasters on n.FeeMasterID equals m.FeeMasterID
                         where (n.FeePeriodID ?? 0) == 0 && n.StudentFeeDueID == StudentFeeDueIID
                         select new { Data = string.Concat(m.Description) }).Distinct().AsNoTracking();

            if (dFnd2.Any())
            {
                return String.Join(",", dFnd2.Select(w => w.Data.ToString()));

            }
            /*Fine*/
            var dFnd3 = (from n in _sContext.FeeDueFeeTypeMaps
                         join m in _sContext.FineMasters on n.FineMasterID equals m.FineMasterID
                         where (n.FeePeriodID ?? 0) == 0 && n.StudentFeeDueID == StudentFeeDueIID
                         select new { Data = string.Concat(m.FineName) }).Distinct().AsNoTracking();

            if (dFnd3.Any())
            {
                return String.Join(",", dFnd3.Select(w => w.Data.ToString()));
            }
            return _sRetData;
        }
        #endregion

        public List<FeeCollectionPreviousFeesDTO> GetIssuedCreditNotesForCollectedFee(long studentId)
        {
            List<FeeCollectionPreviousFeesDTO> feeDueDTO = new List<FeeCollectionPreviousFeesDTO>();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                feeDueDTO = (from stFee in dbContext.StudentFeeDues
                             join FeeTypeMap in dbContext.FeeDueFeeTypeMaps on stFee.StudentFeeDueIID equals FeeTypeMap.StudentFeeDueID
                             where (stFee.StudentId == studentId && stFee.CollectionStatus == true && stFee.IsCancelled != true)
                             orderby stFee.InvoiceDate ascending
                             group stFee by new { stFee.StudentFeeDueIID, stFee.InvoiceNo, stFee.InvoiceDate, FeeTypeMap.Amount, FeeTypeMap.CollectedAmount, FeeTypeMap.TaxAmount, FeeTypeMap.FeePeriodID, FeeTypeMap.TaxPercentage, FeeTypeMap.StudentFeeDueID, FeeTypeMap.FeeDueFeeTypeMapsIID, FeeTypeMap.FeeStructureFeeMapID, FeeTypeMap.FeeMaster, FeeTypeMap.FeePeriod, FeeTypeMap.FeeDueMonthlySplits } into stFeeGroup
                             select new FeeCollectionPreviousFeesDTO()
                             {
                                 InvoiceNo = stFeeGroup.Key.InvoiceNo,
                                 InvoiceDate = stFeeGroup.Key.InvoiceDate,
                                 Amount = stFeeGroup.Key.Amount - (stFeeGroup.Key.CollectedAmount.HasValue ? stFeeGroup.Key.CollectedAmount.Value : 0),
                                 FeeMasterID = stFeeGroup.Key.FeeMaster != null ? stFeeGroup.Key.FeeMaster.FeeMasterID : (int?)null,
                                 FeePeriodID = stFeeGroup.Key.FeePeriodID,
                                 StudentFeeDueID = stFeeGroup.Key.StudentFeeDueID,
                                 FeeStructureFeeMapID = stFeeGroup.Key.FeeStructureFeeMapID,
                                 FeeDueFeeTypeMapsID = stFeeGroup.Key.FeeDueFeeTypeMapsIID,
                                 FeeMaster = stFeeGroup.Key.FeeMaster != null ? stFeeGroup.Key.FeeMaster.Description : null,
                                 FeePeriod = !stFeeGroup.Key.FeePeriodID.HasValue ? null : stFeeGroup.Key.FeePeriod.Description + " ( " + stFeeGroup.Key.FeePeriod.PeriodFrom.ToString("dd/MMM/yyy") + '-'
                                                                     + stFeeGroup.Key.FeePeriod.PeriodTo.ToString("dd/MMM/yyy") + " ) ",
                                 CreditNoteAmount = 200,
                                 Balance = (stFeeGroup.Key.Amount - (stFeeGroup.Key.CollectedAmount.HasValue ? stFeeGroup.Key.CollectedAmount.Value : 0)) - 200,
                                 MontlySplitMaps = (from mf in stFeeGroup.Key.FeeDueMonthlySplits//.AsEnumerable()
                                                    where mf.Status == false
                                                    select new FeeCollectionMonthlySplitDTO()
                                                    {

                                                        FeeDueMonthlySplitID = mf.FeeDueMonthlySplitIID,
                                                        FeeDueFeeTypeMapsID = mf.FeeDueFeeTypeMapsID,
                                                        MonthID = mf.MonthID != 0 ? int.Parse(Convert.ToString(mf.MonthID)) : 0,
                                                        Amount = mf.Amount.HasValue ? mf.Amount : (decimal?)null,
                                                        CreditNoteAmount = 200,
                                                        Balance = mf.Amount.Value - 200,
                                                        Year = mf.Year.HasValue ? mf.Year.Value : 0

                                                    }).ToList(),
                             }).AsNoTracking().ToList();
            }

            return feeDueDTO;
        }


        #region Code Created By Sudish => Get Fee Due By Invoive for Collection # Modified By Vineetha.K.R

        private List<StudentFeeDueDTO> GetStudentFeeDue(long StudentID, int ClassID)
        {
            List<StudentFeeDueDTO> _sRetData = new List<StudentFeeDueDTO>();
            using (var _sContext = new dbEduegateSchoolContext())
            {
                var dFnd = (from stFee in _sContext.StudentFeeDues
                            join FeeTypeMap in _sContext.FeeDueFeeTypeMaps on stFee.StudentFeeDueIID
                            equals FeeTypeMap.StudentFeeDueID
                            where (stFee.StudentId == StudentID
                            && stFee.CollectionStatus == false && FeeTypeMap.Status == false && stFee.IsCancelled != true)
                            select stFee)
                            .Include(w => w.FeeDueFeeTypeMaps)
                            .AsNoTracking().ToList();

                if (dFnd.Any())
                {
                    dFnd.All(w => { _sRetData.Add(GetStudentFeeDue(_sContext, w)); return true; });
                }
                else
                {
                    var rtnData = new StudentFeeDueDTO()
                    {
                        ActCode = "3",
                        ActDescription = "Fee Not Generated",
                        ResposeCode = 3,
                        ResposeDescription = "Fee Not Generated"

                    };
                    _sRetData.Add(rtnData);
                }
            }
            return _sRetData;
        }

        private List<StudentFeeDueDTO> GetStudentFeeDue(long StudentFeeDueID)
        {
            List<StudentFeeDueDTO> _sRetData = new List<StudentFeeDueDTO>();
            using (var _sContext = new dbEduegateSchoolContext())
            {
                var dFnd = _sContext.StudentFeeDues
                            .Where(n => n.StudentFeeDueIID == StudentFeeDueID && n.CollectionStatus == false && n.IsCancelled != true)
                            .Include(w => w.FeeDueFeeTypeMaps)
                            .AsNoTracking().ToList();

                if (dFnd.Any())
                {
                    dFnd.All(w => { _sRetData.Add(GetStudentFeeDue(_sContext, w)); return true; });
                }
            }
            return _sRetData;
        }

        private StudentFeeDueDTO GetStudentFeeDue(dbEduegateSchoolContext _sContext, StudentFeeDue _sFeeDue)
        {
            StudentFeeDueDTO _sRetData = new StudentFeeDueDTO()
            {
                AcadamicYearID = _sFeeDue.AcadamicYearID,
                AcademicYear = null,
                Class = null,
                ClassId = _sFeeDue.ClassId,
                CollectionStatus = _sFeeDue.CollectionStatus,
                CollectionStatusEdit = false,
                CreatedBy = _sFeeDue.CreatedBy,
                CreatedDate = _sFeeDue.CreatedDate,
                DueDate = _sFeeDue.DueDate,
                FeeDueFeeTypeMap = GetFeeDueTypeMap(_sContext, _sFeeDue),
                FeeFineMap = GetFineMap(_sContext, _sFeeDue),
                InvoiceDate = _sFeeDue.InvoiceDate,
                InvoiceNo = _sFeeDue.InvoiceNo,
                StudentFeeDueIID = _sFeeDue.StudentFeeDueIID,
                StudentId = _sFeeDue.StudentId,
                Remarks = GetFeeDueRemarks(_sContext, _sFeeDue.StudentFeeDueIID)

            };

            _sRetData.InvoiceAmount = _sRetData.FeeDueFeeTypeMap.Sum(w => (w.Amount ?? 0) - (w.CollectedAmount ?? 0) - (w.CreditNoteAmount ?? 0));

            return _sRetData;
        }
        private List<FeeDueFeeTypeMapDTO> GetFeeDueTypeMap(dbEduegateSchoolContext _sContext, StudentFeeDue _sFeeDue)
        {
            List<FeeDueFeeTypeMapDTO> _sRetData = new List<FeeDueFeeTypeMapDTO>();
            _sFeeDue.FeeDueFeeTypeMaps.ToList().All(w => { _sRetData.Add(GetFeeDueTypeMap(_sContext, _sFeeDue, w)); return true; });
            return _sRetData;
        }


        private List<FeeDueMonthlySplitDTO> GetFeeDueMonthlySplit(dbEduegateSchoolContext _sContext, FeeDueFeeTypeMap _sFeeDueMap)
        {
            List<FeeDueMonthlySplitDTO> _sRetData = new List<FeeDueMonthlySplitDTO>();
            if ((_sFeeDueMap.FeePeriodID ?? 0) == 0)
                return _sRetData;
            var dFnd = from n in _sContext.FeeDueMonthlySplits.Where(w => w.FeeDueFeeTypeMapsID == _sFeeDueMap.FeeDueFeeTypeMapsIID).AsNoTracking()
                       orderby n.Year, n.MonthID
                       select new FeeDueMonthlySplitDTO
                       {
                           FeeStructureMontlySplitMapID = n.FeeStructureMontlySplitMapID ?? 0,
                           FeeDueMonthlySplitIID = n.FeeDueMonthlySplitIID,
                           FeeDueMonthlySplitID = n.FeeDueMonthlySplitIID,
                           FeeDueFeeTypeMapsID = n.FeeDueFeeTypeMapsID,
                           MonthID = n.MonthID,
                           Amount = n.Amount ?? 0,
                           Year = n.Year ?? 0,
                           CreditNoteAmount = 0,
                           Balance = n.Amount ?? 0,
                           CreditNoteFeeTypeMapID = 0,
                           TaxAmount = n.TaxAmount ?? 0,
                           TaxPercentage = n.TaxPercentage ?? 0
                       };
            if (dFnd.Any())
                _sRetData.AddRange(dFnd);
            return _sRetData;
        }
        //private List<FeeDueMonthlySplitDTO> GetFeeDueMonthlySplit(dbEduegateSchoolContext _sContext, FeeDueFeeTypeMap _sFeeDueMap, List<CreditNoteFeeTypeMap> _sCreditNotes)
        //{
        //    List<FeeDueMonthlySplitDTO> _sRetData = new List<FeeDueMonthlySplitDTO>();
        //    if ((_sFeeDueMap.FeePeriodID ?? 0) == 0)
        //        return _sRetData;
        //    var dFnd = from n in _sContext.FeeDueMonthlySplits.Where(w => w.FeeDueFeeTypeMapsID == _sFeeDueMap.FeeDueFeeTypeMapsIID)
        //               select new FeeDueMonthlySplitDTO
        //               {
        //                   FeeStructureMontlySplitMapID = n.FeeStructureMontlySplitMapID ?? 0,
        //                   FeeDueMonthlySplitIID = n.FeeDueMonthlySplitIID,
        //                   FeeDueFeeTypeMapsID = n.FeeDueFeeTypeMapsID,
        //                   MonthID = n.MonthID,
        //                   Amount = n.Amount ?? 0,
        //                   Year = n.Year ?? 0,
        //                   CreditNoteAmount = 0,
        //                   Balance = n.Amount ?? 0,
        //                   CreditNoteFeeTypeMapID = 0,
        //                   TaxAmount = n.TaxAmount ?? 0,
        //                   TaxPercentage = n.TaxPercentage ?? 0,

        //               };
        //    if (dFnd.Any())
        //    {
        //        _sRetData.AddRange(dFnd);
        //        if (_sCreditNotes.Any(w=> w.FeeMasterID==_sFeeDueMap.FeeMasterID))
        //        {
        //            _sRetData.All(w => {  return true; });
        //        }
        //    }
        //    return _sRetData;
        //}

        private FeeDueFeeTypeMapDTO GetFeeDueTypeMap(dbEduegateSchoolContext sContext, StudentFeeDue sFeeDue, FeeDueFeeTypeMap sFeeDueMap)
        {
            MutualRepository mutualRepository = new MutualRepository();

            var dFeeMaster = sContext.FeeMasters.Where(w => w.FeeMasterID == (sFeeDueMap.FeeMasterID ?? 0))
                .Include(i => i.FeeType).AsNoTracking().FirstOrDefault();

            var dFeePeriod = sContext.FeePeriods.Where(w => w.FeePeriodID == (sFeeDueMap.FeePeriodID ?? 0)).AsNoTracking().FirstOrDefault();

            var feeCycleID = dFeeMaster.FeeCycleID;
            var dCreditNotes = GetCreditNote(sContext, sFeeDue, sFeeDueMap);

            var dMontlySplit = feeCycleID != 1 ? new List<FeeDueMonthlySplitDTO>() : GetFeeDueMonthlySplit(sContext, sFeeDueMap);

            decimal sAmountDue = sFeeDueMap.Amount ?? 0, _sCreditNoteAmount = 0, _sAmountCollected = 0, sBalance = 0;
            var isRefundable = dFeeMaster?.FeeType?.IsRefundable ?? false;

            var draftFeeCollectionStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("FEECOLLECTIONSTATUSID_DRAFT", "1");
            int draftStatusID = int.Parse(draftFeeCollectionStatus);

            #region Partial Receipt => Checking and Allocations
            if (dMontlySplit.Any())
            {
                var dCollectionMonthly = (from n in sContext.FeeCollectionMonthlySplits.AsNoTracking()
                                          join m in sContext.FeeCollectionFeeTypeMaps on n.FeeCollectionFeeTypeMapId equals m.FeeCollectionFeeTypeMapsIID
                                          where m.FeeDueFeeTypeMapsID == sFeeDueMap.FeeDueFeeTypeMapsIID
                                          && (m.FeeCollection.FeeCollectionStatusID ?? 0) != draftStatusID
                                          && m.FeeCollection.IsCancelled != true
                                          group n by new { n.FeeDueMonthlySplitID, m.FeeDueFeeTypeMapsID } into grp
                                          select new
                                          {
                                              FeeDueMonthlySplitID = grp.Key.FeeDueMonthlySplitID,
                                              FeeDueMonthlySplitIID = grp.Key.FeeDueMonthlySplitID,
                                              FeeDueFeeTypeMapsID = grp.Key.FeeDueFeeTypeMapsID,
                                              CollectedAmount = grp.Sum(w => w.Amount),
                                              CreditNoteAmount = grp.Sum(w => w.CreditNoteAmount),
                                          }).AsNoTracking();

                if (dCollectionMonthly.Any())
                {
                    dMontlySplit.All(w =>
                    {
                        if (dCollectionMonthly.Any(x => x.FeeDueFeeTypeMapsID == w.FeeDueFeeTypeMapsID && x.FeeDueMonthlySplitID == w.FeeDueMonthlySplitIID))
                        {
                            w.CreditNoteAmount = dCollectionMonthly.Where(x => x.FeeDueFeeTypeMapsID == w.FeeDueFeeTypeMapsID && x.FeeDueMonthlySplitID == w.FeeDueMonthlySplitID).Sum(k => k.CreditNoteAmount) ?? 0;
                            w.CollectedAmount = dCollectionMonthly.Where(x => x.FeeDueFeeTypeMapsID == w.FeeDueFeeTypeMapsID && x.FeeDueMonthlySplitID == w.FeeDueMonthlySplitID).Sum(k => k.CollectedAmount) ?? 0;
                            //w.ReceivableAmount = isRefundable == false ? (w.CollectedAmount < (w.Amount ?? 0 - w.CreditNoteAmount) ?
                            //                  (w.Amount - w.CreditNoteAmount) - w.CollectedAmount : 0) : 0;
                            //w.RefundAmount = isRefundable == true ? w.CollectedAmount : (w.CollectedAmount > (w.Amount - w.CreditNoteAmount)) ?
                            //                                 w.CollectedAmount - (w.Amount - w.CreditNoteAmount) : 0;
                            w.PrvCollect = dCollectionMonthly.Where(x => x.FeeDueFeeTypeMapsID == w.FeeDueFeeTypeMapsID && x.FeeDueMonthlySplitID == w.FeeDueMonthlySplitID).Sum(k => k.CollectedAmount) ?? 0;
                        }
                        return true;
                    }
                    );

                    _sAmountCollected = dCollectionMonthly.Sum(w => w.CollectedAmount) ?? 0;
                }
                else
                {
                    var dCollection = from n in sContext.FeeCollectionFeeTypeMaps.AsNoTracking()
                                      where n.FeeDueFeeTypeMapsID == sFeeDueMap.FeeDueFeeTypeMapsIID
                                      && (n.FeeCollection.FeeCollectionStatusID ?? 0) != draftStatusID
                                      && n.FeeCollection.IsCancelled != true
                                      group n by n.FeeDueFeeTypeMapsID into grp
                                      select new { FeeDueFeeTypeMapsID = grp.Key.Value, CreditNoteAmount = grp.Sum(w => w.CreditNoteAmount), CollectedAmount = grp.Sum(w => w.Amount) };
                    if (dCollection.Any())
                        _sAmountCollected = dCollection.Sum(w => w.CollectedAmount ?? 0);

                    dMontlySplit.All(w =>
                    {

                        //w.ReceivableAmount = isRefundable == false ?
                        //                  (w.Amount - w.CreditNoteAmount ?? 0) - (w.CollectedAmount ?? 0) : 0;
                        w.RefundAmount = 0;
                        w.PrvCollect = 0;
                        w.CollectedAmount = 0;
                        return true;
                    }
                    );


                }

            }
            else
            {
                var dCollection = from n in sContext.FeeCollectionFeeTypeMaps.AsNoTracking()
                                  where n.FeeDueFeeTypeMapsID == sFeeDueMap.FeeDueFeeTypeMapsIID
                                  && (n.FeeCollection.FeeCollectionStatusID ?? 0) != draftStatusID
                                  && n.FeeCollection.IsCancelled != true
                                  group n by n.FeeDueFeeTypeMapsID into grp
                                  select new { FeeDueFeeTypeMapsID = grp.Key.Value, CollectedAmount = grp.Sum(w => w.Amount) };
                if (dCollection.Any())
                    _sAmountCollected = dCollection.Sum(w => w.CollectedAmount) ?? 0;
            }

            if (dCreditNotes.Any(w => w.FeeMasterID == sFeeDueMap.FeeMasterID))
            {
                if (dMontlySplit.Any())
                {
                    dMontlySplit.All(w => { SetCreditNoteMonthly(sContext, w, dCreditNotes.FindAll(x => x.FeeMasterID == sFeeDueMap.FeeMasterID)); return true; });
                    _sCreditNoteAmount = dMontlySplit.Any(w => (w.CreditNoteAmount ?? 0) != 0) ? dMontlySplit.Sum(w => (w.CreditNoteAmount ?? 0)) : 0;
                }
                else
                {
                    _sCreditNoteAmount = dCreditNotes.Sum(w => w.Amount ?? 0);
                }
            }
            long _sCreditNoteFeeTypeMapID = dCreditNotes.Any() ? dCreditNotes.FirstOrDefault().CreditNoteFeeTypeMapIID : 0;


            #endregion

            var _sRetData = new FeeDueFeeTypeMapDTO()
            {
                Amount = sAmountDue,
                CreditNoteAmount = _sCreditNoteAmount,
                Balance = sBalance,
                CreditNoteFeeTypeMapID = _sCreditNoteFeeTypeMapID,
                InvoiceDate = sFeeDue.InvoiceDate,
                InvoiceNo = sFeeDue.InvoiceNo,
                CollectedAmount = _sAmountCollected,
                CreatedBy = sFeeDue.CreatedBy,
                CreatedDate = sFeeDue.CreatedDate,
                FeeCycleID = 0,
                FeeDueFeeTypeMapsID = sFeeDueMap.FeeDueFeeTypeMapsIID,
                FeeDueFeeTypeMapsIID = sFeeDueMap.FeeDueFeeTypeMapsIID,
                FeeMaster = dFeeMaster != null ? new KeyValueDTO() { Key = dFeeMaster.FeeMasterID.ToString(), Value = dFeeMaster.Description } : new KeyValueDTO(),
                FeeMasterID = sFeeDueMap.FeeMasterID,
                FeePeriod = dFeePeriod != null ? new KeyValueDTO() { Key = dFeePeriod.FeePeriodID.ToString(), Value = dFeePeriod.Description } : new KeyValueDTO(),
                FeePeriodID = sFeeDueMap.FeePeriodID,
                FeeStructureFeeMapID = sFeeDueMap.FeeStructureFeeMapID,
                StudentFeeDueID = sFeeDueMap.StudentFeeDueID,
                TaxAmount = sFeeDueMap.TaxAmount,
                TaxPercentage = sFeeDueMap.TaxPercentage,
                UpdatedBy = sFeeDue.UpdatedBy,
                UpdatedDate = sFeeDue.UpdatedDate,
                IsFeePeriodDisabled = false,
                IsRowSelected = false,
                IsRefundable = isRefundable,
                FeeDueMonthlySplit = dMontlySplit
            };

            return _sRetData;

        }

        //private FeeDueFeeTypeMapDTO GetFeeDueTypeMap(dbEduegateSchoolContext sContext, StudentFeeDue sFeeDue, FeeDueFeeTypeMap sFeeDueMap)
        //{
        //    MutualRepository mutualRepository = new MutualRepository();
        //    var draftFeeCollectionStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("FEECOLLECTIONSTATUSID_DRAFT").SettingValue ?? "1";

        //    int draftStatusID = int.Parse(draftFeeCollectionStatus);

        //    var dFeeMaster = sContext.FeeMasters.Where(w => w.FeeMasterID == (sFeeDueMap.FeeMasterID ?? 0));
        //    var dFeePeriod = sContext.FeePeriods.Where(w => w.FeePeriodID == (sFeeDueMap.FeePeriodID ?? 0));
        //    var feeCycleID = dFeeMaster.FirstOrDefault()?.FeeCycleID;
        //    var dCreditNotes = GetCreditNote(sContext, sFeeDue, sFeeDueMap);
        //    var dMontlySplit = feeCycleID != 1 ? new List<FeeDueMonthlySplitDTO>() : GetFeeDueMonthlySplit(sContext, sFeeDueMap);
        //    decimal sAmountDue = sFeeDueMap.Amount ?? 0, _sCreditNoteAmount = 0, _sAmountCollected = 0, sBalance = 0;      

        //    #region Partial Receipt => Checking and Allocations
        //    if (dMontlySplit.Any())
        //    {
        //        var dCollectionMonthly = from n in sContext.FeeCollectionMonthlySplits
        //                                 join m in sContext.FeeCollectionFeeTypeMaps on n.FeeCollectionFeeTypeMapId equals m.FeeCollectionFeeTypeMapsIID
        //                                 where m.FeeDueFeeTypeMapsID == sFeeDueMap.FeeDueFeeTypeMapsIID && (m.FeeCollection.FeeCollectionStatusID ?? 0) != draftStatusID
        //                                 group n by new { n.FeeDueMonthlySplitID, m.FeeDueFeeTypeMapsID } into grp
        //                                 select new
        //                                 {
        //                                     FeeDueMonthlySplitID = grp.Key.FeeDueMonthlySplitID,
        //                                     FeeDueFeeTypeMapsID = grp.Key.FeeDueFeeTypeMapsID,
        //                                     CollectedAmount = grp.Sum(w => w.Amount),
        //                                     CreditNoteAmount = grp.Sum(w => w.CreditNoteAmount),
        //                                 };
        //        if (dCollectionMonthly.Any())
        //        {
        //            dMontlySplit.All(w =>
        //            {
        //                if (dCollectionMonthly.Any(x => x.FeeDueFeeTypeMapsID == w.FeeDueFeeTypeMapsID && x.FeeDueMonthlySplitID == w.FeeDueMonthlySplitIID))
        //                {

        //                    w.CreditNoteAmount = dCollectionMonthly.Where(x => x.FeeDueFeeTypeMapsID == w.FeeDueFeeTypeMapsID && x.FeeDueMonthlySplitID == w.FeeDueMonthlySplitIID).Sum(k => k.CreditNoteAmount) ?? 0;
        //                    w.CollectedAmount = dCollectionMonthly.Where(x => x.FeeDueFeeTypeMapsID == w.FeeDueFeeTypeMapsID && x.FeeDueMonthlySplitID == w.FeeDueMonthlySplitIID).Sum(k => k.CollectedAmount) ?? 0;

        //                    w.PrvCollect = dCollectionMonthly.Where(x => x.FeeDueFeeTypeMapsID == w.FeeDueFeeTypeMapsID && x.FeeDueMonthlySplitID == w.FeeDueMonthlySplitIID).Sum(k => k.CollectedAmount) ?? 0;

        //                }
        //                return true;
        //            }
        //            );

        //            _sAmountCollected = dCollectionMonthly.Sum(w => w.CollectedAmount) ?? 0;
        //        }
        //        else
        //        {

        //            var dCollection = from n in sContext.FeeCollectionFeeTypeMaps
        //                              where n.FeeDueFeeTypeMapsID == sFeeDueMap.FeeDueFeeTypeMapsIID
        //                              && (n.FeeCollection.FeeCollectionStatusID ?? 0) != draftStatusID
        //                              group n by n.FeeDueFeeTypeMapsID into grp
        //                              select new { FeeDueFeeTypeMapsID = grp.Key.Value, CreditNoteAmount = grp.Sum(w => w.CreditNoteAmount), CollectedAmount = grp.Sum(w => w.Amount) };
        //            if (dCollection.Any())
        //                _sAmountCollected = dCollection.Sum(w => w.CollectedAmount ?? 0);
        //        }

        //    }
        //    else
        //    {
        //        var dCollection = from n in sContext.FeeCollectionFeeTypeMaps
        //                          where n.FeeDueFeeTypeMapsID == sFeeDueMap.FeeDueFeeTypeMapsIID
        //                           && (n.FeeCollection.FeeCollectionStatusID ?? 0) != draftStatusID
        //                          group n by n.FeeDueFeeTypeMapsID into grp
        //                          select new { FeeDueFeeTypeMapsID = grp.Key.Value, CollectedAmount = grp.Sum(w => w.Amount) };
        //        if (dCollection.Any())
        //            _sAmountCollected = dCollection.Sum(w => w.CollectedAmount) ?? 0;
        //    }
        //    #endregion 
        //    if (dCreditNotes.Any(w => w.FeeMasterID == sFeeDueMap.FeeMasterID))
        //    {
        //        if (dMontlySplit.Any())
        //        {
        //            dMontlySplit.All(w => { SetCreditNoteMonthly(sContext, w, dCreditNotes.FindAll(x => x.FeeMasterID == sFeeDueMap.FeeMasterID)); return true; });
        //            _sCreditNoteAmount = dMontlySplit.Any(w => (w.CreditNoteAmount ?? 0) != 0) ? dMontlySplit.Sum(w => (w.CreditNoteAmount ?? 0)) : 0;
        //        }
        //        else
        //        {
        //            _sCreditNoteAmount = dCreditNotes.Sum(w => w.Amount ?? 0);
        //        }
        //    }
        //    long _sCreditNoteFeeTypeMapID = dCreditNotes.Any() ? dCreditNotes.FirstOrDefault().CreditNoteFeeTypeMapIID : 0;


        //    FeeDueFeeTypeMapDTO _sRetData = new FeeDueFeeTypeMapDTO()
        //    {              

        //        Amount = sAmountDue,
        //        CreditNoteAmount = _sCreditNoteAmount,
        //        Balance = sBalance,
        //        CreditNoteFeeTypeMapID = _sCreditNoteFeeTypeMapID,
        //        InvoiceDate = sFeeDue.InvoiceDate,
        //        InvoiceNo = sFeeDue.InvoiceNo,
        //        CollectedAmount = _sAmountCollected,
        //        CreatedBy = sFeeDue.CreatedBy,
        //        CreatedDate = sFeeDue.CreatedDate,
        //        FeeCycleID = 0,
        //        FeeDueFeeTypeMapsID = sFeeDueMap.FeeDueFeeTypeMapsIID,
        //        FeeMaster = dFeeMaster.Any() ? new KeyValueDTO() { Key = dFeeMaster.FirstOrDefault().FeeMasterID.ToString(), Value = dFeeMaster.FirstOrDefault().Description } : new KeyValueDTO(),
        //        FeeMasterID = sFeeDueMap.FeeMasterID,
        //        FeePeriod = dFeePeriod.Any() ? new KeyValueDTO() { Key = dFeePeriod.FirstOrDefault().FeePeriodID.ToString(), Value = dFeePeriod.FirstOrDefault().Description } : new KeyValueDTO(),
        //        FeePeriodID = sFeeDueMap.FeePeriodID,
        //        FeeStructureFeeMapID = sFeeDueMap.FeeStructureFeeMapID,
        //        StudentFeeDueID = sFeeDueMap.StudentFeeDueID,
        //        TaxAmount = sFeeDueMap.TaxAmount,
        //        TaxPercentage = sFeeDueMap.TaxPercentage,
        //        UpdatedBy = sFeeDue.UpdatedBy,
        //        UpdatedDate = sFeeDue.UpdatedDate,
        //        IsFeePeriodDisabled = false,
        //        IsRowSelected = false,               
        //        FeeDueMonthlySplit = dMontlySplit

        //    };
        //    return _sRetData;

        //}


        private bool SetCreditNoteMonthly(dbEduegateSchoolContext _sContext, FeeDueMonthlySplitDTO _sFeeMonthlySplit, List<CreditNoteFeeTypeMap> _sCreditNotes)
        {
            bool _sFlg = false;
            if (!_sCreditNotes.Any())
                return _sFlg;
            var dFnd = _sCreditNotes.Where(w => w.Year == _sFeeMonthlySplit.Year 
            && w.MonthID == _sFeeMonthlySplit.MonthID 
            && w.FeeDueMonthlySplitID == _sFeeMonthlySplit.FeeDueMonthlySplitIID 
            && w.SchoolCreditNote.IsCancelled!=true);
            if (dFnd.Any())
            {
                decimal _sCreditNoteAmount = dFnd.Sum(w => w.Amount) ?? 0;
                _sFeeMonthlySplit.CreditNoteAmount = _sCreditNoteAmount;
                _sFeeMonthlySplit.Balance = _sFeeMonthlySplit.Amount - _sCreditNoteAmount;
                _sFeeMonthlySplit.CreditNoteFeeTypeMapID = dFnd.FirstOrDefault().CreditNoteFeeTypeMapIID;
                _sFlg = true;
            }
            return _sFlg;
        }



        private List<FeeDueFeeFineMapDTO> GetFineMap(dbEduegateSchoolContext _sContext, StudentFeeDue _sFeeDue)
        {
            List<FeeDueFeeFineMapDTO> _sRetData = new List<FeeDueFeeFineMapDTO>();
            List<FeeDueFeeTypeMap> _sFeeDueTypeMas = _sFeeDue.FeeDueFeeTypeMaps.ToList();
            if (!_sFeeDueTypeMas.Any())
                return _sRetData;

            if (!_sFeeDueTypeMas.Any(w => (w.FineMasterID ?? 0) > 0 && !(w.Status)))
                return _sRetData;
            var dFnd = from n in _sFeeDueTypeMas
                       where (n.FineMasterID ?? 0) > 0 && !(n.Status)
                       select new FeeDueFeeFineMapDTO()
                       {
                           FineMasterID = n.FineMasterID,
                           FineName = n.FineMaster.FineName,
                           InvoiceNo = _sFeeDue.InvoiceNo,
                           InvoiceDate = _sFeeDue.InvoiceDate,
                           FineMasterStudentMapID = n.FineMasterStudentMapID,
                           StudentFeeDueID = n.StudentFeeDueID,
                           FeeDueFeeTypeMapsID = n.FeeDueFeeTypeMapsIID,
                           Amount = n.Amount,
                       };
            if (dFnd.Any())
                _sRetData.AddRange(dFnd);
            return _sRetData;
        }

        private List<CreditNoteFeeTypeMap> GetCreditNote(dbEduegateSchoolContext _sContext, StudentFeeDue _sFeeDue, FeeDueFeeTypeMap _sFeeDueMap)
        {
            List<CreditNoteFeeTypeMap> _sRetData = new List<CreditNoteFeeTypeMap>();
            var dFnd = (from n in _sContext.SchoolCreditNotes
                        join m in _sContext.CreditNoteFeeTypeMaps on n.SchoolCreditNoteIID equals m.SchoolCreditNoteID
                        where n.StudentID == _sFeeDue.StudentId && n.IsCancelled!=true &&  m.FeeDueFeeTypeMapsID == _sFeeDueMap.FeeDueFeeTypeMapsIID
                        && m.FeeMasterID == _sFeeDueMap.FeeMasterID && (_sFeeDueMap.FeePeriodID == null || m.PeriodID == _sFeeDueMap.FeePeriodID)
                        select m).Include(w => w.SchoolCreditNote).AsNoTracking();
            if (dFnd.Any())
                _sRetData.AddRange(dFnd);
            return _sRetData;
        }

        #endregion


        #region Final Settlement 

        public List<FeeCollectionDTO> FillFeeDueDataForSettlement(long studentID, int academicYearID)
        {
            var _sRetData = new List<FeeCollectionDTO>();
            using (var _sContext = new dbEduegateSchoolContext())
            {
                var dFndAcademicYear = (from n in _sContext.AcademicYears where n.AcademicYearID == academicYearID select n).AsNoTracking().FirstOrDefault();
                var dFndAcademicYearIDs = (from n in _sContext.AcademicYears where n.AcademicYearCode == dFndAcademicYear.AcademicYearCode  select n.AcademicYearID).ToList();

                var dFnd = (from n in _sContext.StudentFeeDues
                            join f in _sContext.FeeDueFeeTypeMaps on n.StudentFeeDueIID equals f.StudentFeeDueID
                            join fm in _sContext.FeeMasters on f.FeeMasterID equals fm.FeeMasterID
                            where n.StudentId == studentID && (n.Student.Status ?? 0) != 3 && n.Student.IsActive == true && n.IsCancelled != true &&
                             (fm.FeeType.IsRefundable == true || (n.CollectionStatus == false) || (dFndAcademicYearIDs.Contains(n.AcadamicYearID.Value) && n.CollectionStatus == true))
                            select n)
                            .Include(w => w.FeeDueFeeTypeMaps)
                            .AsNoTracking().ToList();

                if (dFnd.Any())
                {
                    dFnd.All(w => { _sRetData.Add(GetFeeDetailsForSettlement(_sContext, w)); return true; });
                }
            }
            return _sRetData;
        }

        private FeeCollectionDTO GetFeeDetailsForSettlement(dbEduegateSchoolContext _sContext, StudentFeeDue _sFeeDue)
        {
            FeeCollectionDTO _sRetData = new FeeCollectionDTO()
            {
                AcadamicYearID = _sFeeDue.AcadamicYearID,
                AcademicYear = null,
                AdmissionNo = string.Empty,
                ClassID = _sFeeDue.ClassId,
                ClassName = string.Empty,
                CreatedBy = _sFeeDue.CreatedBy,
                CreatedDate = _sFeeDue.CreatedDate,
                Description = string.Empty,
                FeeTypes = GetFeeDueTypeMapForSettlement(_sContext, _sFeeDue),
                SchoolID = _sFeeDue.SchoolID,
                SectionID = _sFeeDue.SectionID,
                SectionName = string.Empty,
                StudentID = _sFeeDue.StudentId,
                StudentName = string.Empty,
            };

            return _sRetData;

        }
        private List<FeeCollectionFeeTypeDTO> GetFeeDueTypeMapForSettlement(dbEduegateSchoolContext _sContext, StudentFeeDue _sFeeDue)
        {
            var _sRetData = new List<FeeCollectionFeeTypeDTO>();
            _sFeeDue.FeeDueFeeTypeMaps.ToList().All(w => { _sRetData.Add(GetFeeDueTypeMapForSettlement(_sContext, _sFeeDue, w)); return true; });
            return _sRetData;
        }

        private FeeCollectionFeeTypeDTO GetFeeDueTypeMapForSettlement(dbEduegateSchoolContext sContext, StudentFeeDue sFeeDue, FeeDueFeeTypeMap sFeeDueMap)
        {
            MutualRepository mutualRepository = new MutualRepository();
            var dFeeMaster = sContext.FeeMasters.Where(w => w.FeeMasterID == (sFeeDueMap.FeeMasterID ?? 0)).Include(w => w.FeeType).AsNoTracking();
            var dFeePeriod = sContext.FeePeriods.Where(w => w.FeePeriodID == (sFeeDueMap.FeePeriodID ?? 0)).AsNoTracking();
            var feeCycleID = dFeeMaster.FirstOrDefault().FeeCycleID;
            var dCreditNotes = GetCreditNoteSettlement(sContext, sFeeDue, sFeeDueMap);
            var dMontlySplit = feeCycleID != 1 ? new List<FeeCollectionMonthlySplitDTO>() : GetMonthlySplitForSettlement(sContext, sFeeDueMap);
            decimal sAmountDue = sFeeDueMap.Amount ?? 0, _sCreditNoteAmount = 0, _sAmountCollected = 0, sBalance = 0;
            var isRefundable = dFeeMaster.FirstOrDefault().FeeType.IsRefundable;
            var draftFeeCollectionStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("FEECOLLECTIONSTATUSID_DRAFT", "1");
            int draftStatusID = int.Parse(draftFeeCollectionStatus);
            #region Partial Receipt => Checking and Allocations
            if (dMontlySplit.Any())
            {
                var dCollectionMonthly = (from n in sContext.FeeCollectionMonthlySplits
                                          join m in sContext.FeeCollectionFeeTypeMaps on n.FeeCollectionFeeTypeMapId equals m.FeeCollectionFeeTypeMapsIID
                                          where m.FeeDueFeeTypeMapsID == sFeeDueMap.FeeDueFeeTypeMapsIID
                                          && (m.FeeCollection.FeeCollectionStatusID ?? 0) != draftStatusID && m.FeeCollection.IsCancelled != true
                                          group n by new { n.FeeDueMonthlySplitID, m.FeeDueFeeTypeMapsID } into grp
                                          select new
                                          {
                                              FeeDueMonthlySplitID = grp.Key.FeeDueMonthlySplitID,
                                              FeeDueMonthlySplitIID = grp.Key.FeeDueMonthlySplitID,
                                              FeeDueFeeTypeMapsID = grp.Key.FeeDueFeeTypeMapsID,
                                              CollectedAmount = grp.Sum(w => w.Amount),
                                              CreditNoteAmount = grp.Sum(w => w.CreditNoteAmount),
                                          });
                if (dCollectionMonthly.Any())
                {
                    dMontlySplit.All(w =>
                    {
                        if (dCollectionMonthly.Any(x => x.FeeDueFeeTypeMapsID == w.FeeDueFeeTypeMapsID && x.FeeDueMonthlySplitID == w.FeeDueMonthlySplitID))
                        {

                            w.CreditNoteAmount = dCollectionMonthly.Where(x => x.FeeDueFeeTypeMapsID == w.FeeDueFeeTypeMapsID && x.FeeDueMonthlySplitID == w.FeeDueMonthlySplitID).Sum(k => k.CreditNoteAmount) ?? 0;
                            w.CollectedAmount = dCollectionMonthly.Where(x => x.FeeDueFeeTypeMapsID == w.FeeDueFeeTypeMapsID && x.FeeDueMonthlySplitID == w.FeeDueMonthlySplitID).Sum(k => k.CollectedAmount) ?? 0;
                            w.ReceivableAmount = isRefundable == false ? (w.CollectedAmount < (w.Amount ?? 0 - w.CreditNoteAmount) ?
                                              (w.Amount - w.CreditNoteAmount) - w.CollectedAmount : 0) : 0;
                            w.RefundAmount = isRefundable == true ? w.CollectedAmount : (w.CollectedAmount > (w.Amount - w.CreditNoteAmount)) ?
                                                             w.CollectedAmount - (w.Amount - w.CreditNoteAmount) : 0;
                            w.PrvCollect = dCollectionMonthly.Where(x => x.FeeDueFeeTypeMapsID == w.FeeDueFeeTypeMapsID && x.FeeDueMonthlySplitID == w.FeeDueMonthlySplitID).Sum(k => k.CollectedAmount) ?? 0;

                        }
                        return true;
                    }
                    );

                    _sAmountCollected = dCollectionMonthly.Sum(w => w.CollectedAmount) ?? 0;
                }
                else
                {

                    var dCollection = from n in sContext.FeeCollectionFeeTypeMaps
                                      where n.FeeDueFeeTypeMapsID == sFeeDueMap.FeeDueFeeTypeMapsIID
                                      && (n.FeeCollection.FeeCollectionStatusID ?? 0) != draftStatusID && n.FeeCollection.IsCancelled != true
                                      group n by n.FeeDueFeeTypeMapsID into grp
                                      select new { FeeDueFeeTypeMapsID = grp.Key.Value, CreditNoteAmount = grp.Sum(w => w.CreditNoteAmount), CollectedAmount = grp.Sum(w => w.Amount) };
                    if (dCollection.Any())
                        _sAmountCollected = dCollection.Sum(w => w.CollectedAmount ?? 0);

                    dMontlySplit.All(w =>
                    {

                        w.ReceivableAmount = isRefundable == false ?
                                          (w.Amount - w.CreditNoteAmount ?? 0) - (w.CollectedAmount ?? 0) : 0;
                        w.RefundAmount = 0;
                        w.PrvCollect = 0;
                        w.CollectedAmount = 0;
                        return true;
                    }
                    );


                }

            }
            else
            {
                var dCollection = from n in sContext.FeeCollectionFeeTypeMaps
                                  where n.FeeDueFeeTypeMapsID == sFeeDueMap.FeeDueFeeTypeMapsIID
                                   && (n.FeeCollection.FeeCollectionStatusID ?? 0) != draftStatusID && n.FeeCollection.IsCancelled != true
                                  group n by n.FeeDueFeeTypeMapsID into grp
                                  select new { FeeDueFeeTypeMapsID = grp.Key.Value, CollectedAmount = grp.Sum(w => w.Amount) };
                if (dCollection.Any())
                    _sAmountCollected = dCollection.Sum(w => w.CollectedAmount) ?? 0;
            }

            if (dCreditNotes.Any(w => w.FeeMasterID == sFeeDueMap.FeeMasterID))
            {
                if (dMontlySplit.Any())
                {
                    dMontlySplit.All(w => { SetCreditNoteMonthlySettlement(sContext, w, dCreditNotes.FindAll(x => x.FeeMasterID == sFeeDueMap.FeeMasterID)); return true; });
                    _sCreditNoteAmount = dMontlySplit.Any(w => (w.CreditNoteAmount ?? 0) != 0) ? dMontlySplit.Sum(w => (w.CreditNoteAmount ?? 0)) : 0;
                }
                else
                {
                    _sCreditNoteAmount = dCreditNotes.Sum(w => w.Amount ?? 0);
                }
            }
            long _sCreditNoteFeeTypeMapID = dCreditNotes.Any() ? dCreditNotes.FirstOrDefault().CreditNoteFeeTypeMapIID : 0;


            #endregion

            var _sRetData = new FeeCollectionFeeTypeDTO()
            {
                Amount = sAmountDue,
                CreditNoteAmount = _sCreditNoteAmount,
                Balance = sBalance,
                CreditNoteFeeTypeMapID = _sCreditNoteFeeTypeMapID,
                InvoiceDate = sFeeDue.InvoiceDate,
                InvoiceNo = sFeeDue.InvoiceNo,
                CollectedAmount = _sAmountCollected,
                CreatedBy = sFeeDue.CreatedBy,
                CreatedDate = sFeeDue.CreatedDate,
                FeeCycleID = 0,
                FeeDueFeeTypeMapsID = sFeeDueMap.FeeDueFeeTypeMapsIID,
                FeeMaster = dFeeMaster.FirstOrDefault().Description,
                FeeMasterID = sFeeDueMap.FeeMasterID,
                FeePeriod = sFeeDueMap.FeePeriodID != null ? dFeePeriod.FirstOrDefault().Description : "",
                FeePeriodID = sFeeDueMap.FeePeriodID,
                FeeStructureFeeMapID = sFeeDueMap.FeeStructureFeeMapID,
                StudentFeeDueID = sFeeDueMap.StudentFeeDueID,
                TaxAmount = sFeeDueMap.TaxAmount,
                TaxPercentage = sFeeDueMap.TaxPercentage,
                UpdatedBy = sFeeDue.UpdatedBy,
                UpdatedDate = sFeeDue.UpdatedDate,
                IsFeePeriodDisabled = false,
                IsRowSelected = false,
                IsRefundable = isRefundable,
                MontlySplitMaps = dMontlySplit
            };
            _sRetData.ReceivableAmount = _sRetData.IsRefundable == false ? (_sRetData.CollectedAmount < (_sRetData.Amount ?? 0 - _sRetData.CreditNoteAmount) ?
                                             (_sRetData.Amount - _sRetData.CreditNoteAmount) - _sRetData.CollectedAmount : 0) : 0;
            _sRetData.RefundAmount = _sRetData.IsRefundable == true ? _sRetData.CollectedAmount : (_sRetData.CollectedAmount > (_sRetData.Amount - _sRetData.CreditNoteAmount)) ?
                                             _sRetData.CollectedAmount - (_sRetData.Amount - _sRetData.CreditNoteAmount) : 0;
            return _sRetData;

        }

        private List<FeeCollectionMonthlySplitDTO> GetMonthlySplitForSettlement(dbEduegateSchoolContext _sContext, FeeDueFeeTypeMap _sFeeDueMap)
        {
            var _sRetData = new List<FeeCollectionMonthlySplitDTO>();
            if ((_sFeeDueMap.FeePeriodID ?? 0) == 0)
                return _sRetData;


            var dFnd = from n in _sContext.FeeDueMonthlySplits.Where(w => w.FeeDueFeeTypeMapsID == _sFeeDueMap.FeeDueFeeTypeMapsIID && w.FeeDueFeeTypeMap.StudentFeeDue.IsCancelled != true)
                       .Include(x => x.FeeDueFeeTypeMap)
                       .ThenInclude(x => x.StudentFeeDue)
                       orderby n.Year, n.MonthID
                       select new FeeCollectionMonthlySplitDTO
                       {
                           FeeDueMonthlySplitID = n.FeeDueMonthlySplitIID,
                           FeeDueFeeTypeMapsID = n.FeeDueFeeTypeMapsID,
                           MonthID = n.MonthID,
                           Amount = n.Amount ?? 0,
                           Year = n.Year ?? 0,
                           CreditNoteAmount = 0,
                           Balance = n.Amount ?? 0

                       };

            if (dFnd.Any())
                _sRetData.AddRange(dFnd);
            return _sRetData;
        }

        private bool SetCreditNoteMonthlySettlement(dbEduegateSchoolContext _sContext, FeeCollectionMonthlySplitDTO _sFeeMonthlySplit, List<CreditNoteFeeTypeMap> _sCreditNotes)
        {
            bool _sFlg = false;
            if (!_sCreditNotes.Any())
                return _sFlg;
            var dFnd = _sCreditNotes.Where(w => w.Year == _sFeeMonthlySplit.Year
            && w.MonthID == _sFeeMonthlySplit.MonthID
            && w.FeeDueMonthlySplitID == _sFeeMonthlySplit.FeeDueMonthlySplitID
            && w.SchoolCreditNote.IsCancelled!=true
            );
            if (dFnd.Any())
            {
                decimal _sCreditNoteAmount = dFnd.Sum(w => w.Amount) ?? 0;
                _sFeeMonthlySplit.CreditNoteAmount = _sCreditNoteAmount;
                _sFeeMonthlySplit.Balance = _sFeeMonthlySplit.Amount - _sCreditNoteAmount;
                _sFeeMonthlySplit.CreditNoteFeeTypeMapID = dFnd.FirstOrDefault().CreditNoteFeeTypeMapIID;
                _sFlg = true;
            }
            return _sFlg;
        }

        private List<CreditNoteFeeTypeMap> GetCreditNoteSettlement(dbEduegateSchoolContext _sContext, StudentFeeDue _sFeeDue, FeeDueFeeTypeMap _sFeeDueMap)
        {
            List<CreditNoteFeeTypeMap> _sRetData = new List<CreditNoteFeeTypeMap>();
            var dFnd = (from n in _sContext.SchoolCreditNotes
                        join m in _sContext.CreditNoteFeeTypeMaps on n.SchoolCreditNoteIID equals m.SchoolCreditNoteID
                        where  n.StudentID == _sFeeDue.StudentId && n.IsCancelled!=true && m.FeeDueFeeTypeMapsID == _sFeeDueMap.FeeDueFeeTypeMapsIID 
                        && m.FeeMasterID == _sFeeDueMap.FeeMasterID && (_sFeeDueMap.FeePeriodID == null || m.PeriodID == _sFeeDueMap.FeePeriodID)
                        select m).Include(w => w.SchoolCreditNote);
            if (dFnd.Any())
                _sRetData.AddRange(dFnd);
            return _sRetData;
        }

        #endregion

        public List<StudentFeeDueDTO> GetFeesByInvoiceNo(long StudentFeeDueID)
        {

            return GetStudentFeeDue(StudentFeeDueID);

            List<StudentFeeDueDTO> feeDueDTO = new List<StudentFeeDueDTO>();
            using (var dbContext = new dbEduegateSchoolContext())
            {

                feeDueDTO = (from stFee in dbContext.StudentFeeDues.AsEnumerable()
                             join FeeTypeMap in dbContext.FeeDueFeeTypeMaps on stFee.StudentFeeDueIID equals FeeTypeMap.StudentFeeDueID
                             join FeeMonthlySplit in dbContext.FeeDueMonthlySplits on FeeTypeMap.FeeDueFeeTypeMapsIID equals FeeMonthlySplit.FeeDueFeeTypeMapsID into tmpMonthlySplit
                             from FeeMonthlySplit in tmpMonthlySplit.DefaultIfEmpty()
                             where (stFee.StudentFeeDueIID == StudentFeeDueID && (FeeTypeMap.FineMasterID != null || FeeTypeMap.FeeMasterID != null))
                             orderby stFee.InvoiceDate ascending
                             group stFee by new { stFee.StudentFeeDueIID, stFee.StudentId, stFee.ClassId, stFee.InvoiceNo, stFee.InvoiceDate, stFee.FeeDueFeeTypeMaps } into stFeeGroup
                             select new StudentFeeDueDTO()
                             {
                                 StudentFeeDueIID = stFeeGroup.Key.StudentFeeDueIID,
                                 StudentId = stFeeGroup.Key.StudentId,
                                 ClassId = stFeeGroup.Key.ClassId,
                                 InvoiceNo = stFeeGroup.Key.InvoiceNo,
                                 InvoiceDate = stFeeGroup.Key.InvoiceDate,
                                 FeeFineMap = (from ft in stFeeGroup.Key.FeeDueFeeTypeMaps//.AsEnumerable()//.AsQueryable()
                                               where ft.Status == false && ft.FineMasterID != null
                                               select new FeeDueFeeFineMapDTO()
                                               {
                                                   FineMasterID = ft.FineMasterID,
                                                   FineName = ft.FineMaster.FineName,
                                                   InvoiceNo = stFeeGroup.Key.InvoiceNo,
                                                   InvoiceDate = stFeeGroup.Key.InvoiceDate,
                                                   FineMasterStudentMapID = ft.FineMasterStudentMapID,
                                                   StudentFeeDueID = ft.StudentFeeDueID,
                                                   FeeDueFeeTypeMapsID = ft.FeeDueFeeTypeMapsIID,
                                                   Amount = ft.Amount,
                                               }).ToList(),
                                 FeeDueFeeTypeMap = (from ft in stFeeGroup.Key.FeeDueFeeTypeMaps
                                                     join crno in dbContext.SchoolCreditNotes on
                                                     stFeeGroup.Key.StudentId equals crno.StudentID
                                                     into tmpcrNot
                                                     from crno in tmpcrNot.Where(cr => cr.StudentID == stFeeGroup.Key.StudentId && cr.Status == false
                                                     //&& (ft.FeePeriodID == null || (((DbFunctions.TruncateTime(ft.FeePeriod.PeriodFrom).Value.Month <=
                                                     //DbFunctions.TruncateTime(cr.CreditNoteDate.Value).Value.Month &&
                                                     //    DbFunctions.TruncateTime(ft.FeePeriod.PeriodFrom).Value.Year <= DbFunctions.TruncateTime(cr.CreditNoteDate.Value).Value.Year)) &&
                                                     //    ((DbFunctions.TruncateTime(ft.FeePeriod.PeriodTo).Value.Year != DbFunctions.TruncateTime(ft.FeePeriod.PeriodFrom).Value.Year ?
                                                     //    DbFunctions.TruncateTime(ft.FeePeriod.PeriodTo).Value.Month + 12 : DbFunctions.TruncateTime(ft.FeePeriod.PeriodTo).Value.Month)
                                                     //    >= DbFunctions.TruncateTime(cr.CreditNoteDate.Value).Value.Month &&
                                                     //    DbFunctions.TruncateTime(ft.FeePeriod.PeriodTo).Value.Year >= DbFunctions.TruncateTime(cr.CreditNoteDate.Value).Value.Year)))

                                                     ).DefaultIfEmpty()
                                                     join cnf in dbContext.CreditNoteFeeTypeMaps on
                                                                              (crno == null ? 0 : crno.SchoolCreditNoteIID) equals cnf.SchoolCreditNoteID
                                                                              into tmpCreditNoteFeeType
                                                     from cnf in tmpCreditNoteFeeType.Where(ftm => (ftm.FeeMasterID == null ? 0 : ftm.FeeMasterID) == ft.FeeMasterID && ftm.Status == false).DefaultIfEmpty()



                                                     where ft.Status == false && ft.FeeMasterID != null && ft.Amount > 0
                                                     group ft by new { ft.Amount, cnf, ft.CollectedAmount, ft.TaxAmount, ft.FeePeriodID, ft.TaxPercentage, ft.StudentFeeDueID, ft.FeeDueFeeTypeMapsIID, ft.FeeStructureFeeMapID, ft.FeeMaster, ft.FeePeriod, ft.FeeDueMonthlySplits } into ftGroup
                                                     select new FeeDueFeeTypeMapDTO()
                                                     {
                                                         Amount = ftGroup.Key.Amount - (ftGroup.Key.CollectedAmount.HasValue ? ftGroup.Key.CollectedAmount.Value : 0),
                                                         CreditNoteAmount = (ftGroup.Key.cnf != null ? -(ftGroup.Key.cnf.Amount) : 0),
                                                         Balance = (ftGroup.Key.Amount - (ftGroup.Key.CollectedAmount.HasValue ? ftGroup.Key.CollectedAmount.Value : 0)) + (ftGroup.Key.cnf != null ? -ftGroup.Key.cnf.Amount : 0),
                                                         //TaxAmount = ftGroup.Key.TaxAmount,
                                                         //CreditNoteAmount = 0,//(ftGroup.Key.cnf != null ? -(ftGroup.Key.cnf.Amount) : 0),
                                                         //Balance = (ftGroup.Key.Amount - (ftGroup.Key.CollectedAmount.HasValue ? ftGroup.Key.CollectedAmount.Value : 0)),
                                                         CreditNoteFeeTypeMapID = (ftGroup.Key.cnf != null ? (ftGroup.Key.cnf.CreditNoteFeeTypeMapIID) : (long?)null),
                                                         InvoiceNo = stFeeGroup.Key.InvoiceNo,
                                                         FeePeriodID = ftGroup.Key.FeePeriodID,
                                                         //TaxPercentage = ftGroup.Key.TaxPercentage,
                                                         StudentFeeDueID = ftGroup.Key.StudentFeeDueID,
                                                         FeeStructureFeeMapID = ftGroup.Key.FeeStructureFeeMapID,
                                                         //FeeCycleID = ftGroup.Key.FeeMaster.FeeCycleID,
                                                         FeeDueFeeTypeMapsIID = ftGroup.Key.FeeDueFeeTypeMapsIID,
                                                         InvoiceDate = stFeeGroup.Key.InvoiceDate,
                                                         //IsFeePeriodDisabled = ftGroup.Key.FeeMaster.FeeCycleID.HasValue ? ftGroup.Key.FeeMaster.FeeCycleID.Value != 3 : true,
                                                         FeeMaster = ftGroup.Key.FeeMaster != null ? new KeyValueDTO()
                                                         {
                                                             Key = ftGroup.Key.FeeMaster != null ? Convert.ToString(ftGroup.Key.FeeMaster.FeeMasterID) : null,
                                                             Value = ftGroup.Key.FeeMaster != null ? ftGroup.Key.FeeMaster.Description : null
                                                         } : new KeyValueDTO(),
                                                         FeePeriod = ftGroup.Key.FeePeriodID.HasValue ? new KeyValueDTO()
                                                         {
                                                             Key = ftGroup.Key.FeePeriodID.HasValue ? Convert.ToString(ftGroup.Key.FeePeriodID) : null,
                                                             Value = !ftGroup.Key.FeePeriodID.HasValue ? null : ftGroup.Key.FeePeriod.Description + " ( " + ftGroup.Key.FeePeriod.PeriodFrom.ToString("dd/MMM/yyy") + '-'
                                                                     + ftGroup.Key.FeePeriod.PeriodTo.ToString("dd/MMM/yyy") + " ) "
                                                         } : new KeyValueDTO(),
                                                         IsExternal = ftGroup.Key.FeeMaster?.IsExternal,
                                                         ReportName = ftGroup.Key.FeeMaster?.ReportName,
                                                         FeeDueMonthlySplit = (from mf in ftGroup.Key.FeeDueMonthlySplits
                                                                               join crno in dbContext.SchoolCreditNotes on
                                                                               stFeeGroup.Key.StudentId equals crno.StudentID
                                                                               into tmpcrNot
                                                                               from crno in tmpcrNot.Where(cr => cr.StudentID == stFeeGroup.Key.StudentId
                                                                               && cr.CreditNoteDate.Value.Month == mf.MonthID
                                                                               && cr.CreditNoteDate.Value.Year == mf.Year && cr.Status == false).DefaultIfEmpty()
                                                                               join cnf in dbContext.CreditNoteFeeTypeMaps on
                                                                               (crno == null ? 0 : crno.SchoolCreditNoteIID) equals cnf.SchoolCreditNoteID
                                                                               into tmpCreditNoteFeeType
                                                                               from cnf in tmpCreditNoteFeeType.Where(ft => ftGroup.Key.FeeMaster.FeeMasterID == ft.FeeMasterID && ft.Status == false).DefaultIfEmpty()

                                                                               where mf.Status == false && mf.Amount > 0

                                                                               group mf by new
                                                                               {
                                                                                   mf.FeeStructureMontlySplitMapID,
                                                                                   mf.FeeDueMonthlySplitIID,
                                                                                   mf.FeeDueFeeTypeMapsID,
                                                                                   mf.MonthID,
                                                                                   mf.Amount,
                                                                                   mf.Year,
                                                                                   cnf
                                                                               } into ftGroupSplit
                                                                               orderby ftGroupSplit.Key.Year, ftGroupSplit.Key.MonthID ascending
                                                                               select new FeeDueMonthlySplitDTO()
                                                                               {
                                                                                   FeeStructureMontlySplitMapID = ftGroupSplit.Key.FeeStructureMontlySplitMapID.Value,
                                                                                   FeeDueMonthlySplitIID = ftGroupSplit.Key.FeeDueMonthlySplitIID,
                                                                                   FeeDueFeeTypeMapsID = ftGroupSplit.Key.FeeDueFeeTypeMapsID,
                                                                                   MonthID = ftGroupSplit.Key.MonthID,
                                                                                   Amount = ftGroupSplit.Key.Amount.HasValue ? ftGroupSplit.Key.Amount : (decimal?)null,
                                                                                   Year = ftGroupSplit.Key.Year.HasValue ? ftGroupSplit.Key.Year.Value : 0,
                                                                                   CreditNoteAmount = (ftGroupSplit.Key.cnf != null ? -ftGroupSplit.Key.cnf.Amount : 0),
                                                                                   Balance = ftGroupSplit.Key.Amount.Value + (ftGroupSplit.Key.cnf != null ? -ftGroupSplit.Key.cnf.Amount : 0),
                                                                                   CreditNoteFeeTypeMapID = (ftGroupSplit.Key.cnf != null ? ftGroupSplit.Key.cnf.CreditNoteFeeTypeMapIID : (long?)null),
                                                                                   //TaxAmount = mf.TaxAmount.HasValue ? mf.TaxAmount : (decimal?)null,
                                                                                   //TaxPercentage = mf.TaxPercentage.HasValue ? mf.TaxPercentage : (decimal?)null
                                                                               }).ToList(),

                                                     }).ToList(),
                             }).ToList();

            }
            return feeDueDTO;
        }



        //public List<StudentFeeDueDTO> GetFeeCollected(long studentId)
        //{
        //    DateTime currentDate = System.DateTime.Now;
        //    DateTime preYearDate = DateTime.Now.AddYears(-1);
        //    List<StudentFeeDueDTO> feeDueDTO = new List<StudentFeeDueDTO>();
        //    using (var dbContext = new dbEduegateSchoolContext())
        //    {

        //        feeDueDTO = (from stFee in dbContext.StudentFeeDues.AsEnumerable()
        //                     join ctFee in dbContext.FeeCollections on stFee.StudentId equals ctFee.StudentID
        //                     join FeeTypeMap in dbContext.FeeDueFeeTypeMaps on stFee.StudentFeeDueIID equals FeeTypeMap.StudentFeeDueID
        //                     join FeeMonthlySplit in dbContext.FeeDueMonthlySplits on FeeTypeMap.FeeDueFeeTypeMapsIID equals FeeMonthlySplit.FeeDueFeeTypeMapsID into tmpMonthlySplit
        //                     from FeeMonthlySplit in tmpMonthlySplit.DefaultIfEmpty()
        //                     where (stFee.StudentId == studentId && (ctFee.CollectionDate != null &&
        //                     (ctFee.CollectionDate.Value.Date) <= (currentDate.Date) && (ctFee.CollectionDate.Value.Date) >= (preYearDate.Date)))
        //                     orderby ctFee.CollectionDate descending
        //                     group stFee by new
        //                     {
        //                         stFee.StudentFeeDueIID,
        //                         stFee.StudentId,
        //                         stFee.ClassId,
        //                         stFee.InvoiceNo,
        //                         ctFee.FeeReceiptNo,
        //                         ctFee.CollectionDate,
        //                         stFee.FeeDueFeeTypeMaps
        //                     } into stFeeGroup

        //                     select new StudentFeeDueDTO()
        //                     {
        //                         StudentFeeDueIID = stFeeGroup.Key.StudentFeeDueIID,
        //                         StudentId = stFeeGroup.Key.StudentId,
        //                         ClassId = stFeeGroup.Key.ClassId,
        //                         InvoiceNo = stFeeGroup.Key.FeeReceiptNo,
        //                         InvoiceDate = stFeeGroup.Key.CollectionDate,

        //                         FeeDueFeeTypeMap = (from ft in stFeeGroup.Key.FeeDueFeeTypeMaps//.AsEnumerable().AsQueryable()
        //                                             where ft.Status == true && ft.FeeMasterID != null
        //                                             group ft by new { ft.Amount, ft.TaxAmount, ft.FeePeriodID, ft.TaxPercentage, ft.StudentFeeDueID, ft.FeeDueFeeTypeMapsIID, ft.FeeStructureFeeMapID, ft.FeeMaster, ft.FeePeriod, ft.FeeDueMonthlySplits } into ftGroup
        //                                             select new FeeDueFeeTypeMapDTO()
        //                                             {
        //                                                 Amount = ftGroup.Key.Amount,
        //                                                 TaxAmount = ftGroup.Key.TaxAmount,
        //                                                 InvoiceNo = stFeeGroup.Key.InvoiceNo,
        //                                                 FeePeriodID = ftGroup.Key.FeePeriodID,
        //                                                 TaxPercentage = ftGroup.Key.TaxPercentage,
        //                                                 StudentFeeDueID = ftGroup.Key.StudentFeeDueID,
        //                                                 FeeStructureFeeMapID = ftGroup.Key.FeeStructureFeeMapID,
        //                                                 FeeCycleID = ftGroup.Key.FeeMaster.FeeCycleID,
        //                                                 FeeDueFeeTypeMapsIID = ftGroup.Key.FeeDueFeeTypeMapsIID,
        //                                                 IsFeePeriodDisabled = ftGroup.Key.FeeMaster.FeeCycleID.HasValue ? ftGroup.Key.FeeMaster.FeeCycleID.Value != 3 : true,
        //                                                 FeeMaster = ftGroup.Key.FeeMaster != null ? new KeyValueDTO()
        //                                                 {
        //                                                     Key = ftGroup.Key.FeeMaster != null ? Convert.ToString(ftGroup.Key.FeeMaster.FeeMasterID) : null,
        //                                                     Value = ftGroup.Key.FeeMaster != null ? ftGroup.Key.FeeMaster.Description : null
        //                                                 } : new KeyValueDTO(),
        //                                                 FeePeriod = ftGroup.Key.FeePeriodID.HasValue ? new KeyValueDTO()
        //                                                 {
        //                                                     Key = ftGroup.Key.FeePeriodID.HasValue ? Convert.ToString(ftGroup.Key.FeePeriodID) : null,
        //                                                     Value = !ftGroup.Key.FeePeriodID.HasValue ? null : ftGroup.Key.FeePeriod.Description + " ( " + ftGroup.Key.FeePeriod.PeriodFrom.ToString("dd/MMM/yyy") + '-'
        //                                                             + ftGroup.Key.FeePeriod.PeriodTo.ToString("dd/MMM/yyy") + " ) "
        //                                                 } : new KeyValueDTO(),

        public List<StudentFeeDueDTO> GetFineCollected(long studentId)
        {
            DateTime currentDate = System.DateTime.Now;
            DateTime preYearDate = DateTime.Now.AddYears(-1);
            List<StudentFeeDueDTO> feeDueDTOList = new List<StudentFeeDueDTO>();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var feeDueDTO = (from stFee in dbContext.StudentFeeDues//.AsEnumerable()
                                 join ctFee in dbContext.FeeCollections on stFee.StudentId equals ctFee.StudentID
                                 join FeeTypeMap in dbContext.FeeDueFeeTypeMaps on stFee.StudentFeeDueIID equals FeeTypeMap.StudentFeeDueID
                                 where (stFee.StudentId == studentId && stFee.IsCancelled != true && (ctFee.CollectionDate != null))
                                 orderby ctFee.CollectionDate descending
                                 select stFee)
                                 .AsNoTracking().ToList();

                feeDueDTOList = feeDueDTO.Select(stFeeGroup => new StudentFeeDueDTO()
                {
                    StudentFeeDueIID = stFeeGroup.StudentFeeDueIID,
                    StudentId = stFeeGroup.StudentId,
                    ClassId = stFeeGroup.ClassId,
                    //InvoiceNo = stFeeGroup..FeeReceiptNo,
                    //InvoiceDate = stFeeGroup.CollectionDate,
                    FeeFineMap = (from ft in stFeeGroup.FeeDueFeeTypeMaps//.AsEnumerable().AsQueryable()
                                  where ft.Status == true && ft.FineMasterID != null

                                  select new FeeDueFeeFineMapDTO()
                                  {
                                      // InvoiceNo = stFeeGroup.InvoiceNo,
                                      //InvoiceDate = stFeeGroup.CollectionDate,
                                      FineMasterID = ft.FineMasterID,
                                      FineName = ft.FineMaster.FineName,
                                      StudentFeeDueID = ft.StudentFeeDueID,
                                      FineMasterStudentMapID = ft.FineMasterStudentMapID,
                                      FeeDueFeeTypeMapsID = ft.FeeDueFeeTypeMapsIID,
                                      Amount = ft.Amount,
                                      FineMapDate = ft.FineMasterStudentMap.FineMapDate
                                  }).ToList(),
                }).ToList();
            }

            return feeDueDTOList;
        }

        public FeeDueFeeTypeMap GetTranferFee(StudentFeeDueDTO toDto, long studentId)
        {
            FeeDueFeeTypeMap mapData = new FeeDueFeeTypeMap();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var queryTransferProcess = new Domain.Setting.SettingBL(null).GetSettingValue<string>("TRANSFER_STATUS_COLLECTION", 1, null);
                if (queryTransferProcess == null)
                    throw new Exception("Please set 'TRANSFER_STATUS_COLLECTION' in settings");

                var queryTrans = (from strss in dbContext.StudentTransferRequest
                                  where (strss.StudentID == studentId && strss.TransferRequestStatusID == int.Parse(queryTransferProcess) && (strss.IsTransferRequested.HasValue ? strss.IsTransferRequested.Value : false) == true)
                                  select strss)
                                  .AsNoTracking().FirstOrDefault();

                if (queryTrans != null)
                {
                    var queryTransferId = new Domain.Setting.SettingBL(null).GetSettingValue<string>("TRANSFER_FEE_ID", 1, null);
                    if (queryTransferId == null)
                        throw new Exception("Please set 'TRANSFER_FEE_ID' in settings");

                    var queryTransferAmount = new Domain.Setting.SettingBL(null).GetSettingValue<string>("TRANSFER_FEE_AMOUNT", 1, null);
                    if (queryTransferAmount == null)
                        throw new Exception("Please set 'queryTransferAmount' in settings");

                    mapData = new FeeDueFeeTypeMap()
                    {
                        FeeDueFeeTypeMapsIID = 0,
                        FeeMasterID = int.Parse(queryTransferId),
                        Amount = decimal.Parse(queryTransferAmount),
                        CollectedAmount = 0
                    };
                }
            }

            return mapData;
        }

        public List<FeeDueMonthlySplitDTO> CheckExistTransportFee(FeeStructureFeeMap feeStructureFeeMap, long studentId)
        {
            var monthlysplitup = new List<FeeDueMonthlySplitDTO>();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                decimal? transportFee = 0, pickUpRoutyeFee = 0, dropStopFee = 0;
                var queryTrans = (from strss in dbContext.StudentRouteStopMaps
                                  join pickup in dbContext.RouteStopMaps on strss.PickupStopMapID equals pickup.RouteStopMapIID
                                   into PickupStop
                                  from pickup in PickupStop.DefaultIfEmpty()
                                  join dropStop in dbContext.RouteStopMaps on strss.DropStopMapID equals dropStop.RouteStopMapIID
                                  into dropStop
                                  from rtm in dropStop.DefaultIfEmpty()
                                  where strss.StudentID == studentId && strss.IsActive == true
                                  select strss).AsNoTracking().FirstOrDefault();

                if (queryTrans == null)
                    transportFee = 0;
                else
                {
                    var monthSlip = feeStructureFeeMap.FeeStructureMontlySplitMaps.Where(x =>
                    ((x.MonthID >= queryTrans.DateFrom.Value.Month &&
                    x.Year.Value >= queryTrans.DateFrom.Value.Year) &&
                    (x.MonthID < (queryTrans.DateTo.Value.Year != queryTrans.DateFrom.Value.Year ? queryTrans.DateTo.Value.Month + 12 : queryTrans.DateTo.Value.Month) &&
                    x.Year.Value <= queryTrans.DateTo.Value.Year)) ||
                    ((x.MonthID >= queryTrans.DateFrom.Value.Month &&
                    x.Year.Value >= queryTrans.DateFrom.Value.Year) &&
                    (x.MonthID <= (queryTrans.DateTo.Value.Year != queryTrans.DateFrom.Value.Year ? queryTrans.DateTo.Value.Month + 12 : queryTrans.DateTo.Value.Month) &&
                    x.Year.Value <= queryTrans.DateTo.Value.Year)));

                    if (monthSlip.Count() == 0)
                    {
                        transportFee = 0;
                    }
                    else
                    {
                        if ((queryTrans.PickupStopMapID != null && queryTrans.DropStopMapID != null) && (queryTrans.PickupStopMapID == queryTrans.DropStopMapID) && queryTrans.RouteStopMap1.Routes1.RouteTypeID == 3)
                        {

                            transportFee = ((queryTrans.RouteStopMap1.OneWayFee == null || queryTrans.RouteStopMap1.OneWayFee == 0) ? queryTrans.RouteStopMap1.Routes1.RouteFareOneWay : queryTrans.RouteStopMap1.OneWayFee);
                        }
                        else
                        {
                            pickUpRoutyeFee = queryTrans.PickupStopMapID == null ? 0 : ((queryTrans.RouteStopMap1.OneWayFee == null || queryTrans.RouteStopMap1.OneWayFee == 0) ? queryTrans.RouteStopMap1.Routes1.RouteFareOneWay : queryTrans.RouteStopMap1.OneWayFee);
                            dropStopFee = queryTrans.DropStopMapID == null ? 0 : ((queryTrans.RouteStopMap2.OneWayFee == null || queryTrans.RouteStopMap2.OneWayFee == 0) ? queryTrans.RouteStopMap2.Routes1.RouteFareOneWay : queryTrans.RouteStopMap2.OneWayFee);
                            transportFee = pickUpRoutyeFee + dropStopFee;
                        }
                    }
                }
                if (transportFee != 0)
                {
                    var monthSlip = dbContext.StudentRouteStopMaps.Where(st => st.StudentID == studentId)
                        .AsNoTracking()
                        .OrderByDescending(o => o.StudentRouteStopMapIID)
                        .FirstOrDefault();

                    monthlysplitup = GetSplitUpPeriod(monthSlip.DateFrom.Value, monthSlip.DateTo.Value, transportFee.Value);
                }
                return monthlysplitup;
            }
        }

        public decimal? CheckExistFines(StudentFeeDueDTO toDto, long studentId)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                decimal? lateFee = 0, damageCostFee = 0;

                //var queryFee = (from lt in dbContext.LibraryTransactions
                //                join bk in dbContext.LibraryBooks on lt.BookID equals bk.LibraryBookIID
                //                where lt.StudentID == studentId && lt.IsCollected == false && lt.LibraryTransactionTypeID == 2
                //                select lt).AsNoTracking();
                var queryFee = dbContext.LibraryTransactions
                    .Where(lt => lt.StudentID == studentId && lt.IsCollected == false && lt.LibraryTransactionTypeID == 2)
                    .Include(i => i.LibraryBook)
                    .AsNoTracking().FirstOrDefault();

                if (queryFee == null)
                {
                    lateFee = 0;
                    damageCostFee = 0;
                }
                else
                {


                }
                return lateFee;
            }
        }

        public List<FeeDueMonthlySplitDTO> GetSplitUpPeriod(DateTime periodFrom, DateTime periodTo, decimal transportFee)
        {
            var periods = new List<FeeDueMonthlySplitDTO>();

            for (DateTime dt = periodFrom; dt <= periodTo; dt = dt.AddMonths(1))
            {
                periods.Add(new FeeDueMonthlySplitDTO()
                {
                    MonthID = dt.Month,
                    Year = dt.Year,
                    Amount = transportFee
                });
            }
            return periods;
        }

        #region SAVE FUNCTIONS

        public override string SaveEntity(BaseMasterDTO dto)
        {
            string message = "1#Fee invoice generated successfully";
            var toDto = dto as StudentFeeDueDTO;

            #region Validation for Generating Fee Due

            if (toDto.StudentFeeDueIID != 0)
            {
                throw new Exception("Sorry, edit is not possible for this screen !");
            }

            if ((toDto.AcadamicYearID ?? 0) == 0)
            {
                return "0#Please select Acadamic Year!";
            }

            if (!toDto.DueDate.HasValue)
            {
                return "0#Please select Due Date!";
            }

            if (toDto.IsAccountPostEdit == true)
            {
                return "0#Account Posting already done. so cannot be edit/save the details!";
            }
            if (toDto.CollectionStatusEdit == true)
            {
                return "0#Collection against this invoice is already done. so cannot  be edit/save the details!";
            }

            if (toDto.ClassId == null && toDto.Student.Count == 0)
            {
                return "0#Please select a class or student!";
            }
            //}
            //else
            //{
            //    if (!toDto.AcadamicYearID.HasValue)
            //    {
            //        return "0#Please select Acadamic Year!";
            //    }
            //    if (toDto.ClassId == null || toDto.ClassId == 0)
            //    {
            //        return "0#Please select class!";

            //    }
            //    if (!toDto.DueDate.HasValue)
            //    {
            //        return "0#Please select Due Date!";
            //    }

            //    if (toDto.IsAccountPostEdit == true)
            //    {
            //        return "0#Account Posting already done. so cannot be edit/save the details!";
            //    }
            //    if (toDto.CollectionStatusEdit == true)
            //    {
            //        return "0#Collection against this invoice is already done. so cannot  be edit/save the details!";
            //    }
            //}

            var queryTransId = "0";
            var studentAccountsId = "0";

            #endregion

            #region Code Created By Sudish => Merge Generating Fee Due (Insert/Update/Delete)

            #region Filteration
            bool _sIsSucces = false;
            string _sStudent_IDs = string.Empty;
            if (toDto.Student != null && toDto.Student.Any())
                _sStudent_IDs = string.Join(",", toDto.Student.Select(w => w.Key));

            string _sPeriod_IDs = string.Empty;
            if (toDto.FeePeriod != null && toDto.FeePeriod.Any())
                _sPeriod_IDs = string.Join(",", toDto.FeePeriod.Select(w => w.Key));

            string _sClass_IDs = string.Empty;
            if (toDto.ClassMaster != null && toDto.ClassMaster.Any())
                _sClass_IDs = string.Join(",", toDto.ClassMaster.Select(w => w.Key));
            else
                _sClass_IDs = (toDto.ClassId ?? 0) > 0 ? _sClass_IDs.ToString() : string.Empty;

            string _sSection_IDs = string.Empty;
            if (toDto.SectionMaster != null && toDto.SectionMaster.Any())
                _sSection_IDs = string.Join(",", toDto.SectionMaster.Select(w => w.Key));
            else
                _sSection_IDs = (toDto.SectionId ?? 0) > 0 ? _sSection_IDs.ToString() : string.Empty;

            string _sFeeMaster_IDs = string.Empty;
            if (toDto.FeeMaster != null && toDto.FeeMaster.Any())
                _sFeeMaster_IDs = string.Join(",", toDto.FeeMaster.Select(w => w.Key));

            string _sFineMaster_IDs = string.Empty;
            if (toDto.FineMaster != null && toDto.FineMaster.Any())
                _sFineMaster_IDs = string.Join(",", toDto.FineMaster.Select(w => w.Key));

            string _sParent_IDs = string.Empty;
            if (toDto.Parents != null && toDto.Parents.Any())
                _sParent_IDs = string.Join(",", toDto.Parents.Select(w => w.Key));

            string _sDueTyeMap_IDs = string.Empty;
            if (toDto.FeeDueFeeTypeMap != null && toDto.FeeDueFeeTypeMap.Any())
                _sDueTyeMap_IDs = string.Join(",", toDto.FeeDueFeeTypeMap.Select(w => w.FeeDueFeeTypeMapsIID));

            Decimal _sFeeAmount = Convert.ToDecimal(toDto.FeeMasterAmount);
            #endregion

            SqlConnectionStringBuilder _sBuilder = new SqlConnectionStringBuilder(Infrastructure.ConfigHelper.GetSchoolConnectionString());
            _sBuilder.ConnectTimeout = 30; // Set Timedout
            using (SqlConnection conn = new SqlConnection(_sBuilder.ConnectionString))
            {
                try { conn.Open(); }
                catch (Exception ex)
                {
                    message = ex.Message; return "0#" + message;
                }
                using (SqlCommand sqlCommand = new SqlCommand("[schools].[SPS_FEE_DUE_MERGE]", conn))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    /*
	                    @ACADEMICYEARID INT=1,
	                    @COMPANYID INT=1,
	                    @SCHOOLID INT=1,
	                    @INVOICEDATE DATETIME='20210529',
	                    @ACCOUNTDATE DATETIME='20210530',
	                    @DUEDATE DATETIME='20210530',
	                    @LOGINID INT=2,
	                    @FEEPERIODIDs  VARCHAR(MAX)='',		-- Filter By Fee Period IDs
	                    @CLASSIDs VARCHAR(MAX)='',			-- Filter By Class IDs
	                    @SECTIONIDs VARCHAR(MAX)='',		-- Filter By Section IDs
	                    @STUDENTIDs VARCHAR(MAX)='',		-- Filter By Student IDs
	                    @FEEMASTERIDs  VARCHAR(MAX)='',		-- Filter By Fee Master IDs
	                    @FINEMASTERIDs  VARCHAR(MAX)='',	-- Filter By Fine Master IDs
	                    @PARENTIDs  VARCHAR(MAX)='',		-- Filter By Parent IDs
	                    @FEEDUEIDs  VARCHAR(MAX)=''	,		-- Fee Due IDs for Editing
	                    @FEEDUETYPEMAPIDs  VARCHAR(MAX)=''	-- Fee Due Type Map IDs for Editing
                        @AMOUNT  MONEY=0					-- Fee Amoun for Custom

                    */
                    sqlCommand.Parameters.Add(new SqlParameter("@ACADEMICYEARID", SqlDbType.Int));
                    sqlCommand.Parameters["@ACADEMICYEARID"].Value = toDto.AcadamicYearID ?? 0;

                    sqlCommand.Parameters.Add(new SqlParameter("@SCHOOLID", SqlDbType.Int));
                    sqlCommand.Parameters["@SCHOOLID"].Value = _context.SchoolID ?? 0;

                    sqlCommand.Parameters.Add(new SqlParameter("@COMPANYID", SqlDbType.Int));
                    sqlCommand.Parameters["@COMPANYID"].Value = _context.CompanyID;

                    sqlCommand.Parameters.Add(new SqlParameter("@INVOICEDATE", SqlDbType.DateTime));
                    sqlCommand.Parameters["@INVOICEDATE"].Value = toDto.InvoiceDate;

                    sqlCommand.Parameters.Add(new SqlParameter("@DUEDATE", SqlDbType.DateTime));
                    sqlCommand.Parameters["@DUEDATE"].Value = toDto.DueDate;

                    sqlCommand.Parameters.Add(new SqlParameter("@ACCOUNTDATE", SqlDbType.DateTime));
                    sqlCommand.Parameters["@ACCOUNTDATE"].Value = toDto.InvoiceDate;

                    sqlCommand.Parameters.Add(new SqlParameter("@CLASSIDs", SqlDbType.VarChar));
                    sqlCommand.Parameters["@CLASSIDs"].Value = toDto.ClassId == 0 ? string.Empty : toDto.ClassId.ToString();

                    sqlCommand.Parameters.Add(new SqlParameter("@STUDENTIDs", SqlDbType.VarChar));
                    sqlCommand.Parameters["@STUDENTIDs"].Value = _sStudent_IDs;

                    sqlCommand.Parameters.Add(new SqlParameter("@SECTIONIDs", SqlDbType.VarChar));
                    sqlCommand.Parameters["@SECTIONIDs"].Value = toDto.SectionId == 0 ? string.Empty : toDto.SectionId.ToString();

                    sqlCommand.Parameters.Add(new SqlParameter("@LOGINID", SqlDbType.Int));
                    sqlCommand.Parameters["@LOGINID"].Value = _context.LoginID;

                    sqlCommand.Parameters.Add(new SqlParameter("@FEEPERIODIDs", SqlDbType.VarChar));
                    sqlCommand.Parameters["@FEEPERIODIDs"].Value = _sPeriod_IDs;

                    sqlCommand.Parameters.Add(new SqlParameter("@FEEMASTERIDs", SqlDbType.VarChar));
                    sqlCommand.Parameters["@FEEMASTERIDs"].Value = _sFeeMaster_IDs;

                    sqlCommand.Parameters.Add(new SqlParameter("@FINEMASTERIDs", SqlDbType.VarChar));
                    sqlCommand.Parameters["@FINEMASTERIDs"].Value = _sFineMaster_IDs;

                    sqlCommand.Parameters.Add(new SqlParameter("@PARENTIDs", SqlDbType.VarChar));
                    sqlCommand.Parameters["@PARENTIDs"].Value = _sParent_IDs;

                    sqlCommand.Parameters.Add(new SqlParameter("@FEEDUEIDs", SqlDbType.VarChar));
                    sqlCommand.Parameters["@FEEDUEIDs"].Value = toDto.StudentFeeDueIID == 0 ? string.Empty : toDto.StudentFeeDueIID.ToString();

                    sqlCommand.Parameters.Add(new SqlParameter("@FEEDUETYPEMAPIDs", SqlDbType.VarChar));
                    sqlCommand.Parameters["@FEEDUETYPEMAPIDs"].Value = _sDueTyeMap_IDs;

                    sqlCommand.Parameters.Add(new SqlParameter("@AMOUNT", SqlDbType.Decimal));
                    sqlCommand.Parameters["@AMOUNT"].Value = _sFeeAmount;

                    sqlCommand.Parameters.Add(new SqlParameter("@REMARKS", SqlDbType.VarChar));
                    sqlCommand.Parameters["@REMARKS"].Value = toDto.Remarks;

                    try
                    {
                        // Run the stored procedure.
                        message = Convert.ToString(sqlCommand.ExecuteScalar() ?? string.Empty);

                        #region Insert Data In AccountTransactionHeads

                        if (toDto.StudentFeeDueIID == 0)
                        {
                            using (var dbContext = new dbEduegateSchoolContext())
                            {
                                int type = 0;

                                var dueIIDList = new List<StudentFeeDueDTO>();
                                string _due_IIDs = string.Empty;

                                if (toDto.Student.Count > 0)
                                {
                                    //toDto foreach to single query
                                    var studentIds = toDto.Student.Select(x => long.Parse(x.Key)).ToList();

                                    _due_IIDs = string.Join(",", dbContext.StudentFeeDues.Where(a => a.AcadamicYearID == toDto.AcadamicYearID && a.InvoiceDate == toDto.InvoiceDate && studentIds.Contains(a.StudentId.Value)).AsNoTracking().ToList().Select(b => b.StudentFeeDueIID));

                                    //foreach (var stud in toDto.Student)
                                    //{
                                    //    var studentID = long.Parse(stud.Key);

                                    //    _due_IIDs = string.Join(",", dbContext.StudentFeeDues.Where(a => a.AcadamicYearID == toDto.AcadamicYearID && a.InvoiceDate == toDto.InvoiceDate && a.StudentId == studentID).AsNoTracking().ToList().Select(b => b.StudentFeeDueIID));

                                    //}

                                }
                                else
                                {
                                    _due_IIDs = string.Join(",", dbContext.StudentFeeDues.Where(x => x.AcadamicYearID == toDto.AcadamicYearID && x.InvoiceDate == toDto.InvoiceDate && x.ClassId == toDto.ClassId && ((toDto.SectionId == 0 || toDto.SectionId == null) || x.SectionID == toDto.SectionId)).AsNoTracking().ToList().Select(a => a.StudentFeeDueIID.ToString())).ToString();
                                }

                                if (_due_IIDs.Length > 0)
                                {
                                    new AccountEntryMapper().AccountTransMergewithMultipleIDs(_due_IIDs, toDto.InvoiceDate.Value, int.Parse(_context.LoginID.Value.ToString()), type);
                                }

                            }
                        }
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        // throw new Exception("Something Wrong! Please check after sometime");
                        message = message.Length > 0 ? message : "0#Error on Saving";
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }

            return message;
            #endregion

            #region Temp Hide By Sudish => Converted to SQL Procedure

            if (toDto.StudentFeeDueIID != 0)
            {
                SaveFeeDue(dto, int.Parse(queryTransId), long.Parse(studentAccountsId));
                return GetEntity(toDto.StudentFeeDueIID);
            }
            else
            {


                if (toDto.FeePeriod.Count > 0 && toDto.Student.Count > 0)
                {
                    message = StudentFeePeriodwiseMap(toDto, int.Parse(queryTransId), long.Parse(studentAccountsId));
                }
                else if (toDto.Student.Count > 0)
                {
                    message = StudentwiseMap(toDto, int.Parse(queryTransId), long.Parse(studentAccountsId));
                }
                else if (toDto.FeePeriod.Count > 0)
                {
                    message = FeePeriodwiseMap(toDto, int.Parse(queryTransId), long.Parse(studentAccountsId));
                }
                else if ((toDto.FeePeriod.Count == 0) && (toDto.Student.Count == 0) && (toDto.FeeMaster.Count == 0))
                {
                    //var loginId = CallContext.EmailID;
                    //message = AllstudentsMapping(toDto, int.Parse(queryTransId), long.Parse(studentAccountsId));
                    using (SqlConnection conn = new SqlConnection(Infrastructure.ConfigHelper.GetSchoolConnectionString()))
                    {

                        using (SqlCommand sqlCommand = new SqlCommand("[schools].[SPS_FEE_DUE_CLASSWISE]", conn))
                        {
                            sqlCommand.CommandType = CommandType.StoredProcedure;

                            sqlCommand.Parameters.Add(new SqlParameter("@ACADEMICYEARID", SqlDbType.Int));
                            sqlCommand.Parameters["@ACADEMICYEARID"].Value = toDto.AcadamicYearID;

                            sqlCommand.Parameters.Add(new SqlParameter("@INVOICEDATE", SqlDbType.DateTime));
                            sqlCommand.Parameters["@INVOICEDATE"].Value = toDto.InvoiceDate;

                            sqlCommand.Parameters.Add(new SqlParameter("@DUEDATE", SqlDbType.DateTime));
                            sqlCommand.Parameters["@DUEDATE"].Value = toDto.DueDate;

                            sqlCommand.Parameters.Add(new SqlParameter("@CLASSID", SqlDbType.Int));
                            sqlCommand.Parameters["@CLASSID"].Value = toDto.ClassId;

                            sqlCommand.Parameters.Add(new SqlParameter("@LOGINID", SqlDbType.Int));
                            sqlCommand.Parameters["@LOGINID"].Value = 2;

                            try
                            {
                                conn.Open();

                                // Run the stored procedure.
                                sqlCommand.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                //throw new Exception("Something Wrong! Please check after sometime");
                            }
                            finally
                            {
                                conn.Close();
                            }
                        }
                    }
                }
            }
            return message;
            #endregion 
        }

        public string DueFees(int? feemasterID, DateTime? invoiceDate, decimal? amount, long? studentID, long? creditNoteID)
        {
            if (creditNoteID.HasValue)
            {
                using (var dbContext = new dbEduegateSchoolContext())
                {
                    var dCreditNote = dbContext.SchoolCreditNotes.Where(x => x.SchoolCreditNoteIID == creditNoteID.Value).AsNoTracking().FirstOrDefault();
                    if (dCreditNote != null)
                    {
                        dCreditNote.Status = true;

                        dbContext.Entry(dCreditNote).State = EntityState.Modified;
                        dbContext.SaveChanges();
                    }
                }
            }
            string message = "1#Fee invoice generated successfully";
            #region Filteration
            bool _sIsSucces = false;
            string _sStudent_IDs = string.Empty;
            if (studentID != null)
                _sStudent_IDs = string.Join(",", studentID);

            string _sFeeMaster_IDs = string.Empty;
            if (feemasterID != null)
                _sFeeMaster_IDs = string.Join(",", feemasterID);


            #endregion

            SqlConnectionStringBuilder _sBuilder = new SqlConnectionStringBuilder(Infrastructure.ConfigHelper.GetSchoolConnectionString());
            _sBuilder.ConnectTimeout = 30; // Set Timedout
            using (SqlConnection conn = new SqlConnection(_sBuilder.ConnectionString))
            {
                try { conn.Open(); }
                catch (Exception ex)
                {
                    message = ex.Message; return "0#" + message;
                }
                using (SqlCommand sqlCommand = new SqlCommand("[schools].[SPS_FEE_DUE_MERGE]", conn))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;


                    sqlCommand.Parameters.Add(new SqlParameter("@ACADEMICYEARID", SqlDbType.Int));
                    sqlCommand.Parameters["@ACADEMICYEARID"].Value = _context.AcademicYearID ?? 0;

                    sqlCommand.Parameters.Add(new SqlParameter("@SCHOOLID", SqlDbType.Int));
                    sqlCommand.Parameters["@SCHOOLID"].Value = _context.SchoolID ?? 0;

                    sqlCommand.Parameters.Add(new SqlParameter("@COMPANYID", SqlDbType.Int));
                    sqlCommand.Parameters["@COMPANYID"].Value = _context.CompanyID;

                    sqlCommand.Parameters.Add(new SqlParameter("@INVOICEDATE", SqlDbType.DateTime));
                    sqlCommand.Parameters["@INVOICEDATE"].Value = invoiceDate;

                    sqlCommand.Parameters.Add(new SqlParameter("@DUEDATE", SqlDbType.DateTime));
                    sqlCommand.Parameters["@DUEDATE"].Value = invoiceDate;

                    sqlCommand.Parameters.Add(new SqlParameter("@ACCOUNTDATE", SqlDbType.DateTime));
                    sqlCommand.Parameters["@ACCOUNTDATE"].Value = invoiceDate;

                    sqlCommand.Parameters.Add(new SqlParameter("@CLASSIDs", SqlDbType.VarChar));
                    sqlCommand.Parameters["@CLASSIDs"].Value = string.Empty;

                    sqlCommand.Parameters.Add(new SqlParameter("@STUDENTIDs", SqlDbType.VarChar));
                    sqlCommand.Parameters["@STUDENTIDs"].Value = _sStudent_IDs;

                    sqlCommand.Parameters.Add(new SqlParameter("@SECTIONIDs", SqlDbType.VarChar));
                    sqlCommand.Parameters["@SECTIONIDs"].Value = string.Empty;

                    sqlCommand.Parameters.Add(new SqlParameter("@LOGINID", SqlDbType.Int));
                    sqlCommand.Parameters["@LOGINID"].Value = _context.LoginID;

                    sqlCommand.Parameters.Add(new SqlParameter("@FEEPERIODIDs", SqlDbType.VarChar));
                    sqlCommand.Parameters["@FEEPERIODIDs"].Value = string.Empty;

                    sqlCommand.Parameters.Add(new SqlParameter("@FEEMASTERIDs", SqlDbType.VarChar));
                    sqlCommand.Parameters["@FEEMASTERIDs"].Value = _sFeeMaster_IDs;

                    sqlCommand.Parameters.Add(new SqlParameter("@FINEMASTERIDs", SqlDbType.VarChar));
                    sqlCommand.Parameters["@FINEMASTERIDs"].Value = string.Empty;

                    sqlCommand.Parameters.Add(new SqlParameter("@PARENTIDs", SqlDbType.VarChar));
                    sqlCommand.Parameters["@PARENTIDs"].Value = string.Empty;

                    sqlCommand.Parameters.Add(new SqlParameter("@FEEDUEIDs", SqlDbType.VarChar));
                    sqlCommand.Parameters["@FEEDUEIDs"].Value = string.Empty;

                    sqlCommand.Parameters.Add(new SqlParameter("@FEEDUETYPEMAPIDs", SqlDbType.VarChar));
                    sqlCommand.Parameters["@FEEDUETYPEMAPIDs"].Value = string.Empty;

                    sqlCommand.Parameters.Add(new SqlParameter("@AMOUNT", SqlDbType.Decimal));
                    sqlCommand.Parameters["@AMOUNT"].Value = amount;

                    try
                    {
                        // Run the stored procedure.
                        message = Convert.ToString(sqlCommand.ExecuteScalar() ?? string.Empty);

                    }
                    catch (Exception ex)
                    {
                        // throw new Exception("Something Wrong! Please check after sometime");
                        message = "0#Error on Saving";
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            return message;
        }

        public string SaveFeeDue(BaseMasterDTO dto, int settingsTransId, long studentAccountsId)
        {
            var toDto = dto as StudentFeeDueDTO;

            long studId = 0;
            string message = string.Empty;
            List<long> invGenStudents = new List<long>();
            MutualRepository mutualRepository = new MutualRepository();
            var entityCreate = new StudentFeeDue();
            List<StudentFeeDue> entityFeeDue = new List<StudentFeeDue>();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                if (toDto.StudentId != 0)
                {

                    //if (queryFeeType.Count() > 0)
                    //{
                    //    foreach (var studdet in toDto.Student)
                    //    {
                    studId = toDto.StudentId.Value;

                    entityCreate = new StudentFeeDue()
                    {
                        StudentFeeDueIID = toDto.StudentFeeDueIID,
                        StudentId = toDto.StudentId,
                        ClassId = toDto.ClassId,
                        InvoiceNo = toDto.InvoiceNo,
                        InvoiceDate = toDto.InvoiceDate,
                        AcadamicYearID = toDto.AcadamicYearID,
                        SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                        DueDate = toDto.DueDate,
                        CreatedBy = toDto.StudentFeeDueIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                        UpdatedBy = toDto.StudentFeeDueIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                        CreatedDate = toDto.StudentFeeDueIID == 0 ? DateTime.Now : dto.CreatedDate,
                        UpdatedDate = toDto.StudentFeeDueIID > 0 ? DateTime.Now : dto.UpdatedDate,
                        //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
                        IsAccountPost = toDto.IsAccountPost,

                    };

                    //get FeeDueFeeTypeMap
                    var IIDs = toDto.FeeDueFeeTypeMap
                        .Select(a => a.FeeDueFeeTypeMapsIID).ToList();

                    //delete maps
                    var entities = dbContext.FeeDueFeeTypeMaps.Where(x =>
                        x.StudentFeeDueID == entityCreate.StudentFeeDueIID &&
                        !IIDs.Contains(x.FeeDueFeeTypeMapsIID)).AsNoTracking().ToList();

                    if (entities != null)
                        dbContext.FeeDueFeeTypeMaps.RemoveRange(entities);
                    if (toDto.FeeFineMap.Count > 0)
                    {
                        //get FeeDueFeeTypeMap
                        var IID = toDto.FeeFineMap
                            .Select(a => a.FeeDueFeeTypeMapsID).ToList();

                        //delete maps
                        var entity = dbContext.FeeDueFeeTypeMaps.Where(x =>
                            x.StudentFeeDueID == entityCreate.StudentFeeDueIID &&
                            !IID.Contains(x.FeeDueFeeTypeMapsIID)).AsNoTracking().ToList();

                        if (entity != null)
                            dbContext.FeeDueFeeTypeMaps.RemoveRange(entity);
                    }
                    entityCreate.FeeDueFeeTypeMaps = new List<FeeDueFeeTypeMap>();
                    FeeDueFeeTypeMap mapData = GetTranferFee(toDto, toDto.StudentId.Value);
                    if (mapData.IsNotNull() && mapData.Amount.HasValue)
                    {
                        var queryFeeExist = dbContext.FeeDueFeeTypeMaps.Where(s => s.FeeMasterID == mapData.FeeMasterID && s.StudentFeeDue.StudentId == toDto.StudentId.Value).AsNoTracking().ToList();
                        if (queryFeeExist.Count == 0)
                        {
                            if (invGenStudents.Contains(toDto.StudentId.Value) == false)
                            {
                                invGenStudents.Add(toDto.StudentId.Value);
                            }
                            entityCreate.FeeDueFeeTypeMaps.Add(mapData);
                        }
                    }
                    foreach (var feetypedet in toDto.FeeDueFeeTypeMap)
                    {
                        //get existing split iids
                        var splitIIDs = feetypedet.FeeDueMonthlySplit
                            .Select(a => a.FeeDueMonthlySplitIID).ToList();

                        //delete split maps
                        var splitEntities = dbContext.FeeDueMonthlySplits.Where(x =>
                            x.FeeDueFeeTypeMapsID == feetypedet.FeeDueFeeTypeMapsIID &&
                            !splitIIDs.Contains(x.FeeDueMonthlySplitIID)).AsNoTracking().ToList();

                        if (splitEntities != null)
                            dbContext.FeeDueMonthlySplits.RemoveRange(splitEntities);

                        if (invGenStudents.Contains(studId) == false)
                        {
                            invGenStudents.Add(studId);
                        }

                        var monthlySplit = new List<FeeDueMonthlySplit>();
                        //decimal splitAmount = 0;
                        //if (feetypedet.FeeDueMonthlySplit.Count > 0)
                        //    splitAmount = feetypedet.Amount.Value / feetypedet.FeeDueMonthlySplit.Count;
                        foreach (var feeMasterMonthlyDto in feetypedet.FeeDueMonthlySplit)
                        {
                            var entityChild = new FeeDueMonthlySplit()
                            {
                                FeeStructureMontlySplitMapID = feeMasterMonthlyDto.FeeStructureMontlySplitMapID,
                                Amount = feeMasterMonthlyDto.Amount,
                                FeeDueFeeTypeMapsID = feetypedet.FeeDueFeeTypeMapsIID,
                                MonthID = byte.Parse(feeMasterMonthlyDto.MonthID.ToString()),
                                Year = feeMasterMonthlyDto.Year,
                                FeeDueMonthlySplitIID = feeMasterMonthlyDto.FeeDueMonthlySplitIID,
                                TaxAmount = feeMasterMonthlyDto.TaxAmount.HasValue ? feeMasterMonthlyDto.TaxAmount : (decimal?)null,
                                TaxPercentage = feeMasterMonthlyDto.TaxPercentage.HasValue ? feeMasterMonthlyDto.TaxPercentage : (decimal?)null
                            };

                            monthlySplit.Add(entityChild);
                            if (entityChild.FeeDueMonthlySplitIID != 0)
                            {
                                dbContext.Entry(entityChild).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }
                            else
                            {
                                dbContext.Entry(entityChild).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            }
                        }

                        if (feetypedet.FeeMasterID.HasValue)
                        {
                            entityCreate.FeeDueFeeTypeMaps.Add(new FeeDueFeeTypeMap()
                            {
                                FeeDueFeeTypeMapsIID = feetypedet.FeeDueFeeTypeMapsIID,
                                StudentFeeDueID = toDto.StudentFeeDueIID,
                                FeeMasterID = feetypedet.FeeMasterID,
                                FeeStructureFeeMapID = feetypedet.FeeStructureFeeMapID,
                                FeePeriodID = feetypedet.FeePeriodID,
                                Amount = feetypedet.Amount,
                                CollectedAmount = 0,
                                TaxPercentage = feetypedet.TaxPercentage,
                                TaxAmount = feetypedet.TaxAmount,
                                FeeDueMonthlySplits = monthlySplit,
                                CreatedBy = (int)_context.LoginID,
                                CreatedDate = DateTime.Now,

                            });
                        }
                    }

                    foreach (var feetypedet in toDto.FeeFineMap)
                    {
                        var monthlySplit = new List<FeeDueMonthlySplit>();
                        if (feetypedet.FineMasterID.HasValue)
                        {
                            entityCreate.FeeDueFeeTypeMaps.Add(new FeeDueFeeTypeMap()
                            {
                                FeeDueFeeTypeMapsIID = feetypedet.FeeDueFeeTypeMapsID.Value,
                                FineMasterID = feetypedet.FineMasterID,
                                Amount = feetypedet.Amount,
                                CollectedAmount = 0,
                                FineMasterStudentMapID = feetypedet.FineMasterStudentMapID,
                                FeeDueMonthlySplits = monthlySplit,
                                CreatedBy = (int)_context.LoginID,
                                CreatedDate = DateTime.Now,
                                StudentFeeDueID = toDto.StudentFeeDueIID,
                            });
                        }
                    }
                }
                else
                {
                    throw new Exception("No student is available for assigning fee!");
                }

            }

            using (var dbContext = new dbEduegateSchoolContext())
            {
                if (entityCreate.FeeDueFeeTypeMaps.Count() > 0)
                {
                    if (entityCreate.StudentFeeDueIID == 0)
                    {
                        dbContext.Entry(entityCreate).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    }
                    else
                    {
                        foreach (var feeMap in entityCreate.FeeDueFeeTypeMaps)
                        {
                            if (feeMap.FeeDueFeeTypeMapsIID != 0)
                            {
                                dbContext.Entry(feeMap).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }
                            else
                            {
                                dbContext.Entry(feeMap).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            }
                        }

                        dbContext.Entry(entityCreate).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }
                    dbContext.SaveChanges();

                    //toDto.StudentFeeDueIID = entityCreate.StudentFeeDueIID;


                    if (toDto.IsAccountPost == true)
                    {
                        AccountPostingForGroup(entityFeeDue, studentAccountsId, toDto.ClassId.Value, toDto.DocumentTypeID, toDto.CostCenterID);
                    }
                }
            }

            return ToDTOString(new FeeCollectionDTO());
        }

        private List<FeeDueMonthlySplit> GetFeeMonthlyUpdate(dbEduegateSchoolContext _sEntiy, long accountTransactionHeadID, long FeeDueFeeTypeMapsID)
        {
            List<FeeDueMonthlySplit> _sRetData = new List<FeeDueMonthlySplit>();

            _sRetData = _sEntiy.FeeDueMonthlySplits.Where(x => x.FeeDueFeeTypeMapsID == FeeDueFeeTypeMapsID).AsNoTracking().ToList();
            if (_sRetData.Any())
                _sRetData.All(w => { w.AccountTransactionHeadID = accountTransactionHeadID; return true; });

            return _sRetData;
        }

        //private string AllstudentsMapping(StudentFeeDueDTO toDto, int settingsTransId, long studentAccountsId)
        //{
        //    List<long> invGenStudents = new List<long>();
        //    MutualRepository mutualRepository = new MutualRepository();
        //    Entity.Models.Settings.Sequence sequence = new Entity.Models.Settings.Sequence();

        //    List<long?> feeStructureId = new List<long?>();

        //    var entityCreate = new StudentFeeDue();
        //    using (var dbContext = new dbEduegateSchoolContext())
        //    {
        //        var repositoryStudentFee = new EntityRepository<StudentFeeDue, dbEduegateSchoolContext>(dbContext);
        //        var queryStudent = (from st in dbContext.Students.Where(s => s.ClassID == toDto.ClassId && s.IsActive == true) select st).ToList();

        //        string _sMsg = string.Empty;
        //        bool _sNotFound = true;
        //        //(from st in dbContext.Students.Where(s => s.ClassID == toDto.ClassId && s.IsActive == true) select st).ToList()
        //        //.All(w => { _sNotFound= (w.FeeStartDate != null);  return (w.FeeStartDate != null); });
        //        //if (_sNotFound)
        //        //{
        //        //   // _sMsg = "0#Fee Start Date not set for the student '" + w.AdmissionNumber + " ' '" + w.FirstName + " ' " + w.MiddleName + " ' " + w.LastName + "'!";
        //        //}

        //        if (!queryStudent.Any())
        //        {
        //            return "0#No student is available for assigning fee!";
        //        }
        //        else
        //        {

        //            if (queryStudent.Any(w => w.FeeStartDate == null))
        //            {
        //                queryStudent.Where(w => w.FeeStartDate == null).All(w => { _sMsg += (_sMsg.Length > 0 ? System.Environment.NewLine : string.Empty) + "0#Fee Start Date not set for the student '" + w.AdmissionNumber + " ' '" + w.FirstName + " ' " + w.MiddleName + " ' " + w.LastName + "'!"; return true; });
        //                return _sMsg;
        //            }

        //            //foreach (var studdet in queryStudent)
        //            //{
        //            //    DateTime? feeStartDate = dbContext.Students.Where(x => x.StudentIID == studdet.StudentIID).Select(x => x.FeeStartDate).FirstOrDefault();
        //            //    if (!feeStartDate.HasValue)
        //            //    {
        //            //        var message = "0#Fee Start Date not set for the student '" + studdet.AdmissionNumber + " ' '" + studdet.FirstName + " ' " + studdet.MiddleName + " ' " + studdet.LastName + "'!";
        //            //        return message;
        //            //    }
        //            //}

        //            // queryStudent.All(w => { dbContext.Students.Any(w=> w) });


        //            DateTime? feeStartDate1 = queryStudent.Min(w => w.FeeStartDate);
        //            List<FeeStructureFeeMap> LstFeeStructureMaps = (from st in dbContext.FeeStructureFeeMaps
        //              .Where(s => (s.FeePeriodID == null ||
        //              (
        //               (DbFunctions.TruncateTime(s.FeePeriod.PeriodFrom) >= DbFunctions.TruncateTime(feeStartDate1.Value) ||
        //               (DbFunctions.TruncateTime(s.FeePeriod.PeriodFrom) <= DbFunctions.TruncateTime(feeStartDate1.Value) &&
        //              DbFunctions.TruncateTime(s.FeePeriod.PeriodTo) >= DbFunctions.TruncateTime(feeStartDate1.Value))
        //               ) &&
        //               ((((DbFunctions.TruncateTime(s.FeePeriod.PeriodTo))
        //               <= DbFunctions.TruncateTime(toDto.DueDate.Value)) ||
        //              (DbFunctions.TruncateTime(s.FeePeriod.PeriodFrom) <= DbFunctions.TruncateTime(toDto.DueDate.Value) &&
        //              DbFunctions.TruncateTime(s.FeePeriod.PeriodTo) >= DbFunctions.TruncateTime(toDto.DueDate.Value))
        //               )))
        //              ))
        //                                                            select st).ToList();

        //            _sMsg = string.Empty;
        //            queryStudent.All(w => { _sMsg = AllstudentsMapping(dbContext, toDto, settingsTransId, studentAccountsId, w, LstFeeStructureMaps); return _sMsg.Length == 0; });
        //            return _sMsg;


        //        }
        //        //else
        //        //{
        //        //    return "0#No student is available for assigning fee!";
        //        //}
        //        return "1#Fee invoice generated successfully";
        //    }

        //    //if (invGenStudents.Count == 0)
        //    //{
        //    //    return "0#There is no new fee invoice generated for stundents!";
        //    //}
        //    //else
        //    //{
        //    //return "1#Fee invoice generated successfully for " + Convert.ToString(invGenStudents.Count) + " Student(s).";
        //    //}

        //}
        private string AllstudentsMapping(StudentFeeDueDTO toDto, int settingsTransId, long studentAccountsId)
        {
            List<long> invGenStudents = new List<long>();
            MutualRepository mutualRepository = new MutualRepository();
            Entity.Models.Settings.Sequence sequence = new Entity.Models.Settings.Sequence();
            decimal? transportFee = 0, transportFeeAmount = 0;
            List<long?> feeStructureId = new List<long?>();
            decimal? totalTransportFee = 0;
            List<StudentFeeDue> entityFeeDue = new List<StudentFeeDue>();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var queryStudent = dbContext.Students.Where(s => s.ClassID == toDto.ClassId && s.IsActive == true).AsNoTracking().ToList();

                var entityCreate = new StudentFeeDue();
                if (queryStudent.Count() > 0)
                {
                    var studentIDs = queryStudent.Select(x => x.StudentIID);
                    var studentDet = dbContext.Students.Where(x => studentIDs.Contains(x.StudentIID) && x.FeeStartDate == null).AsNoTracking().FirstOrDefault();
                    if (studentDet != null)
                    {
                        var msg = "0#Fee Start Date not set for the student '" + studentDet.FirstName + " " + studentDet.MiddleName + " " + studentDet.LastName + " ' !";
                        return msg;
                    }

                    foreach (var studdet in queryStudent)
                    {
                        DateTime? feeStartDate = studdet.FeeStartDate;

                        FeeDueFeeTypeMap mapData = GetTranferFee(toDto, studdet.StudentIID);
                        if (mapData.IsNotNull() && mapData.Amount.HasValue)
                        {

                            var queryFeeExist = (!dbContext.FeeDueFeeTypeMaps.AsNoTracking().Any(s => s.FeeMasterID == mapData.FeeMasterID &&
                           (s.FeePeriodID == null || s.FeePeriodID == mapData.FeePeriodID) && s.StudentFeeDue.StudentId == studdet.StudentIID));

                            if (queryFeeExist == true)
                            {

                                try
                                {
                                    sequence = mutualRepository.GetNextSequence("InvoiceNo",null);
                                }
                                catch (Exception ex)
                                {
                                    return "0#Please generate sequence with 'InvoiceNo'!";
                                }
                                entityCreate = new StudentFeeDue()
                                {
                                    StudentFeeDueIID = toDto.StudentFeeDueIID,
                                    StudentId = studdet.StudentIID,
                                    AcadamicYearID = toDto.AcadamicYearID,
                                    InvoiceNo = sequence.Prefix + sequence.LastSequence,
                                    ClassId = toDto.ClassId,
                                    SectionID = studdet.SectionID,
                                    InvoiceDate = toDto.InvoiceDate,
                                    DueDate = toDto.DueDate,
                                    CreatedBy = toDto.CreatedBy,
                                    UpdatedBy = toDto.UpdatedBy,
                                    CreatedDate = toDto.CreatedDate,
                                    UpdatedDate = toDto.UpdatedDate,
                                    IsAccountPost = toDto.IsAccountPost,

                                };

                                if (invGenStudents.Contains(studdet.StudentIID) == false)
                                {
                                    invGenStudents.Add(studdet.StudentIID);
                                }
                                entityCreate.FeeDueFeeTypeMaps.Add(mapData);
                            }

                        }

                        feeStructureId = new FeeDataRepository().GetFeeStructureID(toDto.ClassId, studdet.StudentIID, toDto.AcadamicYearID.Value);


                        var queryFeeType = (from st in dbContext.FeeStructureFeeMaps
                                     .Where(s => feeStructureId.Contains(s.FeeStructureID) && (s.FeePeriodID == null ||
                                     ((s.FeePeriod.PeriodFrom.Date >= feeStartDate.Value.Date ||
                                     (s.FeePeriod.PeriodFrom.Date <= feeStartDate.Value.Date
                                     && s.FeePeriod.PeriodTo.Date >= feeStartDate.Value.Date)
                                     ) &&
                                     ((s.FeePeriod.PeriodTo.Date <= toDto.DueDate.Value.Date) ||
                                    (s.FeePeriod.PeriodFrom.Date <= toDto.DueDate.Value.Date &&
                                    s.FeePeriod.PeriodTo.Date >= toDto.DueDate.Value.Date)))))
                                            select st)
                                    .Include(x => x.FeeMaster).Include(y => y.FeeStructureMontlySplitMaps).AsNoTracking();

                        if (queryFeeType.Count() > 0)
                        {


                            foreach (var feetypedet in queryFeeType)
                            {
                                transportFee = 0;
                                totalTransportFee = 0;
                                transportFeeAmount = 0;

                                var querydue = dbContext.FeeDueFeeTypeMaps
                                    .Where(s => s.FeeStructureFeeMapID == feetypedet.FeeStructureFeeMapIID && s.FeeMasterID == feetypedet.FeeMasterID
                                    && s.StudentFeeDue.StudentId == studdet.StudentIID)
                                    .AsNoTracking()
                                    .ToList();

                                if (querydue.Count() == 0)
                                {
                                    if (invGenStudents.Contains(studdet.StudentIID) == false)
                                    {
                                        entityCreate = new StudentFeeDue()
                                        {
                                            StudentId = studdet.StudentIID,
                                            ClassId = toDto.ClassId,
                                            SectionID = studdet.SectionID,
                                            AcadamicYearID = toDto.AcadamicYearID,
                                            // InvoiceNo = sequence.Prefix + sequence.LastSequence,
                                            InvoiceDate = toDto.InvoiceDate,
                                            DueDate = toDto.DueDate,
                                            CreatedBy = toDto.CreatedBy,
                                            UpdatedBy = toDto.UpdatedBy,
                                            CreatedDate = toDto.CreatedDate,
                                            UpdatedDate = toDto.UpdatedDate,
                                            IsAccountPost = toDto.IsAccountPost,
                                        };

                                        entityCreate.FeeDueFeeTypeMaps = new List<FeeDueFeeTypeMap>();
                                    }

                                    if (feetypedet.FeeMaster.FeeTypeID == settingsTransId)
                                    {
                                        List<FeeDueMonthlySplitDTO> TransportPeriod = CheckExistTransportFee(feetypedet, studdet.StudentIID);
                                        transportFee = TransportPeriod.Select(x => x.Amount).LastOrDefault();
                                        if (TransportPeriod.Count > 0)
                                        {

                                            var monthlySplit = new List<FeeDueMonthlySplit>();
                                            foreach (var feeMasterMonthlyDto in feetypedet.FeeStructureMontlySplitMaps)
                                            {

                                                if (TransportPeriod.Where(x => x.MonthID == feeMasterMonthlyDto.MonthID.Value && x.Year == feeMasterMonthlyDto.Year.Value).Count() > 0 && ((feeStartDate.Value.Month <= ((feeStartDate.Value.Year < feeMasterMonthlyDto.Year.Value ?
                                       feeMasterMonthlyDto.MonthID.Value + 12 : feeMasterMonthlyDto.MonthID.Value))
                                       && feeStartDate.Value.Year <= feeMasterMonthlyDto.Year.Value)))
                                                    transportFeeAmount = transportFee;
                                                else
                                                    transportFeeAmount = 0;
                                                totalTransportFee = totalTransportFee + transportFeeAmount;
                                                if (transportFeeAmount > 0)
                                                {
                                                    var entityChild = new FeeDueMonthlySplit()
                                                    {
                                                        FeeDueMonthlySplitIID = 0,
                                                        FeeDueFeeTypeMapsID = 0,
                                                        FeeStructureMontlySplitMapID = feeMasterMonthlyDto.FeeStructureMontlySplitMapIID,
                                                        MonthID = byte.Parse(feeMasterMonthlyDto.MonthID.ToString()),
                                                        Year = feeMasterMonthlyDto.Year.HasValue ? feeMasterMonthlyDto.Year : (int?)null,
                                                        FeePeriodID = feetypedet.FeePeriodID.HasValue ? feetypedet.FeePeriodID : (int?)null,
                                                        Amount = transportFee,//feeMasterMonthlyDto.Amount.HasValue ? feeMasterMonthlyDto.Amount : (decimal?)null,
                                                        TaxAmount = feeMasterMonthlyDto.TaxAmount.HasValue ? feeMasterMonthlyDto.TaxAmount : (decimal?)null,
                                                        TaxPercentage = feeMasterMonthlyDto.TaxPercentage.HasValue ? feeMasterMonthlyDto.TaxPercentage : (decimal?)null
                                                    };

                                                    monthlySplit.Add(entityChild);

                                                }
                                            }
                                            if (totalTransportFee != 0)
                                            {
                                                if (invGenStudents.Contains(studdet.StudentIID) == false)
                                                {
                                                    invGenStudents.Add(studdet.StudentIID);
                                                    try
                                                    {
                                                        sequence = mutualRepository.GetNextSequence("InvoiceNo",null);
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        return "0#Please generate sequence with 'InvoiceNo'!";
                                                    }
                                                    entityCreate.InvoiceNo = sequence.Prefix + sequence.LastSequence;
                                                }
                                                entityCreate.FeeDueFeeTypeMaps.Add(new FeeDueFeeTypeMap()
                                                {
                                                    FeeDueFeeTypeMapsIID = 0,
                                                    FeeMasterID = feetypedet.FeeMasterID,
                                                    //FeeMasterClassMapID = feetypedet.FeeMasterClassMapIID,
                                                    //ClassFeeMasterID = feetypedet.ClassFeeMasterID,
                                                    FeeStructureFeeMapID = feetypedet.FeeStructureFeeMapIID,
                                                    FeePeriodID = feetypedet.FeePeriodID,
                                                    Amount = totalTransportFee,
                                                    CollectedAmount = 0,
                                                    //TaxPercentage = feetypedet.TaxPercentage,
                                                    //TaxAmount = feetypedet.TaxAmount,
                                                    FeeDueMonthlySplits = monthlySplit,
                                                    CreatedBy = (int)_context.LoginID,
                                                    CreatedDate = DateTime.Now,
                                                });
                                            }
                                        }

                                    }
                                    else
                                    {

                                        //applying discount
                                        // feetypedet.Amount = CheckExistFeeDiscount(feetypedet.FeeMasterID, feetypedet.FeePeriodID, studdet.StudentIID, feetypedet.Amount);
                                        decimal splitAmount = 0;
                                        decimal totalAmount = 0;

                                        var monthlySplit = new List<FeeDueMonthlySplit>();
                                        foreach (var feeMasterMonthlyDto in feetypedet.FeeStructureMontlySplitMaps)
                                        {
                                            if ((feeStartDate.Value.Month <= ((feeStartDate.Value.Year < feeMasterMonthlyDto.Year.Value ?
                              feeMasterMonthlyDto.MonthID.Value + 12 : feeMasterMonthlyDto.MonthID.Value))
                              && feeStartDate.Value.Year <= feeMasterMonthlyDto.Year.Value))
                                            {
                                                splitAmount = feeMasterMonthlyDto.Amount.Value;
                                            }
                                            else
                                            {
                                                splitAmount = 0;
                                            }
                                            totalAmount = totalAmount + splitAmount;
                                            if (splitAmount > 0)
                                            {
                                                var entityChild = new FeeDueMonthlySplit()
                                                {

                                                    FeeDueMonthlySplitIID = 0,
                                                    FeeDueFeeTypeMapsID = 0,
                                                    FeeStructureMontlySplitMapID = feeMasterMonthlyDto.FeeStructureMontlySplitMapIID,
                                                    MonthID = byte.Parse(feeMasterMonthlyDto.MonthID.ToString()),
                                                    Year = feeMasterMonthlyDto.Year.HasValue ? feeMasterMonthlyDto.Year : (int?)null,
                                                    Amount = splitAmount,
                                                    TaxAmount = feeMasterMonthlyDto.TaxAmount.HasValue ? feeMasterMonthlyDto.TaxAmount : (decimal?)null,
                                                    TaxPercentage = feeMasterMonthlyDto.TaxPercentage.HasValue ? feeMasterMonthlyDto.TaxPercentage : (decimal?)null
                                                };

                                                monthlySplit.Add(entityChild);

                                            }
                                        }
                                        if (((feetypedet.FeeStructureMontlySplitMaps.Count > 0) && totalAmount != 0) || ((feetypedet.FeeStructureMontlySplitMaps.Count == 0) && feetypedet.Amount != 0))
                                        {
                                            if (invGenStudents.Contains(studdet.StudentIID) == false)
                                            {
                                                invGenStudents.Add(studdet.StudentIID);
                                                try
                                                {
                                                    sequence = mutualRepository.GetNextSequence("InvoiceNo", null);
                                                }
                                                catch (Exception ex)
                                                {
                                                    return "0#Please generate sequence with 'InvoiceNo'!";
                                                }
                                                entityCreate.InvoiceNo = sequence.Prefix + sequence.LastSequence;
                                            }
                                            entityCreate.FeeDueFeeTypeMaps.Add(new FeeDueFeeTypeMap()
                                            {
                                                FeeDueFeeTypeMapsIID = 0,
                                                FeeMasterID = feetypedet.FeeMasterID,
                                                FeeStructureFeeMapID = feetypedet.FeeStructureFeeMapIID,
                                                //FeeMasterClassMapID = feetypedet.FeeMasterClassMapIID,
                                                //ClassFeeMasterID = feetypedet.ClassFeeMasterID,
                                                FeePeriodID = feetypedet.FeePeriodID,
                                                Amount = totalAmount == 0 ? feetypedet.Amount : totalAmount,
                                                CollectedAmount = 0,
                                                //TaxPercentage = feetypedet.TaxPercentage,
                                                //TaxAmount = feetypedet.TaxAmount,
                                                FeeDueMonthlySplits = monthlySplit,
                                                CreatedBy = (int)_context.LoginID,
                                                CreatedDate = DateTime.Now,
                                            });
                                        }
                                    }


                                }

                            }
                        }

                        var queryFine = (from st in dbContext.FineMasterStudentMaps
                                    .Where(s => s.StudentId == studdet.StudentIID &&
                                    !dbContext.FeeDueFeeTypeMaps.Any(sp => sp.FineMasterStudentMapID == s.FineMasterStudentMapIID))
                                         select st).AsNoTracking();

                        if (queryFine.Count() > 0)
                        {
                            if (invGenStudents.Contains(studdet.StudentIID) == false)
                            {
                                invGenStudents.Add(studdet.StudentIID);

                                try
                                {
                                    sequence = mutualRepository.GetNextSequence("InvoiceNo", null);
                                }
                                catch (Exception ex)
                                {
                                    return "0#Please generate sequence with 'InvoiceNo'!";
                                }
                                entityCreate = new StudentFeeDue()
                                {
                                    StudentFeeDueIID = toDto.StudentFeeDueIID,
                                    StudentId = studdet.StudentIID,
                                    AcadamicYearID = toDto.AcadamicYearID,
                                    InvoiceNo = sequence.Prefix + sequence.LastSequence,
                                    ClassId = toDto.ClassId,
                                    SectionID = studdet.SectionID,
                                    InvoiceDate = toDto.InvoiceDate,
                                    DueDate = toDto.DueDate,
                                    CreatedBy = toDto.CreatedBy,
                                    UpdatedBy = toDto.UpdatedBy,
                                    CreatedDate = toDto.CreatedDate,
                                    UpdatedDate = toDto.UpdatedDate,
                                    IsAccountPost = toDto.IsAccountPost,

                                };
                                entityCreate.FeeDueFeeTypeMaps = new List<FeeDueFeeTypeMap>();
                            }
                            foreach (var queryFineDet in queryFine)
                            {
                                var monthlySplit = new List<FeeDueMonthlySplit>();
                                entityCreate.FeeDueFeeTypeMaps.Add(new FeeDueFeeTypeMap()
                                {
                                    FeeDueFeeTypeMapsIID = 0,
                                    Amount = queryFineDet.Amount,
                                    CollectedAmount = 0,
                                    FineMasterID = queryFineDet.FineMasterID,
                                    FeeDueMonthlySplits = monthlySplit,
                                    FineMasterStudentMapID = queryFineDet.FineMasterStudentMapIID,
                                    CreatedBy = (int)_context.LoginID,
                                    CreatedDate = DateTime.Now,

                                });
                            }

                        }

                        if (invGenStudents.Count > 0 && entityCreate.FeeDueFeeTypeMaps.Count() > 0)
                        {
                            entityFeeDue.Add(entityCreate);
                        }

                    }
                    dbContext.StudentFeeDues.AddRange(entityFeeDue);
                    dbContext.SaveChanges();
                    if (toDto.IsAccountPost == true)
                    {
                        AccountPostingForGroup(entityFeeDue, studentAccountsId, toDto.ClassId.Value, toDto.DocumentTypeID, toDto.CostCenterID);

                    }

                }
                else
                {
                    return "0#No student is available for assigning fee!";
                }

            }
            if (invGenStudents.Count == 0)
            {
                return "0#There is no new fee invoice generated for student(s)!";
            }
            else
            {
                return "1#Fee invoice generated successfully for " + Convert.ToString(invGenStudents.Count) + " Student(s).";
            }
        }

        private string StudentwiseMap(StudentFeeDueDTO toDto, int settingsTransId, long studentAccountsId)
        {
            long studId = 0;
            string message = string.Empty;
            decimal? transportFee = 0, transportFeeAmount = 0;
            List<long?> feeStructureId = new List<long?>();
            decimal? totalTransportFee = 0;
            List<long> invGenStudents = new List<long>();
            MutualRepository mutualRepository = new MutualRepository();
            Entity.Models.Settings.Sequence sequence = new Entity.Models.Settings.Sequence();
            List<FeeDueFeeTypeMapDTO> feeDueFeeTypeMap = new List<FeeDueFeeTypeMapDTO>();
            List<StudentFeeDue> entityFeeDue = new List<StudentFeeDue>();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entityCreate = new StudentFeeDue();
                if (toDto.Student.Count() > 0)
                {
                    var studentIDs = toDto.Student.Select(x => long.Parse(x.Key));

                    var studentDet = dbContext.Students.Where(x => studentIDs.Contains(x.StudentIID) && x.FeeStartDate == null).AsNoTracking().FirstOrDefault();
                    if (studentDet != null)
                    {
                        var msg = "0#Fee Start Date not set for the student '" + studentDet.FirstName + " " + studentDet.MiddleName + " " + studentDet.LastName + " ' !";
                        return msg;
                    }

                    var students = dbContext.Students
                        .Where(x => studentIDs.Contains(x.StudentIID)).AsNoTracking();

                    foreach (var studdet in students)
                    {
                        studId = studdet.StudentIID;

                        DateTime? feeStartDate = studdet.FeeStartDate;
                        FeeDueFeeTypeMap mapData = GetTranferFee(toDto, studId);
                        if (mapData.IsNotNull() && mapData.Amount.HasValue)
                        {
                            var queryFeeExist = (!dbContext.FeeDueFeeTypeMaps.AsNoTracking().Any(s => s.FeeMasterID == mapData.FeeMasterID &&
                            (s.FeePeriodID == null || s.FeePeriodID == mapData.FeePeriodID) && s.StudentFeeDue.StudentId == studId));
                            if (queryFeeExist == true)
                            {
                                try
                                {
                                    sequence = mutualRepository.GetNextSequence("InvoiceNo", null);
                                }
                                catch (Exception ex)
                                {
                                    return "0#Please generate sequence with 'InvoiceNo'!";
                                }
                                entityCreate = new StudentFeeDue()
                                {
                                    StudentFeeDueIID = toDto.StudentFeeDueIID,
                                    StudentId = studId,
                                    AcadamicYearID = toDto.AcadamicYearID,
                                    InvoiceNo = sequence.Prefix + sequence.LastSequence,
                                    ClassId = toDto.ClassId,
                                    SectionID = studdet.SectionID.Value,
                                    InvoiceDate = toDto.InvoiceDate,
                                    DueDate = toDto.DueDate,
                                    CreatedBy = toDto.CreatedBy,
                                    UpdatedBy = toDto.UpdatedBy,
                                    CreatedDate = toDto.CreatedDate,
                                    UpdatedDate = toDto.UpdatedDate,
                                    IsAccountPost = toDto.IsAccountPost,

                                };

                                if (invGenStudents.Contains(studId) == false)
                                {
                                    invGenStudents.Add(studId);
                                }
                                entityCreate.FeeDueFeeTypeMaps.Add(mapData);
                            }

                        }

                        feeStructureId = new FeeDataRepository().GetFeeStructureID(toDto.ClassId, studId, toDto.AcadamicYearID.Value);


                        var queryFeeType = dbContext.FeeStructureFeeMaps
                                     .Where(s => feeStructureId.Contains(s.FeeStructureID) && (s.FeePeriodID == null ||
                                     ((s.FeePeriod.PeriodFrom.Date >= feeStartDate.Value.Date ||
                                     (s.FeePeriod.PeriodFrom.Date <= feeStartDate.Value.Date &&
                                    s.FeePeriod.PeriodTo.Date >= feeStartDate.Value.Date)) &&
                                     (((s.FeePeriod.PeriodTo.Date <= toDto.DueDate.Value.Date) ||
                                    (s.FeePeriod.PeriodFrom.Date <= toDto.DueDate.Value.Date &&
                                    s.FeePeriod.PeriodTo.Date >= toDto.DueDate.Value.Date))))))
                                     .Include(x => x.FeeMaster)
                                     .Include(y => y.FeeStructureMontlySplitMaps)
                                     .AsNoTracking().ToList();

                        if (queryFeeType.Count() > 0)
                        {
                            foreach (var feetypedet in queryFeeType)
                            {
                                transportFee = 0;
                                totalTransportFee = 0;
                                transportFeeAmount = 0;
                                var querydue = (!dbContext.FeeDueFeeTypeMaps.AsNoTracking().Any(s => s.FeeStructureFeeMapID == feetypedet.FeeStructureFeeMapIID && s.FeeMasterID == feetypedet.FeeMasterID && s.FeePeriodID == feetypedet.FeePeriodID && s.StudentFeeDue.StudentId == studId));

                                if (querydue == true)
                                {
                                    if (invGenStudents.Contains(studId) == false)
                                    {
                                        entityCreate = new StudentFeeDue()
                                        {
                                            StudentFeeDueIID = toDto.StudentFeeDueIID,
                                            StudentId = studId,
                                            AcadamicYearID = toDto.AcadamicYearID,
                                            ClassId = toDto.ClassId,
                                            SectionID = studdet.SectionID.Value,
                                            InvoiceDate = toDto.InvoiceDate,
                                            DueDate = toDto.DueDate,
                                            CreatedBy = toDto.CreatedBy,
                                            UpdatedBy = toDto.UpdatedBy,
                                            CreatedDate = toDto.CreatedDate,
                                            UpdatedDate = toDto.UpdatedDate,
                                            IsAccountPost = toDto.IsAccountPost,

                                        };
                                        entityCreate.FeeDueFeeTypeMaps = new List<FeeDueFeeTypeMap>();
                                    }
                                    if (feetypedet.FeeMaster.FeeTypeID == settingsTransId)
                                    {
                                        List<FeeDueMonthlySplitDTO> TransportPeriod = CheckExistTransportFee(feetypedet, studId);

                                        transportFee = TransportPeriod.Select(x => x.Amount).LastOrDefault();

                                        if (TransportPeriod.Count > 0)
                                        {
                                            var monthlySplit = new List<FeeDueMonthlySplit>();

                                            foreach (var feeMasterMonthlyDto in feetypedet.FeeStructureMontlySplitMaps)
                                            {
                                                if (TransportPeriod.Where(x => x.MonthID == feeMasterMonthlyDto.MonthID.Value &&
                                                x.Year == feeMasterMonthlyDto.Year.Value).Count() > 0 &&
                                                ((feeStartDate.Value.Month <= ((feeStartDate.Value.Year < feeMasterMonthlyDto.Year.Value ?
                                                feeMasterMonthlyDto.MonthID.Value + 12 : feeMasterMonthlyDto.MonthID.Value))
                                                && feeStartDate.Value.Year <= feeMasterMonthlyDto.Year.Value)))
                                                    transportFeeAmount = transportFee;
                                                else
                                                    transportFeeAmount = 0;

                                                totalTransportFee = totalTransportFee + transportFeeAmount;

                                                if (transportFeeAmount > 0)
                                                {
                                                    var entityChild = new FeeDueMonthlySplit()
                                                    {
                                                        FeeStructureMontlySplitMapID = feeMasterMonthlyDto.FeeStructureMontlySplitMapIID,
                                                        MonthID = byte.Parse(feeMasterMonthlyDto.MonthID.ToString()),
                                                        FeePeriodID = feetypedet.FeePeriodID.HasValue ? feetypedet.FeePeriodID : (int?)null,
                                                        Year = feeMasterMonthlyDto.Year.HasValue ? feeMasterMonthlyDto.Year : (int?)null,
                                                        Amount = transportFeeAmount,
                                                        TaxAmount = feeMasterMonthlyDto.TaxAmount.HasValue ? feeMasterMonthlyDto.TaxAmount : (decimal?)null,
                                                        TaxPercentage = feeMasterMonthlyDto.TaxPercentage.HasValue ? feeMasterMonthlyDto.TaxPercentage : (decimal?)null
                                                    };

                                                    monthlySplit.Add(entityChild);
                                                }

                                            }
                                            if (totalTransportFee != 0)
                                            {
                                                if (invGenStudents.Contains(studId) == false)
                                                {
                                                    invGenStudents.Add(studId);
                                                    try
                                                    {
                                                        sequence = mutualRepository.GetNextSequence("InvoiceNo", null);
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        return "0#Please generate sequence with 'InvoiceNo'!";
                                                    }
                                                    entityCreate.InvoiceNo = sequence.Prefix + sequence.LastSequence;
                                                }

                                                entityCreate.FeeDueFeeTypeMaps.Add(new FeeDueFeeTypeMap()
                                                {
                                                    FeeMasterID = feetypedet.FeeMasterID,
                                                    FeeStructureFeeMapID = feetypedet.FeeStructureFeeMapIID,
                                                    FeePeriodID = feetypedet.FeePeriodID,
                                                    Amount = totalTransportFee,
                                                    CollectedAmount = 0,
                                                    FeeDueMonthlySplits = monthlySplit,
                                                    CreatedBy = (int)_context.LoginID,
                                                    CreatedDate = DateTime.Now,
                                                });
                                            }
                                        }
                                    }
                                    else
                                    {
                                        decimal splitAmount = 0;
                                        decimal totalAmount = 0;
                                        var monthlySplit = new List<FeeDueMonthlySplit>();

                                        foreach (var feeMasterMonthlyDto in feetypedet.FeeStructureMontlySplitMaps)
                                        {
                                            if ((feeStartDate.Value.Month <= ((feeStartDate.Value.Year < feeMasterMonthlyDto.Year.Value ?
                                            feeMasterMonthlyDto.MonthID.Value + 12 : feeMasterMonthlyDto.MonthID.Value))
                                            && feeStartDate.Value.Year <= feeMasterMonthlyDto.Year.Value))
                                            {
                                                splitAmount = feeMasterMonthlyDto.Amount.Value;
                                            }
                                            else
                                            {
                                                splitAmount = 0;
                                            }
                                            totalAmount = totalAmount + splitAmount;
                                            if (splitAmount > 0)
                                            {
                                                var entityChild = new FeeDueMonthlySplit()
                                                {
                                                    FeeStructureMontlySplitMapID = feeMasterMonthlyDto.FeeStructureMontlySplitMapIID,
                                                    MonthID = byte.Parse(feeMasterMonthlyDto.MonthID.ToString()),
                                                    FeePeriodID = feetypedet.FeePeriodID,
                                                    Year = feeMasterMonthlyDto.Year.HasValue ? feeMasterMonthlyDto.Year : (int?)null,
                                                    Amount = splitAmount,
                                                    TaxAmount = feeMasterMonthlyDto.TaxAmount.HasValue ? feeMasterMonthlyDto.TaxAmount : (decimal?)null,
                                                    TaxPercentage = feeMasterMonthlyDto.TaxPercentage.HasValue ? feeMasterMonthlyDto.TaxPercentage : (decimal?)null
                                                };

                                                monthlySplit.Add(entityChild);
                                            }

                                        }
                                        if (((feetypedet.FeeStructureMontlySplitMaps.Count > 0) && totalAmount != 0) ||
                                            ((feetypedet.FeeStructureMontlySplitMaps.Count == 0) && feetypedet.Amount != 0))
                                        {
                                            if (invGenStudents.Contains(studId) == false)
                                            {
                                                invGenStudents.Add(studId);
                                                try
                                                {
                                                    sequence = mutualRepository.GetNextSequence("InvoiceNo", null);
                                                }
                                                catch (Exception ex)
                                                {
                                                    return "0#Please generate sequence with 'InvoiceNo'!";
                                                }
                                                entityCreate.InvoiceNo = sequence.Prefix + sequence.LastSequence;
                                            }
                                            entityCreate.FeeDueFeeTypeMaps.Add(new FeeDueFeeTypeMap()
                                            {
                                                FeeStructureFeeMapID = feetypedet.FeeStructureFeeMapIID,
                                                FeeMasterID = feetypedet.FeeMasterID,
                                                FeePeriodID = feetypedet.FeePeriodID,
                                                Amount = totalAmount == 0 ? feetypedet.Amount : totalAmount,
                                                CollectedAmount = 0,
                                                FeeDueMonthlySplits = monthlySplit,
                                                CreatedBy = (int)_context.LoginID,
                                                CreatedDate = DateTime.Now,

                                            });
                                        }
                                    }
                                }

                            }
                        }
                        var queryFine = (from st in dbContext.FineMasterStudentMaps
                                   .Where(s => s.StudentId == studId &&
                                   !dbContext.FeeDueFeeTypeMaps.Any(sp => sp.FineMasterStudentMapID == s.FineMasterStudentMapIID))
                                         select st).AsNoTracking();

                        if (queryFine.Count() > 0)
                        {
                            if (invGenStudents.Contains(studId) == false)
                            {
                                invGenStudents.Add(studId);

                                try
                                {
                                    sequence = mutualRepository.GetNextSequence("InvoiceNo", null);
                                }
                                catch (Exception ex)
                                {
                                    return "0#Please generate sequence with 'InvoiceNo'!";
                                }
                                entityCreate = new StudentFeeDue()
                                {
                                    StudentFeeDueIID = toDto.StudentFeeDueIID,
                                    StudentId = studId,
                                    InvoiceNo = sequence.Prefix + sequence.LastSequence,
                                    AcadamicYearID = toDto.AcadamicYearID,
                                    ClassId = toDto.ClassId,
                                    SectionID = studdet.SectionID.Value,
                                    InvoiceDate = toDto.InvoiceDate,
                                    DueDate = toDto.DueDate,
                                    CreatedBy = toDto.CreatedBy,
                                    UpdatedBy = toDto.UpdatedBy,
                                    CreatedDate = toDto.CreatedDate,
                                    UpdatedDate = toDto.UpdatedDate,
                                    IsAccountPost = toDto.IsAccountPost,

                                };
                                entityCreate.FeeDueFeeTypeMaps = new List<FeeDueFeeTypeMap>();
                            }

                            foreach (var queryFineDet in queryFine)
                            {
                                var monthlySplit = new List<FeeDueMonthlySplit>();
                                entityCreate.FeeDueFeeTypeMaps.Add(new FeeDueFeeTypeMap()
                                {
                                    FeeDueFeeTypeMapsIID = 0,
                                    Amount = queryFineDet.Amount,
                                    CollectedAmount = 0,
                                    FineMasterID = queryFineDet.FineMasterID,
                                    FeeDueMonthlySplits = monthlySplit,
                                    FineMasterStudentMapID = queryFineDet.FineMasterStudentMapIID,
                                    CreatedBy = (int)_context.LoginID,
                                    CreatedDate = DateTime.Now,

                                });
                            }

                        }
                        if (invGenStudents.Count > 0 && entityCreate.FeeDueFeeTypeMaps.Count() > 0)
                        {
                            entityFeeDue.Add(entityCreate);
                        }

                    }
                    dbContext.StudentFeeDues.AddRange(entityFeeDue);
                    dbContext.SaveChanges();
                    if (toDto.IsAccountPost == true)
                    {
                        AccountPostingForGroup(entityFeeDue, studentAccountsId, toDto.ClassId.Value, toDto.DocumentTypeID, toDto.CostCenterID);
                    }
                }
                else
                {
                    return "0#No student is available for assigning fee!";
                }
            }

            if (invGenStudents.Count == 0)
            {
                return "0#There is no new fee invoice generated for student(s)!";
            }
            else
            {
                return "1#Fee invoice generated successfully for " + Convert.ToString(invGenStudents.Count) + " Student(s).";
            }
        }

        private string FeePeriodwiseMap(StudentFeeDueDTO toDto, int settingsTransId, long studentAccountsId)
        {
            int feePerioId = 0;
            string message = string.Empty;
            decimal? transportFee = 0, transportFeeAmount = 0;
            decimal? totalTransportFee = 0;
            List<long?> feeStructureId = new List<long?>();
            List<long> invGenStudents = new List<long>();
            var entityCreate = new StudentFeeDue();
            MutualRepository mutualRepository = new MutualRepository();
            Entity.Models.Settings.Sequence sequence = new Entity.Models.Settings.Sequence();
            List<StudentFeeDue> entityFeeDue = new List<StudentFeeDue>();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var queryStudent = dbContext.Students.Where(s => s.ClassID == toDto.ClassId && s.IsActive == true).AsNoTracking().ToList();

                if (queryStudent.Count() > 0)
                {
                    var studentIDs = queryStudent.Select(x => x.StudentIID);
                    var studentDet = dbContext.Students.Where(x => studentIDs.Contains(x.StudentIID) && x.FeeStartDate == null).AsNoTracking().FirstOrDefault();
                    if (studentDet != null)
                    {
                        var msg = "0#Fee Start Date not set for the student '" + studentDet.FirstName + " " + studentDet.MiddleName + " " + studentDet.LastName + " ' !";
                        return msg;
                    }

                    foreach (var studdet in queryStudent)
                    {
                        DateTime? feeStartDate = studdet.FeeStartDate;

                        transportFee = 0;
                        totalTransportFee = 0;

                        FeeDueFeeTypeMap mapData = GetTranferFee(toDto, studdet.StudentIID);
                        if (mapData.IsNotNull() && mapData.Amount.HasValue)
                        {
                            var queryFeeExist = (!dbContext.FeeDueFeeTypeMaps.AsNoTracking().Any(s => s.FeeMasterID == mapData.FeeMasterID &&
                            (s.FeePeriodID == null || s.FeePeriodID == mapData.FeePeriodID) && s.StudentFeeDue.StudentId == studdet.StudentIID));
                            if (queryFeeExist == true)
                            {
                                try
                                {
                                    sequence = mutualRepository.GetNextSequence("InvoiceNo", null);
                                }
                                catch (Exception ex)
                                {
                                    return "0#Please generate sequence with 'InvoiceNo'!";
                                }
                                entityCreate = new StudentFeeDue()
                                {
                                    //StudentFeeDueIID = toDto.StudentFeeDueIID,
                                    StudentId = studdet.StudentIID,
                                    InvoiceNo = sequence.Prefix + sequence.LastSequence,
                                    AcadamicYearID = toDto.AcadamicYearID,
                                    ClassId = toDto.ClassId,
                                    SectionID = studdet.SectionID.Value,
                                    InvoiceDate = toDto.InvoiceDate,
                                    DueDate = toDto.DueDate,
                                    CreatedBy = toDto.CreatedBy,
                                    UpdatedBy = toDto.UpdatedBy,
                                    CreatedDate = toDto.CreatedDate,
                                    UpdatedDate = toDto.UpdatedDate,
                                    IsAccountPost = toDto.IsAccountPost,

                                };
                                if (invGenStudents.Contains(studdet.StudentIID) == false)
                                {
                                    invGenStudents.Add(studdet.StudentIID);
                                }

                                entityCreate.FeeDueFeeTypeMaps.Add(mapData);
                            }
                        }

                        foreach (var selectedFeePeriod in toDto.FeePeriod)
                        {
                            feePerioId = int.Parse(selectedFeePeriod.Key);
                            feeStructureId = new FeeDataRepository().GetFeeStructureID(toDto.ClassId, studdet.StudentIID, toDto.AcadamicYearID.Value);

                            var queryFeeType = dbContext.FeeStructureFeeMaps
                                .Where(s => feeStructureId.Contains(s.FeeStructureID) && s.FeePeriodID == feePerioId &&
                                ((s.FeePeriod.PeriodFrom.Date >= feeStartDate.Value.Date ||
                                (s.FeePeriod.PeriodFrom.Date <= feeStartDate.Value.Date && s.FeePeriod.PeriodTo.Date >= feeStartDate.Value.Date)) &&
                                (((s.FeePeriod.PeriodTo.Date <= toDto.DueDate.Value.Date) ||
                                (s.FeePeriod.PeriodFrom.Date <= toDto.DueDate.Value.Date && s.FeePeriod.PeriodTo.Date >= toDto.DueDate.Value.Date)))))
                                .Include(x => x.FeeMaster)
                                .Include(y => y.FeeStructureMontlySplitMaps)
                                .AsNoTracking().ToList();

                            if (queryFeeType.Count() > 0)
                            {
                                foreach (var feetypedet in queryFeeType)
                                {
                                    transportFee = 0;
                                    totalTransportFee = 0;

                                    var querydue = (!dbContext.FeeDueFeeTypeMaps.AsNoTracking().Any(s => s.FeeStructureFeeMapID == feetypedet.FeeStructureFeeMapIID && s.FeeMasterID == feetypedet.FeeMasterID && s.FeePeriodID == feetypedet.FeePeriodID && s.StudentFeeDue.StudentId == studdet.StudentIID));

                                    if (querydue == true)
                                    {
                                        if (invGenStudents.Contains(studdet.StudentIID) == false)
                                        {
                                            entityCreate = new StudentFeeDue()
                                            {
                                                //StudentFeeDueIID = toDto.StudentFeeDueIID,
                                                StudentId = studdet.StudentIID,
                                                //InvoiceNo = sequence.Prefix + sequence.LastSequence,
                                                AcadamicYearID = toDto.AcadamicYearID,
                                                ClassId = toDto.ClassId,
                                                SectionID = studdet.SectionID.Value,
                                                InvoiceDate = toDto.InvoiceDate,
                                                DueDate = toDto.DueDate,
                                                CreatedBy = toDto.CreatedBy,
                                                UpdatedBy = toDto.UpdatedBy,
                                                CreatedDate = toDto.CreatedDate,
                                                UpdatedDate = toDto.UpdatedDate,
                                                IsAccountPost = toDto.IsAccountPost,

                                            };
                                            entityCreate.FeeDueFeeTypeMaps = new List<FeeDueFeeTypeMap>();
                                        }
                                        if (feetypedet.FeeMaster.FeeTypeID == settingsTransId)
                                        {
                                            List<FeeDueMonthlySplitDTO> TransportPeriod = CheckExistTransportFee(feetypedet, studdet.StudentIID);

                                            transportFee = TransportPeriod.Select(x => x.Amount).LastOrDefault();

                                            if (TransportPeriod.Count > 0)
                                            {
                                                var monthlySplit = new List<FeeDueMonthlySplit>();

                                                foreach (var feeMasterMonthlyDto in feetypedet.FeeStructureMontlySplitMaps)
                                                {
                                                    if (TransportPeriod.Where(x => x.MonthID == feeMasterMonthlyDto.MonthID.Value &&
                                                    x.Year == feeMasterMonthlyDto.Year.Value).Count() > 0 && ((feeStartDate.Value.Month <=
                                                    ((feeStartDate.Value.Year < feeMasterMonthlyDto.Year.Value ?
                                                    feeMasterMonthlyDto.MonthID.Value + 12 : feeMasterMonthlyDto.MonthID.Value))
                                                    && feeStartDate.Value.Year <= feeMasterMonthlyDto.Year.Value)))
                                                        transportFeeAmount = transportFee;
                                                    else
                                                        transportFeeAmount = 0;

                                                    totalTransportFee = totalTransportFee + transportFeeAmount;
                                                    if (transportFeeAmount > 0)
                                                    {
                                                        var entityChild = new FeeDueMonthlySplit()
                                                        {
                                                            FeeStructureMontlySplitMapID = feeMasterMonthlyDto.FeeStructureMontlySplitMapIID,
                                                            MonthID = byte.Parse(feeMasterMonthlyDto.MonthID.ToString()),
                                                            FeePeriodID = feetypedet.FeePeriodID.HasValue ? feetypedet.FeePeriodID : (int?)null,
                                                            Year = feeMasterMonthlyDto.Year.HasValue ? feeMasterMonthlyDto.Year : (int?)null,
                                                            Amount = transportFeeAmount,
                                                            TaxAmount = feeMasterMonthlyDto.TaxAmount.HasValue ? feeMasterMonthlyDto.TaxAmount : (decimal?)null,
                                                            TaxPercentage = feeMasterMonthlyDto.TaxPercentage.HasValue ? feeMasterMonthlyDto.TaxPercentage : (decimal?)null
                                                        };

                                                        monthlySplit.Add(entityChild);

                                                    }
                                                }
                                                if (totalTransportFee != 0)
                                                {
                                                    if (invGenStudents.Contains(studdet.StudentIID) == false)
                                                    {
                                                        invGenStudents.Add(studdet.StudentIID);
                                                        try
                                                        {
                                                            sequence = mutualRepository.GetNextSequence("InvoiceNo", null);
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            return "0#Please generate sequence with 'InvoiceNo'!";
                                                        }
                                                        entityCreate.InvoiceNo = sequence.Prefix + sequence.LastSequence;

                                                    }
                                                    entityCreate.FeeDueFeeTypeMaps.Add(new FeeDueFeeTypeMap()
                                                    {
                                                        FeeStructureFeeMapID = feetypedet.FeeStructureFeeMapIID,
                                                        StudentFeeDueID = entityCreate.StudentFeeDueIID,
                                                        FeeMasterID = feetypedet.FeeMasterID,
                                                        FeePeriodID = feetypedet.FeePeriodID,
                                                        Amount = totalTransportFee,
                                                        CollectedAmount = 0,
                                                        FeeDueMonthlySplits = monthlySplit,
                                                        CreatedBy = (int)_context.LoginID,
                                                        CreatedDate = DateTime.Now,
                                                    });
                                                }
                                            }
                                        }
                                        else
                                        {
                                            decimal splitAmount = 0;
                                            decimal totalAmount = 0;
                                            var monthlySplit = new List<FeeDueMonthlySplit>();

                                            foreach (var feeMasterMonthlyDto in feetypedet.FeeStructureMontlySplitMaps)
                                            {
                                                if ((feeStartDate.Value.Month <= ((feeStartDate.Value.Year < feeMasterMonthlyDto.Year.Value ?
                                                feeMasterMonthlyDto.MonthID.Value + 12 : feeMasterMonthlyDto.MonthID.Value))
                                                && feeStartDate.Value.Year <= feeMasterMonthlyDto.Year.Value))
                                                {
                                                    splitAmount = feeMasterMonthlyDto.Amount.Value;
                                                }
                                                else
                                                {
                                                    splitAmount = 0;
                                                }
                                                totalAmount = totalAmount + splitAmount;
                                                if (splitAmount > 0)
                                                {
                                                    var entityChild = new FeeDueMonthlySplit()
                                                    {
                                                        FeeStructureMontlySplitMapID = feeMasterMonthlyDto.FeeStructureMontlySplitMapIID,
                                                        MonthID = byte.Parse(feeMasterMonthlyDto.MonthID.ToString()),
                                                        FeePeriodID = feetypedet.FeePeriodID,
                                                        Year = feeMasterMonthlyDto.Year.HasValue ? feeMasterMonthlyDto.Year : (int?)null,
                                                        Amount = splitAmount,
                                                        TaxAmount = feeMasterMonthlyDto.TaxAmount.HasValue ? feeMasterMonthlyDto.TaxAmount : (decimal?)null,
                                                        TaxPercentage = feeMasterMonthlyDto.TaxPercentage.HasValue ? feeMasterMonthlyDto.TaxPercentage : (decimal?)null
                                                    };

                                                    monthlySplit.Add(entityChild);
                                                }
                                            }

                                            if (((feetypedet.FeeStructureMontlySplitMaps.Count > 0) && totalAmount != 0) ||
                                                ((feetypedet.FeeStructureMontlySplitMaps.Count == 0) && feetypedet.Amount != 0))
                                            {
                                                if (invGenStudents.Contains(studdet.StudentIID) == false)
                                                {
                                                    invGenStudents.Add(studdet.StudentIID);
                                                    try
                                                    {
                                                        sequence = mutualRepository.GetNextSequence("InvoiceNo", null);
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        return "0#Please generate sequence with 'InvoiceNo'!";
                                                    }

                                                    entityCreate.InvoiceNo = sequence.Prefix + sequence.LastSequence;
                                                }
                                                entityCreate.FeeDueFeeTypeMaps.Add(new FeeDueFeeTypeMap()
                                                {
                                                    FeeStructureFeeMapID = feetypedet.FeeStructureFeeMapIID,
                                                    FeeMasterID = feetypedet.FeeMasterID,
                                                    FeePeriodID = feetypedet.FeePeriodID,
                                                    Amount = totalAmount == 0 ? feetypedet.Amount : totalAmount,
                                                    CollectedAmount = 0,
                                                    FeeDueMonthlySplits = monthlySplit,
                                                    CreatedBy = (int)_context.LoginID,
                                                    CreatedDate = DateTime.Now,
                                                });
                                            }
                                        }

                                    }

                                }
                            }
                        }

                        var queryFine = (from st in dbContext.FineMasterStudentMaps
                               .Where(s => s.StudentId == studdet.StudentIID &&
                               !dbContext.FeeDueFeeTypeMaps.Any(sp => sp.FineMasterStudentMapID == s.FineMasterStudentMapIID))
                                         select st).AsNoTracking();

                        if (queryFine.Count() > 0)
                        {
                            if (invGenStudents.Contains(studdet.StudentIID) == false)
                            {
                                invGenStudents.Add(studdet.StudentIID);

                                try
                                {
                                    sequence = mutualRepository.GetNextSequence("InvoiceNo", null);
                                }
                                catch (Exception ex)
                                {
                                    return "0#Please generate sequence with 'InvoiceNo'!";
                                }
                                entityCreate = new StudentFeeDue()
                                {
                                    StudentId = studdet.StudentIID,
                                    InvoiceNo = sequence.Prefix + sequence.LastSequence,
                                    AcadamicYearID = toDto.AcadamicYearID,
                                    ClassId = toDto.ClassId,
                                    SectionID = studdet.SectionID.Value,
                                    InvoiceDate = toDto.InvoiceDate,
                                    DueDate = toDto.DueDate,
                                    CreatedBy = toDto.CreatedBy,
                                    UpdatedBy = toDto.UpdatedBy,
                                    CreatedDate = toDto.CreatedDate,
                                    UpdatedDate = toDto.UpdatedDate,
                                    IsAccountPost = toDto.IsAccountPost,

                                };
                                entityCreate.FeeDueFeeTypeMaps = new List<FeeDueFeeTypeMap>();
                            }
                            foreach (var queryFineDet in queryFine)
                            {
                                var monthlySplit = new List<FeeDueMonthlySplit>();
                                entityCreate.FeeDueFeeTypeMaps.Add(new FeeDueFeeTypeMap()
                                {
                                    FeeDueFeeTypeMapsIID = 0,
                                    StudentFeeDueID = entityCreate.StudentFeeDueIID,
                                    Amount = queryFineDet.Amount,
                                    CollectedAmount = 0,
                                    FineMasterID = queryFineDet.FineMasterID,
                                    FeeDueMonthlySplits = monthlySplit,
                                    FineMasterStudentMapID = queryFineDet.FineMasterStudentMapIID,
                                    CreatedBy = (int)_context.LoginID,
                                    CreatedDate = DateTime.Now,

                                });
                            }

                        }

                        if (invGenStudents.Count > 0 && entityCreate.FeeDueFeeTypeMaps.Count() > 0)
                        {
                            entityFeeDue.Add(entityCreate);
                        }

                    }

                    dbContext.StudentFeeDues.AddRange(entityFeeDue);
                    dbContext.SaveChanges();

                    if (toDto.IsAccountPost == true)
                    {
                        AccountPostingForGroup(entityFeeDue, studentAccountsId, toDto.ClassId.Value, toDto.DocumentTypeID, toDto.CostCenterID);
                    }
                }
                else
                {
                    return "0#No student is available for assigning fee!";
                }

            }
            if (invGenStudents.Count == 0)
            {
                return "0#There is no new fee invoice generated for student(s)!";
            }
            else
            {
                return "1#Fee invoice generated successfully for " + Convert.ToString(invGenStudents.Count) + " Student(s).";
            }
        }

        private string StudentFeePeriodwiseMap(StudentFeeDueDTO toDto, int settingsTransId, long studentAccountsId)
        {
            int feePerioId = 0;
            long studentId = 0;
            List<long?> feeStructureId = new List<long?>();
            decimal? transportFee = 0, transportFeeAmount = 0;
            decimal? totalTransportFee = 0;
            var entityCreate = new StudentFeeDue();
            List<long> invGenStudents = new List<long>();
            MutualRepository mutualRepository = new MutualRepository();
            List<FeeDueFeeTypeMapDTO> feeDueFeeTypeMap = new List<FeeDueFeeTypeMapDTO>();
            List<StudentFeeDue> entityFeeDue = new List<StudentFeeDue>();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                Entity.Models.Settings.Sequence sequence = new Entity.Models.Settings.Sequence();

                var studentIDs = toDto.Student.Select(x => long.Parse(x.Key));
                var studentDet = dbContext.Students.Where(x => studentIDs.Contains(x.StudentIID) && x.FeeStartDate == null).AsNoTracking().FirstOrDefault();
                if (studentDet != null)
                {
                    var msg = "0#Fee Start Date not set for the student '" + studentDet.FirstName + " " + studentDet.MiddleName + " " + studentDet.LastName + " ' !";
                    return msg;
                }

                var students = dbContext.Students.Where(x => studentIDs.Contains(x.StudentIID)).AsNoTracking().ToList();

                foreach (var studdet in students)
                {
                    studentId = studdet.StudentIID;
                    DateTime? feeStartDate = studdet.FeeStartDate;

                    FeeDueFeeTypeMap mapData = GetTranferFee(toDto, studentId);
                    if (mapData.IsNotNull() && mapData.Amount.HasValue)
                    {
                        var queryFeeExist = (!dbContext.FeeDueFeeTypeMaps.AsNoTracking().Any(s => s.FeeMasterID == mapData.FeeMasterID &&
                      (s.FeePeriodID == null || s.FeePeriodID == mapData.FeePeriodID) && s.StudentFeeDue.StudentId == studentId));
                        if (queryFeeExist == true)
                        {
                            try
                            {
                                sequence = mutualRepository.GetNextSequence("InvoiceNo", null);
                            }
                            catch (Exception ex)
                            {
                                return "0#Please generate sequence with 'InvoiceNo'!";
                            }
                            entityCreate = new StudentFeeDue()
                            {
                                //StudentFeeDueIID = toDto.StudentFeeDueIID,
                                StudentId = studentId,
                                InvoiceNo = sequence.Prefix + sequence.LastSequence,
                                AcadamicYearID = toDto.AcadamicYearID,
                                ClassId = toDto.ClassId,
                                SectionID = studdet.SectionID.Value,
                                InvoiceDate = toDto.InvoiceDate,
                                DueDate = toDto.DueDate,
                                CreatedBy = toDto.CreatedBy,
                                UpdatedBy = toDto.UpdatedBy,
                                CreatedDate = toDto.CreatedDate,
                                UpdatedDate = toDto.UpdatedDate,
                                IsAccountPost = toDto.IsAccountPost,

                            };

                            if (invGenStudents.Contains(studentId) == false)
                            {
                                invGenStudents.Add(studentId);
                            }
                            entityCreate.FeeDueFeeTypeMaps.Add(mapData);
                        }
                    }

                    foreach (var selectedFeePeriod in toDto.FeePeriod)
                    {
                        feePerioId = int.Parse(selectedFeePeriod.Key);
                        feeStructureId = new FeeDataRepository().GetFeeStructureID(toDto.ClassId, studdet.StudentIID, toDto.AcadamicYearID.Value);
                        var queryFeeType = dbContext.FeeStructureFeeMaps
                            .Where(s => feeStructureId.Contains(s.FeeStructureID) && s.FeePeriodID == feePerioId &&
                            ((s.FeePeriod.PeriodFrom.Date >= feeStartDate.Value.Date ||
                            (s.FeePeriod.PeriodFrom.Date <= feeStartDate.Value.Date && s.FeePeriod.PeriodTo.Date >= feeStartDate.Value.Date))
                            && (((s.FeePeriod.PeriodTo.Date <= toDto.DueDate.Value.Date) || (s.FeePeriod.PeriodFrom.Date <= toDto.DueDate.Value.Date
                            && s.FeePeriod.PeriodTo.Date >= toDto.DueDate.Value.Date)))))
                            .Include(x => x.FeeMaster)
                            .Include(y => y.FeeStructureMontlySplitMaps)
                            .AsNoTracking().ToList();

                        if (queryFeeType.Count() > 0)
                        {
                            foreach (var feetypedet in queryFeeType)
                            {
                                transportFee = 0;
                                totalTransportFee = 0;
                                transportFeeAmount = 0;

                                var querydue = (!dbContext.FeeDueFeeTypeMaps.AsNoTracking().Any(s => s.FeeStructureFeeMapID == feetypedet.FeeStructureFeeMapIID && s.FeeMasterID == feetypedet.FeeMasterID && s.FeePeriodID == feetypedet.FeePeriodID && s.StudentFeeDue.StudentId == studdet.StudentIID));

                                if (querydue == true)
                                {
                                    if (invGenStudents.Contains(studentId) == false)
                                    {
                                        //invGenStudents.Add(studentId);
                                        entityCreate = new StudentFeeDue()
                                        {
                                            // StudentFeeDueIID = toDto.StudentFeeDueIID,
                                            StudentId = studentId,
                                            //  InvoiceNo = sequence.Prefix + sequence.LastSequence,
                                            AcadamicYearID = toDto.AcadamicYearID,
                                            ClassId = toDto.ClassId,
                                            SectionID = studdet.SectionID.Value,
                                            InvoiceDate = toDto.InvoiceDate,
                                            DueDate = toDto.DueDate,
                                            CreatedBy = toDto.CreatedBy,
                                            UpdatedBy = toDto.UpdatedBy,
                                            CreatedDate = toDto.CreatedDate,
                                            UpdatedDate = toDto.UpdatedDate,
                                            IsAccountPost = toDto.IsAccountPost,

                                        };
                                        entityCreate.FeeDueFeeTypeMaps = new List<FeeDueFeeTypeMap>();
                                    }
                                    if (feetypedet.FeeMaster.FeeTypeID == settingsTransId)
                                    {
                                        List<FeeDueMonthlySplitDTO> TransportPeriod = CheckExistTransportFee(feetypedet, studentId);

                                        transportFee = TransportPeriod.Select(x => x.Amount).LastOrDefault();

                                        if (TransportPeriod.Count > 0)
                                        {
                                            // transportFee = CheckExistFeeDiscount(feetypedet.FeeMasterID, feetypedet.FeePeriodID, studentId, transportFee);

                                            var monthlySplit = new List<FeeDueMonthlySplit>();

                                            foreach (var feeMasterMonthlyDto in feetypedet.FeeStructureMontlySplitMaps)
                                            {
                                                if (TransportPeriod.Where(x => x.MonthID == feeMasterMonthlyDto.MonthID.Value && x.Year == feeMasterMonthlyDto.Year.Value).Count() > 0 && ((feeStartDate.Value.Month <= ((feeStartDate.Value.Year < feeMasterMonthlyDto.Year.Value ?
                                                    feeMasterMonthlyDto.MonthID.Value + 12 : feeMasterMonthlyDto.MonthID.Value))
                                                    && feeStartDate.Value.Year <= feeMasterMonthlyDto.Year.Value)))
                                                    transportFeeAmount = transportFee;
                                                else
                                                    transportFeeAmount = 0;

                                                totalTransportFee = totalTransportFee + transportFeeAmount;
                                                if (transportFeeAmount > 0)
                                                {
                                                    var entityChild = new FeeDueMonthlySplit()
                                                    {
                                                        FeeStructureMontlySplitMapID = feeMasterMonthlyDto.FeeStructureMontlySplitMapIID,
                                                        MonthID = byte.Parse(feeMasterMonthlyDto.MonthID.ToString()),
                                                        FeePeriodID = feetypedet.FeePeriodID.HasValue ? feetypedet.FeePeriodID : (int?)null,
                                                        Year = feeMasterMonthlyDto.Year.HasValue ? feeMasterMonthlyDto.Year : (int?)null,

                                                        Amount = transportFee,// feeMasterMonthlyDto.Amount.HasValue ? feeMasterMonthlyDto.Amount : (decimal?)null,
                                                        TaxAmount = feeMasterMonthlyDto.TaxAmount.HasValue ? feeMasterMonthlyDto.TaxAmount : (decimal?)null,
                                                        TaxPercentage = feeMasterMonthlyDto.TaxPercentage.HasValue ? feeMasterMonthlyDto.TaxPercentage : (decimal?)null
                                                    };

                                                    monthlySplit.Add(entityChild);
                                                }
                                            }
                                            if (totalTransportFee != 0)
                                            {
                                                if (invGenStudents.Contains(studentId) == false)
                                                {
                                                    invGenStudents.Add(studentId);
                                                    try
                                                    {
                                                        sequence = mutualRepository.GetNextSequence("InvoiceNo", null);
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        return "0#Please generate sequence with 'InvoiceNo'!";
                                                    }
                                                    entityCreate.InvoiceNo = sequence.Prefix + sequence.LastSequence;
                                                }
                                                entityCreate.FeeDueFeeTypeMaps.Add(new FeeDueFeeTypeMap()
                                                {
                                                    FeeStructureFeeMapID = feetypedet.FeeStructureFeeMapIID,
                                                    FeeMasterID = feetypedet.FeeMasterID,
                                                    FeePeriodID = feetypedet.FeePeriodID,
                                                    Amount = totalTransportFee,
                                                    CollectedAmount = 0,
                                                    FeeDueMonthlySplits = monthlySplit,
                                                    CreatedBy = (int)_context.LoginID,
                                                    CreatedDate = DateTime.Now,
                                                });
                                            }
                                        }
                                    }
                                    else
                                    {
                                        //applying discount
                                        // feetypedet.Amount = CheckExistFeeDiscount(feetypedet.FeeMasterID, feetypedet.FeePeriodID, studentId, feetypedet.Amount);

                                        decimal splitAmount = 0;
                                        decimal totalAmount = 0;
                                        var monthlySplit = new List<FeeDueMonthlySplit>();

                                        foreach (var feeMasterMonthlyDto in feetypedet.FeeStructureMontlySplitMaps)
                                        {
                                            if ((feeStartDate.Value.Month <= ((feeStartDate.Value.Year < feeMasterMonthlyDto.Year.Value ?
                                                feeMasterMonthlyDto.MonthID.Value + 12 : feeMasterMonthlyDto.MonthID.Value))
                                                && feeStartDate.Value.Year <= feeMasterMonthlyDto.Year.Value))
                                            {
                                                splitAmount = feeMasterMonthlyDto.Amount.Value;
                                            }
                                            else
                                            {
                                                splitAmount = 0;
                                            }
                                            totalAmount = totalAmount + splitAmount;
                                            if (splitAmount > 0)
                                            {
                                                var entityChild = new FeeDueMonthlySplit()
                                                {
                                                    FeeStructureMontlySplitMapID = feeMasterMonthlyDto.FeeStructureMontlySplitMapIID,
                                                    MonthID = byte.Parse(feeMasterMonthlyDto.MonthID.ToString()),
                                                    FeePeriodID = feetypedet.FeePeriodID,
                                                    Year = feeMasterMonthlyDto.Year.HasValue ? feeMasterMonthlyDto.Year : (int?)null,
                                                    Amount = splitAmount,
                                                    TaxAmount = feeMasterMonthlyDto.TaxAmount.HasValue ? feeMasterMonthlyDto.TaxAmount : (decimal?)null,
                                                    TaxPercentage = feeMasterMonthlyDto.TaxPercentage.HasValue ? feeMasterMonthlyDto.TaxPercentage : (decimal?)null
                                                };

                                                monthlySplit.Add(entityChild);
                                            }
                                        }
                                        if (((feetypedet.FeeStructureMontlySplitMaps.Count > 0) && totalAmount != 0) || ((feetypedet.FeeStructureMontlySplitMaps.Count == 0) && feetypedet.Amount != 0))
                                        {
                                            if (invGenStudents.Contains(studentId) == false)
                                            {
                                                invGenStudents.Add(studentId);
                                                try
                                                {
                                                    sequence = mutualRepository.GetNextSequence("InvoiceNo", null);
                                                }
                                                catch (Exception ex)
                                                {
                                                    return "0#Please generate sequence with 'InvoiceNo'!";
                                                }
                                                entityCreate.InvoiceNo = sequence.Prefix + sequence.LastSequence;
                                            }
                                            entityCreate.FeeDueFeeTypeMaps.Add(new FeeDueFeeTypeMap()
                                            {
                                                FeeStructureFeeMapID = feetypedet.FeeStructureFeeMapIID,
                                                FeeMasterID = feetypedet.FeeMasterID,
                                                FeePeriodID = feetypedet.FeePeriodID,
                                                Amount = totalAmount == 0 ? feetypedet.Amount : totalAmount,
                                                CollectedAmount = 0,
                                                FeeDueMonthlySplits = monthlySplit,
                                                CreatedBy = (int)_context.LoginID,
                                                CreatedDate = DateTime.Now,
                                            });

                                        }

                                    }
                                }
                                //else
                                //{
                                //    return "0#No fee data is found for assigning!";
                                //}
                            }
                        }

                    }
                    var queryFine = (from st in dbContext.FineMasterStudentMaps
                                      .Where(s => s.StudentId == studentId &&
                                      !dbContext.FeeDueFeeTypeMaps.Any(sp => sp.FineMasterStudentMapID == s.FineMasterStudentMapIID))
                                     select st).AsNoTracking().ToList();

                    if (queryFine.Count() > 0)
                    {
                        if (invGenStudents.Contains(studentId) == false)
                        {
                            invGenStudents.Add(studentId);

                            try
                            {
                                sequence = mutualRepository.GetNextSequence("InvoiceNo",null);
                            }
                            catch (Exception ex)
                            {
                                return "0#Please generate sequence with 'InvoiceNo'!";
                            }
                            entityCreate = new StudentFeeDue()
                            {
                                //StudentFeeDueIID = toDto.StudentFeeDueIID,
                                StudentId = studentId,
                                InvoiceNo = sequence.Prefix + sequence.LastSequence,
                                AcadamicYearID = toDto.AcadamicYearID,
                                ClassId = toDto.ClassId,
                                SectionID = studdet.SectionID.Value,
                                InvoiceDate = toDto.InvoiceDate,
                                DueDate = toDto.DueDate,
                                CreatedBy = toDto.CreatedBy,
                                UpdatedBy = toDto.UpdatedBy,
                                CreatedDate = toDto.CreatedDate,
                                UpdatedDate = toDto.UpdatedDate,
                                IsAccountPost = toDto.IsAccountPost,

                            };
                            entityCreate.FeeDueFeeTypeMaps = new List<FeeDueFeeTypeMap>();
                        }
                        foreach (var queryFineDet in queryFine)
                        {
                            var monthlySplit = new List<FeeDueMonthlySplit>();
                            entityCreate.FeeDueFeeTypeMaps.Add(new FeeDueFeeTypeMap()
                            {
                                FeeDueFeeTypeMapsIID = 0,
                                Amount = queryFineDet.Amount,
                                CollectedAmount = 0,
                                FineMasterID = queryFineDet.FineMasterID,
                                FeeDueMonthlySplits = monthlySplit,
                                FineMasterStudentMapID = queryFineDet.FineMasterStudentMapIID,
                                CreatedBy = (int)_context.LoginID,
                                CreatedDate = DateTime.Now,

                            });
                        }

                    }

                    if (invGenStudents.Count > 0 && entityCreate.FeeDueFeeTypeMaps.Count() > 0)
                    {
                        entityFeeDue.Add(entityCreate);
                    }
                }

                dbContext.StudentFeeDues.AddRange(entityFeeDue);
                dbContext.SaveChanges();
                if (toDto.IsAccountPost == true)
                {
                    AccountPostingForGroup(entityFeeDue, studentAccountsId, toDto.ClassId.Value, toDto.DocumentTypeID, toDto.CostCenterID);
                }
            }

            if (invGenStudents.Count == 0)
            {
                return "0#There is no new fee invoice generated for stundent(s)!";
            }
            else
            {
                return "1#Fee invoice generated successfully for " + Convert.ToString(invGenStudents.Count) + " Student(s).";
            }
        }

        #endregion

        public long AccountPosting(StudentFeeDue toDto, long studentAccountsId)
        {
            long accountTransactionHeadIID = 0;
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var documentTypeID = (from st in dbContext.DocumentTypes.AsNoTracking().Where(s => s.TransactionTypeName.ToUpper() == "FEE JOURNAL") select st.DocumentTypeID).FirstOrDefault();
                if (documentTypeID.IsNull())
                    throw new Exception("Document Type 'FEE JOURNAL' is not available.Please check");

                int? studentCostCenter = dbContext.Classes.Where(cls => cls.ClassID == toDto.ClassId).AsNoTracking().Select(cls => cls.CostCenterID).FirstOrDefault();

                var entity = new AccountTransactionHead();

                entity.CreatedBy = int.Parse(_context.LoginID.Value.ToString());
                entity.CreatedDate = DateTime.Now;
                entity.DocumentTypeID = documentTypeID;

                entity.UpdatedBy = int.Parse(_context.LoginID.Value.ToString());
                //entity.UpdatedDate = DateTime.Now;
                entity.AccountID = studentAccountsId;
                entity.CompanyID = _context.CompanyID;
                entity.ReceiptsID = toDto.StudentFeeDueIID;
                entity.CostCenterID = studentCostCenter;
                entity.TransactionStatusID = (byte)Eduegate.Framework.Enums.TransactionStatus.Complete;
                entity.DocumentStatusID = (long)Eduegate.Services.Contracts.Enums.DocumentStatuses.Completed;
                entity.TransactionDate = System.DateTime.Now;
                entity.TransactionNumber = new AccountEntryMapper().GetNextTransactionNumber(documentTypeID); ;

                entity.AccountTransactionDetails = new List<AccountTransactionDetail>();
                decimal total = toDto.FeeDueFeeTypeMaps.Select(x => x.Amount.Value).Sum();

                entity.AccountTransactionDetails = new List<AccountTransactionDetail>();
                foreach (var detailEntityItem in toDto.FeeDueFeeTypeMaps)
                {
                    if (detailEntityItem.FeeMasterID.HasValue)
                    {
                        long? feeAccountId = dbContext.FeeMasters.Where(fee => fee.FeeMasterID == detailEntityItem.FeeMasterID && detailEntityItem.FeeMasterID != null).AsNoTracking().Select(fee => fee.LedgerAccountID).FirstOrDefault();

                        var detailDTO = new AccountTransactionDetail();
                        detailDTO.ReferenceReceiptID = toDto.StudentFeeDueIID;
                        detailDTO.Amount = -(detailEntityItem.Amount);
                        detailDTO.UpdatedBy = detailEntityItem.UpdatedBy;
                        detailDTO.UpdatedDate = detailEntityItem.UpdatedDate;
                        detailDTO.AccountID = feeAccountId;
                        detailDTO.CostCenterID = studentCostCenter.HasValue ? studentCostCenter : (int?)null;
                        detailDTO.CreatedBy = detailEntityItem.CreatedBy;
                        detailDTO.CreatedDate = detailEntityItem.CreatedDate;
                        entity.AccountTransactionDetails.Add(detailDTO);
                    }
                }

                //entity.AccountTransactionDetails = new List<AccountTransactionDetail>();
                foreach (var detailEntityItem in toDto.FeeDueFeeTypeMaps)
                {
                    if (detailEntityItem.FineMasterID.HasValue)
                    {
                        long? feeAccountId = dbContext.FineMasters.Where(fee => fee.FineMasterID == detailEntityItem.FineMasterID && detailEntityItem.FineMasterID != null).AsNoTracking().Select(fee => fee.LedgerAccountID).FirstOrDefault();

                        var detailDTO = new AccountTransactionDetail();
                        detailDTO.ReferenceReceiptID = toDto.StudentFeeDueIID;
                        detailDTO.Amount = -(detailEntityItem.Amount);
                        detailDTO.UpdatedBy = detailEntityItem.UpdatedBy;
                        detailDTO.UpdatedDate = detailEntityItem.UpdatedDate;
                        detailDTO.AccountID = feeAccountId;
                        detailDTO.CostCenterID = studentCostCenter.HasValue ? studentCostCenter : (int?)null;
                        detailDTO.CreatedBy = detailEntityItem.CreatedBy;
                        detailDTO.CreatedDate = detailEntityItem.CreatedDate;
                        entity.AccountTransactionDetails.Add(detailDTO);
                    }
                }


                var studentDetailDTO = new AccountTransactionDetail();
                studentDetailDTO.Amount = (total);
                //studentDetailDTO.UpdatedBy = int.Parse(_context.LoginID.Value.ToString());
                studentDetailDTO.AccountID = studentAccountsId;
                studentDetailDTO.CreatedBy = int.Parse(_context.LoginID.Value.ToString());
                studentDetailDTO.CreatedDate = DateTime.Now;
                studentDetailDTO.ReferenceReceiptID = toDto.StudentFeeDueIID;
                studentDetailDTO.CostCenterID = studentCostCenter.HasValue ? studentCostCenter : (int?)null;
                entity.AccountTransactionDetails.Add(studentDetailDTO);
                entity.AmountPaid = (total);

                dbContext.AccountTransactionHeads.Add(entity);
                dbContext.SaveChanges();

                accountTransactionHeadIID = entity.AccountTransactionHeadIID;
            }

            int loginID = Convert.ToInt32(_context.LoginID);
            new AccountEntryMapper().AccountTransactionSync(accountTransactionHeadIID, toDto.StudentFeeDueIID, loginID, 0);
            return accountTransactionHeadIID;
        }

        public void AccountPostingForGroup(List<StudentFeeDue> feeDueData, long studentAccountsId, int classID, int documentTypeID, int? costCenterID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                List<AccountTransactionHead> entityGrp = new List<AccountTransactionHead>();

                var feeMasterIDs = new List<int?>();
                feeDueData.ForEach(x => feeMasterIDs.AddRange(x.FeeDueFeeTypeMaps.Select(y => y.FeeMasterID)));
                feeMasterIDs = feeMasterIDs.Distinct().ToList();

                var allFeeData = dbContext.FeeMasters.Where(fee => feeMasterIDs.Contains(fee.FeeMasterID)).AsNoTracking().ToList();

                var fineMasterIDs = new List<int?>();
                feeDueData.ForEach(x => fineMasterIDs.AddRange(x.FeeDueFeeTypeMaps.Select(y => y.FineMasterID)));
                fineMasterIDs = fineMasterIDs.Distinct().ToList();

                var allFineData = dbContext.FineMasters.Where(fee => fineMasterIDs.Contains(fee.FineMasterID)).AsNoTracking().ToList();

                foreach (StudentFeeDue feeDueDetails in feeDueData)
                {
                    var entity = new AccountTransactionHead();

                    entity.CreatedBy = int.Parse(_context.LoginID.Value.ToString());
                    entity.CreatedDate = DateTime.Now;
                    entity.DocumentTypeID = documentTypeID;
                    entity.UpdatedBy = int.Parse(_context.LoginID.Value.ToString());
                    entity.AccountID = studentAccountsId;
                    entity.CompanyID = _context.CompanyID;
                    entity.ReceiptsID = feeDueDetails.StudentFeeDueIID;
                    entity.CostCenterID = costCenterID;
                    entity.TransactionStatusID = (byte)Eduegate.Framework.Enums.TransactionStatus.Complete;
                    entity.DocumentStatusID = (long)Eduegate.Services.Contracts.Enums.DocumentStatuses.Completed;
                    entity.TransactionDate = System.DateTime.Now;
                    entity.TransactionNumber = new AccountEntryMapper().GetNextTransactionNumber(documentTypeID); ;

                    entity.AccountTransactionDetails = new List<AccountTransactionDetail>();
                    decimal total = feeDueDetails.FeeDueFeeTypeMaps.Select(x => x.Amount.Value).Sum();

                    entity.AccountTransactionDetails = new List<AccountTransactionDetail>();
                    foreach (var detailEntityItem in feeDueDetails.FeeDueFeeTypeMaps)
                    {
                        if (detailEntityItem.FeeMasterID.HasValue)
                        {
                            long? feeAccountId = allFeeData.FirstOrDefault(fee => fee.FeeMasterID == detailEntityItem.FeeMasterID).LedgerAccountID;

                            var detailDTO = new AccountTransactionDetail();
                            detailDTO.ReferenceReceiptID = feeDueDetails.StudentFeeDueIID;
                            detailDTO.Amount = -(detailEntityItem.Amount);
                            detailDTO.UpdatedBy = detailEntityItem.UpdatedBy;
                            detailDTO.UpdatedDate = detailEntityItem.UpdatedDate;
                            detailDTO.AccountID = feeAccountId;
                            detailDTO.CostCenterID = costCenterID.HasValue ? costCenterID : (int?)null;
                            detailDTO.CreatedBy = detailEntityItem.CreatedBy;
                            detailDTO.CreatedDate = detailEntityItem.CreatedDate;
                            entity.AccountTransactionDetails.Add(detailDTO);
                        }
                    }

                    //entity.AccountTransactionDetails = new List<AccountTransactionDetail>();
                    foreach (var detailEntityItem in feeDueDetails.FeeDueFeeTypeMaps)
                    {
                        if (detailEntityItem.FineMasterID.HasValue)
                        {
                            long? feeAccountId = allFineData.FirstOrDefault(fin => fin.FineMasterID == detailEntityItem.FineMasterID).LedgerAccountID;

                            var detailDTO = new AccountTransactionDetail();
                            detailDTO.ReferenceReceiptID = feeDueDetails.StudentFeeDueIID;
                            detailDTO.Amount = -(detailEntityItem.Amount);
                            detailDTO.UpdatedBy = detailEntityItem.UpdatedBy;
                            detailDTO.UpdatedDate = detailEntityItem.UpdatedDate;
                            detailDTO.AccountID = feeAccountId;
                            detailDTO.CostCenterID = costCenterID.HasValue ? costCenterID : (int?)null;
                            detailDTO.CreatedBy = detailEntityItem.CreatedBy;
                            detailDTO.CreatedDate = detailEntityItem.CreatedDate;
                            entity.AccountTransactionDetails.Add(detailDTO);
                        }
                    }

                    var accountTranDet = new AccountTransactionDetail();
                    accountTranDet.Amount = (total);
                    accountTranDet.AccountID = studentAccountsId;
                    accountTranDet.CreatedBy = int.Parse(_context.LoginID.Value.ToString());
                    accountTranDet.CreatedDate = DateTime.Now;
                    accountTranDet.ReferenceReceiptID = feeDueDetails.StudentFeeDueIID;
                    accountTranDet.CostCenterID = costCenterID.HasValue ? costCenterID : (int?)null;

                    entity.AccountTransactionDetails.Add(accountTranDet);

                    entity.AmountPaid = (total);

                    entityGrp.Add(entity);
                }

                dbContext.AccountTransactionHeads.AddRange(entityGrp);
                dbContext.SaveChanges();

                entityGrp.All(eg =>
                {
                    var entityFeeType = dbContext.FeeDueFeeTypeMaps.Where(x => x.StudentFeeDueID == eg.ReceiptsID).AsNoTracking().ToList();
                    entityFeeType.All(w =>
                    {
                        w.AccountTransactionHeadID = eg.AccountTransactionHeadIID;

                        dbContext.Entry(w).State = EntityState.Modified;
                        dbContext.SaveChanges();

                        GetFeeMonthlyUpdate(dbContext, eg.AccountTransactionHeadIID, w.FeeDueFeeTypeMapsIID).All(x =>
                        {
                            dbContext.Entry(x).State = EntityState.Modified;
                            dbContext.SaveChanges();

                            return true;
                        });
                        return true;
                    });
                    new AccountEntryMapper().AccountTransactionSync(eg.AccountTransactionHeadIID, eg.ReceiptsID.Value, int.Parse(_context.LoginID.Value.ToString()), 0);
                    return true;
                });

            }
        }

        public void UpdateAccountTransactionId(List<FeeDueFeeTypeMapDTO> feeDueFeeTypeMap)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                foreach (FeeDueFeeTypeMapDTO feeTypeDTO in feeDueFeeTypeMap)
                {
                    var entityFeeType = dbContext.FeeDueFeeTypeMaps.Where(x => x.StudentFeeDueID == feeTypeDTO.StudentFeeDueID).AsNoTracking().ToList();
                    foreach (var feeType in entityFeeType)
                    {
                        var entityFeeTypeDet = dbContext.FeeDueFeeTypeMaps.Where(x => x.FeeDueFeeTypeMapsIID == feeType.FeeDueFeeTypeMapsIID).AsNoTracking().FirstOrDefault();
                        entityFeeTypeDet.AccountTransactionHeadID = feeTypeDTO.AccountTransactionHeadID;

                        dbContext.Entry(entityFeeTypeDet).State = EntityState.Modified;
                        dbContext.SaveChanges();

                        var entityDueFeeMonthly = dbContext.FeeDueMonthlySplits.Where(x => x.FeeDueFeeTypeMapsID == feeType.FeeDueFeeTypeMapsIID).AsNoTracking().ToList();
                        if (entityDueFeeMonthly.Count() > 0)
                        {
                            foreach (var feeMonthly in entityDueFeeMonthly)
                            {
                                feeMonthly.AccountTransactionHeadID = feeTypeDTO.AccountTransactionHeadID;

                                dbContext.Entry(feeMonthly).State = EntityState.Modified;
                                dbContext.SaveChanges();
                            }
                        }
                    }
                }
            }
        }

        public string GetNextTransactionNumberByMonthYear(TransactionNumberDTO dto, bool isSave = false)
        {
            int documentTypeID = dto.DocumentTypeID;
            int month = dto.Month;
            int year = dto.Year;
            var parameters = new List<KeyValueParameterDTO>();

            if (!string.IsNullOrEmpty(dto.PaymentMode))
            {
                parameters.Add(new KeyValueParameterDTO { ParameterName = "PaymentMode", ParameterValue = dto.PaymentMode });
            }

            parameters.Add(new KeyValueParameterDTO { ParameterName = "Month", ParameterValue = dto.Month.ToString() });
            parameters.Add(new KeyValueParameterDTO { ParameterName = "Year", ParameterValue = dto.Year.ToString() });

            string nextTransactionNumber = string.Empty;
            var metadataRepository = new MetadataRepository();
            var DocumentTypeEntity = isSave ? metadataRepository.SaveNextTransactionNumberByMonthYear(documentTypeID, month, year) : metadataRepository.GetNextTransactionNumberByMonthYear(documentTypeID, month, year);

            if (DocumentTypeEntity.IsNotNull())
            {
                string monthName = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);

                var documentTypeTransactionNumber = DocumentTypeEntity.DocumentTypeTransactionNumbers.Where(x => x.Month == month && x.Year == year).FirstOrDefault();

                //var transactionNo = default(string);
                nextTransactionNumber = DocumentTypeEntity.TransactionNoPrefix;
                //e.g.RV-{PaymentMode}-{Year}/{Month}
                if (parameters != null && parameters.Any())
                {
                    foreach (var item in parameters)
                    {
                        nextTransactionNumber = nextTransactionNumber.Replace("{" + item.ParameterName.Trim() + "}", item.ParameterValue);
                    }

                    //nextTransactionNumber = transactionNo.Trim() == string.Empty ? nextTransactionNumber : transactionNo.Trim();
                }

                nextTransactionNumber += documentTypeTransactionNumber.LastTransactionNo.IsNull() ? "1" : Convert.ToString(documentTypeTransactionNumber.LastTransactionNo);
                return nextTransactionNumber;
            }
            else return nextTransactionNumber;
        }


        public List<long?> GetFeeStructureID(int? classID, long? studentID, int acadamicYearID)
        {
            List<long?> feeStructureID = new List<long?>();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var packageStudentMaP = (from studMap in dbContext.PackageConfigStudentMaps.AsEnumerable()
                                         join pkgFee in dbContext.PackageConfigFeeStructureMaps on
                                         studMap.PackageConfigID equals pkgFee.PackageConfigID
                                         where studMap.StudentID == studentID && studMap.IsActive == true && pkgFee.FeeStructure.IsActive == true && studMap.PackageConfig.IsActive == true
                                         && studMap.PackageConfig.AcadamicYearID == acadamicYearID && pkgFee.FeeStructure.AcadamicYearID == acadamicYearID
                                         select pkgFee.FeeStructureID).ToList();

                if (packageStudentMaP.Count > 0)
                {
                    feeStructureID = packageStudentMaP;
                    return feeStructureID;
                }
                else
                {
                    var queryGrp = (from studGrp in dbContext.StudentGroupMaps.AsEnumerable()
                                    join pkggroupFee in dbContext.PackageConfigStudentGroupMaps on studGrp.StudentGroupID equals pkggroupFee.StudentGroupID
                                    join pkgFee in dbContext.PackageConfigFeeStructureMaps on
                                    pkggroupFee.PackageConfigID equals pkgFee.PackageConfigID
                                    where (pkggroupFee.IsActive == true && pkggroupFee.PackageConfig.IsActive == true && studGrp.IsActive == true &&
                                    studGrp.StudentID == studentID && pkgFee.IsActive == true
                                    && pkgFee.FeeStructure.IsActive == true
                                    && pkggroupFee.PackageConfig.AcadamicYearID == acadamicYearID && pkgFee.FeeStructure.AcadamicYearID == acadamicYearID)
                                    select pkgFee.FeeStructureID).ToList();

                    if (queryGrp.Count > 0)
                    {
                        feeStructureID = queryGrp;
                        return feeStructureID;
                    }
                    else
                    {
                        var packageClassMaP = (from clsMaps in dbContext.PackageConfigClassMaps.AsEnumerable()
                                               join pkgFee in dbContext.PackageConfigFeeStructureMaps on
                                               clsMaps.PackageConfigID equals pkgFee.PackageConfigID
                                               where clsMaps.ClassID == classID && clsMaps.IsActive == true && clsMaps.PackageConfig.IsActive == true
                                               && pkgFee.IsActive == true && pkgFee.FeeStructure.IsActive == true
                                                && clsMaps.PackageConfig.AcadamicYearID == acadamicYearID && pkgFee.FeeStructure.AcadamicYearID == acadamicYearID
                                               select pkgFee.FeeStructureID).ToList();

                        if (packageClassMaP.Count > 0)
                        {
                            feeStructureID = packageClassMaP;
                            return feeStructureID;
                        }
                        else
                        {
                            var classFeeMaP = (from clsMaps in dbContext.ClassFeeStructureMaps.AsEnumerable()
                                               where clsMaps.ClassID == classID && clsMaps.IsActive == true && clsMaps.FeeStructure.IsActive == true
                                                && clsMaps.AcadamicYearID == acadamicYearID && clsMaps.FeeStructure.AcadamicYearID == acadamicYearID
                                               select clsMaps.FeeStructureID).ToList();

                            if (classFeeMaP.Count > 0)
                            {
                                feeStructureID = classFeeMaP;
                                return feeStructureID;
                            }
                        }
                    }
                }
            }
            return feeStructureID;
        }

        public StudentFeeDueDTO GetGridLookUpsForSchoolCreditNote(long studentId)
        {
            var toDTO = new StudentFeeDueDTO();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

                var getData = dbContext.StudentFeeDues
                    .Where(x => x.StudentId == studentId && x.IsCancelled != true && x.CollectionStatus != true)
                    .Include(i => i.FeeDueFeeTypeMaps).ThenInclude(i => i.FeeMaster)
                    .Include(i => i.FeeDueFeeTypeMaps).ThenInclude(i => i.FeePeriod)
                    .AsNoTracking().ToList();

             
                foreach (var dat in getData)
                {
                    var feeDueFeeTyp = dat.FeeDueFeeTypeMaps.Where(x => x.Status != true).FirstOrDefault();

                    toDTO.FeeInvoiceList.Add(new KeyValueDTO
                    {
                        Key = dat.StudentFeeDueIID.ToString(),
                        Value = dat.InvoiceNo
                    });

                    if (feeDueFeeTyp?.FeeMasterID != null && !toDTO.FeeMaster.Any(d => d.Key == feeDueFeeTyp?.FeeMasterID.ToString()))
                    {
                        toDTO.FeeMaster.Add(new KeyValueDTO
                        {
                            Key = feeDueFeeTyp.FeeMasterID.ToString(),
                            Value = feeDueFeeTyp.FeeMaster.Description,
                        });
                    };

                    if (feeDueFeeTyp?.FeePeriodID != null && !toDTO.FeePeriod.Any(d => d.Key == feeDueFeeTyp?.FeePeriodID.ToString()))
                    {
                        toDTO.FeePeriod.Add(new KeyValueDTO
                        {
                            Key = feeDueFeeTyp.FeePeriodID.ToString(),
                            Value = feeDueFeeTyp.FeePeriod.Description + " (" + feeDueFeeTyp.FeePeriod.PeriodFrom.ToString(dateFormat, CultureInfo.InvariantCulture)
                            + " - " + feeDueFeeTyp.FeePeriod.PeriodTo.ToString(dateFormat, CultureInfo.InvariantCulture) + ")",
                        });
                    }
                }

                return toDTO;
            }
        }

        public StudentFeeDueDTO GetStudentFeeDueDetailsByID(long studentFeeDueID)
        {
            var _sRetData = new StudentFeeDueDTO();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var feeDueDetails = dbContext.StudentFeeDues
                            .Where(n => n.StudentFeeDueIID == studentFeeDueID)
                            .Include(w => w.FeeDueFeeTypeMaps)
                            .Include(i => i.Student).ThenInclude(i => i.Parent)
                            .AsNoTracking().FirstOrDefault();

                if (feeDueDetails != null)
                {
                    _sRetData = GetStudentFeeDue(dbContext, feeDueDetails);
                    _sRetData.ParentLoginID = feeDueDetails?.Student?.Parent?.LoginID;
                    _sRetData.ParentEmailID = feeDueDetails?.Student?.Parent?.GaurdianEmail;
                }
            }

            return _sRetData;
        }

    }
}