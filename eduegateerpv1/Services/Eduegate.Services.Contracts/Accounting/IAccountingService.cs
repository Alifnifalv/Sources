using Eduegate.Services.Contracts.Accounting.MonthlyClosing;
using Eduegate.Services.Contracts.Budgeting;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.SmartView;
using System;
using System.Collections.Generic;

namespace Eduegate.Services.Contracts.Accounting
{
    public interface IAccountingService
    {
        AccountDTO SaveAccount(AccountDTO dto);

        AccountDTO GetAccount(long accountID);

        AccountGroupDTO SaveAccountGroup(AccountGroupDTO dto);

        AccountGroupDTO GetAccountGroup(long accountGroupID);

        ChartOfAccountDTO SaveChartOfAccount(ChartOfAccountDTO dto);

        ChartOfAccountDTO GetChartOfAccount(long chartID);

        List<ChartOfAccountDetailDTO> GetAllChartOfAccount();

        List<FeeGeneralMonthlyClosingDTO> GetFeeCancelMonthlyClosing(DateTime? startDate, DateTime? toDate);

        List<FeeGeneralMonthlyClosingDTO> GetFeeGeneralMonthlyClosing(DateTime? startDate, DateTime? toDate);

        List<InvoiceGeneralMonthlyClosingDTO> GetInventoryCancelMonthlyClosing(DateTime? startDate, DateTime? toDate);

        List<FeeGeneralMonthlyClosingDTO> GetFeeAccountCompareMonthlyClosing(DateTime? startDate, DateTime? toDate);

        List<InvoiceGeneralMonthlyClosingDTO> GetInventoryGeneralMonthlyClosing(DateTime? startDate, DateTime? toDate);

        List<AccountsMonthlyClosingDTO> GetAccountCancelMonthlyClosing(DateTime? startDate, DateTime? toDate);

        List<StockMonthlyClosingDTO> GetStockMonthlyClosing(DateTime? startDate, DateTime? toDate);

        List<FeeMismatchedMonthlyClosingDTO> GetFeeMismatchedMonthlyClosing(DateTime? startDate, DateTime? toDate);

        List<AccountMismatchedMonthlyClosingDTO> GetAccountsMismatchedMonthlyClosing(DateTime? startDate, DateTime? toDate);

        List<AccountsGeneralMonthlyClosingDTO> GetAccountsGeneralMonthlyClosing(DateTime? startDate, DateTime? toDate);

        string SubmitMonthlyClosing(DateTime? startDate, DateTime? toDate);

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
        public string SaveBankStatement(BankStatementDTO bankStatementDTO);
        #endregion Bank Reconciliation 
    }
}