using Eduegate.Domain.Entity;
using Eduegate.Domain.Entity.Budgeting.Models;
using Eduegate.Domain.Entity.Budgeting;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Services.Contracts.Budgeting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Eduegate.Domain.Entity.Models;
using System.Runtime.Serialization;
using Eduegate.Services.Contracts.Enums.Synchronizer;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
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
                            //.Include(i => i.BankReconciliationDetails)
                            .Include(i => i.BankReconciliationMatchedStatus)
                            //.Include(i => i.BankStatement)
                            .AsNoTracking()
                            .Where(a => a.BankReconciliationHeadID == IID).ToList();

                        entity.BankReconciliationDetails = details;

                    }
                    return ToDTO(entity);
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
                    BankStatementData = entity.BankStatementID.HasValue ? new BankStatementDTO()
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
                throw ex;
            }
        }
        private List<BankStatementEntryDTO> GetBankStatementEntryDTO(long bankStatementID)
        {
            var bankStatementEntryDTO = new List<BankStatementEntryDTO>();
            using (var dbContext = new dbEduegateERPContext())
            {

                bankStatementEntryDTO = (from s in dbContext.BankStatementEntries
                                         where s.BankStatementID == bankStatementID
                                         select new BankStatementEntryDTO
                                         {
                                             BankStatementEntryIID = s.BankStatementEntryIID,
                                             BankStatementID = bankStatementID,
                                             ChequeNo = s.ChequeNo,
                                             Credit = s.Credit,
                                             SlNO = s.SlNO,
                                             Debit = s.Debit,
                                             PartyName = s.PartyName,
                                             Description = s.Description,
                                             PostDate = s.PostDate.Value,
                                             Balance = s.Balance,
                                             ReferenceNo = s.ReferenceNo,
                                             ChequeDate = s.ChequeDate
                                         }).ToList();
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
                throw ex;
            }

        }

        public BankReconciliationDTO FillBankTransctions(BankReconciliationDTO transactionDTO)
        {
            var bankReconciliationDTO = new BankReconciliationDTO();
            var listData = new List<BankReconciliationTransactionDTO>();
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

                        if (dataTable != null)
                        {
                            foreach (DataRow row in dataTable.Rows)
                            {
                                listData.Add(new BankReconciliationTransactionDTO()
                                {
                                    AccountID = (long?)row["AccountID"],
                                    AccountName = (string)row["AccountName"],
                                    BankAccountID = (int?)row["BankAccountID"],
                                    AccountNumber = (string)row["AccountNumber"],
                                    Narration = (string)row["Narration"],
                                    Debit = (decimal?)row["Debit"],
                                    Credit = (decimal?)row["Credit"],
                                    Amount = (decimal?)row["Amount"],
                                    ChequeNo = (string)row["Cheque_No"],
                                    Partyref = (string)row["Partyref"],
                                    TranHeadID = (long?)row["TH_ID"],
                                    TranTailID = (long?)row["TL_ID"],
                                    OpeningBalance = (decimal?)row["OpeningBalance"],
                                    ClosingBalance = (decimal?)row["ClosingBalance"],
                                    TransDate = row["TranDate"] == DBNull.Value ? (DateTime?)null : (DateTime?)row["TranDate"],
                                    ChequeDate = row["Cheque_Date"] == DBNull.Value ? (DateTime?)null : (DateTime?)row["Cheque_Date"],

                                });
                            }
                            bankReconciliationDTO.BankName = GetBankName(listData[0].AccountNumber);
                            bankReconciliationDTO.BankAccountID = listData[0].BankAccountID;
                        }

                        bankReconciliationDTO.BankReconciliationTransactionDtos.AddRange(listData);
                        return bankReconciliationDTO;
                    }
                }

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

                //if (dfndt != null && dfndt.Bank != null && !string.IsNullOrEmpty(dfndt.Bank.BankName))
                //{
                return dfndt?.Bank?.BankName;
                //}
                //return string.Empty;
            }
        }
        public string SaveBankStatement(BankStatementDTO bankStatementDTO)
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

                return entity.BankStatementIID.ToString();
            }
        }
        public string SaveBankReconciliationEntry(BankReconciliationHeadDTO bankReconciliationHeadDTO)
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
                                     .Where(x => x.BankReconciliationHeadID == bankReconciliationHeadDTO.BankReconciliationHeadIID && x.IsOpening != true && x.IsReconciled != true).ToList();

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
                                                        TranHeadID = s.TranHeadID,
                                                        TranTailID = s.TranTailID,
                                                        SlNo = s.SlNo,
                                                        AccountID = s.AccountID,
                                                        ReconciliationDate = s.ReconciliationDate,
                                                        Remarks = s.Remarks,
                                                        Amount = s.Amount,
                                                        BankReconciliationMatchedStatusID = s.BankReconciliationMatchedStatusID
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

            }

            //#endregion DB Updates

            return "Saved Successfully";
        }
    }
}
