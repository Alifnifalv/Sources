using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System;
using System.Globalization;
using Eduegate.Services.Contracts.School.Transports;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Transports
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "RouteShifting", "CRUDModel.ViewModel")]
    [DisplayName("Route shifting")]
    public class RouteShiftingViewModel : BaseMasterViewModel
    {
        public RouteShiftingViewModel()
        {
            StudentList = new List<RouteShiftingStudentListViewModel>() { new RouteShiftingStudentListViewModel() };
            StaffList = new List<RouteShiftingStaffListViewModel>() { new RouteShiftingStaffListViewModel() };
        }

        public long StudentRouteStopMapIID { get; set; }

        public byte? SchoolID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, attribs: "ng-change='RouteGroupChanges($event, $element,CRUDModel.ViewModel)'")]
        [CustomDisplay("RouteGroup")]
        [LookUp("LookUps.RouteGroup")]
        public int? RouteGroupID { get; set; }
        public string RouteGroup { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("Route")]
        [Select2("Route", "Numeric", false, "GetRouteStudStaffData($event, $element, CRUDModel.ViewModel)", optionalAttribute1: "ng-disabled='!CRUDModel.ViewModel.RouteID == 0'")]
        [LookUp("LookUps.Route")]
        public KeyValueViewModel Routes { get; set; }
        public int? RouteID { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        //[DisplayName("")]
        //public string NewLine1 { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[Select2("AcademicYear", "Numeric", false, "")]
        //[LookUp("LookUps.AcademicYear")]
        //[CustomDisplay("AcademicYear")]
        //public KeyValueViewModel Academic { get; set; }
        //public int? AcademicYearID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, attribs: "ng-change='ToRouteGroupChanges($event, $element,CRUDModel.ViewModel)'")]
        [CustomDisplay("ToRouteGroup")]
        [LookUp("LookUps.RouteGroup")]
        public int? ToRouteGroupID { get; set; }
        public string ToRouteGroup { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Button, "textleft", "ng-click='GetStudentDetails($event, $index, CRUDModel.ViewModel)'")]
        [CustomDisplay("FillStudents")]
        public string ReferenceNumber { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "textleft", "ng-click='GetStaffDetails($event, $index, CRUDModel.ViewModel)'")]
        [CustomDisplay("FillStaffs")]
        public string StaffReferenceNumber { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Grid, "colspan=6")]
        [CustomDisplay("StudentList")]
        public List<RouteShiftingStudentListViewModel> StudentList { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid, "colspan=6")]
        [CustomDisplay("StaffList")]
        public List<RouteShiftingStaffListViewModel> StaffList { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as RouteShiftingDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<RouteShiftingViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<RouteShiftingDTO, RouteShiftingViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var routeDto = dto as RouteShiftingDTO;
            var vm = Mapper<RouteShiftingDTO, RouteShiftingViewModel>.Map(routeDto);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            vm.Routes = new KeyValueViewModel() { Key = routeDto.Route.Key, Value = routeDto.Route.Value };
            //vm.Academic = new KeyValueViewModel() { Key = routeDto.AcademicYear.Key, Value = routeDto.AcademicYear.Value };
            vm.RouteID = routeDto.RouteID;
            vm.RouteGroup = routeDto.RouteGroupID.HasValue ? routeDto.RouteGroupID.ToString() : null;
            vm.ToRouteGroupID = routeDto.ToRouteGroupID.Value;

            vm.StudentList = new List<RouteShiftingStudentListViewModel>();

            if (routeDto.StudentLists.Count > 0)
            {
                foreach (var map in routeDto.StudentLists)
                {
                    vm.StudentList.Add(new RouteShiftingStudentListViewModel
                    {
                        StudentRouteStopMapLogIID = map.StudentRouteStopMapLogIID,
                        StudentID = map.StudentID.HasValue ? map.StudentID : null,
                        ClassID = map.ClassID,
                        SectionID = map.SectionID,
                        //ClassSection = string.IsNullOrEmpty(map.ClassName) || string.IsNullOrEmpty(map.Section) ? "No records found " : map.ClassName + " - " + map.Section,
                        DateToString = map.DateTo.HasValue ? map.DateTo.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                        DateFromString = map.DateFrom.HasValue ? map.DateFrom.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                        OldPickUpStop = map.OldPickUpStop,
                        Student = new KeyValueViewModel() { Key = map.Student.Key, Value = map.Student.Value },
                        OldDropStop = map.OldDropStop,
                        PickupStopMap = map.PickupStopMapID.HasValue ? map.PickupStopMapID.ToString() : null,
                        DropStopMap = map.DropStopMapID.HasValue ? map.DropStopMapID.ToString() : null,
                        OldDropStopEdit = map.OldDropStopEdit != null ? map.OldDropStopEdit : null,
                        OldPickUpStopEdit = map.OldPickUpStopEdit != null ? map.OldPickUpStopEdit : null,
                    });
                }
            }

            vm.StaffList = new List<RouteShiftingStaffListViewModel>();

            if (routeDto.StaffLists.Count > 0)
            {
                foreach (var staffmap in routeDto.StaffLists)
                {
                    vm.StaffList.Add(new RouteShiftingStaffListViewModel
                    {
                        StaffRouteStopMapLogIID = staffmap.StaffRouteStopMapLogIID,
                        //StaffRouteStopMapID = staffmap.StaffRouteStopMapID,
                        StaffID = staffmap.StaffID.HasValue ? staffmap.StaffID : null,
                        DateToString = staffmap.DateTo.HasValue ? staffmap.DateTo.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                        DateFromString = staffmap.DateFrom.HasValue ? staffmap.DateFrom.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                        OldPickUpStop = staffmap.OldPickUpStop,
                        OldDropStop = staffmap.OldDropStop,
                        Staff = new KeyValueViewModel() { Key = staffmap.Staff.Key, Value = staffmap.Staff.Value },
                        PickupStopMap = staffmap.PickupStopMapID.HasValue ? staffmap.PickupStopMapID.ToString() : null,
                        DropStopMap = staffmap.DropStopMapID.HasValue ? staffmap.DropStopMapID.ToString() : null,
                        OldDropStopEdit = staffmap.OldDropStopEdit != null ? staffmap.OldDropStopEdit : null,
                        OldPickUpStopEdit = staffmap.OldPickUpStopEdit != null ? staffmap.OldPickUpStopEdit : null,
                    });
                }
            }

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<RouteShiftingViewModel, RouteShiftingDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = Mapper<RouteShiftingViewModel, RouteShiftingDTO>.Map(this);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            //dto.AcademicYearID = this.Academic == null || string.IsNullOrEmpty(this.Academic.Key) ? (int?)null : int.Parse(this.Academic.Key);
            dto.RouteID = this.Routes == null || string.IsNullOrEmpty(this.Routes.Key) ? (int?)null : int.Parse(this.Routes.Key);
            dto.RouteGroupID = this.RouteGroupID;
            dto.ToRouteGroupID = this.ToRouteGroupID;

            dto.StudentLists = new List<RouteShiftingStudentMapDTO>();

            if (this.StudentList.Count > 0)
            {
                foreach (var map in this.StudentList)
                {
                    if (map.Student.Key != null && map.PickupStopMap != null || map.Student.Key != null && map.DropStopMap != null)
                    {
                    dto.StudentLists.Add(new RouteShiftingStudentMapDTO
                    {
                        StudentRouteStopMapLogIID = map.StudentRouteStopMapLogIID,
                        StudentID = string.IsNullOrEmpty(map.Student.Key) ? 0 : int.Parse(map.Student.Key),
                        ClassID = map.ClassID,
                        SectionID = map.SectionID,
                        PickupStopMapID = map == null || map.PickupStopMap == null || string.IsNullOrEmpty(map.PickupStopMap) ? (long?)null : long.Parse(map.PickupStopMap),
                        DropStopMapID = map == null || map.DropStopMap == null || string.IsNullOrEmpty(map.DropStopMap) ? (long?)null : long.Parse(map.DropStopMap),
                        DateFrom = string.IsNullOrEmpty(map.DateFromString) ? (DateTime?)null : DateTime.ParseExact(map.DateFromString, dateFormat, CultureInfo.InvariantCulture),
                        DateTo = string.IsNullOrEmpty(map.DateToString) ? (DateTime?)null : DateTime.ParseExact(map.DateToString, dateFormat, CultureInfo.InvariantCulture),
                        OldPickUpStop = map.OldPickUpStop != null ? map.OldPickUpStop : null,
                        OldDropStop = map.OldDropStop != null ? map.OldDropStop : null,
                        OldPickUpStopEdit = map.OldPickUpStopEdit != null ? map.OldPickUpStopEdit : null,
                        OldDropStopEdit = map.OldDropStopEdit != null ? map.OldDropStopEdit : null,
                    });
                    }
                }
            }

            dto.StaffLists = new List<RouteShiftingStaffMapDTO>();
            if (this.StaffList.Count > 0)
            {
                foreach (var map in this.StaffList)
                {
                    if (map.Staff.Key != null && map.PickupStopMap != null || map.Staff.Key != null && map.DropStopMap != null)
                    {
                        dto.StaffLists.Add(new RouteShiftingStaffMapDTO
                        {
                            StaffRouteStopMapLogIID = map.StaffRouteStopMapLogIID,
                            StaffRouteStopMapID = map.StaffRouteStopMapID.HasValue ? map.StaffRouteStopMapID : 0,
                            StaffID = string.IsNullOrEmpty(map.Staff.Key) ? 0 : int.Parse(map.Staff.Key),
                            PickupStopMapID = map == null || map.PickupStopMap == null || string.IsNullOrEmpty(map.PickupStopMap) ? (long?)null : long.Parse(map.PickupStopMap),
                            DropStopMapID = map == null || map.DropStopMap == null || string.IsNullOrEmpty(map.DropStopMap) ? (long?)null : long.Parse(map.DropStopMap),
                            DateFrom = string.IsNullOrEmpty(map.DateFromString) ? (DateTime?)null : DateTime.ParseExact(map.DateFromString, dateFormat, CultureInfo.InvariantCulture),
                            DateTo = string.IsNullOrEmpty(map.DateToString) ? (DateTime?)null : DateTime.ParseExact(map.DateToString, dateFormat, CultureInfo.InvariantCulture),
                            OldPickUpStop = map.OldPickUpStop != null ? map.OldPickUpStop : null,
                            OldDropStop = map.OldDropStop != null ? map.OldDropStop : null,
                            OldPickUpStopEdit = map.OldPickUpStopEdit != null ? map.OldPickUpStopEdit : null,
                            OldDropStopEdit = map.OldDropStopEdit != null ? map.OldDropStopEdit : null,
                        });
                    }
                }
            }

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<RouteShiftingDTO>(jsonString);
        }

    }
}