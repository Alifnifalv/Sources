using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Enums;
using Eduegate.Services.Contracts;
using Eduegate.Domain.Entity;
using Microsoft.Data.SqlClient;

namespace Eduegate.Domain.Repository
{
    public class AccountTransactionRepository
    {
        public CustomerAccountMap GetCustomerAccountMap(long CustomerID, int? EntitlementID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                //return dbContext.CustomerAccountMaps.Where(x => x.CustomerID == CustomerID && x.EntitlementID == EntitlementID).FirstOrDefault();
                return dbContext.CustomerAccountMaps.Where(x => x.CustomerID == CustomerID).AsNoTracking().FirstOrDefault();
            }
        }

        public SupplierAccountMap GetSupplierAccountMap(long SupplierID, int EntitlementID)
        {
            int? EntitlementIDValue = EntitlementID == -1 ? null : (int?)(EntitlementID);
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                //return dbContext.SupplierAccountMaps.Where(x => x.SupplierID == SupplierID && (x.EntitlementID == EntitlementIDValue || EntitlementIDValue == null)).FirstOrDefault();
                return dbContext.SupplierAccountMaps.Where(x => x.SupplierID == SupplierID).AsNoTracking().FirstOrDefault();
            }
        }

        public bool DeleteAccountTransactions(List<long> headIDs)
        {
            bool blnReturn = false;
            try
            {
                using (var dbContext = new dbEduegateERPContext())
                {
                    foreach (var headID in headIDs)
                    {
                        foreach (var head in dbContext.AccountTransactionInventoryHeadMaps.Where(a => a.TransactionHeadID == headID))
                        {
                            dbContext.AccountTransactions.Remove(head.AccountTransaction);
                            dbContext.AccountTransactionInventoryHeadMaps.Remove(head);
                        }
                    }

                    dbContext.SaveChanges();
                    blnReturn = true;
                }

                return blnReturn;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool AddAccountTransactions(IList<AccountTransaction> transactionList)
        {
            bool blnReturn = false;
            try
            {
                using (var dbContext = new dbEduegateERPContext())
                {
                    foreach (var accountTransactionItem in transactionList)
                    {
                        var trans = new List<TransactionHeadAccountMap>();
                        trans.Add(accountTransactionItem.TransactionHeadAccountMaps.FirstOrDefault());
                        accountTransactionItem.TransactionHeadAccountMaps = null;
                        dbContext.AccountTransactions.Add(accountTransactionItem);
                        dbContext.Entry(accountTransactionItem).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        dbContext.SaveChanges();

                        //var tranMap = trans.FirstOrDefault();
                        //tranMap.AccountTransactionID = accountTransactionItem.TransactionIID;
                        //dbContext.TransactionHeadAccountMaps.Add(tranMap);
                        //dbContext.SaveChanges();
                    }


                    blnReturn = true;
                }

                return blnReturn;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool AddFixedAssetAccountingTransactions(IList<AccountTransaction> AccountTransactionList)
        {
            bool blnReturn = false;
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                foreach (AccountTransaction AccountTransactionItem in AccountTransactionList)
                {
                    dbContext.AccountTransactions.Add(AccountTransactionItem);
                }

                dbContext.SaveChanges();
                blnReturn = true;
            }

            return blnReturn;
        }

        public bool UpdateAccountTransactionProcessStatus(TransactionHead TransactionHeadDBModel)
        {
            bool blnReturn = false;
            using (var dbContext = new dbEduegateERPContext())
            {
                var TransactionHeadFromDB = dbContext.AccountTransactionHeads.Where(a => a.AccountTransactionHeadIID == TransactionHeadDBModel.HeadIID).AsNoTracking().FirstOrDefault();

                if (TransactionHeadFromDB != null)
                {
                    TransactionHeadFromDB.TransactionStatusID = TransactionHeadDBModel.TransactionStatusID;
                }

                dbContext.Entry(TransactionHeadFromDB).State = EntityState.Modified;

                dbContext.SaveChanges();
            }
            return blnReturn;
        }

        public bool Update_AssetTransaction_ProcessStatus(long HeadID, int TransactionStatus)
        {
            bool blnReturn = false;
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    AssetTransactionHead TransactionHeadFromDB = dbContext.AssetTransactionHeads.Where(a => a.HeadIID == HeadID).AsNoTracking().FirstOrDefault();
                    if (TransactionHeadFromDB != null)
                    {
                        TransactionHeadFromDB.ProcessingStatusID = (byte)TransactionStatus;
                    }
                    dbContext.SaveChanges();
                    blnReturn = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return blnReturn;
        }

        public bool Update_AccountingTransaction_ProcessStatus(long HeadID, int TransactionStatus)
        {
            bool blnReturn = false;

            try
            {
                using (var dbContext = new dbEduegateERPContext())
                {
                    var TransactionHeadFromDB = dbContext.AccountTransactionHeads.Where(a => a.AccountTransactionHeadIID == HeadID).AsNoTracking().FirstOrDefault();

                    if (TransactionHeadFromDB != null)
                    {
                        TransactionHeadFromDB.TransactionStatusID = (byte)TransactionStatus;
                    }

                    dbContext.SaveChanges();
                    blnReturn = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return blnReturn;
        }

        public bool Update_MissionJob_ProcessStatus(long HeadID, int TransactionStatus)
        {
            bool blnReturn = false;
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    JobEntryHead TransactionHeadFromDB = dbContext.JobEntryHeads.Where(a => a.JobEntryHeadIID == HeadID).AsNoTracking().FirstOrDefault();
                    if (TransactionHeadFromDB != null)
                    {
                        TransactionHeadFromDB.JobOperationStatusID = (byte)TransactionStatus;
                    }
                    dbContext.SaveChanges();
                    blnReturn = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return blnReturn;
        }

        public List<InvetoryTransaction> GetInvetoryTransactionsByTransactionHeadID(long TransactionHeadID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.InvetoryTransactions.Where(x => x.HeadID == TransactionHeadID).AsNoTracking().ToList();
            }
        }

        public Account SetAccountChildLastID(long AccountID, int noOfChildAccounts)
        {
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    Account baseAccount = dbContext.Accounts.Where(x => x.AccountID == AccountID).AsNoTracking().FirstOrDefault();
                    dbContext.Entry(baseAccount).Reference(a => a.Group).Load();
                    dbContext.Entry(baseAccount).Reference(a => a.ParentAccount).Load();
                    dbContext.Entry(baseAccount).Reference(a => a.AccountBehavoir).Load();

                    if (baseAccount != null)
                    {
                        baseAccount.ChildLastID = (baseAccount.ChildLastID == null ? 0 : (long)baseAccount.ChildLastID) + noOfChildAccounts;
                        dbContext.SaveChanges();
                    }
                    return baseAccount;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<TransactionHeadEntitlementMap> GetTransactionEntitlementByHeadId(long headId)
        {
            using (var db = new dbEduegateERPContext())
            {
                return db.TransactionHeadEntitlementMaps.Where(x => x.TransactionHeadID == headId)
                     .Include(x => x.EntityTypeEntitlement).AsNoTracking().ToList();
            }
        }

        public AccountTransaction GetDayBookData(long IID)
        {
            using (dbEduegateERPContext dbcontext = new dbEduegateERPContext())
            {
                return dbcontext.AccountTransactions.Where(x => x.TransactionIID == IID).AsNoTracking().FirstOrDefault();
            }
        }

        public string GetNextTransactionNumber(int documentTypeID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var documentType = dbContext.DocumentTypes.Where(x => x.DocumentTypeID == documentTypeID).AsNoTracking().FirstOrDefault();
                if (documentType != null)
                {
                    documentType.LastTransactionNo = documentType.LastTransactionNo.HasValue ? documentType.LastTransactionNo + 1 : 1;

                    dbContext.Entry(documentType).State = EntityState.Modified;
                    dbContext.SaveChanges();

                    return documentType.TransactionNoPrefix + documentType.LastTransactionNo.ToString();
                }
                else
                {
                    return null;
                }
            }
        }

        public AccountTransactionHead SaveAccountTransactionHead(AccountTransactionHead accountTransactionHead)
        {
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    long HeadIID = accountTransactionHead.AccountTransactionHeadIID;
                    var transactionHeadInDB = dbContext.AccountTransactionHeads.Where(x => x.AccountTransactionHeadIID == HeadIID)
                        .Include(i => i.AccountTransactionDetails)
                        .AsNoTracking().FirstOrDefault();

                    if (transactionHeadInDB == null)
                    {
                        if (accountTransactionHead.DocumentTypeID.HasValue)
                        {
                            accountTransactionHead.TransactionNumber = GetNextTransactionNumber(accountTransactionHead.DocumentTypeID.Value);
                        }
                        if (accountTransactionHead.DocumentStatusID == (int)Eduegate.Services.Contracts.Enums.DocumentStatuses.Completed)
                        {
                            accountTransactionHead.TransactionStatusID = (int)Eduegate.Services.Contracts.Enums.TransactionStatus.Complete;
                        }

                        dbContext.AccountTransactionHeads.Add(accountTransactionHead);
                    }
                    else
                    {
                        #region EDIT
                        transactionHeadInDB.AccountID = accountTransactionHead.AccountID;
                        transactionHeadInDB.AdvanceAmount = accountTransactionHead.AdvanceAmount;
                        transactionHeadInDB.AmountPaid = accountTransactionHead.AmountPaid;
                        transactionHeadInDB.CostCenterID = accountTransactionHead.CostCenterID;
                        transactionHeadInDB.CurrencyID = accountTransactionHead.CurrencyID;
                        transactionHeadInDB.DocumentStatusID = accountTransactionHead.DocumentStatusID;
                        transactionHeadInDB.DocumentTypeID = accountTransactionHead.DocumentTypeID;
                        transactionHeadInDB.ExchangeRate = accountTransactionHead.ExchangeRate;
                        transactionHeadInDB.IsPrePaid = accountTransactionHead.IsPrePaid;
                        transactionHeadInDB.PaymentModeID = accountTransactionHead.PaymentModeID;
                        transactionHeadInDB.Remarks = accountTransactionHead.Remarks;
                        transactionHeadInDB.Reference = accountTransactionHead.Reference;
                        transactionHeadInDB.TransactionDate = accountTransactionHead.TransactionDate;
                        transactionHeadInDB.UpdatedBy = accountTransactionHead.UpdatedBy;
                        transactionHeadInDB.UpdatedDate = accountTransactionHead.UpdatedDate;
                        transactionHeadInDB.DocumentStatusID = accountTransactionHead.DocumentStatusID;
                        transactionHeadInDB.TransactionStatusID = accountTransactionHead.TransactionStatusID;
                        transactionHeadInDB.DiscountAmount = accountTransactionHead.DiscountAmount;
                        transactionHeadInDB.DiscountPercentage = accountTransactionHead.DiscountPercentage;
                        transactionHeadInDB.CompanyID = accountTransactionHead.CompanyID;
                        transactionHeadInDB.BranchID = accountTransactionHead.BranchID;
                        transactionHeadInDB.TransactionStatusID = accountTransactionHead.TransactionStatusID;
                        transactionHeadInDB.ChequeNumber = accountTransactionHead.ChequeNumber;
                        transactionHeadInDB.ChequeDate = accountTransactionHead.ChequeDate;
                        if (transactionHeadInDB.TransactionStatusID == null)
                        {
                            if (accountTransactionHead.DocumentStatusID == (int)Eduegate.Services.Contracts.Enums.DocumentStatuses.Completed)
                            {
                                transactionHeadInDB.TransactionStatusID = (int)Eduegate.Services.Contracts.Enums.TransactionStatus.Complete;
                            }
                        }

                        if (transactionHeadInDB.AccountTransactionDetails == null)
                        {
                            transactionHeadInDB.AccountTransactionDetails = new List<AccountTransactionDetail>();
                        }
                        foreach (AccountTransactionDetail detailItem in accountTransactionHead.AccountTransactionDetails)
                        {

                            var accountTransactionDetailInDb = dbContext.AccountTransactionDetails.Where(x => x.AccountTransactionDetailIID == detailItem.AccountTransactionDetailIID).FirstOrDefault();
                            if (detailItem.AccountTransactionDetailIID == 0)
                            {
                                detailItem.AccountTransactionHeadID = transactionHeadInDB.AccountTransactionHeadIID;

                                transactionHeadInDB.AccountTransactionDetails.Add(detailItem);

                                dbContext.Entry(detailItem).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            }
                            else
                            {
                                accountTransactionDetailInDb.Amount = detailItem.Amount;
                                accountTransactionDetailInDb.AccountID = detailItem.AccountID;
                                accountTransactionDetailInDb.CostCenterID = detailItem.CostCenterID;
                                accountTransactionDetailInDb.ReferenceNumber = detailItem.ReferenceNumber;
                                accountTransactionDetailInDb.Remarks = detailItem.Remarks;
                                accountTransactionDetailInDb.UpdatedBy = detailItem.UpdatedBy;
                                accountTransactionDetailInDb.UpdatedDate = detailItem.UpdatedDate;
                                accountTransactionDetailInDb.AccountID = detailItem.AccountID;
                                accountTransactionDetailInDb.InvoiceAmount = detailItem.InvoiceAmount;
                                accountTransactionDetailInDb.ReturnAmount = detailItem.ReturnAmount;
                                accountTransactionDetailInDb.PaidAmount = detailItem.PaidAmount;
                                accountTransactionDetailInDb.PaymentDueDate = detailItem.PaymentDueDate;
                                accountTransactionDetailInDb.CurrencyID = detailItem.CurrencyID;
                                accountTransactionDetailInDb.ExchangeRate = detailItem.ExchangeRate;
                                accountTransactionDetailInDb.ExternalReference1 = detailItem.ExternalReference1;
                                accountTransactionDetailInDb.ExternalReference2 = detailItem.ExternalReference2;
                                accountTransactionDetailInDb.ExternalReference3 = detailItem.ExternalReference3;
                                accountTransactionDetailInDb.ReferenceQuantity = detailItem.ReferenceQuantity;
                                accountTransactionDetailInDb.ReferenceRate = detailItem.ReferenceRate;
                                accountTransactionDetailInDb.DiscountAmount = detailItem.DiscountAmount;
                                accountTransactionDetailInDb.SubLedgerID = detailItem.SubLedgerID;
                                accountTransactionDetailInDb.BudgetID = detailItem.BudgetID;

                                dbContext.Entry(accountTransactionDetailInDb).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }
                        }
                        #endregion

                        #region DELETE
                        var transDetailIIDs = new HashSet<long>(accountTransactionHead.AccountTransactionDetails.Select(x => x.AccountTransactionDetailIID));
                        var DeletableIds = new HashSet<long>(transactionHeadInDB.AccountTransactionDetails.Where(x => !transDetailIIDs.Contains(x.AccountTransactionDetailIID)).Select(a => a.AccountTransactionDetailIID));
                        dbContext.Entry(transactionHeadInDB).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                        foreach (long id in DeletableIds)
                        {
                            var deleteItem = transactionHeadInDB.AccountTransactionDetails.Where(x => x.AccountTransactionDetailIID == id).FirstOrDefault();
                            if (deleteItem != null)
                            {
                                transactionHeadInDB.AccountTransactionDetails.Remove(deleteItem);

                                dbContext.Entry(deleteItem).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                            }
                        }
                        #endregion

                    }

                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return accountTransactionHead;
        }
        public long? AutoReceiptAccountTransactionSync(long accountTransactionHeadIID, long referenceID, int loginId, int type)
        {
            using (SqlConnection conn = new SqlConnection(Infrastructure.ConfigHelper.GetDefaultConnectionString()))
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
                        return (long?)dataTable.Rows[0]["FeeCollectionID"];
                    }
                    else
                    {
                        return 52711;
                    }
                }
            }
            return (long?)null;
        }
        public List<AdditionalExpensesTransactionsMapDTO> GetAdditionalExpensesTransactions(List<AdditionalExpensesTransactionsMapDTO> additionalExpenseData, long accountTransactionHeadIID, long referenceID)
        {
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    var listAdditionalExpensesTrans = new List<AdditionalExpensesTransactionsMapDTO>();
                    var additionalTransdata = dbContext.AdditionalExpensesTransactionsMaps.Where(x => x.RefInventoryTransactionHeadID == referenceID).AsNoTracking().ToList();
                    if (additionalTransdata.Count() > 0)
                    {
                        var lstAdditionalExp = additionalTransdata.Select(x => x.AdditionalExpenseID);
                        var lstProvisionalAcnt = additionalTransdata.Select(x => x.ProvisionalAccountID);
                        var lstCurrency = additionalTransdata.Select(x => x.ForeignCurrencyID);
                        var localCurrencyID = additionalTransdata.Select(x => x.LocalCurrencyID).FirstOrDefault();

                        var AdditionalExpList = dbContext.AdditionalExpenses.Where(x => lstAdditionalExp.Contains(x.AdditionalExpenseID)).AsNoTracking().ToList();
                        var ProvisionalAcntList = dbContext.Accounts.Where(x => lstProvisionalAcnt.Contains(x.AccountID)).AsNoTracking().ToList();
                        var CurrencyList = dbContext.Currencies.Where(x => lstCurrency.Contains(x.CurrencyID)).AsNoTracking().ToList();

                        foreach (AdditionalExpensesTransactionsMap detail in additionalTransdata)
                        {

                            var additionalExpensesTransactionsMap = new AdditionalExpensesTransactionsMapDTO();

                            additionalExpensesTransactionsMap.AdditionalExpenseID = detail.AdditionalExpenseID;
                            additionalExpensesTransactionsMap.ProvisionalAccountID = detail.ProvisionalAccountID;
                            additionalExpensesTransactionsMap.ForeignCurrencyID = detail.ForeignCurrencyID;
                            additionalExpensesTransactionsMap.LocalCurrencyID = detail.LocalCurrencyID;
                            additionalExpensesTransactionsMap.ExchangeRate = detail.ExchangeRate;
                            additionalExpensesTransactionsMap.ForeignAmount = detail.ForeignAmount;
                            additionalExpensesTransactionsMap.LocalAmount = detail.LocalAmount;
                            additionalExpensesTransactionsMap.FiscalYearID = detail.FiscalYearID;
                            additionalExpensesTransactionsMap.AccountTransactionHeadID = detail.AccountTransactionHeadID;
                            additionalExpensesTransactionsMap.RefInventoryTransactionHeadID = referenceID;
                            additionalExpensesTransactionsMap.RefAccountTransactionHeadID = detail.RefAccountTransactionHeadID;
                            additionalExpensesTransactionsMap.ISAffectSupplier = detail.ISAffectSupplier;
                            additionalExpensesTransactionsMap.AdditionalExpense = AdditionalExpList.Where(x => x.AdditionalExpenseID == detail.AdditionalExpenseID).Select(x => x.AdditionalExpenseName).FirstOrDefault();
                            additionalExpensesTransactionsMap.ProvisionalAccount = ProvisionalAcntList.Where(x => x.AccountID == detail.ProvisionalAccountID).Select(x => x.AccountName).FirstOrDefault();
                            additionalExpensesTransactionsMap.Currency = CurrencyList.Where(x => x.CurrencyID == detail.ForeignCurrencyID).Select(x => x.Name).FirstOrDefault();
                            // add into AdditionalExpensesTransactionsMaps list
                            listAdditionalExpensesTrans.Add(additionalExpensesTransactionsMap);

                        }
                    }
                    return listAdditionalExpensesTrans;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string AdditionalExpensesTransactionsMap(List<AdditionalExpensesTransactionsMapDTO> additionalExpenseData, long accountTransactionHeadIID, long referenceID, int loginId, short documentStatus)
        {

            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {

                    #region DELETE

                    var deleteData = dbContext.AdditionalExpensesTransactionsMaps.Where(x => x.RefInventoryTransactionHeadID == referenceID).ToList();
                    if (deleteData.Count() > 0)
                    {
                        dbContext.AdditionalExpensesTransactionsMaps.RemoveRange(deleteData);
                    }

                    #endregion
                    foreach (AdditionalExpensesTransactionsMapDTO detail in additionalExpenseData)
                    {


                        var additionalExpensesTransactionsMap = new AdditionalExpensesTransactionsMap();

                        additionalExpensesTransactionsMap.AdditionalExpenseID = detail.AdditionalExpenseID;
                        additionalExpensesTransactionsMap.ProvisionalAccountID = detail.ProvisionalAccountID;
                        additionalExpensesTransactionsMap.ForeignCurrencyID = detail.ForeignCurrencyID;
                        additionalExpensesTransactionsMap.LocalCurrencyID = detail.LocalCurrencyID;
                        additionalExpensesTransactionsMap.ExchangeRate = detail.ExchangeRate;
                        additionalExpensesTransactionsMap.ForeignAmount = detail.ForeignAmount;
                        additionalExpensesTransactionsMap.LocalAmount = detail.LocalAmount;
                        additionalExpensesTransactionsMap.FiscalYearID = detail.FiscalYearID;
                        additionalExpensesTransactionsMap.AccountTransactionHeadID = detail.AccountTransactionHeadID;
                        additionalExpensesTransactionsMap.RefInventoryTransactionHeadID = referenceID;
                        additionalExpensesTransactionsMap.RefAccountTransactionHeadID = detail.RefAccountTransactionHeadID;
                        additionalExpensesTransactionsMap.ISAffectSupplier = detail.ISAffectSupplier;

                        // add into AdditionalExpensesTransactionsMaps list
                        dbContext.AdditionalExpensesTransactionsMaps.Add(additionalExpensesTransactionsMap);

                    }

                    dbContext.SaveChanges();
                    //Account Posting
                    if ((Services.Contracts.Enums.DocumentStatuses)documentStatus == Services.Contracts.Enums.DocumentStatuses.Submitted)
                        AccountTransMerge(referenceID, System.DateTime.Now, 0, 11);


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return "Saved Successfully";
        }
        public void AccountTransMerge(long referenceID, DateTime currentDate, int loginId, int type)
        {


            using (SqlConnection conn = new SqlConnection(Infrastructure.ConfigHelper.GetSchoolConnectionString()))
            {
                using (SqlCommand sqlCommand = new SqlCommand("[account].[SPS_ACCOUNT_TRANS_MERGE]", conn))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.Add(new SqlParameter("@REFERENCE_IDs", SqlDbType.BigInt));
                    sqlCommand.Parameters["@REFERENCE_IDs"].Value = referenceID;

                    sqlCommand.Parameters.Add(new SqlParameter("@TRANDATE", SqlDbType.DateTime));
                    sqlCommand.Parameters["@TRANDATE"].Value = currentDate;

                    sqlCommand.Parameters.Add(new SqlParameter("@LOGINID", SqlDbType.Int));
                    sqlCommand.Parameters["@LOGINID"].Value = loginId;

                    sqlCommand.Parameters.Add(new SqlParameter("@TYPE", SqlDbType.Int));
                    sqlCommand.Parameters["@TYPE"].Value = type;

                    try
                    {
                        conn.Open();

                        // Run the stored procedure.
                        sqlCommand.ExecuteNonQuery();


                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Something Wrong! Please check after sometime");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }

        }

        public void AccountTransactionSync(long accountTransactionHeadIID, long referenceID, int loginId, int type)
        {
            using (SqlConnection conn = new SqlConnection(Infrastructure.ConfigHelper.GetDefaultConnectionString()))
            {
                using (SqlCommand sqlCommand = new SqlCommand("[account].[SPS_ACCOUNT_TRANSACTION_SYNC]", conn))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.Add(new SqlParameter("@ACCOUNTTRANHEAD_ID", SqlDbType.BigInt));
                    sqlCommand.Parameters["@ACCOUNTTRANHEAD_ID"].Value = accountTransactionHeadIID;

                    sqlCommand.Parameters.Add(new SqlParameter("@REFERENCE_ID", SqlDbType.BigInt));
                    sqlCommand.Parameters["@REFERENCE_ID"].Value = referenceID;

                    sqlCommand.Parameters.Add(new SqlParameter("@CURRENT_USER_ID", SqlDbType.Int));
                    sqlCommand.Parameters["@CURRENT_USER_ID"].Value = loginId;

                    sqlCommand.Parameters.Add(new SqlParameter("@TYPE", SqlDbType.Int));
                    sqlCommand.Parameters["@TYPE"].Value = type;

                    try
                    {
                        conn.Open();

                        // Run the stored procedure.
                        sqlCommand.ExecuteNonQuery();
                    }

                    catch (Exception ex)
                    {
                        throw new Exception("Something Wrong! Please check after sometime");
                    }

                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }

        public AccountTransactionHead GetAccountTransactionHeadById(long HeadID)
        {
            AccountTransactionHead accountTransactionHead = null;
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    accountTransactionHead = dbContext.AccountTransactionHeads
                    .Include(x => x.AccountTransactionDetails)
                    .Include(x => x.PaymentMode)
                    .Include(x => x.AccountTransactionHeadAccountMaps)
                    //.Include(x => x.DocumentReferenceStatusMap")
                    //.Include(x => x.DocumentReferenceStatusMap).ThenInclude(x => x.DocumentStatus)
                    //.Include(x => x.DocumentReferenceStatusMap1")

                    .Include(x => x.Account)
                    .Include(x => x.DocumentType)
                    .Include(x => x.DocumentStatus)
                    .Include(x => x.AccountTransactionDetails).ThenInclude(x => x.Budget)
                    .Include(x => x.TransactionStatus)
                    .Include(x => x.CostCenter)
                    .Include(x => x.Currency)
                    .Include(x => x.AccountTransactionDetails).ThenInclude(x => x.Account)
                    .Include(x => x.AccountTransactionDetails).ThenInclude(x => x.CostCenter)
                    .Include(x => x.AccountTransactionDetails).ThenInclude(x => x.Accounts_SubLedger)
                    .Include(x => x.AccountTransactionDetails).ThenInclude(x => x.ProductSKUMap)
                    .Include(x => x.AccountTransactionDetails).ThenInclude(x => x.Currency)
                    
                    .Include(x => x.AccountTaxTransactions)
                    .Include(x => x.AccountTaxTransactions).ThenInclude(x => x.TaxTemplate)
                    //.Include(x => x.AccountTaxTransactions).ThenInclude(x => x.TaxTemplateItem)
                    //.Include(x => x.AccountTaxTransactions).ThenInclude(x => x.TaxType)
                    .Where(x => x.AccountTransactionHeadIID == HeadID)
                    .AsNoTracking()
                    .FirstOrDefault();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return accountTransactionHead;
        }

        public List<AccountTransactionHead> GetAccountTransactionHeads(DocumentReferenceTypes referenceType, Eduegate.Framework.Enums.TransactionStatus transactionStatus)
        {
            List<AccountTransactionHead> accountTransactionHeadList = null;
            try
            {
                using (var dbContext = new dbEduegateERPContext())
                {
                    accountTransactionHeadList = dbContext.AccountTransactionHeads
                        .Where(x => x.TransactionStatusID == (int)transactionStatus && x.DocumentType.DocumentReferenceType.ReferenceTypeID == (int)referenceType)
                                                    .Include(x => x.AccountTransactionDetails)
                                                    .Include(x => x.AccountTransactionHeadAccountMaps)
                                                    //.Include(x => x.DocumentReferenceStatusMap)
                                                    //.Include(i => i.DocumentReferenceStatusMap.DocumentStatus")
                                                    .Include(x => x.DocumentType)
                                                    .Include(x => x.TransactionStatus)
                                                    .Include(x => x.DocumentType)
                                                    .Include(x => x.CostCenter)
                                                    .Include(x => x.Currency)
                                                    .Include(x => x.PaymentMode)
                                                    .Include(x => x.DocumentStatus)
                                                    .Include(x => x.AccountTaxTransactions)
                                                     .Include(i => i.AccountTransactionDetails).ThenInclude(i => i.Account)
                                                     .Include(i => i.AccountTransactionDetails).ThenInclude(i => i.CostCenter)
                                                     .Include(i => i.AccountTransactionDetails).ThenInclude(i => i.ProductSKUMap)
                                                     .Include(i => i.AccountTransactionDetails).ThenInclude(i => i.Currency)
                                                    .Include(i => i.DocumentType).ThenInclude(i => i.DocumentReferenceType)
                                                     .Include(i => i.Account)
                                                     .Take(new Domain.Setting.SettingBL(null).GetSettingValue<int>("MaxFetchCount"))
                                                     .AsNoTracking()
                                                    .ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return accountTransactionHeadList;
        }

        public AccountTransactionHead GetTransaction(long headIID)
        {
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    AccountTransactionHead transaction = (from trans in dbContext.AccountTransactionHeads
                                                          where trans.AccountTransactionHeadIID == headIID
                                                          select trans).AsNoTracking().FirstOrDefault();

                    return transaction;
                }
            }

            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<TransactionRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }
        }

        public List<Receivable> GetCustomerPendingInvoices(long customerID)
        {
            List<Receivable> accountTransactionHeadList = null;
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    accountTransactionHeadList = dbContext.Receivables
                        .Include(x => x.TransactionStatus)
                        .Include(x => x.Currency)
                        .Include(x => x.DocumentStatus)
                        .Include(x => x.Account)
                        .Include(x => x.DocumentType)
                        .Where(a => a.AccountID == customerID /*&& (a.PaidAmount == null || a.Amount > a.PaidAmount)*/)
                        .AsNoTracking()
                        .ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return accountTransactionHeadList;
        }

        public void SavePayables(List<Payable> payables)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                foreach (var payable in payables)
                {
                    dbContext.Entry(payable).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
            }
        }

        public void SaveReceivables(List<Receivable> receivables)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                foreach (var receivable in receivables)
                {
                    if (receivable.ReceivableIID != 0)
                        dbContext.Entry(receivable).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    else
                        dbContext.Entry(receivable).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }

                dbContext.SaveChanges();
            }
        }

        public List<Receivable> GetReceivables(List<long> receivableIds, bool loadReferenceData = true)
        {
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    if (loadReferenceData)
                    {
                        return dbContext.Receivables
                            .Include(x => x.TransactionStatus)
                            .Include(x => x.Currency)
                            .Include(x => x.DocumentStatus)
                            .Include(x => x.Account)
                            .Include(x => x.DocumentType)
                            .Include(x => x.DocumentType.DocumentReferenceType)
                            .Where(a => receivableIds.Contains(a.ReceivableIID))
                            .AsNoTracking()
                            .ToList();
                    }
                    else
                    {
                        return dbContext.Receivables
                           .Where(a => receivableIds.Contains(a.ReceivableIID))
                           .AsNoTracking()
                           .ToList();
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Receivable> GetAllocatedReceivable(long receivableId)
        {
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    return dbContext.Receivables
                        .Include(x => x.TransactionStatus)
                        .Include(x => x.Currency)
                        .Include(x => x.DocumentStatus)
                        .Include(x => x.Account)
                        .Include(x => x.DocumentType)
                        .Include(x => x.DocumentType.DocumentReferenceType)
                        .Where(a => a.ReferenceReceivablesID == receivableId && a.DocumentType.ReferenceTypeID != 54)
                        .AsNoTracking()
                        .ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Payable> GetPayables(List<long> ids)
        {
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    return dbContext.Payables
                        .Include(x => x.TransactionStatus)
                        .Include(x => x.Currency)
                        .Include(x => x.DocumentStatus)
                        .Include(x => x.Account)
                        .Include(x => x.DocumentType)
                        .Where(a => ids.Contains(a.PayableIID))
                        .AsNoTracking()
                        .ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Payable> GetSupplierPendingInvoices(long suplierID)
        {
            List<Payable> accountTransactionHeadList = null;
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    accountTransactionHeadList = dbContext.Payables
                        .Include(x => x.TransactionStatus)
                        .Include(x => x.Currency)
                        .Include(x => x.DocumentStatus)
                        .Include(x => x.DocumentType)
                        .Include(x => x.Account)
                        .Where(a => a.AccountID == suplierID && a.Amount > a.PaidAmount)
                        .AsNoTracking()
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return accountTransactionHeadList;
        }

        public List<Receivable> GetMissionReceivablesByAccountId(long AccountID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Receivables
                                    .Where(x => x.AccountID == AccountID)
                                    .Include(x => x.Account)
                                    .Include(x => x.DocumentStatus)
                                    .Include(x => x.DocumentType)

                                    .Include(i => i.JobsEntryHeadReceivableMaps)
                                    .ThenInclude(i => i.JobEntryDetail)

                                    .Include(i => i.TransactionHeadReceivablesMaps)

                                    .Include(i => i.JobsEntryHeadReceivableMaps)
                                    .ThenInclude(i => i.JobEntryDetail)
                                    .ThenInclude(i => i.JobEntryHead1)

                                    .Include(i => i.JobsEntryHeadReceivableMaps)
                                    .ThenInclude(i => i.JobEntryDetail)
                                    .ThenInclude(i => i.JobEntryHead1)
                                    .ThenInclude(i => i.JobEntryDetails)

                                    .Include(i => i.JobsEntryHeadReceivableMaps)
                                    .ThenInclude(i => i.JobEntryDetail)
                                    .ThenInclude(i => i.JobEntryHead1)
                                    .ThenInclude(i => i.TransactionHead)

                                    .Include(i => i.JobsEntryHeadReceivableMaps)
                                    .ThenInclude(i => i.JobEntryDetail)
                                    .ThenInclude(i => i.JobEntryHead1)
                                    .ThenInclude(i => i.TransactionHead)
                                    .ThenInclude(i => i.TransactionHead1)

                                    .Include(i => i.JobsEntryHeadReceivableMaps)
                                    .ThenInclude(i => i.JobEntryDetail)
                                    .ThenInclude(i => i.JobEntryHead1)
                                    .ThenInclude(i => i.TransactionHead)
                                    .ThenInclude(i => i.TransactionHead1)
                                    .ThenInclude(i => i.TransactionDetails)

                                    .Include(i => i.JobsEntryHeadReceivableMaps)
                                    .ThenInclude(i => i.JobEntryDetail)
                                    .ThenInclude(i => i.JobEntryHead1)
                                    .ThenInclude(i => i.TransactionHead)
                                    .ThenInclude(i => i.TransactionHead1)
                                    .ThenInclude(i => i.DocumentType)
                                    .ThenInclude(i => i.DocumentReferenceType)

                                    .Include(i => i.JobsEntryHeadReceivableMaps)
                                    .ThenInclude(i => i.JobEntryDetail)
                                    .ThenInclude(i => i.JobEntryHead1)
                                    .ThenInclude(i => i.TransactionHead)
                                    .ThenInclude(i => i.Currency)

                                    .Include(i => i.JobsEntryHeadReceivableMaps)
                                    .ThenInclude(i => i.JobEntryHead)

                                    .AsNoTracking()
                                    .ToList();
            }
        }

        public List<Receivable> GetInventoryReceivablesByAccountId(long AccountID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Receivables
                                    .Where(x => x.AccountID == AccountID)
                                    .Include(x => x.Account)
                                    .Include(x => x.DocumentStatus)
                                    .Include(x => x.DocumentType)

                                    .Include(i => i.TransactionHeadReceivablesMaps)
                                    .ThenInclude(i => i.TransactionHead)

                                    .Include(i => i.TransactionHeadReceivablesMaps)
                                    .ThenInclude(i => i.TransactionHead)
                                    .ThenInclude(i => i.TransactionDetails)

                                    .Include(i => i.JobsEntryHeadReceivableMaps)

                                    .Include(i => i.TransactionHeadReceivablesMaps)
                                    .ThenInclude(i => i.TransactionHead)
                                    .ThenInclude(i => i.TransactionHead1)

                                    .Include(i => i.TransactionHeadReceivablesMaps)
                                    .ThenInclude(i => i.TransactionHead)
                                    .ThenInclude(i => i.TransactionHead1)
                                    .ThenInclude(i => i.TransactionDetails)

                                    .Include(i => i.TransactionHeadReceivablesMaps)
                                    .ThenInclude(i => i.TransactionHead)
                                    .ThenInclude(i => i.TransactionHead1)
                                    .ThenInclude(i => i.DocumentType)
                                    .ThenInclude(i => i.DocumentReferenceType)

                                    .Include(i => i.TransactionHeadReceivablesMaps)
                                    .ThenInclude(i => i.TransactionHead)
                                    .ThenInclude(i => i.Currency)

                                    .AsNoTracking()
                                    .ToList();
            }
        }

        public bool UpdateReceivablesPaidAmount(long? ReceivableId, decimal amount)
        {
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    Receivable receivable = dbContext.Receivables.Where(x => x.ReceivableIID == ReceivableId).AsNoTracking().FirstOrDefault();
                    if (receivable != null)
                    {
                        receivable.Amount = receivable.Amount - amount;
                    }
                    dbContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Receivable> AddReceivable(List<Receivable> receivablesList)
        {
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    dbContext.Receivables.AddRange(receivablesList);
                    dbContext.SaveChanges();
                }
                return receivablesList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JobEntryHead GetMissionJobEntryHead(long JobEntryHeadID)
        {
            var COD_EntityTypeEntitlements_EntitlementID = 10;//[mutual].[EntityTypeEntitlements] - [EntitlementID-10  COD 2] -- 2 is Customer from [mutual].[EntityTypes]
            /* Only COD Transaction will be fetched*/
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    return dbContext.JobEntryHeads
                            .Include(i => i.JobEntryDetails)
                            .Include(i => i.JobEntryDetails).ThenInclude(i => i.JobEntryHead1)
                            .Include(i => i.JobEntryDetails).ThenInclude(i => i.JobEntryHead1).ThenInclude(i => i.TransactionHead) //--Invoice
                            .Include(i => i.JobEntryDetails).ThenInclude(i => i.JobEntryHead1).ThenInclude(i => i.JobEntryDetails)
                            .Include(i => i.JobEntryDetails).ThenInclude(i => i.JobEntryHead1).ThenInclude(i => i.TransactionHead.TransactionDetails)
                            .Include(i => i.JobEntryDetails).ThenInclude(i => i.JobEntryHead1).ThenInclude(i => i.TransactionHead.TransactionDetails)
                            .Include(i => i.JobEntryDetails).ThenInclude(i => i.JobEntryHead1).ThenInclude(i => i.TransactionHead.TransactionHead1)
                            .Include(i => i.JobEntryDetails).ThenInclude(i => i.JobEntryHead1).ThenInclude(i => i.TransactionHead.TransactionHead1).ThenInclude(i => i.TransactionDetails)
                            .Include(i => i.JobEntryDetails).ThenInclude(i => i.JobEntryHead1).ThenInclude(i => i.TransactionHead.TransactionHead1).ThenInclude(i => i.DocumentType).ThenInclude(i => i.DocumentReferenceType)
                            .Include(i => i.JobsEntryHeadReceivableMaps)

                            .Where(x => x.JobEntryHeadIID == JobEntryHeadID &&
                                    x.JobEntryDetails.Any(heads => heads.JobEntryHead1.TransactionHead.TransactionHeadEntitlementMaps
                                                            .Any(entitlement => entitlement.EntitlementID == COD_EntityTypeEntitlements_EntitlementID)))//COD
                            .AsNoTracking()
                            .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<JobEntryHead> GetAllMissionJobEntryHeads(DocumentReferenceTypes referenceType, Eduegate.Framework.Enums.TransactionStatus transactionStatus)
        {
            List<JobEntryHead> JobEntryHeadList = null;
            try
            {

                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    JobEntryHeadList = dbContext.JobEntryHeads
                                    .Where(x => x.JobOperationStatusID == (int)transactionStatus && x.DocumentType.DocumentReferenceType.ReferenceTypeID == (int)referenceType)
                                    .Include(i => i.JobEntryDetails)
                                    .Include(i => i.JobEntryDetails).ThenInclude(i => i.JobEntryHead1)
                                    .Include(i => i.JobEntryDetails).ThenInclude(i => i.JobEntryHead1).ThenInclude(i => i.TransactionHead)
                                    .Include(i => i.JobEntryDetails).ThenInclude(i => i.JobEntryHead1).ThenInclude(i => i.JobEntryDetails)
                                    .Include(i => i.JobEntryDetails).ThenInclude(i => i.JobEntryHead1).ThenInclude(i => i.TransactionHead).ThenInclude(i => i.TransactionDetails)
                                    .Include(i => i.JobEntryDetails).ThenInclude(i => i.JobEntryHead1).ThenInclude(i => i.TransactionHead).ThenInclude(i => i.TransactionDetails)
                                    .Include(i => i.JobEntryDetails).ThenInclude(i => i.JobEntryHead1).ThenInclude(i => i.TransactionHead).ThenInclude(i => i.TransactionHead1)
                                    .Include(i => i.JobEntryDetails).ThenInclude(i => i.JobEntryHead1).ThenInclude(i => i.TransactionHead).ThenInclude(i => i.TransactionHead1).ThenInclude(i => i.TransactionDetails)
                                    .Include(i => i.JobEntryDetails).ThenInclude(i => i.JobEntryHead1).ThenInclude(i => i.TransactionHead).ThenInclude(i => i.TransactionHead1).ThenInclude(i => i.DocumentType).ThenInclude(i => i.DocumentReferenceType)
                                    .AsNoTracking()
                                    .ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return JobEntryHeadList;
        }

        public Account GetEmployeeAccount(long EmployeeID)
        {
            Account account = null;
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var empAccountMap = dbContext.EmployeeAccountMaps.Include(i => i.Account).Where(x => x.EmployeeID == EmployeeID).FirstOrDefault();
                if (empAccountMap != null)
                {
                    account = empAccountMap.Account;
                }


                if (account == null) // Check -- To Avoid crashes
                {
                    account = dbContext.Accounts.AsNoTracking().FirstOrDefault();
                }
            }
            return account;

        }

        public Receivable GetInventoryReceivable_ByTransHeadId(long TransactionHeadID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Receivables
                                    .Where(x => x.TransactionHeadReceivablesMaps.Any(tr => tr.HeadID == TransactionHeadID))
                                    .AsNoTracking()
                                    .FirstOrDefault();
            }
        }

        public List<Payable> AddPayables(List<Payable> payableEntityList)
        {
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    dbContext.Payables.AddRange(payableEntityList);
                    dbContext.SaveChanges();
                }
                return payableEntityList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Payable> GetInventoryPayablesByAccountId(long AccountID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Payables
                                    .Where(x => x.AccountID == AccountID)
                                    .Include(x => x.Account)
                                    .Include(x => x.DocumentStatus)
                                    .Include(x => x.TransactionStatus)
                                    .Include(x => x.Currency)
                                    .Include(i => i.TransactionHeadPayablesMaps).ThenInclude(i => i.TransactionHead)
                                    .Include(i => i.TransactionHeadPayablesMaps).ThenInclude(i => i.TransactionHead).ThenInclude(i => i.TransactionDetails)
                                    .Include(i => i.TransactionHeadPayablesMaps)
                                    .Include(i => i.TransactionHeadPayablesMaps).ThenInclude(i => i.TransactionHead).ThenInclude(i => i.TransactionHead1)
                                    .Include(i => i.TransactionHeadPayablesMaps).ThenInclude(i => i.TransactionHead).ThenInclude(i => i.TransactionHead1).ThenInclude(i => i.TransactionDetails)
                                    .Include(i => i.TransactionHeadPayablesMaps).ThenInclude(i => i.TransactionHead).ThenInclude(i => i.TransactionHead1).ThenInclude(i => i.DocumentType.DocumentReferenceType)
                                    .Include(i => i.TransactionHeadPayablesMaps).ThenInclude(i => i.TransactionHead).ThenInclude(i => i.Currency)
                                    .AsNoTracking()
                                    .ToList();
            }
        }

        public List<ProductSKUMap> GetProductSKUMapByID(long ProductSKUMapIID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.ProductSKUMaps
                                    .Where(x => x.ProductSKUMapIID == ProductSKUMapIID)
                                    .Include(x => x.Product)
                                    .AsNoTracking()
                                    .ToList();
            }
        }

        public List<Account> GetCustomersAccount(string searchText)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var allocationAllowed = new SettingRepository().GetSettingDetail("RECEIVABLEGLMAINACC");
                var parentAccountID = Convert.ToInt64(allocationAllowed.SettingValue);
                return dbContext.Accounts
                       .Include(a => a.Group).Where(a => a.ParentAccountID == parentAccountID && (a.AccountName.Contains(searchText) || a.AccountCode.Contains(searchText)))
                       .Take(new Domain.Setting.SettingBL(null).GetSettingValue<int>("MaxFetchCount"))
                       .AsNoTracking()
                       .ToList();
            }
        }

        public List<SupplierAccountMap> GetSuppliersAccount(string searchText)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.SupplierAccountMaps.Include(i => i.Account).Where(x => x.EntitlementID == null && (x.Account.AccountName.Contains(searchText) || searchText == null || searchText == "")).AsNoTracking().ToList();
            }
        }

        public bool UpdateProductAvgCost(long ProductSKUId, decimal NewAvgCost)
        {
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    ProductInventory productInventory = dbContext.ProductInventories.Where(x => x.ProductSKUMapID == ProductSKUId).AsNoTracking().FirstOrDefault();
                    if (productInventory != null)
                    {
                        productInventory.CostPrice = NewAvgCost;
                    }
                    dbContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdatePayablesPaidAmount(long? PayableId, decimal paidAmount)
        {
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    Payable payable = dbContext.Payables.Where(x => x.PayableIID == PayableId).AsNoTracking().FirstOrDefault();
                    if (payable != null)
                    {
                        payable.PaidAmount = payable.PaidAmount + paidAmount;
                    }
                    dbContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EmployeeAccountMap> Get_AllDrivers_Accounts(string searchText)
        {
            int EmployeeRoleID_DRIVER = 8; //--FROM [payroll].[EmployeeRoles] -- Also used in Data Feed Driver query
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.EmployeeAccountMaps
                    .Include(i => i.Account)
                    .Include(i => i.Employee)
                    .Include(i => i.Employee).ThenInclude(i => i.EmployeeRoleMaps)
                    .Where(x => x.Employee.EmployeeRoleMaps.Any(roles => roles.EmployeeRoleID == EmployeeRoleID_DRIVER)
                            && (x.Account.AccountName.Contains(searchText) || searchText == null || searchText == "")).AsNoTracking().ToList();
            }

        }

        public List<Account> GetChildAccounts_ByParentAccountId(long ParentAccountID)
        {
            var accounts = new List<Account>();
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var childAccounts = dbContext.Accounts.Where(x => x.ParentAccountID == ParentAccountID).AsNoTracking().ToList();
                foreach (var t in childAccounts)
                {
                    var account = new Account
                    {
                        AccountID = t.AccountID,
                        AccountName = t.AccountName,
                        //Accounts1 = GetChildAccounts_ByParentAccountId(t.AccountID.Value)
                    };
                    accounts.Add(account);
                }
            }
            return accounts;
        }

        public List<Account> GetGLAccounts(string searchText)
        {
            /* This function will return all the Accounts except Supplier, Customer, Driver account*/
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Accounts
                    .Where(x => x.AccountName.Contains(searchText) || searchText == null || searchText == ""
                            && !x.SupplierAccountMaps.Any(acnt => acnt.AccountID == x.AccountID)
                            && !x.CustomerAccountMaps.Any(acnt => acnt.AccountID == x.AccountID)
                            //&& !x.EmployeeAccountMaps.Any(acnt => acnt.AccountID == x.AccountID)
                    )
                    .AsNoTracking()
                    .OrderBy(a => a.AccountName)
                    .ToList();
            }
        }

        public Account GetGLAccountByCode(string code)
        {
            var account = new Account();
            if (code != null)
            {
                /* This function will return all the Accounts except Supplier, Customer, Driver account*/
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    account = dbContext.Accounts
                        .Include(a => a.Group)
                        .Include(a => a.ParentAccount)
                        .Where(x => x.AccountCode.Equals(code)).AsNoTracking().FirstOrDefault();
                }
            }

            return account;
        }

        public List<PaymentMode> GetPaymentModes()
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                return dbContext.PaymentModes.AsNoTracking().ToList();
            }
        }

        public TransactionHead GetParentTransactionHeadByTransHeadID(long headID)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                TransactionHead head = dbContext.TransactionHeads.Include(i => i.TransactionHead2).Where(x => x.HeadIID == headID).AsNoTracking().FirstOrDefault();
                if (head != null)
                {
                    return head.TransactionHead2;
                }

            }
            return null;
        }

        public void ClearPostedData(long accountingHeadID)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var headDetail = dbContext.AccountTransactionHeads.Where(a => a.AccountTransactionHeadIID == accountingHeadID).AsNoTracking().FirstOrDefault();
                dbContext.AccountTransactions.RemoveRange(dbContext.AccountTransactions
                    .Where(a => a.DocumentTypeID == headDetail.DocumentTypeID && a.TransactionNumber == headDetail.TransactionNumber));

                var receivables = dbContext.Receivables
                  .Where(a => a.DocumentTypeID == headDetail.DocumentTypeID && a.TransactionNumber == headDetail.TransactionNumber);
                dbContext.Receivables.RemoveRange(receivables);

                foreach (var recievable in receivables.Where(a => a.ReferenceReceivablesID != null))
                {
                    var existingReference = dbContext.Receivables.Where(a => a.ReceivableIID == recievable.ReferenceReceivablesID).AsNoTracking().LastOrDefault();
                    if (existingReference != null)
                    {
                        existingReference.PaidAmount = existingReference.PaidAmount - recievable.Amount;
                        existingReference.DocumentStatusID = 1; //make this as pending
                    }
                }

                var payables = dbContext.Payables
                  .Where(a => a.DocumentTypeID == headDetail.DocumentTypeID && a.TransactionNumber == headDetail.TransactionNumber);
                dbContext.Payables.RemoveRange(payables);

                foreach (var payable in payables.Where(a => a.ReferencePayablesID != null))
                {
                    var existingReference = dbContext.Payables.Where(a => a.PayableIID == payable.ReferencePayablesID).LastOrDefault();
                    if (existingReference != null)
                    {
                        existingReference.PaidAmount = existingReference.PaidAmount - payable.Amount;
                        existingReference.DocumentStatusID = 1; //make this as pending
                    }
                }
                dbContext.SaveChanges();
            }
        }
    }
}
