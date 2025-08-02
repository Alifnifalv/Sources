using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Transports
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "StaffList", "CRUDModel.ViewModel.StaffList")]
    [DisplayName("Staff list")]
    public class RouteShiftingStaffListViewModel : BaseMasterViewModel
    {
        public RouteShiftingStaffListViewModel()
        {
            Staff = new KeyValueViewModel();
        }

        public long StaffRouteStopMapLogIID { get; set; }

        public long? StaffRouteStopMapID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [DisplayName("Sr.No")]
        public long SerialNo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2,"large-col-width","text-left")]
        [Select2("Employee", "String", false, "FillStaffTransportDatas($event, $element, CRUDModel.ViewModel.StaffList)")]
        //[LookUp("LookUps.Employee")]
        //[LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Employee", "LookUps.Employee")]
        [LookUp("LookUps.TransportStaff")]
        [CustomDisplay("Staff")]
        public KeyValueViewModel Staff { get; set; }
        public long? StaffID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        //[CustomDisplay("ToSchool")]
        //public string ToSchool { get; set; }
        [CustomDisplay("Pickup stop")]
        public string OldPickUpStop { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        //[CustomDisplay("ToHome")]
        //public string FromSchool { get; set; }
        [CustomDisplay("Drop stop")]
        public string OldDropStop { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker, "medium-col-width")]
        [CustomDisplay("Date from")]
        public string DateFromString { get; set; }
        public System.DateTime? DateFrom { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker, "medium-col-width")]
        [CustomDisplay("Date to")]
        public string DateToString { get; set; }
        public System.DateTime? DateTo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown, "medium-col-width")]
        [CustomDisplay("To school")]
        //[LookUp("LookUps.PickupStopALL")]
        [LookUp("LookUps.PickupStopMap")]
        public string PickupStopMap { get; set; }
        public long? PickupStopMapID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown, "medium-col-width")]
        [CustomDisplay("To home")]
        //[LookUp("LookUps.DropStopALL")]
        [LookUp("LookUps.DropStopMap")]
        public string DropStopMap { get; set; }
        public long? DropStopMapID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "medium-col-width", Attributes = "ng-Click=AddBelowStaff($index)")]
        [CustomDisplay("Applybelow")]
        public string AddBelow { get; set; }

        public string OldPickUpStopEdit { get; set; }
        public string OldDropStopEdit { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.CheckBox)]
        //[DisplayName("OneWay")]
        //public bool? IsOneWay { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index, ModelStructure.StaffList[0], CRUDModel.ViewModel.StaffList)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, ModelStructure.StaffList[0],CRUDModel.ViewModel.StaffList)'")]
        [DisplayName("-")]
        public string Remove { get; set; }
    }
}