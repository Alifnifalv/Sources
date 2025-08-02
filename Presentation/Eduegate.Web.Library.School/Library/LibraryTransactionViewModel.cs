using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Library;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System;
using System.Globalization;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Library
{
    public class LibraryTransactionViewModel : BaseMasterViewModel
    {
        public LibraryTransactionViewModel()
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            //Book = new KeyValueViewModel();
            Student = new KeyValueViewModel();
            Employee = new KeyValueViewModel();            
            TransactionDateString = DateTime.Now.ToString(dateFormat, CultureInfo.InvariantCulture);
            BookCondition = "1"; //Book condition Good set as static
        }
        
        public long  LibraryTransactionIID { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.DataPicker)]
        //[DisplayName("Select Book")]
        //[DataPicker("StudenAdvancedSearch")]
        //public string ReferenceBookNo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("TransactionDate")]
        public string TransactionDateString { get; set; }
        public System.DateTime? TransactionDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine5 { get; set; }


        //[ControlType(Framework.Enums.ControlTypes.Button, Attributes = "ng-click=ScanAccNo($event)")]
        //[CustomDisplay("Scan Acc No")]
        //public string Acc_No_Scan { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine9 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-keyup='$event.keyCode == 13 && GetBookDetails(CRUDModel.ViewModel)'")]
        [CustomDisplay("Call/Acc No Search")]
        public string CallAccNo { get; set; }
        public string Acc_No { get; set; }

        public string Call_No { get; set; }



        [ControlType(Framework.Enums.ControlTypes.HtmlLabel, Attributes2 = "htmllabelinfo")]
        [CustomDisplay("BookIssueDetails")]
        public string BookIssueDetails { get; set; }

        [ControlType(Framework.Enums.ControlTypes.HtmlLabel, Attributes2 = "labelinfo-custom")]
        [CustomDisplay("AvailableBookDetails")]
        public string AvailableBookQty { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "onecol-header-left")]
        [Select2("Book", "String", false, "BookChanges($event, $element, CRUDModel.ViewModel)")]
        //[Select2("Book", "String", false, "")]
        [CustomDisplay("Book")]
        [LookUp("LookUps.LibraryBooks")]
        //[LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=LibraryBooks", "LookUps.LibraryBooks")]
        public KeyValueViewModel Book { get; set; }
        public int? LibraryBookMapID { get; set; }
        public long? BookID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Bookcondition")]
        [LookUp("LookUps.BookTypeCondition")]
        public string BookCondition { get; set; }
        public byte? BookConditionID { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        //[DisplayName("")]
        //public string NewLine1 { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.CheckBox, optionalAttribs: "ng-disabled=CRUDModel.ViewModel.LibraryTransactionType.Key==1")]
        //[DisplayName("Is Book Damaged")]     
        //public bool? IsApplyDamageCost { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox, optionalAttribs: "ng-disabled=CRUDModel.ViewModel.LibraryTransactionType.Key==1")]
        //[DisplayName("Damage Cost(%)")]
        //public decimal? PercentageDamageCost { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        //[DisplayName("")]
        //public string NewLine01 { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.CheckBox, optionalAttribs: "ng-disabled=CRUDModel.ViewModel.LibraryTransactionType.Key==1")]
        //[DisplayName("Is Apply Late Fee ")]
        //public bool? IsApplyLateFee { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox, optionalAttribs: "ng-disabled=CRUDModel.ViewModel.LibraryTransactionType.Key==1")]
        //[DisplayName("LateFeeAmount")]
        //public decimal? LateFeeAmount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine02 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("TransactionType")]
        [LookUp("LookUps.LibraryTransactionType")]
        public string LibraryTransactionType { get; set; }
        public byte? LibraryTransactionTypeID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker, attribs: "ng-disabled='CRUDModel.ViewModel.LibraryTransactionType == 2'")]
        [CustomDisplay("ReturnDueDate")]
        public string ReturnDueDateString { get; set; }
        public DateTime? ReturnDueDate { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.DataPicker, attribs: "ng-disabled=CRUDModel.ViewModel.Employee.Key")]
        //[CustomDisplay("SelectStudent")]
        //[DataPicker("LibraryStudentsAdvanceSearchView")]
        public string ReferenceStudent { get; set; }
        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine10 { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "onecol-header-left")]
        [Select2("Student", "String", false,  optionalAttribute1: "ng-disabled=CRUDModel.ViewModel.Employee.Key")]
        [CustomDisplay("Student")]
        //[LookUp("LookUps.Student")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Student", "LookUps.Student")]
        public KeyValueViewModel Student { get; set; }
        public long?  StudentID { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.DataPicker, attribs: "ng-disabled=CRUDModel.ViewModel.Student.Key")]
        //[CustomDisplay("SelectEmployee")]
        //[DataPicker("LibraryStaffssAdvanceSearch")]
        public string ReferenceEmployee { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "onecol-header-left")]
        [Select2("Employee", "String", false, optionalAttribute1: "ng-disabled=CRUDModel.ViewModel.Student.Key")]
        [CustomDisplay("Staff")]
        //[LookUp("LookUps.Employee")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Employee", "LookUps.Employee")]
        public KeyValueViewModel Employee { get; set; }
        public long?  EmployeeID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine20 { get; set; }

        //[Required]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [CustomDisplay("Notes")]
        public string Notes { get; set; }

        public int? Book_Quantity { get; set; }
        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as LibraryTransactionDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<LibraryTransactionViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<LibraryTransactionDTO, LibraryTransactionViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var sDto = dto as LibraryTransactionDTO;
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var vm = Mapper<LibraryTransactionDTO, LibraryTransactionViewModel>.Map(dto as LibraryTransactionDTO);
            vm.TransactionDateString = sDto.TransactionDate.HasValue ? sDto.TransactionDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.ReturnDueDateString = sDto.ReturnDueDate.HasValue ? sDto.ReturnDueDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.LibraryTransactionType = sDto.LibraryTransactionTypeID.HasValue ? sDto.LibraryTransactionTypeID.Value.ToString() : null;
            vm.BookCondition = sDto.BookConditionID.HasValue ? sDto.BookConditionID.Value.ToString() : null;
            vm.BookIssueDetails = sDto.BookIssueDetails != null ? sDto.BookIssueDetails : null;
            //vm.Acc_No = sDto.Acc_No;
            vm.Call_No = sDto.Call_No;
            vm.CallAccNo = sDto.Call_No;
            //vm.IsApplyDamageCost = sDto.IsApplyDamageCost.HasValue ? sDto.IsApplyDamageCost.Value : (bool?)null;
            //vm.PercentageDamageCost = sDto.PercentageDamageCost.HasValue ? sDto.PercentageDamageCost.Value : (decimal?)null;
            //vm.IsApplyLateFee = sDto.IsApplyLateFee.HasValue ? sDto.IsApplyLateFee.Value : (bool?)null;
            //vm.LateFeeAmount = sDto.LateFeeAmount.HasValue ? sDto.LateFeeAmount.Value : (decimal?)null;

            vm.Book = new KeyValueViewModel()
            {
                Key = sDto.LibraryBookMapID.ToString(),
                Value = sDto.Book.Value
            };
            vm.Employee = new KeyValueViewModel()
            {
                Key = sDto.EmployeeID.ToString(),
                Value = sDto.StaffName
            };
            vm.Student = new KeyValueViewModel()
            {
                Key = sDto.StudentID.ToString(),
                Value = sDto.Student.Value
            };
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<LibraryTransactionViewModel, LibraryTransactionDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = Mapper<LibraryTransactionViewModel, LibraryTransactionDTO>.Map(this);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
             
            dto.LibraryTransactionTypeID = string.IsNullOrEmpty(this.LibraryTransactionType) ? (byte?)null : byte.Parse(this.LibraryTransactionType);
            dto.TransactionDate = string.IsNullOrEmpty(this.TransactionDateString) ? (DateTime?)null : DateTime.ParseExact(TransactionDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.ReturnDueDate = string.IsNullOrEmpty(this.ReturnDueDateString) ? (DateTime?)null : DateTime.ParseExact(ReturnDueDateString, dateFormat, CultureInfo.InvariantCulture);
            //dto.BookID = string.IsNullOrEmpty(this.Book.Key) ? (long?)null : long.Parse(this.Book.Key);
            dto.LibraryBookMapID = string.IsNullOrEmpty(this.Book.Key) ? (int?)null : int.Parse(this.Book.Key);
            dto.EmployeeID = this.Employee == null || string.IsNullOrEmpty(this.Employee.Key) ? (long?)null : long.Parse(this.Employee.Key);
            dto.StudentID = this.Student == null || string.IsNullOrEmpty(this.Student.Key) ? (long?)null : long.Parse(this.Student.Key);
            dto.StudentName = this.Student != null ? this.Student.Value : null;
            dto.EmployeeName = this.Employee != null ? this.Employee.Value : null;
            dto.BookConditionID = string.IsNullOrEmpty(this.BookCondition) ? (byte?)null : byte.Parse(this.BookCondition);
            //dto.Acc_No = Acc_No;
            dto.Call_No = this.CallAccNo != null ? this.CallAccNo : null;
            //dto.Acc_No = Book.Key 
            //dto.IsApplyDamageCost = this.IsApplyDamageCost.HasValue ? this.IsApplyDamageCost.Value : (bool?)null;
            //dto.PercentageDamageCost = this.PercentageDamageCost.HasValue ? this.PercentageDamageCost.Value : (decimal?)null;
            //dto.IsApplyLateFee = this.IsApplyLateFee.HasValue ? this.IsApplyLateFee.Value : (bool?)null;
            //dto.LateFeeAmount = this.LateFeeAmount.HasValue ? this.LateFeeAmount.Value : (decimal?)null;
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<LibraryTransactionDTO>(jsonString);
        }
    }
}