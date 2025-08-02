using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Mappers.Accounts;
using Eduegate.Domain.Mappers.Payment;
using Eduegate.Domain.Mappers.School.Accounts;
using Eduegate.Domain.Mappers.School.Students;
using Eduegate.Domain.Repository;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Extensions;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.School.Fees;
using Eduegate.Services.Contracts.School.Mutual;
using Eduegate.Services.Contracts.School.Students;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Domain.Mappers.School.Mutual;

namespace Eduegate.Domain.Mappers.School.Fees
{
    public class FeeCollectionMapper : DTOEntityDynamicMapper
    {
        public static FeeCollectionMapper Mapper(CallContext context)
        {
            var mapper = new FeeCollectionMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<FeeCollectionDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public List<StudentFeeDueDTO> FillFeeDues(long StudentID, string InvoiceNo)
        {
            List<StudentFeeDueDTO> feeDueDTO = new List<StudentFeeDueDTO>();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                feeDueDTO = (from stFee in dbContext.StudentFeeDues
                             join FeeTypeMap in dbContext.FeeDueFeeTypeMaps on stFee.StudentFeeDueIID equals FeeTypeMap.StudentFeeDueID
                             join FeeMonthlySplit in dbContext.FeeDueMonthlySplits on FeeTypeMap.FeeDueFeeTypeMapsIID equals FeeMonthlySplit.FeeDueFeeTypeMapsID into tmpMonthlySplit
                             from FeeMonthlySplit in tmpMonthlySplit.DefaultIfEmpty()
                             where (stFee.InvoiceNo == InvoiceNo && stFee.StudentId == StudentID && stFee.CollectionStatus == false)
                             orderby stFee.InvoiceDate ascending
                             group stFee by new { stFee.StudentFeeDueIID, stFee.StudentId, stFee.ClassId, stFee.InvoiceNo, stFee.InvoiceDate, stFee.FeeDueFeeTypeMaps } into stFeeGroup
                             select new StudentFeeDueDTO()
                             {
                                 StudentFeeDueIID = stFeeGroup.Key.StudentFeeDueIID,
                                 StudentId = stFeeGroup.Key.StudentId,
                                 ClassId = stFeeGroup.Key.ClassId,
                                 InvoiceNo = stFeeGroup.Key.InvoiceNo,
                                 InvoiceDate = stFeeGroup.Key.InvoiceDate,
                                 FeeDueFeeTypeMap = (from ft in stFeeGroup.Key.FeeDueFeeTypeMaps
                                                     where ft.Status == false
                                                     group ft by new { ft.Amount, ft.TaxAmount, ft.FeePeriodID, ft.TaxPercentage, ft.StudentFeeDueID, ft.FeeDueFeeTypeMapsIID, ft.FeeStructureFeeMapID, ft.FeeMaster, ft.FeePeriod, ft.FeeDueMonthlySplits } into ftGroup
                                                     select new FeeDueFeeTypeMapDTO()
                                                     {
                                                         Amount = ftGroup.Key.Amount,
                                                         TaxAmount = ftGroup.Key.TaxAmount,
                                                         InvoiceNo = stFeeGroup.Key.InvoiceNo,
                                                         FeePeriodID = ftGroup.Key.FeePeriodID,
                                                         TaxPercentage = ftGroup.Key.TaxPercentage,
                                                         StudentFeeDueID = ftGroup.Key.StudentFeeDueID,
                                                         FeeStructureFeeMapID = ftGroup.Key.FeeStructureFeeMapID,
                                                         //ClassFeeMasterID = ftGroup.Key.ClassFeeMasterID,
                                                         FeeCycleID = ftGroup.Key.FeeMaster.FeeCycleID,
                                                         //FeeMasterClassMapID = ftGroup.Key.FeeMasterClassMapID,
                                                         FeeDueFeeTypeMapsIID = ftGroup.Key.FeeDueFeeTypeMapsIID,
                                                         InvoiceDate = stFeeGroup.Key.InvoiceDate,
                                                         IsFeePeriodDisabled = ftGroup.Key.FeeMaster.FeeCycleID.HasValue ? ftGroup.Key.FeeMaster.FeeCycleID.Value != 3 : true,
                                                         FeeMaster = new KeyValueDTO() { Key = Convert.ToString(ftGroup.Key.FeeMaster.FeeMasterID), Value = ftGroup.Key.FeeMaster.Description },
                                                         FeePeriod = ftGroup.Key.FeePeriodID.HasValue ? new KeyValueDTO()
                                                         {
                                                             Key = ftGroup.Key.FeePeriodID.HasValue ? Convert.ToString(ftGroup.Key.FeePeriodID) : null,
                                                             Value = !ftGroup.Key.FeePeriodID.HasValue ? null : ftGroup.Key.FeePeriod.Description + " ( " + ftGroup.Key.FeePeriod.PeriodFrom.ToString("dd/MMM/yyy") + '-'
                                                                     + ftGroup.Key.FeePeriod.PeriodTo.ToString("dd/MMM/yyy") + " ) "
                                                         } : new KeyValueDTO(),

                                                         FeeDueMonthlySplit = (from mf in ftGroup.Key.FeeDueMonthlySplits
                                                                               where mf.Status == false
                                                                               select new FeeDueMonthlySplitDTO()
                                                                               {
                                                                                   FeeStructureMontlySplitMapID = mf.FeeStructureMontlySplitMapID.Value,
                                                                                   FeeDueMonthlySplitIID = mf.FeeDueMonthlySplitIID,
                                                                                   FeeDueFeeTypeMapsID = mf.FeeDueFeeTypeMapsID,
                                                                                   Year = mf.Year.Value,
                                                                                   MonthID = mf.MonthID != 0 ? int.Parse(Convert.ToString(mf.MonthID)) : 0,
                                                                                   Amount = mf.Amount.HasValue ? mf.Amount : (decimal?)null,
                                                                                   //TaxAmount = mf.TaxAmount.HasValue ? mf.TaxAmount : (decimal?)null,
                                                                                   //TaxPercentage = mf.TaxPercentage.HasValue ? mf.TaxPercentage : (decimal?)null
                                                                               }).ToList(),
                                                     }).ToList(),
                             }).AsNoTracking().ToList();
            }

            return feeDueDTO;
        }

        public override string GetEntity(long IID)
        {
            FeeCollectionDTO feeDueDTO = new FeeCollectionDTO();
            var studID = new long();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                studID = dbContext.StudentFeeDues.Where(a => a.StudentFeeDueIID == IID).FirstOrDefault().StudentId ?? 0;

                var studenFee = (from stFee in dbContext.Students
                                 where stFee.StudentIID == studID
                                 select stFee
                             ).Include(x => x.AcademicYear).Include(y => y.Class).Include(z => z.Section).AsNoTracking();

                feeDueDTO = studenFee.Select(stFee => new FeeCollectionDTO()
                {
                    FeeCollectionIID = 0,
                    ClassID = stFee.ClassID,
                    StudentID = stFee.StudentIID,
                    StudentName = stFee.AdmissionNumber + '-' + stFee.FirstName + ' ' + stFee.MiddleName + ' ' + stFee.LastName,
                    ClassName = stFee.Class.ClassDescription,
                    SectionID = stFee.SectionID,
                    SectionName = stFee.SectionID.HasValue ? stFee.Section.SectionName : null,
                    AdmissionNo = stFee.AdmissionNumber,
                    AcadamicYearID = stFee.AcademicYearID,
                    SchoolID = stFee.SchoolID,
                    AcademicYear = stFee.AcademicYearID.HasValue ? new Eduegate.Framework.Contracts.Common.KeyValueDTO()
                    {
                        Key = stFee.AcademicYearID.ToString(),
                        Value = stFee.AcademicYear.Description + '(' + stFee.AcademicYear.AcademicYearCode + ')'
                    } : null,
                    IsAutoFill = true

                }).AsNoTracking().FirstOrDefault();
            }
            feeDueDTO.FeeCollectionPaymentModeMapDTO = new List<FeeCollectionPaymentModeMapDTO>();
            feeDueDTO.FeeCollectionPreviousFeesDTO = new List<FeeCollectionPreviousFeesDTO>();
            feeDueDTO.FeeCollectionPendingInvoiceDTO = new List<FeeCollectionPendingInvoiceDTO>();
            feeDueDTO.FeeCollectionPendingInvoiceDTO = FillPendingFees(studID);
            // feeDueDTO.
            if (feeDueDTO.IsNull() == false)
            {
                string siblingDueDetails = string.Empty;
                var siblingInfolist = GetSiblingDueDetailsFromStudentID(studID);
                if (siblingInfolist.Count() > 0)
                {
                    foreach (var dueDet in siblingInfolist)
                    {
                        siblingDueDetails = string.Concat(siblingDueDetails, dueDet.AdmissionNo, "-", dueDet.StudentName, " Due=" + dueDet.Amount, "</br>");
                    }
                }

                else
                {
                    siblingDueDetails = "Not any Sibling available for selected Student";
                }

            }

            return ToDTOString(feeDueDTO);
        }

        public FeeCollectionDTO GetSiblingDueDetails(long? studentID)
        {
            var dtos = new FeeCollectionDTO();
            var studentApplicationDTO = new List<FeeCollectionDTO>();

            var studentSiblingFeeDueDetails = new List<FeeCollectionDTO>();
            string siblingDueDetails = null;

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var studentDet = dbContext.Students.Where(x => x.StudentIID == studentID).AsNoTracking().FirstOrDefault();
                var siblingDetails = dbContext.Students.Where(x => x.ParentID == studentDet.ParentID && x.StudentIID != studentID).AsNoTracking().ToList();

                foreach (Student siblimap in siblingDetails)
                {
                    var feeDueDet = dbContext.StudentFeeDues.Where(x => x.StudentId == siblimap.StudentIID && x.CollectionStatus == false).AsNoTracking().ToList();
                    var studGroup = feeDueDet.GroupBy(x => x.StudentFeeDueIID).ToList();

                    decimal dueTotal = 0;

                    foreach (var feeDue in studGroup)
                    {
                        var feeDueMap = dbContext.FeeDueFeeTypeMaps.Where(x => x.StudentFeeDueID == feeDue.Key && x.Status == false).AsNoTracking().ToList();
                        var totalDue = feeDueMap.Select(x => x.Amount.Value - x.CollectedAmount.Value).Sum();
                        dueTotal += totalDue;
                    }

                    studentApplicationDTO.Add(new FeeCollectionDTO()
                    {
                        AdmissionNo = siblimap.AdmissionNumber,
                        StudentName = siblimap.FirstName + " " + siblimap.MiddleName + " " + siblimap.LastName,
                        Amount = dueTotal,
                    });

                }

            }
            if (studentApplicationDTO.Count > 0)
            {
                foreach (var dueDet in studentApplicationDTO)
                {
                    var bk = new FeeCollectionDTO()
                    {
                        AdmissionNo = dueDet.AdmissionNo,
                        StudentName = dueDet.StudentName,
                        Amount = dueDet.Amount,
                    };

                    studentSiblingFeeDueDetails.Add(bk);
                    siblingDueDetails = string.Concat(siblingDueDetails, bk.AdmissionNo, "-", bk.StudentName, " Due=" + bk.Amount, "</br>");
                }
            }

            else
            {
                siblingDueDetails = "Not any Sibling available for selected Student";
            }
            dtos.SiblingFeeInfo = siblingDueDetails;
            return dtos;
        }
        public List<FeeCollectionPendingInvoiceDTO> FillPendingFees(long studentId)
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

                var detailDTO = feeDueDTO.ToList().Select(stFee => new FeeCollectionPendingInvoiceDTO()
                {
                    StudentFeeDueID = stFee.StudentFeeDueIID,
                    Amount = stFee.InvoiceAmount,
                    InvoiceNo = stFee.InvoiceNo,
                    InvoiceDate = stFee.InvoiceDate,
                    FeePeriodID = stFee.FeePeriodId,
                    FeeMasterID = stFee.FeeMasterID,
                    IsExternal = stFee.IsExternal,
                    CollAmount = FeeDueGenerationMapper.Mapper(_context).GetTotalCollectedAmount(dbContext, stFee.FeeMasterID, stFee.FeePeriodId, stFee.StudentFeeDueIID),
                    CrDrAmount = FeeDueGenerationMapper.Mapper(_context).GetTotalCreditNote(dbContext, studentId, stFee.FeeMasterID, stFee.FeePeriodId, stFee.StudentFeeDueIID),
                    Remarks = FeeDueGenerationMapper.Mapper(_context).GetFeeDueRemarks(dbContext, stFee.StudentFeeDueIID)
                });

                return detailDTO.ToList();
            }
        }


        public List<FeeCollectionDTO> GetSiblingDueDetailsFromStudentID(long studentID)
        {
            var studentApplicationDTO = new List<FeeCollectionDTO>();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var studentDet = dbContext.Students.Where(x => x.StudentIID == studentID).AsNoTracking().FirstOrDefault();
                var siblingDetails = dbContext.Students.Where(x => x.ParentID == studentDet.ParentID && x.IsActive == true && x.StudentIID != studentID)
                    .AsNoTracking()
                    .ToList();

                foreach (Student siblimap in siblingDetails)
                {
                    var feeDueDet = dbContext.StudentFeeDues.Where(x => x.StudentId == siblimap.StudentIID && x.CollectionStatus == false && x.IsCancelled != true).AsNoTracking().ToList();
                    var studGroup = feeDueDet.GroupBy(x => x.StudentFeeDueIID).ToList();

                    decimal dueTotal = 0.000M;

                    foreach (var feeDue in studGroup)
                    {
                        var feeDueMap = dbContext.FeeDueFeeTypeMaps.Where(x => x.StudentFeeDueID == feeDue.Key && x.Status == false)
                            .Include(i=> i.CreditNoteFeeTypeMaps)
                            .AsNoTracking().ToList();

                        var totalDue = feeDueMap.Select(x => x.Amount.Value - x.CollectedAmount.Value).Sum();

                        decimal totalCreditNote = 0;

                        foreach (var type in feeDueMap)
                        {
                            foreach (var creditNote in type.CreditNoteFeeTypeMaps)
                            {
                                totalCreditNote += Convert.ToDecimal(creditNote.Amount);
                            }
                        }

                        dueTotal += totalDue - totalCreditNote;
                    }

                    studentApplicationDTO.Add(new FeeCollectionDTO()
                    {
                        AdmissionNo = siblimap.AdmissionNumber,
                        StudentName = siblimap.FirstName + " " + siblimap.MiddleName + " " + siblimap.LastName,
                        Amount = dueTotal,
                    });

                }

            }
            return studentApplicationDTO;
        }

        #region Code Created By Sudish for Saving Fee Collection Details
        private FeeCollectionDTO SaveFeeCollection(FeeCollectionDTO _sFeeCollection)
        {
            #region Common Declarations
            Entity.Models.Settings.Sequence sequence = new Entity.Models.Settings.Sequence();
            MutualRepository mutualRepository = new MutualRepository();
            bool _sIsSuccess = false;
            List<long> _sLstFeeDue_IDs = new List<long>();
            List<long> _sLstFeeDueTypeMap_IDs = new List<long>();
            List<long> _sLstFeeDueMonthlyTypeMap_IDs = new List<long>();
            List<long> _sLstTransporteFeeDueMonthlyMap_IDs = new List<long>();
            List<long> _sLstCreditNoteTypeMap_IDs = new List<long>();
            List<long> _sLstCreditNote_IDs = new List<long>();
            List<long> _sLstFineTypeMap_IDs = new List<long>();

            #endregion

            #region Create New Sequence Number for Fee Collection
            try
            {
                sequence = mutualRepository.GetNextSequence("FeeReceiptNo", null);
            }
            catch (Exception ex)
            {
                throw new Exception("Please generate sequence with 'FeeReceiptNo'");
            }
            #endregion
            var _sRetData = new FeeCollection();
            #region Allocate Data to Fee Allocation
            try
            {
                //Update Fee Collection
                _sRetData = new FeeCollection()
                {
                    AcadamicYearID = _sFeeCollection.AcadamicYearID,
                    Amount = _sFeeCollection.Amount,
                    ClassFeeMasterId = _sFeeCollection.ClassFeeMasterId,
                    ClassID = _sFeeCollection.ClassID,
                    CashierID = _sFeeCollection.CashierID,
                    Remarks = _sFeeCollection.Remarks,
                    CollectionDate = _sFeeCollection.CollectionDate,
                    SchoolID = _sFeeCollection.SchoolID == null ? (byte)_context.SchoolID : _sFeeCollection.SchoolID,
                    CreatedBy = _sFeeCollection.FeeCollectionIID == 0 ? !_sFeeCollection.CreatedBy.HasValue ? _context == null ? (int?)null : (int?)_context.LoginID : _sFeeCollection.CreatedBy : _sFeeCollection.CreatedBy,
                    CreatedDate = _sFeeCollection.FeeCollectionIID == 0 ? DateTime.Now : _sFeeCollection.CreatedDate,
                    DiscountAmount = _sFeeCollection.DiscountAmount,
                    FeeCollectionIID = _sFeeCollection.FeeCollectionIID,
                    FeeCollectionStatusID = _sFeeCollection.FeeCollectionStatusID,
                    GroupTransactionNumber = _sFeeCollection.GroupTransactionNumber,
                    // Update Payment Modes
                    FeeCollectionPaymentModeMaps = !_sFeeCollection.FeeCollectionPaymentModeMapDTO.Any(w => (w.PaymentModeID ?? 0) > 0) ? new List<FeeCollectionPaymentModeMap>()
                                                    : (from n in _sFeeCollection.FeeCollectionPaymentModeMapDTO
                                                       where (n.PaymentModeID ?? 0) > 0
                                                       select new FeeCollectionPaymentModeMap()
                                                       {
                                                           Amount = n.Amount,
                                                           FeeCollectionID = n.FeeCollectionID,
                                                           Balance = n.Amount,
                                                           CreatedBy = _sFeeCollection.FeeCollectionIID == 0 ? !_sFeeCollection.CreatedBy.HasValue ? _context == null ? (int?)null : (int?)_context.LoginID : _sFeeCollection.CreatedBy : _sFeeCollection.CreatedBy,
                                                           CreatedDate = _sFeeCollection.FeeCollectionIID == 0 ? DateTime.Now : _sFeeCollection.CreatedDate,
                                                           FeeCollectionPaymentModeMapIID = n.FeeCollectionPaymentModeMapIID,
                                                           PaymentModeID = n.PaymentModeID,
                                                           BankID = n.BankID.HasValue ? n.BankID : null,
                                                           ReferenceNo = n.ReferenceNo,
                                                           TypeDate = n.TDate,
                                                           UpdatedBy = n.FeeCollectionPaymentModeMapIID > 0 ? _context == null ? (int?)null : (int?)_context.LoginID : _sFeeCollection.UpdatedBy,
                                                           UpdatedDate = n.FeeCollectionPaymentModeMapIID > 0 ? DateTime.Now : _sFeeCollection.UpdatedDate,
                                                       }
                                                    ).ToList(),

                    //Update Fee Type Maps
                    FeeCollectionFeeTypeMaps = GetFeeCollectionTyeMap(_sFeeCollection),
                    FeeReceiptNo = sequence.Prefix + sequence.LastSequence, // Update by Sequence Number
                    FineAmount = _sFeeCollection.FineAmount,
                    IsPaid = true,
                    PaidAmount = _sFeeCollection.PaidAmount,
                    SectionID = _sFeeCollection.SectionID,
                    StudentID = _sFeeCollection.StudentID,
                    PaymentTrackID = _sFeeCollection.PaymentTrackID,
                    UpdatedBy = _sFeeCollection.FeeCollectionIID > 0 ? _context == null ? (int?)null : (int)_context.LoginID : _sFeeCollection.UpdatedBy,
                    UpdatedDate = _sFeeCollection.FeeCollectionIID > 0 ? DateTime.Now : _sFeeCollection.UpdatedDate,
                };
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<FeeCollectionMapper>.Fatal(ex.Message, ex);
                throw ex;
            }
            #endregion

            using (var dbContext = new dbEduegateSchoolContext())
            {
                string defaultMail = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DEFAULT_MAIL_ID");

                int collectedStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<int>("FEECOLLECTIONSTATUSID_COLLECTED", 2);

                #region Insert or Update Fee Collection
                try
                {

                    dbContext.FeeCollections.Add(_sRetData);

                    // Insert Repositories
                    if (_sRetData.FeeCollectionIID == 0)
                    {
                        dbContext.Entry(_sRetData).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    }
                    // Update Repositories
                    else
                    {
                        // Payment Modes
                        if (_sRetData.FeeCollectionPaymentModeMaps.Any())
                        {
                            _sRetData.FeeCollectionPaymentModeMaps
                                     .All(w =>
                                     {
                                         dbContext.Entry(w).State = w.FeeCollectionPaymentModeMapIID == 0 ? Microsoft.EntityFrameworkCore.EntityState.Added : Microsoft.EntityFrameworkCore.EntityState.Modified;
                                         return true;
                                     });
                        }
                        // Fee Collection Types
                        if (_sRetData.FeeCollectionFeeTypeMaps.Any())
                        {
                            _sLstFeeDueTypeMap_IDs.AddRange(_sRetData.FeeCollectionFeeTypeMaps.Where(w => w.FeeDueFeeTypeMapsID != 0).Select(w => w.FeeDueFeeTypeMapsID ?? 0).Distinct());
                            _sRetData.FeeCollectionFeeTypeMaps
                                     .All(w =>
                                     {
                                         dbContext.Entry(w).State = w.FeeCollectionFeeTypeMapsIID == 0 ? Microsoft.EntityFrameworkCore.EntityState.Added : Microsoft.EntityFrameworkCore.EntityState.Modified;
                                         // Fee Monthly Splits

                                         if (w.FeeCollectionMonthlySplits.Any())
                                         {

                                             _sLstFeeDueMonthlyTypeMap_IDs.AddRange(w.FeeCollectionMonthlySplits.Where(x => x.FeeDueMonthlySplitID != 0 && x.Amount != 0 && x.Balance == 0).Select(x => x.FeeDueMonthlySplitID ?? 0).Distinct());
                                             w.FeeCollectionMonthlySplits
                                              .All(x =>
                                              {
                                                  dbContext.Entry(x).State = x.FeeCollectionMonthlySplitIID == 0 ? Microsoft.EntityFrameworkCore.EntityState.Added : Microsoft.EntityFrameworkCore.EntityState.Modified;
                                                  return true;
                                              });
                                         };
                                         return true;
                                     });
                        }
                        dbContext.Entry(_sRetData).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    }
                    dbContext.SaveChanges();

                    _sIsSuccess = true;
                }
                catch (Exception ex)
                {
                    _sIsSuccess = false;
                    Eduegate.Logger.LogHelper<FeeCollectionMapper>.Fatal(ex.Message, ex);
                    throw ex;
                }
                #endregion

                if (_sRetData.FeeCollectionStatusID == collectedStatusID)
                {
                    //Update status for fee due and credit notes
                    UpdateFeeDueAndCreditNotesStatus(_sIsSuccess, _sRetData, _sLstFeeDueTypeMap_IDs, _sLstFineTypeMap_IDs, _sLstFeeDueMonthlyTypeMap_IDs, _sLstTransporteFeeDueMonthlyMap_IDs, _sLstFeeDue_IDs, _sFeeCollection, _sLstCreditNoteTypeMap_IDs, _sLstCreditNote_IDs);

                    //Direct account posting
                    CollectionAutoAccountPosting(_sRetData);
                }

                var studentData = dbContext.Students.Where(x => x.StudentIID == _sRetData.StudentID)
                    .Include(i => i.Parent)
                    .Include(i => i.Class)
                    .Include(i => i.School)
                    .AsNoTracking().FirstOrDefault();

                _sFeeCollection = new FeeCollectionDTO();
                _sFeeCollection.FeeReceiptNo = _sRetData.FeeReceiptNo;
                _sFeeCollection.EmailID = studentData.Parent.GaurdianEmail ?? defaultMail;
                _sFeeCollection.AdmissionNo = studentData.AdmissionNumber;
                _sFeeCollection.FeeCollectionIID = _sRetData.FeeCollectionIID;
                _sFeeCollection.SchoolID = _sRetData.SchoolID;
                _sFeeCollection.FeeCollectionStatusID = _sRetData.FeeCollectionStatusID;
                _sFeeCollection.ClassID = studentData.ClassID;
                _sFeeCollection.StudentClassCode = studentData.Class?.Code;
                _sFeeCollection.StudentSchoolShortName = studentData.School?.SchoolShortName;
                _sFeeCollection.AcadamicYearID = _sRetData.AcadamicYearID;
            }

            return _sFeeCollection;
        }

        private List<FeeCollectionFeeTypeMap> GetFeeCollectionTyeMap(FeeCollectionDTO _sFeeCollection)
        {
            List<FeeCollectionFeeTypeMap> _sRetData = new List<FeeCollectionFeeTypeMap>();

            // Update Fee Types
            var dFeeTypes = from n in _sFeeCollection.FeeTypes
                            where (n.Amount ?? 0) != 0
                            select new FeeCollectionFeeTypeMap()
                            {
                                DueAmount = n.Amount ?? 0,
                                Amount = n.NowPaying,
                                Balance = n.Balance,//(n.Amount ?? 0) - (n.CreditNoteAmount ?? 0),
                                CreditNoteAmount = n.CreditNoteAmount ?? 0,
                                CreatedBy = n.FeeCollectionFeeTypeMapsIID == 0 ? !n.CreatedBy.HasValue ? _context == null ? (int?)null : (int?)_context.LoginID : n.CreatedBy : n.CreatedBy,
                                CreatedDate = n.FeeCollectionFeeTypeMapsIID == 0 ? DateTime.Now : n.CreatedDate,
                                FeeCollectionFeeTypeMapsIID = n.FeeCollectionFeeTypeMapsIID,
                                FeeCollectionID = n.FEECollectionID,
                                FeeCollectionMonthlySplits = !n.MontlySplitMaps.Any(w => (w.Amount ?? 0) != 0) ? new List<FeeCollectionMonthlySplit>()
                                                             : (from m in n.MontlySplitMaps
                                                                where (m.Amount ?? 0) != 0
                                                                select new FeeCollectionMonthlySplit()
                                                                {
                                                                    Amount = m.NowPaying ?? 0,
                                                                    DueAmount = m.Amount ?? 0,
                                                                    Balance = m.Balance,//(m.Amount ?? 0) - (m.CreditNoteAmount ?? 0),
                                                                    CreditNoteAmount = m.CreditNoteAmount ?? 0,
                                                                    FeeCollectionFeeTypeMapId = m.FeeCollectionFeeTypeMapId,
                                                                    FeeCollectionMonthlySplitIID = m.FeeCollectionMonthlySplitIID,
                                                                    FeeDueMonthlySplitID = m.FeeDueMonthlySplitID,
                                                                    FeePeriodID = n.FeePeriodID,
                                                                    MonthID = m.MonthID,
                                                                    Year = m.Year,
                                                                    RefundAmount = m.RefundAmount,
                                                                    TaxAmount = m.TaxAmount,
                                                                    TaxPercentage = m.TaxPercentage,
                                                                }
                                                              ).ToList(),
                                FeeDueFeeTypeMapsID = n.FeeDueFeeTypeMapsID,
                                FeeMasterID = n.FeeMasterID,
                                FeePeriodID = n.FeePeriodID,
                                UpdatedBy = n.FeeCollectionFeeTypeMapsIID > 0 ? _context == null ? (int?)null : (int?)_context.LoginID : n.UpdatedBy,
                                UpdatedDate = n.FeeCollectionFeeTypeMapsIID > 0 ? DateTime.Now : n.UpdatedDate,
                            };

            if (dFeeTypes.Any())
                _sRetData.AddRange(dFeeTypes);

            //Update Fine Details
            if (_sFeeCollection.FeeFines != null)
            {
                var dFines = from n in _sFeeCollection.FeeFines
                             where (n.Amount ?? 0) != 0 && (n.FineMasterID ?? 0) > 0
                             select new FeeCollectionFeeTypeMap()
                             {
                                 Amount = n.Amount ?? 0,
                                 Balance = n.Amount ?? 0,
                                 CreatedBy = n.FeeCollectionFeeTypeMapsIID == 0 ? _context == null ? (int?)null : (int)_context.LoginID : n.CreatedBy,
                                 CreatedDate = n.FeeCollectionFeeTypeMapsIID == 0 ? DateTime.Now : n.CreatedDate,
                                 FeeCollectionFeeTypeMapsIID = n.FeeCollectionFeeTypeMapsIID,
                                 FeeCollectionID = n.FEECollectionID,
                                 FeeDueFeeTypeMapsID = n.FeeDueFeeTypeMapsID,
                                 FineMasterID = n.FineMasterID,
                                 FineMasterStudentMapID = n.FineMasterStudentMapID,
                                 UpdatedBy = n.FeeCollectionFeeTypeMapsIID > 0 ? _context == null ? (int?)null : (int)_context.LoginID : n.UpdatedBy,
                                 UpdatedDate = n.FeeCollectionFeeTypeMapsIID > 0 ? DateTime.Now : n.UpdatedDate,
                             };
                if (dFines.Any())
                    _sRetData.AddRange(dFines);
            }

            return _sRetData;
        }
        #endregion


        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as FeeCollectionDTO;
            decimal paidAmount = 0;
            decimal fineAmount = 0;
            decimal collectedAmount = 0;
            try
            {
                if (!toDto.AcadamicYearID.HasValue)
                {
                    throw new Exception("Please select Academic Year");
                }
                if ((toDto.FeeTypes == null || toDto.FeeTypes.Count == 0) && (toDto.FeeFines == null || toDto.FeeFines.Count == 0))
                {
                    throw new Exception("The invoice needs to be selected for collecting fees!");
                }
                if (toDto.FeeCollectionPaymentModeMapDTO == null || toDto.FeeCollectionPaymentModeMapDTO.Count == 0)
                {
                    throw new Exception("The Payment method needs to be selected for collecting fees!");
                }
                if (toDto.FeeCollectionPaymentModeMapDTO == null || toDto.FeeCollectionPaymentModeMapDTO.Count == 0)
                {
                    throw new Exception("The Payment Amount needs to be selected for collecting fees!");
                }

                if(toDto.CashierID == null || toDto.CashierID == 0)
                {
                    throw new Exception("Please Select Cashier !");
                }

                if (toDto.FeeCollectionPaymentModeMapDTO.Select(x => x.Amount).Sum() <= 0)
                {
                    throw new Exception("The Payment amount cannot be zero!");
                }
                toDto.FeeCollectionIID = 0;
                paidAmount = toDto.FeeCollectionPaymentModeMapDTO.Select(x => x.Amount.Value).Sum();
                collectedAmount = toDto.FeeTypes.Select(x => x.NowPaying.Value).Sum();
                fineAmount = toDto.FeeFines.Select(x => x.Amount.Value).Sum();
                if (paidAmount != (collectedAmount + fineAmount))
                {
                    throw new Exception("Amount need to be collected and Paid amount must be equal");
                }


                toDto.SchoolID = new AcademicYearMapper().GetSchoolByAcademicYearID(toDto.AcadamicYearID);

                var monthlyClosingDate = new MonthlyClosingMapper().GetMonthlyClosingDate(toDto.SchoolID);
                if (monthlyClosingDate.HasValue && monthlyClosingDate.Value.Year > 1900 && toDto.CollectionDate.Value.Date <= monthlyClosingDate.Value.Date)
                {
                    throw new Exception("This Transaction could not be saved due to monthly closing");
                }

                if (toDto.FeeCollectionPaymentModeMapDTO.Any(a => !a.PaymentModeID.HasValue))
                {
                    throw new Exception("Please select payment mode!");
                }

                #region Code Created By Sudish 20210606

                //toDto.Remarks = string.Empty;
                toDto.FeeCollectionPreviousFeesDTO = new List<FeeCollectionPreviousFeesDTO>();

                return ToDTOString(SaveFeeCollection(toDto));
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<FeeCollectionMapper>.Fatal(ex.Message, ex);
                throw ex;
            }
            #endregion

            #region Code not in use
            Entity.Models.Settings.Sequence sequence = new Entity.Models.Settings.Sequence();
            //convert the dto to entity and pass to the repository.
            using (var dbContext = new dbEduegateSchoolContext())
            {
                //var repository = new EntityRepository<FeeCollection, dbEduegateSchoolContext>(dbContext);
                //var repositoryStudentFeeDue = new EntityRepository<StudentFeeDue, dbEduegateSchoolContext>(dbContext);
                //var repositoryFeeDueFeeTypeMap = new EntityRepository<FeeDueFeeTypeMap, dbEduegateSchoolContext>(dbContext);
                //var repositoryFeeDueMonthlySplit = new EntityRepository<FeeDueMonthlySplit, dbEduegateSchoolContext>(dbContext);
                //var repositoryCreditNoteFeeTypeMaps = new EntityRepository<CreditNoteFeeTypeMap, dbEduegateSchoolContext>(dbContext);
                //var repositorySchoolCreditNote = new EntityRepository<SchoolCreditNote, dbEduegateSchoolContext>(dbContext);


                MutualRepository mutualRepository = new MutualRepository();

                //var repositoryAcadamicYear = new EntityRepository<AcademicYear, dbEduegateSchoolContext>(dbContext);
                //var entityAcadamicYear = repositoryAcadamicYear.GetById(toDto.AcadamicYearID);
                //if (!entityAcadamicYear.IsNull())
                //{
                //    if (!(entityAcadamicYear.StartDate.Value.Date <= toDto.CollectionDate && entityAcadamicYear.EndDate.Value.Date >= toDto.CollectionDate))
                //    {
                //        throw new Exception("Please select Collection Date within academic year!");

                //    }
                //}


                try
                {
                    sequence = mutualRepository.GetNextSequence("FeeReceiptNo", null);
                }
                catch (Exception ex)
                {
                    throw new Exception("Please generate sequence with 'FeeReceiptNo'");
                }


                var entity = new FeeCollection()
                {
                    FeeCollectionIID = toDto.FeeCollectionIID,
                    ClassID = toDto.ClassID,
                    AcadamicYearID = toDto.AcadamicYearID,
                    SectionID = toDto.SectionID,
                    StudentID = toDto.StudentID,
                    CashierID = toDto.CashierID,
                    Remarks = toDto.Remarks,
                    Amount = paidAmount,
                    DiscountAmount = toDto.DiscountAmount,
                    FineAmount = toDto.FineAmount,
                    PaidAmount = paidAmount,
                    IsPaid = true,
                    FeeReceiptNo = sequence.Prefix + sequence.LastSequence,
                    ClassFeeMasterId = toDto.ClassFeeMasterId,
                    SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                    CollectionDate = toDto.CollectionDate.IsNotNull() ? Convert.ToDateTime(toDto.CollectionDate) : DateTime.Now,
                    CreatedBy = toDto.FeeCollectionIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                    UpdatedBy = toDto.FeeCollectionIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                    CreatedDate = toDto.FeeCollectionIID == 0 ? DateTime.Now : dto.CreatedDate,
                    UpdatedDate = toDto.FeeCollectionIID > 0 ? DateTime.Now : dto.UpdatedDate,
                    ////TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
                };

                ////get fee type maps
                //var IIDs = toDto.FeeTypes
                //    .Select(a => a.FeeCollectionFeeTypeMapsIID).ToList();

                ////delete maps
                //var entities = dbContext.FeeCollectionFeeTypeMaps.Where(x =>
                //    x.FeeCollectionID == entity.FeeCollectionIID &&
                //    !IIDs.Contains(x.FeeCollectionFeeTypeMapsIID)).ToList();

                //if (entities.IsNotNull())
                //    dbContext.FeeCollectionFeeTypeMaps.RemoveRange(entities);

                /*//newly added   */
                entity.FeeCollectionPaymentModeMaps = new List<FeeCollectionPaymentModeMap>();
                foreach (var paymentType in toDto.FeeCollectionPaymentModeMapDTO)
                {
                    entity.FeeCollectionPaymentModeMaps.Add(new FeeCollectionPaymentModeMap()
                    {
                        //CreatedBy = int.Parse(_context.UserId),
                        Amount = paymentType.Amount,
                        CreatedDate = System.DateTime.Now,
                        FeeCollectionID = toDto.FeeCollectionIID,
                        PaymentModeID = paymentType.PaymentModeID,
                        BankID = paymentType.BankID.HasValue ? paymentType.BankID : null,
                        ReferenceNo = paymentType.ReferenceNo,
                        Balance = paymentType.Amount,
                    });
                }
                entity.FeeCollectionFeeTypeMaps = new List<FeeCollectionFeeTypeMap>();

                foreach (var feeType in toDto.FeeTypes)
                {
                    var monthlySplit = new List<FeeCollectionMonthlySplit>();
                    foreach (var feeMasterMonthlyDto in feeType.MontlySplitMaps)
                    {

                        var entityChild = new FeeCollectionMonthlySplit()
                        {
                            CreditNoteAmount = feeMasterMonthlyDto.CreditNoteAmount,
                            Balance = feeMasterMonthlyDto.Amount.Value - (feeMasterMonthlyDto.CreditNoteAmount.HasValue ? feeMasterMonthlyDto.CreditNoteAmount.Value : 0),
                            FeeDueMonthlySplitID = feeMasterMonthlyDto.FeeDueMonthlySplitID,
                            FeeCollectionMonthlySplitIID = feeMasterMonthlyDto.FeeCollectionMonthlySplitIID,
                            MonthID = byte.Parse(feeMasterMonthlyDto.MonthID.ToString()),
                            Year = feeMasterMonthlyDto.Year,
                            FeePeriodID = feeType.FeePeriodID,
                            Amount = feeMasterMonthlyDto.Amount.HasValue ? feeMasterMonthlyDto.Amount : (decimal?)null,
                            FeeCollectionFeeTypeMapId = feeType.FeeCollectionFeeTypeMapsIID

                        };

                        monthlySplit.Add(entityChild);
                        //var entityFeeDueMonthlySplit = repositoryFeeDueMonthlySplit.GetById(feeMasterMonthlyDto.FeeDueMonthlySplitID);
                        //entityFeeDueMonthlySplit.Status = true;
                        //entityFeeDueMonthlySplit = repositoryFeeDueMonthlySplit.Update(entityFeeDueMonthlySplit);

                        if ((feeMasterMonthlyDto.CreditNoteFeeTypeMapID ?? 0) > 0)
                        {
                            //var entityCreditNoteFeeType = repositoryCreditNoteFeeTypeMaps.GetById(feeMasterMonthlyDto.CreditNoteFeeTypeMapID);
                            //entityCreditNoteFeeType.Status = true;
                            //entityCreditNoteFeeType = repositoryCreditNoteFeeTypeMaps.Update(entityCreditNoteFeeType);

                            var schoolCreId = dbContext.CreditNoteFeeTypeMaps.AsEnumerable().Where(i => i.CreditNoteFeeTypeMapIID == feeMasterMonthlyDto.CreditNoteFeeTypeMapID).Select(x => x.SchoolCreditNoteID).LastOrDefault();

                            if (dbContext.CreditNoteFeeTypeMaps.AsEnumerable().Where(i => i.SchoolCreditNoteID == schoolCreId && i.Status != true).Count() == 0)
                            {
                                //var entityCrNote = repositorySchoolCreditNote.GetById(schoolCreId);
                                //entityCrNote.Status = true;
                                //entityCrNote = repositorySchoolCreditNote.Update(entityCrNote);
                            }
                        }
                    }

                    if (feeType.Amount.HasValue)
                    {

                        entity.FeeCollectionFeeTypeMaps.Add(new FeeCollectionFeeTypeMap()
                        {
                            FeeCollectionFeeTypeMapsIID = feeType.FeeCollectionFeeTypeMapsIID,
                            FeeCollectionID = toDto.FeeCollectionIID,
                            FeeMasterID = feeType.FeeMasterID,
                            FeePeriodID = feeType.FeePeriodID,
                            TaxAmount = feeType.TaxAmount.HasValue ? feeType.TaxAmount : (decimal?)null,
                            TaxPercentage = feeType.TaxPercentage.HasValue ? feeType.TaxPercentage : (decimal?)null,
                            Amount = feeType.Amount.HasValue ? feeType.Amount : (decimal?)null,
                            FeeDueFeeTypeMapsID = feeType.FeeDueFeeTypeMapsID,
                            CreditNoteAmount = feeType.CreditNoteAmount,
                            Balance = feeType.Amount.Value - (feeType.CreditNoteAmount.HasValue ? feeType.CreditNoteAmount.Value : 0),
                            FeeCollectionMonthlySplits = monthlySplit
                        });

                        //var entityFeeDueTypeMap = repositoryFeeDueFeeTypeMap.GetById(feeType.FeeDueFeeTypeMapsID);
                        //if (entityFeeDueTypeMap != null)
                        //{
                        //    if (dbContext.FeeDueMonthlySplits.AsEnumerable().Where(i => i.FeeDueFeeTypeMapsID == feeType.FeeDueFeeTypeMapsID && i.Status != true).Count() == 0)
                        //    {

                        //        entityFeeDueTypeMap.Status = true;


                        //    }
                        //    entityFeeDueTypeMap.CollectedAmount = (entityFeeDueTypeMap.CollectedAmount.HasValue ? entityFeeDueTypeMap.CollectedAmount : 0) + feeType.Amount;
                        //    entityFeeDueTypeMap = repositoryFeeDueFeeTypeMap.Update(entityFeeDueTypeMap);
                        //}

                        if (dbContext.FeeDueFeeTypeMaps.AsEnumerable().Where(i => i.StudentFeeDueID == feeType.StudentFeeDueID && i.Status != true).Count() == 0)
                        {
                            //var entityFeeDue = repositoryStudentFeeDue.GetById(feeType.StudentFeeDueID);
                            //entityFeeDue.CollectionStatus = true;
                            //entityFeeDue = repositoryStudentFeeDue.Update(entityFeeDue);
                        }
                        if (feeType.CreditNoteFeeTypeMapID.HasValue && monthlySplit.Count == 0)
                        {
                            //var entityCreditNoteFeeType = repositoryCreditNoteFeeTypeMaps.GetById(feeType.CreditNoteFeeTypeMapID);
                            //if (entityCreditNoteFeeType != null)
                            //{
                            //    entityCreditNoteFeeType.Status = true;
                            //    entityCreditNoteFeeType = repositoryCreditNoteFeeTypeMaps.Update(entityCreditNoteFeeType);

                            //    var schoolCreId = dbContext.CreditNoteFeeTypeMaps.AsEnumerable().Where(i => i.CreditNoteFeeTypeMapIID == feeType.CreditNoteFeeTypeMapID).Select(x => x.SchoolCreditNoteID).LastOrDefault();

                            //    if (dbContext.CreditNoteFeeTypeMaps.AsEnumerable().Where(i => i.SchoolCreditNoteID == schoolCreId && i.Status != true).Count() == 0)
                            //    {
                            //        var entityCrNote = repositorySchoolCreditNote.GetById(schoolCreId);
                            //        entityCrNote.Status = true;
                            //        entityCrNote = repositorySchoolCreditNote.Update(entityCrNote);
                            //    }
                            //}
                        }

                    }
                }

                foreach (var feeFines in toDto.FeeFines)
                {
                    if (feeFines.Amount.HasValue)
                    {

                        entity.FeeCollectionFeeTypeMaps.Add(new FeeCollectionFeeTypeMap()
                        {

                            FeeCollectionID = toDto.FeeCollectionIID,
                            FineMasterID = feeFines.FineMasterID,
                            FineMasterStudentMapID = feeFines.FineMasterStudentMapID,
                            Amount = feeFines.Amount.HasValue ? feeFines.Amount : (decimal?)null,
                            FeeDueFeeTypeMapsID = feeFines.FeeDueFeeTypeMapsID,
                            FeeCollectionFeeTypeMapsIID = feeFines.FeeCollectionFeeTypeMapsIID,
                            Balance = feeFines.Amount.HasValue ? feeFines.Amount : (decimal?)null,
                        });

                        //var entityFeeDueTypeMap = repositoryFeeDueFeeTypeMap.GetById(feeFines.FeeDueFeeTypeMapsID);
                        //if (entityFeeDueTypeMap != null)
                        //{
                        //    entityFeeDueTypeMap.Status = true;
                        //    entityFeeDueTypeMap.CollectedAmount = (entityFeeDueTypeMap.CollectedAmount.HasValue ? entityFeeDueTypeMap.CollectedAmount : 0) + feeFines.Amount;
                        //    entityFeeDueTypeMap = repositoryFeeDueFeeTypeMap.Update(entityFeeDueTypeMap);

                        //    if (dbContext.FeeDueFeeTypeMaps.AsEnumerable().Where(i => i.StudentFeeDueID == feeFines.StudentFeeDueID && i.Status != true).Count() == 0)
                        //    {
                        //        var entityFeeDue = repositoryStudentFeeDue.GetById(feeFines.StudentFeeDueID);
                        //        entityFeeDue.CollectionStatus = true;
                        //        entityFeeDue = repositoryStudentFeeDue.Update(entityFeeDue);
                        //    }
                        //}
                        //var repositoryFineStudentMap = new EntityRepository<FineMasterStudentMap, dbEduegateSchoolContext>(dbContext);
                        //var entityFineStudentMap = repositoryFineStudentMap.GetById(feeFines.FineMasterStudentMapID);
                        //if (entityFineStudentMap != null)
                        //{
                        //    entityFineStudentMap.IsCollected = true;
                        //    entityFineStudentMap = repositoryFineStudentMap.Update(entityFineStudentMap);
                        //}
                    }
                }

                if (entity.FeeCollectionIID == 0)
                {
                    //entity = repository.Insert(entity);
                }
                else
                {
                    foreach (var paymentMode in entity.FeeCollectionPaymentModeMaps)
                    {
                        if (paymentMode.FeeCollectionPaymentModeMapIID != 0)
                        {
                            dbContext.Entry(paymentMode).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                        else
                        {
                            dbContext.Entry(paymentMode).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                    }

                    foreach (var classMap in entity.FeeCollectionFeeTypeMaps)
                    {
                        foreach (var monthlySplit in classMap.FeeCollectionMonthlySplits)
                        {
                            if (monthlySplit.FeeCollectionMonthlySplitIID != 0)
                            {
                                dbContext.Entry(monthlySplit).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }
                            else
                            {
                                dbContext.Entry(monthlySplit).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            }
                        }

                        if (classMap.FeeCollectionFeeTypeMapsIID != 0)
                        {
                            dbContext.Entry(classMap).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                        else
                        {
                            dbContext.Entry(classMap).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                    }

                    //entity = repository.Update(entity);
                }
                //string parentEmailID = dbContext.Students.Where(x => x.StudentIID == entity.StudentID).Select(y => y.EmailID).FirstOrDefault();
                //return GetEntity(entity.FeeCollectionIID);
                return ToDTOString(new FeeCollectionDTO()
                {
                    FeeCollectionIID = entity.FeeCollectionIID,
                    FeeReceiptNo = entity.FeeReceiptNo,

                });
            }
            #endregion
        }

        private void CollectionAutoAccountPosting(FeeCollection _sRetData)
        {
            byte? isDirectAccountPosting = new Domain.Setting.SettingBL(null).GetSettingValue<byte?>("DIRECT_ACCOUNT_POSTING");
            if (isDirectAccountPosting == 1)
            {
                int type = 1;
                int loginID = Convert.ToInt32(_context.LoginID);
                new AccountEntryMapper().AccountTransMerge(_sRetData.FeeCollectionIID, _sRetData.CollectionDate.Value, loginID, type);
            }
        }

        private void UpdateFeeDueAndCreditNotesStatus(bool _sIsSuccess, FeeCollection _sRetData, List<long> _sLstFeeDueTypeMap_IDs, List<long> _sLstFineTypeMap_IDs, List<long> _sLstFeeDueMonthlyTypeMap_IDs, List<long> _sLstTransporteFeeDueMonthlyMap_IDs, List<long> _sLstFeeDue_IDs, FeeCollectionDTO _sFeeCollection, List<long> _sLstCreditNoteTypeMap_IDs, List<long> _sLstCreditNote_IDs)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                if (_sIsSuccess)
                {
                    var transportFeeMasterID = dbContext.FeeMasters.AsQueryable().Where(X => X.FeeType.FeeGroup.IsTransport == true).AsNoTracking()
                                              .Select(Y => Y.FeeMasterID).FirstOrDefault();

                    int draftFeeCollectionStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<int>("FEECOLLECTIONSTATUSID_DRAFT", 1);

                    //Update Credit Notes Type Maps
                    _sFeeCollection.FeeTypes.Where(w => (w.CreditNoteAmount ?? 0) != 0)
                                            .All(w =>
                                            {
                                                if ((w.CreditNoteFeeTypeMapID ?? 0) > 0 && !_sLstCreditNoteTypeMap_IDs.Contains(w.CreditNoteFeeTypeMapID ?? 0))
                                                    _sLstCreditNoteTypeMap_IDs.Add(w.CreditNoteFeeTypeMapID ?? 0);

                                                if (w.MontlySplitMaps.Any(x => (x.CreditNoteFeeTypeMapID ?? 0) != 0 && (x.CreditNoteAmount ?? 0) != 0))
                                                {
                                                    w.MontlySplitMaps.Where(x => (x.CreditNoteFeeTypeMapID ?? 0) != 0 && (x.CreditNoteAmount ?? 0) != 0)
                                                                     .All(k =>
                                                                     {
                                                                         if ((k.CreditNoteFeeTypeMapID ?? 0) > 0 && !_sLstCreditNoteTypeMap_IDs.Contains(k.CreditNoteFeeTypeMapID ?? 0))
                                                                             _sLstCreditNoteTypeMap_IDs.Add(k.CreditNoteFeeTypeMapID ?? 0);
                                                                         return true;
                                                                     });
                                                }
                                                return true;
                                            });
                    if (_sLstCreditNoteTypeMap_IDs.Any())
                    {
                        _sLstCreditNoteTypeMap_IDs.All(w =>
                        {
                            var dCreditNoteTyepMaps = dbContext.CreditNoteFeeTypeMaps.Where(x => x.CreditNoteFeeTypeMapIID == w).FirstOrDefault();
                            if (dCreditNoteTyepMaps != null)
                            {
                                if ((dCreditNoteTyepMaps.SchoolCreditNoteID ?? 0) > 0 && !_sLstCreditNote_IDs.Contains(dCreditNoteTyepMaps.SchoolCreditNoteID ?? 0))
                                {
                                    _sLstCreditNote_IDs.Add((dCreditNoteTyepMaps.SchoolCreditNoteID ?? 0));
                                    _sLstFeeDueMonthlyTypeMap_IDs.Add((dCreditNoteTyepMaps.FeeDueMonthlySplitID ?? 0));
                                    _sLstFeeDueTypeMap_IDs.Add((dCreditNoteTyepMaps.FeeDueFeeTypeMapsID ?? 0));
                                    dCreditNoteTyepMaps.Status = true;

                                    dbContext.Entry(dCreditNoteTyepMaps).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                    dbContext.SaveChanges();
                                }
                            }
                            return true;
                        });
                    }
                    //Update Credit Notes
                    if (_sLstCreditNote_IDs.Any())
                    {
                        _sLstCreditNote_IDs.All(w =>
                        {
                            //var dCreditNote = repositorySchoolCreditNote.GetById(w);
                            var dCreditNote = dbContext.SchoolCreditNotes.Where(x => x.SchoolCreditNoteIID == w).FirstOrDefault();
                            if (dCreditNote != null)
                            {
                                dCreditNote.Status = true;

                                dbContext.Entry(dCreditNote).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                dbContext.SaveChanges();
                            }
                            return true;
                        });
                    }

                    // Update Status On Fee Due Monthly Type Maps
                    if (_sRetData.FeeCollectionFeeTypeMaps.Any())
                    {
                        var feeDueFeeTypesMapID = _sRetData.FeeCollectionFeeTypeMaps
                            .Where(w => w.FeeDueFeeTypeMapsID != 0 && _sRetData.FeeCollectionStatusID != draftFeeCollectionStatusID).Select(w => w.FeeDueFeeTypeMapsID ?? 0).Distinct();

                        //To avoid duplicate entries getting added for credit noted fees -- previously happened as double amount
                        if (!feeDueFeeTypesMapID.All(id => _sLstFeeDueTypeMap_IDs.Contains(id)))
                        {
                            _sLstFeeDueTypeMap_IDs.AddRange(feeDueFeeTypesMapID);
                        }
                        _sLstFineTypeMap_IDs.AddRange(_sRetData.FeeCollectionFeeTypeMaps.Where(w => w.FineMasterStudentMapID != 0).Select(w => w.FineMasterStudentMapID ?? 0).Distinct());
                        _sRetData.FeeCollectionFeeTypeMaps
                                      .All(w =>
                                      {

                                          if (w.FeeCollectionMonthlySplits.Any())
                                          {

                                              _sLstFeeDueMonthlyTypeMap_IDs.AddRange(w.FeeCollectionMonthlySplits.Where(x => x.FeeDueMonthlySplitID != 0 && x.Amount != 0 && x.Balance == 0).Select(x => x.FeeDueMonthlySplitID ?? 0).Distinct());

                                          };
                                          return true;
                                      });
                    }


                    if (_sLstFeeDueMonthlyTypeMap_IDs.Any())
                    {
                        _sLstFeeDueMonthlyTypeMap_IDs
                            .All(w =>
                            {
                                var dRepMonthlyTypeMap = dbContext.FeeDueMonthlySplits.Where(x => x.FeeDueMonthlySplitIID == w).FirstOrDefault();
                                if (dRepMonthlyTypeMap != null)
                                {
                                    dRepMonthlyTypeMap.Status = true;

                                    dbContext.Entry(dRepMonthlyTypeMap).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                    dbContext.SaveChanges();
                                }
                                return true;
                            });
                    }
                    // Update Collected Status On Student Route Monthly Split
                    if (_sLstTransporteFeeDueMonthlyMap_IDs.Any())
                    {
                        _sLstTransporteFeeDueMonthlyMap_IDs
                            .All(w =>
                            {
                                var dRouteMonthlySplit = dbContext.StudentRouteMonthlySplits.Where(x => x.StudentRouteMonthlySplitIID == w).FirstOrDefault();
                                if (dRouteMonthlySplit != null)
                                {
                                    dRouteMonthlySplit.IsCollected = true;

                                    dbContext.Entry(dRouteMonthlySplit).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                    dbContext.SaveChanges();
                                }
                                return true;
                            });
                    }

                    // Update Status On Fee Due Type Maps
                    if (_sLstFeeDueTypeMap_IDs.Any())
                    {
                        var regualrFeeCycle = (from st in dbContext.Settings where st.SettingCode == "REGULAR_FEE_CYCLE" select st.SettingValue).AsNoTracking().FirstOrDefault();
                        var regualrFeeCycleID = string.IsNullOrEmpty(regualrFeeCycle) ? 1 : byte.Parse(regualrFeeCycle);
                        using (var dbSubContext = new dbEduegateSchoolContext())
                        {
                            _sLstFeeDueTypeMap_IDs
                            .All(w =>
                            {

                                var entityFeeDueTypeMaps = dbSubContext.FeeDueFeeTypeMaps.Where(x => x.FeeDueFeeTypeMapsIID == w).Include(x => x.FeeMaster).ThenInclude(y => y.FeeCycle).FirstOrDefault();
                                if (entityFeeDueTypeMaps != null)
                                {

                                    if (entityFeeDueTypeMaps.FeeMaster.FeeCycleID == regualrFeeCycleID && dbSubContext.FeeDueMonthlySplits.Where(i => i.FeeDueFeeTypeMapsID == w && i.Status != true).AsNoTracking().Count() == 0)
                                    {
                                        entityFeeDueTypeMaps.Status = true;
                                    }

                                    var amount = _sRetData.FeeCollectionFeeTypeMaps.Where(x => x.FeeDueFeeTypeMapsID == w && _sRetData.IsCancelled != true && _sRetData.FeeCollectionStatusID != draftFeeCollectionStatusID).Select(y => y.Amount).Sum();
                                    _sLstFeeDue_IDs.Add(entityFeeDueTypeMaps.StudentFeeDueID ?? 0);

                                    entityFeeDueTypeMaps.CollectedAmount = (entityFeeDueTypeMaps.CollectedAmount.HasValue ? entityFeeDueTypeMaps.CollectedAmount : 0) + amount;
                                    if (entityFeeDueTypeMaps.CollectedAmount == entityFeeDueTypeMaps.Amount)
                                    {
                                        entityFeeDueTypeMaps.Status = true;
                                    }

                                    dbSubContext.Entry(entityFeeDueTypeMaps).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                    dbSubContext.SaveChanges();
                                }
                                return true;
                            });
                        }
                    }

                    // Update Status On Fee Due
                    if (_sLstFeeDue_IDs.Any())
                    {
                        _sLstFeeDue_IDs
                           .All(w =>
                           {
                               if (dbContext.FeeDueFeeTypeMaps.Where(i => i.StudentFeeDueID == w && i.Status != true).AsNoTracking().Count() == 0)
                               {
                                   var entityFeeDue = dbContext.StudentFeeDues.Where(x => x.StudentFeeDueIID == w).FirstOrDefault();
                                   if (entityFeeDue != null)
                                   {
                                       entityFeeDue.CollectionStatus = true;

                                       dbContext.Entry(entityFeeDue).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                       dbContext.SaveChanges();
                                   }
                               }
                               return true;
                           });
                    }

                    // Update Status On Fee Due Type Maps
                    //if (_sLstFeeDueTypeMap_IDs.Any())
                    //{
                    //    _sLstFeeDueTypeMap_IDs
                    //        .All(w =>
                    //        {
                    //            var dRepTypeMap = repositoryFeeDueFeeTypeMap.GetById(w);
                    //            var dRepMonthlyTypeMap = repositoryFeeDueMonthlySplit.Get(x=>x.FeeDueFeeTypeMapsID== w);
                    //            if (dRepTypeMap != null)
                    //            {
                    //                if ((dRepTypeMap.StudentFeeDueID ?? 0) > 0 && !_sLstFeeDue_IDs.Contains((dRepTypeMap.StudentFeeDueID ?? 0)) && (dRepMonthlyTypeMap==null || ( dRepMonthlyTypeMap != null && dRepMonthlyTypeMap.All(y=>y.Status==true))))
                    //                    _sLstFeeDue_IDs.Add(dRepTypeMap.StudentFeeDueID ?? 0);
                    //                dRepTypeMap.Status = true;
                    //               // dRepTypeMap.CollectedAmount = (dRepTypeMap.CollectedAmount ??0)+ (_sRetData.FeeCollectionFeeTypeMaps.Where(x => x.FeeDueFeeTypeMapsID == w).Select(x => x.Amount ?? 0);
                    //                repositoryFeeDueFeeTypeMap.Update(dRepTypeMap);
                    //            }
                    //            return true;
                    //        });
                    //}
                    // Update Status On Fee Due
                    //if (_sLstFeeDue_IDs.Any())
                    //{
                    //    _sLstFeeDue_IDs
                    //       .All(w =>
                    //        {
                    //            var dRepDue = repositoryStudentFeeDue.GetById(w);
                    //            if (dRepDue != null)
                    //            {
                    //                dRepDue.CollectionStatus = true;
                    //                repositoryStudentFeeDue.Update(dRepDue);
                    //            }
                    //            return true;
                    //        });
                    //}

                    //Update Fine Status
                    if (_sLstFineTypeMap_IDs.Any())
                    {
                        _sLstFineTypeMap_IDs.All(w =>
                        {
                            //var dFine = repositoryFineStudentMap.GetById(w);
                            var dFine = dbContext.FineMasterStudentMaps.Where(x => x.FineMasterStudentMapIID == w).FirstOrDefault();
                            if (dFine != null)
                            {
                                dFine.IsCollected = true;

                                dbContext.Entry(dFine).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                dbContext.SaveChanges();
                            }
                            return true;
                        });
                    }
                }
            }
        }

        public List<FeeCollectionDTO> AutoReceiptAccountTransactionSync(long accountTransactionHeadIID, long referenceID, int loginId, int type, int isDueAsNegative)
        {
            var lstFeeCollectionDTO = new List<FeeCollectionDTO>();
            var feeCollectionDTO = new FeeCollectionDTO();

            using (System.Data.SqlClient.SqlConnection conn = new SqlConnection(Infrastructure.ConfigHelper.GetSchoolConnectionString()))
            {
                using (SqlCommand sqlCommand = new SqlCommand("[schools].[SPS_AUTO_FEE_RECEIPT_DUE]", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@ACADEMICYEARID", SqlDbType.BigInt));
                    adapter.SelectCommand.Parameters["@ACADEMICYEARID"].Value = 21;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@SCHOOLID", SqlDbType.TinyInt));
                    adapter.SelectCommand.Parameters["@SCHOOLID"].Value = 30;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@REFERENCE_IDs", SqlDbType.NVarChar));
                    adapter.SelectCommand.Parameters["@REFERENCE_IDs"].Value = referenceID;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@TYPE", SqlDbType.Int));
                    adapter.SelectCommand.Parameters["@TYPE"].Value = type;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@ISDUEASNEGATIVE", SqlDbType.Int));
                    adapter.SelectCommand.Parameters["@ISDUEASNEGATIVE"].Value = isDueAsNegative;

                    DataSet dt = new DataSet();
                    adapter.Fill(dt);
                    DataTable dataTable = null;

                    if (dt.Tables.Count > 0)
                    {
                        if (dt.Tables[0].Rows.Count > 0)
                        {
                            dataTable = dt.Tables[0];
                        }
                    }

                    if (dataTable != null && type == 1)
                    {
                        lstFeeCollectionDTO.Add(new FeeCollectionDTO()
                        {
                            FeeCollectionIID = (long)(dataTable.Rows[0]["FeeCollectionID"]),
                            FeeReceiptNo = dataTable.Rows[0]["FeeReceiptNo"].ToString()
                        });
                    }
                    return lstFeeCollectionDTO;
                }
            }

        }

        public OperationResultDTO FeeCollectionEntry(List<FeeCollectionDTO> feeCollectionList)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                try
                {
                    var feePaymentModeID = feeCollectionList.Count > 0 ? feeCollectionList[0].FeePaymentModeID : null;

                    string draftFeeCollectionStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("FEECOLLECTIONSTATUSID_DRAFT");

                    string transactionPrefix = string.Empty;
                    string onlinePaymentMode = string.Empty;
                    string cashierID = string.Empty;

                    if (feePaymentModeID == (byte?)Eduegate.Services.Contracts.Enums.PaymentModes.QPAY)
                    {
                        transactionPrefix = new Domain.Setting.SettingBL(null).GetSettingValue<string>("QPAY_TRANSACTION_PREFIX");
                        onlinePaymentMode = new Domain.Setting.SettingBL(null).GetSettingValue<string>("QPAY_PAYMENT_MODE_ID");
                        cashierID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("QPAY_PAYMENT_DEFAULTSTAFFID");
                    }
                    else
                    {
                        transactionPrefix = new Domain.Setting.SettingBL(null).GetSettingValue<string>("ONLINE_PAYMENT_TRANSACTION_PREFIX");
                        onlinePaymentMode = new Domain.Setting.SettingBL(null).GetSettingValue<string>("FEECOLLECTIONPAYMENTMODE_ONLINE");
                        cashierID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("ONLINEPAYMENT_DEFAULTSTAFFID");
                    }

                    var masterVisaData = PaymentMasterVisaMapper.Mapper(_context).GetPaymentMasterVisaData();

                    if (masterVisaData == null || string.IsNullOrEmpty(masterVisaData.TransID))
                    {
                        Eduegate.Logger.LogHelper<FeeCollectionMapper>.Fatal("GroupTransactionNumber", new Exception("TransID in PaymentMasterVisas is null"));

                        return new OperationResultDTO()
                        {
                            operationResult = OperationResult.Error,
                            Message = "Something went wrong, try again later!"
                        };
                    }

                    string groupTransNo = transactionPrefix + masterVisaData.TransID;

                    if (string.IsNullOrEmpty(groupTransNo))
                    {
                        return new OperationResultDTO()
                        {
                            operationResult = OperationResult.Error,
                            Message = "Something went wrong, try again later!"
                        };
                    }

                    decimal? totalCollectionAmount = feeCollectionList
                        .Where(fc => fc.FeeTypes != null && fc.FeeTypes.Count > 0) // Check if FeeTypes is not null and has count > 0
                        .Sum(fc => fc.FeeTypes.Sum(ft => ft.Amount));

                    if (masterVisaData.PaymentAmount != totalCollectionAmount)
                    {
                        return new OperationResultDTO()
                        {
                            operationResult = OperationResult.Error,
                            Message = "The total amount does not match the sum of the selected types!"
                        };
                    }

                    foreach (var feeCollection in feeCollectionList)
                    {
                        decimal? totalAmount = 0;
                        decimal? paidAmount = 0;
                        if (feeCollection.FeeTypes.Count > 0)
                        {
                            totalAmount = feeCollection.FeeTypes.Sum(t => t.Amount);

                            paidAmount = feeCollection.FeeTypes.Sum(t => t.NowPaying);

                            feeCollection.CashierID = cashierID != null ? long.Parse(cashierID) : (long?)null;
                            feeCollection.CollectionDate = DateTime.Now.Date;
                            feeCollection.FeeCollectionStatusID = draftFeeCollectionStatus != null ? int.Parse(draftFeeCollectionStatus) : (int?)null;
                            feeCollection.GroupTransactionNumber = groupTransNo;
                            feeCollection.Amount = totalAmount;
                            feeCollection.PaidAmount = paidAmount;
                            feeCollection.PaymentTrackID = masterVisaData?.TrackIID;

                            feeCollection.FeeCollectionPaymentModeMapDTO = new List<FeeCollectionPaymentModeMapDTO>
                            {
                                new FeeCollectionPaymentModeMapDTO()
                                {
                                    FeeCollectionPaymentModeMapIID = 0,
                                    PaymentModeID = onlinePaymentMode != null ? int.Parse(onlinePaymentMode) : (int?)null,
                                    Amount = paidAmount,
                                }
                            };

                            var savedData = SaveFeeCollection(feeCollection);
                        }
                    }

                    return new OperationResultDTO()
                    {
                        operationResult = OperationResult.Success,
                        Message = "Saved Successfully"
                    };
                }
                catch (Exception ex)
                {
                    var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception")
                    ? ex.InnerException?.Message : ex.Message;

                    Eduegate.Logger.LogHelper<string>.Fatal($"Online fee payment failed. Error message: {errorMessage}", ex);

                    return new OperationResultDTO()
                    {
                        operationResult = OperationResult.Error,
                        Message = "Something went wrong, try again later!"
                    };
                }
            }
        }

        public List<FeeCollectionDTO> UpdateStudentsFeePaymentStatus(string transactionNo, long? parentLoginID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                if (string.IsNullOrEmpty(transactionNo))
                {
                    Eduegate.Logger.LogHelper<FeeCollectionMapper>.Fatal("GroupTransactionNumber", new Exception("TransactionNo is null!"));
                    return null;
                }

                var visaMasterData = PaymentMasterVisaMapper.Mapper(_context).GetPaymentMasterVisaDataByTransID(transactionNo);

                string defaultMail = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DEFAULT_MAIL_ID");

                string draftFeeCollectionStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("FEECOLLECTIONSTATUSID_DRAFT");

                string colectedFeeCollectionStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("FEECOLLECTIONSTATUSID_COLLECTED");

                string cashierIDString = string.Empty;
                string transactionPrefix = string.Empty;

                if (!string.IsNullOrEmpty(visaMasterData.CardType) && visaMasterData.CardType?.ToLower() == Eduegate.Services.Contracts.Enums.PaymentModes.QPAY.ToString()?.ToLower())
                {
                    transactionPrefix = new Domain.Setting.SettingBL(null).GetSettingValue<string>("QPAY_TRANSACTION_PREFIX");
                    cashierIDString = new Domain.Setting.SettingBL(null).GetSettingValue<string>("QPAY_PAYMENT_DEFAULTSTAFFID");
                }
                else
                {
                    transactionPrefix = new Domain.Setting.SettingBL(null).GetSettingValue<string>("ONLINE_PAYMENT_TRANSACTION_PREFIX");
                    cashierIDString = new Domain.Setting.SettingBL(null).GetSettingValue<string>("ONLINEPAYMENT_DEFAULTSTAFFID");
                }

                long cashierID = long.Parse(cashierIDString);
                int draftStatusID = int.Parse(draftFeeCollectionStatus);
                int collectedStatusID = int.Parse(colectedFeeCollectionStatus);

                var feeCollectionsListDTO = new List<FeeCollectionDTO>();

                try
                {
                    string groupTransNo = transactionPrefix + transactionNo;

                    if (string.IsNullOrEmpty(groupTransNo) || string.IsNullOrEmpty(transactionPrefix) || groupTransNo.ToLower() == transactionPrefix.ToLower())
                    {
                        Eduegate.Logger.LogHelper<FeeCollectionMapper>.Fatal("GroupTransactionNumber", new Exception("TransactionNo is null or invalid!"));
                        return null;
                    }
                    else
                    {
                        var feeCollectionTransactionList = dbContext.FeeCollections.Where(f => f.GroupTransactionNumber == groupTransNo && f.CreatedBy == parentLoginID)
                        .Include(f => f.Student).ThenInclude(i => i.Parent)
                        .Include(f => f.Student).ThenInclude(i => i.Class)
                        .Include(f => f.Student).ThenInclude(i => i.School)
                        .Include(i => i.FeeCollectionFeeTypeMaps).ThenInclude(i => i.FeeMaster)
                        .AsNoTracking().ToList();

                        if (feeCollectionTransactionList.Count > 0)
                        {
                            foreach (var feeCollection in feeCollectionTransactionList)
                            {
                                var feeCollectionDTO = new FeeCollectionDTO();

                                feeCollection.CollectionDate = DateTime.Now.Date;
                                feeCollection.FeeCollectionStatusID = collectedStatusID;
                                feeCollection.UpdatedBy = _context != null && _context.LoginID.HasValue ? Convert.ToInt32(_context.LoginID) : (int?)null;
                                feeCollection.UpdatedDate = DateTime.Now;

                                dbContext.Entry(feeCollection).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                                dbContext.SaveChanges();

                                feeCollectionDTO = new FeeCollectionDTO()
                                {
                                    FeeCollectionIID = feeCollection.FeeCollectionIID,
                                    FeeReceiptNo = feeCollection.FeeReceiptNo,
                                    EmailID = (feeCollection?.Student?.Parent?.GaurdianEmail) ?? defaultMail,
                                    AdmissionNo = feeCollection?.Student?.AdmissionNumber,
                                    SchoolID = feeCollection.SchoolID,
                                    FeeCollectionStatusID = feeCollection.FeeCollectionStatusID,
                                    ReportName = feeCollection?.FeeCollectionFeeTypeMaps?.FirstOrDefault()?.FeeMaster?.ReportName,
                                    ClassID = feeCollection.Student?.ClassID,
                                    StudentClassCode = feeCollection.Student?.Class?.Code,
                                    StudentSchoolShortName = feeCollection.Student?.School?.SchoolShortName,
                                };

                                feeCollectionsListDTO.Add(feeCollectionDTO);

                                //Update fee due status
                                PaymentFeeDueUpdation(feeCollection, feeCollectionDTO);

                                //Direct account posting
                                CollectionAutoAccountPosting(feeCollection);
                            }

                            return feeCollectionsListDTO;
                        }
                        else
                        {
                            Eduegate.Logger.LogHelper<FeeCollectionMapper>.Fatal("FeeCollectionStatusUpdate", new Exception("Fee collection list is null!"));
                            return null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    var errorMessage = ex.Message;
                    Eduegate.Logger.LogHelper<FeeCollectionMapper>.Fatal(ex.Message, ex);
                    return null;
                }
            }
        }

        public void PaymentFeeDueUpdation(FeeCollection feeCollectionData, FeeCollectionDTO feeCollectionDTO)
        {
            bool _sIsSuccess = true;
            List<long> _sLstFeeDue_IDs = new List<long>();
            List<long> _sLstFeeDueTypeMap_IDs = new List<long>();
            List<long> _sLstFeeDueMonthlyTypeMap_IDs = new List<long>();
            List<long> _sLstTransporteFeeDueMonthlyMap_IDs = new List<long>();
            List<long> _sLstCreditNoteTypeMap_IDs = new List<long>();
            List<long> _sLstCreditNote_IDs = new List<long>();
            List<long> _sLstFineTypeMap_IDs = new List<long>();

            //Update FeeCollectionDTO file
            var feeTypeList = new List<FeeCollectionFeeTypeDTO>();

            if (feeCollectionData.FeeCollectionFeeTypeMaps.Count > 0)
            {
                foreach (var type in feeCollectionData.FeeCollectionFeeTypeMaps)
                {
                    var monthlySplitList = new List<FeeCollectionMonthlySplitDTO>();

                    if (type.FeeCollectionMonthlySplits.Count > 0)
                    {
                        foreach (var monthSplit in type.FeeCollectionMonthlySplits)
                        {
                            monthlySplitList.Add(new FeeCollectionMonthlySplitDTO()
                            {
                                FeeCollectionMonthlySplitIID = monthSplit.FeeCollectionMonthlySplitIID,
                                FeeCollectionFeeTypeMapId = monthSplit.FeeCollectionFeeTypeMapId,
                                FeeDueMonthlySplitID = monthSplit.FeeDueMonthlySplitID,
                                MonthID = monthSplit.MonthID,
                                Year = monthSplit.Year,
                                Amount = monthSplit.Amount,
                                TaxPercentage = monthSplit.TaxPercentage,
                                TaxAmount = monthSplit.TaxAmount,
                                RefundAmount = monthSplit.RefundAmount,
                                CreditNoteAmount = monthSplit.CreditNoteAmount,
                                Balance = monthSplit.Balance,
                            });
                        }

                    }

                    feeTypeList.Add(new FeeCollectionFeeTypeDTO()
                    {
                        FeeCollectionFeeTypeMapsIID = type.FeeCollectionFeeTypeMapsIID,
                        FEECollectionID = type.FeeCollectionID,
                        FeeMasterID = type.FeeMasterID,
                        FeePeriodID = type.FeePeriodID,
                        Amount = type.Amount,
                        TaxPercentage = type.TaxPercentage,
                        TaxAmount = type.TaxAmount,
                        FeeDueFeeTypeMapsID = type.FeeDueFeeTypeMapsID,
                        RefundAmount = type.RefundAmount,
                        CreditNoteAmount = type.CreditNoteAmount,
                        Balance = type.Balance,
                        DueAmount = type.DueAmount,
                        CreatedBy = type.CreatedBy,
                        UpdatedBy = type.UpdatedBy,
                        CreatedDate = type.CreatedDate,
                        UpdatedDate = type.UpdatedDate,
                        MontlySplitMaps = monthlySplitList
                    });
                }

            }

            feeCollectionDTO.FeeTypes = feeTypeList;

            UpdateFeeDueAndCreditNotesStatus(_sIsSuccess, feeCollectionData, _sLstFeeDueTypeMap_IDs, _sLstFineTypeMap_IDs, _sLstFeeDueMonthlyTypeMap_IDs, _sLstTransporteFeeDueMonthlyMap_IDs, _sLstFeeDue_IDs, feeCollectionDTO, _sLstCreditNoteTypeMap_IDs, _sLstCreditNote_IDs);
        }

        public List<FeeCollectionDTO> GetStudentFeeCollectionsHistory(StudentDTO studentData, byte? schoolID, int? academicYearID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                string draftFeeCollectionStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("FEECOLLECTIONSTATUSID_DRAFT");

                string colectedFeeCollectionStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("FEECOLLECTIONSTATUSID_COLLECTED");

                int draftStatusID = int.Parse(draftFeeCollectionStatus);
                int collectedStatusID = int.Parse(colectedFeeCollectionStatus);

                var currentDate = DateTime.Now;

                var feeCollectionsListDTO = new List<FeeCollectionDTO>();

                var feeCollectionDataList = dbContext.FeeCollections
                    .Where(f => f.StudentID == studentData.StudentIID && f.SchoolID == schoolID && f.AcadamicYearID == academicYearID)
                    .Include(i => i.Employee)
                    .Include(i => i.FeeCollectionStatus)
                    .Include(i => i.FeeCollectionFeeTypeMaps).ThenInclude(i => i.FeeMaster)
                    .Include(i => i.FeeCollectionFeeTypeMaps).ThenInclude(i => i.FeePeriod)
                    .Include(i => i.FeeCollectionFeeTypeMaps).ThenInclude(i => i.FeeCollectionMonthlySplits)
                    .AsNoTracking().ToList();

                foreach (var feeCollection in feeCollectionDataList)
                {
                    var feeCollectionTypeList = new List<FeeCollectionFeeTypeDTO>();

                    if (feeCollection.FeeCollectionFeeTypeMaps.Count > 0)
                    {
                        foreach (var type in feeCollection.FeeCollectionFeeTypeMaps)
                        {
                            var monthlySplit = new List<FeeCollectionMonthlySplitDTO>();

                            if (type.FeeCollectionMonthlySplits.Count > 0)
                            {
                                foreach (var mnthSplit in type.FeeCollectionMonthlySplits)
                                {
                                    monthlySplit.Add(new FeeCollectionMonthlySplitDTO()
                                    {
                                        FeeCollectionMonthlySplitIID = mnthSplit.FeeCollectionMonthlySplitIID,
                                        FeeCollectionFeeTypeMapId = mnthSplit.FeeCollectionFeeTypeMapId,
                                        MonthID = mnthSplit.MonthID,
                                        Year = mnthSplit.Year,
                                        Amount = mnthSplit.Amount,
                                        FeeDueMonthlySplitID = mnthSplit.FeeDueMonthlySplitID,
                                        CreditNoteAmount = mnthSplit.CreditNoteAmount,
                                        Balance = mnthSplit.Balance,
                                        RefundAmount = mnthSplit.RefundAmount,
                                        DueAmount = mnthSplit.DueAmount,
                                    });
                                }
                            }

                            feeCollectionTypeList.Add(new FeeCollectionFeeTypeDTO()
                            {
                                FeeCollectionFeeTypeMapsIID = type.FeeCollectionFeeTypeMapsIID,
                                FEECollectionID = type.FeeCollectionID,
                                FeeMasterID = type.FeeMasterID,
                                FeeMaster = type.FeeMasterID.HasValue ? type.FeeMaster?.Description : null,
                                FeePeriodID = type.FeePeriodID,
                                FeePeriod = type.FeePeriodID.HasValue ? type.FeePeriod?.Description : null,
                                Amount = type.Amount,
                                FeeDueFeeTypeMapsID = type.FeeDueFeeTypeMapsID,
                                CreditNoteAmount = type.CreditNoteAmount,
                                Balance = type.Balance,
                                DueAmount = type.DueAmount,
                                MontlySplitMaps = monthlySplit
                            });
                        }
                    }

                    feeCollectionsListDTO.Add(new FeeCollectionDTO()
                    {
                        FeeCollectionIID = feeCollection.FeeCollectionIID,
                        CollectionDate = feeCollection.CollectionDate,
                        Amount = feeCollection.Amount,
                        IsPaid = feeCollection.IsPaid,
                        FeeReceiptNo = feeCollection.FeeReceiptNo,
                        FeeCollectionStatusID = feeCollection.FeeCollectionStatusID != null ? feeCollection.FeeCollectionStatusID : collectedStatusID,
                        FeeCollectionStatusName = feeCollection.FeeCollectionStatusID.HasValue ? feeCollection.FeeCollectionStatus?.StatusNameEn : null,
                        CashierID = feeCollection.CashierID,
                        CashierEmployee = feeCollection.CashierID.HasValue ? new KeyValueDTO()
                        {
                            Key = feeCollection.CashierID.ToString(),
                            Value = feeCollection.Employee?.EmployeeCode + " - " + feeCollection.Employee?.MiddleName + " " + feeCollection.Employee?.LastName
                        } : new KeyValueDTO(),
                        FeeTypes = feeCollectionTypeList,
                        FeeCollectionDraftStatusID = draftStatusID,
                        FeeCollectionCollectedStatusID = collectedStatusID,
                        GroupTransactionNumber = feeCollection.GroupTransactionNumber,
                    });
                }

                return feeCollectionsListDTO;
            }
        }

        public List<FeeCollectionDTO> GetFeeCollectionHistories(List<StudentDTO> studentDatas, byte? schoolID, int? academicYearID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var academicYearListData = new List<AcademicYearDTO>();

                if (academicYearID == 0)
                {
                    academicYearListData = CurrentAcademicYearsDatas();
                }

                var feeCollectionsListDTO = new List<FeeCollectionDTO>();

                foreach (var studentData in studentDatas)
                {
                    var currentSchoolID = schoolID;
                    var currentAcademicYearID = academicYearID;

                    if (currentSchoolID == 0)
                    {
                        currentSchoolID = studentData.SchoolID;
                    }

                    if (currentAcademicYearID == 0)
                    {
                        currentAcademicYearID = academicYearListData.Find(a => a.SchoolID == currentSchoolID).AcademicYearID;
                    }

                    var feeCollectionDataList = dbContext.FeeCollections
                        .Where(f => f.StudentID == studentData.StudentIID && f.SchoolID == currentSchoolID && f.AcadamicYearID == currentAcademicYearID)
                        .Include(i => i.Student).ThenInclude(i => i.Parent)
                        .Include(i => i.Class)
                        .Include(i => i.Section)
                        .Include(i => i.School)
                        .Include(i => i.AcademicYear)
                        .Include(i => i.Employee)
                        .Include(i => i.FeeCollectionPaymentModeMaps).ThenInclude(i => i.PaymentMode)
                        .Include(i => i.FeeCollectionFeeTypeMaps).ThenInclude(i => i.FeeMaster)
                        .Include(i => i.FeeCollectionFeeTypeMaps).ThenInclude(i => i.FeePeriod)
                        .Include(i => i.FeeCollectionFeeTypeMaps).ThenInclude(i => i.FeeCollectionMonthlySplits)
                        .OrderByDescending(x => x.CreatedDate)
                        .AsNoTracking()
                        .ToList();

                    var collections = FillFeeCollectionDTOs(feeCollectionDataList);
                    if (collections.Count > 0)
                    {
                        feeCollectionsListDTO.AddRange(collections);
                    }
                }

                feeCollectionsListDTO = feeCollectionsListDTO.OrderByDescending(fc => fc.FeeCollectionIID).ToList();

                //var feeCollectionsDTOs = new List<FeeCollectionDTO>();

                //foreach (var feeColn in sortedFeeCollectionList)
                //{
                //    feeCollectionsDTOs.Add(feeColn);
                //}

                return feeCollectionsListDTO;
            }
        }

        public List<FeeCollectionDTO> FillFeeCollectionDTOs(List<FeeCollection> feeCollectionDataList)
        {
            string defaultMail = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DEFAULT_MAIL_ID");
            var draftStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<int>("FEECOLLECTIONSTATUSID_DRAFT", 1);
            var collectedStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<int>("FEECOLLECTIONSTATUSID_COLLECTED", 2);

            var feeCollectionsListDTO = new List<FeeCollectionDTO>();


            foreach (var feeCollection in feeCollectionDataList)
            {
                var feeCollectionTypeList = new List<FeeCollectionFeeTypeDTO>();

                var feeCollectionStatus = string.Empty;

                if (feeCollection.FeeCollectionStatusID == draftStatusID)
                {
                    feeCollectionStatus = "Pending";
                }
                else
                {
                    feeCollectionStatus = "Paid";
                }

                if (feeCollection.FeeCollectionFeeTypeMaps.Count > 0)
                {
                    foreach (var type in feeCollection.FeeCollectionFeeTypeMaps)
                    {
                        var monthlySplit = new List<FeeCollectionMonthlySplitDTO>();

                        if (type.FeeCollectionMonthlySplits.Count > 0)
                        {
                            foreach (var mnthSplit in type.FeeCollectionMonthlySplits)
                            {
                                monthlySplit.Add(new FeeCollectionMonthlySplitDTO()
                                {
                                    FeeCollectionMonthlySplitIID = mnthSplit.FeeCollectionMonthlySplitIID,
                                    FeeCollectionFeeTypeMapId = mnthSplit.FeeCollectionFeeTypeMapId,
                                    MonthID = mnthSplit.MonthID,
                                    Year = mnthSplit.Year,
                                    Amount = mnthSplit.Amount,
                                    FeeDueMonthlySplitID = mnthSplit.FeeDueMonthlySplitID,
                                    CreditNoteAmount = mnthSplit.CreditNoteAmount,
                                    Balance = mnthSplit.Balance,
                                    RefundAmount = mnthSplit.RefundAmount,
                                    DueAmount = mnthSplit.DueAmount,
                                });
                            }
                        }

                        feeCollectionTypeList.Add(new FeeCollectionFeeTypeDTO()
                        {
                            FeeCollectionFeeTypeMapsIID = type.FeeCollectionFeeTypeMapsIID,
                            FEECollectionID = type.FeeCollectionID,
                            FeeMasterID = type.FeeMasterID,
                            FeeMaster = type.FeeMasterID.HasValue ? type.FeeMaster?.Description : null,
                            FeePeriodID = type.FeePeriodID,
                            FeePeriod = type.FeePeriodID.HasValue ? type.FeePeriod?.Description : null,
                            Amount = type.Amount,
                            FeeDueFeeTypeMapsID = type.FeeDueFeeTypeMapsID,
                            CreditNoteAmount = type.CreditNoteAmount,
                            Balance = type.Balance,
                            DueAmount = type.DueAmount,
                            MontlySplitMaps = monthlySplit
                        });
                    }

                }

                feeCollectionsListDTO.Add(new FeeCollectionDTO()
                {
                    FeeCollectionIID = feeCollection.FeeCollectionIID,
                    CollectionDate = feeCollection.CollectionDate,
                    StudentID = feeCollection.StudentID,
                    StudentName = feeCollection.StudentID.HasValue ? feeCollection.Student.AdmissionNumber + " - " + feeCollection.Student.FirstName + " " + feeCollection.Student.MiddleName + " " + feeCollection.Student.LastName : "NA",
                    ClassID = feeCollection.ClassID,
                    ClassName = feeCollection.ClassID.HasValue ? feeCollection.Class?.ClassDescription : null,
                    SectionID = feeCollection.SectionID,
                    SectionName = feeCollection.SectionID.HasValue ? feeCollection.Section?.SectionName : null,
                    SchoolID = feeCollection.SchoolID,
                    SchoolName = feeCollection.SchoolID.HasValue ? feeCollection.School?.SchoolName : null,
                    AcadamicYearID = feeCollection.AcadamicYearID,
                    AcademicYear = feeCollection.AcadamicYearID.HasValue ? new KeyValueDTO()
                    {
                        Key = feeCollection.AcadamicYearID.ToString(),
                        Value = feeCollection.AcademicYear?.Description + " " + "(" + feeCollection.AcademicYear?.AcademicYearCode + ")"
                    } : new KeyValueDTO(),
                    Amount = feeCollection.Amount,
                    IsPaid = feeCollection.IsPaid,
                    FeeReceiptNo = feeCollection.FeeReceiptNo,
                    FeeCollectionStatusID = feeCollection.FeeCollectionStatusID != null ? feeCollection.FeeCollectionStatusID : collectedStatusID,
                    FeeCollectionStatusName = feeCollectionStatus,
                    CashierID = feeCollection.CashierID,
                    CashierEmployee = feeCollection.CashierID.HasValue ? new KeyValueDTO()
                    {
                        Key = feeCollection.CashierID.ToString(),
                        Value = feeCollection.Employee?.EmployeeCode + " - " + feeCollection.Employee?.MiddleName + " " + feeCollection.Employee?.LastName
                    } : new KeyValueDTO(),
                    FeeTypes = feeCollectionTypeList,
                    FeeCollectionDraftStatusID = draftStatusID,
                    FeeCollectionCollectedStatusID = collectedStatusID,
                    GroupTransactionNumber = feeCollection.GroupTransactionNumber,
                    CreatedBy = feeCollection.CreatedBy,
                    CreatedDate = feeCollection.CreatedDate,
                    UpdatedBy = feeCollection.UpdatedBy,
                    UpdatedDate = feeCollection.UpdatedDate,
                    FeePaymentModeID = feeCollection.FeeCollectionPaymentModeMaps.Count > 0 ? feeCollection.FeeCollectionPaymentModeMaps.FirstOrDefault()?.PaymentModeID : null,
                    FeePaymentMode = feeCollection.FeeCollectionPaymentModeMaps.Count > 0 ? feeCollection.FeeCollectionPaymentModeMaps.FirstOrDefault()?.PaymentMode?.PaymentModeName : null,
                    IsEnableRetry = false,
                    EmailID = feeCollection?.Student?.Parent?.GaurdianEmail ?? defaultMail,
                });
            }

            feeCollectionsListDTO = feeCollectionsListDTO.OrderByDescending(fc => fc.CollectionDate).ToList();

            //var feeCollectionsDTOs = new List<FeeCollectionDTO>();

            //foreach (var feeColn in sortedFeeCollectionList)
            //{
            //    feeCollectionsDTOs.Add(feeColn);
            //}

            return feeCollectionsListDTO;
        }

        public List<FeeCollectionDTO> GetLastTenFeeCollectionHistories(List<StudentDTO> studentDatas)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                string draftFeeCollectionStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("FEECOLLECTIONSTATUSID_DRAFT");

                string collectedFeeCollectionStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("FEECOLLECTIONSTATUSID_COLLECTED");

                int draftStatusID = int.Parse(draftFeeCollectionStatus);
                int collectedStatusID = int.Parse(collectedFeeCollectionStatus);

                var currentDate = DateTime.Now;

                var feeCollectionsListDTO = new List<FeeCollectionDTO>();

                foreach (var studentData in studentDatas)
                {
                    var feeCollectionDataList = dbContext.FeeCollections
                        .Where(f => f.StudentID == studentData.StudentIID)
                        .Include(i => i.Student).ThenInclude(i => i.Parent)
                        .Include(i => i.Class)
                        .Include(i => i.Section)
                        .Include(i => i.School)
                        .Include(i => i.AcademicYear)
                        .Include(i => i.Employee)
                        .Include(i => i.FeeCollectionPaymentModeMaps).ThenInclude(i => i.PaymentMode)
                        .Include(i => i.FeeCollectionFeeTypeMaps).ThenInclude(i => i.FeeMaster)
                        .Include(i => i.FeeCollectionFeeTypeMaps).ThenInclude(i => i.FeePeriod)
                        .Include(i => i.FeeCollectionFeeTypeMaps).ThenInclude(i => i.FeeCollectionMonthlySplits)
                        .OrderByDescending(x => x.CreatedDate)
                        .Take(10)
                        .AsNoTracking()
                        .ToList();

                    var collections = FillFeeCollectionDTOs(feeCollectionDataList);
                    if (collections.Count > 0)
                    {
                        feeCollectionsListDTO.AddRange(collections);
                    }
                }

                feeCollectionsListDTO = feeCollectionsListDTO.OrderByDescending(fc => fc.FeeCollectionIID).ToList();

                //var feeCollectionsDTOs = new List<FeeCollectionDTO>();

                //foreach (var feeColn in sortedFeeCollectionList)
                //{
                //    feeCollectionsDTOs.Add(feeColn);
                //}

                return feeCollectionsListDTO;
            }
        }

        public List<FeeCollectionDTO> GetFeeCollectionHistory(long studentID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                string draftFeeCollectionStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("FEECOLLECTIONSTATUSID_DRAFT");

                string collectedFeeCollectionStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("FEECOLLECTIONSTATUSID_COLLECTED");

                int draftStatusID = int.Parse(draftFeeCollectionStatus);
                int collectedStatusID = int.Parse(collectedFeeCollectionStatus);

                var currentDate = DateTime.Now;

                var feeCollectionsListDTO = new List<FeeCollectionDTO>();


                var feeCollectionDataList = dbContext.FeeCollections
                    .Where(f => f.StudentID == studentID)
                    .Include(i => i.Student).ThenInclude(i => i.Parent)
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .Include(i => i.School)
                    .Include(i => i.AcademicYear)
                    .Include(i => i.Employee)
                    .Include(i => i.FeeCollectionPaymentModeMaps).ThenInclude(i => i.PaymentMode)
                    .Include(i => i.FeeCollectionFeeTypeMaps).ThenInclude(i => i.FeeMaster)
                    .Include(i => i.FeeCollectionFeeTypeMaps).ThenInclude(i => i.FeePeriod)
                    .Include(i => i.FeeCollectionFeeTypeMaps).ThenInclude(i => i.FeeCollectionMonthlySplits)
                    .OrderByDescending(x => x.CreatedDate)
                    .Take(10)
                    .AsNoTracking()
                    .ToList();

                var collections = FillFeeCollectionDTOs(feeCollectionDataList);
                if (collections.Count > 0)
                {
                    feeCollectionsListDTO.AddRange(collections);
                }


                feeCollectionsListDTO = feeCollectionsListDTO.OrderByDescending(fc => fc.FeeCollectionIID).ToList();


                return feeCollectionsListDTO;
            }
        }


        public List<AcademicYearDTO> CurrentAcademicYearsDatas()
        {
            var academicYearDTOs = new List<AcademicYearDTO>();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var currentAcademicYearStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CURRENT_ACADEMIC_YEAR_STATUSID", "2");

                byte? currentAcademicStatusID = byte.Parse(currentAcademicYearStatus);

                var academicYearDatas = dbContext.AcademicYears.Where(a => a.AcademicYearStatusID == currentAcademicStatusID && a.IsActive == true).AsNoTracking().ToList();

                foreach (var academic in academicYearDatas)
                {
                    academicYearDTOs.Add(new AcademicYearDTO()
                    {
                        AcademicYearID = academic.AcademicYearID,
                        AcademicYearCode = academic.AcademicYearCode,
                        Description = academic.Description,
                        StartDate = academic.StartDate,
                        EndDate = academic.EndDate,
                        SchoolID = academic.SchoolID,
                        IsActive = academic.IsActive,
                        AcademicYearStatusID = academic.AcademicYearStatusID,
                        ORDERNO = academic.ORDERNO,
                    });
                }

                return academicYearDTOs;
            }
        }

        public string CheckFeeCollectionStatusByTransactionNumber(string transactionNo)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                string draftFeeCollectionStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("FEECOLLECTIONSTATUSID_DRAFT");

                string colectedFeeCollectionStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("FEECOLLECTIONSTATUSID_COLLECTED");

                int draftStatusID = int.Parse(draftFeeCollectionStatus);
                int collectedStatusID = int.Parse(colectedFeeCollectionStatus);

                var transFeeCollectionDatas = dbContext.FeeCollections.Where(f => f.GroupTransactionNumber == transactionNo)
                    .Include(i => i.FeeCollectionFeeTypeMaps).ThenInclude(i => i.FeeCollectionMonthlySplits)
                    .AsNoTracking().ToList();

                if (transFeeCollectionDatas != null && transFeeCollectionDatas.Count > 0)
                {
                    foreach (var transCollection in transFeeCollectionDatas)
                    {
                        foreach (var type in transCollection.FeeCollectionFeeTypeMaps)
                        {
                            if (type.FeeCollectionMonthlySplits.Count > 0)
                            {
                                foreach (var monthSplit in type.FeeCollectionMonthlySplits)
                                {
                                    var feeCollectionDatasWithMonth = dbContext.FeeCollections.Where(f => f.StudentID == transCollection.StudentID && f.ClassID == transCollection.ClassID &&
                                    f.SectionID == transCollection.SectionID && f.SchoolID == transCollection.SchoolID && f.AcadamicYearID == transCollection.AcadamicYearID &&
                                    f.FeeCollectionFeeTypeMaps.Any(t => t.FeeDueFeeTypeMapsID == type.FeeDueFeeTypeMapsID &&
                                    t.FeeCollectionMonthlySplits.Any(m => m.FeeDueMonthlySplitID == monthSplit.FeeDueMonthlySplitID)) && f.FeeCollectionStatusID == collectedStatusID).AsNoTracking().ToList();

                                    if (feeCollectionDatasWithMonth != null && feeCollectionDatasWithMonth.Count != 0)
                                    {
                                        return "Data already exist";
                                    }
                                }
                            }
                            else
                            {
                                var feeCollectionDatasWithType = dbContext.FeeCollections.Where(f => f.StudentID == transCollection.StudentID && f.ClassID == transCollection.ClassID &&
                                f.SectionID == transCollection.SectionID && f.SchoolID == transCollection.SchoolID && f.AcadamicYearID == transCollection.AcadamicYearID &&
                                f.FeeCollectionFeeTypeMaps.Any(x => x.FeeDueFeeTypeMapsID == type.FeeDueFeeTypeMapsID) && f.FeeCollectionStatusID == collectedStatusID).AsNoTracking().ToList();

                                if (feeCollectionDatasWithType != null && feeCollectionDatasWithType.Count != 0)
                                {
                                    return "Data already exist";
                                }
                            }
                        }
                    }
                }

                return null;
            }
        }

        public List<FeeCollectionDTO> GetFeeCollectionDetailsByTransactionNumber(string transactionNumber, string mailID, string feeReceiptNo = null)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var feeCollectionList = new List<FeeCollectionDTO>();

                string defaultMail = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DEFAULT_MAIL_ID");

                var collectionDatas = dbContext.FeeCollections.Where(f => f.GroupTransactionNumber == transactionNumber)
                    .Include(i => i.FeeCollectionPaymentModeMaps).ThenInclude(i => i.PaymentMode)
                    .Include(i => i.Student).ThenInclude(i => i.Parent)
                    .Include(i => i.Student).ThenInclude(i => i.Class)
                    .Include(i => i.Student).ThenInclude(i => i.School)
                    .AsNoTracking().ToList();

                if (collectionDatas.Count == 0)
                {
                    collectionDatas = dbContext.FeeCollections.Where(f => f.FeeReceiptNo == feeReceiptNo)
                    .Include(i => i.FeeCollectionPaymentModeMaps).ThenInclude(i => i.PaymentMode)
                    .Include(i => i.Student).ThenInclude(i => i.Parent)
                    .Include(i => i.Student).ThenInclude(i => i.Class)
                    .Include(i => i.Student).ThenInclude(i => i.School)
                    .AsNoTracking().ToList();
                }

                foreach (var collection in collectionDatas)
                {
                    feeCollectionList.Add(new FeeCollectionDTO()
                    {
                        FeeCollectionIID = collection.FeeCollectionIID,
                        Amount = collection.Amount,
                        FeeReceiptNo = collection.FeeReceiptNo,
                        EmailID = !string.IsNullOrEmpty(mailID) ? mailID : (collection?.Student?.Parent?.GaurdianEmail) ?? defaultMail,
                        AdmissionNo = collection?.Student?.AdmissionNumber,
                        SchoolID = collection.SchoolID,
                        FeeCollectionStatusID = collection.FeeCollectionStatusID,
                        CreatedBy = collection.CreatedBy,
                        ClassID = collection.Student?.ClassID,
                        StudentClassCode = collection.Student?.Class?.Code,
                        StudentSchoolShortName = collection.School?.SchoolShortName,
                        FeePaymentModeID = collection.FeeCollectionPaymentModeMaps?.FirstOrDefault()?.PaymentModeID,
                        FeePaymentMode = collection.FeeCollectionPaymentModeMaps?.FirstOrDefault()?.PaymentMode?.PaymentModeName,
                    });
                }

                return feeCollectionList;
            }
        }

        #region Mobile app fee collection related codes

        public List<FeePaymentDTO> FillStudentsFeePaymentDetailsByParentLogin(long loginID)
        {
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

            var feePaymentDTO = new List<FeePaymentDTO>();

            var studentDatas = StudentMapper.Mapper(_context).GetStudentsSiblings(loginID);

            foreach (var studDet in studentDatas)
            {
                var paymentData = GetStudentFeeDetails(studDet.StudentIID);

                feePaymentDTO.Add(new FeePaymentDTO()
                {
                    IsExpand = paymentData.IsExpand,
                    IsSelected = paymentData.IsSelected,
                    StudentID = studDet.StudentIID,
                    StudentName = studDet.AdmissionNumber + " - " + studDet.FirstName + " " + studDet.MiddleName + " " + studDet.LastName,
                    ClassID = studDet.ClassID,
                    ClassName = studDet.ClassName,
                    SectionID = studDet.SectionID,
                    SectionName = studDet.SectionName,
                    SchoolID = studDet.SchoolID,
                    SchoolName = studDet.SchoolName,
                    AcademicYearID = studDet.AcademicYearID,
                    AcademicYear = studDet.AcademicYear,
                    TotalAmount = paymentData.TotalAmount,
                    OldTotalAmount = paymentData.OldTotalAmount,
                    NowPaying = paymentData.NowPaying,
                    StudentFeeDueTypes = paymentData.StudentFeeDueTypes,
                });
            }

            return feePaymentDTO;
        }

        public FeePaymentDTO GetStudentFeeDetails(long studentID)
        {
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

            List<StudentFeeDueDTO> feeDueData = FeeDueGenerationMapper.Mapper(_context).FillFeeDue(0, studentID);

            var feeTypeList = new List<FeeDueFeeTypeMapDTO>();

            foreach (var dueData in feeDueData)
            {
                foreach (var feeType in dueData.FeeDueFeeTypeMap)
                {
                    var balanceTypeAmountToPay = feeType.Amount - (feeType.CreditNoteAmount ?? 0) - (feeType.CollectedAmount ?? 0);

                    if (balanceTypeAmountToPay != 0)
                    {
                        feeTypeList.Add(new FeeDueFeeTypeMapDTO()
                        {
                            InvoiceNo = feeType.InvoiceNo,
                            Amount = balanceTypeAmountToPay,
                            NowPaying = balanceTypeAmountToPay,
                            FeePeriodID = feeType.FeePeriodID,
                            StudentFeeDueID = feeType.StudentFeeDueID,
                            FeeDueFeeTypeMapsID = feeType.FeeDueFeeTypeMapsIID,
                            FeeMaster = feeType.FeeMaster,
                            IsPayingNow = true,
                            FeePeriod = feeType.FeePeriod,
                            IsMandatoryToPay = feeType.IsMandatoryToPay,
                            FeeMasterID = !string.IsNullOrEmpty(feeType.FeeMaster.Key) ? int.Parse(feeType.FeeMaster.Key) : (int?)null,
                            InvoiceStringDate = feeType.InvoiceDate.HasValue ? Convert.ToDateTime(feeType.InvoiceDate).ToString(dateFormat, CultureInfo.InvariantCulture) : "NA",
                            InvoiceDate = feeType.InvoiceDate == null ? (DateTime?)null : Convert.ToDateTime(feeType.InvoiceDate),
                            FeeDueMonthlySplit = (from split in feeType.FeeDueMonthlySplit
                                                      //where split.Amount - (split.CreditNoteAmount ?? 0) - (split.CollectedAmount ?? 0) != 0
                                                  select new FeeDueMonthlySplitDTO()
                                                  {
                                                      TotalAmount = split.Amount,
                                                      Amount = split.Amount - (split.CreditNoteAmount ?? 0) - (split.CollectedAmount ?? 0),
                                                      CreditNote = split.CreditNoteAmount.HasValue ? split.CreditNoteAmount.Value : 0,
                                                      Balance = 0,
                                                      NowPaying = split.Amount - (split.CreditNoteAmount ?? 0) - (split.CollectedAmount ?? 0),
                                                      OldNowPaying = split.Amount - (split.CreditNoteAmount ?? 0) - (split.CollectedAmount ?? 0),
                                                      MonthID = split.MonthID,
                                                      Year = split.Year,
                                                      FeeDueMonthlySplitID = split.FeeDueMonthlySplitIID,
                                                      IsRowSelected = true,
                                                      MonthName = split.MonthID == 0 ? null : new DateTime(split.Year, split.MonthID, 1).ToString("MMM")
                                                  }).ToList(),
                        });
                    }
                }
            }

            return new FeePaymentDTO()
            {
                IsExpand = true,
                IsSelected = true,
                StudentID = studentID,
                TotalAmount = feeTypeList.Sum(s => s.Amount),
                OldTotalAmount = feeTypeList.Sum(s => s.Amount),
                NowPaying = feeTypeList.Sum(s => s.Amount),
                StudentFeeDueTypes = feeTypeList,
            };
        }

        public List<FeeCollectionDTO> FillStudentsFeeCollectionHistories(byte schoolID, int academicYearID)
        {
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

            var studentDatas = StudentMapper.Mapper(_context).GetStudentsSiblings(_context.LoginID.HasValue ? _context.LoginID.Value : 0);

            var feeCollectionDatas = GetFeeCollectionHistories(studentDatas, schoolID, academicYearID);

            var feeCollectionHistories = new List<FeeCollectionDTO>();

            var collectionGroupByCreatedDate = feeCollectionDatas.GroupBy(g => g.CollectionDate).ToList();

            foreach (var collectionGroup in collectionGroupByCreatedDate)
            {
                var feeCollectionGrouping = collectionGroup.GroupBy(g => g.GroupTransactionNumber).ToList();

                foreach (var collection in feeCollectionGrouping)
                {
                    feeCollectionHistories.Add(new FeeCollectionDTO()
                    {
                        FeeCollectionStatusID = collection.Max(s => s.FeeCollectionStatusID),
                        FeeCollectionStatusName = collection.Max(s => s.FeeCollectionStatusName),
                        FeeCollectionDraftStatusID = collection.Max(s => s.FeeCollectionDraftStatusID),
                        FeeCollectionCollectedStatusID = collection.Max(s => s.FeeCollectionCollectedStatusID),
                        GroupTransactionNumber = string.IsNullOrEmpty(collection?.Key) ? "NA" : collection.Key,
                        CollectionDate = collection.Max(s => s.CollectionDate),
                        CollectionStringDate = collection.Max(s => s.CollectionDate.HasValue ? Convert.ToDateTime(s.CollectionDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null),
                        Amount = collection.Sum(s => s.Amount),
                        EmailID = string.IsNullOrEmpty(studentDatas[0].ParentEmailID) ? _context.EmailID : studentDatas[0].ParentEmailID,
                        FeePaymentModeID = collection.FirstOrDefault().FeePaymentModeID,
                        FeePaymentMode = collection.FirstOrDefault().FeePaymentMode,
                    });
                }
            }

            return feeCollectionHistories;
        }

        public List<FeeCollectionDTO> FillCollectionDetailsByTransactionNumber(string transactionNumber)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

                string draftFeeCollectionStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("FEECOLLECTIONSTATUSID_DRAFT");

                string colectedFeeCollectionStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("FEECOLLECTIONSTATUSID_COLLECTED");

                int draftStatusID = int.Parse(draftFeeCollectionStatus);
                int collectedStatusID = int.Parse(colectedFeeCollectionStatus);

                var currentDate = DateTime.Now;

                var feeCollectionDatas = dbContext.FeeCollections.Where(f => f.GroupTransactionNumber == transactionNumber && f.GroupTransactionNumber != null)
                    .Include(i => i.Student)
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .Include(i => i.School)
                    .Include(i => i.AcademicYear)
                    .Include(i => i.Employee)
                    .Include(i => i.FeeCollectionFeeTypeMaps).ThenInclude(i => i.FeeMaster)
                    .Include(i => i.FeeCollectionFeeTypeMaps).ThenInclude(i => i.FeePeriod)
                    .Include(i => i.FeeCollectionFeeTypeMaps).ThenInclude(i => i.FeeCollectionMonthlySplits)
                    .OrderByDescending(x => x.CreatedDate)
                    .AsNoTracking().ToList();

                var feeCollectionsListDTO = new List<FeeCollectionDTO>();
                foreach (var feeCollection in feeCollectionDatas)
                {
                    var feeCollectionTypeList = new List<FeeCollectionFeeTypeDTO>();

                    var feeCollectionStatus = string.Empty;

                    if (feeCollection.FeeCollectionStatusID == draftStatusID)
                    {
                        feeCollectionStatus = "Pending";
                    }
                    else
                    {
                        feeCollectionStatus = "Paid";
                    }

                    if (feeCollection.FeeCollectionFeeTypeMaps.Count > 0)
                    {
                        foreach (var type in feeCollection.FeeCollectionFeeTypeMaps)
                        {
                            var monthlySplit = new List<FeeCollectionMonthlySplitDTO>();

                            if (type.FeeCollectionMonthlySplits.Count > 0)
                            {
                                foreach (var mnthSplit in type.FeeCollectionMonthlySplits)
                                {
                                    monthlySplit.Add(new FeeCollectionMonthlySplitDTO()
                                    {
                                        FeeCollectionMonthlySplitIID = mnthSplit.FeeCollectionMonthlySplitIID,
                                        FeeCollectionFeeTypeMapId = mnthSplit.FeeCollectionFeeTypeMapId,
                                        MonthID = mnthSplit.MonthID,
                                        Year = mnthSplit.Year,
                                        Amount = mnthSplit.Amount,
                                        FeeDueMonthlySplitID = mnthSplit.FeeDueMonthlySplitID,
                                        CreditNoteAmount = mnthSplit.CreditNoteAmount,
                                        Balance = mnthSplit.Balance,
                                        RefundAmount = mnthSplit.RefundAmount,
                                        DueAmount = mnthSplit.DueAmount,
                                        MonthName = mnthSplit.MonthID == 0 ? null : new DateTime(mnthSplit.Year, mnthSplit.MonthID, 1).ToString("MMM"),
                                    });
                                }
                            }

                            feeCollectionTypeList.Add(new FeeCollectionFeeTypeDTO()
                            {
                                FeeCollectionFeeTypeMapsIID = type.FeeCollectionFeeTypeMapsIID,
                                FEECollectionID = type.FeeCollectionID,
                                FeeMasterID = type.FeeMasterID,
                                FeeMaster = type.FeeMasterID.HasValue ? type.FeeMaster?.Description : null,
                                FeePeriodID = type.FeePeriodID,
                                FeePeriod = type.FeePeriodID.HasValue ? type.FeePeriod?.Description : null,
                                Amount = type.Amount,
                                FeeDueFeeTypeMapsID = type.FeeDueFeeTypeMapsID,
                                CreditNoteAmount = type.CreditNoteAmount,
                                Balance = type.Balance,
                                DueAmount = type.DueAmount,
                                MontlySplitMaps = monthlySplit
                            });
                        }

                    }

                    string feePaymentModes = string.Empty;
                    if (feeCollection.FeeCollectionPaymentModeMaps.Count > 0)
                    {
                        foreach (var mode in feeCollection.FeeCollectionPaymentModeMaps)
                        {
                            feePaymentModes = string.Concat(feePaymentModes, mode.PaymentMode.PaymentModeName, ",");
                        }
                        feePaymentModes = feePaymentModes.TrimEnd(",");
                    }
                    else
                    {
                        feePaymentModes = "NA";
                    }

                    feeCollectionsListDTO.Add(new FeeCollectionDTO()
                    {
                        FeeCollectionIID = feeCollection.FeeCollectionIID,
                        CollectionDate = feeCollection.CollectionDate,
                        CollectionStringDate = feeCollection.CollectionDate.HasValue ? Convert.ToDateTime(feeCollection.CollectionDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                        StudentID = feeCollection.StudentID,
                        StudentName = feeCollection.StudentID.HasValue ? feeCollection.Student.AdmissionNumber + " - " + feeCollection.Student.FirstName + " " + feeCollection.Student.MiddleName + " " + feeCollection.Student.LastName : "NA",
                        ClassID = feeCollection.ClassID,
                        ClassName = feeCollection.ClassID.HasValue ? feeCollection.Class?.ClassDescription : null,
                        SectionID = feeCollection.SectionID,
                        SectionName = feeCollection.SectionID.HasValue ? feeCollection.Section?.SectionName : null,
                        SchoolID = feeCollection.SchoolID,
                        SchoolName = feeCollection.SchoolID.HasValue ? feeCollection.School?.SchoolName : null,
                        AcadamicYearID = feeCollection.AcadamicYearID,
                        AcademicYear = feeCollection.AcadamicYearID.HasValue ? new KeyValueDTO()
                        {
                            Key = feeCollection.AcadamicYearID.ToString(),
                            Value = feeCollection.AcademicYear?.Description + " " + "(" + feeCollection.AcademicYear?.AcademicYearCode + ")"
                        } : new KeyValueDTO(),
                        Amount = feeCollection.Amount,
                        IsPaid = feeCollection.IsPaid,
                        FeeReceiptNo = feeCollection.FeeReceiptNo,
                        FeeCollectionStatusID = feeCollection.FeeCollectionStatusID != null ? feeCollection.FeeCollectionStatusID : collectedStatusID,
                        FeeCollectionStatusName = feeCollectionStatus,
                        CashierID = feeCollection.CashierID,
                        CashierEmployee = feeCollection.CashierID.HasValue ? new KeyValueDTO()
                        {
                            Key = feeCollection.CashierID.ToString(),
                            Value = feeCollection.Employee?.EmployeeCode + " - " + feeCollection.Employee?.MiddleName + " " + feeCollection.Employee?.LastName
                        } : new KeyValueDTO(),
                        FeeTypes = feeCollectionTypeList,
                        FeeCollectionDraftStatusID = draftStatusID,
                        FeeCollectionCollectedStatusID = collectedStatusID,
                        GroupTransactionNumber = feeCollection.GroupTransactionNumber,
                        FeePaymentMode = feePaymentModes,
                    });
                }

                return feeCollectionsListDTO;
            }
        }

        #endregion

    }
}