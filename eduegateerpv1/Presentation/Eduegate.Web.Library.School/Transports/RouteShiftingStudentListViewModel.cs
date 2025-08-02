using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Transports
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "StudentList", "CRUDModel.ViewModel.StudentList")]
    [DisplayName("Student list")]
    public class RouteShiftingStudentListViewModel : BaseMasterViewModel
    {
        public RouteShiftingStudentListViewModel()
        {
            Student = new KeyValueViewModel();
        }

        public long StudentRouteStopMapLogIID { get; set; }

        public long? StudentRouteStopMapID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [DisplayName("Sr.No")]
        public long SerialNo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2,"large-col-width","text-left")]
        [Select2("Student", "String", false, "FillStudentTransportDatas($event, $element, CRUDModel.ViewModel.StudentList)")]
        //[LookUp("LookUps.Student")]
        //[LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Student", "LookUps.Student")]
        [LookUp("LookUps.TransportStudent")]
        [CustomDisplay("Student")]
        public KeyValueViewModel Student { get; set; }
        public long? StudentID { get; set; }


        //[ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        //[CustomDisplay("Class and section")]
        //public string ClassSection { get; set; }

        public int? ClassID { get; set; }

        public int? SectionID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("Pickup stop")]
        //public string ToSchool { get; set; }
        public string OldPickUpStop { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("Drop stop")]
        //public string FromSchool { get; set; }
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

        [ControlType(Framework.Enums.ControlTypes.Button, "medium-col-width", Attributes = "ng-Click=AddBelowStud($index)")]
        [CustomDisplay("Applybelow")]
        public string AddBelow { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.CheckBox)]
        //[DisplayName("OneWay")]
        //public bool? IsOneWay { get; set; }

        public string OldPickUpStopEdit { get; set; }
        public string OldDropStopEdit { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index, ModelStructure.StudentList[0], CRUDModel.ViewModel.StudentList)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, ModelStructure.StudentList[0],CRUDModel.ViewModel.StudentList)'")]
        [DisplayName("-")]
        public string Remove { get; set; }
    }
}