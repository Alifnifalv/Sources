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
using Eduegate.Services.Contracts.School.Students;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Mappers.School.Accounts;
using System.Data.SqlClient;
using System.Data;
using Eduegate.Domain.Mappers.Accounts;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.School.Fees
{
    public class FinalSettlementMapper : DTOEntityDynamicMapper
    {
        public static FinalSettlementMapper Mapper(CallContext context)
        {
            var mapper = new FinalSettlementMapper();
            mapper._context = context;
            return mapper;
        }
        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<FinalSettlementDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            FinalSettlementDTO feeDueDTO = new FinalSettlementDTO();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                #region old methode
                //feeDueDTO = (from stFee in dbContext.StudentFeeDues.AsEnumerable()
                //             join FeeTypeMap in dbContext.FeeDueFeeTypeMaps on stFee.StudentFeeDueIID equals FeeTypeMap.StudentFeeDueID
                //             join FeeMonthlySplit in dbContext.FeeDueMonthlySplits on FeeTypeMap.FeeDueFeeTypeMapsIID equals FeeMonthlySplit.FeeDueFeeTypeMapsID into tmpMonthlySplit
                //             from FeeMonthlySplit in tmpMonthlySplit.DefaultIfEmpty()
                //             where (stFee.StudentFeeDueIID == IID && stFee.CollectionStatus == false)
                //             orderby stFee.InvoiceDate ascending

                //             select new FeeCollectionDTO()
                //             {
                //                 FeeCollectionIID = 0,
                //                 ClassID = stFee.ClassId,
                //                 StudentID = stFee.StudentId,
                //                 AcadamicYearID = stFee.AcadamicYearID,
                //                 SchoolID = stFee.SchoolID,
                //                 AcademicYear = new KeyValueDTO() { Key = Convert.ToString(stFee.AcademicYear.AcademicYearID), Value = stFee.AcademicYear.Description },
                //                 StudentName = stFee.Student.AdmissionNumber + '-' + stFee.Student.FirstName + ' ' + stFee.Student.MiddleName + ' ' + stFee.Student.LastName,
                //                 ClassName = stFee.Class.ClassDescription,
                //                 SectionID = stFee.Student.SectionID,
                //                 SectionName = stFee.Student.SectionID.HasValue ? stFee.Student.Section.SectionName : null,
                //                 AdmissionNo = stFee.Student.AdmissionNumber,
                //                 FeeTypes = (from ft in stFee.FeeDueFeeTypeMaps
                //                             where ft.Status == false && ft.StudentFeeDueID == stFee.StudentFeeDueIID

                //                             select new FeeCollectionFeeTypeDTO()
                //                             {
                //                                 Amount = ft.Amount,
                //                                 //IsRowSelected = true,
                //                                 TaxAmount = ft.TaxAmount,
                //                                 InvoiceNo = stFee.InvoiceNo,
                //                                 FeePeriodID = ft.FeePeriodID,
                //                                 InvoiceDate = stFee.InvoiceDate,
                //                                 TaxPercentage = ft.TaxPercentage,
                //                                 StudentFeeDueID = ft.StudentFeeDueID,
                //                                 FeeCycleID = ft.FeeMaster.FeeCycleID,
                //                                 FeeStructureFeeMapID = ft.FeeStructureFeeMapID,
                //                                 FeeDueFeeTypeMapsID = ft.FeeDueFeeTypeMapsIID,
                //                                 IsFeePeriodDisabled = ft.FeeMaster.FeeCycleID.HasValue ? ft.FeeMaster.FeeCycleID.Value != 3 : true,

                //                                 FeeMaster = ft.FeeMaster == null ? null : ft.FeeMaster.Description,
                //                                 FeePeriod = ft.FeePeriodID.HasValue ? ft.FeePeriod.Description + " ( " + ft.FeePeriod.PeriodFrom.ToString("dd/MMM/yyy") + '-'
                //                                             + ft.FeePeriod.PeriodTo.ToString("dd/MMM/yyy") + " ) " : null,

                //                                 MontlySplitMaps = (from mf in ft.FeeDueMonthlySplits
                //                                                    where mf.Status == false//.AsEnumerable()
                //                                                    select new FeeCollectionMonthlySplitDTO()
                //                                                    {
                //                                                        MonthID = mf.MonthID != 0 ? int.Parse(Convert.ToString(mf.MonthID)) : 0,
                //                                                        Amount = mf.Amount.HasValue ? mf.Amount : (decimal?)null,
                //                                                        TaxAmount = mf.TaxAmount.HasValue ? mf.TaxAmount : (decimal?)null,
                //                                                        TaxPercentage = mf.TaxPercentage.HasValue ? mf.TaxPercentage : (decimal?)null
                //                                                    }).ToList(),
                //                             }).ToList(),
                //             }).FirstOrDefault();

                #endregion

                var finalData = dbContext.FinalSettlements.Where(f => f.FinalSettlementIID == IID)
                    .Include(i => i.AcademicYear)
                    .Include(i => i.Student).ThenInclude(i => i.Class)
                    .Include(i => i.Student).ThenInclude(i => i.Section)
                    .Include(i => i.FinalSettlementPaymentModeMaps).ThenInclude(i =>i.PaymentMode)
                    .Include(i => i.FinalSettlementFeeTypeMaps).ThenInclude(i => i.FeeMaster)
                    .Include(i => i.FinalSettlementFeeTypeMaps).ThenInclude(i => i.FeePeriod)
                    .AsNoTracking().FirstOrDefault();

                //var feeDueData = dbContext.StudentFeeDues.FirstOrDefault(d => d.FeeDueFeeTypeMaps.Any(x => x.))

                feeDueDTO.FeeTypes = new List<FeeCollectionFeeTypeDTO>();
                feeDueDTO.FeeCollectionPaymentModeMapDTO = new List<FeeCollectionPaymentModeMapDTO>();

                feeDueDTO = new FinalSettlementDTO()
                {
                    FinalSettlementIID = finalData.FinalSettlementIID,
                    FeeCollectionIID = 0,
                    ClassID = finalData.ClassID,
                    StudentID = finalData.StudentID,
                    AcadamicYearID = finalData.AcadamicYearID,
                    SchoolID = finalData.SchoolID,
                    AcademicYear = new KeyValueDTO() { Key = Convert.ToString(finalData.AcademicYear?.AcademicYearID), Value = finalData.AcademicYear?.Description },
                    StudentName = finalData.Student?.AdmissionNumber + '-' + finalData.Student?.FirstName + ' ' + finalData.Student?.MiddleName + ' ' + finalData.Student?.LastName,
                    ClassName = finalData.Student.ClassID.HasValue ? finalData.Student.Class.ClassDescription : null,
                    SectionID = finalData.Student?.SectionID,
                    SectionName = finalData.Student.SectionID.HasValue ? finalData.Student.Section.SectionName : null,
                    AdmissionNo = finalData.Student?.AdmissionNumber,
                    Remarks = finalData.Remarks,
                };

                foreach (var fc in finalData?.FinalSettlementPaymentModeMaps)
                {
                    feeDueDTO.FeeCollectionPaymentModeMapDTO.Add(new FeeCollectionPaymentModeMapDTO()
                    {
                        PaymentMode = new KeyValueDTO() { Key = Convert.ToString(fc.PaymentMode?.PaymentModeID), Value = fc.PaymentMode?.PaymentModeName },
                        CreatedDate = fc.CreatedDate,
                        Amount = fc.Amount,
                        ReferenceNo = fc.ReferenceNo,
                    });
                }

                feeDueDTO.CollectedAmount = feeDueDTO.FeeCollectionPaymentModeMapDTO?.Select(x => x.Amount).Sum();

                foreach (var ft in finalData.FinalSettlementFeeTypeMaps)
                {
                    var stdfeeDue = dbContext.FeeDueFeeTypeMaps.Where(y => y.FeeDueFeeTypeMapsIID == ft.FeeDueFeeTypeMapsID)
                                    .Include(i => i.StudentFeeDue)
                                    .Include(i => i.FeeDueMonthlySplits)
                                    .Include(i => i.FeeMaster)
                                    .Include(i => i.FeePeriod)
                                    .AsNoTracking().FirstOrDefault();

                    var monthlySplit = new List<FeeCollectionMonthlySplitDTO>();

                    foreach (var mf in stdfeeDue?.FeeDueMonthlySplits)
                    {
                        monthlySplit.Add(new FeeCollectionMonthlySplitDTO()
                        {
                            MonthID = mf.MonthID != 0 ? int.Parse(Convert.ToString(mf.MonthID)) : 0,
                            MonthName = mf.MonthID == 0 ? null : new DateTime(2010, mf.MonthID, 1).ToString("MMM") + " " + mf.Year,
                            Amount = mf.Amount.HasValue ? mf.Amount : (decimal?)null,
                            TaxAmount = mf.TaxAmount.HasValue ? mf.TaxAmount : (decimal?)null,
                            TaxPercentage = mf.TaxPercentage.HasValue ? mf.TaxPercentage : (decimal?)null
                        });
                    }

                    feeDueDTO.FeeTypes.Add(new FeeCollectionFeeTypeDTO()
                    {
                        Amount = ft.Amount,
                        //IsRowSelected = true,
                        DueAmount = ft.DueAmount,
                        Balance = ft.Balance,
                        RefundAmount = ft.RefundAmount,
                        CollectedAmount = ft.CollectedAmount,
                        TaxAmount = stdfeeDue?.TaxAmount,
                        InvoiceNo = stdfeeDue.StudentFeeDue?.InvoiceNo,
                        FeePeriodID = ft.FeePeriodID,
                        InvoiceDate = stdfeeDue.StudentFeeDue?.InvoiceDate,
                        TaxPercentage = stdfeeDue.TaxPercentage,
                        StudentFeeDueID = stdfeeDue.StudentFeeDueID,
                        FeeCycleID = ft.FeeMaster?.FeeCycleID,
                        FeeStructureFeeMapID = stdfeeDue.FeeStructureFeeMapID,
                        FeeDueFeeTypeMapsID = stdfeeDue.FeeDueFeeTypeMapsIID,
                        IsFeePeriodDisabled = ft.FeeMaster.FeeCycleID.HasValue ? ft.FeeMaster.FeeCycleID.Value != 3 : true,
                        FeeMaster = ft.FeeMaster == null ? null : ft.FeeMaster.Description,
                        FeePeriod = ft.FeePeriodID.HasValue ? ft.FeePeriod.Description + " ( " + ft.FeePeriod.PeriodFrom.ToString("dd/MMM/yyy") + '-'
                                                             + ft.FeePeriod.PeriodTo.ToString("dd/MMM/yyy") + " ) " : null,

                        MontlySplitMaps = monthlySplit
                    });
                }
            }
            return ToDTOString(feeDueDTO);
        }


        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as FinalSettlementDTO;
            decimal paidAmount = 0;
            decimal fineAmount = 0;
            decimal collectedAmount = 0;

            if (toDto.FinalSettlementIID != 0)
            {
                throw new Exception("Edit option is currently not available");
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

            //if (toDto.FeeCollectionPaymentModeMapDTO.Select(x => x.Amount).Sum() == 0)
            //{
            //    throw new Exception("The Payment amount cannot be zero!");
            //}

            paidAmount = toDto.FeeCollectionPaymentModeMapDTO.Select(x => x.Amount.Value).Sum();
            collectedAmount = toDto.FeeTypes.Select(x => x.Balance.Value).Sum();

            fineAmount = (toDto.FeeFines == null || toDto.FeeFines.Count == 0) ? 0 : toDto.FeeFines.Select(x => x.Amount.Value).Sum();
            if (paidAmount != (collectedAmount + fineAmount))
            {
                throw new Exception("Amount need to be collected and Paid amount must be equal");
            }

            var monthlyClosingDate = new MonthlyClosingMapper().GetMonthlyClosingDate();
            if (monthlyClosingDate != null && monthlyClosingDate.Value.Year > 1900 && toDto.SettlementDate.Value >= monthlyClosingDate)
            {
                throw new Exception("This Transaction could not be saved due to monthly closing");
            }

            if (toDto.FeeCollectionPaymentModeMapDTO.IsNull() || toDto.FeeCollectionPaymentModeMapDTO.Count() == 0)
                return "0#There is no data found for account posting!";

            Entity.Models.Settings.Sequence sequence = new Entity.Models.Settings.Sequence();

            //convert the dto to entity and pass to the repository.
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var studentAccountsId = new Domain.Setting.SettingBL(null).GetSettingValue<string>("STUDENT_ACCOUNT_ID");
                if (studentAccountsId.IsNull())
                    return "0#There is no Student Account found!";

                var documentTypeID = (from st in dbContext.DocumentTypes.AsNoTracking().Where(s => s.TransactionTypeName.ToUpper() == "JOURNAL") select st.DocumentTypeID).FirstOrDefault();
                if (documentTypeID.IsNull())
                    throw new Exception("Document Type 'JOURNAL' is not available.Please check");

                if (paidAmount > 0)
                {
                    _sFeeMaster_IDs = new Domain.Setting.SettingBL(null).GetSettingValue<string>("FINALSETTLEMENTFEEMASTERID", 1, null);
                    if (_sFeeMaster_IDs == null)
                        throw new Exception("Please set 'FINALSETTLEMENTFEEMASTERID' in settings");
                }

                MutualRepository mutualRepository = new MutualRepository();

                //var repositoryAcadamicYear = new EntityRepository<AcademicYear, dbEduegateSchoolContext>(dbContext);
                //var entityAcadamicYear = repositoryAcadamicYear.GetById(toDto.AcadamicYearID);
                //if (!entityAcadamicYear.IsNull())
                //{
                //    if (!(entityAcadamicYear.StartDate.Value.Date <= toDto.CollectionDate && entityAcadamicYear.EndDate.Value.Date >= toDto.CollectionDate))
                //    {
                //        throw new Exception("Please select Final Settlement Date within academic year!");

                //    }
                //}

                sequence = mutualRepository.GetNextSequence("FinalSettlementReceiptNo", null);
                try
                {
                    sequence = mutualRepository.GetNextSequence("FinalSettlementReceiptNo", null);
                }
                catch (Exception ex)
                {
                    throw new Exception("Please generate sequence with 'FinalSettlementReceiptNo'");
                }

                var entity = new FinalSettlement()
                {
                    //FeeCollectionIID = toDto.FeeCollectionIID,
                    ClassID = toDto.ClassID,
                    SectionID = toDto.SectionID,
                    StudentID = toDto.StudentID,
                    AcadamicYearID = toDto.AcadamicYearID,
                    Remarks = toDto.Remarks,
                    Amount = collectedAmount,
                    PaidAmount = paidAmount,
                    IsPaid = true,
                    FeeReceiptNo = sequence.Prefix + sequence.LastSequence,
                    SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                    FinalSettlementDate = toDto.SettlementDate.IsNotNull() ? Convert.ToDateTime(toDto.SettlementDate) : DateTime.Now,
                    CreatedBy = toDto.FinalSettlementIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                    UpdatedBy = toDto.FinalSettlementIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                    CreatedDate = toDto.FinalSettlementIID == 0 ? DateTime.Now : dto.CreatedDate,
                    UpdatedDate = toDto.FinalSettlementIID > 0 ? DateTime.Now : dto.UpdatedDate,
                    //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
                };

                /*//newly added   */
                entity.FinalSettlementPaymentModeMaps = new List<FinalSettlementPaymentModeMap>();
                foreach (var paymentType in toDto.FeeCollectionPaymentModeMapDTO)
                {
                    entity.FinalSettlementPaymentModeMaps.Add(new FinalSettlementPaymentModeMap()
                    {
                        //CreatedBy = int.Parse(_context.UserId),
                        Amount = paymentType.Amount,
                        RefundAmount = paymentType.Amount < 0 ? paymentType.Amount : 0,
                        Balance = paymentType.Amount,
                        CreatedDate = System.DateTime.Now,
                        //FeeCollectionID = toDto.FeeCollectionIID,
                        PaymentModeID = paymentType.PaymentModeID,
                        ReferenceNo = paymentType.ReferenceNo
                    });
                }

                entity.FinalSettlementFeeTypeMaps = new List<FinalSettlementFeeTypeMap>();
                foreach (var feeType in toDto.FeeTypes)
                {
                    var monthlySplit = new List<FinalSettlementMonthlySplit>();
                    foreach (var feeMasterMonthlyDto in feeType.MontlySplitMaps)
                    {
                        var entityChild = new FinalSettlementMonthlySplit()
                        {
                            CreditNoteAmount = feeMasterMonthlyDto.CreditNoteAmount,
                            Balance = feeMasterMonthlyDto.Balance,
                            FeeDueMonthlySplitID = feeMasterMonthlyDto.FeeDueMonthlySplitID,
                            MonthID = byte.Parse(feeMasterMonthlyDto.MonthID.ToString()),
                            Year = feeMasterMonthlyDto.Year,
                            FeePeriodID = feeType.FeePeriodID,
                            Amount = feeMasterMonthlyDto.PrvCollect.HasValue ? feeMasterMonthlyDto.PrvCollect : (decimal?)null,
                            DueAmount = feeMasterMonthlyDto.Amount,
                            Receivable = feeMasterMonthlyDto.ReceivableAmount,
                            RefundAmount = feeMasterMonthlyDto.RefundAmount,
                            NowPaying = feeMasterMonthlyDto.NowPaying,
                        };

                        monthlySplit.Add(entityChild);
                    }

                    entity.FinalSettlementFeeTypeMaps.Add(new FinalSettlementFeeTypeMap()
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
                        Receivable = feeType.ReceivableAmount,
                        FinalSettlementMonthlySplits = monthlySplit
                    });
                }

                dbContext.FinalSettlements.Add(entity);

                if (entity.FinalSettlementIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    dbContext.SaveChanges();

                    string message1;
                    if (paidAmount > 0)
                    {
                        SqlConnectionStringBuilder _sBuilder = new SqlConnectionStringBuilder(Infrastructure.ConfigHelper.GetSchoolConnectionString());
                        _sBuilder.ConnectTimeout = 30; // Set Timedout
                        using (SqlConnection conn = new SqlConnection(_sBuilder.ConnectionString))
                        {
                            try { conn.Open(); }
                            catch (Exception ex)
                            {
                                message1 = ex.Message; return "0#" + message1;
                            }
                            using (SqlCommand sqlCommand = new SqlCommand("[schools].[SPS_FEE_DUE_MERGE]", conn))
                            {
                                sqlCommand.CommandType = CommandType.StoredProcedure;


                                sqlCommand.Parameters.Add(new SqlParameter("@ACADEMICYEARID", SqlDbType.Int));
                                sqlCommand.Parameters["@ACADEMICYEARID"].Value = toDto.AcadamicYearID ?? 0;

                                sqlCommand.Parameters.Add(new SqlParameter("@SCHOOLID", SqlDbType.Int));
                                sqlCommand.Parameters["@SCHOOLID"].Value = _context.SchoolID ?? 0;

                                sqlCommand.Parameters.Add(new SqlParameter("@COMPANYID", SqlDbType.Int));
                                sqlCommand.Parameters["@COMPANYID"].Value = _context.CompanyID;

                                sqlCommand.Parameters.Add(new SqlParameter("@INVOICEDATE", SqlDbType.DateTime));
                                sqlCommand.Parameters["@INVOICEDATE"].Value = toDto.SettlementDate;

                                sqlCommand.Parameters.Add(new SqlParameter("@DUEDATE", SqlDbType.DateTime));
                                sqlCommand.Parameters["@DUEDATE"].Value = toDto.SettlementDate;

                                sqlCommand.Parameters.Add(new SqlParameter("@ACCOUNTDATE", SqlDbType.DateTime));
                                sqlCommand.Parameters["@ACCOUNTDATE"].Value = toDto.CollectionDate;

                                sqlCommand.Parameters.Add(new SqlParameter("@CLASSIDs", SqlDbType.VarChar));
                                sqlCommand.Parameters["@CLASSIDs"].Value = toDto.ClassID == 0 ? string.Empty : toDto.ClassID.ToString();

                                sqlCommand.Parameters.Add(new SqlParameter("@STUDENTIDs", SqlDbType.VarChar));
                                sqlCommand.Parameters["@STUDENTIDs"].Value = toDto.StudentID;

                                sqlCommand.Parameters.Add(new SqlParameter("@SECTIONIDs", SqlDbType.VarChar));
                                sqlCommand.Parameters["@SECTIONIDs"].Value = toDto.SectionID;

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
                                sqlCommand.Parameters["@AMOUNT"].Value = paidAmount;

                                try
                                {
                                    // Run the stored procedure.
                                    message1 = Convert.ToString(sqlCommand.ExecuteScalar() ?? string.Empty);

                                }
                                catch (Exception ex)
                                {
                                    // throw new Exception("Something Wrong! Please check after sometime");
                                    message1 = "0#Error on Saving";
                                }
                                finally
                                {
                                    conn.Close();
                                }
                            }
                        }

                    }

                    SqlConnectionStringBuilder _sBuilderNew = new SqlConnectionStringBuilder(Infrastructure.ConfigHelper.GetSchoolConnectionString());
                    _sBuilderNew.ConnectTimeout = 30; // Set Timedout
                    using (SqlConnection conn = new SqlConnection(_sBuilderNew.ConnectionString))
                    {
                        try { conn.Open(); }
                        catch (Exception ex)
                        {
                            message1 = ex.Message; return "0#" + message1;
                        }
                        using (SqlCommand sqlCommand = new SqlCommand("[schools].[SPS_FINAL_SETTLEMENT]", conn))
                        {
                            sqlCommand.CommandType = CommandType.StoredProcedure;

                            sqlCommand.Parameters.Add(new SqlParameter("@STUDENTID", SqlDbType.BigInt));
                            sqlCommand.Parameters["@STUDENTID"].Value = toDto.StudentID ?? 0;

                            sqlCommand.Parameters.Add(new SqlParameter("@FINALSETTLEMENTID ", SqlDbType.BigInt));
                            sqlCommand.Parameters["@FINALSETTLEMENTID "].Value = entity.FinalSettlementIID;

                            sqlCommand.Parameters.Add(new SqlParameter("@FEEMASTERID ", SqlDbType.Int));
                            sqlCommand.Parameters["@FEEMASTERID "].Value = _sFeeMaster_IDs;

                            try
                            {
                                // Run the stored procedure.
                                message1 = Convert.ToString(sqlCommand.ExecuteScalar() ?? string.Empty);

                            }
                            catch (Exception ex)
                            {
                                // throw new Exception("Something Wrong! Please check after sometime");
                                message1 = "0#Error on Saving";
                            }
                            finally
                            {
                                conn.Close();
                            }
                        }
                    }

                }
                else
                {
                    foreach (var paymentMode in entity.FinalSettlementFeeTypeMaps)
                    {
                        if (paymentMode.FinalSettlementFeeTypeMapsIID != 0)
                        {
                            dbContext.Entry(paymentMode).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                        else
                        {
                            dbContext.Entry(paymentMode).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                    }

                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    dbContext.SaveChanges();
                }

                var entityStudent = dbContext.Students.Where(x => x.StudentIID == toDto.StudentID).AsNoTracking().FirstOrDefault();
                entityStudent.Status = 3;

                dbContext.Entry(entityStudent).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                dbContext.SaveChanges();

                toDto.FinalSettlementIID = entity.FinalSettlementIID;

                var message = AccountPosting(toDto, documentTypeID, studentAccountsId);
                string[] resp = message.Split('#');
                if (resp[0] == "0")
                    throw new Exception(resp[1]);

                return GetEntity(entity.FinalSettlementIID);
            }
        }

        public string AccountPosting(FinalSettlementDTO toDto, int documentTypeID, string studentAccountsId)
        {
            List<FeeCollectionPaymentModeMapDTO> feePaymentDTO = new List<FeeCollectionPaymentModeMapDTO>();
            //List<FeeCollectionDTO> collectionDTO = new List<FeeCollectionDTO>();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                int? studentCostCenter = (from cls in dbContext.Classes.AsNoTracking() where cls.ClassID == toDto.ClassID select cls.CostCenterID).FirstOrDefault();

                new AccountEntryMapper().AccountTransMerge(toDto.FinalSettlementIID, System.DateTime.Now, int.Parse(_context.LoginID.Value.ToString()), 5);

                var entityFinal = dbContext.FinalSettlements.Where(x => x.FinalSettlementIID == toDto.FinalSettlementIID).AsNoTracking().FirstOrDefault();
                entityFinal.IsAccountPosted = true;

                dbContext.Entry(entityFinal).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                dbContext.SaveChanges();

                return "1#Saved successfully!";
            }
        }
    }
}