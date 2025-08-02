using Eduegate.ERP.Admin.Controllers;
using Eduegate.ERP.Admin.Helpers;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Web.Library.ViewModels.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Eduegate.Domain;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Eduegate.ERP.Admin.Areas.Inventories.Controllers
{
    [Area("Inventories")]
    public class BundleWrapController : BaseSearchController
    {
        private string dateTimeFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateTimeFormat");

        // GET: Inventories/BundleWrap
        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> List()
        {
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Infrastructure.Enums.SearchView.BundleWrap);

            return View(new SearchListViewModel
            {
                ControllerName = "Inventories/" + Infrastructure.Enums.SearchView.BundleWrap,
                ViewName = Infrastructure.Enums.SearchView.BundleWrap,
                HeaderList = metadata.Columns,
                //SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                IsCommentLink = true,
                HasFilters = metadata.HasFilters,
                FilterColumns = metadata.QuickFilterColumns,
                UserValues = metadata.UserValues,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                InfoBar = @"<li class='status-label-mobile'>
                                                        <div class='right status-label'>
                                                            <div class='status-label-color'><label class='status-color-label  green'></label>Complete</div>
                                                            <div class='status-label-color'><label class='status-color-label red'></label>Fail</div>
                                                            <div class='status-label-color'><label class='status-color-label orange'></label>Others</div>
                                                        </div>
                                                    </li>",
                IsChild = false,
                HasChild = true,
            });
        }
        [HttpGet]
        public async Task<IActionResult> ChildList(long ID)
        {
            // call the service and get the meta data
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList(Services.Contracts.Enums.SearchView.PurchaseSKU);

            return PartialView("_ChildList", new SearchListViewModel
            {
                ControllerName = "ProductSKU",
                ViewName = Infrastructure.Enums.SearchView.PurchaseSKU,
                HeaderList = metadata.Columns,
               // SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                //IsMultilineEnabled = metadata.MultilineEnabled,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                FilterColumns = metadata.QuickFilterColumns,
                RuntimeFilter = Convert.ToString("HeadIID=" + ID.ToString()).Trim(), // child view must have this column
                IsChild = true,
                ParentViewName = Infrastructure.Enums.SearchView.BundleWrap,
                IsEditableLink = false
            });
        }

        [HttpGet]
        public Task<IActionResult> SearchData(int currentPage = 1, string orderBy = "", string runtimeFilter = "")
        {
            var view = Infrastructure.Enums.SearchView.BundleWrap;
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

        //[HttpGet]
        //public Task<IActionResult> SearchSummaryData()
        //{
        //    return base.SearchSummaryData(Infrastructure.Enums.SearchView.BundleWrapSummary);
        //}

        [HttpGet]
        public ActionResult DetailedView(long IID = 0)
        {
            ViewBag.IID = IID;
            return View();
        }


        public ActionResult Create(long ID = 0)
        {
            var vm = new BundleWrapViewModel()
            {
                DetailViewModel = new List<BundleWrapDetailViewModel>() { new BundleWrapDetailViewModel() { } },
                MasterViewModel = new BundleWrapMasterViewModel()
                {
                    IsSummaryPanel = false,
                    TransactionDate = DateTime.Now.ToString(dateTimeFormat),
                    //Currency = KeyValueViewModel.ToViewModel(ClientFactory.ReferenceDataServiceClient(CallContext).GetDefaultCurrencyWithName()),
                }
            };

            return Json(vm);
        }

        [HttpGet]
        public ActionResult Edit(long ID = 0)
        {
            return RedirectToAction("Create", new { ID = ID });
        }

        [HttpGet]
        public ActionResult Get(long ID = 0)
        {
            var vm = TransactionViewModel.FromBundleWrapDTO(ClientFactory.TransactionServiceClient(CallContext).GetTransaction(ID, false, false));
            if (vm != null)
            {
                vm.DetailViewModel.Add(new BundleWrapDetailViewModel());
            }

            return Json(vm);
        }

        [HttpPost]
        public JsonResult Save(string model)
        {
            try
            {
                var jsonData = JObject.Parse(model);
                var vm = JsonConvert.DeserializeObject<BundleWrapViewModel>(jsonData.SelectToken("data").ToString());

                var updatedVM = new BundleWrapViewModel();
                var transactionDTO = TransactionViewModel.ToBundleWrapDTO(vm);

                if (vm != null)
                {

                    var quantityCheck = ClientFactory.TransactionServiceClient(CallContext).CheckAvailibilityBundleItem(transactionDTO.TransactionHead.BranchID.Value, transactionDTO.TransactionDetails);

                    if (quantityCheck.IsError)
                    {

                        return Json(new { IsError = true, UserMessage = "There is not enough quantity for the product '" + quantityCheck.ProductName + "'!" });
                    }

                    var result = ClientFactory.TransactionServiceClient(CallContext).SaveTransactions(TransactionViewModel.ToBundleWrapDTO(vm));
                    updatedVM = TransactionViewModel.FromBundleWrapDTO(result);

                    if (updatedVM != null)
                    {
                        updatedVM.DetailViewModel.Add(new BundleWrapDetailViewModel());
                    }

                    if (updatedVM.MasterViewModel.IsError == true)
                    {
                        return Json(new { IsError = true, UserMessage = PortalWebHelper.GetErrorMessage(updatedVM.MasterViewModel.ErrorCode), data = vm });
                    }
                }

                return Json(updatedVM);
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<BundleWrapController>.Fatal(ex.Message.ToString(), ex);
                return Json(new { IsError = true, UserMessage = ex.Message });
            }
        }

        [HttpGet]
        public JsonResult GetDeliveryAddress(long customerID, AddressType addressType)
        {
            var addresses = GetAddressDetail(customerID);
            return Json(addresses);
        }

        public Tuple<BillingAddressViewModel, DeliveryAddressViewModel> GetAddressDetail(long customerID)
        {
            var list = ClientFactory.AccountServiceClient(CallContext).GetShippingAddressDetail(customerID);

            var billingDto = list.Where(x => x.IsBillingAddress == true).FirstOrDefault();
            var billing = BillingAddressViewModel.FromOrderContactDTOToVM(billingDto);

            var deliveryDto = list.Where(x => x.IsShippingAddress == true).FirstOrDefault();
            var delivery = DeliveryAddressViewModel.FromOrderContactDTOToVM(deliveryDto);

            return Tuple.Create(billing, delivery);
        }


        [HttpGet]
        public JsonResult GetCountries()
        {
            var countries = new List<CountryDTO>();
            var message = "";
            var siteID = new Domain.Setting.SettingBL().GetSettingValue<int>("SiteID");

            try
            {
                countries = ClientFactory.CountryMasterServiceClient(CallContext).GetCountriesBySiteMap(siteID);
            }
            catch (Exception ex) { message = ex.Message.ToString(); }

            return Json(new { CountryList = countries, Message = message });
        }

        [HttpGet]
        public JsonResult GetAreaByCountryID(int countryID = 0)
        {
            var areas = new List<AreaDTO>();
            var areaVMList = new List<KeyValueViewModel>();
            var message = "";

            try
            {
                areas = ClientFactory.MutualServiceClient(CallContext).GetAreaByCountryID((int)countryID);
            }
            catch (Exception ex) { message = ex.Message.ToString(); }

            // Convert dto to View model
            if (areas.IsNotNull() && areas.Count > 0)
            {
                foreach (var area in areas)
                {
                    // creating new area vm
                    var vm = new KeyValueViewModel();
                    vm.Key = area.AreaID.ToString();
                    vm.Value = area.AreaName;

                    // Adding this to vm list
                    areaVMList.Add(vm);
                }
            }

            return Json(new { AreaList = areaVMList, Message = message });
        }

        [HttpPost]
        public ActionResult SaveDeliveryDetails(OrderDeliveryDetailViewModel order)
        {
            try
            {
                var detailDTO = ClientFactory.TransactionServiceClient(CallContext).SaveDeliveryDetails(OrderDeliveryDetailViewModel.ToDTO(order));
                var vm = detailDTO.IsNotNull() ? OrderDeliveryDetailViewModel.ToVM(detailDTO) : null;
                return Json(new { IsError = false, UserMessage = "", data = vm });
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<BundleWrapController>.Info("Exception : " + ex.Message.ToString());
                return Json(new { IsError = true, ErrorMessage = ex.Message.ToString(), data = "" });
            }

        }

        [HttpGet]
        public ActionResult GetDeliveryDetails(long Id)
        {
            try
            {
                var orderDTO = ClientFactory.TransactionServiceClient(CallContext).GetDeliveryDetails(Id);
                var vm = OrderDeliveryDetailViewModel.ToVM(orderDTO);

                return Json(new { IsError = false, UserMessage = "", data = vm });
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<BundleWrapController>.Info("Exception : " + ex.Message.ToString());
                return Json(new { IsError = true, ErrorMessage = ex.Message.ToString(), data = "" });
            }

        }

        [HttpGet]
        public ActionResult GetDeliveryOptions()
        {
            List<KeyValueViewModel> kVMs = new List<KeyValueViewModel>();
            KeyValueViewModel kvm = null;

            var deliveryTypeList = ClientFactory.TransactionServiceClient(CallContext).GetDeliveryOptions();

            if (deliveryTypeList.IsNotNull() && deliveryTypeList.Count > 0)
            {
                foreach (var dtp in deliveryTypeList)
                {
                    kvm = new KeyValueViewModel();

                    kvm.Key = dtp.DeliveryTypeID.ToString();
                    kvm.Value = dtp.DeliveryTypeName;

                    kVMs.Add(kvm);
                }
            }
            return Json(kVMs);
        }
    }
}