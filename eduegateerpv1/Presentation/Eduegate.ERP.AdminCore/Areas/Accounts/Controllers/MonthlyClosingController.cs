using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.Accounting.MonthlyClosing;
using Eduegate.Web.Library.ViewModels.AccountTransactions.MonthlyClosing;
using Eduegate.Application.Mvc;
using Eduegate.Domain;
using System.Collections.Generic;
using System;
using System.Linq;

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
        public ActionResult GetFeeGeneralMonthlyClosing(string dateFromString, string dateToString)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            var dateFrom = DateTime.ParseExact(dateFromString, dateFormat, CultureInfo.InvariantCulture);

            var dateTo = DateTime.ParseExact(dateToString, dateFormat, CultureInfo.InvariantCulture);
            var viewModelList = new List<MCGridFeeDueFeeTypeViewModel>();

            var entry = ClientFactory.AccountingServiceClient(CallContext).GetFeeGeneralMonthlyClosing(dateFrom, dateTo);



            viewModelList = GetFeeGeneralFeeTypeDetails(entry);
            var jsonResult = Json(viewModelList);

            //TODO: Required Check
            //jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;

        }
        private List<MCGridFeeDueFeeTypeViewModel> GetFeeGeneralFeeTypeDetails(List<FeeGeneralMonthlyClosingDTO> listData)
        {
            //var feeDuedetails = new List<MCGridFeeDueFeeTypeViewModel>();

            var feeDuedetails = listData
           .GroupBy(a => new { a.FeeTypeID, a.FeeTypeName })
           .Select(group => new MCGridFeeDueFeeTypeViewModel
           {
               FeeTypeID = group.Key.FeeTypeID,
               FeeTypeName = group.Key.FeeTypeName,
               ClosingAmount = group.Sum(a => a.ClosingAmount),
               ClosingCredit = group.Sum(a => a.ClosingCredit),
               ClosingDebit = group.Sum(a => a.ClosingDebit),
               OpeningAmount = group.Sum(a => a.OpeningAmount),
               OpeningCredit = group.Sum(a => a.OpeningCredit),
               OpeningDebit = group.Sum(a => a.OpeningDebit),
               TransactionAmount = group.Sum(a => a.TransactionAmount),
               TransactionCredit = group.Sum(a => a.TransactionCredit),
               TransactionDebit = group.Sum(a => a.TransactionDebit),
               DifferenceAmount = (group.Sum(a => a.ClosingAmount) - group.Sum(a => a.OpeningAmount)),
               DifferencePlusKPI = group.Sum(a => a.ClosingAmount) != 0 ?
                                 ((group.Sum(a => a.ClosingAmount) - group.Sum(a => a.OpeningAmount)) / group.Sum(a => a.ClosingAmount)) * 100 : null,
               MCGridFeeDueFeeCycle = GetFeeGeneralFeeCycles(listData.Where(x => x.FeeTypeID == group.Key.FeeTypeID).ToList())
           })
           .ToList();

            return feeDuedetails;
        }


        private List<MCGridFeeDueFeeCycleViewModel> GetFeeGeneralFeeCycles(List<FeeGeneralMonthlyClosingDTO> listData)
        {

            var feeDueFeeCycleDetail = listData
          .GroupBy(a => new { a.FeeCycleID, a.FeeCycleName })
          .Select(group => new MCGridFeeDueFeeCycleViewModel
          {
              FeeCycleID = group.Key.FeeCycleID,
              FeeCycleName = group.Key.FeeCycleName,
              ClosingAmount = group.Sum(a => a.ClosingAmount),
              ClosingCredit = group.Sum(a => a.ClosingCredit),
              ClosingDebit = group.Sum(a => a.ClosingDebit),
              OpeningAmount = group.Sum(a => a.OpeningAmount),
              OpeningCredit = group.Sum(a => a.OpeningCredit),
              OpeningDebit = group.Sum(a => a.OpeningDebit),
              TransactionAmount = group.Sum(a => a.TransactionAmount),
              TransactionCredit = group.Sum(a => a.TransactionCredit),
              TransactionDebit = group.Sum(a => a.TransactionDebit),
              DifferenceAmount = (group.Sum(a => a.ClosingAmount) - group.Sum(a => a.OpeningAmount)),
              DifferencePlusKPI = group.Sum(a => a.ClosingAmount) != 0 ?
                                ((group.Sum(a => a.ClosingAmount) - group.Sum(a => a.OpeningAmount)) / group.Sum(a => a.ClosingAmount)) * 100 : null,
              MCGridFeeDueFeeMaster = GetFeeGeneralFeeMasters(listData.Where(x => x.FeeCycleID == group.Key.FeeCycleID).ToList())
          })
          .ToList();

            return feeDueFeeCycleDetail;
        }

        private List<MCGridFeeDueFeeMasterViewModel> GetFeeGeneralFeeMasters(List<FeeGeneralMonthlyClosingDTO> listData)
        {
            //List<MCGridFeeDueFeeMasterViewModel> feeDueFeemasterDetail = new List<MCGridFeeDueFeeMasterViewModel>();

            var groupedSums = listData
       .GroupBy(a => new { a.FeeMasterID, a.FeeMasterName })
       .Select(group => new MCGridFeeDueFeeMasterViewModel
       {
           FeeMasterID = group.Key.FeeMasterID,
           FeeMasterName = group.Key.FeeMasterName,
           ClosingAmount = group.Sum(a => a.ClosingAmount),
           ClosingCredit = group.Sum(a => a.ClosingCredit),
           ClosingDebit = group.Sum(a => a.ClosingDebit),
           OpeningAmount = group.Sum(a => a.OpeningAmount),
           OpeningCredit = group.Sum(a => a.OpeningCredit),
           OpeningDebit = group.Sum(a => a.OpeningDebit),
           TransactionAmount = group.Sum(a => a.TransactionAmount),
           TransactionCredit = group.Sum(a => a.TransactionCredit),
           TransactionDebit = group.Sum(a => a.TransactionDebit),
           DifferenceAmount = (group.Sum(a => a.ClosingAmount) - group.Sum(a => a.OpeningAmount)),
           DifferencePlusKPI = group.Sum(a => a.ClosingAmount) != 0 ?
                             ((group.Sum(a => a.ClosingAmount) - group.Sum(a => a.OpeningAmount)) / group.Sum(a => a.ClosingAmount)) * 100 : null,

       })
       .ToList();


            return groupedSums;


        }

        #endregion  Fee General Tab

        #region 2.Fee Account Compare Tab

        [HttpGet]
        public ActionResult GetFeeAccountCompareMonthlyClosing(string dateFromString, string dateToString)
        {

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            var dateFrom = DateTime.ParseExact(dateFromString, dateFormat, CultureInfo.InvariantCulture);

            var dateTo = DateTime.ParseExact(dateToString, dateFormat, CultureInfo.InvariantCulture);
            var viewModelList = new List<MCGridFeeVSAccountsFeeTypelViewModel>();

            var entry = ClientFactory.AccountingServiceClient(CallContext).GetFeeAccountCompareMonthlyClosing(dateFrom, dateTo);
            viewModelList = GetFeeVSAccountsFeeTypes(entry);
            var jsonResult = Json(viewModelList);

            //TODO: Required Check
            //jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        private List<MCGridFeeVSAccountsFeeTypelViewModel> GetFeeVSAccountsFeeTypes(List<FeeGeneralMonthlyClosingDTO> listData)
        {

            var feeDuedetails = listData
           .GroupBy(a => new { a.FeeTypeID, a.FeeTypeName })
           .Select(group => new MCGridFeeVSAccountsFeeTypelViewModel
           {
               FeeTypeID = group.Key.FeeTypeID,
               FeeTypeName = group.Key.FeeTypeName,
               FeeAmount = group.Sum(a => a.FeeAmount),
               AccountAmount = group.Sum(a => a.AccountAmount),
               DifferenceAmount = (group.Sum(a => a.FeeAmount) - group.Sum(a => a.AccountAmount)),
               DifferencePlusKPI = group.Sum(a => a.AccountAmount) != 0 ?
                             ((group.Sum(a => a.FeeAmount) - group.Sum(a => a.AccountAmount)) / group.Sum(a => a.AccountAmount)) * 100 : null,

               MCGridFeeVSAccountsFeeCycle = GetFeeVSAccountsFeeCycles(listData.Where(x => x.FeeTypeID == group.Key.FeeTypeID).ToList())
           })
           .ToList();

            return feeDuedetails;
        }

        private List<MCGridFeeVSAccountsFeeCycleViewModel> GetFeeVSAccountsFeeCycles(List<FeeGeneralMonthlyClosingDTO> listData)
        {

            var feeDueFeeCycleDetail = listData
          .GroupBy(a => new { a.FeeCycleID, a.FeeCycleName })
          .Select(group => new MCGridFeeVSAccountsFeeCycleViewModel
          {
              FeeCycleID = group.Key.FeeCycleID,
              FeeCycleName = group.Key.FeeCycleName,
              FeeAmount = group.Sum(a => a.FeeAmount),
              AccountAmount = group.Sum(a => a.AccountAmount),
              DifferenceAmount = (group.Sum(a => a.FeeAmount) - group.Sum(a => a.AccountAmount)),
              DifferencePlusKPI = group.Sum(a => a.AccountAmount) != 0 ?
                             ((group.Sum(a => a.FeeAmount) - group.Sum(a => a.AccountAmount)) / group.Sum(a => a.AccountAmount)) * 100 : null,

              MCGridFeeVSAccountsFeeMaster = GetFeeVSAccountsFeeMasters(listData.Where(x => x.FeeCycleID == group.Key.FeeCycleID).ToList())
          })
          .ToList();

            return feeDueFeeCycleDetail;
        }

        private List<MCGridFeeVSAccountsFeeMasterViewModel> GetFeeVSAccountsFeeMasters(List<FeeGeneralMonthlyClosingDTO> listData)
        {

            var groupedSums = listData
       .GroupBy(a => new { a.FeeMasterID, a.FeeMasterName })
       .Select(group => new MCGridFeeVSAccountsFeeMasterViewModel
       {
           FeeMasterID = group.Key.FeeMasterID,
           FeeMasterName = group.Key.FeeMasterName,
           FeeAmount = group.Sum(a => a.FeeAmount),
           AccountAmount = group.Sum(a => a.AccountAmount),
           DifferenceAmount = (group.Sum(a => a.FeeAmount) - group.Sum(a => a.AccountAmount)),
           DifferencePlusKPI = group.Sum(a => a.AccountAmount) != 0 ?
                             ((group.Sum(a => a.FeeAmount) - group.Sum(a => a.AccountAmount)) / group.Sum(a => a.AccountAmount)) * 100 : null,

       })
       .ToList();


            return groupedSums;


        }

        #endregion Fee Account Compare Tab

        #region Inventory General Tab

        [HttpGet]
        public ActionResult GetInventoryGeneralMonthlyClosing(string dateFromString, string dateToString)
        {

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            var dateFrom = DateTime.ParseExact(dateFromString, dateFormat, CultureInfo.InvariantCulture);

            var dateTo = DateTime.ParseExact(dateToString, dateFormat, CultureInfo.InvariantCulture);
            var viewModelList = new List<MCGridInventoryTransTypeViewModel>();

            var entry = ClientFactory.AccountingServiceClient(CallContext).GetInventoryGeneralMonthlyClosing(dateFrom, dateTo);
            viewModelList = GetInventoryTransactionTypes(entry);
            var jsonResult = Json(viewModelList);

            //TODO: Required Check
            //jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        private List<MCGridInventoryTransTypeViewModel> GetInventoryTransactionTypes(List<InvoiceGeneralMonthlyClosingDTO> listData)
        {

            var feeDuedetails = listData
           .GroupBy(a => new { a.TransactionTypeID, a.TransactionTypeName })
           .Select(group => new MCGridInventoryTransTypeViewModel
           {
               TransactionTypeID = group.Key.TransactionTypeID,
               TransactionTypeName = group.Key.TransactionTypeName,
               ClosingAmount = group.Sum(a => a.ClosingAmount),
               ClosingCredit = group.Sum(a => a.ClosingCredit),
               ClosingDebit = group.Sum(a => a.ClosingDebit),
               OpeningAmount = group.Sum(a => a.OpeningAmount),
               OpeningCredit = group.Sum(a => a.OpeningCredit),
               OpeningDebit = group.Sum(a => a.OpeningDebit),
               TransactionAmount = group.Sum(a => a.TransactionAmount),
               TransactionCredit = group.Sum(a => a.TransactionCredit),
               TransactionDebit = group.Sum(a => a.TransactionDebit),
               DifferenceAmount = (group.Sum(a => a.ClosingAmount) - group.Sum(a => a.OpeningAmount)),
               DifferencePlusKPI = group.Sum(a => a.ClosingAmount) != 0 ?
                                ((group.Sum(a => a.ClosingAmount) - group.Sum(a => a.OpeningAmount)) / group.Sum(a => a.ClosingAmount)) * 100 : null,

               MCGridInventoryDocumentType = GetInventoryDocumentTypes(listData.Where(x => x.TransactionTypeID == group.Key.TransactionTypeID).ToList())
           })
           .ToList();

            return feeDuedetails;
        }

        private List<MCGridInventoryDocumentTypeViewModel> GetInventoryDocumentTypes(List<InvoiceGeneralMonthlyClosingDTO> listData)
        {

            var feeDueFeeCycleDetail = listData
          .GroupBy(a => new { a.DocumentTypeID, a.DocumentTypeName })
          .Select(group => new MCGridInventoryDocumentTypeViewModel
          {
              DocumentTypeID = group.Key.DocumentTypeID,
              DocumentTypeName = group.Key.DocumentTypeName,
              ClosingAmount = group.Sum(a => a.ClosingAmount),
              ClosingCredit = group.Sum(a => a.ClosingCredit),
              ClosingDebit = group.Sum(a => a.ClosingDebit),
              OpeningAmount = group.Sum(a => a.OpeningAmount),
              OpeningCredit = group.Sum(a => a.OpeningCredit),
              OpeningDebit = group.Sum(a => a.OpeningDebit),
              TransactionAmount = group.Sum(a => a.TransactionAmount),
              TransactionCredit = group.Sum(a => a.TransactionCredit),
              TransactionDebit = group.Sum(a => a.TransactionDebit),
              DifferenceAmount = (group.Sum(a => a.ClosingAmount) - group.Sum(a => a.OpeningAmount)),
              DifferencePlusKPI = group.Sum(a => a.ClosingAmount) != 0 ?
                                ((group.Sum(a => a.ClosingAmount) - group.Sum(a => a.OpeningAmount)) / group.Sum(a => a.ClosingAmount)) * 100 : null,

          })
          .ToList();

            return feeDueFeeCycleDetail;
        }

        #endregion Inventory General Tab

        #region 4.Stock Tab

        [HttpGet]
        public ActionResult GetStockMonthlyClosing(string dateFromString, string dateToString)
        {

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            var dateFrom = DateTime.ParseExact(dateFromString, dateFormat, CultureInfo.InvariantCulture);

            var dateTo = DateTime.ParseExact(dateToString, dateFormat, CultureInfo.InvariantCulture);
            var viewModelList = new List<MCGridStockTypeViewModel>();

            var entry = ClientFactory.AccountingServiceClient(CallContext).GetStockMonthlyClosing(dateFrom, dateTo);
            viewModelList = GetStockTypes(entry);
            var jsonResult = Json(viewModelList);

            //TODO: Required Check
            //jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        private List<MCGridStockTypeViewModel> GetStockTypes(List<StockMonthlyClosingDTO> listData)
        {

            var feeDuedetails = listData
           .GroupBy(a => new { a.TypeID, a.TypeName })
           .Select(group => new MCGridStockTypeViewModel
           {
               TypeID = group.Key.TypeID,
               TypeName = group.Key.TypeName,
               ClosingAmount = group.Sum(a => a.ClosingAmount),
               ClosingCredit = group.Sum(a => a.ClosingCredit),
               ClosingDebit = group.Sum(a => a.ClosingDebit),
               OpeningAmount = group.Sum(a => a.OpeningAmount),
               OpeningCredit = group.Sum(a => a.OpeningCredit),
               OpeningDebit = group.Sum(a => a.OpeningDebit),
               TransactionAmount = group.Sum(a => a.TransactionAmount),
               TransactionCredit = group.Sum(a => a.TransactionCredit),
               TransactionDebit = group.Sum(a => a.TransactionDebit),
               DifferenceAmount = (group.Sum(a => a.ClosingAmount) - group.Sum(a => a.OpeningAmount)),
               DifferencePlusKPI = group.Sum(a => a.ClosingAmount) != 0 ?
                                ((group.Sum(a => a.ClosingAmount) - group.Sum(a => a.OpeningAmount)) / group.Sum(a => a.ClosingAmount)) * 100 : null,

               MCGridStockAccount = GetStockAccounts(listData.Where(x => x.TypeID == group.Key.TypeID).ToList())
           })
           .ToList();

            return feeDuedetails;
        }

        private List<MCGridStockAccountViewModel> GetStockAccounts(List<StockMonthlyClosingDTO> listData)
        {

            var details = listData
          .GroupBy(a => new { a.AccountID, a.AccountName })
          .Select(group => new MCGridStockAccountViewModel
          {
              AccountID = group.Key.AccountID,
              AccountName = group.Key.AccountName,
              ClosingAmount = Decimal.Round(group.Sum(a => a.ClosingAmount.Value), 3),
              ClosingCredit = Decimal.Round(group.Sum(a => a.ClosingCredit.Value), 3),
              ClosingDebit = Decimal.Round(group.Sum(a => a.ClosingDebit.Value), 3),
              OpeningAmount = Decimal.Round(group.Sum(a => a.OpeningAmount.Value), 3),
              OpeningCredit = Decimal.Round(group.Sum(a => a.OpeningCredit.Value), 3),
              OpeningDebit = Decimal.Round(group.Sum(a => a.OpeningDebit.Value), 3),
              TransactionAmount = Decimal.Round(group.Sum(a => a.TransactionAmount.Value), 3),
              TransactionCredit = Decimal.Round(group.Sum(a => a.TransactionCredit.Value), 3),
              TransactionDebit = Decimal.Round(group.Sum(a => a.TransactionDebit.Value), 3),
              DifferenceAmount = Decimal.Round(group.Sum(a => a.ClosingAmount.Value) - group.Sum(a => a.OpeningAmount.Value), 3),
              DifferencePlusKPI = group.Sum(a => a.ClosingAmount) != 0 ?
                                ((group.Sum(a => a.ClosingAmount) - group.Sum(a => a.OpeningAmount)) / group.Sum(a => a.ClosingAmount)) * 100
                                : null,
              //DifferencePlusKPI = group.Sum(a => a.ClosingAmount) != 0 ?
              //                  Decimal.Round(((group.Sum(a => a.ClosingAmount.Value) - group.Sum(a => a.OpeningAmount.Value)) 
              //                  / group.Sum(a => a.ClosingAmount.Value)) * 100), 3) : null,

          })
          .ToList();

            return details;
        }

        #endregion Inventory General Tab

        #region Fee Cancel Tab

        [HttpGet]
        public JsonResult GetFeeCancelMonthlyClosing(string dateFromString, string dateToString)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            var dateFrom = DateTime.ParseExact(dateFromString, dateFormat, CultureInfo.InvariantCulture);

            var dateTo = DateTime.ParseExact(dateToString, dateFormat, CultureInfo.InvariantCulture);
            var viewModelList = new List<MCGridFeeCancelFeeTypeViewModel>();

            var entry = ClientFactory.AccountingServiceClient(CallContext).GetFeeCancelMonthlyClosing(dateFrom, dateTo);
            viewModelList = GetFeeCancelFeeTypeDetails(entry);
            var jsonResult = Json(viewModelList);

            //TODO: Required Check
            //jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        private List<MCGridFeeCancelFeeTypeViewModel> GetFeeCancelFeeTypeDetails(List<FeeGeneralMonthlyClosingDTO> listData)
        {

            var feeDuedetails = listData
           .GroupBy(a => new { a.FeeTypeID, a.FeeTypeName })
           .Select(group => new MCGridFeeCancelFeeTypeViewModel
           {
               FeeTypeID = group.Key.FeeTypeID,
               FeeTypeName = group.Key.FeeTypeName,
               ClosingAmount = group.Sum(a => a.ClosingAmount),
               ClosingCredit = group.Sum(a => a.ClosingCredit),
               ClosingDebit = group.Sum(a => a.ClosingDebit),
               OpeningAmount = group.Sum(a => a.OpeningAmount),
               OpeningCredit = group.Sum(a => a.OpeningCredit),
               OpeningDebit = group.Sum(a => a.OpeningDebit),
               TransactionAmount = group.Sum(a => a.TransactionAmount),
               TransactionCredit = group.Sum(a => a.TransactionCredit),
               TransactionDebit = group.Sum(a => a.TransactionDebit),
               DifferenceAmount = (group.Sum(a => a.ClosingAmount) - group.Sum(a => a.OpeningAmount)),
               DifferencePlusKPI = group.Sum(a => a.ClosingAmount) != 0 ?
                                 ((group.Sum(a => a.ClosingAmount) - group.Sum(a => a.OpeningAmount)) / group.Sum(a => a.ClosingAmount)) * 100 : null,
               MCGridFeeDueFeeCycle = GetFeeGeneralFeeCycles(listData.Where(x => x.FeeTypeID == group.Key.FeeTypeID).ToList())
           })
           .ToList();

            return feeDuedetails;
        }

        #endregion Fee Cancel Tab

        #region Account Cancel Tab

        [HttpGet]
        public ActionResult GetAccountCancelMonthlyClosing(string dateFromString, string dateToString)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            var dateFrom = DateTime.ParseExact(dateFromString, dateFormat, CultureInfo.InvariantCulture);

            var dateTo = DateTime.ParseExact(dateToString, dateFormat, CultureInfo.InvariantCulture);
            var viewModelList = new List<MCGridAccountCancelTransType>();

            var entry = ClientFactory.AccountingServiceClient(CallContext).GetAccountCancelMonthlyClosing(dateFrom, dateTo);
            viewModelList = GetAccountCancelFeeTypeDetails(entry);
            var jsonResult = Json(viewModelList);

            //TODO: Required Check
            //jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        private List<MCGridAccountCancelTransType> GetAccountCancelFeeTypeDetails(List<AccountsMonthlyClosingDTO> listData)
        {

            var transTypes = listData
           .GroupBy(a => new { a.TransactionTypeID, a.TransactionTypeName })
           .Select(group => new MCGridAccountCancelTransType
           {
               TransactionTypeID = group.Key.TransactionTypeID,
               TransactionTypeName = group.Key.TransactionTypeName,
               ClosingAmount = Decimal.Round(group.Sum(a => a.ClosingAmount.Value), 3),
               ClosingCredit = Decimal.Round(group.Sum(a => a.ClosingCredit.Value), 3),
               ClosingDebit = Decimal.Round(group.Sum(a => a.ClosingDebit.Value), 3),
               OpeningAmount = Decimal.Round(group.Sum(a => a.OpeningAmount.Value), 3),
               OpeningCredit = Decimal.Round(group.Sum(a => a.OpeningCredit.Value), 3),
               OpeningDebit = Decimal.Round(group.Sum(a => a.OpeningDebit.Value), 3),
               TransactionAmount = Decimal.Round(group.Sum(a => a.TransactionAmount.Value), 3),
               TransactionCredit = Decimal.Round(group.Sum(a => a.TransactionCredit.Value), 3),
               TransactionDebit = Decimal.Round(group.Sum(a => a.TransactionDebit.Value), 3),
               DifferenceAmount = (group.Sum(a => a.ClosingAmount) - group.Sum(a => a.OpeningAmount)),
               DifferencePlusKPI = group.Sum(a => a.ClosingAmount) != 0 ?
                                ((group.Sum(a => a.ClosingAmount) - group.Sum(a => a.OpeningAmount)) / group.Sum(a => a.ClosingAmount)) * 100
                                : null,

               MCGridInventoryDocumentType = GetInventoryDocumentTypes(listData.Where(x => x.TransactionTypeID == group.Key.TransactionTypeID).ToList())
           })
           .ToList();

            return transTypes;
        }

        private List<MCGridInventoryDocumentTypeViewModel> GetInventoryDocumentTypes(List<AccountsMonthlyClosingDTO> listData)
        {

            var inventoryDocumentTypes = listData
          .GroupBy(a => new { a.DocumentTypeID, a.DocumentTypeName })
          .Select(group => new MCGridInventoryDocumentTypeViewModel
          {
              DocumentTypeID = group.Key.DocumentTypeID,
              DocumentTypeName = group.Key.DocumentTypeName,
              ClosingAmount = group.Sum(a => a.ClosingAmount),
              ClosingCredit = group.Sum(a => a.ClosingCredit),
              ClosingDebit = group.Sum(a => a.ClosingDebit),
              OpeningAmount = group.Sum(a => a.OpeningAmount),
              OpeningCredit = group.Sum(a => a.OpeningCredit),
              OpeningDebit = group.Sum(a => a.OpeningDebit),
              TransactionAmount = group.Sum(a => a.TransactionAmount),
              TransactionCredit = group.Sum(a => a.TransactionCredit),
              TransactionDebit = group.Sum(a => a.TransactionDebit),
              DifferenceAmount = (group.Sum(a => a.ClosingAmount) - group.Sum(a => a.OpeningAmount)),
              DifferencePlusKPI = group.Sum(a => a.ClosingAmount) != 0 ?
                                ((group.Sum(a => a.ClosingAmount) - group.Sum(a => a.OpeningAmount)) / group.Sum(a => a.ClosingAmount)) * 100 : null,

          })
          .ToList();

            return inventoryDocumentTypes;
        }
        #endregion Account Cancel Tab

        #region Inventory Cancel Tab

        [HttpGet]
        public JsonResult GetInventoryCancelMonthlyClosing(string dateFromString, string dateToString)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            var dateFrom = DateTime.ParseExact(dateFromString, dateFormat, CultureInfo.InvariantCulture);

            var dateTo = DateTime.ParseExact(dateToString, dateFormat, CultureInfo.InvariantCulture);
            var viewModelList = new List<MCGridInventoryCancelViewModel>();

            var entry = ClientFactory.AccountingServiceClient(CallContext).GetInventoryCancelMonthlyClosing(dateFrom, dateTo);
            viewModelList = GetInventoryCancelTransactionTypes(entry);
            var jsonResult = Json(viewModelList);

            //TODO: Required Check
            //jsonResult.MaxJsonLength = int.MaxValue;


            return jsonResult;
        }
        private List<MCGridInventoryCancelViewModel> GetInventoryCancelTransactionTypes(List<InvoiceGeneralMonthlyClosingDTO> listData)
        {

            var transactionTypes = listData
           .GroupBy(a => new { a.TransactionTypeID, a.TransactionTypeName })
           .Select(group => new MCGridInventoryCancelViewModel
           {
               TransactionTypeID = group.Key.TransactionTypeID,
               TransactionTypeName = group.Key.TransactionTypeName,
               ClosingAmount = group.Sum(a => a.ClosingAmount),
               ClosingCredit = group.Sum(a => a.ClosingCredit),
               ClosingDebit = group.Sum(a => a.ClosingDebit),
               OpeningAmount = group.Sum(a => a.OpeningAmount),
               OpeningCredit = group.Sum(a => a.OpeningCredit),
               OpeningDebit = group.Sum(a => a.OpeningDebit),
               TransactionAmount = group.Sum(a => a.TransactionAmount),
               TransactionCredit = group.Sum(a => a.TransactionCredit),
               TransactionDebit = group.Sum(a => a.TransactionDebit),
               DifferenceAmount = (group.Sum(a => a.ClosingAmount) - group.Sum(a => a.OpeningAmount)),
               DifferencePlusKPI = group.Sum(a => a.ClosingAmount) != 0 ?
                                ((group.Sum(a => a.ClosingAmount) - group.Sum(a => a.OpeningAmount)) / group.Sum(a => a.ClosingAmount)) * 100 : null,

               MCGridInventoryDocumentType = GetInventoryDocumentTypes(listData.Where(x => x.TransactionTypeID == group.Key.TransactionTypeID).ToList())
           })
           .ToList();

            return transactionTypes;
        }
        #endregion Inventory Cancel Tab

        #region 8.Mismatched Fees Tab

        [HttpGet]
        public ActionResult GetFeeMismatchedMonthlyClosing(string dateFromString, string dateToString)
        {

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            var dateFrom = DateTime.ParseExact(dateFromString, dateFormat, CultureInfo.InvariantCulture);

            var dateTo = DateTime.ParseExact(dateToString, dateFormat, CultureInfo.InvariantCulture);
            var viewModelList = new List<MCGridMismatchFeesViewModel>();

            var entry = ClientFactory.AccountingServiceClient(CallContext).GetFeeMismatchedMonthlyClosing(dateFrom, dateTo);
            viewModelList = GetFeeMismatchedMonthlyClosing(entry);
            var jsonResult = Json(viewModelList);

            //TODO: Required Check
            //jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;

        }

        private List<MCGridMismatchFeesViewModel> GetFeeMismatchedMonthlyClosing(List<FeeMismatchedMonthlyClosingDTO> listData)
        {
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
        public ActionResult GetAccountsMismatchedMonthlyClosing(string dateFromString, string dateToString)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            var dateFrom = DateTime.ParseExact(dateFromString, dateFormat, CultureInfo.InvariantCulture);

            var dateTo = DateTime.ParseExact(dateToString, dateFormat, CultureInfo.InvariantCulture);
            var viewModelList = new List<MCGridMismatchAccountsViewModel>();

            var entry = ClientFactory.AccountingServiceClient(CallContext).GetAccountsMismatchedMonthlyClosing(dateFrom, dateTo);
            viewModelList = GetAccountMismatchedMonthlyClosing(entry);

            var jsonResult = Json(viewModelList);

            //TODO: Required Check
            //jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        private List<MCGridMismatchAccountsViewModel> GetAccountMismatchedMonthlyClosing(List<AccountMismatchedMonthlyClosingDTO> listData)
        {
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
               TranDateString = (group.Key.TranDate.HasValue ? group.Key.TranDate.Value : DateTime.Now).ToString(dateFormat, CultureInfo.InvariantCulture),

           })
           .ToList();

            return accountDetails;
        }
        #endregion

        #region 10.Account General Tab
        [HttpGet]
        public ActionResult GetAccountsGeneralMonthlyClosing(string dateFromString, string dateToString)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            var dateFrom = DateTime.ParseExact(dateFromString, dateFormat, CultureInfo.InvariantCulture);

            var dateTo = DateTime.ParseExact(dateToString, dateFormat, CultureInfo.InvariantCulture);
            var viewModelList = new List<MCGridAccountGeneralMainGroupViewModel>();

            var entry = ClientFactory.AccountingServiceClient(CallContext).GetAccountsGeneralMonthlyClosing(dateFrom, dateTo);
            viewModelList = GetAccountsGeneralMainGroups(entry);

            var jsonResult = Json(viewModelList);

            //TODO: Required Check
            //jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;

        }
        private List<MCGridAccountGeneralMainGroupViewModel> GetAccountsGeneralMainGroups(List<AccountsGeneralMonthlyClosingDTO> listData)
        {

            var feeDuedetails = listData
           .GroupBy(a => new { a.MainGroupID, a.MainGroupName })
           .Select(group => new MCGridAccountGeneralMainGroupViewModel
           {
               MainGroupID = group.Key.MainGroupID,
               MainGroupName = group.Key.MainGroupName,
               ClosingAmount = group.Sum(a => a.ClosingAmount),
               ClosingCredit = group.Sum(a => a.ClosingCredit),
               ClosingDebit = group.Sum(a => a.ClosingDebit),
               OpeningAmount = group.Sum(a => a.OpeningAmount),
               OpeningCredit = group.Sum(a => a.OpeningCredit),
               OpeningDebit = group.Sum(a => a.OpeningDebit),
               TransactionAmount = group.Sum(a => a.TransactionAmount),
               TransactionCredit = group.Sum(a => a.TransactionCredit),
               TransactionDebit = group.Sum(a => a.TransactionDebit),
               DifferenceAmount = (group.Sum(a => a.ClosingAmount) - group.Sum(a => a.OpeningAmount)),
               DifferencePlusKPI = group.Sum(a => a.ClosingAmount) != 0 ?
                                 ((group.Sum(a => a.ClosingAmount) - group.Sum(a => a.OpeningAmount)) / group.Sum(a => a.ClosingAmount)) * 100 : null,
               MCGridAccountGeneralSubGroup = GetAccountGeneralSubGroups(listData.Where(x => x.MainGroupID == group.Key.MainGroupID).ToList())
           })
           .ToList();

            return feeDuedetails;
        }


        private List<MCGridAccountGeneralSubGroupViewModel> GetAccountGeneralSubGroups(List<AccountsGeneralMonthlyClosingDTO> listData)
        {

            var feeDueFeeCycleDetail = listData
          .GroupBy(a => new { a.SubGroupID, a.SubGroupName })
          .Select(group => new MCGridAccountGeneralSubGroupViewModel
          {
              SubGroupID = group.Key.SubGroupID,
              SubGroupName = group.Key.SubGroupName,
              ClosingAmount = group.Sum(a => a.ClosingAmount),
              ClosingCredit = group.Sum(a => a.ClosingCredit),
              ClosingDebit = group.Sum(a => a.ClosingDebit),
              OpeningAmount = group.Sum(a => a.OpeningAmount),
              OpeningCredit = group.Sum(a => a.OpeningCredit),
              OpeningDebit = group.Sum(a => a.OpeningDebit),
              TransactionAmount = group.Sum(a => a.TransactionAmount),
              TransactionCredit = group.Sum(a => a.TransactionCredit),
              TransactionDebit = group.Sum(a => a.TransactionDebit),
              DifferenceAmount = (group.Sum(a => a.ClosingAmount) - group.Sum(a => a.OpeningAmount)),
              DifferencePlusKPI = group.Sum(a => a.ClosingAmount) != 0 ?
                                ((group.Sum(a => a.ClosingAmount) - group.Sum(a => a.OpeningAmount)) / group.Sum(a => a.ClosingAmount)) * 100 : null,
              MCGridAccountGeneralGroup = GetAccountsGeneralGroups(listData.Where(x => x.SubGroupID == group.Key.SubGroupID).ToList())
          })
          .ToList();

            return feeDueFeeCycleDetail;
        }

        private List<MCGridAccountGeneralGroupViewModel> GetAccountsGeneralGroups(List<AccountsGeneralMonthlyClosingDTO> listData)
        {

            var groupedSums = listData
               .GroupBy(a => new { a.GroupID, a.GroupName })
               .Select(group => new MCGridAccountGeneralGroupViewModel
               {
                   GroupID = group.Key.GroupID,
                   GroupName = group.Key.GroupName,
                   ClosingAmount = group.Sum(a => a.ClosingAmount),
                   ClosingCredit = group.Sum(a => a.ClosingCredit),
                   ClosingDebit = group.Sum(a => a.ClosingDebit),
                   OpeningAmount = group.Sum(a => a.OpeningAmount),
                   OpeningCredit = group.Sum(a => a.OpeningCredit),
                   OpeningDebit = group.Sum(a => a.OpeningDebit),
                   TransactionAmount = group.Sum(a => a.TransactionAmount),
                   TransactionCredit = group.Sum(a => a.TransactionCredit),
                   TransactionDebit = group.Sum(a => a.TransactionDebit),
                   DifferenceAmount = (group.Sum(a => a.ClosingAmount) - group.Sum(a => a.OpeningAmount)),
                   DifferencePlusKPI = group.Sum(a => a.ClosingAmount) != 0 ?
                                     ((group.Sum(a => a.ClosingAmount) - group.Sum(a => a.OpeningAmount)) / group.Sum(a => a.ClosingAmount)) * 100 : null,

               })
               .ToList();


            return groupedSums;


        }
        #endregion
        [HttpPost]
        public ActionResult SubmitMonthlyClosing(string dateFromString, string dateToString)
        {
            var message = "Saved sussessfully";
            bool isSuccess = true;
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            var dateFrom = DateTime.ParseExact(dateFromString, dateFormat, CultureInfo.InvariantCulture);

            var dateTo = DateTime.ParseExact(dateToString, dateFormat, CultureInfo.InvariantCulture);
            var viewModelList = new List<MCGridInventoryTransTypeViewModel>();

            var resp = ClientFactory.AccountingServiceClient(CallContext).SubmitMonthlyClosing(dateFrom, dateTo);
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
    }
}