using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Services.Contracts.School.Transports;
using Eduegate.Framework.Translator;
using System.Globalization;
using Eduegate.Web.Library.Common;
using System.Collections.Generic;

namespace Eduegate.Web.Library.School.Transports
{
    public class StudentRouteStopMapViewModel : BaseMasterViewModel
    {
        public StudentRouteStopMapViewModel()
        {
            IsActive = true;
            IsOneWay = false;
            Approve = false;
            Termsandco = false;
            FeePeriod = new List<KeyValueViewModel>();
            //Student = new KeyValueViewModel();
            RouteType = new KeyValueViewModel();
            DropStopMap = new KeyValueViewModel();
            PickupStopMap = new KeyValueViewModel();
            PickupSeatAvailability = new PickupSeatAvailabilityViewModel();
            DropSeatAvailability = new DropSeatAvailabilityViewModel();
            TransportStatus = new KeyValueViewModel();
            PickUpRoute = new KeyValueViewModel();
            DropRoute = new KeyValueViewModel();
            AcademicYear = new KeyValueViewModel();
            RouteMonthlySplit = new List<StudentRouteMonthlySplitViewModel>() { new StudentRouteMonthlySplitViewModel() };
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            //DateFromString = DateTime.Now.ToString(dateFormat, CultureInfo.InvariantCulture);

        }
        public int IsFilterFeePeriod { get; set; } = 0;
        public long StudentRouteStopMapIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("TransportApplicationNo.")]
        public string ApplicationNumber { get; set; }
        public long? TransportApplctnStudentMapID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("Student")]
        [Select2("Student", "Numeric", false)]
        //[LookUp("LookUps.Student")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Student", "LookUps.Student")]
        public KeyValueViewModel Student { get; set; }
        public long? StudentID { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[CustomDisplay("Academic Year")]
        //[Select2("AcademicYear", "Numeric", false, "AcademicYearChanges(CRUDModel.ViewModel)", false)]
        //[LookUp("LookUps.AcademicYear")]
        public KeyValueViewModel AcademicYear { get; set; }

        public int? AcademicYearID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox, "medium-col-width")]
        [CustomDisplay("IsOneWay")]
        public bool? IsOneWay { get; set; }

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
        public bool? Termsandco { get; set; }

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
        [Select2("TransportStatus", "Numeric", false, "", optionalAttribute1: "ng-disabled=!CRUDModel.ViewModel.Termsandco")]
        [LookUp("LookUps.TransportStatus")]
        public KeyValueViewModel TransportStatus { get; set; }

        public long? TransportStatusID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine5 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("ToSchool")]
        [Select2("PickupStopMap", "Numeric", false, "PickupStopChanges($event, $element, CRUDModel.ViewModel)", optionalAttribute1: "ng-disabled='CRUDModel.ViewModel.IsOneWay && CRUDModel.ViewModel.RouteType.Key != 1'")]
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

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("ToHome")]
        [Select2("DropStopMap", "Numeric", false, "DropStopChanges($event, $element, CRUDModel.ViewModel)", optionalAttribute1: "ng-disabled='CRUDModel.ViewModel.IsOneWay && CRUDModel.ViewModel.RouteType.Key != 3'")]
        [LookUp("LookUps.DropStopMap")]
        public KeyValueViewModel DropStopMap { get; set; }
        public long? DropStopMapID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("RouteCode")]
        public string DropBusNumber { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine8 { get; set; }


        [ControlType(Framework.Enums.ControlTypes.FieldSet, "fullwidth", isFullColumn: false)]
        [DisplayName("")]
        public DropSeatAvailabilityViewModel DropSeatAvailability { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine9 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("DateFrom")]
        public string DateFromString { get; set; }
        public System.DateTime? DateFrom { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("DateTo")]
        public string DateToString { get; set; }
        public System.DateTime? DateTo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine10 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("FeePeriod", "Numeric", true, "PeriodChanges($event, $element, CRUDModel.ViewModel)")]
        [CustomDisplay("FeePeriods")]
        [LookUp("LookUps.FeePeriod")]
        public List<KeyValueViewModel> FeePeriod { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [DisplayName("Monthly Split")]
        public List<StudentRouteMonthlySplitViewModel> RouteMonthlySplit { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsActive")]
        public bool? IsActive { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine11 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DatePicker, attribs: "ng-disabled=CRUDModel.ViewModel.IsActive == true")]
        [CustomDisplay("CancelDate")]
        public string CancelDateString { get; set; }
        public System.DateTime? CancelDate { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextArea)]//, attribs: "ng-disabled=CRUDModel.ViewModel.IsActive == true"
        [CustomDisplay("Remarks")]
        public string Remarks { get; set; }

        public byte? SchoolID { get; set; }


        #region for email sending old data reference
        public string OldPickUpRoute { get; set; }

        public int? OldPickupRouteID { get; set; }

        public string OldPickupStopMap { get; set; }

        public long? OldPickupStopMapID { get; set; }

        public string OldDropStopMap { get; set; }

        public long? OldDropStopMapID { get; set; }

        public string OldDropRoute { get; set; }

        public int? OldDropStopRouteID { get; set; }
        #endregion


        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as StudentRouteStopMapDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<StudentRouteStopMapViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<StudentRouteStopMapDTO, StudentRouteStopMapViewModel>.CreateMap();
            Mapper<StudentRouteMonthlySplitDTO, StudentRouteMonthlySplitViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var sDto = dto as StudentRouteStopMapDTO;
            var vm = Mapper<StudentRouteStopMapDTO, StudentRouteStopMapViewModel>.Map(dto as StudentRouteStopMapDTO);
            vm.IsOneWay = sDto.IsOneWay;
            vm.Approve = sDto.Approve;
            vm.Termsandco = sDto.Termsandco;
            vm.IsActive = sDto.IsActive.HasValue ? sDto.IsActive : true;
            vm.Remarks = sDto.Remarks;
            vm.TransportApplctnStudentMapID = sDto.TransportApplctnStudentMapID.HasValue ? sDto.TransportApplctnStudentMapID : null;
            vm.Student = sDto.StudentID.HasValue ? new KeyValueViewModel()
            {
                Key = sDto.StudentID.ToString(),
                Value = sDto.Student.Value
            } : null;
            vm.AcademicYear = sDto.AcademicYearID.HasValue ? new KeyValueViewModel()
            {
                Key = sDto.AcademicYearID.ToString(),
                Value = sDto.AcademicYear.Value
            } : null;
            vm.RouteType = sDto.RouteTypeID.HasValue ? new KeyValueViewModel()
            {
                Key = sDto.RouteTypeID.ToString(),
                Value = sDto.RouteType.Value
            } : null;
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
            vm.DateToString = sDto.DateTo.HasValue ? sDto.DateTo.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.DateFromString = sDto.DateFrom.HasValue ? sDto.DateFrom.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.CancelDateString = sDto.CancelDate.HasValue ? sDto.CancelDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
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
            vm.FeePeriod = new List<KeyValueViewModel>();
            foreach (var listPrd in sDto.FeePeriod)
            {
                vm.FeePeriod.Add(new KeyValueViewModel()
                {
                    Key = listPrd.Key,
                    Value = listPrd.Value
                });
            }
            vm.RouteMonthlySplit = new List<StudentRouteMonthlySplitViewModel>();
            foreach (var mnthlySplt in sDto.MonthlySplitDTO)
            {
                vm.RouteMonthlySplit.Add(new StudentRouteMonthlySplitViewModel()
                {
                    IsRowSelected = mnthlySplt.IsRowSelected,
                    FeePeriodID = mnthlySplt.FeePeriodID,
                    MonthID = mnthlySplt.MonthID,
                    Year = mnthlySplt.Year,
                    MonthName = new DateTime(mnthlySplt.Year.Value, mnthlySplt.MonthID.Value, 1).ToString("MMM", CultureInfo.InvariantCulture).ToString() + " " + mnthlySplt.Year,
                    StudentRouteMonthlySplitIID = mnthlySplt.StudentRouteMonthlySplitIID,
                    StudentRouteStopMapID = mnthlySplt.StudentRouteStopMapID,
                    IsCollected = mnthlySplt.IsCollected
                });

            }
            vm.RouteGroup = sDto.RouteGroupID.HasValue ? sDto.RouteGroupID.ToString() : null;


            //for email send old data reference
            vm.OldPickUpRoute = sDto.OldPickupRouteID.HasValue ? sDto.OldPickUpRoute : null;
            vm.OldPickupRouteID = sDto.OldPickupRouteID;

            vm.OldDropRoute = sDto.OldDropStopRouteID.HasValue ? sDto.OldDropRoute : null;
            vm.OldDropStopRouteID = sDto.OldDropStopRouteID;

            vm.OldPickupStopMap = sDto.OldPickupStopMapID.HasValue ? sDto.OldPickupStopMap : null;
            vm.OldPickupStopMapID = sDto.OldPickupStopMapID;

            vm.OldDropStopMap = sDto.OldDropStopMapID.HasValue ? sDto.OldDropStopMap : null;
            vm.OldDropStopMapID = sDto.OldDropStopMapID;
            //end

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<StudentRouteStopMapViewModel, StudentRouteStopMapDTO>.CreateMap();
            Mapper<StudentRouteMonthlySplitViewModel, StudentRouteMonthlySplitDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var dto = Mapper<StudentRouteStopMapViewModel, StudentRouteStopMapDTO>.Map(this);
            dto.IsActive = this.IsActive;
            dto.IsOneWay = this.IsOneWay;
            dto.Approve = this.Approve == true ? true : false;
            dto.Remarks = this.Remarks;
            dto.Termsandco = this.Termsandco;
            dto.SchoolID = this.SchoolID;
            dto.ApplicationNumber = this.ApplicationNumber;
            dto.TransportApplctnStudentMapID = this.TransportApplctnStudentMapID.HasValue ? this.TransportApplctnStudentMapID : null;
            dto.StudentID = string.IsNullOrEmpty(this.Student.Key) ? (long?)null : long.Parse(this.Student.Key);
            dto.AcademicYearID = this.AcademicYear == null || string.IsNullOrEmpty(this.AcademicYear.Key) ? (int?)null : int.Parse(this.AcademicYear.Key);
            //dto.RouteTypeID = string.IsNullOrEmpty(this.RouteType.Key) ? (byte?)null : byte.Parse(this.RouteType.Key);
            dto.PickupStopMapID = this == null || this.PickupStopMap == null || string.IsNullOrEmpty(this.PickupStopMap.Key) ? (long?)null : long.Parse(this.PickupStopMap.Key);
            dto.DropStopMapID = this == null || this.DropStopMap == null || string.IsNullOrEmpty(this.DropStopMap.Key) ? (long?)null : long.Parse(this.DropStopMap.Key);
            //dto.RouteStopMapID = string.IsNullOrEmpty(this.RouteStopMap.Key) ? (long?)null : long.Parse(this.RouteStopMap.Key);
            //dto.DateFrom = string.IsNullOrEmpty(this.DateFromString) ? (DateTime?)null : DateTime.Parse(DateFromString);
            //dto.DateTo = string.IsNullOrEmpty(this.DateToString) ? (DateTime?)null : DateTime.Parse(DateToString);
            dto.DateFrom = string.IsNullOrEmpty(this.DateFromString) ? (DateTime?)null : DateTime.ParseExact(this.DateFromString, dateFormat, CultureInfo.InvariantCulture);
            dto.DateTo = this.IsActive == true ? (string.IsNullOrEmpty(this.DateToString) ? (DateTime?)null : DateTime.ParseExact(this.DateToString, dateFormat, CultureInfo.InvariantCulture)) : string.IsNullOrEmpty(this.CancelDateString) ? (DateTime?)null : DateTime.ParseExact(this.CancelDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.CancelDate = string.IsNullOrEmpty(this.CancelDateString) ? (DateTime?)null : DateTime.ParseExact(this.CancelDateString, dateFormat, CultureInfo.InvariantCulture);
            // dto.RouteTypeID = this == null || this.RouteType == null || string.IsNullOrEmpty(this.RouteType) ? (byte?)null : byte.Parse(this.RouteType);
            dto.RouteTypeID = this == null || this.RouteType == null || string.IsNullOrEmpty(this.RouteType.Key) ? (byte?)null : byte.Parse(this.RouteType.Key);
            dto.PickupRouteID = this == null || this.PickUpRoute == null || string.IsNullOrEmpty(this.PickUpRoute.Key) ? (int?)null : int.Parse(this.PickUpRoute.Key);
            dto.DropStopRouteID = this == null || this.DropRoute == null || string.IsNullOrEmpty(this.DropRoute.Key) ? (int?)null : int.Parse(this.DropRoute.Key);
            dto.TransporStatusID = this == null || this.TransportStatus == null || string.IsNullOrEmpty(this.TransportStatus.Key) ? (byte?)null : byte.Parse(this.TransportStatus.Key);
            dto.FeePeriod = new List<KeyValueDTO>();
            foreach (var listPrd in this.FeePeriod)
            {
                dto.FeePeriod.Add(new KeyValueDTO()
                {
                    Key = listPrd.Key,
                    Value = listPrd.Value
                });
            }
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
            dto.MonthlySplitDTO = new List<StudentRouteMonthlySplitDTO>();
            foreach (var mnthlySplt in this.RouteMonthlySplit)
            {
                if (mnthlySplt.IsRowSelected == true)
                {
                    dto.MonthlySplitDTO.Add(new StudentRouteMonthlySplitDTO()
                    {
                        FeePeriodID = mnthlySplt.FeePeriodID,
                        MonthID = mnthlySplt.MonthID,
                        Year = mnthlySplt.Year,
                        StudentRouteMonthlySplitIID = mnthlySplt.StudentRouteMonthlySplitIID,
                        StudentRouteStopMapID = mnthlySplt.StudentRouteStopMapID
                    });
                }
            }

            //passing old data for reference
            dto.OldPickupStopMapID = this.OldPickupStopMapID.HasValue ? this.OldPickupStopMapID : null;
            dto.OldDropStopMapID = this.OldDropStopMapID.HasValue ? this.OldDropStopMapID : null;
            dto.OldPickupRouteID = this.OldPickupRouteID.HasValue ? this.OldPickupRouteID : null;
            dto.OldDropStopRouteID = this.OldDropStopRouteID.HasValue ? this.OldDropStopRouteID : null;

            dto.OldPickupStopMap = this.OldPickupStopMap != null ? this.OldPickupStopMap : null;
            dto.OldPickUpRoute = this.OldPickUpRoute != null ? this.OldPickUpRoute : null;
            dto.OldDropRoute = this.OldDropRoute != null ? this.OldDropRoute : null;
            dto.OldDropStopMap = this.OldDropStopMap != null ? this.OldDropStopMap : null;

            //end

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<StudentRouteStopMapDTO>(jsonString);
        }

    }
}