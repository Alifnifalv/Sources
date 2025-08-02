using Eduegate.ERP.Admin.Controllers;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Helper;
//using Eduegate.Framework.Web.Library.Common;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.Settings;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Web.Library.ViewModels.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Eduegate.ERP.Admin.Areas.Inventories.Controllers
{
    public class PointOfSaleController : BaseSearchController
    {
        private string dateTimeFormat = ConfigurationExtensions.GetAppConfigValue("DateTimeFormat");

        // GET: Inventories/PointOfSale
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult List()
        {
            var metadata = ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Infrastructure.Enums.SearchView.PurchaseInvoice);
            return View(new SearchListViewModel
            {
                ControllerName = "Inventories/" + Infrastructure.Enums.SearchView.PointOfSale,
                ViewName = Infrastructure.Enums.SearchView.PointOfSale,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                IsCommentLink = true,
                HasFilters = metadata.HasFilters,
                FilterColumns = metadata.QuickFilterColumns,
                //IsMultilineEnabled = metadata.MultilineEnabled,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                InfoBar = @"<li class='status-label-mobile'>
                                        <div class='right status-label'>
                                            <div class='status-label-color'><label class='status-color-label  green'></label>Complete</div>
                                            <div class='status-label-color'><label class='status-color-label red'></label>Fail</div>
                                             <div class='status-label-color'><label class='status-color-label Yellow'></label>Others</div>
                                        </div>
                                    </li>",

                IsChild = false,
                HasChild = true,
                UserValues = metadata.UserValues
            });
        }

        [HttpGet]
        public ActionResult ChildList(long ID)
        {
            // call the service and get the meta data
            var metadata = ClientFactory.SearchServiceClient(CallContext).SearchList(Services.Contracts.Enums.SearchView.PurchaseSKU);

            return PartialView("_ChildList", new SearchListViewModel
            {
                ControllerName = "ProductSKU",
                ViewName = Infrastructure.Enums.SearchView.PurchaseSKU,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                //IsMultilineEnabled = metadata.MultilineEnabled,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                FilterColumns = metadata.QuickFilterColumns,
                RuntimeFilter = Convert.ToString("HeadIID=" + ID.ToString()).Trim(), // child view must have this column
                IsChild = true,
                ParentViewName = Infrastructure.Enums.SearchView.PurchaseInvoice,
                IsEditableLink = false
            });
        }

        [HttpGet]
        public JsonResult SearchData(int currentPage = 1, string orderBy = "", string runtimeFilter = "")
        {
            var view = Infrastructure.Enums.SearchView.PointOfSale;
            if (runtimeFilter.Trim().IsNotNullOrEmpty())
            {
                view = Infrastructure.Enums.SearchView.PurchaseSKU;
                runtimeFilter = runtimeFilter + " AND CompanyID = " + CallContext.CompanyID.ToString();
            }
            else
            {
                runtimeFilter = " CompanyID = " + CallContext.CompanyID.ToString();
            }

            return base.SearchData(view, currentPage, orderBy, runtimeFilter);
        }

        [HttpGet]
        public JsonResult SearchSummaryData()
        {
            return base.SearchSummaryData(Infrastructure.Enums.SearchView.PointOfSaleSummary);
        }

        [HttpGet]
        [OutputCache(CacheProfile = "CacheEduegate")]
        public ActionResult DetailedView(long IID = 0)
        {
            ViewBag.IID = IID;
            return View();
        }

        [OutputCache(CacheProfile = "CacheEduegate")]
        public ActionResult Create(long ID = 0, string parameters = "")
        {
            var tModel = new TransactionViewModel();

            if (!string.IsNullOrEmpty(parameters))
            {
                foreach (var param in parameters.Split(','))
                {
                    var paramDetail = param.Split('=');
                    tModel.Parameters.Add(new KeyValueViewModel()
                    {
                        Key = paramDetail.Length > 0 ? paramDetail[0] : string.Empty,
                        Value = paramDetail.Length > 1 ? paramDetail[1] : string.Empty
                    });
                }
            }

            tModel.TransactionDetails = new List<TransactionDetailViewModel>();
            tModel.TransactionDate = DateTime.Now.ToString(dateTimeFormat);

            int documentTypeID;
            var documentType = tModel.Parameters.Where(a => a.Key.Equals("DocumentType.Key")).FirstOrDefault();

            if (documentType != null)
            {
                tModel.SettingCode = "";
                documentTypeID = Convert.ToInt32(documentType.Value);
            }
            else
            {
                tModel.SettingCode = Constants.TransactionSettings.POSDOCTYPEID;
                SettingDTO settingDetail = ClientFactory.SettingServiceClient(CallContext).GetSettingDetail(Constants.TransactionSettings.POSDOCTYPEID);
                documentTypeID = Convert.ToInt32(settingDetail.SettingValue);
            }

            var branch = tModel.Parameters.Where(a => a.Key.Equals("Branch.Key")).FirstOrDefault();

            if (branch != null)
            {
                tModel.BranchID = Convert.ToInt32(branch.Value);
            }

            var documentDetails = ClientFactory.MetadataServiceClient(CallContext).GetDocumentType(documentTypeID);

            if (documentDetails != null)
            {
                tModel.DocumentName = documentDetails.TransactionTypeName;
                tModel.DocumentTypeID = documentDetails.DocumentTypeID;
            }

            var result = ClientFactory.ReferenceDataServiceClient(CallContext).GetTaxTemplateDetails(documentTypeID);
            tModel.TaxDetails = TaxDetailsViewModel.ToVM(result, tModel.TaxDetails.Taxes);
            ViewBag.HASDELIVERY = ClientFactory.SecurityServiceClient(CallContext).HasClaimSetAccessByResource(Framework.Helper.Enums.HasClaims.HASDELIVERY.ToString(), CallContext.LoginID.Value);
            var printSetting = ClientFactory.SettingServiceClient(CallContext).GetSettingDetailByCompany("PRINTSETTING", CallContext.CompanyID.Value);

            if (printSetting != null)
            {
                ViewBag.PRINTSETTING = printSetting.SettingValue;
            }

            ViewBag.ModelStructure = tModel;
            Helpers.PortalWebHelper.GetDefaultSettingsToViewBag(CallContext, ViewBag);

            if (ID > 0)
            {
                tModel = GetTransaction(ID);
            }

            return View(tModel);
        }

        [HttpGet]
        [OutputCache(CacheProfile = "CacheEduegate")]
        public ActionResult Edit(long ID, string parameters = "")
        {
            return RedirectToAction("Create", new { ID = ID });
        }


        private TransactionViewModel GetTransaction(long ID)
        {
            var transactionDTO = ClientFactory.TransactionServiceClient(CallContext).GetTransaction(ID, false, true);
            return TransactionViewModel.ToVM(transactionDTO);
        }

        [OutputCache(CacheProfile = "CacheEduegate")]
        public ActionResult PrintPOSInvoice(long ID)
        {
            if (ID <= 0)
                return null;

            var transactionDTO = ClientFactory.TransactionServiceClient(CallContext).GetTransaction(ID, false, true);

            var printSetting = ClientFactory.SettingServiceClient(CallContext).GetSettingDetailByCompany("PRINTSETTING", CallContext.CompanyID.Value);

            if (printSetting != null)
            {
                ViewBag.PRINTSETTING = printSetting.SettingValue;
            }

            var printTitle = ClientFactory.SettingServiceClient(CallContext).GetSettingDetailByCompany("PRINT_TITLE", CallContext.CompanyID.Value);

            if (printTitle != null)
            {
                ViewBag.PRINTTITLE = printTitle.SettingValue;
            }

            var printFooter = ClientFactory.SettingServiceClient(CallContext).GetSettingDetailByCompany("PRINT_FOOTER", CallContext.CompanyID.Value);

            if (printFooter != null)
            {
                ViewBag.PRINTFOOTER = printFooter.SettingValue;
            }

            Helpers.PortalWebHelper.GetDefaultSettingsToViewBag(CallContext, ViewBag);

            return View(transactionDTO);
        }
    }
}