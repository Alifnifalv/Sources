using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using Eduegate.ERP.Admin.Controllers;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Web.Library.ViewModels.Settings;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Client.Factory;

namespace Eduegate.ERP.Admin.Areas.Accounts.Controllers
{
    public class AccountDocumentTypeController : BaseSearchController
    {
        // GET: DocumentType
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            // call the service and get the meta data
            var metadata = ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Eduegate.Infrastructure.Enums.SearchView.AccountDocumentType);

            return View(new SearchListViewModel
            {
                ControllerName = Eduegate.Infrastructure.Enums.SearchView.AccountDocumentType.ToString(),
                ViewName = Eduegate.Infrastructure.Enums.SearchView.AccountDocumentType,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                //IsMultilineEnabled = metadata.MultilineEnabled,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                InfoBar = @"<li class='status-label-mobile'>
                                        <div class='right status-label'>
                                            <div class='status-label-color'><label class='status-color-label orange'></label>Draft</div>
                                            <div class='status-label-color'><label class='status-color-label  green'></label>Active</div>
                                            <div class='status-label-color'><label class='status-color-label red'></label>In Active</div>
                                        </div>
                                    </li>",
                HasFilters = metadata.HasFilters,
                FilterColumns = metadata.QuickFilterColumns,
                UserValues = metadata.UserValues
            });
        }

        [HttpGet]
        public JsonResult SearchData(int currentPage = 1, string orderBy = "")
        {
            return base.SearchData(Eduegate.Infrastructure.Enums.SearchView.AccountDocumentType, currentPage, orderBy);
        }

        [HttpGet]
        public JsonResult SearchSummaryData()
        {
            return base.SearchSummaryData(Eduegate.Infrastructure.Enums.SearchView.AccountDocumentTypeSummary);
        }

        [HttpGet]
        public ActionResult DetailedView(long IID = 0)
        {
            ViewBag.IID = IID;
            return View();
        }

        [HttpGet]
        public ActionResult Create(long ID = 0)
        {
            var viewModel = new CRUDViewModel();
            viewModel.Name = "AccountDocumentType";
            viewModel.ListActionName = "AccountDocumentType";
            var vm = new DocumentTypeViewModel();
            //vm.TransactionSequenceType = "1";
            viewModel.ViewModel = vm;

            viewModel.IID = ID;
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "ReferenceType", Url = "Mutual/GetLookUpData?lookType=AccountDocumentReferenceTypes" });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Months", Url = "AccountDocumentType/GetMonths" });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "SequenceType", Url = "AccountDocumentType/GetSequenceType" });
            TempData["viewModel"] = viewModel;
            return RedirectToAction("Create", "CRUD");
        }

        [HttpGet]
        public JsonResult GetMonths()
        {
            List<KeyValueViewModel> list = new List<KeyValueViewModel>();
            list.Add(new KeyValueViewModel() { Key = "1", Value = "Jan" });
            list.Add(new KeyValueViewModel() { Key = "2", Value = "Feb" });
            list.Add(new KeyValueViewModel() { Key = "3", Value = "Mar" });
            list.Add(new KeyValueViewModel() { Key = "4", Value = "Apr" });
            list.Add(new KeyValueViewModel() { Key = "5", Value = "May" });
            list.Add(new KeyValueViewModel() { Key = "6", Value = "June" });
            list.Add(new KeyValueViewModel() { Key = "7", Value = "July" });
            list.Add(new KeyValueViewModel() { Key = "8", Value = "Aug" });
            list.Add(new KeyValueViewModel() { Key = "9", Value = "Sep" });
            list.Add(new KeyValueViewModel() { Key = "10", Value = "Oct" });
            list.Add(new KeyValueViewModel() { Key = "11", Value = "Nov" });
            list.Add(new KeyValueViewModel() { Key = "12", Value = "Dec" });
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetSequenceType()
        {
            List<KeyValueViewModel> list = new List<KeyValueViewModel>();
            list.Add(new KeyValueViewModel() { Key = "1", Value = "Regular" });
            list.Add(new KeyValueViewModel() { Key = "2", Value = "Monthly" });
            list.Add(new KeyValueViewModel() { Key = "3", Value = "Yearly" });
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Edit(long ID = 0)
        {
            return RedirectToAction("Create", new { ID = ID });
        }

        [HttpGet]
        public ActionResult Get(long ID = 0)
        {
            var vm = DocumentTypeViewModel.ToVM(ClientFactory.MetadataServiceClient(CallContext).GetDocumentType(ID));
            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(DocumentTypeViewModel vm)
        {
            var dto = DocumentTypeViewModel.ToDTO(vm);
            dto.System = Systems.Account.ToString();
            var updatedVM = DocumentTypeViewModel.ToVM(ClientFactory.MetadataServiceClient(CallContext).SaveDocumentType(dto));
            return Json(updatedVM, JsonRequestBehavior.AllowGet);
        }
    }
}