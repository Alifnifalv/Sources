using Eduegate.ERP.Admin.Controllers;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Helper;
using Eduegate.Frameworks.Enums;
using Eduegate.Infrastructure.Enums;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Eduegate.Domain;

namespace Eduegate.ERP.Admin.Areas.Securities.Controllers
{
    [Area("Securities")]
    public class ParentLoginController : BaseSearchController
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> List()
        {
            // call the service and get the meta data
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Eduegate.Infrastructure.Enums.SearchView.ParentLogin);

            return View(new SearchListViewModel
            {
                ViewName = Eduegate.Infrastructure.Enums.SearchView.ParentLogin,
                ControllerName = "ParentLogin",
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                UserViews = metadata.UserViews,
                SortColumns = metadata.SortColumns,
                HasFilters = metadata.HasFilters,
                FilterColumns = metadata.QuickFilterColumns,
                //IsMultilineEnabled = metadata.MultilineEnabled,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                //                InfoBar = @"<li class='status-label-mobile'>
                //                            <div class='right status-label'>
                //                                <div class='status-label-color'><label class='status-color-label orange'></label>Draft</div>
                //                                <div class='status-label-color'><label class='status-color-label  green'></label>Active</div>
                //                                <div class='status-label-color'><label class='status-color-label red'></label>In Active</div>
                //                            </div>
                //                        </li>"
                IsDeleteLink = false,
                UserValues = metadata.UserValues,
            });
        }

        [HttpGet]
        public Task<IActionResult> SearchData(int currentPage = 1, string orderBy = "")
        {
            return base.SearchData(Eduegate.Infrastructure.Enums.SearchView.ParentLogin, currentPage, orderBy);
        }

        [HttpGet]
        public Task<IActionResult> SearchSummaryData()
        {
            return base.SearchSummaryData(Eduegate.Infrastructure.Enums.SearchView.ParentLoginSummary);
        }

        [HttpGet]
        public ActionResult DetailedView(long IID = 0)
        {
            ViewBag.IID = IID;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create(long ID = 0)
        {
            var viewModel = new CRUDViewModel();
            viewModel.Name = "ParentLogin";
            viewModel.ListActionName = "ParentLogin";
            viewModel.ScreenTypeID = 1;
            var claimTypes = ClientFactory.ReferenceDataServiceClient(CallContext).GetLookUpData(Eduegate.Services.Contracts.Enums.LookUpTypes.ClaimType, string.Empty, 0, 0);
            var customerVm = new UserMasterViewModel();
            customerVm.SecuritySettings.Claims = Eduegate.Web.Library.ViewModels.Security.SecurityClaimViewModel.GetSecurityVM(claimTypes, new List<Eduegate.Services.Contracts.Admin.ClaimDetailDTO>());

            viewModel.ViewModel = customerVm;
            viewModel.IID = ID;
            viewModel.UserRoles = Eduegate.Web.Library.ViewModels.Security.UserRoleViewModel.ToVM(await ClientFactory.SecurityServiceClient(CallContext).GetUserRoles(CallContext.LoginID.Value));

            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "LoginUserStatus", Url = "Mutual/GetLookUpData?lookType=LoginUserStatus" });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Roles", Url = "Mutual/GetLookUpData?lookType=Roles&defaultBlank=false" });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "ClaimSets", Url = "Mutual/GetLookUpData?lookType=ClaimSets&defaultBlank=false" });

            foreach (var claim in claimTypes)
            {
                viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Claims_" + claim.Key.ToString(), Url = "Securities/ParentLogin/GetClaimsByType?lookupName=Claims&claimType=" + claim.Key.ToString(), IsOnInit = false });
            }

            //Lookup for ony ERP
            if (viewModel.UserRoles.Exists(a => a.Role == Framework.Helper.Enums.UserRole.ERP))
            {
                //customerVm.Contacts.Add(new ContactsViewModel() { });
                viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Title", Url = "Mutual/GetLookUpData?lookType=Title" });
                viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Countries", Url = "Mutual/GetCountries" }); // Countries
                viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Areas", Url = "Mutual/GetAreaByCountryID?countryID=10003" }); // Areas
                viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Cities", Url = "Mutual/GetCityByCountryID?countryID=10003" }); // Cities
                viewModel.Urls.Add(new UrlViewModel() { LookUpName = "PhoneTypes", Url = "Mutual/GetEntityPropertiesByType?entityType=" + (int)EntityPropertyTypes.Telephone }); // Phone
            }

            TempData["viewModel"] = Newtonsoft.Json.JsonConvert.SerializeObject(viewModel);
            return RedirectToAction("Create", "CRUD", new { area = "Frameworks", screen = Screens.ParentLogin });
        }

        [HttpGet]
        public ActionResult Edit(long ID = 0)
        {
            return RedirectToAction("Create", new { ID = ID });
        }

        [HttpGet]
        public ActionResult Get(long ID = 0)
        {
            var userDTO = ClientFactory.AccountServiceClient(CallContext).GetUserDetailsByID(ID.ToString());
            var claimTypes = ClientFactory.ReferenceDataServiceClient(CallContext).GetLookUpData(Eduegate.Services.Contracts.Enums.LookUpTypes.ClaimType, string.Empty, 0, 0);
            return Json(UserMasterViewModel.FromDTO(userDTO, claimTypes));
        }

        [HttpPost]
        public JsonResult Save([FromBody] UserMasterViewModel vm)
        {
            var userID = CallContext.LoginID;
            var fileName = System.IO.Path.GetFileName(vm.ProfileUrl);
            //move from temparary folder to oringal location
            string tempFolderPath = string.Format("{0}\\{1}\\{2}\\{3}\\{4}",
                    new Domain.Setting.SettingBL().GetSettingValue<string>("ImagesPhysicalPath").ToString(), EduegateImageTypes.UserProfile.ToString(), Constants.TEMPFOLDER, userID, fileName);
            string orignalFolderPath = string.Format("{0}//{1}//{2}",
                     new Domain.Setting.SettingBL().GetSettingValue<string>("ImagesPhysicalPath").ToString(), EduegateImageTypes.UserProfile.ToString(), fileName);

            if (System.IO.File.Exists(tempFolderPath))
            {
                if (!Directory.Exists(Path.GetDirectoryName(orignalFolderPath)))
                    Directory.CreateDirectory(orignalFolderPath);

                System.IO.File.Copy(tempFolderPath, orignalFolderPath, true);
                vm.ProfileFile = fileName;

            }
            var userDTO = ClientFactory.AccountServiceClient(CallContext).SaveLogin(UserMasterViewModel.ToDTO(vm));
            var claimTypes = ClientFactory.ReferenceDataServiceClient(CallContext).GetLookUpData(Eduegate.Services.Contracts.Enums.LookUpTypes.ClaimType, string.Empty, 0, 0);
            return Json(UserMasterViewModel.FromDTO(userDTO, claimTypes));
        }

        [HttpGet]
        public bool CheckCustomerEmailIDAvailability(long loginID, string loginEmailID)
        {
            bool isAvailibilty = false;
            if (ClientFactory.AccountServiceClient(CallContext).CheckCustomerEmailIDAvailability(loginID, loginEmailID))
            {
                isAvailibilty = true;
            }

            return isAvailibilty;
        }

        [HttpGet]
        public ActionResult Delete(long ID = 0)
        {
            var dto = ClientFactory.AccountServiceClient(CallContext).GetUserDetailsByID(ID.ToString());
            return Json(UserMasterViewModel.FromDTO(dto));
        }

        public JsonResult GetClaimsByType(ClaimType claimType, string lookupName, string searchText)
        {
            return Json(new
            {
                LookUpName = lookupName,
                Data = Eduegate.Web.Library.ViewModels.Security.ClaimViewModel.ToKeyValueVM(ClientFactory.SecurityServiceClient(CallContext).GetClaimsByType(0, claimType))
            }); ;
        }
    }
}