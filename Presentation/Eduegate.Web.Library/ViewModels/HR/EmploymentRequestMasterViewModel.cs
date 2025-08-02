using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels.HR
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "EmploymentRequestMasterViewModel", "CRUDModel.Model.MasterViewModel")]
    [DisplayName("Employment Request")]
    public class EmploymentRequestMasterViewModel : BaseMasterViewModel
    {

        public EmploymentRequestMasterViewModel()
        {
            ProposedIncrease = new EmploymentProposedIncreaseViewModel();
            Allowance = new EmploymentAllowanceViewModel();
            Document = new CRUDDocumentViewViewModel();
        }

    

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.TextBox, "")]
        //[MaxLength(40, ErrorMessage = "Max 40 characters")]
        //[DisplayName("NAME")]
        //public string NAME { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "")]
        [DisplayName("EMP REQ NUMBER")]
        public int? EMP_REQ_NO { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "")]
        [DisplayName("EMP NO")]
        public int? EMP_NO { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.FileUpload)]
        //[DisplayName("PHOTO")]
        //[FileUploadInfo("Mutual/UploadDocument", Framework.Enums.WBImageTypes.EmployeePicture, "Photo")]
        public string Photo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "")]
        [MaxLength(20, ErrorMessage = "Max 20 characters")]
        [DisplayName("FIRST NAME")]
        public string F_NAME { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "")]
        [MaxLength(20, ErrorMessage = "Max 20 characters")]
        [DisplayName("MIDDLE NAME")]
        public string M_NAME { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "")]
        [MaxLength(20, ErrorMessage = "Max 20 characters")]
        [DisplayName("LAST NAME")]
        public string L_NAME { get; set; }

        

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [DisplayName("GENDER")]
        [LookUp("LookUps.Gender")]
        public string GENDER { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [DisplayName("MARITAL STATUS")]
        [Select2("MaritalStatus", "Numeric", false)]
        [LookUp("LookUps.MaritalStatus")]
        public KeyValueViewModel MaritalStatus { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "" )]
        [DisplayName("Employment Process Status")]
        [Select2("EmploymentProcessStatus", "Numeric",false,"",optionalAttribute1:"ng-click=GetEmploymentProcessStatus()")]
        [LookUp("LookUps.EmploymentProcessStatus")]
        public KeyValueViewModel EmpProcessRequestStatus { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "")]
        [DisplayName(" ")]
        public string BlankLabel12 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "")]
        [DisplayName(" ")]
        public string BlankLabel7 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "")]
        [DisplayName(" ")]
        public string BlankLabel8 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [DisplayName("RECRUITMENT TYPE")]
        //ng-change='ValidateField($event,$element,\"RecruitmentType\",\"HR/EmploymentRequest\")'
        [Select2("Recruitment Type", "Numeric", false, "GetDocType($select,$index, CRUDModel.Model.MasterViewModel)", false)]
        [LookUp("LookUps.RecruitmentType")]
        public KeyValueViewModel RecruitmentType { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "", "ng-disabled='CRUDModel.Model.MasterViewModel.RecruitmentType.Key != 2' ng-blur='ValidateField($event,$element,\"CIVILID\",\"HR/EmploymentRequest\")'")]
        [MaxLength(12, ErrorMessage = "Max 12 characters")]
        [DisplayName("CIVIL ID")]
        public string CIVILID { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [DisplayName("AGENT")]
        [Select2("Agent", "Numeric", false, "ng-hide='CRUDModel.Model.MasterViewModel.RecruitmentType.Key == 2' ng-blur='ValidateField($event,$element,\"Agent\",\"HR/EmploymentRequest\")'", false)]
        [LookUp("LookUps.Agent")]
        public KeyValueViewModel Agent { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "")]
        [DisplayName(" ")]
        public string BlankLabel14 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [DisplayName("NATIONALITY")]
        [Select2("Nationality", "Numeric", false, "ValidateField($event,$element,\"DATE_OF_BIRTH\",\"HR/EmploymentRequest\")")]
        [LookUp("LookUps.Nationality")]
        public KeyValueViewModel Nationality { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker, "", "ng-blur='ValidateField($event,$element,\"DATE_OF_BIRTH\",\"HR/EmploymentRequest\")'")]
        [DisplayName("DOB")]
        public string DATE_OF_BIRTH { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "")]
        [DisplayName(" ")]
        public string BlankLabel5 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "")]
        [DisplayName(" ")]
        public string BlankLabel6 { get; set; }


        //[ControlType(Framework.Enums.ControlTypes.Label, "")]
        //[DisplayName(" ")]
        //public string BlankLabel1 { get; set; }


        //[ControlType(Framework.Enums.ControlTypes.Label, "")]
        //[DisplayName(" ")]
        //public string BlankLabel13 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "", "ng-blur='ValidateField($event,$element,\"PASSPORT_NO\",\"HR/EmploymentRequest\")'")]
        [MaxLength(12, ErrorMessage = "Passport number should be of 12 characters")]
        //[MinLength(12, ErrorMessage = "Passport number should be of 12 characters")]
        [DisplayName("PASSPORT NUMBER")]
        public string PASSPORT_NO { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker, "")]
        [DisplayName("PASSPORT ISSUE DATE")]
        public string PASSPORT_ISSUE { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker, "")]
        [DisplayName("PASSPORT EXPIRY DATE")]
        public string PASSPORT_EXPIRY { get; set; }

       

        [ControlType(Framework.Enums.ControlTypes.Label, "")]
        [DisplayName(" ")]
        public string BlankLabel2 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "")]
        [DisplayName(" ")]
        public string BlankLabel3 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "")]
        [DisplayName(" ")]
        public string BlankLabel1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [DisplayName("EMPLOYMENT TYPE")]
        [Select2("Employment Type", "Numeric", false, "", false)]
        [LookUp("LookUps.EmploymentType")]
        public KeyValueViewModel EmploymentType { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Select2, "", "ng-disabled='CRUDModel.Model.MasterViewModel.EmploymentType.Key != 3'")]
        //[DisplayName("REPLACEMENT FOR")]
        //[Select2("EmployeeList", "Numeric", false, "", false)]
        //[LookUp("LookUps.EmployeeList")]
        //public KeyValueViewModel replacedEmployee { get; set; } // Sale select box for Re appointment also


        //[ControlType(Framework.Enums.ControlTypes.CheckBox, "", "ng-disabled='CRUDModel.Model.MasterViewModel.EmploymentType.Key != 1'")]
        //[DisplayName("is Budgeted")]
        //public bool? isBudgeted { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Select2, "", "ng-hide='CRUDModel.Model.MasterViewModel.EmploymentType.Key != 3 && CRUDModel.Model.MasterViewModel.EmploymentType.Key != 4'")]
        [ControlType(Framework.Enums.ControlTypes.Select2, "", "ng-hide='CRUDModel.Model.MasterViewModel.EmploymentType.Key != 3 && CRUDModel.Model.MasterViewModel.EmploymentType.Key != 4' ng-blur='ValidateField($event,$element,\"ReplacedEmployee\",\"HR/EmploymentRequest\")'")]
        [DisplayName("REPLACEMENT/REAPPOINTMENT OF")]
        [Select2("EmployeeList", "Numeric", false, "", false)]
        [LookUp("LookUps.EmployeeList")]
        public KeyValueViewModel ReplacedEmployee { get; set; } // Sale select box for Re appointment also


        [ControlType(Framework.Enums.ControlTypes.CheckBox, "", "ng-disabled='CRUDModel.Model.MasterViewModel.EmploymentType.Key != 1'")]
        [DisplayName("is Budgeted")]
        public bool? isBudgeted { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "")]
        [DisplayName(" ")]
        public string BlankLabel15 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [DisplayName("CONTRACT TYPE")]
        [Select2("Shop", "Numeric", false, "", false)]
        [LookUp("LookUps.ContractType")]
        public KeyValueViewModel ContractType { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "", "ng-disabled='CRUDModel.Model.MasterViewModel.ContractType.Key != 2'")]
        //[MaxLength(12, ErrorMessage = "Max 12 characters")]
        [DisplayName("PERIOD (YEARS)")]
        public int? Period { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [DisplayName("DEPARTMENT")]
        [Select2("Shop", "Numeric", false, "GetLocationByDept($select,$index, CRUDModel.Model.MasterViewModel)", false)]
        [LookUp("LookUps.Department")]
        public KeyValueViewModel Shop { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [DisplayName("LOCATION")]
        [Select2("Location", "Numeric", false, "", false)]
        [LookUp("LookUps.Location")]
        public KeyValueViewModel Location { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [DisplayName("PAYCOMP")]
        [Select2("PayComp", "Numeric", false, "GetAllowancebyPayComp($select,$index, CRUDModel.Model.MasterViewModel)", false)]
        [LookUp("LookUps.PayComp")]
        public KeyValueViewModel PayComp { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "")]
        [DisplayName(" ")]
        public string BlankLabel11 { get; set; }
        [ControlType(Framework.Enums.ControlTypes.Label, "")]
        [DisplayName(" ")]
        public string BlankLabel9 { get; set; }
        [ControlType(Framework.Enums.ControlTypes.Label, "")]
        [DisplayName(" ")]
        public string BlankLabel10 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "")]
        [DisplayName(" ")]
        public string BlankLabel13 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "")]
        [DisplayName(" ")]
        public string BlankLabel16 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [DisplayName("GROUP")]
        [Select2("Group", "Numeric", false, "GetMainDesignation($select,$index, CRUDModel.Model.MasterViewModel)", false)]
        [LookUp("LookUps.GroupDesignation")]
        public KeyValueViewModel Group { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [DisplayName("MAIN DESIGNATION")]
        [Select2("MainDesignation", "Numeric", false, "GetHRDesignation($select,$index, CRUDModel.Model.MasterViewModel)", false)]
        [LookUp("LookUps.MainDesignation")]
        public KeyValueViewModel MainDesignation { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [DisplayName("DESIGNATION")]
        [Select2("Designation", "Numeric", false, "GetNextEmpNo($select,$index, CRUDModel.Model.MasterViewModel)", false)]
        [LookUp("LookUps.Designation")]
        public KeyValueViewModel Designation { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [DisplayName("PRODUCTIVE TYPE")]
        [Select2("Productive Type", "Numeric", false, "", false)]
        [LookUp("LookUps.ProductiveType")]
        public KeyValueViewModel ProductiveType { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "", "ng-blur='ValidateField($event,$element,\"BasicSalary\",\"HR/EmploymentRequest\")'")]
        [DisplayName("STARTING SALARY")]
        public decimal BasicSalary { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [DisplayName("STAFF CAR")]
        public bool StaffCar { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "onecol-header-left")]
        [DisplayName(" ")]
        public string Reason_Basic_Blank { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextArea, "onecol-header-left")]
        [DisplayName("REASON TO INCREASE BASIC SALARY")]
        public string Reason_Basic { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-change='SetProposedIncreaseAmount($event,$element,$index)'")]
        //[DisplayName("PROPOSED INCREASE AMOUNT")]
        //public decimal? ProposedIncreaseAmount { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-change='SetProposedIncreasePercent($event,$element,$index)'")]
        //[DisplayName("PROPOSED INCREASE %")]
        //public decimal? ProposedIncreasePercent { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Label)]
        //[DisplayName("PROPOSED INCREASE")]
        //public decimal? ProposedIncrease { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[DisplayName("SALARY CHANGE AFTER PERIOD")]
        //[Select2("PeriodSalary", "Numeric", false, "", false)]
        //[LookUp("LookUps.PeriodSalary")]
        //public KeyValueViewModel SalaryChangeAfterPeriod { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextArea, "onecol-header-left")]
        [DisplayName("RECRUITING REMARK (NOC, LOCAL..)")]
        public string RecuritRemark { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [DisplayName("PLEASE ENTER YOUR OFFICIAL E-MAIL ID TO RECEIVE NOTIFICATION FROM HR")]
        public string EmailID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [DisplayName("ALTERNATIVE E-MAIL ID")]
        public string AlternativeEmailID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextArea, "onecol-header-left")]
        [MaxLength(500, ErrorMessage = "Maximum Length should be within 500!")]
        [DisplayName("Remarks")]
        public string EmpPersonalRemarks { get; set; }

        public string CRE_WEBUSER { get; set; }

        public string CRE_IP { get; set; }

        public string CRE_DT { get; set; }

        public string CRE_BY { get; set; }

        public string REQ_DT { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Label, "onecol-header-left")]
        //[DisplayName(" ")]
        public bool? isNewRequest { get; set; }


        public KeyValueViewModel VisaCompany { get; set; }

        public KeyValueViewModel QuotaType { get; set; }

        //public KeyValueViewModel EmpProcessRequestStatus { get; set; }

        public KeyValueViewModel EmpRequestStatus { get; set; }

        public string PersonalRemarks { get; set; }


        [ContainerType(Framework.Enums.ContainerTypes.Tab, "ProposedIncreaseDetails", "CRUDModel.Model.MasterViewModel.ProposedIncrease", attributes2: "header-list")]
        [DisplayName("Proposed Increase")]
        public EmploymentProposedIncreaseViewModel ProposedIncrease { get; set; }


        [ContainerType(Framework.Enums.ContainerTypes.Tab, "AllowanceDetails", "CRUDModel.Model.MasterViewModel.Allowance", attributes2: "header-list")]
        [DisplayName("Allowance")]
        public EmploymentAllowanceViewModel Allowance { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "Documents", "Documents", attributes2: "header-list")]
        [DisplayName("Documents")]
        [LazyLoad("Mutual/DocumentFile", "Mutual/GetDocumentFiles", "CRUDModel.Model.MasterViewModel.Document")]
        public CRUDDocumentViewViewModel Document { get; set; }
    }
}
