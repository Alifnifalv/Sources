using Microsoft.AspNetCore.Mvc;
using Eduegate.Application.Mvc;
using Eduegate.Services.Client.Factory;
using Eduegate.Web.Library.Accounts.Assets;
using System.Globalization;
using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Domain.Accounts.Assets;

namespace Eduegate.ERP.Admin.Controllers
{
    public class AssetController : BaseController
    {
        public AssetController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetAssetDetailsByID(long assetID)
        {
            var result = ClientFactory.FixedAssetServiceClient(CallContext).GetAssetDetailsByID(assetID);

            return Json(result);
        }

        public JsonResult GetAssetsByCategoryID(long categoryID = 0)
        {
            var result = ClientFactory.FixedAssetServiceClient(CallContext).GetAssetsByCategoryID(categoryID);

            return Json(result);
        }

        public JsonResult GetAssetSerialMapsByAssetID(long assetID)
        {
            var result = ClientFactory.FixedAssetServiceClient(CallContext).GetAssetSerialMapsByAssetID(assetID);

            return Json(result);
        }

        public JsonResult CalculateAndFillDepreciationDetail(string assetListString = null, string categoryListString = null, int? month = null, int? year = null, long? branchID = null)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            var categoryKeyValueDTOs = new List<KeyValueDTO>();
            var assetKeyValueDTOs = new List<KeyValueDTO>();

            if (!string.IsNullOrEmpty(categoryListString))
            {
                categoryKeyValueDTOs = JsonConvert.DeserializeObject<List<KeyValueDTO>>(categoryListString);
            }

            if (!string.IsNullOrEmpty(assetListString))
            {
                assetKeyValueDTOs = JsonConvert.DeserializeObject<List<KeyValueDTO>>(assetListString);
            }

            var assetTransactionDTO = new AssetBL(CallContext).GetCalculatedDepreciationDetail(categoryKeyValueDTOs, assetKeyValueDTOs, month, year, branchID);

            var assetTransactionDetailVMs = new List<AssetDepreciationDetailViewModel>();
            foreach (var transactionDetail in assetTransactionDTO.AssetTransactionDetails)
            {
                var transactionSerialMapVMs = new List<AssetDepreciationDetailSerialMapViewModel>();
                foreach (var transactionSerialMap in transactionDetail.AssetTransactionSerialMaps)
                {
                    transactionSerialMapVMs.Add(new AssetDepreciationDetailSerialMapViewModel()
                    {
                        AssetTransactionSerialMapIID = transactionSerialMap.AssetTransactionSerialMapIID,
                        Quantity = transactionSerialMap.Quantity,
                        AssetID = transactionSerialMap.AssetID,
                        AssetSerialMapID = transactionSerialMap.AssetSerialMapID,
                        AssetSequenceCode = transactionSerialMap.AssetSequenceCode,
                        PreviousAcculatedDepreciationAmount = transactionSerialMap.PreviousAcculatedDepreciationAmount,
                        AccumulatedTillDateString = transactionSerialMap.DepAccumulatedTillDate.HasValue ? transactionSerialMap.DepAccumulatedTillDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                        DepFromDateString = transactionSerialMap.DepFromDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture),
                        DepToDateString = transactionSerialMap.DepToDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture),
                        AccountingPeriodDays = transactionSerialMap.AccountingPeriodDays,
                        DepAbovePeriod = transactionSerialMap.DepAbovePeriod,
                        BookedDepreciation = transactionSerialMap.BookedDepreciation,
                        AccumulatedDepreciationAmount = transactionSerialMap.AccumulatedDepreciationAmount,
                        NetValue = transactionSerialMap.NetValue,
                    });
                }

                assetTransactionDetailVMs.Add(new AssetDepreciationDetailViewModel()
                {
                    TransactionDetailIID = transactionDetail.DetailIID,
                    AssetID = transactionDetail.AssetID,
                    AssetCode = transactionDetail.AssetCode,
                    AssetDescription = transactionDetail.AssetName,
                    AssetGlAccID = transactionDetail.AssetGlAccID,
                    AccumulatedDepGLAccID = transactionDetail.AccumulatedDepGLAccID,
                    DepreciationExpGLAccID = transactionDetail.DepreciationExpGLAccID,
                    AssetCategoryID = transactionDetail.AssetCategoryID,
                    AssetCategory = transactionDetail.AssetCategoryName,
                    Quantity = transactionDetail.Quantity,
                    IsSerialNumberOnAssetEntry = transactionDetail.AssetTransactionSerialMaps.Count > 0 ? true : false,
                    AccumulatedTillDateString = transactionDetail.DepAccumulatedTillDate.HasValue ? transactionDetail.DepAccumulatedTillDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                    DepFromDateString = transactionDetail.DepFromDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture),
                    DepToDateString = transactionDetail.DepToDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture),
                    AccountingPeriodDays = transactionDetail.AccountingPeriodDays,
                    DepAbovePeriod = transactionDetail.DepAbovePeriod,
                    BookedDepreciation = transactionDetail.BookedDepreciation,
                    AccumulatedDepreciationAmount = transactionDetail.AccumulatedDepreciationAmount,
                    NetValue = transactionDetail.NetValue,
                    PreviousAcculatedDepreciationAmount = transactionDetail.PreviousAcculatedDepreciationAmount,
                    TransactionSerialMaps = transactionSerialMapVMs
                });
            }

            return Json(new { IsError = false, Response = assetTransactionDetailVMs });
        }

        public JsonResult DepreciationAccountPosting(long transactionHeadID, long? documentTypeID)
        {
            var result = false;
            if (transactionHeadID > 0)
            {
                var transDate = DateTime.Now;
                int type = documentTypeID.HasValue ? (int)documentTypeID : 205;
                var loginID = CallContext.LoginID.HasValue ? (int)CallContext.LoginID.Value : 0;

                result = ClientFactory.AccountingServiceClient(CallContext).AccountMergeWithMultipleTransactionIDs(transactionHeadID.ToString(), transDate, loginID, type);
            }

            return Json(result);
        }

        public JsonResult GetAssetSequencesByAssetID(long assetID)
        {
            var result = ClientFactory.FixedAssetServiceClient(CallContext).GetAssetSequencesByAssetID(assetID);

            return Json(new { IsError = false, Response = result });
        }

        public JsonResult GetAssetCategoryDetailsByID(long assetCategoryID)
        {
            var result = ClientFactory.FixedAssetServiceClient(CallContext).GetAssetCategoryDetailsByID(assetCategoryID);

            return Json(result);
        }

        public JsonResult GetAssetsByProductSKUID(long productSKUID)
        {
            var result = ClientFactory.FixedAssetServiceClient(CallContext).GetAssetsByProductSKUID(productSKUID);

            return Json(result);
        }

        public JsonResult GetAssetSerialMapsByAssetAndBranchID(long assetID, long branchID)
        {
            var result = ClientFactory.FixedAssetServiceClient(CallContext).GetAssetSerialMapsByAssetAndBranchID(assetID, branchID);

            return Json(result);
        }

    }
}