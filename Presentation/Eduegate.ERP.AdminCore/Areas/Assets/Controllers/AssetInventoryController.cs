using Eduegate.ERP.Admin.Controllers;
using Eduegate.ERP.Admin.Helpers;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.Warehouses;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Web.Library.ViewModels.Common;
using Eduegate.Web.Library.ViewModels.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Eduegate.Services.Contracts.School.Fees;
using Eduegate.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Web.Library.Accounts.Assets;

namespace Eduegate.ERP.Admin.Areas.Assets.Controllers
{
    [Area("Assets")]
    public class AssetInventoryController : BaseSearchController
    {
        private string dateTimeFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateTimeFormat");

        // GET: Assets/AssetInventory
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetAssetInventoryDetail(long assetID, long branchID = 0)
        {
            var result = ClientFactory.FixedAssetServiceClient(CallContext).GetAssetInventoryDetail(assetID, branchID);

            return Json(result);
        }

    }
}