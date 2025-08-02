using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Eduegate.Domain.Entity.HR.Payroll;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel.DataAnnotations;
using Eduegate.Domain.Entity.HR;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.HR.Payroll;
using Newtonsoft.Json;
using System.Globalization;
using Microsoft.Reporting.Map.WebForms.BingMaps;
using Eduegate.Domain.Frameworks;
using Eduegate.Web.Library.HR.Payroll;

namespace Eduegate.Web.Library.HR.Payroll
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "EmployeeDepartmentAccountMap", "CRUDModel.ViewModel")]
    [DisplayName("Employee Department Account Maps")]
    public class EmployeeDepartmentAccountMapViewModel : BaseMasterViewModel
    {
        public EmployeeDepartmentAccountMapViewModel()
        {
            SalaryComponent = new KeyValueViewModel();
            EmployeeDeptAccountMapDetail = new List<EmployeeDeptAccountMapDetailViewModel>() { new EmployeeDeptAccountMapDetailViewModel() };
            //EmpDepartmentAccountMap = new EmployeeDeptAccountTabViewModel();
        }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("SalaryComponent", "Numeric", false)]
        [LookUp("LookUps.SalaryComponent")]
        [CustomDisplay("Components")]
        public KeyValueViewModel SalaryComponent { get; set; }
        public int? SalaryComponentID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("Details")]
        public List<EmployeeDeptAccountMapDetailViewModel> EmployeeDeptAccountMapDetail { get; set; }

        //[ContainerType(Framework.Enums.ContainerTypes.Tab, "EmpDepartmentAccountMap", "EmpDepartmentAccountMap")]
        //[CustomDisplay("Department Account Map Details")]
        //public EmployeeDeptAccountTabViewModel EmpDepartmentAccountMap { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as EmployeeDepartmentAccountMapDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<EmployeeDepartmentAccountMapViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<EmployeeDepartmentAccountMapDTO, EmployeeDepartmentAccountMapViewModel>.CreateMap();
            Mapper<EmployeeDeptAccountMapDetailDTO, EmployeeDeptAccountMapDetailViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var sDto = dto as EmployeeDepartmentAccountMapDTO;
            var vm = Mapper<EmployeeDepartmentAccountMapDTO, EmployeeDepartmentAccountMapViewModel>.Map(sDto);
            vm.SalaryComponentID = sDto.SalaryComponentID;
            vm.SalaryComponent = sDto.SalaryComponentID.HasValue ? new KeyValueViewModel() { Key = sDto.SalaryComponentID.ToString(), Value = sDto.SalaryComponent.Value } : null;
            vm.EmployeeDeptAccountMapDetail = new List<EmployeeDeptAccountMapDetailViewModel>();
            foreach (var detail in sDto.EmployeeDeptAccountMapDetailDTO)
            {
                vm.EmployeeDeptAccountMapDetail.Add(new EmployeeDeptAccountMapDetailViewModel()
                {
                    EmployeeDepartmentAccountMaplIID = detail.EmployeeDepartmentAccountMaplIID,
                    ExpenseLedgerAccountID = detail.ExpenseLedgerAccountID,
                    ProvisionLedgerAccountID = detail.ProvisionLedgerAccountID,
                    StaffLedgerAccountID = detail.StaffLedgerAccountID,
                   // TaxLedgerAccountID = detail.TaxLedgerAccountID,
                   // TaxLedgerAccount = detail.TaxLedgerAccountID.HasValue ? new KeyValueViewModel() { Key = detail.TaxLedgerAccountID.ToString(), Value = detail.TaxLedgerAccount.Value } : null,
                    ProvisionLedgerAccount = detail.ProvisionLedgerAccountID.HasValue ? new KeyValueViewModel() { Key = detail.ProvisionLedgerAccountID.ToString(), Value = detail.ProvisionLedgerAccount.Value } : null,
                    ExpenseLedgerAccount = detail.ExpenseLedgerAccountID.HasValue ? new KeyValueViewModel() { Key = detail.ExpenseLedgerAccountID.ToString(), Value = detail.ExpenseLedgerAccount.Value } : null,
                    StaffLedgerAccount = detail.StaffLedgerAccountID.HasValue ? new KeyValueViewModel() { Key = detail.StaffLedgerAccountID.ToString(), Value = detail.StaffLedgerAccount.Value } : null,
                    Department = detail.DepartmentID.HasValue ? detail.DepartmentID.Value.ToString() : string.Empty
                });
            }
            return vm;
        }
        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<EmployeeDepartmentAccountMapDTO>(jsonString);
        }
        public override BaseMasterDTO ToDTO()
        {
            Mapper<EmployeeDepartmentAccountMapViewModel, EmployeeDepartmentAccountMapDTO>.CreateMap();
            Mapper<EmployeeDeptAccountMapDetailViewModel, EmployeeDeptAccountMapDetailDTO>.CreateMap();
            var dto = Mapper<EmployeeDepartmentAccountMapViewModel, EmployeeDepartmentAccountMapDTO>.Map(this);

            dto.SalaryComponentID = this.SalaryComponent == null || string.IsNullOrEmpty(this.SalaryComponent.Key) ? (int?)null : int.Parse(this.SalaryComponent.Key);

            dto.EmployeeDeptAccountMapDetailDTO = new List<EmployeeDeptAccountMapDetailDTO>();
            foreach (var dat in this.EmployeeDeptAccountMapDetail)
            {
                if (!string.IsNullOrEmpty(dat.Department) && ((dat.ProvisionLedgerAccount != null && dat.ProvisionLedgerAccount.Key != null)
                    || (dat.ProvisionLedgerAccount != null && dat.ProvisionLedgerAccount.Key != null)
                    || (dat.ExpenseLedgerAccount != null && dat.ExpenseLedgerAccount.Key != null)
                    || (dat.StaffLedgerAccount != null && dat.StaffLedgerAccount.Key != null)
                    //|| (dat.TaxLedgerAccount != null && dat.TaxLedgerAccount.Key != null)
                    ))

                {
                    dto.EmployeeDeptAccountMapDetailDTO.Add(new EmployeeDeptAccountMapDetailDTO()
                    {
                        EmployeeDepartmentAccountMaplIID = dat.EmployeeDepartmentAccountMaplIID,
                        ExpenseLedgerAccountID = dat.ExpenseLedgerAccount == null || string.IsNullOrEmpty(dat.ExpenseLedgerAccount.Key) ? (long?)null : long.Parse(dat.ExpenseLedgerAccount.Key),
                        ProvisionLedgerAccountID = dat.ProvisionLedgerAccount == null || string.IsNullOrEmpty(dat.ProvisionLedgerAccount.Key) ? (long?)null : long.Parse(dat.ProvisionLedgerAccount.Key),
                        StaffLedgerAccountID = dat.StaffLedgerAccount == null || string.IsNullOrEmpty(dat.StaffLedgerAccount.Key) ? (long?)null : long.Parse(dat.StaffLedgerAccount.Key),
                        //TaxLedgerAccountID = dat.TaxLedgerAccount == null || string.IsNullOrEmpty(dat.TaxLedgerAccount.Key) ? (long?)null : long.Parse(dat.TaxLedgerAccount.Key),
                        DepartmentID = string.IsNullOrEmpty(dat.Department) ? (int?)null : int.Parse(dat.Department),
                    });

                }
            }
            return dto;
        }
    }
}

