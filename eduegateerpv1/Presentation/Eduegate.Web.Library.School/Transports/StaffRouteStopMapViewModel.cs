using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.Transports;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Eduegate.Web.Library.Common;


namespace Eduegate.Web.Library.School.Transports
{
    public class StaffRouteStopMapViewModel : BaseMasterViewModel
    {
        public StaffRouteStopMapViewModel()
        {
            //Staff = new KeyValueViewModel();
            RouteType= new KeyValueViewModel();
            PickupStopMap = new KeyValueViewModel();
            DropStopMap = new KeyValueViewModel();
            IsActive = true;
            IsOneWay = false;
            Approve = false;
            TermsAndConditions = false;
            PickupSeatAvailability = new PickupSeatAvailabilityViewModel();
            DropSeatAvailability = new DropSeatAvailabilityViewModel();
            AcademicYear = new KeyValueViewModel();
        }

        public long StaffRouteStopMapIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("Employee")]
        [Select2("Employee", "String", false, "")]
        [LookUp("LookUps.ActiveEmployees")]
        //[LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Employee", "LookUps.Employee")]
        public KeyValueViewModel Staff { get; set; }
        public long? StaffID { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[CustomDisplay("Academic Year")]
        //[Select2("AcademicYear", "Numeric", false, "", optionalAttribute1: "")]
        //[LookUp("LookUps.AcademicYear")]
        public KeyValueViewModel AcademicYear { get; set; }
        public int? AcademicYearID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox, "medium-col-width")]
        [CustomDisplay("IsOneWay")]
        public bool? IsOneWay { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, "fullwidth alignleft")]
        [CustomDisplay("Forceassign(MaketheexistingrouteInactive&replacewiththeselectedone.)")]
        public bool? Approve { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("RouteType")]
        [Select2("OneSideRouteType", "Numeric", false, "RouteTypeChanges($event, $element, CRUDModel.ViewModel)", optionalAttribute1: "ng-disabled=!CRUDModel.ViewModel.IsOneWay")]
        [LookUp("LookUps.OneSideRouteType")]
        public KeyValueViewModel RouteType { get; set; }
        public byte? RouteTypeID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.HiddenWithLabel)]
        [DisplayName("")]
        public string space { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("ExcludedFromSeatAvailablity")]
        public bool? TermsAndConditions { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, attribs: "ng-change='RouteGroupChanges($event, $element,CRUDModel.ViewModel)'")]
        [CustomDisplay("RouteGroup")]
        [LookUp("LookUps.RouteGroup")]
        public string RouteGroup { get; set; }
        public int? RouteGroupID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine4 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("RouteToSchool")]
        [Select2("Route", "Numeric", false, "ToSchoolRouteChanges($event, $element, CRUDModel.ViewModel)", optionalAttribute1: "ng-disabled='CRUDModel.ViewModel.IsOneWay && CRUDModel.ViewModel.RouteType.Key != 1'")]
        [LookUp("LookUps.Route")]
        public KeyValueViewModel PickUpRoute { get; set; }
        public int? PickupRouteID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.HiddenWithLabel)]
        [DisplayName("")]
        public string space2 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("TransportStatus")]
        [Select2("TransportStatus", "Numeric", false, "", optionalAttribute1: "ng-disabled=!CRUDModel.ViewModel.TermsAndConditions")]
        [LookUp("LookUps.TransportStatus")]
        public KeyValueViewModel TransportStatus { get; set; }

        public long? TransportStatusID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine5 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("ToSchool")]
        [Select2("PickupStopMap", "Numeric", false, "StaffPickupStopChanges($event, $element, CRUDModel.ViewModel)", optionalAttribute1: "ng-disabled='CRUDModel.ViewModel.IsOneWay && CRUDModel.ViewModel.RouteType.Key != 1'")]
        [LookUp("LookUps.PickupStopMap")]
        public KeyValueViewModel PickupStopMap { get; set; }
        public long? PickupStopMapID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("RouteCode")]
        public string PickupBusNumber { get; set; }

        [ControlType(Framework.Enums.ControlTypes.FieldSet, "fullwidth", isFullColumn: false)]
        [DisplayName("")]
        public PickupSeatAvailabilityViewModel PickupSeatAvailability { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine6 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("RouteToHome")]
        [Select2("Route", "Numeric", false, "ToHomeRouteChanges($event, $element, CRUDModel.ViewModel)", optionalAttribute1: "ng-disabled='CRUDModel.ViewModel.IsOneWay && CRUDModel.ViewModel.RouteType.Key != 3'")]
        [LookUp("LookUps.Route")]
        public KeyValueViewModel DropRoute { get; set; }
        public int? DropStopRouteID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine7 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("ToHome")]
        [Select2("DropStopMap", "Numeric", false, "StaffDropStopChanges($event, $element, CRUDModel.ViewModel)", optionalAttribute1: "ng-disabled='CRUDModel.ViewModel.IsOneWay && CRUDModel.ViewModel.RouteType.Key != 3'")]
        [LookUp("LookUps.DropStopMap")]
        public KeyValueViewModel DropStopMap { get; set; }
        public long? DropStopMapID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("RouteCode")]
        public string DropBusNumber { get; set; }

        [ControlType(Framework.Enums.ControlTypes.FieldSet, "fullwidth", isFullColumn: false)]
        [DisplayName("")]
        public DropSeatAvailabilityViewModel DropSeatAvailability { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine8 { get; set; }
        
        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("DateFrom")]
        public string DateFromString { get; set; }
        public DateTime? DateFrom { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("DateTo")]
        public string DateToString { get; set; }
        public DateTime? DateTo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine9 { get; set; }

        

        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsActive")]
        public bool? IsActive { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine10 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DatePicker, attribs: "ng-disabled=CRUDModel.ViewModel.IsActive == true")]
        [CustomDisplay("CancelDate")]
        public string CancelDateString { get; set; }
        public System.DateTime? CancelDate { get; set; }


        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as StaffRouteStopMapDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<StaffRouteStopMapViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<StaffRouteStopMapDTO, StaffRouteStopMapViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var sDto = dto as StaffRouteStopMapDTO;
            var vm = Mapper<StaffRouteStopMapDTO, StaffRouteStopMapViewModel>.Map(dto as StaffRouteStopMapDTO);
            vm.DateFromString = sDto.DateFrom.Value.ToString(dateFormat, CultureInfo.InvariantCulture);
            vm.DateToString = sDto.DateTo.HasValue ? sDto.DateTo.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.TermsAndConditions = sDto.TermsAndConditions;
            vm.Approve = sDto.Approve;
            vm.CancelDateString = sDto.CancelDate.HasValue ? sDto.CancelDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.Staff = sDto.StaffID.HasValue ? new KeyValueViewModel()
            {
                Key = sDto.StaffID.ToString(),
                Value = sDto.StaffName
            } : null;
            vm.AcademicYear = sDto.AcademicYearID.HasValue ? new KeyValueViewModel()
            {
                Key = sDto.AcademicYearID.ToString(),
                Value = sDto.Academicyear.Value
            } : null;
            vm.PickupStopMap = sDto.PickupStopMapID.HasValue ? new KeyValueViewModel()
            {
                Key = sDto.PickupStopMapID.ToString(),
                Value = sDto.PickupStopMap.Value
            } : null;
            vm.DropStopMap = sDto.DropStopMapID.HasValue ? new KeyValueViewModel()
            {
                Key = sDto.DropStopMapID.ToString(),
                Value = sDto.DropStopMap.Value
            } : null;
            vm.RouteType = sDto.RouteTypeID.HasValue ? new KeyValueViewModel()
            {
                Key = sDto.RouteTypeID.ToString(),
                Value = sDto.RouteType.Value
            } : null;
            vm.DropStopMapID = sDto.DropStopMapID;
            if (sDto.DropRouteCode != null || sDto.DropSeatMap != null)
            {
                if (sDto.DropRouteCode != null)
                {
                    vm.DropBusNumber = sDto.DropRouteCode;
                }
                else
                {
                    vm.DropBusNumber = sDto.DropSeatMap.VehicleCode;
                };
            }
            if (sDto.PickUpRouteCode != null || sDto.PickupSeatMap != null)
            {
                if (sDto.PickUpRouteCode != null)
                {
                    vm.PickupBusNumber = sDto.PickUpRouteCode;
                }
                else
                {
                    vm.PickupBusNumber = sDto.PickupSeatMap.VehicleCode;
                };
            }
            vm.PickupStopMapID = sDto.PickupStopMapID;
            vm.PickUpRoute = sDto.PickupRouteID.HasValue ? new KeyValueViewModel()
            {
                Key = sDto.PickupRouteID.ToString(),
                Value = sDto.PickUpRoute.Value
            } : null;
            vm.DropRoute = sDto.DropStopRouteID.HasValue ? new KeyValueViewModel()
            {
                Key = sDto.DropStopRouteID.ToString(),
                Value = sDto.DropRoute.Value
            } : null;
            vm.TransportStatus = sDto.TransporStatusID.HasValue ? new KeyValueViewModel()
            {
                Key = sDto.TransporStatusID.ToString(),
                Value = sDto.TransporStatus.Value
            } : null;
            vm.PickupSeatAvailability = new PickupSeatAvailabilityViewModel()
            {
                SeatOccupied = sDto == null || sDto.PickupSeatMap == null || sDto.PickupSeatMap.SeatOccupied == null ? null : sDto.PickupSeatMap.SeatOccupied,
                SeatAvailability = sDto == null || sDto.PickupSeatMap == null || sDto.PickupSeatMap.SeatAvailability == null ? null : sDto.PickupSeatMap.SeatAvailability,
                AllowSeatCapacity = sDto == null || sDto.PickupSeatMap == null || sDto.PickupSeatMap.AllowSeatCapacity == null ? null : sDto.PickupSeatMap.AllowSeatCapacity,
                MaximumSeatCapacity = sDto == null || sDto.PickupSeatMap == null || sDto.PickupSeatMap.MaximumSeatCapacity == null ? null : sDto.PickupSeatMap.MaximumSeatCapacity,
            };
            vm.DropSeatAvailability = new DropSeatAvailabilityViewModel()
            {
                SeatOccupied = sDto == null || sDto.DropSeatMap == null || sDto.DropSeatMap.SeatOccupied == null ? null : sDto.DropSeatMap.SeatOccupied,
                SeatAvailability = sDto == null || sDto.DropSeatMap == null || sDto.DropSeatMap.SeatAvailability == null ? null : sDto.DropSeatMap.SeatAvailability,
                AllowSeatCapacity = sDto == null || sDto.DropSeatMap == null || sDto.DropSeatMap.AllowSeatCapacity == null ? null : sDto.DropSeatMap.AllowSeatCapacity,
                MaximumSeatCapacity = sDto == null || sDto.DropSeatMap == null || sDto.DropSeatMap.MaximumSeatCapacity == null ? null : sDto.DropSeatMap.MaximumSeatCapacity,
            };
            vm.RouteGroup = sDto.RouteGroupID.HasValue ? sDto.RouteGroupID.ToString() : null;

            return vm;
        }
        public override BaseMasterDTO ToDTO()
        {
            Mapper<StaffRouteStopMapViewModel, StaffRouteStopMapDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var dto = Mapper<StaffRouteStopMapViewModel, StaffRouteStopMapDTO>.Map(this);
            dto.IsActive = this.IsActive;
            dto.IsOneWay = this.IsOneWay;
            dto.Approve = this.Approve;
            dto.TermsAndConditions = this.TermsAndConditions;
            dto.CancelDate = string.IsNullOrEmpty(this.CancelDateString) ? (DateTime?)null : DateTime.ParseExact(this.CancelDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.StaffID = string.IsNullOrEmpty(this.Staff.Key) ? (long?)null : long.Parse(this.Staff.Key);
            dto.AcademicYearID = this.AcademicYear == null || string.IsNullOrEmpty(this.AcademicYear.Key) ? (int?)null : int.Parse(this.AcademicYear.Key);
            dto.RouteTypeID = this == null || this.RouteType == null || string.IsNullOrEmpty(this.RouteType.Key) ? (byte?)null : byte.Parse(this.RouteType.Key);
            dto.PickupStopMapID = this.PickupStopMap == null || string.IsNullOrEmpty(this.PickupStopMap.Key) ? (long?)null : long.Parse(this.PickupStopMap.Key);
            dto.DropStopMapID = this.DropStopMap == null || string.IsNullOrEmpty(this.DropStopMap.Key) ? (long?)null : long.Parse(this.DropStopMap.Key);
            dto.DateFrom = string.IsNullOrEmpty(this.DateFromString) ? (DateTime?)null : DateTime.ParseExact(DateFromString, dateFormat, CultureInfo.InvariantCulture);
            dto.DateTo = string.IsNullOrEmpty(this.DateToString) ? (DateTime?)null : DateTime.ParseExact(DateToString, dateFormat, CultureInfo.InvariantCulture);
            dto.PickupRouteID = this == null || this.PickUpRoute == null || string.IsNullOrEmpty(this.PickUpRoute.Key) ? (int?)null : int.Parse(this.PickUpRoute.Key);
            dto.DropStopRouteID = this == null || this.DropRoute == null || string.IsNullOrEmpty(this.DropRoute.Key) ? (int?)null : int.Parse(this.DropRoute.Key);
            dto.TransporStatusID = this == null || this.TransportStatus == null || string.IsNullOrEmpty(this.TransportStatus.Key) ? (byte?)null : byte.Parse(this.TransportStatus.Key);
            dto.PickupSeatMap = new SeatingAvailabilityDTO()
            {
                SeatOccupied = this.PickupSeatAvailability.SeatOccupied,
                SeatAvailability = this.PickupSeatAvailability.SeatAvailability,
                AllowSeatCapacity = this.PickupSeatAvailability.AllowSeatCapacity,
                MaximumSeatCapacity = this.PickupSeatAvailability.MaximumSeatCapacity,
            };
            dto.DropSeatMap = new SeatingAvailabilityDTO()
            {
                SeatOccupied = this.DropSeatAvailability.SeatOccupied,
                SeatAvailability = this.DropSeatAvailability.SeatAvailability,
                AllowSeatCapacity = this.DropSeatAvailability.AllowSeatCapacity,
                MaximumSeatCapacity = this.DropSeatAvailability.MaximumSeatCapacity,
            };
            return dto;
        }
        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<StaffRouteStopMapDTO>(jsonString);
        }
    }
}
