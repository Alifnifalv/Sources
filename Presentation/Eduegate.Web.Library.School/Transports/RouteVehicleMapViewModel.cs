using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.Transports;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Transports
{
    public class RouteVehicleMapViewModel : BaseMasterViewModel
    {
        public RouteVehicleMapViewModel()
        {
            IsActive = true;
        }

        public long RouteVehicleMapIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("Vehicle")]
        [Select2("VehicleDetails", "Numeric", false)]
        [LookUp("LookUps.VehicleDetails")]
        public KeyValueViewModel Vehicle { get; set; }
        public long? VehicleID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, attribs: "ng-change='RouteGroupChanges($event, $element,CRUDModel.ViewModel)'")]
        [CustomDisplay("RouteGroup")]
        [LookUp("LookUps.RouteGroup")]
        public string RouteGroup { get; set; }
        public int? RouteGroupID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("Route")]
        [Select2("VehicleDetails", "Numeric", true)]
        [LookUp("LookUps.Route")]
        public List<KeyValueViewModel> Routes { get; set; }
        public int? RouteID { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.DropDown)]
        //[CustomDisplay("Academic Year")]
        //[LookUp("LookUps.AcademicYear")]
        //public string AcademicYear { get; set; }
        public int? AcademicYearID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("DateFrom")]
        public string DateFromString { get; set; }
        public DateTime? DateFrom { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("DateTo")]
        public string DateToString { get; set; }
        public DateTime? DateTo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsActive")]
        public bool? IsActive { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as RouteVehicleMapDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<RouteVehicleMapViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<RouteVehicleMapDTO, RouteVehicleMapViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var mapDto = dto as RouteVehicleMapDTO;
            var vm = Mapper<RouteVehicleMapDTO, RouteVehicleMapViewModel>.Map(dto as RouteVehicleMapDTO);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            vm.DateFromString = mapDto.DateFrom.Value.ToString(dateFormat, CultureInfo.InvariantCulture);
            vm.DateToString = mapDto.DateTo.HasValue ? mapDto.DateTo.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.Vehicle = mapDto.VehicleID.HasValue ? new KeyValueViewModel()
            {
                Key = mapDto.Vehicle.Key,
                Value = mapDto.Vehicle.Value
            } : new KeyValueViewModel();
            vm.RouteGroup = mapDto.RouteGroupID.HasValue ? mapDto.RouteGroupID.ToString() : null;
            //vm.AcademicYear = mapDto.AcademicYearID.HasValue ? mapDto.AcademicYearID.ToString() : null;

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<RouteVehicleMapViewModel, RouteVehicleMapDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var dto = Mapper<RouteVehicleMapViewModel, RouteVehicleMapDTO>.Map(this);

            dto.VehicleID = string.IsNullOrEmpty(this.Vehicle.Key) ? (long?)null : long.Parse(this.Vehicle.Key);
            dto.DateFrom = string.IsNullOrEmpty(this.DateFromString) ? (DateTime?)null : DateTime.ParseExact(DateFromString, dateFormat, CultureInfo.InvariantCulture);
            dto.DateTo = string.IsNullOrEmpty(this.DateToString) ? (DateTime?)null : DateTime.ParseExact(DateToString, dateFormat, CultureInfo.InvariantCulture);
            dto.RouteGroupID = string.IsNullOrEmpty(this.RouteGroup) ? (int?)null : int.Parse(this.RouteGroup);

            dto.Routes = new List<KeyValueDTO>();

            foreach (var route in this.Routes)
            {
                dto.Routes.Add(new KeyValueDTO
                {
                    Key = route.Key,
                    Value = route.Value
                });
            }

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<RouteVehicleMapDTO>(jsonString);
        }

        public static List<KeyValueViewModel> ToKeyValueViewModel(List<RouteVehicleMapDTO> dtos)
        {
            var vMs = new List<KeyValueViewModel>();

            foreach (var dto in dtos)
            {
                vMs.Add(new KeyValueViewModel() { Key = dto.Vehicle.Key.ToString(), Value = dto.Vehicle.Value });

            }

            return vMs;
        }

    }
}