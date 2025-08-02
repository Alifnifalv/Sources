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


namespace Eduegate.Web.Library.ViewModels
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "AuthendicationList", "CRUDModel.ViewModel.AuthendicationList")]
    [DisplayName("Authendication List / Tender Committee")]
    public class TenderAuthenticationViewModel : BaseMasterViewModel
    {
        public TenderAuthenticationViewModel()
        {

        }

        public long AuthenticationID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [DisplayName("Sr.No")]
        public long SerialNo { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.DataPicker)]
        //[CustomDisplay("Pick user")]
        //[DataPicker("StudentApplicationAdvancedSearch")]
        //public string PickUser { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.DataPicker)]
        //[CustomDisplay("Pick staff")]
        //[DataPicker("StudentApplicationAdvancedSearch")]
        //public string PickStaff { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("User name")]
        public string UserName { get; set; } 
        
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("User ID")]
        public string UserID { get; set; }   
        
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