using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Web.Library.Common;
using Eduegate.Services.Contracts.School.Transports;
using System;
using System.Globalization;
using System.Collections.Generic;

namespace Eduegate.Web.Library.School.Transports
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "DriverScheduleLog", "CRUDModel.ViewModel")]
    [DisplayName("Driver schedule log")]
    public class DriverScheduleLogViewModel : BaseMasterViewModel
    {
        public DriverScheduleLogViewModel()
        {
            Staff = new KeyValueViewModel();
            Student = new KeyValueViewModel();
            Route = new KeyValueViewModel();
            RouteStop = new KeyValueViewModel();
            Vehicle = new KeyValueViewModel();
        }

        public long DriverScheduleLogIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker, "", attribs: "ng-disabled=CRUDModel.ViewModel.DriverScheduleLogIID != 0")]
        [DisplayName("Schedule date")]
        public string SheduleDateString { get; set; }
        public DateTime? SheduleDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, attribs: "ng-change='StopEntryStatusChanges(CRUDModel.ViewModel)' ng-disabled=CRUDModel.ViewModel.DriverScheduleLogIID != 0")]
        [LookUp("LookUps.StopEntryStatus")]
        [DisplayName("Type")]
        public string StopEntryStatus { get; set; }
        public int? StopEntryStatusID { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Label)]
        //[DisplayName("Schedule log type")]
        public string ScheduleLogType { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Employee", "String", false, "", false, optionalAttribute1: "ng-disabled='CRUDModel.ViewModel.DriverScheduleLogIID != 0  || CRUDModel.ViewModel.Student.Key'")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Employee", "LookUps.Employee")]
        [CustomDisplay("Employee")]
        public KeyValueViewModel Staff { get; set; }
        public long? EmployeeID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Students", "Numeric", false, "StudentChanges(CRUDModel.ViewModel)", false, optionalAttribute1: "ng-disabled='CRUDModel.ViewModel.DriverScheduleLogIID != 0 || CRUDModel.ViewModel.Staff.Key'")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Student", "LookUps.Students")]
        [CustomDisplay("Student")]
        public KeyValueViewModel Student { get; set; }
        public long? StudentID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Class")]
        public string ClassName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Section")]
        public string SectionName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown, attribs: "ng-change='RouteGroupChanges(CRUDModel.ViewModel)' ng-disabled=CRUDModel.ViewModel.DriverScheduleLogIID != 0")]
        [LookUp("LookUps.RouteGroup")]
        [CustomDisplay("RouteGroup")]
        public string RouteGroup { get; set; }
        public int? RouteGroupID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Route", "Numeric", false, "RouteChanges(CRUDModel.ViewModel)", false, optionalAttribute1: "ng-disabled=CRUDModel.ViewModel.DriverScheduleLogIID != 0")]
        [LookUp("LookUps.Route")]
        [DisplayName("Route")]
        public KeyValueViewModel Route { get; set; }
        public int? RouteID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine4 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("RouteStopMap", "Numeric", false, "", false, optionalAttribute1: "ng-disabled=CRUDModel.ViewModel.DriverScheduleLogIID != 0")]
        [LookUp("LookUps.RouteStopMap")]
        [DisplayName("Stop")]
        public KeyValueViewModel RouteStop { get; set; }
        public long? RouteStopMapID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("VehicleDetails", "Numeric", false, "", false, optionalAttribute1: "ng-disabled=CRUDModel.ViewModel.DriverScheduleLogIID != 0")]
        [LookUp("LookUps.VehicleDetails")]
        [DisplayName("Vehicle")]
        public KeyValueViewModel Vehicle { get; set; }
        public long? VehicleID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine5 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, attribs: "ng-change='SheduleLogInStatusChanges(CRUDModel.ViewModel)'")]
        [LookUp("LookUps.SheduleLogStatus")]
        [DisplayName("In status")]
        public string SheduleLogInStatus { get; set; }
        public int? SheduleLogInStatusID { get; set; }

        public long DriverScheduleLogInIID { get; set; }
        public string InStatus { get; set; }
        public string ScheduleLogInType { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, attribs: "ng-change='SheduleLogOutStatusChanges(CRUDModel.ViewModel)'")]
        [LookUp("LookUps.SheduleLogStatus")]
        [DisplayName("Out status")]
        public string SheduleLogOutStatus { get; set; }
        public int? SheduleLogOutStatusID { get; set; }

        public long DriverScheduleLogOutIID { get; set; }
        public string OutStatus { get; set; }
        public string ScheduleLogOutType { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as DriverScheduleLogDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<DriverScheduleLogViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<DriverScheduleLogDTO, DriverScheduleLogViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var logDTO = dto as DriverScheduleLogDTO;
            var vm = Mapper<DriverScheduleLogDTO, DriverScheduleLogViewModel>.Map(logDTO);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            vm.SheduleDateString = logDTO.SheduleDate.HasValue ? logDTO.SheduleDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.StopEntryStatus = logDTO.StopEntryStatusID.HasValue ? logDTO.StopEntryStatusID.ToString() : null;
            vm.Staff = logDTO.EmployeeID.HasValue ? new KeyValueViewModel() { Key = logDTO.EmployeeID.ToString(), Value = logDTO.StaffName } : new KeyValueViewModel();
            vm.Student = logDTO.StudentID.HasValue ? new KeyValueViewModel() { Key = logDTO.StudentID.ToString(), Value = logDTO.StudentName } : new KeyValueViewModel();
            vm.Route = logDTO.RouteID.HasValue ? new KeyValueViewModel() { Key = logDTO.RouteID.ToString(), Value = logDTO.RouteCode } : new KeyValueViewModel();
            vm.RouteStop = logDTO.RouteStopMapID.HasValue ? new KeyValueViewModel() { Key = logDTO.RouteStopMapID.ToString(), Value = logDTO.StopName } : new KeyValueViewModel();
            vm.Vehicle = logDTO.VehicleID.HasValue ? new KeyValueViewModel() { Key = logDTO.VehicleID.ToString(), Value = logDTO.VehicleRegistrationNumber } : new KeyValueViewModel();
            vm.RouteGroup = logDTO.RouteGroupID.HasValue ? logDTO.RouteGroupID.ToString() : null;

            if (logDTO.InLog != null)
            {
                vm.DriverScheduleLogInIID = logDTO.InLog.IID;
                vm.SheduleLogInStatus = logDTO.InLog.StatusID.HasValue ? logDTO.InLog?.StatusID?.ToString() : null;
                vm.InStatus = logDTO.InLog?.Status;
                vm.ScheduleLogInType = logDTO.InLog?.ScheduleLogType;
            }

            if (logDTO.OutLog != null)
            {
                vm.DriverScheduleLogOutIID = logDTO.OutLog.IID;
                vm.SheduleLogOutStatus = logDTO.OutLog.StatusID.HasValue ? logDTO.OutLog?.StatusID?.ToString() : null;
                vm.OutStatus = logDTO.OutLog?.Status;
                vm.ScheduleLogOutType = logDTO.OutLog?.ScheduleLogType;
            }            

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<DriverScheduleLogViewModel, DriverScheduleLogDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = Mapper<DriverScheduleLogViewModel, DriverScheduleLogDTO>.Map(this);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            dto.SheduleDate = string.IsNullOrEmpty(this.SheduleDateString) ? (DateTime?)null : DateTime.ParseExact(this.SheduleDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.StopEntryStatusID = string.IsNullOrEmpty(this.StopEntryStatus) ? (int?)null : int.Parse(this.StopEntryStatus);
            dto.EmployeeID = this.Staff != null && string.IsNullOrEmpty(this.Staff.Key) ? (long?)null : long.Parse(this.Staff.Key);
            dto.StudentID = this.Student != null && string.IsNullOrEmpty(this.Student.Key) ? (long?)null : long.Parse(this.Student.Key);
            dto.RouteGroupID = string.IsNullOrEmpty(this.RouteGroup) ? (int?)null : int.Parse(this.RouteGroup);
            dto.RouteID = this.Route != null && string.IsNullOrEmpty(this.Route.Key) ? (int?)null : int.Parse(this.Route.Key);
            dto.RouteStopMapID = this.RouteStop != null && string.IsNullOrEmpty(this.RouteStop.Key) ? (long?)null : long.Parse(this.RouteStop.Key);
            dto.VehicleID = this.Vehicle != null && string.IsNullOrEmpty(this.Vehicle.Key) ? (long?)null : long.Parse(this.Vehicle.Key);

            dto.InLog = new ScheduleLogInfoDTO()
            {
                IID = this.DriverScheduleLogInIID,
                StatusID = string.IsNullOrEmpty(this.SheduleLogInStatus) || int.Parse(this.SheduleLogInStatus) == 0 ? (int?)null : int.Parse(this.SheduleLogInStatus),
                Status = this.InStatus,
                ScheduleLogType = this.ScheduleLogInType
            };

            dto.OutLog = new ScheduleLogInfoDTO()
            {
                IID = this.DriverScheduleLogOutIID,
                StatusID = string.IsNullOrEmpty(this.SheduleLogOutStatus) || int.Parse(this.SheduleLogOutStatus) == 0 ? (int?)null : int.Parse(this.SheduleLogOutStatus),
                Status = this.OutStatus,
                ScheduleLogType = this.ScheduleLogOutType
            };

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<DriverScheduleLogDTO>(jsonString);
        }

    }
}