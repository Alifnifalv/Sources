using Eduegate.Domain.Entity;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.Budgeting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Eduegate.Domain.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Eduegate.Services.Contracts.Accounts.Accounting;
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Services.Contracts.AI;
using Eduegate.Domain.Entity.Accounts;
using System.ComponentModel.DataAnnotations;
using Eduegate.Domain.Entity.Accounts.Models.AI;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq.Expressions;
using Eduegate.Services.Contracts.Catalog;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Eduegate.Domain.Mappers.Accounts
{
    public class BankReconciliationMapper : DTOEntityDynamicMapper
    {
        public static BankReconciliationMapper Mapper(CallContext context)
        {
            var mapper = new BankReconciliationMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<BankReconciliationDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public BankReconciliationHeadDTO ToDTO(long IID)
        {
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    var entity = dbContext.BankReconciliationHeads
                        //.Include(i => i.BankReconciliationDetails)
                        .Include(i => i.BankReconciliationStatus)
                        .Include(i => i.BankStatement)
                        .AsNoTracking()
                        .FirstOrDefault(a => a.BankReconciliationHeadIID == IID);
                    if (entity != null)
                    {
                        var details = dbContext.BankReconciliationDetails
                            .Include(i => i.BankReconciliationMatchingEntries)
                            .Include(i => i.BankReconciliationMatchedStatus)
                            //.Include(i => i.BankStatement)
                            .AsNoTracking()
                            .Where(a => a.BankReconciliationHeadID == IID).ToList();

                        entity.BankReconciliationDetails = details;

                    }

                    return entity != null ? ToDTO(entity) : null;
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        private BankReconciliationHeadDTO ToDTO(BankReconciliationHead entity)
        {
            try
            {

                var dto = new BankReconciliationHeadDTO()
                {
                    BankReconciliationHeadIID = entity.BankReconciliationHeadIID,
                    BankStatementID = entity.BankStatementID,
                    BankAccountID = entity.BankAccountID,
                    FromDate = entity.FromDate,
                    ToDate = entity.ToDate,
                    OpeningBalanceAccount = entity.OpeningBalanceAccount,
                    OpeningBalanceBankStatement = entity.OpeningBalanceBankStatement,
                    ClosingBalanceAccount = entity.ClosingBalanceAccount,
                    ClosingBalanceBankStatement = entity.ClosingBalanceBankStatement,
                    BankReconciliationStatusID = entity.BankReconciliationStatusID,
                    BankReconciliationDetailDtos = GetReconciliationDetailDTO(entity.BankReconciliationDetails.ToList()),
                    BankStatementEntryDTOs = entity.BankStatementID.HasValue ? GetBankStatementEntryDTO(entity.BankStatementID.Value) : new List<BankStatementEntryDTO>(),
                    BankStatementData = entity.BankStatement != null && entity.BankStatement?.BankStatementIID != 0 ? new BankStatementDTO()
                    {
                        BankStatementIID = entity.BankStatement.BankStatementIID,
                        ContentFileID = entity?.BankStatement.ContentFileID,
                        ContentFileName = entity.BankStatement.ContentFileName

                    } : new BankStatementDTO()

                };
                return dto;
            }
            catch (Exception ex)
            {
                var exceptionMessage = ex.Message;

                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception") ? ex.InnerException?.Message : ex.Message;

                Eduegate.Logger.LogHelper<string>.Fatal($"Exception in bank Reconciliation ToDTO(BankReconciliationHead entity). Error message: {errorMessage}", ex);

                throw ex;
            }
        }

        public List<BankReconciliationMatchingEntryDTO> GetMatchingEntry(List<BankReconciliationMatchingEntry> matchingEntry)
        {
            var matchingEntryDTO = new List<BankReconciliationMatchingEntryDTO>();
            matchingEntryDTO = (from s in matchingEntry
                                select new BankReconciliationMatchingEntryDTO()
                                {
                                    BankReconciliationMatchingEntryIID = s.BankReconciliationMatchingEntryIID,
                                    BankReconciliationDetailID = s.BankReconciliationDetailID,
                                    BankReconciliationHeadID = s.BankReconciliationHeadID,
                                    TranHeadID = null,
                                    TranTailID = s.TranTailID,
                                    SlNo = s.SlNo,
                                    AccountID = s.AccountID,
                                    ReconciliationDate = s.ReconciliationDate,
                                    Remarks = s.Remarks,
                                    Amount = s.Amount,
                                    ChequeNo = s.ChequeNo,
                                    //ChequeDate=s.ChequeDate,
                                    PartyName = s.PartyName,
                                    Reference = s.Reference,
                                    BankStatementEntryID = s.BankStatementEntryID,
                                    ReferenceGroupNO = s.ReferenceGroupNO,
                                    ReferenceGroupName = s.ReferenceGroupName,

                                }).ToList();
            return matchingEntryDTO;

        }
        private List<BankStatementEntryDTO> GetBankStatementEntryDTO(long bankStatementID)
        {
            var bankStatementEntryDTO = new List<BankStatementEntryDTO>();
            try
            {

                using (var dbContext = new dbEduegateERPContext())
                {

                    bankStatementEntryDTO = (from s in dbContext.BankStatementEntries
                                             where s.BankStatementID == bankStatementID
                                             select new BankStatementEntryDTO
                                             {
                                                 BankStatementEntryIID = s.BankStatementEntryIID,
                                                 BankStatementID = bankStatementID,
                                                 ChequeNo = s.ChequeNo ?? string.Empty,
                                                 Credit = s.Credit ?? 0,
                                                 SlNO = s.SlNO ?? 0,
                                                 Debit = s.Debit ?? 0,
                                                 PartyName = s.PartyName ?? string.Empty,
                                                 Description = s.Description ?? string.Empty,
                                                 PostDate = s.PostDate ?? DateTime.MinValue,
                                                 Balance = s.Balance ?? 0,
                                                 ReferenceNo = s.ReferenceNo ?? string.Empty,
                                                 //ChequeDate = s.ChequeDate
                                             }).ToList();
                    //                var rawEntries = dbContext.BankStatementEntries
                    //.Where(s => s.BankStatementID == bankStatementID)
                    //.Select(x=>x.BankStatementID)
                    //.ToList(); // Fetch raw data

                    //foreach (var entry in rawEntries)
                    //{
                    //    Console.WriteLine($"PostDate: {entry.PostDate}, ChequeNo: {entry.ChequeNo}, Credit: {entry.Credit}, Debit: {entry.Debit}");
                    //}
                    //var entries = dbContext.BankStatementEntries
                    //   .Where(s => s.BankStatementID == bankStatementID)
                    //   .Select(s => new
                    //   {
                    //       s.BankStatementEntryIID ,
                    //       s.BankStatementID ,
                    //       s.ChequeNo ?? string.Empty,
                    //       s.Credit,
                    //       s.SlNO,
                    //       s.Debit,
                    //       s.PartyName,
                    //       s.Description,
                    //       //s.PostDate,
                    //       //s.Balance,
                    //       //s.ReferenceNo

                    //   })
                    //   .ToList();

                    //bankStatementEntryDTO = entries.Select(s => new BankStatementEntryDTO
                    //{
                    //    //PostDate = s.PostDate ?? DateTime.MinValue, // Ensure null-safe assignment
                    //    //BankStatementEntryIID = s.BankStatementEntryIID,
                    //    BankStatementID = bankStatementID,
                    //    //ChequeNo = s.ChequeNo,
                    //   // Credit = s.Credit,
                    //   // SlNO = s.SlNO,
                    //   // Debit = s.Debit,
                    //    //PartyName = s.PartyName,
                    //    Description = s.Description,
                    //    //Balance = s.Balance,
                    //    //ReferenceNo = s.ReferenceNo
                    //}).ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bankStatementEntryDTO;
        }

        private List<BankReconciliationDetailDTO> GetReconciliationDetailDTO(List<BankReconciliationDetail> bankReconciliationDetail)
        {
            var lstBudgetEntryAccountMap = new List<BudgetEntryAccountMapDTO>();

            var listBankReconciliationDetail = (from detail in bankReconciliationDetail
                                                where detail.IsOpening != true
                                                select new BankReconciliationDetailDTO
                                                {
                                                    ReconciliationDetailIID = detail.ReconciliationDetailIID,
                                                    BankReconciliationHeadID = detail.BankReconciliationHeadID,
                                                    TranHeadID = detail.TranHeadID,
                                                    TranTailID = detail.TranTailID,
                                                    SlNo = detail.SlNo,
                                                    AccountID = detail.AccountID,
                                                    ReconciliationDate = detail.ReconciliationDate,
                                                    Remarks = detail.Remarks,
                                                    Amount = detail.Amount,
                                                    BankReconciliationMatchedStatusID = detail.BankReconciliationMatchedStatusID,
                                                    BankStatementEntryID = detail.BankStatementEntryID,
                                                    ChequeDate = detail.ChequeDate,
                                                    ChequeNo = detail.ChequeNo,
                                                    PartyName = detail.PartyName,
                                                    Reference = detail.Reference,
                                                    ReferenceGroupName = detail.ReferenceGroupName,
                                                    ReferenceGroupNO = detail.ReferenceGroupNO,
                                                    BankReconciliationMatchingEntry = detail.BankReconciliationMatchingEntries.Count() > 0 ? GetMatchingEntry(detail.BankReconciliationMatchingEntries.ToList()) : new List<BankReconciliationMatchingEntryDTO>()

                                                }).ToList();

            return listBankReconciliationDetail;
        }
        public BankOpeningParametersDTO SaveBankOpeningDetails(BankOpeningParametersDTO transactionDTO)
        {
            var bankOpeningParametersDTO = new BankOpeningParametersDTO();
            try
            {
                using (var dbContext = new dbEduegateERPContext())
                {
                    string message = string.Empty;
                    SqlConnectionStringBuilder _sBuilder = new SqlConnectionStringBuilder(Infrastructure.ConfigHelper.GetDefaultConnectionString());
                    _sBuilder.ConnectTimeout = 30; // Set Timedout
                    using (SqlConnection conn = new SqlConnection(_sBuilder.ConnectionString))
                    {
                        try { conn.Open(); }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        using (SqlCommand sqlCommand = new SqlCommand("[account].[SPS_BankReconcilationOpening]", conn))
                        {
                            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                            adapter.SelectCommand.Parameters.Add(new SqlParameter("@SDATE", SqlDbType.DateTime));
                            adapter.SelectCommand.Parameters["@SDATE"].Value = transactionDTO.SDate;

                            adapter.SelectCommand.Parameters.Add(new SqlParameter("@EDATE", SqlDbType.DateTime));
                            adapter.SelectCommand.Parameters["@EDATE"].Value = transactionDTO.EDate;

                            adapter.SelectCommand.Parameters.Add(new SqlParameter("@BankAccountID", SqlDbType.BigInt));
                            adapter.SelectCommand.Parameters["@BankAccountID"].Value = transactionDTO.BankAccountID;

                            adapter.SelectCommand.Parameters.Add(new SqlParameter("@ClosingBalance_Book", SqlDbType.Money));
                            adapter.SelectCommand.Parameters["@ClosingBalance_Book"].Value = transactionDTO.LedgerClosingBalance;

                            adapter.SelectCommand.Parameters.Add(new SqlParameter("@ClosingBalance_Bank", SqlDbType.Money));
                            adapter.SelectCommand.Parameters["@ClosingBalance_Bank"].Value = transactionDTO.BankClosingBalInput;

                            adapter.SelectCommand.Parameters.Add(new SqlParameter("@FeedLogID", SqlDbType.BigInt));
                            adapter.SelectCommand.Parameters["@FeedLogID"].Value = transactionDTO.FeedLogID;

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

                            if (dataTable != null)
                            {
                                foreach (DataRow row in dataTable.Rows)
                                {
                                    transactionDTO.BankReconciliationHeadIID = (long?)row["BankReconciliationHeadIID"];
                                }
                            }

                            return transactionDTO;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                var exceptionMessage = ex.Message;

                var fields = " SDate: " + transactionDTO.SDate.ToString() + "EDate: " + transactionDTO.EDate.ToString() + "BankAccountID" + transactionDTO.BankAccountID.ToString() + "FeedLogID" + transactionDTO;
                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception") ? ex.InnerException?.Message : ex.Message;
                errorMessage = errorMessage + fields;
                Eduegate.Logger.LogHelper<string>.Fatal($"Exception in bank Reconciliation SaveBankOpeningDetails(). Error message: {errorMessage}", ex);

                throw ex;
            }
        }

        public List<RuleDTO> GetBankRules(long? bankAccountID)
        {
            var ruleDTO = new List<RuleDTO>();
            int? matchingRuleSetID = 0;
            int? dataExtractionRuleSetID = 0;
            try
            {
                using (var dbContext = new dbEduegateAccountsContext())
                {

                    var bankAccountData = dbContext.BankAccounts.Where(x => x.BankAccountID == bankAccountID).AsNoTracking().FirstOrDefault();
                    if (bankAccountData != null)
                    {
                        if (bankAccountData.MatchingRuleSetID != null)
                        {
                            matchingRuleSetID = bankAccountData.MatchingRuleSetID;
                            var matchingRules = dbContext.Rules.Where(x => x.RuleSetID == matchingRuleSetID).AsNoTracking().ToList();
                            if (matchingRules.Any())
                            {
                                matchingRules.All(w =>
                                {
                                    ruleDTO.Add(new RuleDTO()
                                    {
                                        RuleID = w.RuleID,
                                        RuleSetID = w.RuleSetID,
                                        RuleTypeID = w.RuleTypeID,
                                        RuleOrder = w.RuleOrder,
                                        ColumnDataType = w.ColumnDataType,
                                        ColumnName = w.ColumnName,
                                        PatternTypeID = w.PatternTypeID,
                                        Pattern = w.Pattern,
                                        Description = w.Description,
                                        Expression = w.Expression

                                    });
                                    return true;
                                });

                            }

                            if (bankAccountData.DataExtractionRuleSetID != null)
                            {
                                dataExtractionRuleSetID = bankAccountData.DataExtractionRuleSetID;
                                var extractionRules = dbContext.Rules.Where(x => x.RuleSetID == dataExtractionRuleSetID).AsNoTracking().ToList();
                                if (extractionRules.Any())
                                {
                                    extractionRules.All(w =>
                                    {
                                        ruleDTO.Add(new RuleDTO()
                                        {
                                            RuleID = w.RuleID,
                                            RuleSetID = w.RuleSetID,
                                            RuleTypeID = w.RuleTypeID,
                                            RuleOrder = w.RuleOrder,
                                            ColumnDataType = w.ColumnDataType,
                                            ColumnName = w.ColumnName,
                                            PatternTypeID = w.PatternTypeID,
                                            Pattern = w.Pattern,
                                            Description = w.Description,
                                            Expression = w.Expression

                                        });
                                        return true;
                                    });

                                }
                            }



                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var exceptionMessage = ex.Message;
                var fields = "bankAccountID: " + bankAccountID.ToString();
                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception") ? ex.InnerException?.Message : ex.Message;
                errorMessage = errorMessage + fields;
                Eduegate.Logger.LogHelper<string>.Fatal($"Exception in GetBankRules(long? bankAccountID). Error message: {errorMessage}", ex);
                throw ex;
            }
            return ruleDTO;
        }
        public BankReconciliationDTO FillBankTransctions(BankReconciliationDTO transactionDTO)
        {
            var bankReconciliationDTO = new BankReconciliationDTO();
            var listData = new List<BankReconciliationTransactionDTO>();
            int count = 0;
            try
            {
                using (var dbContext = new dbEduegateERPContext())
                {
                    string message = string.Empty;
                    SqlConnectionStringBuilder _sBuilder = new SqlConnectionStringBuilder(Infrastructure.ConfigHelper.GetDefaultConnectionString());
                    _sBuilder.ConnectTimeout = 30; // Set Timedout
                    using (SqlConnection conn = new SqlConnection(_sBuilder.ConnectionString))
                    {
                        try { conn.Open(); }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        using (SqlCommand sqlCommand = new SqlCommand("[account].[SPS_BANK_ACCOUNT_TRANSACTIONS]", conn))
                        {
                            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                            adapter.SelectCommand.Parameters.Add(new SqlParameter("@SDATE", SqlDbType.DateTime));
                            adapter.SelectCommand.Parameters["@SDATE"].Value = transactionDTO.SDate;

                            adapter.SelectCommand.Parameters.Add(new SqlParameter("@EDATE", SqlDbType.DateTime));
                            adapter.SelectCommand.Parameters["@EDATE"].Value = transactionDTO.EDate;

                            adapter.SelectCommand.Parameters.Add(new SqlParameter("@IBAN", SqlDbType.VarChar));
                            adapter.SelectCommand.Parameters["@IBAN"].Value = transactionDTO.IBAN;

                            adapter.SelectCommand.Parameters.Add(new SqlParameter("@ACCOUNTNO", SqlDbType.VarChar));
                            adapter.SelectCommand.Parameters["@ACCOUNTNO"].Value = transactionDTO.AccountNo;

                            adapter.SelectCommand.Parameters.Add(new SqlParameter("@BankAccountID", SqlDbType.BigInt));
                            adapter.SelectCommand.Parameters["@BankAccountID"].Value = transactionDTO.BankAccountID;

                            adapter.SelectCommand.Parameters.Add(new SqlParameter("@BankReconciliationHeadID", SqlDbType.BigInt));
                            adapter.SelectCommand.Parameters["@BankReconciliationHeadID"].Value = transactionDTO.BankReconciliationHeadIID;


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
                            try
                            {



                                if (dataTable != null)
                                {
                                    foreach (DataRow row in dataTable.Rows)
                                    {
                                        count = count + 1;
                                        listData.Add(new BankReconciliationTransactionDTO()
                                        {
                                            AccountID = row["AccountID"] == DBNull.Value ? (long?)null : (long?)row["AccountID"],
                                            AccountName = row["AccountName"] == DBNull.Value ? null : (string)row["AccountName"],
                                            BankAccountID = row["BankAccountID"] == DBNull.Value ? (int?)null : (int?)row["BankAccountID"],
                                            AccountNumber = row["AccountNumber"] == DBNull.Value ? null : (string)row["AccountNumber"],
                                            Narration = row["Narration"] == DBNull.Value ? null : (string)row["Narration"],
                                            Debit = row["Debit"] == DBNull.Value ? (decimal?)null : (decimal?)row["Debit"],
                                            Credit = row["Credit"] == DBNull.Value ? (decimal?)null : (decimal?)row["Credit"],
                                            Amount = row["Amount"] == DBNull.Value ? (decimal?)null : (decimal?)row["Amount"],
                                            ChequeNo = row["Cheque_No"] == DBNull.Value ? null : (string)row["Cheque_No"],
                                            PartyName = row["Partyref"] == DBNull.Value ? null : (string)row["Partyref"],
                                            Reference = row["VoucherNo"] == DBNull.Value ? null : (string)row["VoucherNo"],
                                            TranHeadID = row["TH_ID"] == DBNull.Value ? (long?)null : (long?)row["TH_ID"],
                                            TranTailID = row["TL_ID"] == DBNull.Value ? (long?)null : (long?)row["TL_ID"],
                                            OpeningBalance = row["OpeningBalance"] == DBNull.Value ? (decimal?)null : (decimal?)row["OpeningBalance"],
                                            ClosingBalance = row["ClosingBalance"] == DBNull.Value ? (decimal?)null : (decimal?)row["ClosingBalance"],
                                            TransDate = row["TranDate"] == DBNull.Value ? (DateTime?)null : (DateTime?)row["TranDate"],
                                            ChequeDate = row["Cheque_Date"] == DBNull.Value ? (DateTime?)null : (DateTime?)row["Cheque_Date"],
                                        });
                                    }
                                    bankReconciliationDTO.BankName = GetBankName(listData[0].AccountNumber);
                                    bankReconciliationDTO.BankAccountID = listData[0].BankAccountID;
                                }
                            }

                            catch (Exception ex)
                            {

                                Console.WriteLine($"Error at row {count}: {ex.Message}");
                                var exceptionMessage = ex.Message;
                                var fields = $"Error at row {count}";
                                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception") ? ex.InnerException?.Message : ex.Message;
                                errorMessage = errorMessage + fields;
                                Eduegate.Logger.LogHelper<string>.Fatal($"Exception in FillBankTransctions(BankReconciliationDTO transactionDTO). Error message: {errorMessage}", ex);

                                throw ex;
                            }
                            bankReconciliationDTO.BankReconciliationTransactionDtos.AddRange(listData);
                            return bankReconciliationDTO;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                var exceptionMessage = ex.Message;

                var fields = " SDate: " + transactionDTO.SDate.ToString() + "EDate: " + transactionDTO.EDate.ToString() + "BankAccountID" + transactionDTO.BankAccountID.ToString() + "FeedLogID" + transactionDTO;
                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception") ? ex.InnerException?.Message : ex.Message;
                errorMessage = errorMessage + fields;
                Eduegate.Logger.LogHelper<string>.Fatal($"Exception in FillBankTransctions(BankReconciliationDTO transactionDTO). Error message: {errorMessage}", ex);

                throw ex;
            }

        }
        public string GetBankName(string accountNumber)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var dfndt = dbContext.BankAccounts
                    .Where(a => a.AccountNumber == accountNumber)
                    .Include(i => i.Bank)
                    .AsNoTracking()
                    .FirstOrDefault();
                return dfndt?.Bank?.BankName;

            }
        }

        public List<BankStatementEntryDTO> SaveBankStatementEntry(List<BankStatementEntryDTO> bankStatementEntryDTO)
        {
            try
            {
                var bankStatementEntries = new List<BankStatementEntry>();
                using (var dbContext = new dbEduegateERPContext())
                {
                    bankStatementEntries = (from s in bankStatementEntryDTO
                                            select new BankStatementEntry()
                                            {
                                                BankStatementEntryIID = s.BankStatementEntryIID,
                                                BankStatementID = s.BankStatementID,
                                                ChequeNo = s.ChequeNo,
                                                Credit = s.Credit,
                                                SlNO = s.SlNO,
                                                Debit = s.Debit,
                                                PartyName = s.PartyName,
                                                Description = s.Description,
                                                PostDate = s.PostDate.Value,
                                                Balance = s.Balance,
                                                ReferenceNo = s.ReferenceNo,
                                                ChequeDate = (DateTime?)null,
                                            }).ToList();


                    foreach (var entry in bankStatementEntries)
                    {
                        if (entry.PostDate < new DateTime(1753, 1, 1) || entry.PostDate > new DateTime(9999, 12, 31))
                        {
                            // Handle invalid date
                            throw new Exception($"Invalid PostDate: {entry.PostDate}");
                        }
                        dbContext.BankStatementEntries.Add(entry);
                        if (entry.BankStatementEntryIID == 0)
                        {
                            dbContext.Entry(entry).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                        else
                        {
                            dbContext.Entry(entry).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }

                    }
                    dbContext.SaveChanges();

                    if (bankStatementEntries.Any())
                    {
                        foreach (var dtoEntry in bankStatementEntryDTO)
                        {
                            var matchingEntityEntry = bankStatementEntries
                                .FirstOrDefault(e =>
                                    e.Description == dtoEntry.Description &&
                                    e.SlNO == dtoEntry.SlNO &&
                                    e.ReferenceNo == dtoEntry.ReferenceNo &&
                                    e.ChequeNo == dtoEntry.ChequeNo);

                            if (matchingEntityEntry != null)
                            {
                                dtoEntry.BankStatementEntryIID = matchingEntityEntry.BankStatementEntryIID;
                            }
                        }
                    }
                    return bankStatementEntryDTO;
                }
            }
            catch (Exception ex)
            {
                var exceptionMessage = ex.Message;

                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception") ? ex.InnerException?.Message : ex.Message;

                Eduegate.Logger.LogHelper<string>.Fatal($"Exception in List<BankStatementEntry> bankStatementEntryDTO. Error message: {errorMessage}", ex);

                throw ex;
            }
        }

        public BankStatementDTO SaveBankStatement(BankStatementDTO bankStatementDTO)
        {
            try
            {
                using (var dbContext = new dbEduegateERPContext())
                {
                    //Map DTO to Entities
                    #region Map Entities

                    var _sLstBudgetEntryIDs = new List<long>();
                    var _sLstBudgetEntryAccountMapIDs = new List<long>();


                    var entity = new BankStatement()
                    {
                        BankStatementIID = bankStatementDTO.BankStatementIID,
                        ContentFileID = bankStatementDTO.ContentFileID,
                        ContentFileName = bankStatementDTO.ContentFileName,
                        ExtractedTextData = bankStatementDTO.ExtractedTextData,
                        CreatedBy = bankStatementDTO.BankStatementIID == 0 ? (int)_context.LoginID : bankStatementDTO.CreatedBy,
                        UpdatedBy = bankStatementDTO.BankStatementIID > 0 ? (int)_context.LoginID : bankStatementDTO.UpdatedBy,
                        CreatedDate = bankStatementDTO.BankStatementIID == 0 ? DateTime.Now : bankStatementDTO.CreatedDate,
                        UpdatedDate = bankStatementDTO.BankStatementIID > 0 ? DateTime.Now : bankStatementDTO.UpdatedDate,

                    };
                    #endregion Map Entities

                    #region Delete Entities

                    var removableBankStatementEntries = dbContext.BankStatementEntries
                                         .Where(x => x.BankStatementID == entity.BankStatementIID).ToList();

                    if (removableBankStatementEntries.Any())
                        dbContext.BankStatementEntries.RemoveRange(removableBankStatementEntries);
                    dbContext.SaveChanges();



                    #endregion Delete Entities

                    //Add or Modify entities
                    #region Save Entities
                    entity.BankStatementEntries = (from s in bankStatementDTO.BankStatementEntries
                                                   select new BankStatementEntry()
                                                   {
                                                       BankStatementEntryIID = s.BankStatementEntryIID,
                                                       BankStatementID = bankStatementDTO.BankStatementIID,
                                                       ChequeNo = s.ChequeNo,
                                                       Credit = s.Credit,
                                                       SlNO = s.SlNO,
                                                       Debit = s.Debit,
                                                       PartyName = s.PartyName,
                                                       Description = s.Description,
                                                       PostDate = s.PostDate.Value,
                                                       Balance = s.Balance,
                                                       ReferenceNo = s.ReferenceNo,
                                                       ChequeDate = (DateTime?)null,
                                                   }).ToList();

                    #endregion Save Entities
                    foreach (var entry in entity.BankStatementEntries)
                    {
                        if (entry.PostDate < new DateTime(1753, 1, 1) || entry.PostDate > new DateTime(9999, 12, 31))
                        {
                            // Handle invalid date
                            throw new Exception($"Invalid PostDate: {entry.PostDate}");
                        }
                    }
                    dbContext.BankStatements.Add(entity);

                    if (entity.BankStatementIID == 0)
                    {
                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    }
                    else
                    {
                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }
                    dbContext.SaveChanges();

                    bankStatementDTO.BankStatementIID = entity.BankStatementIID;

                    if (entity.BankStatementEntries.Any())
                    {
                        foreach (var dtoEntry in bankStatementDTO.BankStatementEntries)
                        {
                            var matchingEntityEntry = entity.BankStatementEntries
                                .FirstOrDefault(e =>
                                    e.Description == dtoEntry.Description &&
                                    e.SlNO == dtoEntry.SlNO &&
                                    e.ReferenceNo == dtoEntry.ReferenceNo &&
                                    e.ChequeNo == dtoEntry.ChequeNo);

                            if (matchingEntityEntry != null)
                            {
                                dtoEntry.BankStatementEntryIID = matchingEntityEntry.BankStatementEntryIID;
                            }
                        }
                    }
                    return bankStatementDTO;
                }
            }
            catch (Exception ex)
            {
                var exceptionMessage = ex.Message;
                var fields = " ContentFileID: " + bankStatementDTO.ContentFileID.ToString() + "ContentFileName: " + bankStatementDTO.ContentFileName.ToString() + "ExtractedTextData" + bankStatementDTO.ExtractedTextData;
                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception") ? ex.InnerException?.Message : ex.Message;
                errorMessage = errorMessage + fields;
                Eduegate.Logger.LogHelper<string>.Fatal($"Exception in SaveBankStatement(BankStatementDTO bankStatementDTO). Error message: {errorMessage}", ex);

                throw ex;
            }
        }
        public BankReconciliationHeadDTO SaveBankReconciliationMatchedEntry(BankReconciliationHeadDTO bankReconciliationHeadDTO)
        {
            try
            {
                using (var dbContext = new dbEduegateERPContext())
                {
                    var bankReconciliationDetails = (from s in bankReconciliationHeadDTO.BankReconciliationDetailDtos
                                                     select new BankReconciliationDetail()
                                                     {
                                                         ReconciliationDetailIID = s.ReconciliationDetailIID,
                                                         BankReconciliationHeadID = bankReconciliationHeadDTO.BankReconciliationHeadIID,
                                                         TranHeadID = null,
                                                         TranTailID = s.TranTailID,
                                                         SlNo = s.SlNo,
                                                         AccountID = s.AccountID,
                                                         ReconciliationDate = s.ReconciliationDate,
                                                         Remarks = s.Remarks,
                                                         Amount = s.Amount,
                                                         BankReconciliationMatchedStatusID = s.BankReconciliationMatchedStatusID,
                                                         //ChequeDate = s.ChequeDate,
                                                         ChequeNo = s.ChequeNo,
                                                         PartyName = s.PartyName,
                                                         Reference = s.Reference,
                                                         BankStatementEntryID = s.BankStatementEntryID,
                                                         ReferenceGroupNO = s.ReferenceGroupNO,
                                                         ReferenceGroupName = s.ReferenceGroupName,
                                                         BankReconciliationMatchingEntries = s.BankReconciliationMatchingEntry.Count() > 0 ? GetMatchingEntry(s.BankReconciliationMatchingEntry) : new List<BankReconciliationMatchingEntry>()
                                                     }).ToList();
                    foreach (var detail in bankReconciliationDetails)
                    {
                        foreach (var matchingDetail in detail.BankReconciliationMatchingEntries)
                        {
                            if (matchingDetail.BankReconciliationMatchingEntryIID == 0)
                            {
                                dbContext.Entry(matchingDetail).State = Microsoft.EntityFrameworkCore.EntityState.Added;

                            }
                            else
                            {
                                dbContext.Entry(matchingDetail).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }
                        }
                        if (detail.ReconciliationDetailIID == 0)
                        {
                            dbContext.Entry(detail).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                        else
                        {
                            dbContext.Entry(detail).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                    }
                    foreach (var dtoEntry in bankReconciliationHeadDTO.BankReconciliationDetailDtos)
                    {
                        var matchingEntityEntry = bankReconciliationDetails
                            .FirstOrDefault(e =>
                                e.Remarks == dtoEntry.Remarks &&
                                e.Amount == dtoEntry.Amount &&
                                e.ReconciliationDate.Value.Date == dtoEntry.ReconciliationDate.Value.Date &&
                                e.ChequeNo == dtoEntry.ChequeNo);

                        if (matchingEntityEntry != null)
                        {
                            dtoEntry.ReconciliationDetailIID = matchingEntityEntry.ReconciliationDetailIID;
                        }

                        foreach (var item in dtoEntry.BankReconciliationMatchingEntry)
                        {
                            var matchingEntityEntryDet = bankReconciliationDetails
                                .SelectMany(x => x.BankReconciliationMatchingEntries)
                                .FirstOrDefault(e =>
                                    e.Remarks == dtoEntry.Remarks &&
                                    e.Amount == dtoEntry.Amount &&
                                    e.AccountID == dtoEntry.AccountID &&
                                    e.BankStatementEntryID == dtoEntry.BankStatementEntryID &&
                                    e.ChequeNo == dtoEntry.ChequeNo);

                            if (matchingEntityEntryDet != null)
                            {
                                item.BankReconciliationMatchingEntryIID = matchingEntityEntryDet.BankReconciliationMatchingEntryIID;
                            }
                        }
                    }

                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                var exceptionMessage = ex.Message;
                var fields = " BankReconciliationHeadIID: " + bankReconciliationHeadDTO.BankReconciliationHeadIID.ToString() + "BankStatementID: " + bankReconciliationHeadDTO.BankStatementID.ToString();
                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception") ? ex.InnerException?.Message : ex.Message;
                errorMessage = errorMessage + fields;
                Eduegate.Logger.LogHelper<string>.Fatal($"Exception in SaveBankReconciliationMatchedEntry(BankReconciliationHeadDTO bankReconciliationHeadDTO). Error message: {errorMessage}", ex);

                throw ex;
            }
            return bankReconciliationHeadDTO;
        }
        public string SaveBankReconciliationEntry(BankReconciliationHeadDTO bankReconciliationHeadDTO)
        {
            try
            {
                using (var dbContext = new dbEduegateERPContext())
                {
                    //Map DTO to Entities
                    #region Map Entities

                    var _sLstBudgetEntryIDs = new List<long>();
                    var _sLstBudgetEntryAccountMapIDs = new List<long>();


                    var entity = new BankReconciliationHead()
                    {
                        BankReconciliationHeadIID = bankReconciliationHeadDTO.BankReconciliationHeadIID,
                        BankStatementID = bankReconciliationHeadDTO.BankStatementID,
                        BankAccountID = bankReconciliationHeadDTO.BankAccountID,

                        FromDate = bankReconciliationHeadDTO.FromDate,

                        ToDate = bankReconciliationHeadDTO.ToDate,

                        OpeningBalanceAccount = bankReconciliationHeadDTO.OpeningBalanceAccount,

                        OpeningBalanceBankStatement = bankReconciliationHeadDTO.OpeningBalanceBankStatement,

                        ClosingBalanceAccount = bankReconciliationHeadDTO.ClosingBalanceAccount,

                        ClosingBalanceBankStatement = bankReconciliationHeadDTO.ClosingBalanceBankStatement,

                        BankReconciliationStatusID = bankReconciliationHeadDTO.BankReconciliationStatusID,
                        CreatedBy = bankReconciliationHeadDTO.BankReconciliationHeadIID == 0 ? (int)_context.LoginID : bankReconciliationHeadDTO.CreatedBy,
                        UpdatedBy = bankReconciliationHeadDTO.BankReconciliationHeadIID > 0 ? (int)_context.LoginID : bankReconciliationHeadDTO.UpdatedBy,
                        CreatedDate = bankReconciliationHeadDTO.BankReconciliationHeadIID == 0 ? DateTime.Now : bankReconciliationHeadDTO.CreatedDate,
                        UpdatedDate = bankReconciliationHeadDTO.BankReconciliationHeadIID > 0 ? DateTime.Now : bankReconciliationHeadDTO.UpdatedDate,

                    };
                    #endregion Map Entities

                    #region Delete Entities




                    var removablebankReconciliationEntities = dbContext.BankReconciliationDetails
                                         .Where(x => x.BankReconciliationHeadID == bankReconciliationHeadDTO.BankReconciliationHeadIID
                                          && x.IsOpening != true && x.IsReconciled != true
                                          ).ToList();
                    var bankReconciliationDetailIDs = removablebankReconciliationEntities.Select(x => x.ReconciliationDetailIID).ToList();
                    var removeBankReconciliationMatchingEntries = dbContext.BankReconciliationMatchingEntries
                   .Where(x => bankReconciliationDetailIDs.Contains(x.BankReconciliationDetailID ?? 0)
                                     ).ToList();
                    if (removeBankReconciliationMatchingEntries.Any())
                        dbContext.BankReconciliationMatchingEntries.RemoveRange(removeBankReconciliationMatchingEntries);
                    dbContext.SaveChanges();

                    if (removablebankReconciliationEntities.Any())
                        dbContext.BankReconciliationDetails.RemoveRange(removablebankReconciliationEntities);
                    dbContext.SaveChanges();

                    #endregion Delete Entities

                    //Add or Modify entities
                    #region Save Entities
                    entity.BankReconciliationDetails = (from s in bankReconciliationHeadDTO.BankReconciliationDetailDtos
                                                        select new BankReconciliationDetail()
                                                        {
                                                            ReconciliationDetailIID = s.ReconciliationDetailIID,
                                                            BankReconciliationHeadID = bankReconciliationHeadDTO.BankReconciliationHeadIID,
                                                            TranHeadID = null,
                                                            TranTailID = s.TranTailID,
                                                            SlNo = s.SlNo,
                                                            AccountID = s.AccountID,
                                                            ReconciliationDate = s.ReconciliationDate,
                                                            Remarks = s.Remarks,
                                                            Amount = s.Amount,
                                                            BankReconciliationMatchedStatusID = s.BankReconciliationMatchedStatusID,
                                                            //ChequeDate = s.ChequeDate,
                                                            ChequeNo = s.ChequeNo,
                                                            PartyName = s.PartyName,
                                                            Reference = s.Reference,
                                                            BankStatementEntryID = s.BankStatementEntryID,
                                                            ReferenceGroupNO = s.ReferenceGroupNO,
                                                            ReferenceGroupName = s.ReferenceGroupName,
                                                            BankReconciliationMatchingEntries = s.BankReconciliationMatchingEntry.Count() > 0 ? GetMatchingEntry(s.BankReconciliationMatchingEntry) : new List<BankReconciliationMatchingEntry>()
                                                        }).ToList();


                    dbContext.BankReconciliationHeads.Add(entity);


                    if (entity.BankReconciliationHeadIID == 0)
                    {
                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    }
                    else
                    {
                        foreach (var detail in entity.BankReconciliationDetails)
                        {
                            foreach (var matchingDetail in detail.BankReconciliationMatchingEntries)
                            {
                                if (matchingDetail.BankReconciliationMatchingEntryIID == 0)
                                {
                                    dbContext.Entry(matchingDetail).State = Microsoft.EntityFrameworkCore.EntityState.Added;

                                }
                                else
                                {
                                    dbContext.Entry(matchingDetail).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                }
                            }
                            if (detail.ReconciliationDetailIID == 0)
                            {
                                dbContext.Entry(detail).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            }
                            else
                            {
                                dbContext.Entry(detail).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }
                        }
                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }
                    dbContext.SaveChanges();


                    #endregion Save Entities
                    bankReconciliationHeadDTO.BankReconciliationHeadIID = entity.BankReconciliationHeadIID;
                    //foreach (var dtoEntry in bankReconciliationHeadDTO.BankReconciliationDetailDtos)
                    //{
                    //    var matchingEntityEntry = entity.BankReconciliationDetails
                    //        .FirstOrDefault(e =>
                    //            e.Remarks == dtoEntry.Remarks &&
                    //            e.Amount == dtoEntry.Amount &&
                    //            e.ReconciliationDate.Value.Date == dtoEntry.ReconciliationDate.Value.Date &&
                    //            e.ChequeNo == dtoEntry.ChequeNo);

                    //    if (matchingEntityEntry != null)
                    //    {
                    //        dtoEntry.ReconciliationDetailIID = matchingEntityEntry.ReconciliationDetailIID;
                    //    }

                    //    foreach (var item in dtoEntry.BankReconciliationMatchingEntry)
                    //    {
                    //        var matchingEntityEntryDet = entity.BankReconciliationDetails
                    //            .SelectMany(x => x.BankReconciliationMatchingEntries)
                    //            .FirstOrDefault(e =>
                    //                e.Remarks == dtoEntry.Remarks &&
                    //                e.Amount == dtoEntry.Amount &&
                    //                e.AccountID == dtoEntry.AccountID &&
                    //                e.BankStatementEntryID == dtoEntry.BankStatementEntryID &&
                    //                e.ChequeNo == dtoEntry.ChequeNo);

                    //        if (matchingEntityEntryDet != null)
                    //        {
                    //            item.BankReconciliationMatchingEntryIID = matchingEntityEntryDet.BankReconciliationMatchingEntryIID;
                    //        }
                    //    }
                    //}

                    return bankReconciliationHeadDTO.BankReconciliationHeadIID.ToString();

                }

                //#endregion DB Updates
            }
            catch (Exception ex)
            {
                var exceptionMessage = ex.Message;
                var fields = " BankReconciliationHeadIID: " + bankReconciliationHeadDTO.BankReconciliationHeadIID.ToString() + "BankStatementID: " + bankReconciliationHeadDTO.BankStatementID.ToString();
                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception") ? ex.InnerException?.Message : ex.Message;
                errorMessage = errorMessage + fields;
                Eduegate.Logger.LogHelper<string>.Fatal($"Exception in SaveBankReconciliationEntry(BankReconciliationHeadDTO bankReconciliationHeadDTO). Error message: {errorMessage}", ex);

                throw ex;
            }

        }

        public List<BankReconciliationMatchingEntry> GetMatchingEntry(List<BankReconciliationMatchingEntryDTO> matchingEntryDTO)
        {
            var matchingEntry = new List<BankReconciliationMatchingEntry>();
            matchingEntry = (from s in matchingEntryDTO
                             select new BankReconciliationMatchingEntry()
                             {
                                 BankReconciliationMatchingEntryIID = s.BankReconciliationMatchingEntryIID,
                                 BankReconciliationDetailID = s.BankReconciliationDetailID,
                                 BankReconciliationHeadID = s.BankReconciliationHeadID,
                                 TranHeadID = null,
                                 TranTailID = s.TranTailID,
                                 SlNo = s.SlNo,
                                 AccountID = s.AccountID,
                                 ReconciliationDate = s.ReconciliationDate,
                                 Remarks = s.Remarks,
                                 Amount = s.Amount,
                                 ChequeNo = s.ChequeNo,
                                 //ChequeDate=s.ChequeDate,
                                 PartyName = s.PartyName,
                                 Reference = s.Reference,
                                 BankStatementEntryID = s.BankStatementEntryID,
                                 ReferenceGroupNO = s.ReferenceGroupNO,
                                 ReferenceGroupName = s.ReferenceGroupName,

                             }).ToList();
            return matchingEntry;

        }
    }
}
