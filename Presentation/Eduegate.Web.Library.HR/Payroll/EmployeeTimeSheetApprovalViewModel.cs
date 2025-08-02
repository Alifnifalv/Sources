using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.HR.Payroll;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Eduegate.Web.Library.HR.Payroll
{
    public class EmployeeTimeSheetApprovalViewModel : BaseMasterViewModel
    {
        public EmployeeTimeSheetApprovalViewModel()
        {
            IsSelected = false;
            IsExpand = false;
            Employee = new KeyValueViewModel();
            TimesheetTimeType = new KeyValueViewModel();
            TimesheetApprovalStatus = new KeyValueViewModel();
            TimeSheetTimeDetails = new List<EmployeeTimeSheetApprovalTimeViewModel>() { new EmployeeTimeSheetApprovalTimeViewModel() };
        }

        public long EmployeeTimeSheetApprovalIID { get; set; }

        public long? EmployeeTimeSheetID { get; set; }

        public bool IsSelected { get; set; }

        public bool IsExpand { get; set; }

        public long? EmployeeID { get; set; }
        public KeyValueViewModel Employee { get; set; }

        public DateTime? TimesheetDate { get; set; }
        public string TimesheetDateString { get; set; }

        public decimal? TotalNormalHours { get; set; }
        public decimal? TotalOverTimeHours { get; set; }

        public byte? TimesheetTimeTypeID { get; set; }
        public KeyValueViewModel TimesheetTimeType { get; set; }

        public decimal? ApprovedNormalHours { get; set; }

        public decimal? ApprovedOTHours { get; set; }

        public byte? TimesheetApprovalStatusID { get; set; }
        public KeyValueViewModel TimesheetApprovalStatus { get; set; }

        public string Remarks { get; set; }

        public DateTime? TimesheetDateFrom { get; set; }
        public string DateFromString { get; set; }

        public DateTime? TimesheetDateTo { get; set; }
        public string DateToString { get; set; }

        public List<EmployeeTimeSheetApprovalTimeViewModel> TimeSheetTimeDetails { get; set; }


        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as EmployeeTimeSheetApprovalDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<EmployeeTimeSheetApprovalViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<EmployeeTimeSheetApprovalDTO, EmployeeTimeSheetApprovalViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var aprvlDTO = dto as EmployeeTimeSheetApprovalDTO;
            var vm = Mapper<EmployeeTimeSheetApprovalDTO, EmployeeTimeSheetApprovalViewModel>.Map(aprvlDTO);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var timeFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("TimeFormatWithoutSecond");

            vm.Employee = aprvlDTO.EmployeeID.HasValue ? new KeyValueViewModel()
            {
                Key = aprvlDTO.EmployeeID.ToString(),
                Value = aprvlDTO.EmployeeName
            } : new KeyValueViewModel();

            vm.TimeSheetTimeDetails = new List<EmployeeTimeSheetApprovalTimeViewModel>();
            foreach (var sheetTime in aprvlDTO.TimeSheetDetails)
            {
                vm.TimeSheetTimeDetails.Add(new EmployeeTimeSheetApprovalTimeViewModel()
                {
                    EmployeeTimeSheetIID = sheetTime.EmployeeTimeSheetIID,
                    Remarks = sheetTime.Remarks,
                    FromTimeString = sheetTime.FromTime.HasValue ? DateTime.Parse(sheetTime.FromTime.Value.ToString()).ToString(timeFormat) : null,
                    ToTimeString = sheetTime.ToTime.HasValue ? DateTime.Parse(sheetTime.ToTime.Value.ToString()).ToString(timeFormat) : null,
                });
            }

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<EmployeeTimeSheetApprovalViewModel, EmployeeTimeSheetApprovalDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = Mapper<EmployeeTimeSheetApprovalViewModel, EmployeeTimeSheetApprovalDTO>.Map(this);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var timeFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("TimeFormatWithoutSecond");

            dto.EmployeeTimeSheetApprovalIID = this.EmployeeTimeSheetApprovalIID;
            dto.EmployeeID = this.Employee != null && !string.IsNullOrEmpty(this.Employee.Key) ? long.Parse(this.Employee.Key) : (long?)null;
            dto.NormalHours = this.ApprovedNormalHours;
            dto.OTHours = this.ApprovedOTHours;
            dto.TimesheetDateFrom = string.IsNullOrEmpty(this.DateFromString) ? (DateTime?)null : DateTime.ParseExact(this.DateFromString, dateFormat, CultureInfo.InvariantCulture);
            dto.TimesheetDateTo = string.IsNullOrEmpty(this.DateToString) ? (DateTime?)null : DateTime.ParseExact(this.DateToString, dateFormat, CultureInfo.InvariantCulture);
            dto.TimesheetTimeTypeID = this.TimesheetTimeType != null && !string.IsNullOrEmpty(this.TimesheetTimeType.Key) ? byte.Parse(this.TimesheetTimeType.Key) : (byte?)null;
            dto.TimesheetApprovalStatusID = this.TimesheetApprovalStatus != null && !string.IsNullOrEmpty(this.TimesheetApprovalStatus.Key) ? byte.Parse(this.TimesheetApprovalStatus.Key) : (byte?)null;
            dto.Remarks = this.Remarks;

            dto.TimeSheetDetails = new List<EmployeeTimeSheetApprovalTimeDTO>();

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<EmployeeTimeSheetApprovalDTO>(jsonString);
        }

    }
}