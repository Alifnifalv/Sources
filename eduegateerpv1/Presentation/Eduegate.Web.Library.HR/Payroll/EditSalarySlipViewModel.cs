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
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "EditSalarySlipMap", "CRUDModel.ViewModel")]
    [DisplayName("Salary Slip Generation")]
    public class EditSalarySlipViewModel : BaseMasterViewModel
    {
        public EditSalarySlipViewModel()
        {
            SlipComponents = new List<SalarySlipComponentViewModel>() { new SalarySlipComponentViewModel() };
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            SlipDateString = DateTime.Now.ToString(dateFormat, CultureInfo.InvariantCulture);
           
        }

        public long SalarySlipIID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [DisplayName("Employee Name")]
        public string EmployeeName { get; set; }
        public long? EmployeeID { get; set; }

        public string EmployeeCode { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [DisplayName("Slip Date")]
        public string SlipDateString { get; set; }
        public DateTime? SlipDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, Attributes = "ng-bind=GetTotalSalaryAmount(CRUDModel.ViewModel.SlipComponents) | number")]
        [DisplayName("Total Amount")]
        public decimal? Amount { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", attribs: "ng-click=\"ModifySalarySlip(CRUDModel.ViewModel)\"")]
        [CustomDisplay("Regenerate")]
        public string RegenerateButton { get; set; }


        public long? BranchID { get; set; }
        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", attribs: "ng-click=\"ReviewSalarySlip()\"")]
        //[CustomDisplay("Review")]
        //public string ReviewButton { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [DisplayName("EditSalarySlip")]
        public List<SalarySlipComponentViewModel> SlipComponents { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as SalarySlipDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<EditSalarySlipViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<SalarySlipDTO, EditSalarySlipViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            Mapper<SalarySlipDTO, SalarySlipViewModel>.CreateMap();
            Mapper<SalarySlipDTO, SalarySlipComponentViewModel>.CreateMap();
            var ssDto = dto as SalarySlipDTO;
            var vm = Mapper<SalarySlipDTO, EditSalarySlipViewModel>.Map(ssDto);
            vm.EmployeeID = ssDto.EmployeeID;
            vm.EmployeeName = ssDto.EmployeeName;
            vm.EmployeeCode = ssDto.EmployeeCode;
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            vm.SlipDateString = (vm.SlipDate.HasValue ? vm.SlipDate.Value : DateTime.Now).ToString(dateFormat, CultureInfo.InvariantCulture);
            vm.SlipComponents = new List<SalarySlipComponentViewModel>();
            foreach (var salaryvm in ssDto.SalaryComponent)
            {
                vm.SlipComponents.Add(new SalarySlipComponentViewModel()
                {
                    SalarySlipIID = salaryvm.SalarySlipIID,
                    SalaryComponentID = string.IsNullOrEmpty(salaryvm.SalaryComponent.Key) ? (int?)null : int.Parse(salaryvm.SalaryComponent.Key),
                    SalaryComponent = KeyValueViewModel.ToViewModel(salaryvm.SalaryComponent),
                    Deduction = salaryvm.Amount.Value < 0 ? salaryvm.Amount.Value * -1 : (decimal?)null,
                    Earnings = salaryvm.Amount.Value > 0 ? salaryvm.Amount.Value * 1 : (decimal?)null,
                    Description=salaryvm.Description,
                    NoOfDays = salaryvm.NoOfDays,
                    NoOfHours = salaryvm.NoOfHours,
                    ReportContentID = salaryvm.ReportContentID,
                });
            }
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<EditSalarySlipViewModel, SalarySlipDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            Mapper<SalarySlipViewModel, SalarySlipDTO>.CreateMap();
            Mapper<SalarySlipComponentViewModel, SalarySlipDTO>.CreateMap();
            var dto = Mapper<EditSalarySlipViewModel, SalarySlipDTO>.Map(this);
            dto.SalarySlipIID = this.SalarySlipIID;
            dto.EmployeeID = this.EmployeeID;
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            dto.SlipDate = string.IsNullOrEmpty(this.SlipDateString) ? (DateTime?)null : DateTime.ParseExact(this.SlipDateString, dateFormat, CultureInfo.InvariantCulture);

            dto.SalaryComponent = new List<SalarySlipComponentDTO>();
            if (dto.SalaryComponent != null)
            {
                foreach (var salrydto in this.SlipComponents)
                {
                    if (salrydto.SalaryComponent != null && !string.IsNullOrEmpty(salrydto.SalaryComponent.Key))
                    {
                        dto.SalaryComponent.Add(new SalarySlipComponentDTO()
                        {
                            SalarySlipIID = salrydto.SalarySlipIID,
                            SalaryComponentID = string.IsNullOrEmpty(salrydto.SalaryComponent.Key) ? (int?)null : int.Parse(salrydto.SalaryComponent.Key),
                            Amount = salrydto.Earnings.HasValue ? salrydto.Earnings : salrydto.Deduction.HasValue ? salrydto.Deduction * -1 : (decimal?)null,
                            Description = salrydto.Description,
                            NoOfDays = salrydto.NoOfDays,
                            NoOfHours = salrydto.NoOfHours,
                            ReportContentID = salrydto.ReportContentID
                        });
                    }
                }
            }
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<SalarySlipDTO>(jsonString);
        }
    }
}