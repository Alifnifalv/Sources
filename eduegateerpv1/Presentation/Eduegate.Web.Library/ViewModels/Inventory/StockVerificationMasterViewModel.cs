using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain;
using Eduegate.Framework.Mvc.Attributes;


namespace Eduegate.Web.Library.ViewModels.Inventory
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "StockVerificationMaster", "CRUDModel.Model.MasterViewModel")]
    [DisplayName("Stock Verification")]
    public class StockVerificationMasterViewModel : BaseMasterViewModel
    {
        public StockVerificationMasterViewModel()
        {
            IsSummaryPanel = false;
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            TransactionDate = DateTime.Now.ToString(dateFormat, CultureInfo.InvariantCulture);
            DocumentStatus = new KeyValueViewModel { Key = "1", Value = "Draft" };
        }
        public long HeadIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Transaction No.")]
        public string TransactionNo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [DisplayName("Date")]
        public string TransactionDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Branch", "Numeric", false)]
        [DisplayName("Branch")]
        [LookUp("LookUps.Branch")]
        public KeyValueViewModel Branch { get; set; }
        public long? BranchID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [DisplayName("Employee")]
        [Select2("Employee", "Numeric", false)]
        //[LookUp("LookUps.Employee")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Employee", "LookUps.Employee")]
        public KeyValueViewModel Employee { get; set; }
        public long? EmployeeID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Comments")]
        public string PostedComments { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("DocumentStatus", "Numeric", false)]
        [LookUp("LookUps.DocumentStatus")]
        [DisplayName("Status")]
        public KeyValueViewModel DocumentStatus { get; set; }

        public byte? TransactionStatusID { get; set; }


        public byte? CurrentStatusID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "textleft", "ng-click='FillProductList($event, $index, CRUDModel.Model.MasterViewModel)'")]
        [DisplayName("Fill product Items")]
        public string FillProductItems { get; set; }

    }
}
