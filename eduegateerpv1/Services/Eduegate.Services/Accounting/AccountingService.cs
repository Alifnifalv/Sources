using Eduegate.Domain.Accounts;
using Eduegate.Domain.Mappers.Accounts;
using Eduegate.Domain.Mappers.Budgeting;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Services.Contracts.Accounting.MonthlyClosing;
using Eduegate.Services.Contracts.Budgeting;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.School.Fees;
using Eduegate.Services.Contracts.SmartView;
using System;
using System.Collections.Generic;

namespace Eduegate.Services.Accounting
{
    public class AccountingService : BaseService, IAccountingService
    {
        public AccountDTO SaveAccount(AccountDTO dto)
        {
            return new AccountingBL(CallContext).SaveAccount(dto);
        }

        public AccountDTO GetAccount(long accountID)
        {
            return new AccountingBL(CallContext).GetAccount(accountID);
        }

        public AccountGroupDTO SaveAccountGroup(AccountGroupDTO dto)
        {
            return new AccountingBL(CallContext).SaveAccountGroup(dto);
        }

        public AccountGroupDTO GetAccountGroup(long accountGroupID)
        {
            return new AccountingBL(CallContext).GetAccountGroup(accountGroupID);
        }

        public ChartOfAccountDTO SaveChartOfAccount(ChartOfAccountDTO dto)
        {
            return new AccountingBL(CallContext).SaveChartOfAccount(dto);
        }

        public ChartOfAccountDTO GetChartOfAccount(long chartID)
        {
            return new AccountingBL(CallContext).GetChartOfAccount(chartID);
        }

        public List<ChartOfAccountDetailDTO> GetAllChartOfAccount()
        {
            return new AccountingBL(CallContext).GetAllChartOfAccount();
        }

        #region Monthly Closing
        public List<FeeGeneralMonthlyClosingDTO> GetFeeCancelMonthlyClosing(DateTime? startDate, DateTime? toDate)
        {
            return MonthlyClosingMapper.Mapper(CallContext).GetFeeCancelMonthlyClosing(startDate, toDate);
        }
        public List<FeeGeneralMonthlyClosingDTO> GetFeeGeneralMonthlyClosing(DateTime? startDate, DateTime? toDate)
        {
            return MonthlyClosingMapper.Mapper(CallContext).GetFeeGeneralMonthlyClosing(startDate, toDate);
        }
        public List<InvoiceGeneralMonthlyClosingDTO> GetInventoryCancelMonthlyClosing(DateTime? startDate, DateTime? toDate)
        {
            return MonthlyClosingMapper.Mapper(CallContext).GetInventoryCancelMonthlyClosing(startDate, toDate);
        }

        public List<FeeGeneralMonthlyClosingDTO> GetFeeAccountCompareMonthlyClosing(DateTime? startDate, DateTime? toDate)
        {
            return MonthlyClosingMapper.Mapper(CallContext).GetFeeAccountCompareMonthlyClosing(startDate, toDate);
        }
        public List<InvoiceGeneralMonthlyClosingDTO> GetInventoryGeneralMonthlyClosing(DateTime? startDate, DateTime? toDate)
        {
            return MonthlyClosingMapper.Mapper(CallContext).GetInventoryGeneralMonthlyClosing(startDate, toDate);
        }
        public List<AccountsMonthlyClosingDTO> GetAccountCancelMonthlyClosing(DateTime? startDate, DateTime? toDate)
        {
            return MonthlyClosingMapper.Mapper(CallContext).GetAccountCancelMonthlyClosing(startDate, toDate);
        }
        public List<StockMonthlyClosingDTO> GetStockMonthlyClosing(DateTime? startDate, DateTime? toDate)
        {
            return MonthlyClosingMapper.Mapper(CallContext).GetStockMonthlyClosing(startDate, toDate);
        }
        public List<FeeMismatchedMonthlyClosingDTO> GetFeeMismatchedMonthlyClosing(DateTime? startDate, DateTime? toDate)
        {
            return MonthlyClosingMapper.Mapper(CallContext).GetFeeMismatchedMonthlyClosing(startDate, toDate);
        }
        public List<AccountMismatchedMonthlyClosingDTO> GetAccountsMismatchedMonthlyClosing(DateTime? startDate, DateTime? toDate)
        {
            return MonthlyClosingMapper.Mapper(CallContext).GetAccountsMismatchedMonthlyClosing(startDate, toDate);
        }
        public List<AccountsGeneralMonthlyClosingDTO> GetAccountsGeneralMonthlyClosing(DateTime? startDate, DateTime? toDate)
        {
            return MonthlyClosingMapper.Mapper(CallContext).GetAccountsGeneralMonthlyClosing(startDate, toDate);
        }

        public string SubmitMonthlyClosing(DateTime? startDate, DateTime? toDate)
        {
            return MonthlyClosingMapper.Mapper(CallContext).SubmitMonthlyClosing(startDate, toDate);
        }
        #endregion Monthly Closing

        #region Budgeting 

        public string SaveBudgetEntry(List<BudgetEntryDTO> budgetEntryListDTO)
        {
            return BudgetingMapper.Mapper(CallContext).SaveBudgetEntry(budgetEntryListDTO);
        }

        public BudgetMasterDTO GetBudgetDetailsByID(int budgetID)
        {
            return BudgetMasterMapper.Mapper(CallContext).GetBudgetDetailsByID(budgetID);
        }

        public List<BudgetEntryDTO> FillBudgetEntriesByID(int budgetID)
        {
            return BudgetingMapper.Mapper(CallContext).FillBudgetEntriesByID(budgetID);
        }
        public List<AccountSuggesionMapDTO> GetBudgetSuggestionValue(int? budgetID, byte? suggestionType, List<long?> accountGroupIDs, DateTime? fromDate, DateTime? toDate, List<long?> accountIDs = null)
        {
            return BudgetingMapper.Mapper(CallContext).GetBudgetSuggestionValue(budgetID, suggestionType, accountGroupIDs, fromDate, toDate, accountIDs);
        }

        #endregion Budgeting

        #region Bank Reconciliation 

        public BankReconciliationDTO FillBankTransctions(BankReconciliationDTO transactionDTO)
        {
            return BankReconciliationMapper.Mapper(CallContext).FillBankTransctions(transactionDTO);
        }
        public BankReconciliationHeadDTO FillBankReconcilationData(long bankReconciliationHeadIID)
        {
            return BankReconciliationMapper.Mapper(CallContext).ToDTO(bankReconciliationHeadIID);
        }
        public BankOpeningParametersDTO SaveBankOpeningDetails(BankOpeningParametersDTO transactionDTO)
        {
            return BankReconciliationMapper.Mapper(CallContext).SaveBankOpeningDetails(transactionDTO);
        }
        public string SaveBankReconciliationEntry(BankReconciliationHeadDTO bankReconciliationHeadDTO)
        {
            return BankReconciliationMapper.Mapper(CallContext).SaveBankReconciliationEntry(bankReconciliationHeadDTO);
        }
        public string SaveBankStatement(BankStatementDTO bankStatementDTO)
        {
            return BankReconciliationMapper.Mapper(CallContext).SaveBankStatement(bankStatementDTO);
        }

        #endregion Bank Reconciliation 

        public List<FiscalYearDTO> GetFiscalYearDetails()
        {
            return FinancialYearClosingMapper.Mapper(CallContext).GetFiscalYearDetails();
        }
        public SmartTreeViewDTO GetSmartTreeView(SmartViewType type, long? parentID, string searchText)
        {
            return FinancialYearClosingMapper.Mapper(CallContext).GetProductTree(type, parentID, searchText);
        }

        public OperationResultDTO SaveFYCEntries(int companyID, int prvFCY, int crntFCY)
        {
            return FinancialYearClosingMapper.Mapper(CallContext).SaveFYCEntries(companyID, prvFCY, crntFCY);
        }
    }
}