using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Eduegate.Services.Client.Factory;
using Eduegate.Web.Library.ViewModels.AccountTransactions.MonthlyClosing;
using Eduegate.Application.Mvc;
using Eduegate.Services.Contracts.Accounts.MonthlyClosing;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Web.Library.ViewModels.Accounts;

namespace Eduegate.ERP.Admin.Areas.Accounts.Controllers
{
    [Area("Accounts")]
    public class MonthlyClosingController : BaseController
    {
        // GET: Schools/Mark
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MonthlyClosing()
        {
            return View();
        }


        #region 1. Fee General Tab

        [HttpGet]
        public ActionResult GetFeeGeneralMonthlyClosing(string dateFromString, string dateToString, int? companyID)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            var dateFrom = DateTime.ParseExact(dateFromString, dateFormat, CultureInfo.InvariantCulture);

            var dateTo = DateTime.ParseExact(dateToString, dateFormat, CultureInfo.InvariantCulture);
            var viewModelList = new List<MCGridFeeDueFeeTypeViewModel>();

            var entry = ClientFactory.AccountingServiceClient(CallContext).GetFeeGeneralMonthlyClosing(dateFrom, dateTo, companyID);

            viewModelList = GetFeeGeneralFeeTypeDetails(entry);
            var jsonResult = Json(viewModelList);

            //TODO: Required Check
            //jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;

        }
        private List<MCGridFeeDueFeeTypeViewModel> GetFeeGeneralFeeTypeDetails(List<FeeGeneralMonthlyClosingDTO> listData)
        {
            var decimalvalue = new Domain.Setting.SettingBL().GetSettingValue<int?>("DEFAULT_DECIMALPOINTS");
            int roundinvalue = decimalvalue.HasValue ? decimalvalue.Value : 2;

            var feeDuedetails = listData
 .GroupBy(a => new { a.FeeTypeID, a.FeeTypeName })
 .Select(group => new MCGridFeeDueFeeTypeViewModel
 {
     IsExpand = false,
     FeeTypeID = group.Key.FeeTypeID,
     FeeTypeName = group.Key.FeeTypeName,
     ClosingAmount = FormatDecimal(group.Sum(a => a.ClosingAmount) ?? 0, roundinvalue),
     ClosingCredit = FormatDecimal(group.Sum(a => a.ClosingCredit) ?? 0, roundinvalue),
     ClosingDebit = FormatDecimal(group.Sum(a => a.ClosingDebit) ?? 0, roundinvalue),
     OpeningAmount = FormatDecimal(group.Sum(a => a.OpeningAmount) ?? 0, roundinvalue),
     OpeningCredit = FormatDecimal(group.Sum(a => a.OpeningCredit) ?? 0, roundinvalue),
     OpeningDebit = FormatDecimal(group.Sum(a => a.OpeningDebit) ?? 0, roundinvalue),
     TransactionAmount = FormatDecimal(group.Sum(a => a.TransactionAmount) ?? 0, roundinvalue),
     TransactionCredit = FormatDecimal(group.Sum(a => a.TransactionCredit) ?? 0, roundinvalue),
     TransactionDebit = FormatDecimal(group.Sum(a => a.TransactionDebit) ?? 0, roundinvalue),
     DifferenceAmount = FormatDecimal((group.Sum(a => a.ClosingAmount) - group.Sum(a => a.OpeningAmount)) ?? 0, roundinvalue),
     DifferencePlusKPI = group.Sum(a => a.ClosingAmount) != 0 ?
                       FormatDecimal(((group.Sum(a => a.ClosingAmount) - group.Sum(a => a.OpeningAmount)) / group.Sum(a => a.ClosingAmount)) * 100 ?? 0, roundinvalue) : null,
     MCGridFeeDueFeeCycle = GetFeeGeneralFeeCycles(listData.Where(x => x.FeeTypeID == group.Key.FeeTypeID).ToList())
 })
 .ToList();

            return feeDuedetails;
        }


        private List<MCGridFeeDueFeeCycleViewModel> GetFeeGeneralFeeCycles(List<FeeGeneralMonthlyClosingDTO> listData)
        {
            var decimalvalue = new Domain.Setting.SettingBL().GetSettingValue<int?>("DEFAULT_DECIMALPOINTS");
            int roundinvalue = decimalvalue.HasValue ? decimalvalue.Value : 2;
            var feeDueFeeCycleDetail = listData
          .GroupBy(a => new { a.FeeCycleID, a.FeeCycleName })
          .Select(group => new MCGridFeeDueFeeCycleViewModel
          {
              IsExpand = false,
              FeeCycleID = group.Key.FeeCycleID,
              FeeCycleName = group.Key.FeeCycleName,
              ClosingAmount = group.Sum(a => a.ClosingAmount) ?? 0,
              ClosingCredit = group.Sum(a => a.ClosingCredit) ?? 0,
              ClosingDebit = group.Sum(a => a.ClosingDebit) ?? 0,
              OpeningAmount = group.Sum(a => a.OpeningAmount) ?? 0,
              OpeningCredit = group.Sum(a => a.OpeningCredit) ?? 0,
              OpeningDebit = group.Sum(a => a.OpeningDebit) ?? 0,
              TransactionAmount = group.Sum(a => a.TransactionAmount) ?? 0,
              TransactionCredit = group.Sum(a => a.TransactionCredit) ?? 0,
              TransactionDebit = group.Sum(a => a.TransactionDebit) ?? 0,
              DifferenceAmount = (group.Sum(a => a.ClosingAmount) - group.Sum(a => a.OpeningAmount)) ?? 0,
              DifferencePlusKPI = group.Sum(a => a.ClosingAmount) != 0 ?
                               ((group.Sum(a => a.ClosingAmount) - group.Sum(a => a.OpeningAmount)) / group.Sum(a => a.ClosingAmount)) * 100 ?? 0 : 0,
              MCGridFeeDueFeeMaster = GetFeeGeneralFeeMasters(listData.Where(x => x.FeeCycleID == group.Key.FeeCycleID).ToList())
          })
          .ToList();

            return feeDueFeeCycleDetail;
        }

        private List<MCGridFeeDueFeeMasterViewModel> GetFeeGeneralFeeMasters(List<FeeGeneralMonthlyClosingDTO> listData)
        {
            var decimalvalue = new Domain.Setting.SettingBL().GetSettingValue<int?>("DEFAULT_DECIMALPOINTS");
            int roundinvalue = decimalvalue.HasValue ? decimalvalue.Value : 2;
            var groupedSums = listData
       .GroupBy(a => new { a.FeeMasterID, a.FeeMasterName })
       .Select(group => new MCGridFeeDueFeeMasterViewModel
       {
           FeeMasterID = group.Key.FeeMasterID,
           FeeMasterName = group.Key.FeeMasterName,
           ClosingAmount = FormatDecimal(group.Sum(a => a.ClosingAmount) ?? 0, roundinvalue),
           ClosingCredit = FormatDecimal(group.Sum(a => a.ClosingCredit) ?? 0, roundinvalue),
           ClosingDebit = FormatDecimal(group.Sum(a => a.ClosingDebit) ?? 0, roundinvalue),
           OpeningAmount = FormatDecimal(group.Sum(a => a.OpeningAmount) ?? 0, roundinvalue),
           OpeningCredit = FormatDecimal(group.Sum(a => a.OpeningCredit) ?? 0, roundinvalue),
           OpeningDebit = FormatDecimal(group.Sum(a => a.OpeningDebit) ?? 0, roundinvalue),
           TransactionAmount = FormatDecimal(group.Sum(a => a.TransactionAmount) ?? 0, roundinvalue),
           TransactionCredit = FormatDecimal(group.Sum(a => a.TransactionCredit) ?? 0, roundinvalue),
           TransactionDebit = FormatDecimal(group.Sum(a => a.TransactionDebit) ?? 0, roundinvalue),
           DifferenceAmount = FormatDecimal((group.Sum(a => a.ClosingAmount) - group.Sum(a => a.OpeningAmount)) ?? 0, roundinvalue),
           DifferencePlusKPI = group.Sum(a => a.ClosingAmount) != 0 ?
                             FormatDecimal(((group.Sum(a => a.ClosingAmount) - group.Sum(a => a.OpeningAmount)) / group.Sum(a => a.ClosingAmount)) * 100 ?? 0, roundinvalue) : null,

       })
       .ToList();
            return groupedSums;
        }

        #endregion  Fee General Tab

        #region 2.Fee Account Compare Tab

        [HttpGet]
        public ActionResult GetFeeAccountCompareMonthlyClosing(string dateFromString, string dateToString, int? companyID)
        {

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            var dateFrom = DateTime.ParseExact(dateFromString, dateFormat, CultureInfo.InvariantCulture);

            var dateTo = DateTime.ParseExact(dateToString, dateFormat, CultureInfo.InvariantCulture);
            var viewModelList = new List<MCGridFeeVSAccountsFeeTypelViewModel>();

            var entry = ClientFactory.AccountingServiceClient(CallContext).GetFeeAccountCompareMonthlyClosing(dateFrom, dateTo, companyID);
            viewModelList = GetFeeVSAccountsFeeTypes(entry);
            var jsonResult = Json(viewModelList);

            //TODO: Required Check
            //jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        private List<MCGridFeeVSAccountsFeeTypelViewModel> GetFeeVSAccountsFeeTypes(List<FeeGeneralMonthlyClosingDTO> listData)
        {
            var decimalvalue = new Domain.Setting.SettingBL().GetSettingValue<int?>("DEFAULT_DECIMALPOINTS");
            int roundinvalue = decimalvalue.HasValue ? decimalvalue.Value : 2;
            var feeDuedetails = listData
           .GroupBy(a => new { a.FeeTypeID, a.FeeTypeName })
           .Select(group => new MCGridFeeVSAccountsFeeTypelViewModel
           {
               IsExpand = false,
               FeeTypeID = group.Key.FeeTypeID,
               FeeTypeName = group.Key.FeeTypeName,
               FeeAmount = FormatDecimal(group.Sum(a => a.FeeAmount) ?? 0, roundinvalue),
               AccountAmount = FormatDecimal(group.Sum(a => a.AccountAmount) ?? 0, roundinvalue),
               DifferenceAmount = FormatDecimal((group.Sum(a => a.FeeAmount) - group.Sum(a => a.AccountAmount)) ?? 0, roundinvalue),
               DifferencePlusKPI = group.Sum(a => a.AccountAmount) != 0 ?
                             FormatDecimal(((group.Sum(a => a.FeeAmount) - group.Sum(a => a.AccountAmount)) / group.Sum(a => a.AccountAmount)) * 100 ?? 0, roundinvalue) : null,

               MCGridFeeVSAccountsFeeCycle = GetFeeVSAccountsFeeCycles(listData.Where(x => x.FeeTypeID == group.Key.FeeTypeID).ToList())
           })
           .ToList();

            return feeDuedetails;
        }

        private List<MCGridFeeVSAccountsFeeCycleViewModel> GetFeeVSAccountsFeeCycles(List<FeeGeneralMonthlyClosingDTO> listData)
        {
            var decimalvalue = new Domain.Setting.SettingBL().GetSettingValue<int?>("DEFAULT_DECIMALPOINTS");
            int roundinvalue = decimalvalue.HasValue ? decimalvalue.Value : 2;
            var feeDueFeeCycleDetail = listData
          .GroupBy(a => new { a.FeeCycleID, a.FeeCycleName })
          .Select(group => new MCGridFeeVSAccountsFeeCycleViewModel
          {
              IsExpand = false,
              FeeCycleID = group.Key.FeeCycleID,
              FeeCycleName = group.Key.FeeCycleName,
              FeeAmount = FormatDecimal(group.Sum(a => a.FeeAmount) ?? 0, roundinvalue),
              AccountAmount = FormatDecimal(group.Sum(a => a.AccountAmount) ?? 0, roundinvalue),
              DifferenceAmount = FormatDecimal((group.Sum(a => a.FeeAmount) - group.Sum(a => a.AccountAmount)) ?? 0, roundinvalue),
              DifferencePlusKPI = group.Sum(a => a.AccountAmount) != 0 ?
                             FormatDecimal(((group.Sum(a => a.FeeAmount) - group.Sum(a => a.AccountAmount)) / group.Sum(a => a.AccountAmount)) * 100 ?? 0, roundinvalue) : null,

              MCGridFeeVSAccountsFeeMaster = GetFeeVSAccountsFeeMasters(listData.Where(x => x.FeeCycleID == group.Key.FeeCycleID).ToList())
          })
          .ToList();

            return feeDueFeeCycleDetail;
        }

        private List<MCGridFeeVSAccountsFeeMasterViewModel> GetFeeVSAccountsFeeMasters(List<FeeGeneralMonthlyClosingDTO> listData)
        {
            var decimalvalue = new Domain.Setting.SettingBL().GetSettingValue<int?>("DEFAULT_DECIMALPOINTS");
            int roundinvalue = decimalvalue.HasValue ? decimalvalue.Value : 2;
            var groupedSums = listData
       .GroupBy(a => new { a.FeeMasterID, a.FeeMasterName })
       .Select(group => new MCGridFeeVSAccountsFeeMasterViewModel
       {

           FeeMasterID = group.Key.FeeMasterID,
           FeeMasterName = group.Key.FeeMasterName,
           FeeAmount = FormatDecimal(group.Sum(a => a.FeeAmount) ?? 0,
           roundinvalue),
           AccountAmount = FormatDecimal(group.Sum(a => a.AccountAmount) ?? 0,
           roundinvalue),
           DifferenceAmount = FormatDecimal((group.Sum(a => a.FeeAmount) - group.Sum(a => a.AccountAmount)) ?? 0,
           roundinvalue),
           DifferencePlusKPI = group.Sum(a => a.AccountAmount) != 0 ?
                             FormatDecimal(((group.Sum(a => a.FeeAmount) - group.Sum(a => a.AccountAmount)) / group.Sum(a => a.AccountAmount)) * 100 ?? 0, roundinvalue) : null,

       })
       .ToList();

            return groupedSums;
        }

        #endregion Fee Account Compare Tab

        #region Inventory General Tab

        [HttpGet]
        public ActionResult GetInventoryGeneralMonthlyClosing(string dateFromString, string dateToString, int? companyID)
        {

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            var dateFrom = DateTime.ParseExact(dateFromString, dateFormat, CultureInfo.InvariantCulture);

            var dateTo = DateTime.ParseExact(dateToString, dateFormat, CultureInfo.InvariantCulture);
            var viewModelList = new List<MCGridInventoryTransTypeViewModel>();

            var entry = ClientFactory.AccountingServiceClient(CallContext).GetInventoryGeneralMonthlyClosing(dateFrom, dateTo, companyID);
            viewModelList = GetInventoryTransactionTypes(entry);
            var jsonResult = Json(viewModelList);

            //TODO: Required Check
            //jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        private List<MCGridInventoryTransTypeViewModel> GetInventoryTransactionTypes(List<InvoiceGeneralMonthlyClosingDTO> listData)
        {

            var decimalvalue = new Domain.Setting.SettingBL().GetSettingValue<int?>("DEFAULT_DECIMALPOINTS");
            int roundinvalue = decimalvalue.HasValue ? decimalvalue.Value : 2;
            var feeDuedetails = listData
           .GroupBy(a => new { a.TransactionTypeID, a.TransactionTypeName })
           .Select(group => new MCGridInventoryTransTypeViewModel
           {
               IsExpand = false,
               TransactionTypeID = group.Key.TransactionTypeID,
               TransactionTypeName = group.Key.TransactionTypeName,
               ClosingAmount = FormatDecimal(group.Sum(a => a.ClosingAmount) ?? 0, roundinvalue),
               ClosingCredit = FormatDecimal(group.Sum(a => a.ClosingCredit) ?? 0, roundinvalue),
               ClosingDebit = FormatDecimal(group.Sum(a => a.ClosingDebit) ?? 0, roundinvalue),
               OpeningAmount = FormatDecimal(group.Sum(a => a.OpeningAmount) ?? 0, roundinvalue),
               OpeningCredit = FormatDecimal(group.Sum(a => a.OpeningCredit) ?? 0, roundinvalue),
               OpeningDebit = FormatDecimal(group.Sum(a => a.OpeningDebit) ?? 0, roundinvalue),
               TransactionAmount = FormatDecimal(group.Sum(a => a.TransactionAmount) ?? 0, roundinvalue),
               TransactionCredit = FormatDecimal(group.Sum(a => a.TransactionCredit) ?? 0, roundinvalue),
               TransactionDebit = FormatDecimal(group.Sum(a => a.TransactionDebit) ?? 0, roundinvalue),
               DifferenceAmount = FormatDecimal((group.Sum(a => a.ClosingAmount) - group.Sum(a => a.OpeningAmount)) ?? 0, roundinvalue),
               DifferencePlusKPI = group.Sum(a => a.ClosingAmount) != 0 ?
                               FormatDecimal(((group.Sum(a => a.ClosingAmount) - group.Sum(a => a.OpeningAmount)) / group.Sum(a => a.ClosingAmount)) * 100 ?? 0, roundinvalue) : null,

               MCGridInventoryDocumentType = GetInventoryDocumentTypes(listData.Where(x => x.TransactionTypeID == group.Key.TransactionTypeID).ToList())
           })
           .ToList();

            return feeDuedetails;
        }

        private List<MCGridInventoryDocumentTypeViewModel> GetInventoryDocumentTypes(List<InvoiceGeneralMonthlyClosingDTO> listData)
        {
            var decimalvalue = new Domain.Setting.SettingBL().GetSettingValue<int?>("DEFAULT_DECIMALPOINTS");
            int roundinvalue = decimalvalue.HasValue ? decimalvalue.Value : 2;
            var feeDueFeeCycleDetail = listData
          .GroupBy(a => new { a.DocumentTypeID, a.DocumentTypeName })
          .Select(group => new MCGridInventoryDocumentTypeViewModel
          {

              DocumentTypeID = group.Key.DocumentTypeID,
              DocumentTypeName = group.Key.DocumentTypeName,
              ClosingAmount = FormatDecimal(group.Sum(a => a.ClosingAmount) ?? 0,
              roundinvalue),
              ClosingCredit = FormatDecimal(group.Sum(a => a.ClosingCredit) ?? 0, roundinvalue),
              ClosingDebit = FormatDecimal(group.Sum(a => a.ClosingDebit) ?? 0, roundinvalue),
              OpeningAmount = FormatDecimal(group.Sum(a => a.OpeningAmount) ?? 0, roundinvalue),
              OpeningCredit = FormatDecimal(group.Sum(a => a.OpeningCredit) ?? 0, roundinvalue),
              OpeningDebit = FormatDecimal(group.Sum(a => a.OpeningDebit) ?? 0, roundinvalue),
              TransactionAmount = FormatDecimal(group.Sum(a => a.TransactionAmount) ?? 0, roundinvalue),
              TransactionCredit = FormatDecimal(group.Sum(a => a.TransactionCredit) ?? 0, roundinvalue),
              TransactionDebit = FormatDecimal(group.Sum(a => a.TransactionDebit) ?? 0, roundinvalue),
              DifferenceAmount = FormatDecimal((group.Sum(a => a.ClosingAmount) - group.Sum(a => a.OpeningAmount)) ?? 0, roundinvalue),
              DifferencePlusKPI = group.Sum(a => a.ClosingAmount) != 0 ?
                               FormatDecimal(((group.Sum(a => a.ClosingAmount) - group.Sum(a => a.OpeningAmount)) / group.Sum(a => a.ClosingAmount)) * 100 ?? 0, roundinvalue) : null,

          })
          .ToList();

            return feeDueFeeCycleDetail;
        }

        #endregion Inventory General Tab

        #region 4.Stock Tab

        [HttpGet]
        public ActionResult GetStockMonthlyClosing(string dateFromString, string dateToString, int? companyID)
        {

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            var dateFrom = DateTime.ParseExact(dateFromString, dateFormat, CultureInfo.InvariantCulture);

            var dateTo = DateTime.ParseExact(dateToString, dateFormat, CultureInfo.InvariantCulture);
            var viewModelList = new List<MCGridStockTypeViewModel>();

            var entry = ClientFactory.AccountingServiceClient(CallContext).GetStockMonthlyClosing(dateFrom, dateTo, companyID);
            viewModelList = GetStockTypes(entry);
            var jsonResult = Json(viewModelList);

            //TODO: Required Check
            //jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        private List<MCGridStockTypeViewModel> GetStockTypes(List<StockMonthlyClosingDTO> listData)
        {
            var decimalvalue = new Domain.Setting.SettingBL().GetSettingValue<int?>("DEFAULT_DECIMALPOINTS");
            int roundinvalue = decimalvalue.HasValue ? decimalvalue.Value : 2;
            var feeDuedetails = listData
           .GroupBy(a => new { a.TypeID, a.TypeName })
           .Select(group => new MCGridStockTypeViewModel
           {
               IsExpand = false,
               TypeID = group.Key.TypeID,
               TypeName = group.Key.TypeName,
               ClosingAmount = FormatDecimal(group.Sum(a => a.ClosingAmount) ?? 0, roundinvalue),
               ClosingCredit = FormatDecimal(group.Sum(a => a.ClosingCredit) ?? 0, roundinvalue),
               ClosingDebit = FormatDecimal(group.Sum(a => a.ClosingDebit) ?? 0, roundinvalue),
               OpeningAmount = FormatDecimal(group.Sum(a => a.OpeningAmount) ?? 0, roundinvalue),
               OpeningCredit = FormatDecimal(group.Sum(a => a.OpeningCredit) ?? 0, roundinvalue),
               OpeningDebit = FormatDecimal(group.Sum(a => a.OpeningDebit) ?? 0, roundinvalue),
               TransactionAmount = FormatDecimal(group.Sum(a => a.TransactionAmount) ?? 0, roundinvalue),
               TransactionCredit = FormatDecimal(group.Sum(a => a.TransactionCredit) ?? 0, roundinvalue),
               TransactionDebit = FormatDecimal(group.Sum(a => a.TransactionDebit) ?? 0, roundinvalue),
               DifferenceAmount = FormatDecimal((group.Sum(a => a.ClosingAmount) - group.Sum(a => a.OpeningAmount)) ?? 0, roundinvalue),
               DifferencePlusKPI = group.Sum(a => a.ClosingAmount) != 0 ?
                                FormatDecimal(((group.Sum(a => a.ClosingAmount) - group.Sum(a => a.OpeningAmount)) / group.Sum(a => a.ClosingAmount)) * 100 ?? 0, roundinvalue) : null,

               MCGridStockAccount = GetStockAccounts(listData.Where(x => x.TypeID == group.Key.TypeID).ToList())
           })
           .ToList();

            return feeDuedetails;
        }

        private List<MCGridStockAccountViewModel> GetStockAccounts(List<StockMonthlyClosingDTO> listData)
        {
            var decimalvalue = new Domain.Setting.SettingBL().GetSettingValue<int?>("DEFAULT_DECIMALPOINTS");
            int roundinvalue = decimalvalue.HasValue ? decimalvalue.Value : 2;
            var details = listData.GroupBy(a => new { a.AccountID, a.AccountName })
              .Select(group => new MCGridStockAccountViewModel
              {
                  AccountID = group.Key.AccountID,
                  AccountName = group.Key.AccountName,
                  ClosingAmount = FormatDecimal(group.Sum(a => a.ClosingAmount.Value), roundinvalue),
                  ClosingCredit = FormatDecimal(group.Sum(a => a.ClosingCredit.Value), roundinvalue),
                  ClosingDebit = FormatDecimal(group.Sum(a => a.ClosingDebit.Value), roundinvalue),
                  OpeningAmount = FormatDecimal(group.Sum(a => a.OpeningAmount.Value), roundinvalue),
                  OpeningCredit = FormatDecimal(group.Sum(a => a.OpeningCredit.Value), roundinvalue),
                  OpeningDebit = FormatDecimal(group.Sum(a => a.OpeningDebit.Value), roundinvalue),
                  TransactionAmount = FormatDecimal(group.Sum(a => a.TransactionAmount.Value), roundinvalue),
                  TransactionCredit = FormatDecimal(group.Sum(a => a.TransactionCredit.Value), roundinvalue),
                  TransactionDebit = FormatDecimal(group.Sum(a => a.TransactionDebit.Value), roundinvalue),
                  DifferenceAmount = FormatDecimal(group.Sum(a => a.ClosingAmount.Value) - group.Sum(a => a.OpeningAmount.Value), roundinvalue),
                  DifferencePlusKPI = group.Sum(a => a.ClosingAmount) != 0 ?
                                    FormatDecimal(((group.Sum(a => a.ClosingAmount) - group.Sum(a => a.OpeningAmount)) / group.Sum(a => a.ClosingAmount)) * 100 ?? 0,
                  roundinvalue)
                                    : null,


              }).ToList();

            return details;
        }

        #endregion Inventory General Tab

        #region Fee Cancel Tab

        [HttpGet]
        public JsonResult GetFeeCancelMonthlyClosing(string dateFromString, string dateToString, int? companyID)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            var dateFrom = DateTime.ParseExact(dateFromString, dateFormat, CultureInfo.InvariantCulture);

            var dateTo = DateTime.ParseExact(dateToString, dateFormat, CultureInfo.InvariantCulture);
            var viewModelList = new List<MCGridFeeCancelFeeTypeViewModel>();

            var entry = ClientFactory.AccountingServiceClient(CallContext).GetFeeCancelMonthlyClosing(dateFrom, dateTo, companyID);
            viewModelList = GetFeeCancelFeeTypeDetails(entry);
            var jsonResult = Json(viewModelList);

            //TODO: Required Check
            //jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        private List<MCGridFeeCancelFeeTypeViewModel> GetFeeCancelFeeTypeDetails(List<FeeGeneralMonthlyClosingDTO> listData)
        {
            var decimalvalue = new Domain.Setting.SettingBL().GetSettingValue<int?>("DEFAULT_DECIMALPOINTS");
            int roundinvalue = decimalvalue.HasValue ? decimalvalue.Value : 2;
            var feeDuedetails = listData
           .GroupBy(a => new { a.FeeTypeID, a.FeeTypeName })
           .Select(group => new MCGridFeeCancelFeeTypeViewModel
           {
               IsExpand = false,

               FeeTypeID = group.Key.FeeTypeID,
               FeeTypeName = group.Key.FeeTypeName,
               ClosingAmount = FormatDecimal(group.Sum(a => a.ClosingAmount) ?? 0, roundinvalue),
               ClosingCredit = FormatDecimal(group.Sum(a => a.ClosingCredit) ?? 0, roundinvalue),
               ClosingDebit = FormatDecimal(group.Sum(a => a.ClosingDebit) ?? 0, roundinvalue),
               OpeningAmount = FormatDecimal(group.Sum(a => a.OpeningAmount) ?? 0, roundinvalue),
               OpeningCredit = FormatDecimal(group.Sum(a => a.OpeningCredit) ?? 0, roundinvalue),
               OpeningDebit = FormatDecimal(group.Sum(a => a.OpeningDebit) ?? 0, roundinvalue),
               TransactionAmount = FormatDecimal(group.Sum(a => a.TransactionAmount) ?? 0, roundinvalue),
               TransactionCredit = FormatDecimal(group.Sum(a => a.TransactionCredit) ?? 0, roundinvalue),
               TransactionDebit = FormatDecimal(group.Sum(a => a.TransactionDebit) ?? 0, roundinvalue),
               DifferenceAmount = FormatDecimal((group.Sum(a => a.ClosingAmount) - group.Sum(a => a.OpeningAmount)) ?? 0, roundinvalue),
               DifferencePlusKPI = group.Sum(a => a.ClosingAmount) != 0 ?
                                 FormatDecimal(((group.Sum(a => a.ClosingAmount) - group.Sum(a => a.OpeningAmount)) / group.Sum(a => a.ClosingAmount)) * 100 ?? 0, roundinvalue) : null,
               MCGridFeeDueFeeCycle = GetFeeGeneralFeeCycles(listData.Where(x => x.FeeTypeID == group.Key.FeeTypeID).ToList())
           })
           .ToList();

            return feeDuedetails;
        }

        #endregion Fee Cancel Tab

        #region Account Cancel Tab

        [HttpGet]
        public ActionResult GetAccountCancelMonthlyClosing(string dateFromString, string dateToString, int? companyID)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            var dateFrom = DateTime.ParseExact(dateFromString, dateFormat, CultureInfo.InvariantCulture);

            var dateTo = DateTime.ParseExact(dateToString, dateFormat, CultureInfo.InvariantCulture);
            var viewModelList = new List<MCGridAccountCancelTransType>();

            var entry = ClientFactory.AccountingServiceClient(CallContext).GetAccountCancelMonthlyClosing(dateFrom, dateTo, companyID);
            viewModelList = GetAccountCancelFeeTypeDetails(entry);
            var jsonResult = Json(viewModelList);

            //TODO: Required Check
            //jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        private List<MCGridAccountCancelTransType> GetAccountCancelFeeTypeDetails(List<AccountsMonthlyClosingDTO> listData)
        {
            var decimalvalue = new Domain.Setting.SettingBL().GetSettingValue<int?>("DEFAULT_DECIMALPOINTS");
            int roundinvalue = decimalvalue.HasValue ? decimalvalue.Value : 2;
            var transTypes = listData
           .GroupBy(a => new { a.TransactionTypeID, a.TransactionTypeName })
           .Select(group => new MCGridAccountCancelTransType
           {
               IsExpand = false,
               TransactionTypeID = group.Key.TransactionTypeID,
               TransactionTypeName = group.Key.TransactionTypeName,
               ClosingAmount = FormatDecimal(group.Sum(a => a.ClosingAmount.Value), roundinvalue),
               ClosingCredit = FormatDecimal(group.Sum(a => a.ClosingCredit.Value), roundinvalue),
               ClosingDebit = FormatDecimal(group.Sum(a => a.ClosingDebit.Value), roundinvalue),
               OpeningAmount = FormatDecimal(group.Sum(a => a.OpeningAmount.Value), roundinvalue),
               OpeningCredit = FormatDecimal(group.Sum(a => a.OpeningCredit.Value), roundinvalue),
               OpeningDebit = FormatDecimal(group.Sum(a => a.OpeningDebit.Value), roundinvalue),
               TransactionAmount = FormatDecimal(group.Sum(a => a.TransactionAmount.Value), roundinvalue),
               TransactionCredit = FormatDecimal(group.Sum(a => a.TransactionCredit.Value), roundinvalue),
               TransactionDebit = FormatDecimal(group.Sum(a => a.TransactionDebit.Value), roundinvalue),
               DifferenceAmount = (group.Sum(a => a.ClosingAmount) - group.Sum(a => a.OpeningAmount)),
               DifferencePlusKPI = group.Sum(a => a.ClosingAmount) != 0 ?
                                 FormatDecimal(((group.Sum(a => a.ClosingAmount) - group.Sum(a => a.OpeningAmount)) / group.Sum(a => a.ClosingAmount)) * 100 ?? 0,
               roundinvalue)
                                : null,

               MCGridInventoryDocumentType = GetInventoryDocumentTypes(listData.Where(x => x.TransactionTypeID == group.Key.TransactionTypeID).ToList())
           })
           .ToList();

            return transTypes;
        }

        private List<MCGridInventoryDocumentTypeViewModel> GetInventoryDocumentTypes(List<AccountsMonthlyClosingDTO> listData)
        {
            var decimalvalue = new Domain.Setting.SettingBL().GetSettingValue<int?>("DEFAULT_DECIMALPOINTS");
            int roundinvalue = decimalvalue.HasValue ? decimalvalue.Value : 2;
            var inventoryDocumentTypes = listData
          .GroupBy(a => new { a.DocumentTypeID, a.DocumentTypeName })
          .Select(group => new MCGridInventoryDocumentTypeViewModel
          {
              DocumentTypeID = group.Key.DocumentTypeID,
              DocumentTypeName = group.Key.DocumentTypeName,
              ClosingAmount = FormatDecimal(group.Sum(a => a.ClosingAmount) ?? 0, roundinvalue),
              ClosingCredit = FormatDecimal(group.Sum(a => a.ClosingCredit) ?? 0, roundinvalue),
              ClosingDebit = FormatDecimal(group.Sum(a => a.ClosingDebit) ?? 0, roundinvalue),
              OpeningAmount = FormatDecimal(group.Sum(a => a.OpeningAmount) ?? 0, roundinvalue),
              OpeningCredit = FormatDecimal(group.Sum(a => a.OpeningCredit) ?? 0, roundinvalue),
              OpeningDebit = FormatDecimal(group.Sum(a => a.OpeningDebit) ?? 0, roundinvalue),
              TransactionAmount = FormatDecimal(group.Sum(a => a.TransactionAmount) ?? 0, roundinvalue),
              TransactionCredit = FormatDecimal(group.Sum(a => a.TransactionCredit) ?? 0, roundinvalue),
              TransactionDebit = FormatDecimal(group.Sum(a => a.TransactionDebit) ?? 0, roundinvalue),
              DifferenceAmount = FormatDecimal((group.Sum(a => a.ClosingAmount) - group.Sum(a => a.OpeningAmount)) ?? 0, roundinvalue),
              DifferencePlusKPI = group.Sum(a => a.ClosingAmount) != 0 ?
                                FormatDecimal(((group.Sum(a => a.ClosingAmount) - group.Sum(a => a.OpeningAmount)) / group.Sum(a => a.ClosingAmount)) * 100 ?? 0, roundinvalue) : null,

          })
          .ToList();

            return inventoryDocumentTypes;
        }
        #endregion Account Cancel Tab

        #region Inventory Cancel Tab

        [HttpGet]
        public JsonResult GetInventoryCancelMonthlyClosing(string dateFromString, string dateToString, int? companyID)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            var dateFrom = DateTime.ParseExact(dateFromString, dateFormat, CultureInfo.InvariantCulture);

            var dateTo = DateTime.ParseExact(dateToString, dateFormat, CultureInfo.InvariantCulture);
            var viewModelList = new List<MCGridInventoryCancelViewModel>();

            var entry = ClientFactory.AccountingServiceClient(CallContext).GetInventoryCancelMonthlyClosing(dateFrom, dateTo, companyID);
            viewModelList = GetInventoryCancelTransactionTypes(entry);
            var jsonResult = Json(viewModelList);

            //TODO: Required Check
            //jsonResult.MaxJsonLength = int.MaxValue;


            return jsonResult;
        }
        private List<MCGridInventoryCancelViewModel> GetInventoryCancelTransactionTypes(List<InvoiceGeneralMonthlyClosingDTO> listData)
        {
            var decimalvalue = new Domain.Setting.SettingBL().GetSettingValue<int?>("DEFAULT_DECIMALPOINTS");
            int roundinvalue = decimalvalue.HasValue ? decimalvalue.Value : 2;
            var transactionTypes = listData
           .GroupBy(a => new { a.TransactionTypeID, a.TransactionTypeName })
           .Select(group => new MCGridInventoryCancelViewModel
           {
               IsExpand = false,
               TransactionTypeID = group.Key.TransactionTypeID,
               TransactionTypeName = group.Key.TransactionTypeName,
               ClosingAmount = FormatDecimal(group.Sum(a => a.ClosingAmount) ?? 0, roundinvalue),
               ClosingCredit = FormatDecimal(group.Sum(a => a.ClosingCredit) ?? 0, roundinvalue),
               ClosingDebit = FormatDecimal(group.Sum(a => a.ClosingDebit) ?? 0, roundinvalue),
               OpeningAmount = FormatDecimal(group.Sum(a => a.OpeningAmount) ?? 0, roundinvalue),
               OpeningCredit = FormatDecimal(group.Sum(a => a.OpeningCredit) ?? 0, roundinvalue),
               OpeningDebit = FormatDecimal(group.Sum(a => a.OpeningDebit) ?? 0, roundinvalue),
               TransactionAmount = FormatDecimal(group.Sum(a => a.TransactionAmount) ?? 0, roundinvalue),
               TransactionCredit = FormatDecimal(group.Sum(a => a.TransactionCredit) ?? 0, roundinvalue),
               TransactionDebit = FormatDecimal(group.Sum(a => a.TransactionDebit) ?? 0, roundinvalue),
               DifferenceAmount = FormatDecimal((group.Sum(a => a.ClosingAmount) - group.Sum(a => a.OpeningAmount)) ?? 0, roundinvalue),
               DifferencePlusKPI = group.Sum(a => a.ClosingAmount) != 0 ?
                                FormatDecimal(((group.Sum(a => a.ClosingAmount) - group.Sum(a => a.OpeningAmount)) / group.Sum(a => a.ClosingAmount)) * 100 ?? 0, roundinvalue) : null,

               MCGridInventoryDocumentType = GetInventoryDocumentTypes(listData.Where(x => x.TransactionTypeID == group.Key.TransactionTypeID).ToList())
           })
           .ToList();

            return transactionTypes;
        }
        #endregion Inventory Cancel Tab

        #region 8.Mismatched Fees Tab

        [HttpGet]
        public ActionResult GetFeeMismatchedMonthlyClosing(string dateFromString, string dateToString, int? companyID)
        {

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            var dateFrom = DateTime.ParseExact(dateFromString, dateFormat, CultureInfo.InvariantCulture);

            var dateTo = DateTime.ParseExact(dateToString, dateFormat, CultureInfo.InvariantCulture);
            var viewModelList = new List<MCGridMismatchFeesViewModel>();

            var entry = ClientFactory.AccountingServiceClient(CallContext).GetFeeMismatchedMonthlyClosing(dateFrom, dateTo, companyID);
            viewModelList = GetFeeMismatchedMonthlyClosing(entry);
            var jsonResult = Json(viewModelList);

            //TODO: Required Check
            //jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;

        }

        private List<MCGridMismatchFeesViewModel> GetFeeMismatchedMonthlyClosing(List<FeeMismatchedMonthlyClosingDTO> listData)
        {
            var decimalvalue = new Domain.Setting.SettingBL().GetSettingValue<int?>("DEFAULT_DECIMALPOINTS");
            int roundinvalue = decimalvalue.HasValue ? decimalvalue.Value : 2;
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var feeDuedetails = listData
           .GroupBy(a => new { a.FeeTypeID, a.FeeTypeName, a.SchoolName, a.AdmissionNumber, a.StudentName, a.FeeName, a.InvoiceDate, a.InvoiceNo, a.Remarks })
           .Select(group => new MCGridMismatchFeesViewModel
           {
               FeeTypeID = group.Key.FeeTypeID,
               FeeName = group.Key.FeeName,
               Remarks = group.Key.Remarks,
               InvoiceNo = group.Key.InvoiceNo,
               Amount = group.Sum(a => a.Amount),
               FeeTypeName = group.Key.FeeTypeName,
               SchoolName = group.Key.SchoolName,
               StudentName = group.Key.StudentName,
               AdmissionNumber = group.Key.AdmissionNumber,
               InvoiceDateString = (group.Key.InvoiceDate.HasValue ? group.Key.InvoiceDate.Value : DateTime.Now).ToString(dateFormat, CultureInfo.InvariantCulture),

           })
           .ToList();

            return feeDuedetails;
        }
        #endregion 8.Mismatched Fees Tab

        #region 9. Account Mismatched Tab

        [HttpGet]
        public ActionResult GetAccountsMismatchedMonthlyClosing(string dateFromString, string dateToString, int? companyID)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            var dateFrom = DateTime.ParseExact(dateFromString, dateFormat, CultureInfo.InvariantCulture);

            var dateTo = DateTime.ParseExact(dateToString, dateFormat, CultureInfo.InvariantCulture);
            var viewModelList = new List<MCGridMismatchAccountsViewModel>();

            var entry = ClientFactory.AccountingServiceClient(CallContext).GetAccountsMismatchedMonthlyClosing(dateFrom, dateTo, companyID);
            viewModelList = GetAccountMismatchedMonthlyClosing(entry);

            var jsonResult = Json(viewModelList);

            //TODO: Required Check
            //jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        private List<MCGridMismatchAccountsViewModel> GetAccountMismatchedMonthlyClosing(List<AccountMismatchedMonthlyClosingDTO> listData)
        {
            var decimalvalue = new Domain.Setting.SettingBL().GetSettingValue<int?>("DEFAULT_DECIMALPOINTS");
            int roundinvalue = decimalvalue.HasValue ? decimalvalue.Value : 2;
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var accountDetails = listData
           .GroupBy(a => new { a.DocumentTypeID, a.DocumentTypeName, a.TranNo, a.TranDate, a.TranTypeID, a.TranTypeName, a.VoucherNo, a.InvoiceNo, a.Remarks, a.Narration, a.Branch })
           .Select(group => new MCGridMismatchAccountsViewModel
           {
               TranTypeName = group.Key.TranTypeName,
               DocumentTypeName = group.Key.DocumentTypeName,
               Branch = group.Key.Branch,
               TranNo = group.Key.TranNo,
               Narration = group.Key.Narration,
               VoucherNo = group.Key.VoucherNo,
               Remarks = group.Key.Remarks,
               Amount = group.Sum(a => a.Amount),
               TranDateString = group.Key.TranDate.HasValue ? (group.Key.TranDate.Value).ToString(dateFormat, CultureInfo.InvariantCulture) : null,
           })
           .ToList();

            return accountDetails;
        }
        #endregion

        #region 10.Account General Tab
        [HttpGet]
        public ActionResult GetAccountsGeneralMonthlyClosing(string dateFromString, string dateToString, int? companyID)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            var dateFrom = DateTime.ParseExact(dateFromString, dateFormat, CultureInfo.InvariantCulture);

            var dateTo = DateTime.ParseExact(dateToString, dateFormat, CultureInfo.InvariantCulture);
            var viewModelList = new List<MCGridAccountGeneralMainGroupViewModel>();

            var entry = ClientFactory.AccountingServiceClient(CallContext).GetAccountsGeneralMonthlyClosing(dateFrom, dateTo, companyID);
            viewModelList = GetAccountsGeneralMainGroups(entry);

            var jsonResult = Json(viewModelList);

            //TODO: Required Check
            //jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;

        }
        private List<MCGridAccountGeneralMainGroupViewModel> GetAccountsGeneralMainGroups(List<AccountsGeneralMonthlyClosingDTO> listData)
        {
            var decimalvalue = new Domain.Setting.SettingBL().GetSettingValue<int?>("DEFAULT_DECIMALPOINTS");
            int roundinvalue = decimalvalue.HasValue ? decimalvalue.Value : 2;
            var feeDuedetails = listData
           .GroupBy(a => new { a.MainGroupName })
           .Select(group => new MCGridAccountGeneralMainGroupViewModel
           {
               IsExpand = false,

               //MainGroupID = group.Key.MainGroupID,
               MainGroupName = group.Key.MainGroupName,
               ClosingAmount = FormatDecimal(group.Sum(a => a.ClosingAmount) ?? 0, roundinvalue),
               ClosingCredit = FormatDecimal(group.Sum(a => a.ClosingCredit) ?? 0, roundinvalue),
               ClosingDebit = FormatDecimal(group.Sum(a => a.ClosingDebit) ?? 0, roundinvalue),
               OpeningAmount = FormatDecimal(group.Sum(a => a.OpeningAmount) ?? 0, roundinvalue),
               OpeningCredit = FormatDecimal(group.Sum(a => a.OpeningCredit) ?? 0, roundinvalue),
               OpeningDebit = FormatDecimal(group.Sum(a => a.OpeningDebit) ?? 0, roundinvalue),
               TransactionAmount = FormatDecimal(group.Sum(a => a.TransactionAmount) ?? 0, roundinvalue),
               TransactionCredit = FormatDecimal(group.Sum(a => a.TransactionCredit) ?? 0, roundinvalue),
               TransactionDebit = FormatDecimal(group.Sum(a => a.TransactionDebit) ?? 0, roundinvalue),
               DifferenceAmount = FormatDecimal((group.Sum(a => a.ClosingAmount) - group.Sum(a => a.OpeningAmount)) ?? 0, roundinvalue),
               DifferencePlusKPI = group.Sum(a => a.ClosingAmount) != 0 ?
                                 FormatDecimal(((group.Sum(a => a.ClosingAmount) - group.Sum(a => a.OpeningAmount)) / group.Sum(a => a.ClosingAmount)) * 100 ?? 0, roundinvalue) : null,
               MCGridAccountGeneralSubGroup = GetAccountGeneralSubGroups(listData.Where(x => x.MainGroupName == group.Key.MainGroupName).ToList())
           })
           .ToList();

            return feeDuedetails;
        }


        private List<MCGridAccountGeneralSubGroupViewModel> GetAccountGeneralSubGroups(List<AccountsGeneralMonthlyClosingDTO> listData)
        {
            var decimalvalue = new Domain.Setting.SettingBL().GetSettingValue<int?>("DEFAULT_DECIMALPOINTS");
            int roundinvalue = decimalvalue.HasValue ? decimalvalue.Value : 2;
            var feeDueFeeCycleDetail = listData
          .GroupBy(a => new { a.SubGroupName })
          .Select(group => new MCGridAccountGeneralSubGroupViewModel
          {
              IsExpand = false,

              //SubGroupID = group.Key.SubGroupID,
              SubGroupName = group.Key.SubGroupName,
              ClosingAmount = FormatDecimal(group.Sum(a => a.ClosingAmount) ?? 0,
              roundinvalue),
              ClosingCredit = FormatDecimal(group.Sum(a => a.ClosingCredit) ?? 0,
              roundinvalue),
              ClosingDebit = FormatDecimal(group.Sum(a => a.ClosingDebit) ?? 0,
              roundinvalue),
              OpeningAmount = FormatDecimal(group.Sum(a => a.OpeningAmount) ?? 0,
              roundinvalue),
              OpeningCredit = FormatDecimal(group.Sum(a => a.OpeningCredit) ?? 0,
              roundinvalue),
              OpeningDebit = FormatDecimal(group.Sum(a => a.OpeningDebit) ?? 0,
              roundinvalue),
              TransactionAmount = FormatDecimal(group.Sum(a => a.TransactionAmount) ?? 0,
              roundinvalue),
              TransactionCredit = FormatDecimal(group.Sum(a => a.TransactionCredit) ?? 0,
              roundinvalue),
              TransactionDebit = FormatDecimal(group.Sum(a => a.TransactionDebit) ?? 0,
              roundinvalue),
              DifferenceAmount = FormatDecimal((group.Sum(a => a.ClosingAmount) - group.Sum(a => a.OpeningAmount)) ?? 0,
              roundinvalue),
              DifferencePlusKPI = group.Sum(a => a.ClosingAmount) != 0 ?
                              FormatDecimal(((group.Sum(a => a.ClosingAmount) - group.Sum(a => a.OpeningAmount)) / group.Sum(a => a.ClosingAmount)) * 100 ?? 0,
              roundinvalue) : null,
              MCGridAccountGeneralGroup = GetAccountsGeneralGroups(listData.Where(x => x.SubGroupName == group.Key.SubGroupName).ToList())
          })
          .ToList();

            return feeDueFeeCycleDetail;
        }

        private List<MCGridAccountGeneralGroupViewModel> GetAccountsGeneralGroups(List<AccountsGeneralMonthlyClosingDTO> listData)
        {
            var decimalvalue = new Domain.Setting.SettingBL().GetSettingValue<int?>("DEFAULT_DECIMALPOINTS");
            int roundinvalue = decimalvalue.HasValue ? decimalvalue.Value : 2;
            var groupedSums = listData
               .GroupBy(a => new { a.GroupName })
               .Select(group => new MCGridAccountGeneralGroupViewModel
               {
                   // GroupID = group.Key.GroupID,
                   GroupName = group.Key.GroupName,
                   ClosingAmount = FormatDecimal(group.Sum(a => a.ClosingAmount) ?? 0, roundinvalue),
                   ClosingCredit = FormatDecimal(group.Sum(a => a.ClosingCredit) ?? 0, roundinvalue),
                   ClosingDebit = FormatDecimal(group.Sum(a => a.ClosingDebit) ?? 0, roundinvalue),
                   OpeningAmount = FormatDecimal(group.Sum(a => a.OpeningAmount) ?? 0, roundinvalue),
                   OpeningCredit = FormatDecimal(group.Sum(a => a.OpeningCredit) ?? 0, roundinvalue),
                   OpeningDebit = FormatDecimal(group.Sum(a => a.OpeningDebit) ?? 0, roundinvalue),
                   TransactionAmount = FormatDecimal(group.Sum(a => a.TransactionAmount) ?? 0, roundinvalue),
                   TransactionCredit = FormatDecimal(group.Sum(a => a.TransactionCredit) ?? 0, roundinvalue),
                   TransactionDebit = FormatDecimal(group.Sum(a => a.TransactionDebit) ?? 0, roundinvalue),
                   DifferenceAmount = FormatDecimal((group.Sum(a => a.ClosingAmount) - group.Sum(a => a.OpeningAmount)) ?? 0, roundinvalue),
                   DifferencePlusKPI = group.Sum(a => a.ClosingAmount) != 0 ?
                                     FormatDecimal(((group.Sum(a => a.ClosingAmount) - group.Sum(a => a.OpeningAmount)) / group.Sum(a => a.ClosingAmount)) * 100 ?? 0, roundinvalue) : FormatDecimal(0, roundinvalue),

               })
               .ToList();


            return groupedSums;


        }
        #endregion
        [HttpPost]
        public ActionResult SubmitMonthlyClosing(string dateFromString, string dateToString, int? companyID)
        {
            var message = "Saved sussessfully";
            bool isSuccess = true;
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            var dateFrom = DateTime.ParseExact(dateFromString, dateFormat, CultureInfo.InvariantCulture);

            var dateTo = DateTime.ParseExact(dateToString, dateFormat, CultureInfo.InvariantCulture);
            var viewModelList = new List<MCGridInventoryTransTypeViewModel>();

            var resp = ClientFactory.AccountingServiceClient(CallContext).SubmitMonthlyClosing(dateFrom, dateTo, companyID);
            if (resp == "1")
            {
                message = "Saved successfully!!";
                isSuccess = true;
            }
            else
            {
                message = "Error occured while saving!!";
                isSuccess = false;
            }

            dynamic response = new { IsSuccess = isSuccess, Message = message };
            return Json(response);
        }
        decimal? FormatDecimal(decimal? value, int decimalPlaces)
        {
            return value.HasValue
                ? Math.Round(value.Value, decimalPlaces)
                : (decimal?)0.00M;
        }
        string FormatForDisplay(decimal? value, int decimalPlaces)
        {
            return value.HasValue
                ? Math.Round(value.Value, decimalPlaces).ToString($"N{decimalPlaces}", CultureInfo.InvariantCulture)
                : $"0.{new string('0', decimalPlaces)}";
        }
        [HttpGet]
        public ActionResult GetMonthlyClosingDate(int? companyID)
        {
            var monthlyclosingViewmodel= new MonthlyClosingMainViewModel();
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");
            var branchID= companyID == 2 ? 30 : 10;
            var closingDate = ClientFactory.AccountingServiceClient(CallContext).GetMonthlyClosingDate(branchID);
            if (closingDate.HasValue)
            {
                monthlyclosingViewmodel.StartDate = new DateTime(closingDate.Value.Year, closingDate.Value.Month, 1).AddMonths(1);
                monthlyclosingViewmodel.StartDateString = monthlyclosingViewmodel.StartDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture);
               
            }
            return Json(new { Success = true, Data = monthlyclosingViewmodel });
           
        }
    }
}