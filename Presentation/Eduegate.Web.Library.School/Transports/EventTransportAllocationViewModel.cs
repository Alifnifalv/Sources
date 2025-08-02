using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.Transports;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;

namespace Eduegate.Web.Library.School.Transports
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "EventTransportAllocation", "CRUDModel.ViewModel")]
    [DisplayName("Details")]
    public class EventTransportAllocationViewModel : BaseMasterViewModel
    {
        public EventTransportAllocationViewModel()
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            IsDrop = false;
            IsPickUp = false;
            ListStudents = false;
            ListStaffs = false;
            Route = new List<KeyValueViewModel>();
            Vehicle = new KeyValueViewModel();
            Attendanter = new KeyValueViewModel();
            ToRoute = new KeyValueViewModel();
            Class = new List<KeyValueViewModel>();
            EventDateString = DateTime.Now.ToString(dateFormat, CultureInfo.InvariantCulture);
            StudentList = new List<EventTransportAllocationStudListViewModel>() { new EventTransportAllocationStudListViewModel() };
            StaffList = new List<EventTransportAllocationStaffListViewModel>() { new EventTransportAllocationStaffListViewModel() };
        }

        public string IsRouteType { get; set; }
        public long EventTransportAllocationIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("Event")]
        [Select2("SchoolEvents", "Numeric", false, "")]
        [LookUp("LookUps.SchoolEvents")]
        public KeyValueViewModel Event { get; set; }
        public int? EventID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Event Date")]
        public string EventDateString { get; set; }
        public System.DateTime? EventDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Description")]
        public string Description { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine6 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, "medium-col-width textleft", Attributes = "ng-change='FillPickUpData($event, $element,CRUDModel.ViewModel)'")]
        [CustomDisplay("Pick Up")]
        public bool? IsPickUp { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, "medium-col-width textleft", Attributes = "ng-change='FillDropData($event, $element,CRUDModel.ViewModel)'")]
        [CustomDisplay("Drop")]
        public bool? IsDrop { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width textleft")]
        [CustomDisplay("")]
        public bool? Space { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("From Route")]
        //[Select2("Route", "Numeric", true, "FillStudentStaffDatasByRouteIDforEvent($event, $element, CRUDModel.ViewModel)")]
        [Select2("Route", "Numeric", true, "")]
        [LookUp("LookUps.Route")]
        public List<KeyValueViewModel> Route { get; set; }
        public int? RouteID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("Class")]
        [Select2("Class", "Numeric", true, "")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Classes", "LookUps.Classes")]
        public List<KeyValueViewModel> Class { get; set; }
        public int? ClassID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine5 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [DisplayName("Vehicle")]
        [Select2("VehicleDetails", "Numeric", false, "")]
        [LookUp("LookUps.VehicleDetails")]
        public KeyValueViewModel Vehicle { get; set; }
        public long? VehicleID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("AssigntoDriver")]
        [Select2("Driver", "String", false, "")]
        //[LookUp("LookUps.Driver")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Driver", "LookUps.Driver")]

        public KeyValueViewModel Driver { get; set; }
        public long? DriverID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("AssigntoAttendant")]
        [Select2("VehicleAttender", "Numeric", false, "")]
        //[LookUp("LookUps.VehicleAttender")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=VehicleAttender", "LookUps.VehicleAttender")]
        public KeyValueViewModel Attendanter { get; set; }
        public long? AttendanterID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine8 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, "medium-col-width textleft")]
        [CustomDisplay("List Students")]
        public bool ListStudents { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, "medium-col-width textleft")]
        [CustomDisplay("List Staffs")]
        public bool ListStaffs { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width textleft")]
        [CustomDisplay("")]
        public bool? Space1 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [DisplayName("To Route")]
        [Select2("Route", "Numeric", false, "")]
        [LookUp("LookUps.Route")]
        public KeyValueViewModel ToRoute { get; set; }
        public int? ToRouteID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "medium-col-width textleft", "ng-disabled='CRUDModel.ViewModel.EventTransportAllocationIID' ng-click='FillData($event, $element, CRUDModel.ViewModel)'")]
        [CustomDisplay("Fill Data")]
        public string FillData { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine7 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.HtmlLabel, Attributes2 = "labelinfo-custom")]
        [CustomDisplay(" ")]
        public string SelectedDetails { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [DisplayName("Student List")]
        public List<EventTransportAllocationStudListViewModel> StudentList { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [DisplayName("Staff List")]
        public List<EventTransportAllocationStaffListViewModel> StaffList { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as EventTransportAllocationDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<EventTransportAllocationViewModel>(jsonString);
        }


        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<EventTransportAllocationDTO, EventTransportAllocationViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            Mapper<EventTransportAllocationMapDTO, EventTransportAllocationStudListViewModel>.CreateMap();
            Mapper<EventTransportAllocationMapDTO, EventTransportAllocationStaffListViewModel>.CreateMap();
            var frmDTO = dto as EventTransportAllocationDTO;
            var vm = Mapper<EventTransportAllocationDTO, EventTransportAllocationViewModel>.Map(frmDTO);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            vm.EventTransportAllocationIID = frmDTO.EventTransportAllocationIID;
            vm.Event = frmDTO.EventID.HasValue ? new KeyValueViewModel() { Key = frmDTO.Event.Key, Value = frmDTO.Event.Value } : new KeyValueViewModel();
            vm.EventDateString = frmDTO.EventDate.HasValue ? frmDTO.EventDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.Description = frmDTO.Description;
            vm.Vehicle = frmDTO.VehicleID.HasValue ? new KeyValueViewModel() { Key = frmDTO.Vehicle.Key, Value = frmDTO.Vehicle.Value } : new KeyValueViewModel();
            vm.Driver = frmDTO.DriverID.HasValue ? new KeyValueViewModel() { Key = frmDTO.Driver.Key, Value = frmDTO.Driver.Value } : new KeyValueViewModel();
            vm.Attendanter = frmDTO.AttendarID.HasValue ? new KeyValueViewModel() { Key = frmDTO.Attendar.Key, Value = frmDTO.Attendar.Value } : new KeyValueViewModel();
            vm.ToRoute = frmDTO.RouteID.HasValue ? new KeyValueViewModel() { Key = frmDTO.ToRoute.Key, Value = frmDTO.ToRoute.Value } : new KeyValueViewModel();
            vm.IsPickUp = frmDTO.IsPickUp == true ? true : false;
            vm.SchoolID = frmDTO.SchoolID;
            vm.AcademicYearID = frmDTO.AcademicYearID;
            vm.IsDrop = frmDTO.IsPickUp == true ? false : true;
            vm.IsRouteType = frmDTO.IsPickUp == true ? "Pick" : "Drop";
            vm.StudentList = new List<EventTransportAllocationStudListViewModel>();
            foreach (var stud in frmDTO.StudentList)
            {
                vm.StudentList.Add(new EventTransportAllocationStudListViewModel()
                {
                    EventTransportAllocationMapIID = stud.EventTransportAllocationMapIID,
                    StudentRouteStopMapID = stud.StudentRouteStopMapID,
                    ClassSection = stud.ClassSection,
                    StudentID = stud.StudentID,
                    PickUpRoute = stud.PickUpRoute,
                    PickupStop = stud.PickupStop,
                    DropRoute = stud.DropRoute,
                    DropStop = stud.DropStop,
                    PickupRouteID = stud.PickupRouteID,
                    DropRouteID = stud.DropRouteID,
                    ToRouteID = stud.ToRouteID,
                    Student = stud.StudentID.HasValue ? new KeyValueViewModel()
                    {
                        Key = stud.Student.Key.ToString(),
                        Value = stud.Student.Value
                    } : null,
                });
            }

            vm.StaffList = new List<EventTransportAllocationStaffListViewModel>();
            foreach (var staff in frmDTO.StaffList)
            {
                vm.StaffList.Add(new EventTransportAllocationStaffListViewModel()
                {
                    EventTransportAllocationMapIID = staff.EventTransportAllocationMapIID,
                    StaffRouteStopMapID = staff.StaffRouteStopMapID,
                    StaffID = staff.StaffID,
                    Designation = staff.Designation,
                    PickUpRoute = staff.PickUpRoute,
                    PickupStop = staff.PickupStop,
                    DropRoute = staff.DropRoute,
                    DropStop = staff.DropStop,
                    PickupRouteID = staff.PickupRouteID,
                    DropRouteID= staff.DropRouteID,
                    ToRouteID = staff.ToRouteID,
                    Staff = staff.StaffID.HasValue ? new KeyValueViewModel()
                    {
                        Key = staff.Staff.Key.ToString(),
                        Value = staff.Staff.Value
                    } : null,
                });
            }

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<EventTransportAllocationViewModel, EventTransportAllocationDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            Mapper<EventTransportAllocationStudListViewModel, EventTransportAllocationMapDTO>.CreateMap();
            Mapper<EventTransportAllocationStaffListViewModel, EventTransportAllocationMapDTO>.CreateMap();
            var dto = Mapper<EventTransportAllocationViewModel, EventTransportAllocationDTO>.Map(this);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            dto.EventTransportAllocationIID = this.EventTransportAllocationIID;
            dto.EventID = string.IsNullOrEmpty(this.Event.Key) ? (long?)null : long.Parse(this.Event.Key);
            dto.EventDate = string.IsNullOrEmpty(this.EventDateString) ? (DateTime?)null : DateTime.ParseExact(this.EventDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.Description = this.Description;
            dto.SchoolID = this.SchoolID;
            dto.AcademicYearID = this.AcademicYearID;
            dto.IsPickUp = this.IsPickUp == true ? true : false;
            dto.VehicleID = string.IsNullOrEmpty(this.Vehicle.Key) ? (long?)null : long.Parse(this.Vehicle.Key);
            dto.DriverID = string.IsNullOrEmpty(this.Driver.Key) ? (long?)null : long.Parse(this.Driver.Key);
            dto.AttendarID = string.IsNullOrEmpty(this.Attendanter.Key) ? (long?)null : long.Parse(this.Attendanter.Key);
            dto.RouteID = string.IsNullOrEmpty(this.ToRoute.Key) ? null : int.Parse(this.ToRoute.Key);


            dto.StudentList = new List<EventTransportAllocationMapDTO>();
            foreach (var stud in this.StudentList)
            {
                if (!dto.StudentList.Any(x => x.StudentID == stud.StudentID) && stud.StudentID != null)
                {
                    dto.StudentList.Add(new EventTransportAllocationMapDTO()
                    {
                        EventTransportAllocationMapIID = stud.EventTransportAllocationMapIID,
                        StudentRouteStopMapID = stud.StudentRouteStopMapID,
                        StudentID = string.IsNullOrEmpty(stud.Student.Key) ? (long?)null : long.Parse(stud.Student.Key),
                        PickUpRoute = stud.PickUpRoute != null ? stud.PickUpRoute : null,
                        DropRoute = stud.DropRoute != null ? stud.DropRoute : null,
                        PickupStop = stud.PickupStop != null ? stud.PickupStop : null,
                        DropStop = stud.DropStop != null ? stud.DropStop : null,
                        PickupRouteID = stud.PickupRouteID,
                        DropRouteID = stud.DropRouteID,
                        ToRouteID = stud.ToRouteID,
                    });
                }
            }

            dto.StaffList = new List<EventTransportAllocationMapDTO>();
            foreach (var staff in this.StaffList)
            {
                if (!dto.StaffList.Any(x => x.StaffID == staff.StaffID) && staff.StaffID != null)
                {
                    dto.StaffList.Add(new EventTransportAllocationMapDTO()
                    {
                        EventTransportAllocationMapIID = staff.EventTransportAllocationMapIID,
                        StaffRouteStopMapID = staff.StaffRouteStopMapID,
                        StaffID = string.IsNullOrEmpty(staff.Staff.Key) ? (long?)null : long.Parse(staff.Staff.Key),
                        PickUpRoute = staff.PickUpRoute != null ? staff.PickUpRoute : null,
                        DropRoute = staff.DropRoute != null ? staff.DropRoute : null,
                        PickupStop = staff.PickupStop != null ? staff.PickupStop : null,
                        DropStop = staff.DropStop != null ? staff.DropStop : null,
                        PickupRouteID = staff.PickupRouteID,
                        DropRouteID = staff.DropRouteID,
                        ToRouteID = staff.ToRouteID,
                    });
                }
            }

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<EventTransportAllocationDTO>(jsonString);
        }
    }
}