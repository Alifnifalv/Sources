using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Students;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Web.Library.Common;
using System.Runtime.Serialization;


namespace Eduegate.Web.Library.ViewModels
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "AuthendicationList", "CRUDModel.ViewModel.AuthendicationList")]
    [DisplayName("Authendication List / Tender Committee")]
    public class TenderAuthenticationViewModel : BaseMasterViewModel
    {
        public TenderAuthenticationViewModel()
        {
            IsActive = true;
            Employee = new KeyValueViewModel();
        }

        public long AuthenticationID { get; set; }

        public long TenderAuthMapIID { get; set; }

        public long? TenderID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [DisplayName("Sr.No")]
        public long SerialNo { get; set; }

        //[DataPicker("EmployeeAdvancedSearch")]
        //[ControlType(Framework.Enums.ControlTypes.DataPicker, "onecol-header-left")]
        //[CustomDisplay("Select Staff")]
        //public string PickStaff { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("BidUsers", "Numeric", false, "ExistingUserSelects($event,$index,CRUDModel.ViewModel.AuthendicationList)")]
        [CustomDisplay("Existing user")]
        [LookUp("LookUps.BidUsers")]
        public KeyValueViewModel BidUsers { get; set; }
        public long? BidUsersID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Employee", "Numeric", false, "EmployeeDropdownChanges($event,$index,CRUDModel.ViewModel.AuthendicationList)")]
        [CustomDisplay("Employee")]
        [LookUp("LookUps.ActiveEmployees")]
        public KeyValueViewModel Employee { get; set; }
        public long? EmployeeID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("User name")]
        public string UserName { get; set; } 
        
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("User ID")]
        public string UserID { get; set; }

        [EmailAddress]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Email ID")]
        public string EmailID { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Password)]
        //[CustomDisplay("Password")]
        public string Password { get; set; }

        public string OldPassword { get; set; }
        public string OldPasswordSalt { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("Is Active")]
        public bool? IsActive { get; set; }   
        
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("Is Approver")]
        public bool? IsApprover { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index,ModelStructure.AuthendicationList[0],CRUDModel.ViewModel.AuthendicationList)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index,ModelStructure.AuthendicationList[0],CRUDModel.ViewModel.AuthendicationList)'")]
        [DisplayName("-")]
        public string Remove { get; set; }

    }
}