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
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "StudentList", "CRUDModel.ViewModel.StudentList")]
    [DisplayName("Student List")]
    public class EventTransportAllocationStudListViewModel : BaseMasterViewModel
    {
        public EventTransportAllocationStudListViewModel()
        {
            Student = new KeyValueViewModel();
        }

        public long EventTransportAllocationMapIID { get; set; }
        public long? StudentRouteStopMapID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Student", "String", false, "FillStudentTransportDatas($event, $element, CRUDModel.ViewModel.StudentList)")]
        [LookUp("LookUps.TransportStudent")]
        [CustomDisplay("Student")]
        public KeyValueViewModel Student { get; set; }

        public long? StudentID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Class Section")]
        public string ClassSection { get; set; }    

        //[ControlType(Framework.Enums.ControlTypes.Label)]
        //[DisplayName("Class")]
        //public string ClassName { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Label)]
        //[DisplayName("Section")]
        //public string Section { get; set; }

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

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index,ModelStructure.StudentList[0],CRUDModel.ViewModel.StudentList)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index,ModelStructure.StudentList[0],CRUDModel.ViewModel.StudentList)'")]
        [DisplayName("-")]
        public string Remove { get; set; }

    }
}