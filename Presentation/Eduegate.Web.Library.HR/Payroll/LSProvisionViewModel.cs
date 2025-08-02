using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Frameworks.Mvc.Attributes;
using Eduegate.Services.Contracts.HR.Payroll;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.HR.Payroll
{
    public class LSProvisionViewModel : BaseMasterViewModel
    {
        public LSProvisionViewModel()
        {
            EmployeeList = new List<KeyValueViewModel>();
            LSProvisionTab = new LSProvisionTabViewModel();
        }

        public long EmployeeLSProvisionHeadIID { get; set; }
        public int? LSSalaryComponentID { get; set; }
        public KeyValueViewModel DocumentType { get; set; }
        public long? DocumentTypeID { get; set; }
        public bool IsRegenerate { get; set; } = false;
        public bool? IsOpening { get; set; }
        public bool? IsaccountPosted { get; set; } = false;

        [ControlType(Framework.Enums.ControlTypes.TextBox, "textleft", Attributes= "ng-disabled=true")]
        [CustomDisplay("Entry No.")]
        public string EntryNumber { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("LS provision upto.")]
        public string EntryDateToString { get; set; }
        public System.DateTime? EntryDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, Attributes = "ng-change=GetEmployeeByBranch(CRUDModel.ViewModel)")]
        [LookUp("LookUps.Branch")]
        [CustomDisplay("Branch")]
        [HasClaim(HasClaims.HASMULTIBRANCH)]
        public string Branch { get; set; }

        public long? BranchID { get; set; }
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Department", "Numeric", false, "GetEmployeesByDepartmentBranch(CRUDModel.ViewModel)")]
        [LookUp("LookUps.Department")]
        [CustomDisplay("Department")]
        public KeyValueViewModel Department { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Employee", "Numeric", true)]
        [LookUp("LookUps.ActiveEmployees")]
        [CustomDisplay("Employee")]
        public List<KeyValueViewModel> EmployeeList { get; set; }

     
        [ControlType(Framework.Enums.ControlTypes.Button, Attributes = "ng-Click=GenerateLeaveSalaryProvision(CRUDModel.ViewModel.Department,0)")]
        [CustomDisplay("Generate")]
        public string GenerateButton { get; set; }
        [ControlType(Framework.Enums.ControlTypes.Button, Attributes = "ng-Click=GenerateLeaveSalaryProvision(CRUDModel.ViewModel.Department,1)")]
        [CustomDisplay("Regenerates")]
        public string ReGenerateButton { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, Attributes = "ng-Click=ViewLSProvisionReport(CRUDModel.ViewModel)")]
        [CustomDisplay("View Report")]
        public string ViewReportButton { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine13 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, Attributes = "ng-click='AccountPosting(CRUDModel.ViewModel)' ng-disabled='(CRUDModel.ViewModel.EmployeeLSProvisionHeadIID.length<=0)'")]
        [CustomDisplay("Account Post")]
        public string AccountPostButton { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, Attributes = "ng-Click='ViewJournalLS(CRUDModel.ViewModel)'ng-disabled='(CRUDModel.ViewModel.EmployeeLSProvisionHeadIID.length<=0)'")]
        [CustomDisplay("View Journal LS")]
        public string ViewJournalLSButton { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "LSProvisionTab", "LSProvisionTab")]
        [CustomDisplay("Employee Details")]
        public LSProvisionTabViewModel LSProvisionTab { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as EmployeeLSProvisionHeadDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<LSProvisionViewModel>(jsonString);
        }
        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<EmployeeLSProvisionHeadDTO>(jsonString);
        }
        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<EmployeeLSProvisionHeadDTO, LSProvisionViewModel>.CreateMap();
            Mapper<EmployeeLSProvisionDetailDTO, LSProvisionDetailsViewModel>.CreateMap();
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var sDto = dto as EmployeeLSProvisionHeadDTO;
            var vm = Mapper<EmployeeLSProvisionHeadDTO, LSProvisionViewModel>.Map(sDto);
            vm.EntryNumber = sDto.EntryNumber;
            vm.EntryDate = sDto.EntryDate;
            vm.EntryDateToString = (sDto.EntryDate.HasValue ? sDto.EntryDate.Value : DateTime.Now).ToString(dateFormat, CultureInfo.InvariantCulture);
            vm.EmployeeList = new List<KeyValueViewModel>();
            vm.BranchID = sDto.BranchID;
            vm.Branch = sDto.BranchID.ToString();
            vm.LSProvisionTab.LSProvisionDetail = new List<LSProvisionDetailsViewModel>();
            foreach (var detail in sDto.LSProvisionDetails)
            {
                vm.LSProvisionTab.LSProvisionDetail.Add(new LSProvisionDetailsViewModel()
                {
                    LastLeaveSalaryDateString = detail.LastLeaveSalaryDate.HasValue ? detail.LastLeaveSalaryDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                    DOJString = detail.DOJ.HasValue ? detail.DOJ.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                    DOJ = detail.DOJ, 
                    Category = detail.Category,
                    BasicSalary = detail.BasicSalary ?? 0,
                    Department = detail.Department,
                    EmployeeID = detail.EmployeeID,
                    EmployeeCode = detail.EmployeeCode,
                    EmployeeName = detail.EmployeeName,                    
                    NoofLeaveSalaryDays = detail.NoofLeaveSalaryDays,
                    LastLeaveSalaryDate = detail.LastLeaveSalaryDate,
                    LeaveSalaryAmount = detail.LeaveSalaryAmount,
                    OpeningAmount=detail.OpeningAmount,
                    Balance=detail.Balance 
                    
                });
            }
            return vm;
        }

    }
}

