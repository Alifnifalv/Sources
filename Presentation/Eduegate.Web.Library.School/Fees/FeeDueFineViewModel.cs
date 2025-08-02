using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Eduegate.Web.Library.School.Fees
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "FeeFines", "CRUDModel.ViewModel.FeeFines")]
    [DisplayName("FeeFines")]
   public class FeeDueFineViewModel : BaseMasterViewModel
    {
       
        [ControlType(Framework.Enums.ControlTypes.Label, "medium - col - width")]
        [DisplayName("Fine")]
        public string Fine { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width")]
        [DisplayName("Amount")]
        public decimal? Amount { get; set; }

        public int? FineMasterID { get; set; }
        public long? FineMasterStudentMapID { get; set; }

        public long FeeCollectionFeeTypeMapsIID { get; set; }
        public long? FeeDueFeeTypeMapsID { get; set; }
        public long? StudentFeeDueID { get; set; }


    }
}
   