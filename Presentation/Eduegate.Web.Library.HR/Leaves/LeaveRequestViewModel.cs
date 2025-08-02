using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.HR.Leaves;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Eduegate.Web.Library.HR.Leaves
{
    public class LeaveRequestViewModel : BaseMasterViewModel
    {
        public long LeaveApplicationIID { get; set; }
        public bool IsDisable { get; set; } = false;

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.DropDown)]
        //[DisplayName("Company")]
        //[LookUp("LookUps.Company")]
        //public string Company { get; set; }
        //public int? CompanyID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "onecol-header-left")]
        [Select2("Employee", "String", false, "")]
        [CustomDisplay("Employee")]
        //[LookUp("LookUps.Employee")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Employee", "LookUps.Employee")]
        public KeyValueViewModel Employee { get; set; }
        public long? EmployeeID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, "medium-col-width", attribs: "ng-change=LeaveTypeChanges($event,CRUDModel.ViewModel)")]
        [CustomDisplay("LeaveType")]
        [LookUp("LookUps.LeaveType")]
        public string LeaveType { get; set; }
        public int? LeaveTypeID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine0 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsThisIsAHalfDay")]
        public bool? IsHalfDay { get; set; }


        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsLeaveWithoutPay")]
        public bool? IsLeaveWithoutPay { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker, attribs: "ng-disabled='CRUDModel.ViewModel.IsDisable'")]
        [CustomDisplay("FromDate")]
        public string FromDateString { get; set; }
        public System.DateTime? FromDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker, attribs: "ng-disabled='CRUDModel.ViewModel.IsDisable'")]
        [CustomDisplay("ToDate")]
        public string ToDateString { get; set; }
        public System.DateTime? ToDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("ExpectedRejoiningDate")]
        public string ExpectedRejoiningDateString { get; set; }
        public DateTime? ExpectedRejoiningDate { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Rejoining Date")]
        public string RejoiningDateString { get; set; }
        public DateTime? RejoiningDate { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine4 { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.LeaveSession")]
        [CustomDisplay("FromSession")]
        public string FromSessionString { get; set; }
        public byte? FromSessionID { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.LeaveSession")]
        [CustomDisplay("ToSession")]
        public string ToSessionString { get; set; }
        public byte? ToSessionID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine5 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.LeaveStatus")]
        [CustomDisplay("LeaveStatus")]
        public string LeaveStatus { get; set; }
        public byte? LeaveStatusID { get; set; }

        [MaxLength(200, ErrorMessage = "Maximum Length should be within 200!")]
        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [CustomDisplay("Reason")]
        public string OtherReason { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as LeaveRequestDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<LeaveRequestViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<LeaveRequestDTO, LeaveRequestViewModel>.CreateMap();
            var lvDtO = dto as LeaveRequestDTO;
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var vm = Mapper<LeaveRequestDTO, LeaveRequestViewModel>.Map(dto as LeaveRequestDTO);
            //vm.Company = lvDtO.CompanyID.ToString();
            vm.LeaveType = lvDtO.LeaveTypeID.ToString();
            vm.Employee = new KeyValueViewModel() { Key = lvDtO.EmployeeID.ToString(), Value = lvDtO.StaffName };
            vm.LeaveStatus = lvDtO.LeaveStatusID.ToString();
            vm.FromDateString = lvDtO.FromDate.HasValue ? lvDtO.FromDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.ToDateString = lvDtO.ToDate.HasValue ? lvDtO.ToDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.RejoiningDateString = lvDtO.RejoiningDate.HasValue ? lvDtO.RejoiningDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.FromSessionString = lvDtO.FromSessionID.ToString();
            vm.ToSessionString = lvDtO.ToSessionID.ToString();
            vm.IsLeaveWithoutPay = lvDtO.IsLeaveWithoutPay;
            vm.IsHalfDay = lvDtO.IsHalfDay;
            vm.ExpectedRejoiningDateString = lvDtO.ExpectedRejoiningDate.HasValue ? lvDtO.ExpectedRejoiningDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<LeaveRequestViewModel, LeaveRequestDTO>.CreateMap();
            var dto = Mapper<LeaveRequestViewModel, LeaveRequestDTO>.Map(this);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            //dto.CompanyID=string.IsNullOrEmpty(this.Company)?(int?)null : int.Parse(this.Company);
            dto.LeaveTypeID= string.IsNullOrEmpty(this.LeaveType) ? (int?)null : int.Parse(this.LeaveType);
            dto.EmployeeID = string.IsNullOrEmpty(this.Employee.Key) ? (int?)null : int.Parse(this.Employee.Key);
            dto.LeaveStatusID = string.IsNullOrEmpty(this.LeaveStatus) ? (byte?)null : byte.Parse(this.LeaveStatus);
            dto.FromDate = string.IsNullOrEmpty(this.FromDateString) ? (DateTime?)null : DateTime.ParseExact(this.FromDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.ToDate = string.IsNullOrEmpty(this.ToDateString) ? (DateTime?)null : DateTime.ParseExact(this.ToDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.RejoiningDate = string.IsNullOrEmpty(this.RejoiningDateString) ? (DateTime?)null : DateTime.ParseExact(this.RejoiningDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.FromSessionID = string.IsNullOrEmpty(this.FromSessionString) ? (byte?)null : byte.Parse(this.FromSessionString);
            dto.ToSessionID = string.IsNullOrEmpty(this.ToSessionString) ? (byte?)null : byte.Parse(this.ToSessionString);
            dto.IsLeaveWithoutPay = this.IsLeaveWithoutPay;
            dto.IsHalfDay = this.IsHalfDay;
            dto.ExpectedRejoiningDate = string.IsNullOrEmpty(this.ExpectedRejoiningDateString) ? (DateTime?)null : DateTime.ParseExact(this.ExpectedRejoiningDateString, dateFormat, CultureInfo.InvariantCulture);

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<LeaveRequestDTO>(jsonString);
        }

    }
}
