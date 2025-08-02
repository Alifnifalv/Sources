using Eduegate.Application.Mvc;
using Eduegate.Domain;
using Eduegate.Domain.Repository;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Web.Library.Common;
using Eduegate.Infrastructure.Enums;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.Enums.Common;
using Eduegate.Services.Contracts.Vendor;
using Eduegate.Vendor.PortalCore.Models;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Web.Library.ViewModels.Filter;
using Eduegate.Web.Library.ViewModels.Inventory.Purchase;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Eduegate.Vendor.PortalCore.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            //return View();
            return RedirectToAction("Login","Account");
        }

        public IActionResult Home()
        {
            return View();
        } 
        
        public IActionResult Cytoscape()
        {
            return View(); 
        }

        public IActionResult _ToastMessage() 
        {
            return View();
        }

        public IActionResult VendorRegistration()
        {
            return View(); 
        }

        public IActionResult QuotationList()
        {
            return View();
        }

        public IActionResult RFQItemList(long iid,string screen,string window)
        {
            var model = new PurchaseQuotationMasterViewModel
            {
                TransactionHeadIID = iid,
                Screen = screen,
                Window = window,
                ShowSaveButtons = window == "VendorRFQList" ? false : true,
            };
            return View(model);
        }

        [HttpGet]
        public JsonResult GetCountsForDashBoardMenuCards(int chartID)
        {
            var result = ClientFactory.SchoolServiceClient(CallContext).GetCountsForDashBoardMenuCards(chartID);
            return Json(result);
        }


        public JsonResult GetNotifications()
        {
            var result = new NotificationBL(CallContext).GetAlerts((long)CallContext.LoginID);
            return Json(result);
        }

        public async Task<IActionResult> DataListView(SearchView listName)
        {
            var view = listName;
            var paramview = view;
            var viewModel = new FilterViewModel() { View = paramview, ViewName = paramview.ToString() };
            var filterClient = ClientFactory.MetadataServiceClient(CallContext);
            var metadata = filterClient.GetFilterMetadata((Eduegate.Services.Contracts.Enums.SearchView)view);
            viewModel.Columns = FilterColumnViewModel.FromDTO(metadata).OrderBy(x => x.FilterColumnID).ToList();
            viewModel.UserValues = filterClient.GetUserFilterMetadata((Services.Contracts.Enums.SearchView)(int)view);
            var gridMetadata = await ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)view);

            return View(new AdvanceFilterViewModel()
            {
                FilterViewModel = viewModel,
                SearchMetadata = new SearchListViewModel
                {
                    ViewName = paramview,
                    ViewTitle = gridMetadata.ViewName,
                    HeaderList = gridMetadata.Columns,
                    SummaryHeaderList = gridMetadata.SummaryColumns,
                    UserViews = gridMetadata.UserViews,
                    SortColumns = gridMetadata.SortColumns,
                    IsMultilineEnabled = false,
                    IsCategoryColumnEnabled = false,
                    InfoBar = string.Empty,
                    IsEditableLink = gridMetadata.IsEditable,
                    IsGenericCRUDSave = gridMetadata.IsGenericCRUDSave,
                    ActualControllerName = gridMetadata.ControllerName,
                    ViewFullPath = gridMetadata.ViewFullPath,
                    RuntimeFilter = "",
                }
            });
        }  
        
        public IActionResult PurchaseOrder() 
        {
            return View();
        }

        public JsonResult GetUserDetails()
        {
            var userDetail = ClientFactory.AccountServiceClient(CallContext).GetUserDetails(CallContext.EmailID);

            if (!string.IsNullOrEmpty(userDetail.ProfileFile))
            {
                userDetail.ProfileFile = string.Format("{0}/{1}/{2}", new Domain.Setting.SettingBL().GetSettingValue<string>("ImageHostUrl"), EduegateImageTypes.UserProfile, userDetail.ProfileFile);
            }

            return Json(new { IsError = false, Response = userDetail });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult VendorContacts()
        {
            return View();
        }

        public IActionResult VendorContactList()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
