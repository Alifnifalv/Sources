using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Framework;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Fees;
using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Framework.Extensions;
using System.Data;
using Eduegate.Domain.Mappers.School.Accounts;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Eduegate.Domain.Mappers.School.Fees
{
    public class CollectFeeAccountPostingMapper : DTOEntityDynamicMapper
    {
        private CallContext _context;
        public static CollectFeeAccountPostingMapper Mapper(CallContext context)
        {
            var mapper = new CollectFeeAccountPostingMapper();
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

        private static string GetTenderName(int TenderID)
        {
            string _sRetData = string.Empty;
            switch (TenderID)
            {
                case 1:
                    _sRetData = "Cash";
                    break;
                case 3:
                case 4:
                    _sRetData = "Bank";
                    break;
                case 5:
                case 6:
                    _sRetData = "Online";
                    break;
                default:
                    _sRetData = "Others";
                    break;

            }

            return _sRetData;

        }

        public List<CollectFeeAccountDetailDTO> GetCollectFeeAccountData(DateTime fromDate, DateTime toDate, long CashierID)
        {
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");
            List<CollectFeeAccountDetailDTO> FeeCollection = new List<CollectFeeAccountDetailDTO>();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var draftStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<int>("FEECOLLECTIONSTATUSID_DRAFT", 1);

                #region Fee Collection 
                var dCollection = (from feecollect in dbContext.FeeCollections
                                   where (feecollect.IsCancelled != true && (feecollect.FeeCollectionStatusID ?? 0) != draftStatusID && feecollect.CollectionDate.Value.Date >= fromDate.Date && feecollect.CollectionDate.Value.Date <= toDate.Date)
                                   && feecollect.CashierID == CashierID
                                   select new CollectFeeAccountDetailDTO()
                                   {
                                       FeeCollectionID = feecollect.FeeCollectionIID,
                                       ReceiptNo = feecollect.FeeReceiptNo,
                                       StudentId = feecollect.StudentID,
                                       Student = feecollect.Student.AdmissionNumber,
                                       CollectionDateString = feecollect.CollectionDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture),
                                       CollectionType = 0,
                                       Amount = feecollect.Amount ?? 0,
                                       IsPosted = feecollect.IsAccountPosted,
                                       GroupTransactionNumber=feecollect.GroupTransactionNumber
                                   }).AsNoTracking().ToList();

                if (dCollection.Any())
                {
                    dCollection.All(w =>
                    {
                        w.CollectFeePaymentModeList = new List<CollectFeePaymentModeDTO>();
                        w.FeeAccountSplit = new List<CollectFeeAccountSplitDTO>();

                        #region Fee Payment Modes
                        var dPaymentModes = from n in dbContext.FeeCollectionPaymentModeMaps.AsNoTracking()
                                            where n.FeeCollectionID == w.FeeCollectionID
                                            group n by new
                                            {

                                                PaymentName = (n.PaymentMode.TenderTypeID == 1 ? "Cash" :
                                                (n.PaymentMode.TenderTypeID == 3) ? "Card" : (n.PaymentMode.TenderTypeID == 4) ? "Cheque" :
                                                (n.PaymentMode.TenderTypeID == 5) ? "Bank" : (n.PaymentMode.TenderTypeID == 6) ? "OnlineDirect" : "Others"
                                                )
                                                //PaymentModeID = (n.PaymentModeID ?? 0), PaymentName = n.PaymentMode.PaymentModeName ?? string.Empty 

                                            } into grp
                                            select new CollectFeePaymentModeDTO
                                            {
                                                Amount = grp.Sum(x => x.Amount),
                                                PaymentModeName = grp.Key.PaymentName,
                                                //PaymentModeTypeID = grp.Key.PaymentModeID
                                            };

                        if (dPaymentModes.Any())
                        {
                            w.CollectFeePaymentModeList.AddRange(dPaymentModes);
                        }
                        #endregion

                        #region Fees Details
                        //var dFees = (from split in dbContext.FeeCollectionFeeTypeMaps
                        //             join feecollect in dbContext.FeeCollections on split.FeeCollectionID equals feecollect.FeeCollectionIID
                        //             join feemst in dbContext.FeeMasters on split.FeeMasterID equals feemst.FeeMasterID
                        //             join acc in dbContext.Accounts on feemst.LedgerAccountID equals acc.AccountID
                        //             where (feecollect.FeeCollectionIID == w.FeeCollectionID)
                        //             group split by new { split.Amount, split.FeePeriod, split.FeeMaster } into ftGroup1
                        //             select new CollectFeeAccountSplitDTO()
                        //             {
                        //                 // FeePeriod = ftGroup1.Key.FeePeriod == null ? null : ftGroup1.Key.FeePeriod.Description + " ( " + ftGroup1.Key.FeePeriod.PeriodFrom.ToString("dd/MMM/yyy") + '-'
                        //                 //+ ftGroup1.Key.FeePeriod.PeriodTo.ToString("dd/MMM/yyy") + " ) ",
                        //                 FeeMaster = ftGroup1.Key.FeeMaster.Description,
                        //                 Amount = ftGroup1.Key.Amount.HasValue ? ftGroup1.Key.Amount.Value : 0,
                        //                 AccountID = ftGroup1.Key.FeeMaster.LedgerAccountID,
                        //                 CollectionType = 0
                        //             }).AsNoTracking();

                        var clFeesEntity = dbContext.FeeCollectionFeeTypeMaps
                            .Where(x => x.FeeCollectionID == w.FeeCollectionID)
                            .Include(x => x.FeeMaster)
                            .Include(x => x.FeePeriod)
                            .AsNoTracking()
                            .ToList();

                        var dFees = clFeesEntity
                            .GroupBy(g => new { g.Amount, g.FeePeriodID, g.FeeMasterID })
                            .Select(group => new CollectFeeAccountSplitDTO
                            {
                                FeeMaster = group.First().FeeMaster.Description,
                                Amount = group.Key.Amount ?? 0,
                                AccountID = group.First().FeeMaster.LedgerAccountID,
                                CollectionType = 0
                            })
                            .ToList();



                        if (dFees.Any())
                            w.FeeAccountSplit.AddRange(dFees);
                        #endregion

                        #region Fines Details
                        //var dFines = (from split in dbContext.FeeCollectionFeeTypeMaps
                        //              join feecollect in dbContext.FeeCollections on split.FeeCollectionID equals feecollect.FeeCollectionIID
                        //              join feemst in dbContext.FineMasters on split.FineMasterID equals feemst.FineMasterID
                        //              join acc in dbContext.Accounts on feemst.LedgerAccountID equals acc.AccountID
                        //              where (feecollect.FeeCollectionIID == w.FeeCollectionID)
                        //              group split by new { split.Amount, split.FeePeriod, split.FineMaster } into ftGroup1
                        //              select new CollectFeeAccountSplitDTO()
                        //              {
                        //                  //FeePeriod = ftGroup1.Key.FeePeriod == null ? null : ftGroup1.Key.FeePeriod.Description + " ( " + ftGroup1.Key.FeePeriod.PeriodFrom.ToString("dd/MMM/yyy") + '-'
                        //                  //+ ftGroup1.Key.FeePeriod.PeriodTo.ToString("dd/MMM/yyy") + " ) ",
                        //                  FeeMaster = ftGroup1.Key.FineMaster.FineName,
                        //                  Amount = ftGroup1.Key.Amount.HasValue ? ftGroup1.Key.Amount.Value : 0,
                        //                  AccountID = ftGroup1.Key.FineMaster.LedgerAccountID,
                        //                  CollectionType = 0
                        //              }).AsNoTracking();

                        //if (dFines.Any())
                        //    w.FeeAccountSplit.AddRange(dFines);
                        #endregion

                        return true;
                    });

                    FeeCollection.AddRange(dCollection);
                }
                #endregion

                //#region Final Settlement
                //var dSettlement = (from feecollect in dbContext.FinalSettlements.AsEnumerable()
                //                   where (feecollect.IsAccountPosted == false && (feecollect.FinalSettlementDate.Value.Date >= fromDate.Date && feecollect.FinalSettlementDate.Value.Date <= toDate.Date))
                //                   //&& feecollect.CashierID == CashierID
                //                   group feecollect by new
                //                   {
                //                       feecollect.FinalSettlementIID,
                //                       feecollect.FinalSettlementDate,
                //                       feecollect.FinalSettlementFeeTypeMaps,
                //                       feecollect.FeeReceiptNo,
                //                       feecollect.StudentID,
                //                       Student = feecollect.Student.AdmissionNumber
                //                   } into ft
                //                   select new CollectFeeAccountDetailDTO()
                //                   {
                //                       FeeCollectionID = ft.Key.FinalSettlementIID,
                //                       ReceiptNo = ft.Key.FeeReceiptNo,
                //                       StudentId = ft.Key.StudentID,
                //                       Student = ft.Key.Student,
                //                       CollectionDateString = ft.Key.FinalSettlementDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture),
                //                       CollectionType = 1
                //                   }).ToList();
                //if (dSettlement.Any())
                //{
                //    dSettlement.All(w =>
                //    {
                //        #region Final Settlement Details
                //        w.FeeAccountSplit = new List<CollectFeeAccountSplitDTO>();
                //        var dSettlementTail = (from split in dbContext.FinalSettlementFeeTypeMaps
                //                               join feecollect in dbContext.FinalSettlements on split.FinalSettlementID equals feecollect.FinalSettlementIID
                //                               join feemst in dbContext.FeeMasters on split.FeeMasterID equals feemst.FeeMasterID
                //                               join acc in dbContext.Accounts on feemst.LedgerAccountID equals acc.AccountID
                //                               where (feecollect.FinalSettlementIID == w.FeeCollectionID)
                //                               group split by new { split.RefundAmount, split.FeePeriod, split.FeeMaster } into ftGroup1
                //                               select new CollectFeeAccountSplitDTO()
                //                               {
                //                                   // FeePeriod = ftGroup1.Key.FeePeriod == null ? null : ftGroup1.Key.FeePeriod.Description + " ( " + ftGroup1.Key.FeePeriod.PeriodFrom.ToString("dd/MMM/yyy") + '-'
                //                                   //+ ftGroup1.Key.FeePeriod.PeriodTo.ToString("dd/MMM/yyy") + " ) ",
                //                                   FeeMaster = ftGroup1.Key.FeeMaster.Description,
                //                                   Amount = ftGroup1.Key.RefundAmount.HasValue ? ftGroup1.Key.RefundAmount.Value : 0,
                //                                   AccountID = ftGroup1.Key.FeeMaster.LedgerAccountID,
                //                                   CollectionType = 1
                //                               });
                //        if (dSettlementTail.Any())
                //            w.FeeAccountSplit.AddRange(dSettlementTail);
                //        #endregion
                //        return true;
                //    }
                //    );
                //    FeeCollection.AddRange(dSettlement);
                //}
                //#endregion

            }

            return FeeCollection;
        }

        //public List<CollectFeeAccountDTO> GetCollectFeeAccountData(DateTime fromDate, DateTime toDate)
        //{
        //    List<CollectFeeAccountDTO> feeDueDTO = new List<CollectFeeAccountDTO>();
        //    using (var dbContext = new dbEduegateSchoolContext())
        //    {
        //        feeDueDTO = (from feemst in dbContext.FeeMasters.AsEnumerable()
        //                     join acc in dbContext.Accounts on feemst.LedgerAccountID equals acc.AccountID
        //                     join feecollectfeetype in dbContext.FeeCollectionFeeTypeMaps on feemst.FeeMasterID equals feecollectfeetype.FeeMasterID
        //                     join feecollect in dbContext.FeeCollections on feecollectfeetype.FeeCollectionID equals feecollect.FeeCollectionIID
        //                     where (feecollect.CollectionDate.Value.Date >= fromDate.Date && feecollect.CollectionDate.Value.Date < toDate.Date)
        //                     group feecollect by new { feecollect.StudentID, feecollect.Student, feecollect.FeeCollectionIID, feecollect.FeeReceiptNo, feecollect.CollectionDate, feecollect.FeeCollectionFeeTypeMaps } into ft
        //                     select new CollectFeeAccountDTO()
        //                     {
        //                         CollectionDateFrom = ft.Key.CollectionDate,
        //                         CollectionDateTo = ft.Key.CollectionDate,
        //                         MainDataDto = (from ftc in ft.Key.FeeCollectionFeeTypeMaps//.AsEnumerable().AsQueryable()

        //                                        group ftc by new { ftc.Amount, ftc.TaxAmount, ftc.FeePeriodID, ftc.TaxPercentage, ftc.FeeMaster, ftc.FeePeriod, ftc.FeeCollectionMonthlySplits } into ftGroup
        //                                        select new CollectFeeAccountMainDTO()
        //                                        {

        //                                            Amount = ftGroup.Key.Amount.Value,
        //                                            FeeAccountDetail = (from ftc in ft.Key.FeeCollectionFeeTypeMaps//.AsEnumerable().AsQueryable()

        //                                                                group ftc by new { ftc.FeeCollectionMonthlySplits } into ftGroup1
        //                                                                select new CollectFeeAccountDetailDTO()
        //                                                                {
        //                                                                    ReceiptNo = ft.Key.FeeReceiptNo,
        //                                                                    StudentId = ft.Key.StudentID,
        //                                                                    CollectionDateString = ft.Key.CollectionDate.Value.ToString("dd/mm/yyyy"),
        //                                                                    FeeAccountSplit = (from ftc in ft.Key.FeeCollectionFeeTypeMaps//.AsEnumerable().AsQueryable()

        //                                                                                       group ftc by new { ftc.Amount, ftc.FeeMaster, ftc.FeePeriod, ftc.FeeCollectionMonthlySplits } into ftGroup2
        //                                                                                       select new CollectFeeAccountSplitDTO()
        //                                                                                       {
        //                                                                                           FeePeriod = !ftGroup.Key.FeePeriodID.HasValue ? null : ftGroup.Key.FeePeriod.Description + " ( " + ftGroup.Key.FeePeriod.PeriodFrom.ToString("dd/MMM/yyy") + '-'
        //                                                          + ftGroup.Key.FeePeriod.PeriodTo.ToString("dd/MMM/yyy") + " ) ",
        //                                                                                           FeeMaster = ftGroup.Key.FeeMaster.Description,
        //                                                                                           Amount = ftGroup2.Key.Amount.Value,
        //                                                                                       }).ToList(),
        //                                                                }).ToList(),
        //                                        }).ToList(),
        //                     }).ToList();
        //    }
        //    return feeDueDTO;
        //}

        public string SaveFeeAccount(DateTime fromDate, DateTime toDate, long cashierID)
        {

            if (fromDate == null || toDate == null)
            {
                return "0#Collection from date and to date can not be left empty!";
            }
            if (fromDate.Date > toDate.Date)
            {
                return "0#Collection from date must be less than to date!";
            }
            List<FeeCollectionPaymentModeMapDTO> feePaymentDTO = new List<FeeCollectionPaymentModeMapDTO>();
            List<FeeCollectionDTO> collectionDTO = new List<FeeCollectionDTO>();

            List<FeeCollectionPaymentModeMapDTO> settlementPaymentDTO = new List<FeeCollectionPaymentModeMapDTO>();
            List<FeeCollectionDTO> settlementDTO = new List<FeeCollectionDTO>();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var studentAccountsName = (from st in dbContext.Accounts where st.AccountID == 1 select st.AccountName).FirstOrDefault();

                var studentAccountsId = new Domain.Setting.SettingBL(null).GetSettingValue<long>("STUDENT_ACCOUNT_ID", 0);
                var draftStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<int>("FEECOLLECTIONSTATUSID_DRAFT", 1);

                collectionDTO = (from feecollect in dbContext.FeeCollections
                                 where (feecollect.IsAccountPosted == false
                                 && (feecollect.FeeCollectionStatusID ?? 0) != draftStatusID
                                 && feecollect.CashierID == cashierID
                                 && (feecollect.CollectionDate.Value.Date >= fromDate.Date && feecollect.CollectionDate.Value.Date <= toDate.Date))
                                 select new FeeCollectionDTO()
                                 {
                                     FeeCollectionIID = feecollect.FeeCollectionIID,
                                     StudentID = feecollect.StudentID,
                                     ClassID = feecollect.ClassID,
                                     PaidAmount = feecollect.PaidAmount,
                                     FeeReceiptNo = feecollect.FeeReceiptNo,
                                     FeeCollectionPaymentModeMapDTO =
                                      (from split in feecollect.FeeCollectionPaymentModeMaps
                                       where (split.FeeCollectionID == feecollect.FeeCollectionIID)
                                       group split by new { split.FeeCollectionID, split.PaymentModeID, split.PaymentMode, split.Amount } into ft
                                       select new FeeCollectionPaymentModeMapDTO()
                                       {
                                           FeeCollectionID = ft.Key.FeeCollectionID,
                                           PaymentModeID = ft.Key.PaymentModeID,
                                           AccountId = ft.Key.PaymentMode.AccountId,
                                           Amount = ft.Key.Amount,
                                       }).ToList()
                                 }).AsNoTracking().ToList();

                //settlementDTO = (from feecollect in dbContext.FinalSettlements
                //                 where (feecollect.IsAccountPosted == false && (DbFunctions.TruncateTime(feecollect.FinalSettlementDate.Value) >= fromDate && DbFunctions.TruncateTime(feecollect.FinalSettlementDate.Value) <= toDate))
                //                 select new FeeCollectionDTO()
                //                 {
                //                     FeeCollectionIID = feecollect.FinalSettlementIID,
                //                     StudentID = feecollect.StudentID,
                //                     ClassID = feecollect.ClassID,
                //                     PaidAmount = feecollect.PaidAmount,
                //                     FeeReceiptNo = feecollect.FeeReceiptNo,
                //                     FeeCollectionPaymentModeMapDTO =
                //                      (from split in feecollect.FinalSettlementPaymentModeMaps
                //                       where (split.FinalSettlementID == feecollect.FinalSettlementIID)
                //                       group split by new { split.FinalSettlementID, split.PaymentModeID, split.PaymentMode, split.Amount } into ft
                //                       select new FeeCollectionPaymentModeMapDTO()
                //                       {
                //                           FeeCollectionID = ft.Key.FinalSettlementID,
                //                           PaymentModeID = ft.Key.PaymentModeID,
                //                           AccountId = ft.Key.PaymentMode.AccountId,
                //                           Amount = ft.Key.Amount,
                //                       }).ToList()settlementDTO
                //                 }).ToList();

                if ((collectionDTO.IsNull() || collectionDTO.Count() == 0))
                    return "0#There is no data found for account posting!";

                var documentTypeID = (from st in dbContext.DocumentTypes.Where(s => s.TransactionTypeName.ToUpper() == "FEE RECEIPTS").AsNoTracking() select st.DocumentTypeID).FirstOrDefault();
                if (documentTypeID.IsNull())
                    return "0# Document Type 'FEE RECEIPTS' is not available.Please check";

                var settlementDocumentTypeID = (from st in dbContext.DocumentTypes.Where(s => s.TransactionTypeName.ToUpper() == "FEE PAYMENTS").AsNoTracking() select st.DocumentTypeID).FirstOrDefault();
                if (settlementDocumentTypeID.IsNull())
                    return "0# Document Type 'Fee Payments' is not available.Please check";




                #region Fee Collection
                if (collectionDTO.Count() > 0)
                {

                    string _colle_IDs = string.Empty;
                    if (collectionDTO != null && collectionDTO.Any())
                        _colle_IDs = string.Join(",", collectionDTO.Select(w => w.FeeCollectionIID));

                    //calling AccountPosting Procedure
                    new AccountEntryMapper().AccountTransMergewithMultipleIDs(_colle_IDs, System.DateTime.Now, int.Parse(_context.LoginID.Value.ToString()), 1);


                    if (collectionDTO.Any())
                    {
                        collectionDTO.All(w =>
                        {
                            var entityColle = dbContext.FeeCollections.Where(x => x.FeeCollectionIID == w.FeeCollectionIID).AsNoTracking().FirstOrDefault();

                            entityColle.IsAccountPosted = true;

                            dbContext.Entry(entityColle).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            dbContext.SaveChanges();

                            return true;
                        });
                    }
                }
                //    var repositoryFeeCollection = new EntityRepository<FeeCollection, dbEduegateSchoolContext>(dbContext);
                //    var repository = new EntityRepository<AccountTransactionHead, dbEduegateSchoolContext>(dbContext);
                //    foreach (var headDto in collectionDTO)
                //    {
                //        int? studentCostCenter = (from cls in dbContext.Classes.AsEnumerable() where cls.ClassID == headDto.ClassID select cls.CostCenterID).FirstOrDefault();

                //        var prrDocument = new SettingRepository().GetSettingDetail("PRRDOCUMENTTYPEID", _context.CompanyID.Value);

                //        var entity = new AccountTransactionHead();

                //        entity.CreatedBy = int.Parse(_context.LoginID.Value.ToString());
                //        entity.CreatedDate = DateTime.Now;
                //        entity.DocumentTypeID = documentTypeID;

                //        entity.UpdatedBy = int.Parse(_context.LoginID.Value.ToString());
                //        //entity.UpdatedDate = DateTime.Now;
                //        entity.AccountID = long.Parse(studentAccountsId);
                //        entity.CompanyID = _context.CompanyID;
                //        entity.AmountPaid = headDto.Amount;
                //        entity.CostCenterID = studentCostCenter.HasValue ? studentCostCenter : (int?)null;
                //        entity.ReceiptsID = headDto.FeeCollectionIID;
                //        entity.TransactionStatusID = (byte)Eduegate.Framework.Enums.TransactionStatus.Complete;

                //        entity.DocumentStatusID = (long)Eduegate.Services.Contracts.Enums.DocumentStatuses.Completed;
                //        entity.TransactionDate = System.DateTime.Now;
                //        entity.TransactionNumber = new AccountEntryMapper().GetNextTransactionNumber(documentTypeID); ;
                //        decimal total = headDto.FeeCollectionPaymentModeMapDTO.Select(x => x.Amount.Value).Sum();
                //        entity.AccountTransactionDetails = new List<AccountTransactionDetail>();
                //        var detailDTO = new AccountTransactionDetail();
                //        foreach (var headtl in headDto.FeeCollectionPaymentModeMapDTO)
                //        {

                //            //save in detail table
                //            //---------------------------------------------
                //            detailDTO = new AccountTransactionDetail();
                //            detailDTO.Amount = headtl.Amount;
                //            detailDTO.UpdatedBy = int.Parse(_context.LoginID.Value.ToString());
                //            // detailDTO.UpdatedDate = DateTime.Now;
                //            detailDTO.AccountID = headtl.AccountId;
                //            detailDTO.CreatedBy = int.Parse(_context.LoginID.Value.ToString());
                //            detailDTO.CreatedDate = DateTime.Now;
                //            detailDTO.ReferenceReceiptID = headDto.FeeCollectionIID;
                //            detailDTO.CostCenterID = studentCostCenter.HasValue ? studentCostCenter : (int?)null;
                //            entity.AccountTransactionDetails.Add(detailDTO);
                //        }

                //        detailDTO = new AccountTransactionDetail();
                //        detailDTO.Amount = -(total);
                //        detailDTO.UpdatedBy = int.Parse(_context.LoginID.Value.ToString());
                //        // detailDTO.UpdatedDate = DateTime.Now;
                //        detailDTO.AccountID = long.Parse(studentAccountsId);
                //        detailDTO.CreatedBy = int.Parse(_context.LoginID.Value.ToString());
                //        detailDTO.CreatedDate = DateTime.Now;
                //        detailDTO.ReferenceReceiptID = headDto.FeeCollectionIID;
                //        detailDTO.CostCenterID = studentCostCenter.HasValue ? studentCostCenter : (int?)null;
                //        entity.AccountTransactionDetails.Add(detailDTO);
                //        //---------------------------------------------
                //        entity.AmountPaid = (total);
                //        entity = repository.Insert(entity);
                //        total = 0;

                //        var entityFeeCollection = repositoryFeeCollection.GetById(headDto.FeeCollectionIID);
                //        entityFeeCollection.IsAccountPosted = true;
                //        entityFeeCollection.AccountTransactionHeadID = entity.AccountTransactionHeadIID;
                //        entityFeeCollection = repositoryFeeCollection.Update(entityFeeCollection);
                //        var repositoryFeeCollectFeeTypeMap = new EntityRepository<FeeCollectionFeeTypeMap, dbEduegateSchoolContext>(dbContext);
                //        var repositoryCollectMonthly = new EntityRepository<FeeCollectionMonthlySplit, dbEduegateSchoolContext>(dbContext);
                //        var entityFeeType = repositoryFeeCollectFeeTypeMap.Get(x => x.FeeCollectionID == headDto.FeeCollectionIID).ToList();
                //        foreach (var feeType in entityFeeType)
                //        {
                //            feeType.AccountTransactionHeadID = entity.AccountTransactionHeadIID;
                //            repositoryFeeCollectFeeTypeMap.Update(feeType);
                //            var entityDueFeeMonthly = repositoryCollectMonthly.Get(x => x.FeeCollectionFeeTypeMapId == feeType.FeeCollectionFeeTypeMapsIID).ToList();
                //            if (entityDueFeeMonthly.Count() > 0)
                //            {
                //                foreach (var feeMonthly in entityDueFeeMonthly)
                //                {
                //                    feeMonthly.AccountTransactionHeadID = entity.AccountTransactionHeadIID;
                //                    repositoryCollectMonthly.Update(feeMonthly);

                //                }
                //            }
                //        }

                //        new AccountEntryMapper().AccountTransactionSync(entity.AccountTransactionHeadIID, headDto.FeeCollectionIID, int.Parse(_context.LoginID.Value.ToString()), 1);

                //    }
                //}
                #endregion
                //#region Final Settlement
                //if (settlementDTO.Count() > 0)
                //{
                //    //var repositoryFinal = new EntityRepository<FinalSettlement, dbEduegateSchoolContext>(dbContext);
                //    //var repository = new EntityRepository<AccountTransactionHead, dbEduegateSchoolContext>(dbContext);
                //    if (settlementDTO.Any())
                //    {
                //        settlementDTO.All(w => { new AccountEntryMapper().AccountTransMerge(w.FeeCollectionIID, System.DateTime.Now, int.Parse(_context.LoginID.Value.ToString()), 6); return true; });

                //    }



                //    var repositoryFinal = new EntityRepository<FinalSettlement, dbEduegateSchoolContext>(dbContext);

                //    if (settlementDTO.Any())
                //    {
                //        settlementDTO.All(w =>
                //        {
                //            var entityFinal = repositoryFinal.GetById(w.FeeCollectionIID);
                //            entityFinal.IsAccountPosted = true;
                //            entityFinal = repositoryFinal.Update(entityFinal);
                //            return true;
                //        });
                //    }

                //}

                //  #endregion
            }
            return "1#Saved successfully!";
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            return "1#Saved successfully!";
        }

    }
}