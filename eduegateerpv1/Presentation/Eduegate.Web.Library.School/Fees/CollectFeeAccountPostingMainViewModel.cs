using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Eduegate.Web.Library.Common;


namespace Eduegate.Web.Library.School.Fees
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "MainData", "CRUDModel.ViewModel.MainData")]
    [DisplayName("Particulars")]
    public class CollectFeeAccountPostingMainViewModel : BaseMasterViewModel
    {
        public CollectFeeAccountPostingMainViewModel()
        {
            FeeDetail = new List<CollectFeeAccountPostingDetailViewModel>() { new CollectFeeAccountPostingDetailViewModel() };
        }
        [ControlType(Framework.Enums.ControlTypes.GridGroup, "feeStructure")]
        [DisplayName("Particulars")]
        public List<CollectFeeAccountPostingDetailViewModel> FeeDetail { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [DisplayName("Amount")]
        public decimal Amount { get; set; }
       
    }
}
