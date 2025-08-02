using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Fees;
using Eduegate.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Framework.Extensions;
using Eduegate.Domain.Repository;
using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Mappers.School.Accounts;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Eduegate.Domain.Mappers.Accounts;
using static Eduegate.Services.Contracts.ErrorCodes;

namespace Eduegate.Domain.Mappers.School.Fees
{
    public class RefundMapper : DTOEntityDynamicMapper
    {
        public static RefundMapper Mapper(CallContext context)
        {
            var mapper = new RefundMapper();
            mapper._context = context;
            return mapper;
        }
        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<RefundDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            RefundDTO refundDTO = new RefundDTO();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var getData = dbContext.Refunds
                    .Include(x => x.Student)
                    .Include(x => x.Class)
                    .Include(x => x.Section)
                    .Include(x => x.AcademicYear)

                    .Include(x => x.RefundFeeTypeMaps)
                    .ThenInclude(y => y.RefundMonthlySplits)

                    .Include(x => x.RefundFeeTypeMaps)
                    .ThenInclude(y => y.FeeMaster)

                    .Include(x => x.RefundFeeTypeMaps)
                    .ThenInclude(y => y.FeePeriod)

                    .Include(x => x.RefundPaymentModeMaps)
                    .ThenInclude(z => z.PaymentMode)

                    .AsNoTracking()
                    .FirstOrDefault(x => x.RefundIID == IID);

                refundDTO.RefundIID = getData.RefundIID;
                refundDTO.CollectionDate = getData.CreatedDate;
                refundDTO.RefundDate = getData.RefundDate;
                refundDTO.Student = new KeyValueDTO() { Key = Convert.ToString(getData.StudentID), Value = getData.Student.AdmissionNumber + " - " + getData.Student.FirstName + " " + getData.Student.MiddleName + " " + getData.Student.LastName };
                refundDTO.AcademicYear = new KeyValueDTO() { Key = Convert.ToString(getData.AcadamicYearID), Value = getData.AcademicYear.Description };
                refundDTO.AdmissionNo = getData.Student.AdmissionNumber;
                refundDTO.ClassName = getData.Class.ClassDescription;
                refundDTO.SectionName = getData.Section.SectionName;

                refundDTO.FeeCollectionPaymentModeMapDTO = new List<FeeCollectionPaymentModeMapDTO>();
                refundDTO.FeeTypes = new List<FeeCollectionFeeTypeDTO>();

                foreach (var payment in getData.RefundPaymentModeMaps)
                {
                    refundDTO.FeeCollectionPaymentModeMapDTO.Add(new FeeCollectionPaymentModeMapDTO()
                    {
                        PaymentMode = new KeyValueDTO() { Key = Convert.ToString(payment.PaymentModeID), Value = payment.PaymentMode.PaymentModeName },
                        TDate = payment.CreatedDate,
                        ReferenceNo = payment.ReferenceNo,
                        Amount = payment.Amount,
                    });
                }

                refundDTO.CollectedAmount = refundDTO.FeeCollectionPaymentModeMapDTO.Sum(x => x.Amount);

                foreach (var ft in getData.RefundFeeTypeMaps)
                {
                    var stFee = dbContext.FeeDueFeeTypeMaps
                        .Include(x => x.StudentFeeDue)
                        .FirstOrDefault(d => d.FeeDueFeeTypeMapsIID == ft.FeeDueFeeTypeMapsID);

                    var monthlysplit = new List<FeeCollectionMonthlySplitDTO>();

                    foreach (var monthly in ft.RefundMonthlySplits)
                    {
                        var month = dbContext.Months
                            .AsNoTracking()
                            .FirstOrDefault(m => m.MonthID == monthly.MonthID);

                        monthlysplit.Add(new FeeCollectionMonthlySplitDTO()
                        {
                            CreditNoteAmount = monthly.CreditNoteAmount,
                            DueAmount = monthly.DueAmount,
                            RefundAmount = monthly.RefundAmount,
                            MonthName = month.Description
                        });
                    }

                    refundDTO.FeeTypes.Add(new FeeCollectionFeeTypeDTO()
                    {
                        Amount = ft.Amount,
                        DueAmount = ft.DueAmount,
                        RefundAmount = ft.RefundAmount,
                        InvoiceNo = stFee.StudentFeeDue.InvoiceNo,
                        FeePeriodID = ft.FeePeriodID,
                        InvoiceDate = stFee.StudentFeeDue.InvoiceDate,
                        StudentFeeDueID = stFee.StudentFeeDue.StudentFeeDueIID,
                        FeeCycleID = ft.FeeMaster.FeeCycleID,
                        FeeDueFeeTypeMapsID = ft.FeeDueFeeTypeMapsID,
                        FeeMaster = ft.FeeMaster == null ? null : ft.FeeMaster.Description,
                        FeePeriod = ft.FeePeriodID.HasValue ? ft.FeePeriod.Description + " ( " + ft.FeePeriod.PeriodFrom.ToString("dd/MMM/yyy") + '-'
                                                             + ft.FeePeriod.PeriodTo.ToString("dd/MMM/yyy") + " ) " : null,
                        MontlySplitMaps = monthlysplit,
                    });
                }
            }

            #region old code -- commented
            //using (var dbContext = new dbEduegateSchoolContext())
            //{
            //    var feeDueDTO = (from stFee in dbContext.StudentFeeDues
            //                     join AcademicYear in dbContext.AcademicYears on stFee.AcadamicYearID equals AcademicYear.AcademicYearID
            //                     join FeeTypeMap in dbContext.FeeDueFeeTypeMaps on stFee.StudentFeeDueIID equals FeeTypeMap.StudentFeeDueID
            //                 join FeeMonthlySplit in dbContext.FeeDueMonthlySplits on FeeTypeMap.FeeDueFeeTypeMapsIID equals FeeMonthlySplit.FeeDueFeeTypeMapsID into tmpMonthlySplit
            //                 from FeeMonthlySplit in tmpMonthlySplit.DefaultIfEmpty()
            //                 where (stFee.StudentFeeDueIID == IID && stFee.CollectionStatus == false)
            //                 orderby stFee.InvoiceDate ascending

            //                 select new FeeCollectionDTO()
            //                 {
            //                     FeeCollectionIID = 0,
            //                     ClassID = stFee.ClassId,
            //                     StudentID = stFee.StudentId,
            //                     AcadamicYearID = stFee.AcadamicYearID,
            //                     SchoolID = stFee.SchoolID,
            //                     AcademicYear = new KeyValueDTO() { Key = Convert.ToString(stFee.AcademicYear.AcademicYearID), Value = stFee.AcademicYear.Description },
            //                     StudentName = stFee.Student.AdmissionNumber + '-' + stFee.Student.FirstName + ' ' + stFee.Student.MiddleName + ' ' + stFee.Student.LastName,
            //                     ClassName = stFee.Class.ClassDescription,
            //                     SectionID = stFee.Student.SectionID,
            //                     SectionName = stFee.Student.SectionID.HasValue ? stFee.Student.Section.SectionName : null,
            //                     AdmissionNo = stFee.Student.AdmissionNumber,
            //                     FeeTypes = (from ft in stFee.FeeDueFeeTypeMaps
            //                                 where ft.Status == false && ft.StudentFeeDueID == stFee.StudentFeeDueIID

            //                                 select new FeeCollectionFeeTypeDTO()
            //                                 {
            //                                     Amount = ft.Amount,
            //                                     //IsRowSelected = true,
            //                                     TaxAmount = ft.TaxAmount,
            //                                     InvoiceNo = stFee.InvoiceNo,
            //                                     FeePeriodID = ft.FeePeriodID,
            //                                     InvoiceDate = stFee.InvoiceDate,
            //                                     TaxPercentage = ft.TaxPercentage,
            //                                     StudentFeeDueID = ft.StudentFeeDueID,
            //                                     FeeCycleID = ft.FeeMaster.FeeCycleID,
            //                                     FeeStructureFeeMapID = ft.FeeStructureFeeMapID,
            //                                     FeeDueFeeTypeMapsID = ft.FeeDueFeeTypeMapsIID,
            //                                     IsFeePeriodDisabled = ft.FeeMaster.FeeCycleID.HasValue ? ft.FeeMaster.FeeCycleID.Value != 3 : true,

            //                                     FeeMaster = ft.FeeMaster == null ? null : ft.FeeMaster.Description,
            //                                     FeePeriod = ft.FeePeriodID.HasValue ? ft.FeePeriod.Description + " ( " + ft.FeePeriod.PeriodFrom.ToString("dd/MMM/yyy") + '-'
            //                                                 + ft.FeePeriod.PeriodTo.ToString("dd/MMM/yyy") + " ) " : null,

            //                                     MontlySplitMaps = (from mf in ft.FeeDueMonthlySplits
            //                                                        where mf.Status == false//.AsEnumerable()
            //                                                        select new FeeCollectionMonthlySplitDTO()
            //                                                        {
            //                                                            MonthID = mf.MonthID != 0 ? int.Parse(Convert.ToString(mf.MonthID)) : 0,
            //                                                            Amount = mf.Amount.HasValue ? mf.Amount : (decimal?)null,
            //                                                            TaxAmount = mf.TaxAmount.HasValue ? mf.TaxAmount : (decimal?)null,
            //                                                            TaxPercentage = mf.TaxPercentage.HasValue ? mf.TaxPercentage : (decimal?)null
            //                                                        }).ToList(),
            //                                 }).ToList(),
            //                 }).AsNoTracking().FirstOrDefault();
            //}
            #endregion

            return ToDTOString(refundDTO);
        }


        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as RefundDTO;
            decimal paidAmount = 0;
            decimal fineAmount = 0;
            decimal collectedAmount = 0;

            if (toDto.RefundIID != 0)
            {
                throw new Exception("Sorry , refund screen is not editable !");
            }

            if (!toDto.AcadamicYearID.HasValue)
            {
                throw new Exception("0#Please select Acadamic Year!");
            }
            string _sFeeMaster_IDs = null;

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

            if (toDto.FeeCollectionPaymentModeMapDTO.Select(x => x.Amount).Sum() == 0)
            {
                throw new Exception("The Payment amount cannot be zero!");
            }

            paidAmount = toDto.FeeCollectionPaymentModeMapDTO.Select(x => x.Amount.Value).Sum();
            collectedAmount = toDto.FeeTypes.Select(x => x.Balance.Value).Sum();

            fineAmount = (toDto.FeeFines == null || toDto.FeeFines.Count == 0) ? 0 : toDto.FeeFines.Select(x => x.Amount.Value).Sum();
            if (paidAmount != (collectedAmount + fineAmount))
            {
                throw new Exception("Amount need to be collected and Paid amount must be equal");
            }
            var monthlyClosingDate = new MonthlyClosingMapper().GetMonthlyClosingDate(toDto.SchoolID == null ? (long?)_context.SchoolID : toDto.SchoolID);

            if (monthlyClosingDate.HasValue && monthlyClosingDate.Value.Year > 1900 && toDto.RefundDate.Value.Date <= monthlyClosingDate.Value.Date)
            {
                throw new Exception("This Transaction could not be saved due to monthly closing");
            }

            if (toDto.FeeCollectionPaymentModeMapDTO.IsNull() || toDto.FeeCollectionPaymentModeMapDTO.Count() == 0)
            {
                return "0#There is no data found for account posting!";
            }

            Entity.Models.Settings.Sequence sequence = new Entity.Models.Settings.Sequence();
            //convert the dto to entity and pass to the repository.
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var studentAccountsId = new Domain.Setting.SettingBL(null).GetSettingValue<string>("STUDENT_ACCOUNT_ID");
                if (studentAccountsId.IsNull())
                {
                    return "0#There is no Student Account found!";
                }

                var documentTypeID = (from st in dbContext.DocumentTypes.Where(s => s.TransactionTypeName.ToUpper() == "JOURNAL").AsNoTracking() select st.DocumentTypeID).FirstOrDefault();
                if (documentTypeID.IsNull())
                {
                    throw new Exception("Document Type 'JOURNAL' is not available.Please check");
                }

                MutualRepository mutualRepository = new MutualRepository();

                List<long> _sLstFeeDueTypeMap_IDs = new List<long>();
                List<long> _sLstFeeDueMonthlyTypeMap_IDs = new List<long>();
                try
                {
                    sequence = mutualRepository.GetNextSequence("RefundNo", null);
                }
                catch (Exception ex)
                {
                    throw new Exception("Please generate sequence with 'RefundNo'");
                }
                var entity = new Refund()
                {
                    //FeeCollectionIID = toDto.FeeCollectionIID,
                    ClassID = toDto.ClassID,
                    SectionID = toDto.SectionID,
                    StudentID = toDto.StudentID,
                    AcadamicYearID = toDto.AcadamicYearID,
                    Amount = collectedAmount,
                    PaidAmount = paidAmount,
                    IsPaid = true,
                    FeeReceiptNo = sequence.Prefix + sequence.LastSequence,
                    SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                    RefundDate = toDto.RefundDate.IsNotNull() ? Convert.ToDateTime(toDto.RefundDate) : DateTime.Now,
                    CreatedBy = toDto.RefundIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                    UpdatedBy = toDto.RefundIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                    CreatedDate = toDto.RefundIID == 0 ? DateTime.Now : dto.CreatedDate,
                    UpdatedDate = toDto.RefundIID > 0 ? DateTime.Now : dto.UpdatedDate,
                    //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
                };

                dbContext.Refunds.Add(entity);

                /*//newly added   */
                entity.RefundPaymentModeMaps = new List<RefundPaymentModeMap>();
                foreach (var paymentType in toDto.FeeCollectionPaymentModeMapDTO)
                {
                    entity.RefundPaymentModeMaps.Add(new RefundPaymentModeMap()
                    {
                        Amount = paymentType.Amount,
                        RefundAmount = paymentType.Amount < 0 ? paymentType.Amount : 0,
                        Balance = paymentType.Amount,
                        CreatedDate = System.DateTime.Now,
                        PaymentModeID = paymentType.PaymentModeID,
                        ReferenceNo = paymentType.ReferenceNo
                    });
                }

                entity.RefundFeeTypeMaps = new List<RefundFeeTypeMap>();
                foreach (var feeType in toDto.FeeTypes)
                {
                    var monthlySplit = new List<RefundMonthlySplit>();
                    foreach (var feeMasterMonthlyDto in feeType.MontlySplitMaps)
                    {
                        var entityChild = new RefundMonthlySplit()
                        {
                            CreditNoteAmount = feeMasterMonthlyDto.CreditNoteAmount,
                            Balance = feeMasterMonthlyDto.Balance,
                            FeeDueMonthlySplitID = feeMasterMonthlyDto.FeeDueMonthlySplitID,
                            MonthID = byte.Parse(feeMasterMonthlyDto.MonthID.ToString()),
                            Year = feeMasterMonthlyDto.Year,
                            FeePeriodID = feeType.FeePeriodID,
                            Amount = feeMasterMonthlyDto.PrvCollect.HasValue ? feeMasterMonthlyDto.PrvCollect : (decimal?)null,
                            DueAmount = feeMasterMonthlyDto.Amount,
                            RefundAmount = feeMasterMonthlyDto.RefundAmount,
                            NowPaying = feeMasterMonthlyDto.NowPaying,
                        };
                        monthlySplit.Add(entityChild);
                    }

                    entity.RefundFeeTypeMaps.Add(new RefundFeeTypeMap()
                    {
                        FeeCollectionFeeTypeMapsID = feeType.FeeCollectionFeeTypeMapsIID == 0 ? (long?)null : feeType.FeeCollectionFeeTypeMapsIID,
                        FeeMasterID = feeType.FeeMasterID,
                        FeePeriodID = feeType.FeePeriodID,
                        RefundAmount = feeType.RefundAmount,
                        Balance = feeType.Balance,//(feeType.CollectedAmount.HasValue ? feeType.CollectedAmount.Value : 0) - (feeType.RefundAmount.HasValue ? feeType.RefundAmount.Value : 0),
                        Amount = feeType.CollectedAmount.HasValue ? feeType.CollectedAmount : (decimal?)null,
                        CollectedAmount = feeType.CollectedAmount.HasValue ? feeType.CollectedAmount : (decimal?)null,
                        DueAmount = feeType.DueAmount,
                        FeeDueFeeTypeMapsID = feeType.FeeDueFeeTypeMapsID,
                        NowPaying = feeType.NowPaying,
                        RefundMonthlySplits = monthlySplit
                    });

                    if (feeType.FeeDueFeeTypeMapsID.HasValue)
                    {
                        var feeDueEntity = new StudentFeeDue();

                        feeDueEntity = dbContext.StudentFeeDues
                           .Include(x => x.FeeDueFeeTypeMaps)
                           .FirstOrDefault(x => x.StudentFeeDueIID == feeType.StudentFeeDueID);

                        if (feeDueEntity != null)
                        {
                            feeDueEntity.CollectionStatus = true;

                            dbContext.StudentFeeDues.Add(feeDueEntity);

                            feeDueEntity.FeeDueFeeTypeMaps.FirstOrDefault(y => y.FeeDueFeeTypeMapsIID == feeType.FeeDueFeeTypeMapsID).CollectedAmount = feeType.Balance;

                            dbContext.Entry(feeDueEntity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                    }
                }

                if (entity.RefundIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    foreach (var paymentMode in entity.RefundFeeTypeMaps)
                    {
                        if (paymentMode.RefundFeeTypeMapsIID != 0)
                        {
                            dbContext.Entry(paymentMode).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                        else
                        {
                            dbContext.Entry(paymentMode).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                    }

                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();

                toDto.RefundIID = entity.RefundIID;

                var message = AccountPosting(toDto, documentTypeID, studentAccountsId);
                string[] resp = message.Split('#');
                if (resp[0] == "0")
                    throw new Exception(resp[1]);

                return GetEntity(toDto.RefundIID);
            }
        }

        public string AccountPosting(RefundDTO toDto, int documentTypeID, string studentAccountsId)
        {
            List<FeeCollectionPaymentModeMapDTO> feePaymentDTO = new List<FeeCollectionPaymentModeMapDTO>();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                int loginID = Convert.ToInt32(_context.LoginID);
                new AccountEntryMapper().AccountTransMerge(toDto.RefundIID, toDto.RefundDate.Value, loginID, 7);//Refund Account Entry

                var entityFinal = dbContext.Refunds.Where(x => x.RefundIID == toDto.RefundIID).AsNoTracking().FirstOrDefault();

                entityFinal.IsAccountPosted = true;

                dbContext.Entry(entityFinal).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                dbContext.SaveChanges();

                return "1#Saved successfully!";
            }
        }
        #region Final  

        public List<FeeCollectionDTO> FillCollectedFeesDetails(long studentID, int academicYearID)
        {
            var _sRetData = new List<FeeCollectionDTO>();
            using (var _sContext = new dbEduegateSchoolContext())
            {
                var dFnd = (from n in _sContext.StudentFeeDues
                            join f in _sContext.FeeDueFeeTypeMaps on n.StudentFeeDueIID equals f.StudentFeeDueID
                            join fm in _sContext.FeeMasters on f.FeeMasterID equals fm.FeeMasterID
                            where n.StudentId == studentID && n.Student.Status != 3 && n.Student.IsActive == true && n.AcadamicYearID == academicYearID &&
                             (n.CollectionStatus == true || fm.FeeType.IsRefundable == true)
                            select n).Include(w => w.FeeDueFeeTypeMaps).AsNoTracking().ToList();

                if (dFnd.Any())
                {
                    dFnd.All(w => { _sRetData.Add(GetFeeDetails(_sContext, w)); return true; });
                }
            }
            return _sRetData;
        }

        private FeeCollectionDTO GetFeeDetails(dbEduegateSchoolContext _sContext, StudentFeeDue _sFeeDue)
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
                FeeTypes = GetFeeDueTypeMap(_sContext, _sFeeDue),
                SchoolID = _sFeeDue.SchoolID,
                SectionID = _sFeeDue.SectionID,
                SectionName = string.Empty,
                StudentID = _sFeeDue.StudentId,
                StudentName = string.Empty,
            };

            return _sRetData;

        }

        private List<FeeCollectionFeeTypeDTO> GetFeeDueTypeMap(dbEduegateSchoolContext _sContext, StudentFeeDue _sFeeDue)
        {
            var _sRetData = new List<FeeCollectionFeeTypeDTO>();
            _sFeeDue.FeeDueFeeTypeMaps.ToList().All(w => { _sRetData.Add(GetFeeDueTypeMap(_sContext, _sFeeDue, w)); return true; });
            return _sRetData;
        }

        private FeeCollectionFeeTypeDTO GetFeeDueTypeMap(dbEduegateSchoolContext sContext, StudentFeeDue sFeeDue, FeeDueFeeTypeMap sFeeDueMap)
        {
            var dFeeMaster = sContext.FeeMasters.Where(w => w.FeeMasterID == (sFeeDueMap.FeeMasterID ?? 0))
                .Include(i => i.FeeType)
                .AsNoTracking().FirstOrDefault();

            var dFeePeriod = sContext.FeePeriods.Where(w => w.FeePeriodID == (sFeeDueMap.FeePeriodID ?? 0))
                .AsNoTracking().FirstOrDefault();

            var feeCycleID = dFeeMaster.FeeCycleID;

            var dCreditNotes = GetCreditNote(sContext, sFeeDue, sFeeDueMap);

            var dMontlySplit = feeCycleID != 1 ? new List<FeeCollectionMonthlySplitDTO>() : GetMonthlySplitFor(sContext, sFeeDueMap);
            decimal sAmountDue = sFeeDueMap.Amount ?? 0, _sCreditNoteAmount = 0, _sAmountCollected = 0, sBalance = 0;
            var isRefundable = dFeeMaster.FeeType.IsRefundable;

            var draftFeeCollectionStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("FEECOLLECTIONSTATUSID_DRAFT", "1");

            int draftStatusID = int.Parse(draftFeeCollectionStatus);

            var feeCollectionFeeTypeMaps = sContext.FeeCollectionFeeTypeMaps.Where(c => c.FeeDueFeeTypeMapsID == sFeeDueMap.FeeDueFeeTypeMapsIID).AsNoTracking().FirstOrDefault();

            #region Partial Receipt => Checking and Allocations
            if (dMontlySplit.Any())
            {
                var dCollectionMonthly = (from n in sContext.FeeCollectionMonthlySplits
                                          join m in sContext.FeeCollectionFeeTypeMaps on n.FeeCollectionFeeTypeMapId equals m.FeeCollectionFeeTypeMapsIID
                                          where m.FeeDueFeeTypeMapsID == sFeeDueMap.FeeDueFeeTypeMapsIID && (m.FeeCollection.FeeCollectionStatusID ?? 0) != draftStatusID
                                          group n by new { n.FeeDueMonthlySplitID, m.FeeDueFeeTypeMapsID, m.FeeCollectionFeeTypeMapsIID, m.FeeCollectionID, n.FeeCollectionMonthlySplitIID } into grp
                                          select new
                                          {
                                              FeeDueMonthlySplitID = grp.Key.FeeDueMonthlySplitID,
                                              FeeDueFeeTypeMapsID = grp.Key.FeeDueFeeTypeMapsID,
                                              FeeCollectionFeeTypeMapsID = grp.Key.FeeCollectionFeeTypeMapsIID,
                                              FeeCollectionID = grp.Key.FeeCollectionID,
                                              FeeCollectionMonthlySplitIID = grp.Key.FeeCollectionMonthlySplitIID,
                                              CollectedAmount = grp.Sum(w => w.Amount),
                                              CreditNoteAmount = grp.Sum(w => w.CreditNoteAmount),
                                          }).AsNoTracking();
                if (dCollectionMonthly.Any())
                {
                    dMontlySplit.All(w =>
                    {
                        if (dCollectionMonthly.Any(x => x.FeeDueFeeTypeMapsID == w.FeeDueFeeTypeMapsID && x.FeeDueMonthlySplitID == w.FeeDueMonthlySplitID))
                        {

                            w.CreditNoteAmount = dCollectionMonthly.Where(x => x.FeeDueFeeTypeMapsID == w.FeeDueFeeTypeMapsID && x.FeeDueMonthlySplitID == w.FeeDueMonthlySplitID).Sum(k => k.CreditNoteAmount) ?? 0;
                            w.CollectedAmount = dCollectionMonthly.Where(x => x.FeeDueFeeTypeMapsID == w.FeeDueFeeTypeMapsID && x.FeeDueMonthlySplitID == w.FeeDueMonthlySplitID).Sum(k => k.CollectedAmount) ?? 0;
                            w.RefundAmount = isRefundable == true ? w.CollectedAmount : (w.CollectedAmount > (w.Amount - w.CreditNoteAmount)) ?
                                                             w.CollectedAmount - (w.Amount - w.CreditNoteAmount) : 0;
                            w.PrvCollect = dCollectionMonthly.Where(x => x.FeeDueFeeTypeMapsID == w.FeeDueFeeTypeMapsID && x.FeeDueMonthlySplitID == w.FeeDueMonthlySplitID).Sum(k => k.CollectedAmount) ?? 0;

                            w.FeeCollectionFeeTypeMapId = dCollectionMonthly.Where(x => x.FeeDueFeeTypeMapsID == w.FeeDueFeeTypeMapsID).Select(x => x.FeeCollectionFeeTypeMapsID).FirstOrDefault();
                            w.FeeCollectionMonthlySplitIID = dCollectionMonthly.Where(x => x.FeeDueFeeTypeMapsID == w.FeeDueFeeTypeMapsID).Select(x => x.FeeCollectionMonthlySplitIID).FirstOrDefault();
                        }
                        return true;
                    }
                    );

                    _sAmountCollected = dCollectionMonthly.Sum(w => w.CollectedAmount) ?? 0;
                }
                else
                {
                    var dCollection = (from n in sContext.FeeCollectionFeeTypeMaps
                                       where n.FeeDueFeeTypeMapsID == sFeeDueMap.FeeDueFeeTypeMapsIID && (n.FeeCollection.FeeCollectionStatusID ?? 0) != draftStatusID
                                       group n by n.FeeDueFeeTypeMapsID into grp
                                       select new
                                       {
                                           FeeDueFeeTypeMapsID = grp.Key.Value,
                                           CreditNoteAmount = grp.Sum(w => w.CreditNoteAmount),
                                           CollectedAmount = grp.Sum(w => w.Amount)
                                       }).AsNoTracking();

                    if (dCollection.Any())
                        _sAmountCollected = dCollection.Sum(w => w.CollectedAmount ?? 0);

                    dMontlySplit.All(w =>
                    {
                        // w.feedue
                        w.ReceivableAmount = isRefundable == false ?
                                          (w.Amount - w.CreditNoteAmount ?? 0) - (w.CollectedAmount ?? 0) : 0;
                        w.RefundAmount = 0;
                        w.PrvCollect = 0;
                        w.CollectedAmount = 0;
                        return true;
                    });
                }

            }
            else
            {
                var dCollection = (from n in sContext.FeeCollectionFeeTypeMaps
                                   where n.FeeDueFeeTypeMapsID == sFeeDueMap.FeeDueFeeTypeMapsIID && (n.FeeCollection.FeeCollectionStatusID ?? 0) != draftStatusID
                                   group n by n.FeeDueFeeTypeMapsID into grp
                                   select new
                                   {
                                       FeeDueFeeTypeMapsID = grp.Key.Value,
                                       CollectedAmount = grp.Sum(w => w.Amount)
                                   }).AsNoTracking();

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
                FeeCollectionFeeTypeMapsIID = feeCollectionFeeTypeMaps == null ? 0 : feeCollectionFeeTypeMaps.FeeCollectionFeeTypeMapsIID,
                FeeMaster = dFeeMaster.Description,
                FeeMasterID = sFeeDueMap.FeeMasterID,
                FeePeriod = sFeeDueMap.FeePeriodID != null ? dFeePeriod.Description : "",
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

            _sRetData.RefundAmount = _sRetData.IsRefundable == true ? _sRetData.CollectedAmount : (_sRetData.CollectedAmount > (_sRetData.Amount - _sRetData.CreditNoteAmount) && _sRetData.CollectedAmount != 0) ?
                                            _sRetData.CollectedAmount - (_sRetData.Amount - _sRetData.CreditNoteAmount) : 0;
            return _sRetData;

        }

        private List<FeeCollectionMonthlySplitDTO> GetMonthlySplitFor(dbEduegateSchoolContext _sContext, FeeDueFeeTypeMap _sFeeDueMap)
        {
            var _sRetData = new List<FeeCollectionMonthlySplitDTO>();
            if ((_sFeeDueMap.FeePeriodID ?? 0) == 0)
                return _sRetData;

            var dFnd = (from n in _sContext.FeeDueMonthlySplits.Where(w => w.FeeDueFeeTypeMapsID == _sFeeDueMap.FeeDueFeeTypeMapsIID)
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
                        }).AsNoTracking();

            if (dFnd.Any())
                _sRetData.AddRange(dFnd);
            return _sRetData;
        }

        private bool SetCreditNoteMonthly(dbEduegateSchoolContext _sContext, FeeCollectionMonthlySplitDTO _sFeeMonthlySplit, List<CreditNoteFeeTypeMap> _sCreditNotes)
        {
            bool _sFlg = false;
            if (!_sCreditNotes.Any())
                return _sFlg;
            var dFnd = _sCreditNotes.Where(w => w.Year == _sFeeMonthlySplit.Year && w.MonthID == _sFeeMonthlySplit.MonthID);
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

        private List<CreditNoteFeeTypeMap> GetCreditNote(dbEduegateSchoolContext _sContext, StudentFeeDue _sFeeDue, FeeDueFeeTypeMap _sFeeDueMap)
        {
            List<CreditNoteFeeTypeMap> _sRetData = new List<CreditNoteFeeTypeMap>();
            var dFnd = (from n in _sContext.SchoolCreditNotes
                        join m in _sContext.CreditNoteFeeTypeMaps on n.SchoolCreditNoteIID equals m.SchoolCreditNoteID
                        where n.StudentID == _sFeeDue.StudentId && n.ClassID == _sFeeDue.ClassId
                        && m.FeeMasterID == _sFeeDueMap.FeeMasterID && (_sFeeDueMap.FeePeriodID == null || m.PeriodID == _sFeeDueMap.FeePeriodID)
                        select m).Include(w => w.SchoolCreditNote).AsNoTracking();
            if (dFnd.Any())
                _sRetData.AddRange(dFnd);
            return _sRetData;
        }

        #endregion
    }
}
