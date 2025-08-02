using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Web.Library.Common;
using System.Globalization;

namespace Eduegate.Web.Library.School.Fees
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "SplitData", "gridModel.SplitData")]
    [DisplayName("FeeAssign")]
    public class CollectFeeAccountPostingSplitViewModel : BaseMasterViewModel
    {

        public long? FeeCollectionFeeTypeMapsID { get; set; }

        public long? AccountID { get; set; }
        [ControlType(Framework.Enums.ControlTypes.Label, " ")]
        [CustomDisplay("FeeType")]
        public string FeeMaster { get; set; }
        public long? FeeMasterId { get; set; }
        [ControlType(Framework.Enums.ControlTypes.Label, " ")]
        [DisplayName(" ")]
        public string NewLine11 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("Amount")]
        public decimal Amount { get; set; }

    }
}
