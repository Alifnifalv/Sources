using Eduegate.Domain;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts.Catalog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Eduegate.Web.Library.ViewModels.Inventory.Purchase
{
    public class PurchaseRequestViewModel : BaseMasterViewModel
    {
        public PurchaseRequestViewModel()
        {
            MasterViewModel = new PurchaseRequestMasterViewModel();
            DetailViewModel = new List<PurchaseRequestDetailViewModel>() { new PurchaseRequestDetailViewModel() };
        }
        public PurchaseRequestMasterViewModel MasterViewModel { get; set; }
        public List<PurchaseRequestDetailViewModel> DetailViewModel { get; set; }

    }
}