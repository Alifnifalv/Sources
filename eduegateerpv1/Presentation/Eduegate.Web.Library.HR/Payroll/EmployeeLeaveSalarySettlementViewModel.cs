using Eduegate.Domain.Entity.HR.Payroll;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.HR.Leaves;
using Eduegate.Services.Contracts.HR.Payroll;
using Eduegate.Web.Library.ViewModels.Payroll;
using Newtonsoft.Json;
using System.Globalization;
using Eduegate.Services.Contracts.Catalog;
using System.Runtime.Serialization;

namespace Eduegate.Web.Library.HR.Payroll
{
    public class EmployeeLeaveSalarySettlementViewModel : BaseMasterViewModel
    {
        public EmployeeLeaveSalarySettlementViewModel()
        {
            Employee = new KeyValueViewModel();
            MCTabSalaryStructure = new TabSalaryStructureViewModel();
            // MCTabLeaveSalaryStructure = new TabLeaveSalaryStructureViewModel();
            MCTabEmployeeLeaveDatails = new TabEmployeeLeaveDatailsViewModel();
        }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Leave Salary Settlement Date")]

        public string LeaveSalarySettlementDateToString { get; set; }

        public System.DateTime? LeaveSalarySettlementDate { get; set; }


        [ControlType(Framework.Enums.ControlTypes.DatePicker,Attributes = "ng-change='CalculationDateChanges(CRUDModel.ViewModel,1)'")] 
        [CustomDisplay("Salary Calculation Date")]
        public string SalaryCalculationDateString { get; set; }
        public System.DateTime? SalaryCalculationDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine13 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("AllEmployee", "Numeric", false)]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=AllEmployee", "LookUps.AllEmployee")]
        [CustomDisplay("EmployeeName")]
        public KeyValueViewModel Employee { get; set; }
        public long? EmployeeID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("No.of Days in a month for calculation")]
        public decimal? NoofDaysInTheMonth { get; set; } 

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine12 { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Label)]
        //[CustomDisplay("Employee Code")]
        public string EmployeeCode { get; set; }



        //[ControlType(Framework.Enums.ControlTypes.Label)]
        //[CustomDisplay("Date Of Joining")]
        public string DateOfJoiningString { get; set; }
        public System.DateTime? DateOfJoining { get; set; }
        public decimal? NoofDaysInTheMonthEoSB { get; set; }
        public string DateOfLeavingString { get; set; }
        public System.DateTime? DateOfLeavingDate { get; set; }
        public decimal? EndofServiceDays { get; set; }
        public long? BranchID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine14 { get; set; }
        [ControlType(Framework.Enums.ControlTypes.Button, Attributes = "ng-Click=FillDetails(CRUDModel.ViewModel,1)")]
        [CustomDisplay("Fill")]
        public string GenerateButton { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, Attributes = "ng-Click=SaveSalarySettlement(CRUDModel.ViewModel,1)")]
        [CustomDisplay("Submit")]
        public string SubmitButton { get; set; }

        [ControlType(Framework.Enums.ControlTypes.HtmlLabel, Attributes2 = "labelinfo-custom")]
        [CustomDisplay("Remarks")]
        public string Remarks { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "MCTabEmployeeLeaveDatails", "MCTabEmployeeLeaveDatails")]
        [CustomDisplay("Leave Date Details")]
        public TabEmployeeLeaveDatailsViewModel MCTabEmployeeLeaveDatails { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "MCTabSalaryStructure", "MCTabSalaryStructure")]
        [CustomDisplay("Salary Structure")]
        public TabSalaryStructureViewModel MCTabSalaryStructure { get; set; }


        //[ContainerType(Framework.Enums.ContainerTypes.Tab, "MCTabLeaveSalaryStructure", "MCTabLeaveSalaryStructure")]
        //[CustomDisplay("Leave Salary Structure")]
        //public TabLeaveSalaryStructureViewModel MCTabLeaveSalaryStructure { get; set; }


        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as EmployeeSettlementDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<EmployeeLeaveSalarySettlementViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<EmployeeSettlementDTO, EmployeeLeaveSalarySettlementViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();


            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var ssDto = dto as EmployeeSettlementDTO;
            var vm = Mapper<EmployeeSettlementDTO, EmployeeLeaveSalarySettlementViewModel>.Map(ssDto);

            vm.LeaveSalarySettlementDateToString = (ssDto.EmployeeSettlementDate.HasValue ? ssDto.EmployeeSettlementDate.Value : DateTime.Now).ToString(dateFormat, CultureInfo.InvariantCulture);
            vm.SalaryCalculationDateString = (ssDto.SalaryCalculationDate.HasValue ? ssDto.SalaryCalculationDate.Value : DateTime.Now).ToString(dateFormat, CultureInfo.InvariantCulture);

            vm.Employee = KeyValueViewModel.ToViewModel(ssDto.Employee);
            vm.EmployeeID = ssDto.EmployeeID;
            vm.EmployeeCode = ssDto.EmployeeCode;
            vm.Remarks = ssDto.Remarks; 
            vm.MCTabSalaryStructure = new TabSalaryStructureViewModel();
            vm.MCTabEmployeeLeaveDatails = new TabEmployeeLeaveDatailsViewModel();

            vm.MCTabEmployeeLeaveDatails = new TabEmployeeLeaveDatailsViewModel()
            {
                AnnualLeaveEntitilements = ssDto.AnnualLeaveEntitilements,
                LeaveDueFromString = (ssDto.LeaveDueFrom.HasValue ? ssDto.LeaveDueFrom.Value : DateTime.Now).ToString(dateFormat, CultureInfo.InvariantCulture),
                LeaveDueFrom = ssDto.LeaveDueFrom,
                NoofSalaryDays = ssDto.NoofSalaryDays,
                LeaveStartDateString = (ssDto.LeaveStartDate.HasValue ? ssDto.LeaveStartDate.Value : DateTime.Now).ToString(dateFormat, CultureInfo.InvariantCulture),
                LeaveEndDateString = (ssDto.LeaveEndDate.HasValue ? ssDto.LeaveEndDate.Value : DateTime.Now).ToString(dateFormat, CultureInfo.InvariantCulture),
                DateOfJoiningString = (ssDto.DateOfJoining.HasValue ? ssDto.DateOfJoining.Value : DateTime.Now).ToString(dateFormat, CultureInfo.InvariantCulture),
                LeaveStartDate = ssDto.LeaveStartDate,
                LeaveEndDate = ssDto.LeaveEndDate,
                NoofDaysInTheMonth = ssDto.NoofDaysInTheMonth,
                EarnedLeave = ssDto.EarnedLeave,
                LossofPay = ssDto.LossofPay
            };
            vm.MCTabSalaryStructure.SalaryComponents = new List<EmployeeSettlementComponentsViewModel>();
            foreach (var salaryComp in ssDto.SalarySlipDTOs)
            {
                vm.MCTabSalaryStructure.SalaryComponents.Add(new EmployeeSettlementComponentsViewModel()
                {
                    SalaryComponentID = string.IsNullOrEmpty(salaryComp.SalaryComponentKeyValue.Key) ? (int?)null : int.Parse(salaryComp.SalaryComponentKeyValue.Key),
                    SalaryComponent = KeyValueViewModel.ToViewModel(salaryComp.SalaryComponentKeyValue),
                    Deduction = salaryComp.Amount.Value < 0 ? salaryComp.Amount.Value * -1 : (decimal?)null,
                    Earnings = salaryComp.Amount.Value > 0 ? salaryComp.Amount.Value * 1 : (decimal?)null,
                    NoOfDays = salaryComp.NoOfDays,
                    Description = salaryComp.Description,
                });
            }
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<EmployeeLeaveSalarySettlementViewModel, EmployeeSettlementDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = Mapper<EmployeeLeaveSalarySettlementViewModel, EmployeeSettlementDTO>.Map(this);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            dto.EmployeeSettlementDate = string.IsNullOrEmpty(this.LeaveSalarySettlementDateToString) ? (DateTime?)null : DateTime.ParseExact(this.LeaveSalarySettlementDateToString, dateFormat, CultureInfo.InvariantCulture);
            dto.SalaryCalculationDate = string.IsNullOrEmpty(this.SalaryCalculationDateString) ? (DateTime?)null : DateTime.ParseExact(this.SalaryCalculationDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.EmployeeID = string.IsNullOrEmpty(this.Employee.Key) ? (long?)null : long.Parse(this.Employee.Key);
            dto.Employee = new KeyValueDTO { Key = this.Employee.Key, Value = this.Employee.Value };
            dto.AnnualLeaveEntitilements = this.MCTabEmployeeLeaveDatails.AnnualLeaveEntitilements;

            dto.LeaveDueFrom = string.IsNullOrEmpty(this.MCTabEmployeeLeaveDatails.LeaveDueFromString) ? (DateTime?)null : DateTime.ParseExact(this.MCTabEmployeeLeaveDatails.LeaveDueFromString, dateFormat, CultureInfo.InvariantCulture);

            dto.NoofSalaryDays = this.MCTabEmployeeLeaveDatails.NoofSalaryDays;
            dto.LeaveStartDate = string.IsNullOrEmpty(this.MCTabEmployeeLeaveDatails.LeaveStartDateString) ? (DateTime?)null : DateTime.ParseExact(this.MCTabEmployeeLeaveDatails.LeaveStartDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.LeaveEndDate = string.IsNullOrEmpty(this.MCTabEmployeeLeaveDatails.LeaveEndDateString) ? (DateTime?)null : DateTime.ParseExact(this.MCTabEmployeeLeaveDatails.LeaveEndDateString, dateFormat, CultureInfo.InvariantCulture);

            dto.NoofDaysInTheMonth = this.MCTabEmployeeLeaveDatails.NoofDaysInTheMonth;
            dto.EarnedLeave = this.MCTabEmployeeLeaveDatails.EarnedLeave;
            dto.LossofPay = this.MCTabEmployeeLeaveDatails.LossofPay;


            foreach (var salaryComp in this.MCTabSalaryStructure.SalaryComponents)
            {
                if (salaryComp.SalaryComponent != null && !string.IsNullOrEmpty(salaryComp.SalaryComponent.Key))
                {
                    dto.SalarySlipDTOs.Add(new SalarySlipDTO()
                    {
                        SalaryComponentID = string.IsNullOrEmpty(salaryComp.SalaryComponent.Key) ? (int?)null : int.Parse(salaryComp.SalaryComponent.Key),
                        Amount = salaryComp.Earnings.HasValue ? salaryComp.Earnings : salaryComp.Deduction.HasValue ? salaryComp.Deduction * -1 : (decimal?)null,
                        NoOfDays = salaryComp.NoOfDays,
                        Description = salaryComp.Description
                    });
                }
            }
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<EmployeeSettlementDTO>(jsonString);
        }


    }
}
