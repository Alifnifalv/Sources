using Eduegate.Services.Contracts.Accounting.MonthlyClosing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Accounting
{
    [ServiceContract]
    public interface IAccountingService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveAccount")]
        AccountDTO SaveAccount(AccountDTO dto);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetAccount?accountID={accountID}")]
        AccountDTO GetAccount(long accountID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveAccountGroup")]
        AccountGroupDTO SaveAccountGroup(AccountGroupDTO dto);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetAccountGroup?accountGroupID={accountGroupID}")]
        AccountGroupDTO GetAccountGroup(long accountGroupID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveChartOfAccount")]
        ChartOfAccountDTO SaveChartOfAccount(ChartOfAccountDTO dto);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetChartOfAccount?chartID={chartID}")]
        ChartOfAccountDTO GetChartOfAccount(long chartID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
          BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetAllChartOfAccount")]
        List<ChartOfAccountDetailDTO> GetAllChartOfAccount();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
          BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetFeeCancelMonthlyClosing")]
        List<FeeGeneralMonthlyClosingDTO> GetFeeCancelMonthlyClosing(DateTime? startDate, DateTime? toDate);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
          BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetFeeGeneralMonthlyClosing")]
        List<FeeGeneralMonthlyClosingDTO> GetFeeGeneralMonthlyClosing(DateTime? startDate, DateTime? toDate);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetInventoryCancelMonthlyClosing")]
        List<InvoiceGeneralMonthlyClosingDTO> GetInventoryCancelMonthlyClosing(DateTime? startDate, DateTime? toDate);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
       BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetFeeAccountCompareMonthlyClosing")]
        List<FeeGeneralMonthlyClosingDTO> GetFeeAccountCompareMonthlyClosing(DateTime? startDate, DateTime? toDate);

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
       BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetInventoryGeneralMonthlyClosing")]
        List<InvoiceGeneralMonthlyClosingDTO> GetInventoryGeneralMonthlyClosing(DateTime? startDate, DateTime? toDate);

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
      BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetAccountCancelMonthlyClosing")]
        List<AccountsMonthlyClosingDTO> GetAccountCancelMonthlyClosing(DateTime? startDate, DateTime? toDate);

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
     BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetStockMonthlyClosing")]
        List<StockMonthlyClosingDTO> GetStockMonthlyClosing(DateTime? startDate, DateTime? toDate);

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
     BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetFeeMismatchedMonthlyClosing")]
        List<FeeMismatchedMonthlyClosingDTO> GetFeeMismatchedMonthlyClosing(DateTime? startDate, DateTime? toDate);

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
  BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetAccountsMismatchedMonthlyClosing")]
        List<AccountMismatchedMonthlyClosingDTO> GetAccountsMismatchedMonthlyClosing(DateTime? startDate, DateTime? toDate);

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
 BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetAccountsGeneralMonthlyClosing")]
        List<AccountsGeneralMonthlyClosingDTO> GetAccountsGeneralMonthlyClosing(DateTime? startDate, DateTime? toDate);


        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
     BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SubmitMonthlyClosing")]
        string SubmitMonthlyClosing(DateTime? startDate, DateTime? toDate);

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
     BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetBudgetingData")]
        List<BudgetingAccountGroupsDTO> GetBudgetingData(BudgetingDTO budgetingDTO);

    }
}
