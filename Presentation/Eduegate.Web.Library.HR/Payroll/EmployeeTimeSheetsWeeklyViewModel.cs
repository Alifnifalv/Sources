using Eduegate.Domain;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.HR.Payroll;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Eduegate.Web.Library.HR.Payroll
{
    public class EmployeeTimeSheetsWeeklyViewModel : BaseMasterViewModel
    {
        public EmployeeTimeSheetsWeeklyViewModel()
        {
            Employee = new KeyValueViewModel();
            TimeSheets = new List<EmployeeTimeSheetsTimeViewModel>() { new EmployeeTimeSheetsTimeViewModel() };
            SpecialOTTimeTypeID = new Domain.Setting.SettingBL(null).GetSettingValue<byte?>("SPECIAL_OT_TIME_TYPE_ID", 0, null);
        }

        public long EmployeeTimeSheetIID { get; set; }

        public bool IsSelf { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Employee", "Numeric", false, "EmployeeChanges(CRUDModel.ViewModel)", false, OptionalAttribute1 = "ng-disabled=CRUDModel.ViewModel.IsSelf")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=ActiveEmployees", "LookUps.ActiveEmployees")]
        [CustomDisplay("EmployeeName")]
        public KeyValueViewModel Employee { get; set; }
        public long EmployeeID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, Attributes = "ng-Click=FillEmployeeData(CRUDModel.ViewModel) ng-hide=!CRUDModel.ViewModel.IsSelf")]
        [CustomDisplay("Fill Employee")]
        public string FillEmployee { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }


        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("DateFrom")]
        public string CollectionDateFromString { get; set; }

        public System.DateTime? CollectionDateFrom { get; set; }


        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("DateTo")]
        public string CollectionDateToString { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, Attributes = "ng-Click=FillTimesheetsData(CRUDModel.ViewModel)")]
        [CustomDisplay("Filter")]
        public string GenerateButton { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("TimeSheetDetails")]
        public List<EmployeeTimeSheetsTimeViewModel> TimeSheets { get; set; }

        public byte? SpecialOTTimeTypeID { get; set; }


        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as EmployeeTimeSheetsWeeklyDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<EmployeeTimeSheetsWeeklyViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<EmployeeTimeSheetsWeeklyDTO, EmployeeTimeSheetsWeeklyViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            Mapper<EmployeeTimeSheetsTimeDTO, EmployeeTimeSheetsTimeViewModel>.CreateMap();

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var timeFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("TimeFormatWithoutSecond");

            var ssDto = dto as EmployeeTimeSheetsWeeklyDTO;
            var vm = Mapper<EmployeeTimeSheetsWeeklyDTO, EmployeeTimeSheetsWeeklyViewModel>.Map(ssDto);
            
            vm.Employee = ssDto.EmployeeID != 0 ? new KeyValueViewModel()
            {
                Key = ssDto.EmployeeID.ToString(),
                Value = ssDto.Employee.Value
            } : new KeyValueViewModel();

            vm.TimeSheets = new List<EmployeeTimeSheetsTimeViewModel>();
            foreach (var sheetTime in ssDto.TimeSheet)
            {
                vm.TimeSheets.Add(new EmployeeTimeSheetsTimeViewModel()
                {
                    EmployeeTimeSheetIID = sheetTime.EmployeeTimeSheetIID,
                    Task = KeyValueViewModel.ToViewModel(sheetTime.Task),
                    TimesheetDateString = sheetTime.TimesheetDate.ToString(dateFormat, CultureInfo.InvariantCulture),
                    NormalHours = sheetTime.NormalHours,
                    OTHours = sheetTime.OTHours,
                    TimesheetEntryStatusID = sheetTime.TimesheetEntryStatusID,
                    Remarks = sheetTime.Remarks,
                    FromTimeString = sheetTime.FromTime.HasValue ? DateTime.Parse(sheetTime.FromTime.Value.ToString()).ToString(timeFormat) : null,
                    ToTimeString = sheetTime.ToTime.HasValue ? DateTime.Parse(sheetTime.ToTime.Value.ToString()).ToString(timeFormat) : null,
                    //TimesheetEntryStatus = sheetTime.TimesheetEntryStatusID.HasValue ? KeyValueViewModel.ToViewModel(sheetTime.TimesheetEntryStatus) : new KeyValueViewModel(),
                    EntryStatus = sheetTime.TimesheetEntryStatusID.HasValue ? sheetTime.TimesheetEntryStatusID.ToString() : null,
                    TimesheetTimeType = sheetTime.TimesheetTimeTypeID.HasValue ? sheetTime.TimesheetTimeTypeID.ToString() : null
                });
            }

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<EmployeeTimeSheetsWeeklyViewModel, EmployeeTimeSheetsWeeklyDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            Mapper<EmployeeTimeSheetsTimeViewModel, EmployeeTimeSheetsWeeklyDTO>.CreateMap();
            Mapper<EmployeeTimeSheetsTimeViewModel, EmployeeTimeSheetsTimeDTO>.CreateMap();
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var dto = Mapper<EmployeeTimeSheetsWeeklyViewModel, EmployeeTimeSheetsWeeklyDTO>.Map(this);

            dto.EmployeeID = this.Employee == null || string.IsNullOrEmpty(this.Employee.Key) || long.Parse(this.Employee.Key) == 0 ? 0 : long.Parse(this.Employee.Key);
            dto.EmployeeTimeSheetIID = this.EmployeeTimeSheetIID;

            foreach (var sheets in this.TimeSheets)
            {
                if (sheets.Task != null && !string.IsNullOrEmpty(sheets.Task.Key))
                {
                    //TimeSpan? fromTime = string.IsNullOrEmpty(sheets.FromTimeString) ? (TimeSpan?)null : DateTime.Parse(sheets.FromTimeString).TimeOfDay;
                    //if (fromTime != null && fromTime > TimeSpan.FromHours(12))
                    //{
                    //    fromTime -= TimeSpan.FromHours(12);
                    //}

                    //TimeSpan? toTime = string.IsNullOrEmpty(sheets.ToTimeString) ? (TimeSpan?)null : DateTime.Parse(sheets.ToTimeString).TimeOfDay;
                    //if (toTime != null && toTime > TimeSpan.FromHours(12))
                    //{
                    //    toTime -= TimeSpan.FromHours(12);
                    //}

                    dto.TimeSheet.Add(new EmployeeTimeSheetsTimeDTO()
                    {
                        TimesheetDate = DateTime.ParseExact(sheets.TimesheetDateString, dateFormat, CultureInfo.InvariantCulture),
                        //TimesheetEntryStatusID = string.IsNullOrEmpty(sheets.TimesheetEntryStatus?.Key) || byte.Parse(sheets.TimesheetEntryStatus?.Key) == 0 ? (byte?)null : byte.Parse(sheets.TimesheetEntryStatus?.Key),
                        TimesheetEntryStatusID = string.IsNullOrEmpty(sheets.EntryStatus) ? (byte?)null : byte.Parse(sheets.EntryStatus),
                        TimesheetTimeTypeID = string.IsNullOrEmpty(sheets.TimesheetTimeType) ? (byte?)null : byte.Parse(sheets.TimesheetTimeType),
                        EmployeeTimeSheetIID = sheets.EmployeeTimeSheetIID,
                        TaskID = long.Parse(sheets.Task.Key),
                        NormalHours = sheets.NormalHours,
                        OTHours = sheets.OTHours,
                        FromTime = string.IsNullOrEmpty(sheets.FromTimeString) ? (TimeSpan?)null : DateTime.Parse(sheets.FromTimeString).TimeOfDay,
                        ToTime = string.IsNullOrEmpty(sheets.ToTimeString) ? (TimeSpan?)null : DateTime.Parse(sheets.ToTimeString).TimeOfDay,
                        Remarks = sheets.Remarks
                    });
                }
            }

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<EmployeeTimeSheetsWeeklyDTO>(jsonString);
        }

    }
}