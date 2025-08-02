using Eduegate.Services.Contracts.Accounts.MonthlyClosing;
using Eduegate.Services.Contracts.Accounts.Accounting;
using Eduegate.Services.Contracts.Accounts.MonthlyClosing;
using Eduegate.Services.Contracts.Budgeting;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.SmartView;
using System;
using System.Collections.Generic;
using Eduegate.Services.Contracts.AI;
using Eduegate.Services.Contracts.School.Accounts;

namespace Eduegate.Services.Contracts.Accounting
{
    public interface IAccountingService
    {
        AccountDTO SaveAccount(AccountDTO dto);

        AccountDTO GetAccount(long accountID);

        string SaveAccountGroup(AccountsGroupDTO dto);

        AccountGroupDTO GetAccountGroup(long accountGroupID);

        ChartOfAccountDTO SaveChartOfAccount(ChartOfAccountDTO dto);

        ChartOfAccountDTO GetChartOfAccount(long chartID);

        List<ChartOfAccountDetailDTO> GetAllChartOfAccount();
        List<ChartOfAccountLedgerDetailsDTO> GetLedgerTransactions(int groupID);
        List<FeeGeneralMonthlyClosingDTO> GetFeeCancelMonthlyClosing(DateTime? startDate, DateTime? toDate, int? companyID);

        List<FeeGeneralMonthlyClosingDTO> GetFeeGeneralMonthlyClosing(DateTime? startDate, DateTime? toDate, int? companyID);

        List<InvoiceGeneralMonthlyClosingDTO> GetInventoryCancelMonthlyClosing(DateTime? startDate, DateTime? toDate, int? companyID);

        List<FeeGeneralMonthlyClosingDTO> GetFeeAccountCompareMonthlyClosing(DateTime? startDate, DateTime? toDate, int? companyID);

        List<InvoiceGeneralMonthlyClosingDTO> GetInventoryGeneralMonthlyClosing(DateTime? startDate, DateTime? toDate, int? companyID);

        List<AccountsMonthlyClosingDTO> GetAccountCancelMonthlyClosing(DateTime? startDate, DateTime? toDate, int? companyID);

        List<StockMonthlyClosingDTO> GetStockMonthlyClosing(DateTime? startDate, DateTime? toDate, int? companyID);

        List<FeeMismatchedMonthlyClosingDTO> GetFeeMismatchedMonthlyClosing(DateTime? startDate, DateTime? toDate, int? companyID);

        List<AccountMismatchedMonthlyClosingDTO> GetAccountsMismatchedMonthlyClosing(DateTime? startDate, DateTime? toDate, int? companyID);

        List<AccountsGeneralMonthlyClosingDTO> GetAccountsGeneralMonthlyClosing(DateTime? startDate, DateTime? toDate, int? companyID);

        string SubmitMonthlyClosing(DateTime? startDate, DateTime? toDate, int? companyID);

        public DateTime? GetMonthlyClosingDate(long? branchID);


        // List<BudgetEntryDTO> GetBudgetingData(BudgetEntryDTO budgetingDTO);

        string SaveBudgetEntry(List<BudgetEntryDTO> budgetEntryListDTO);

        public BudgetMasterDTO GetBudgetDetailsByID(int budgetID);

        public List<BudgetEntryDTO> FillBudgetEntriesByID(int budgetID);

        public List<AccountSuggesionMapDTO> GetBudgetSuggestionValue(int? budgetID, byte? suggestionType, List<long?> accountGroupIDs, DateTime? fromDate, DateTime? toDate, List<long?> accountIDs = null);
        
        public List<FiscalYearDTO> GetFiscalYearDetails();

        SmartTreeViewDTO GetSmartTreeView(SmartViewType type, long? parentID, string searchText);

        public OperationResultDTO SaveFYCEntries(int companyID, int prvFCY, int crntFCY);

        #region Bank Reconciliation 

        public BankReconciliationDTO FillBankTransctions(BankReconciliationDTO transactionDTO);
        public BankReconciliationHeadDTO FillBankReconcilationData(long bankReconciliationHeadIID);
        public BankOpeningParametersDTO SaveBankOpeningDetails(BankOpeningParametersDTO transactionDTO);
        public string SaveBankReconciliationEntry(BankReconciliationHeadDTO bankReconciliationHeadDTO);
        public BankReconciliationHeadDTO SaveBankReconciliationMatchedEntry(BankReconciliationHeadDTO bankReconciliationHeadDTO);
        public BankStatementDTO SaveBankStatement(BankStatementDTO bankStatementDTO);
        public List<RuleDTO> GetBankRules(long? bankAccountID);
        public List<BankStatementEntryDTO> SaveBankStatementEntry(List<BankStatementEntryDTO> bankStatementEntryDTO);

        #endregion Bank Reconciliation 

        public bool AccountMergeWithMultipleTransactionIDs(string referenceIDs, DateTime currentDate, int loginId, int type);

        public AuditDataDTO DownloadDatas(AuditDataDTO dto);

        public FiscalYearDTO GetFiscalYearByFiscalYear(int fiscalYearID);
    }
}