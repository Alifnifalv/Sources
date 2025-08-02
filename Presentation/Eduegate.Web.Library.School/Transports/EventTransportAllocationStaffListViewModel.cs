using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Students;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Transports
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "StaffList", "CRUDModel.ViewModel.StaffList")]
    [DisplayName("Staff List")]
    public class EventTransportAllocationStaffListViewModel : BaseMasterViewModel
    {
        public EventTransportAllocationStaffListViewModel()
        {
            Staff = new KeyValueViewModel();
        }

        public long EventTransportAllocationMapIID { get; set; }
        public long? StaffRouteStopMapID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Staff", "String", false, "FillStaffTransportDatas($event, $element, CRUDModel.ViewModel.StaffList)")]
        //[LookUp("LookUps.TransportStaff")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=TransportStaff", "LookUps.TransportStaff")]
        [CustomDisplay("Staff")]
        public KeyValueViewModel Staff { get; set; }

        public long? StaffID { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Label)]
        //[DisplayName("Department")]
        //public string Department { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Designation")]
        public string Designation { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Pickup Route")]
        public string PickUpRoute { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("PickUp Stop")]
        public string PickupStop { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Drop Route")]
        public string DropRoute { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Drop Stop")]
        public string DropStop { get; set; }

        public int? PickupRouteID { get; set; }
        public int? DropRouteID { get; set; }

        public int? ToRouteID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index,ModelStructure.StaffList[0],CRUDModel.ViewModel.StaffList)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index,ModelStructure.StaffList[0],CRUDModel.ViewModel.StaffList)'")]
        [DisplayName("-")]
        public string Remove { get; set; }

    }
}