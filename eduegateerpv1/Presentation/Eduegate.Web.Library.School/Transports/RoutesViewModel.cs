using Eduegate.Domain;
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

namespace Eduegate.Web.Library.School.Transports
{
    public class RoutesViewModel : BaseMasterViewModel
    {
        public RoutesViewModel()
        {
            Stop = new List<RouteStopFeeViewModel>();           
            StopFees = new List<RouteStopFeeViewModel>() { new RouteStopFeeViewModel() };
            IsActive = true;
            RouteType = new Domain.Setting.SettingBL(null).GetSettingValue<string>("ROUTE_TYPEID_BOTH");
        }

        public int RouteID { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.DropDown)]
        //[DisplayName("Route Type")]
        //[LookUp("LookUps.RouteType")]
        public string RouteType { get; set; }
        public byte? RouteTypeID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("RouteCode")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string RouteCode { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsActive")]
        public bool? IsActive { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("RouteGroup")]
        [LookUp("LookUps.RouteGroup")]
        public string RouteGroup { get; set; }
        public int? RouteGroupID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("RouteDescription")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string RouteDescription { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Stop", "Numeric", true, isFreeText: true)]
        [LookUp("LookUps.Stop")]
        [CustomDisplay("Stop")]
        public List<RouteStopFeeViewModel> Stop { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.DropDown)]
        //[CustomDisplay("Academic Year")]
        //[LookUp("LookUps.AcademicYear")]
        //public string AcademicYear { get; set; }
        public int? AcademicYearID { get; set; }
       

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("CostCenter")]
        [Select2("CostCenterDetails", "Numeric", false)]
        [LookUp("LookUps.CostCenterDetails")]
        public KeyValueViewModel CostCenter { get; set; }
        public int? CostCenterID { get; set; }

        [Required]
        [MaxLength(15, ErrorMessage = "Maximum Length should be within 15!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("MonthlyFare")]
        public decimal? RouteFareOneWay { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("ContactNumber")]
        [MaxLength(13, ErrorMessage = "Max 13 characters"), MinLength(8, ErrorMessage = "Min 8 characters")]
        [RegularExpression(@"^[0-9*#+]+$", ErrorMessage = "Use  digits only")]
        public string ContactNumber { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("StopFees")]
        public List<RouteStopFeeViewModel> StopFees { get; set; }

        [ControlType(Framework.Enums.ControlTypes.GeoLocation,"fullwidth alignleft")]
        [CustomDisplay("Geo")]
        public string Geo { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as RoutesDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<RoutesViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<RoutesDTO, RoutesViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            Mapper<RouteStopFeeDTO, RouteStopFeeViewModel>.CreateMap();
            var routeDto = dto as RoutesDTO;
            var vm = Mapper<RoutesDTO, RoutesViewModel>.Map(dto as RoutesDTO);

            vm.RouteType = routeDto.RouteTypeID.HasValue ? routeDto.RouteTypeID.ToString() : null;
            vm.RouteID = routeDto.RouteID;
            vm.RouteCode = routeDto.RouteCode;
            vm.IsActive = routeDto.IsActive;
            vm.RouteDescription = routeDto.RouteDescription;
            vm.RouteFareOneWay = routeDto.RouteFareOneWay;
            //vm.AcademicYear = routeDto.AcademicYearID.HasValue ? routeDto.AcademicYearID.ToString() : null;
            vm.ContactNumber = routeDto.ContactNumber;
            vm.CostCenter = routeDto.CostCenter?.Key != null ? new KeyValueViewModel()
            {
                Key = routeDto.CostCenter.Key.ToString(),
                Value = routeDto.CostCenter.Value
            } : new KeyValueViewModel();
            vm.RouteGroup = routeDto.RouteGroupID.HasValue ? routeDto.RouteGroupID.ToString() : null;

            vm.StopFees = new List<RouteStopFeeViewModel>();

            foreach (var stp in routeDto.StopFees)
            {
                vm.StopFees.Add(new RouteStopFeeViewModel()
                {
                    Key = stp.StopName,
                    Value = stp.StopName,
                    RouteFareOneWay = stp.RouteFareOneWay.HasValue ? stp.RouteFareOneWay : (decimal?)null,
                    RouteStopMapID = stp.RouteStopMapIID,
                    RouteID = stp.RouteID,
                });

                vm.Stop.Add(new RouteStopFeeViewModel()
                {
                    Key = stp.StopName,
                    Value = stp.StopName,
                    RouteFareOneWay = stp.RouteFareOneWay,
                    RouteStopMapID = stp.RouteStopMapIID,
                    RouteID = stp.RouteID,
                    IsActive = stp.IsActive,
                    Latitude = stp.Latitude,
                    Longitude = stp.Longitude,

                });
            }

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<RoutesViewModel, RoutesDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            Mapper<RouteStopFeeViewModel, RouteStopFeeDTO>.CreateMap();
            var dto = Mapper<RoutesViewModel, RoutesDTO>.Map(this);

            dto.RouteTypeID = string.IsNullOrEmpty(this.RouteType) ? (byte?)null : Convert.ToByte(this.RouteType);
            dto.RouteID = this.RouteID;
            dto.RouteCode = this.RouteCode;
            dto.IsActive = this.IsActive;
            dto.RouteDescription = this.RouteDescription;
            dto.RouteFareOneWay = this.RouteFareOneWay;
            dto.CostCenterID = this == null || this.CostCenter == null || this.CostCenter.Key == null ? (int?)null : int.Parse(this.CostCenter.Key);
            //dto.AcademicYearID = string.IsNullOrEmpty(this.AcademicYear) ? (int?)null : int.Parse(this.AcademicYear);
            dto.ContactNumber = this.ContactNumber;
            dto.RouteGroupID = string.IsNullOrEmpty(this.RouteGroup) ? (int?)null : Convert.ToInt16(this.RouteGroup);

            dto.StopFees = new List<RouteStopFeeDTO>();

            foreach (var stpdto in this.Stop)
            {
                dto.StopFees.Add(new RouteStopFeeDTO()
                {
                    RouteID = this.RouteID,
                    StopName = stpdto.Key,
                    RouteFareOneWay = stpdto.RouteFareOneWay,
                    RouteStopMapIID = stpdto.RouteStopMapID,
                    IsActive = stpdto.IsActive,
                    Latitude = stpdto.Latitude,
                    Longitude = stpdto.Longitude,
                });
            }

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<RoutesDTO>(jsonString);
        }

    }
}