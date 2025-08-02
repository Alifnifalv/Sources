using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain;
using Eduegate.Domain.Mappers.Accounts;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Services.Contracts.Accounts.MonthlyClosing;
using Eduegate.Services.Contracts.Accounts.Accounting;
using Eduegate.Services.Contracts.Budgeting;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.School.Fees;
using Eduegate.Services.Contracts.SmartView;
using Eduegate.Services.Contracts.Supports;
using Eduegate.Services.Contracts.AI;
using Eduegate.Domain.Entity.Models;
using Eduegate.Services.Contracts.School.Accounts;

namespace Eduegate.Service.Client.Accounts
{
    public class AccountingServiceClient : BaseClient, IAccountingService
    {
        private static string serviceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        private static string accountService = string.Concat(serviceHost, Eduegate.Framework.Helper.Constants.ACCOUNTING_SERVICE);

        public AccountingServiceClient(CallContext context = null, Action<string> logger = null)
            : base(context, logger)
        {
        }

        public AccountDTO SaveAccount(AccountDTO dto)
        {
            var uri = accountService + "SaveAccount";
            return ServiceHelper.HttpPostGetRequest<AccountDTO>(uri, dto, _callContext);
        }

        public AccountDTO GetAccount(long accountID)
        {
            var uri = accountService + "GetAccount?accountID=" + accountID;
            return ServiceHelper.HttpGetRequest<AccountDTO>(uri, _callContext);
        }

        public string SaveAccountGroup(AccountsGroupDTO dto)
        {
            var uri = accountService + "SaveAccountGroup";
            return ServiceHelper.HttpGetRequest<string>(uri, _callContext);
        }

        public AccountGroupDTO GetAccountGroup(long accountGroupID)
        {
            var uri = accountService + "GetAccountGroup?accountGroupID=" + accountGroupID;
            return ServiceHelper.HttpGetRequest<AccountGroupDTO>(uri, _callContext);
        }

        public ChartOfAccountDTO SaveChartOfAccount(ChartOfAccountDTO dto)
        {
            var uri = accountService + "SaveChartOfAccount";
            return ServiceHelper.HttpPostGetRequest<ChartOfAccountDTO>(uri, dto, _callContext);
        }

        public ChartOfAccountDTO GetChartOfAccount(long chartID)
        {
            var uri = accountService + "GetChartOfAccount?chartID=" + chartID;
            return ServiceHelper.HttpGetRequest<ChartOfAccountDTO>(uri, _callContext);
        }

        public List<ChartOfAccountDetailDTO> GetAllChartOfAccount()
        {
            var uri = accountService + "GetAllChartOfAccount";
            return ServiceHelper.HttpGetRequest<List<ChartOfAccountDetailDTO>>(uri, _callContext);
        }
        public List<ChartOfAccountLedgerDetailsDTO> GetLedgerTransactions(int groupID)
        {
            var uri = accountService + "GetLedgerTransactions";
            return ServiceHelper.HttpGetRequest<List<ChartOfAccountLedgerDetailsDTO>>(uri, _callContext);
        }

        #region Monthly Closing

        public List<FeeGeneralMonthlyClosingDTO> GetFeeGeneralMonthlyClosing(DateTime? startDate, DateTime? toDate, int? companyID)
        {
            var uri = accountService + "GetFeeGeneralMonthlyClosing";
            return ServiceHelper.HttpGetRequest<List<FeeGeneralMonthlyClosingDTO>>(uri, _callContext);

        }
        public List<FeeGeneralMonthlyClosingDTO> GetFeeAccountCompareMonthlyClosing(DateTime? startDate, DateTime? toDate, int? companyID)
        {
            var uri = accountService + "GetFeeAccountCompareMonthlyClosing";
            return ServiceHelper.HttpGetRequest<List<FeeGeneralMonthlyClosingDTO>>(uri, _callContext);
        }
        public List<AccountsMonthlyClosingDTO> GetAccountCancelMonthlyClosing(DateTime? startDate, DateTime? toDate, int? companyID)
        {
            var uri = accountService + "GetAccountCancelMonthlyClosing";
            return ServiceHelper.HttpGetRequest<List<AccountsMonthlyClosingDTO>>(uri, _callContext);
        }

        public List<InvoiceGeneralMonthlyClosingDTO> GetInventoryGeneralMonthlyClosing(DateTime? startDate, DateTime? toDate, int? companyID)
        {
            var uri = accountService + "GetFeeAccountCompareMonthlyClosing";
            return ServiceHelper.HttpGetRequest<List<InvoiceGeneralMonthlyClosingDTO>>(uri, _callContext);
        }
        public List<InvoiceGeneralMonthlyClosingDTO> GetInventoryCancelMonthlyClosing(DateTime? startDate, DateTime? toDate, int? companyID)
        {

            var uri = accountService + "GetInventoryCancelMonthlyClosing";
            return ServiceHelper.HttpGetRequest<List<InvoiceGeneralMonthlyClosingDTO>>(uri, _callContext);

        }
        public List<FeeGeneralMonthlyClosingDTO> GetFeeCancelMonthlyClosing(DateTime? startDate, DateTime? toDate, int? companyID)
        {
            var uri = accountService + "GetFeeCancelMonthlyClosing";
            return ServiceHelper.HttpGetRequest<List<FeeGeneralMonthlyClosingDTO>>(uri, _callContext);
        }
        public List<StockMonthlyClosingDTO> GetStockMonthlyClosing(DateTime? startDate, DateTime? toDate, int? companyID)
        {
            var uri = accountService + "GetStockMonthlyClosing";
            return ServiceHelper.HttpGetRequest<List<StockMonthlyClosingDTO>>(uri, _callContext);
        }
        public List<FeeMismatchedMonthlyClosingDTO> GetFeeMismatchedMonthlyClosing(DateTime? startDate, DateTime? toDate, int? companyID)
        {
            var uri = accountService + "GetFeeMismatchedMonthlyClosing";
            return ServiceHelper.HttpGetRequest<List<FeeMismatchedMonthlyClosingDTO>>(uri, _callContext);
        }

        public List<AccountMismatchedMonthlyClosingDTO> GetAccountsMismatchedMonthlyClosing(DateTime? startDate, DateTime? toDate, int? companyID)
        {
            var uri = accountService + "GetAccountsMismatchedMonthlyClosing";
            return ServiceHelper.HttpGetRequest<List<AccountMismatchedMonthlyClosingDTO>>(uri, _callContext);
        }
        public List<AccountsGeneralMonthlyClosingDTO> GetAccountsGeneralMonthlyClosing(DateTime? startDate, DateTime? toDate, int? companyID)
        {
            var uri = accountService + "GetAccountsGeneralMonthlyClosing";
            return ServiceHelper.HttpGetRequest<List<AccountsGeneralMonthlyClosingDTO>>(uri, _callContext);
        }

        public string SubmitMonthlyClosing(DateTime? startDate, DateTime? toDate, int? companyID)
        {
            var uri = accountService + "SubmitMonthlyClosing";
            return ServiceHelper.HttpGetRequest<string>(uri, _callContext);
        }
        public DateTime? GetMonthlyClosingDate(long? branchID)
        {
            var uri = accountService + "GetMonthlyClosingDate";
            return ServiceHelper.HttpGetRequest<DateTime?>(uri, _callContext);
        }
        #endregion Monthly Closing

        #region Budgeting 
        //public List<BudgetEntryDTO> GetBudgetingData (BudgetEntryDTO budgetingDTO)
        //{
        //    var uri = accountService + "GetBudgetingData";
        //    return ServiceHelper.HttpGetRequest<List<BudgetEntryDTO>>(uri, _callContext);
        //}

        public string SaveBudgetEntry(List<BudgetEntryDTO> budgetEntryListDTO)
        {
            var uri = accountService + "SaveBudgetEntry";
            return ServiceHelper.HttpGetRequest<string>(uri, _callContext);
        }

        public BudgetMasterDTO GetBudgetDetailsByID(int budgetID)
        {
            throw new NotImplementedException();
        }

        public List<BudgetEntryDTO> FillBudgetEntriesByID(int budgetID)
        {
            throw new NotImplementedException();
        }
        public List<AccountSuggesionMapDTO> GetBudgetSuggestionValue(int? budgetID, byte? suggestionType, List<long?> accountGroupIDs, DateTime? fromDate, DateTime? toDate, List<long?> accountIDs = null)
        {
            throw new NotImplementedException();
        }
        #endregion Budgeting

        #region Bank Reconciliation 

        public BankReconciliationDTO FillBankTransctions(BankReconciliationDTO transactionDTO)
        {
            throw new NotImplementedException();
        }
        public BankReconciliationHeadDTO FillBankReconcilationData(long bankReconciliationHeadIID)
        {
            throw new NotImplementedException();
        }
        public BankOpeningParametersDTO SaveBankOpeningDetails(BankOpeningParametersDTO transactionDTO)
        {
            throw new NotImplementedException();
        }
        public string SaveBankReconciliationEntry(BankReconciliationHeadDTO bankReconciliationHeadDTO)
        {
            var uri = accountService + "SaveBankReconciliationEntry";
            return ServiceHelper.HttpGetRequest<string>(uri, _callContext);
        }
        public BankReconciliationHeadDTO SaveBankReconciliationMatchedEntry(BankReconciliationHeadDTO bankReconciliationHeadDTO)
        {
            var uri = accountService + "SaveBankReconciliationMatchedEntry";
            return ServiceHelper.HttpGetRequest<BankReconciliationHeadDTO>(uri, _callContext);
        }
        public BankStatementDTO SaveBankStatement(BankStatementDTO bankStatementDTO)
        {
            var uri = accountService + "SaveBankStatement";
            return ServiceHelper.HttpGetRequest<BankStatementDTO>(uri, _callContext);
        }

        public List<BankStatementEntryDTO> SaveBankStatementEntry(List<BankStatementEntryDTO> bankStatementEntryDTO)
        {
            var uri = accountService + "SaveBankStatementEntry";
            return ServiceHelper.HttpGetRequest<List<BankStatementEntryDTO>>(uri, _callContext);
        }
        public List<RuleDTO> GetBankRules(long? bankAccountID)
        {
            var uri = accountService + "GetBankRules";
            return ServiceHelper.HttpGetRequest<List<RuleDTO>>(uri, _callContext);
        }
        #endregion Bank Reconciliation 
        public List<FiscalYearDTO> GetFiscalYearDetails()
        {
            var uri = accountService + "GetFiscalYearDetails";
            return ServiceHelper.HttpGetRequest<List<FiscalYearDTO>>(uri, _callContext);
        }

        public SmartTreeViewDTO GetSmartTreeView(SmartViewType type, long? parentID, string searchText)
        {
            string uri = accountService + "GetSmartTreeView?type="
                + type + "&parentID=" + parentID + "&searchText=" + searchText;
            return ServiceHelper.HttpGetRequest<SmartTreeViewDTO>(uri, _callContext, _logger);
        }

        public OperationResultDTO SaveFYCEntries(int companyID, int prvFCY, int crntFCY)
        {
            string uri = accountService + "SaveFYCEntries?companyID="
                + companyID + "&prvFCY=" + prvFCY + "&crntFCY=" + crntFCY;
            return ServiceHelper.HttpGetRequest<OperationResultDTO>(uri, _callContext, _logger);
        }

        public bool AccountMergeWithMultipleTransactionIDs(string referenceIDs, DateTime currentDate, int loginId, int type)
        {
            throw new NotImplementedException();
        }

        public AuditDataDTO DownloadDatas(AuditDataDTO dto)
        {
            throw new NotImplementedException();
        }

        public FiscalYearDTO GetFiscalYearByFiscalYear(int fiscalYearID)
        {
            throw new NotImplementedException();
        }

    }
}
