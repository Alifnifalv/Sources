using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Services.Accounting;
using Eduegate.Services.Contracts.Accounts.MonthlyClosing;
using Eduegate.Services.Contracts.Budgeting;
using Eduegate.Services.Contracts.School.Fees;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.SmartView;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Accounts.Accounting;
using Eduegate.Services.Contracts.AI;
using Eduegate.Domain.Entity.Models;
using Eduegate.Services.Contracts.School.Accounts;

namespace Eduegate.Service.Client.Direct.Accounts
{
    public class AccountingServiceClient : IAccountingService
    {
        AccountingService service = new AccountingService();

        public AccountingServiceClient(CallContext context = null, Action<string> logger = null)
        {
            service.CallContext = context;
        }

        public AccountDTO SaveAccount(AccountDTO dto)
        {
            return service.SaveAccount(dto);
        }

        public AccountDTO GetAccount(long accountID)
        {
            return service.GetAccount(accountID);
        }

        public string SaveAccountGroup(AccountsGroupDTO dto)
        {
            return service.SaveAccountGroup(dto);
        }

        public AccountGroupDTO GetAccountGroup(long accountGroupID)
        {
            return service.GetAccountGroup(accountGroupID);
        }

        public ChartOfAccountDTO SaveChartOfAccount(ChartOfAccountDTO dto)
        {
            return service.SaveChartOfAccount(dto);
        }

        public ChartOfAccountDTO GetChartOfAccount(long chartID)
        {
            return service.GetChartOfAccount(chartID);
        }

        public List<ChartOfAccountDetailDTO> GetAllChartOfAccount()
        {
            return service.GetAllChartOfAccount();
        }

        public List<ChartOfAccountLedgerDetailsDTO> GetLedgerTransactions(int groupID)
        {
            return service.GetLedgerTransactions(groupID);
        }

        #region Monthly Closing
        public List<FeeGeneralMonthlyClosingDTO> GetFeeCancelMonthlyClosing(DateTime? startDate, DateTime? toDate, int? companyID)
        {
            return service.GetFeeCancelMonthlyClosing(startDate, toDate, companyID);
        }
        public List<FeeGeneralMonthlyClosingDTO> GetFeeGeneralMonthlyClosing(DateTime? startDate, DateTime? toDate, int? companyID)
        {
            return service.GetFeeGeneralMonthlyClosing(startDate, toDate, companyID);
        }
        public List<FeeGeneralMonthlyClosingDTO> GetFeeAccountCompareMonthlyClosing(DateTime? startDate, DateTime? toDate, int? companyID)
        {
            return service.GetFeeAccountCompareMonthlyClosing(startDate, toDate, companyID);
        }
        public List<InvoiceGeneralMonthlyClosingDTO> GetInventoryGeneralMonthlyClosing(DateTime? startDate, DateTime? toDate, int? companyID)
        {
            return service.GetInventoryGeneralMonthlyClosing(startDate, toDate, companyID);
        }
        public List<AccountsMonthlyClosingDTO> GetAccountCancelMonthlyClosing(DateTime? startDate, DateTime? toDate, int? companyID)
        {
            return service.GetAccountCancelMonthlyClosing(startDate, toDate, companyID);
        }

        public List<InvoiceGeneralMonthlyClosingDTO> GetInventoryCancelMonthlyClosing(DateTime? startDate, DateTime? toDate, int? companyID)
        {
            return service.GetInventoryCancelMonthlyClosing(startDate, toDate, companyID);
        }

        public List<StockMonthlyClosingDTO> GetStockMonthlyClosing(DateTime? startDate, DateTime? toDate, int? companyID)
        {
            return service.GetStockMonthlyClosing(startDate, toDate, companyID);
        }
        public List<FeeMismatchedMonthlyClosingDTO> GetFeeMismatchedMonthlyClosing(DateTime? startDate, DateTime? toDate, int? companyID)
        {
            return service.GetFeeMismatchedMonthlyClosing(startDate, toDate, companyID);
        }
        public List<AccountMismatchedMonthlyClosingDTO> GetAccountsMismatchedMonthlyClosing(DateTime? startDate, DateTime? toDate, int? companyID)
        {
            return service.GetAccountsMismatchedMonthlyClosing(startDate, toDate, companyID);
        }
        public List<AccountsGeneralMonthlyClosingDTO> GetAccountsGeneralMonthlyClosing(DateTime? startDate, DateTime? toDate, int? companyID)
        {
            return service.GetAccountsGeneralMonthlyClosing(startDate, toDate, companyID);
        }
        public string SubmitMonthlyClosing(DateTime? startDate, DateTime? toDate, int? companyID)
        {
            return service.SubmitMonthlyClosing(startDate, toDate, companyID);
        }

        public DateTime? GetMonthlyClosingDate(long? branchID)
        {
            return service.GetMonthlyClosingDate(branchID);
        }

        #endregion Monthly Closing
        #region Budgeting 
        //public List<BudgetEntryDTO> GetBudgetingData(BudgetEntryDTO budgetingDTO)
        //{
        //    return service.GetBudgetingData(budgetingDTO);
        //}

        public string SaveBudgetEntry(List<BudgetEntryDTO> budgetEntryListDTO)
        {
            return service.SaveBudgetEntry(budgetEntryListDTO);
        }

        public BudgetMasterDTO GetBudgetDetailsByID(int budgetID)
        {
            return service.GetBudgetDetailsByID(budgetID);
        }

        public List<BudgetEntryDTO> FillBudgetEntriesByID(int budgetID)
        {
            return service.FillBudgetEntriesByID(budgetID);
        }

        public List<AccountSuggesionMapDTO> GetBudgetSuggestionValue(int? budgetID, byte? suggestionType, List<long?> accountGroupIDs, DateTime? fromDate, DateTime? toDate, List<long?> accountIDs = null)
        {
            return service.GetBudgetSuggestionValue(budgetID, suggestionType, accountGroupIDs, fromDate, toDate, accountIDs);
        }
        #endregion Budgeting
        #region Bank Reconciliation       
        public BankReconciliationDTO FillBankTransctions(BankReconciliationDTO transactionDTO)
        {
            return service.FillBankTransctions(transactionDTO);
        }
        public BankReconciliationHeadDTO FillBankReconcilationData(long bankReconciliationHeadIID)
        {
            return service.FillBankReconcilationData(bankReconciliationHeadIID);
        }
        public BankOpeningParametersDTO SaveBankOpeningDetails(BankOpeningParametersDTO transactionDTO)
        {
            return service.SaveBankOpeningDetails(transactionDTO);
        }
        public string SaveBankReconciliationEntry(BankReconciliationHeadDTO bankReconciliationHeadDTO)
        {
            return service.SaveBankReconciliationEntry(bankReconciliationHeadDTO);
        }
        public BankReconciliationHeadDTO SaveBankReconciliationMatchedEntry(BankReconciliationHeadDTO bankReconciliationHeadDTO)
        {
            return service.SaveBankReconciliationMatchedEntry(bankReconciliationHeadDTO);
        }
        public List<BankStatementEntryDTO> SaveBankStatementEntry(List<BankStatementEntryDTO> bankStatementEntryDTO)
        {
            return service.SaveBankStatementEntry(bankStatementEntryDTO);
        }
        public BankStatementDTO SaveBankStatement(BankStatementDTO bankStatementDTO)
        {
            return service.SaveBankStatement(bankStatementDTO);
        }
        public List<RuleDTO> GetBankRules(long? bankAccountID)
        {
            return service.GetBankRules(bankAccountID);
        }

        #endregion Bank Reconciliation 
        public List<FiscalYearDTO> GetFiscalYearDetails()
        {
            return service.GetFiscalYearDetails();
        }

        public SmartTreeViewDTO GetSmartTreeView(SmartViewType type, long? parentID, string searchText)
        {
            return service.GetSmartTreeView(type, parentID, searchText);
        }

        public OperationResultDTO SaveFYCEntries(int companyID, int prvFCY, int crntFCY)
        {
            return service.SaveFYCEntries(companyID, prvFCY, crntFCY);
        }

        public bool AccountMergeWithMultipleTransactionIDs(string referenceIDs, DateTime currentDate, int loginId, int type)
        {
            return service.AccountMergeWithMultipleTransactionIDs(referenceIDs, currentDate, loginId, type);
        }

        public AuditDataDTO DownloadDatas(AuditDataDTO dto)
        {
            return service.DownloadDatas(dto);
        }

        public FiscalYearDTO GetFiscalYearByFiscalYear(int fiscalYearID)
        {
            return service.GetFiscalYearByFiscalYear(fiscalYearID);
        }
    }
}
