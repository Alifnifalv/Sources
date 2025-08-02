using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.HR.Payroll
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "TimeSheet", "CRUDModel.ViewModel")]
    [DisplayName("TimeSheet View")]
    public class EmployeeTimesheetsListViewModel : BaseMasterViewModel
    {
        public EmployeeTimesheetsListViewModel()
        {
            DetailData = new List<EmployeeTimeSheetsTimeViewModel>() { new EmployeeTimeSheetsTimeViewModel() };
        }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Employee", "Numeric", false, "")]
        //[LookUp("LookUps.Employee")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Employee", "LookUps.Employee")]
        [CustomDisplay("EmployeeName")]
        public KeyValueViewModel Employee { get; set; }
        public long EmployeeID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.DateTimePicker)]
        [CustomDisplay("DateFrom")]
        public string CollectionDateFromString { get; set; }

        public System.DateTime? CollectionDateFrom { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DateTimePicker)]
        [CustomDisplay("DateTo")]
        public string CollectionDateToString { get; set; }

        public System.DateTime? CollectionDateTo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, Attributes = "ng-Click=FillCollectFee()")]
        [CustomDisplay("View")]
        public string GenerateButton { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("")]
        public List<EmployeeTimeSheetsTimeViewModel> DetailData { get; set; }
    }
}
