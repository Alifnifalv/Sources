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

namespace Eduegate.Web.Library.School.Transports
{
    public class AssignVehicleViewModel : BaseMasterViewModel
    {
        public AssignVehicleViewModel()
        {
            Attendanter = new List<KeyValueViewModel>();
            Vehicle = new KeyValueViewModel();
            IsActive = true;
        }

        public long AssignVehicleMapIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("DateFrom")]
        public string DateFromString { get; set; }
        public System.DateTime? DateFrom { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("DateTo")]
        public string DateToString { get; set; }
        public DateTime? DateTo { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.DropDown)]
        //[CustomDisplay("Academic Year")]
        //[LookUp("LookUps.AcademicYear")]
        //public string AcademicYear { get; set; }
        public int? AcademicYearID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsActive")]
        public bool? IsActive { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("AssigntoDriver")]
        [Select2("Driver", "String", false, "")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Driver", "LookUps.Driver")]
        public KeyValueViewModel AssignBackEmployee { get; set; }
        public long? EmployeeID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("AssigntoAttendant")]
        [Select2("VehicleAttender", "String", true, "")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=VehicleAttender", "LookUps.VehicleAttender")]
        public List<KeyValueViewModel> Attendanter { get; set; }
        public long? AttendanterID { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("Vehicle")]
        [Select2("VehicleDetails", "Numeric", false)]
        [LookUp("LookUps.VehicleDetails")]
        public KeyValueViewModel Vehicle { get; set; }
        public long? VehicleID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, attribs: "ng-change='RouteGroupChanges($event, $element,CRUDModel.ViewModel)'")]
        [CustomDisplay("RouteGroup")]
        [LookUp("LookUps.RouteGroup")]
        public string RouteGroup { get; set; }
        public int? RouteGroupID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("Route")]
        [Select2("Route", "Numeric", false)]
        [LookUp("LookUps.Route")]
        public KeyValueViewModel Routes { get; set; }
        public int? RouteID { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [CustomDisplay("Notes")]
        [MaxLength(250, ErrorMessage = "Maximum Length should be within 250!")]
        public string Notes { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as AssignVehicleDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<AssignVehicleViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<AssignVehicleDTO, AssignVehicleViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var asgnDto = dto as AssignVehicleDTO;
            var vm = Mapper<AssignVehicleDTO, AssignVehicleViewModel>.Map(dto as AssignVehicleDTO);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            vm.DateFromString = asgnDto.DateFrom.Value.ToString(dateFormat, CultureInfo.InvariantCulture);
            vm.DateToString = asgnDto.DateTo.HasValue ? asgnDto.DateTo.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            //vm.AcademicYear = asgnDto.AcademicYearID.HasValue ? asgnDto.AcademicYearID.ToString() : null;
            vm.AssignBackEmployee = new KeyValueViewModel()
            {
                Key = asgnDto.EmployeeID.ToString(),
                Value = asgnDto.DriverName
            };
            vm.Vehicle = asgnDto.VehicleID.HasValue ? new KeyValueViewModel()
            {
                Key = asgnDto.Vehicle.Key,
                Value = asgnDto.Vehicle.Value
            } : new KeyValueViewModel();
            vm.Routes = asgnDto.RouteID.HasValue ? new KeyValueViewModel()
            {
                Key = asgnDto.Routes.Key,
                Value = asgnDto.Routes.Value
            } : new KeyValueViewModel();
            vm.RouteGroup = asgnDto.RouteGroupID.HasValue ? asgnDto.RouteGroupID.ToString() : null;
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<AssignVehicleViewModel, AssignVehicleDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = Mapper<AssignVehicleViewModel, AssignVehicleDTO>.Map(this);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            dto.EmployeeID = string.IsNullOrEmpty(this.AssignBackEmployee.Key) ? (long?)null : long.Parse(this.AssignBackEmployee.Key);
            dto.VehicleID = string.IsNullOrEmpty(this.Vehicle.Key) ? (long?)null : long.Parse(this.Vehicle.Key);
            dto.RouteID = string.IsNullOrEmpty(this.Routes.Key) ? (int?)null : int.Parse(this.Routes.Key);
            dto.DateFrom = string.IsNullOrEmpty(this.DateFromString) ? (DateTime?)null : DateTime.ParseExact(DateFromString, dateFormat, CultureInfo.InvariantCulture);
            dto.DateTo = string.IsNullOrEmpty(this.DateToString) ? (DateTime?)null : DateTime.ParseExact(DateToString, dateFormat, CultureInfo.InvariantCulture);
            dto.RouteGroupID = string.IsNullOrEmpty(this.RouteGroup) ? (int?)null : int.Parse(this.RouteGroup);

            List<KeyValueDTO> AttendanterList = new List<KeyValueDTO>();

            foreach (KeyValueViewModel vm in this.Attendanter)
            {
                AttendanterList.Add(new KeyValueDTO
                {
                    Key = vm.Key,
                    Value = vm.Value
                });
            }

            dto.Attendanter = AttendanterList;

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<AssignVehicleDTO>(jsonString);
        }

    }
}