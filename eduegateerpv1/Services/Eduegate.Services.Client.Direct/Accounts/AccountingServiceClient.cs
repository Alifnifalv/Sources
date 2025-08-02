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
using Eduegate.Services.Contracts.Accounting.MonthlyClosing;
using Eduegate.Services.Contracts.Budgeting;
using Eduegate.Services.Contracts.School.Fees;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.SmartView;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.Catalog;

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

        public AccountGroupDTO SaveAccountGroup(AccountGroupDTO dto)
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
        #region Monthly Closing
        public List<FeeGeneralMonthlyClosingDTO> GetFeeCancelMonthlyClosing(DateTime? startDate, DateTime? toDate)
        {
            return service.GetFeeCancelMonthlyClosing(startDate, toDate);
        }
        public List<FeeGeneralMonthlyClosingDTO> GetFeeGeneralMonthlyClosing(DateTime? startDate, DateTime? toDate)
        {
            return service.GetFeeGeneralMonthlyClosing(startDate, toDate);
        }
        public List<FeeGeneralMonthlyClosingDTO> GetFeeAccountCompareMonthlyClosing(DateTime? startDate, DateTime? toDate)
        {
            return service.GetFeeAccountCompareMonthlyClosing(startDate, toDate);
        }
        public List<InvoiceGeneralMonthlyClosingDTO> GetInventoryGeneralMonthlyClosing(DateTime? startDate, DateTime? toDate)
        {
            return service.GetInventoryGeneralMonthlyClosing(startDate, toDate);
        }
        public List<AccountsMonthlyClosingDTO> GetAccountCancelMonthlyClosing(DateTime? startDate, DateTime? toDate)
        {
            return service.GetAccountCancelMonthlyClosing(startDate, toDate);
        }

        public List<InvoiceGeneralMonthlyClosingDTO> GetInventoryCancelMonthlyClosing(DateTime? startDate, DateTime? toDate)
        {
            return service.GetInventoryCancelMonthlyClosing(startDate, toDate);
        }

        public List<StockMonthlyClosingDTO> GetStockMonthlyClosing(DateTime? startDate, DateTime? toDate)
        {
            return service.GetStockMonthlyClosing(startDate, toDate);
        }
        public List<FeeMismatchedMonthlyClosingDTO> GetFeeMismatchedMonthlyClosing(DateTime? startDate, DateTime? toDate)
        {
            return service.GetFeeMismatchedMonthlyClosing(startDate, toDate);
        }
        public List<AccountMismatchedMonthlyClosingDTO> GetAccountsMismatchedMonthlyClosing(DateTime? startDate, DateTime? toDate)
        {
            return service.GetAccountsMismatchedMonthlyClosing(startDate, toDate);
        }
        public List<AccountsGeneralMonthlyClosingDTO> GetAccountsGeneralMonthlyClosing(DateTime? startDate, DateTime? toDate)
        {
            return service.GetAccountsGeneralMonthlyClosing(startDate, toDate);
        }
        public string SubmitMonthlyClosing(DateTime? startDate, DateTime? toDate)
        {
            return service.SubmitMonthlyClosing(startDate, toDate);
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

        public List<AccountSuggesionMapDTO> GetBudgetSuggestionValue(int? budgetID, byte? suggestionType, List<long?> accountGroupIDs,  DateTime? fromDate, DateTime? toDate, List<long?> accountIDs = null)
        {
            return service.GetBudgetSuggestionValue(budgetID, suggestionType,accountGroupIDs, fromDate, toDate, accountIDs);
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

        public string SaveBankStatement(BankStatementDTO bankStatementDTO)
        {
            return service.SaveBankStatement(bankStatementDTO);
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
        
    }
}
