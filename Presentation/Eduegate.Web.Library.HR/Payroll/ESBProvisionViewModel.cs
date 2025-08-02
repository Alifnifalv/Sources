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
using System.Runtime.Intrinsics.X86;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.HR.Payroll
{
    public class ESBProvisionViewModel : BaseMasterViewModel
    {
        public ESBProvisionViewModel()
        {
            //EmployeeList = new List<KeyValueViewModel>();
            ESBProvisionTab = new ESBProvisionTabViewModel();
        }
        public long EmployeeESBProvisionHeadIID { get; set; }
        public KeyValueViewModel DocumentType { get; set; }
        public long? DocumentTypeID { get; set; }
        public int? ESBSalaryComponentID { get; set; }

        public bool? IsaccountPosted { get; set; } = false;
        public bool? IsOpening { get; set; }
        public bool IsRegenerate { get; set; } = false;

        [ControlType(Framework.Enums.ControlTypes.TextBox, "textleft", Attributes = "ng-disabled=true")]
        [CustomDisplay("Entry No.")]
        public string EntryNumber { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("ESB Provision Upto.")]
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


        [ControlType(Framework.Enums.ControlTypes.Button, Attributes = "ng-Click=GenerateESBProvision(CRUDModel.ViewModel.Department,0)")]
        [CustomDisplay("Generate")]
        public string GenerateButton { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, Attributes = "ng-Click=GenerateESBProvision(CRUDModel.ViewModel.Department,1)")]
        [CustomDisplay("Regenerates")]
        public string ReGenerateButton { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, Attributes = "ng-Click=ViewESBProvisionReport(CRUDModel.ViewModel)")]
        [CustomDisplay("View Report")]
        public string ViewReportButton { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine13 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, Attributes = "ng-click='AccountPosting(CRUDModel.ViewModel)' ng-disabled='(CRUDModel.ViewModel.IDList.length<=0)'")]
        [CustomDisplay("Account Post")]
        public string AccountPostButton { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, Attributes = "ng-Click=ViewJournalESB(CRUDModel.ViewModel)")]
        [CustomDisplay("View Journal ESB")]
        public string ViewJournalESBButton { get; set; }
        [ContainerType(Framework.Enums.ContainerTypes.Tab, "ESBProvisionTab", "ESBProvisionTab")]
        [CustomDisplay("Employee Details")]
        public ESBProvisionTabViewModel ESBProvisionTab { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as EmployeeESBProvisionHeadDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<ESBProvisionViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<EmployeeESBProvisionHeadDTO, ESBProvisionViewModel>.CreateMap();
            Mapper<EmployeeESBProvisionDetailDTO, ESBProvisionDetailsViewModel>.CreateMap();
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var sDto = dto as EmployeeESBProvisionHeadDTO;
            var vm = Mapper<EmployeeESBProvisionHeadDTO, ESBProvisionViewModel>.Map(sDto);

            vm.EntryDateToString = (sDto.EntryDate.HasValue ? sDto.EntryDate.Value : DateTime.Now).ToString(dateFormat, CultureInfo.InvariantCulture);
            vm.EmployeeList = new List<KeyValueViewModel>();
            vm.BranchID = sDto.BranchID;
            vm.Branch = sDto.BranchID.ToString();
            vm.ESBProvisionTab.ESBProvisionDetail = new List<ESBProvisionDetailsViewModel>();
            foreach (var detail in sDto.ESBProvisionDetails)
            {
                vm.ESBProvisionTab.ESBProvisionDetail.Add(new ESBProvisionDetailsViewModel()
                {
                    DOJString = detail.DOJ.HasValue ? detail.DOJ.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                    DOJ = detail.DOJ,
                    Category = detail.Category,
                    BasicSalary = detail.BasicSalary ?? 0,
                    Department = detail.Department,
                    EmployeeID = detail.EmployeeID,
                    EmployeeCode = detail.EmployeeCode,
                    EmployeeName = detail.EmployeeName,
                    ESBAmount = detail.ESBAmount ?? 0,
                    OpeningAmount = detail.OpeningAmount,
                    Balance = detail.Balance
                });
            }
            return vm;
        }
        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<EmployeeESBProvisionHeadDTO>(jsonString);
        }
    }
}
