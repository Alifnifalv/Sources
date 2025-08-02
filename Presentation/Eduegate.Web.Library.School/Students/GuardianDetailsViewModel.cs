using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Students;
using System.Globalization;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Students
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "GuardianDetails", "CRUDModel.ViewModel.GuardianDetails")]
    [DisplayName("Guardian Details")]
    public class GuardianDetailsViewModel : BaseMasterViewModel
    {
        public GuardianDetailsViewModel()
        {
            //PreviousSchoolClass = new KeyValueViewModel();
            IsWhatsAppNoSameAsMobile = false;
        }

        public long? GuardianVisaDetailNoID { get; set; }

        public long? GuardianPassportDetailNoID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, "", Attributes = "ng-change='FillFatherDetailsFromGuardian($event, $element,CRUDModel.ViewModel)'")]
        [CustomDisplay("FatherDetailsasGuardianDetails")]
        public bool? IsGuardianDetailAsFatherDetails { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, Attributes = "ng-change='FillMotherDetailsFromGuardian($event, $element,CRUDModel.ViewModel)'")]
        [CustomDisplay("MotherDetailsasGuardianDetails")]
        public bool? IsGuardianDetailAsMotherDetails { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [CustomDisplay("")]
        public string NewLine3 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("First Name")]
        [StringLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        [RegularExpression(@"^[a-zA-Z_ ]*$", ErrorMessage = "Use alphabets only")]
        public string GuardianFirstName { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Middle Name")]
        [StringLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        [RegularExpression(@"^[a-zA-Z_ ]*$", ErrorMessage = "Use alphabets only")]
        public string GuardianMiddleName { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Last Name")]
        [StringLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        [RegularExpression(@"^[a-zA-Z_ ]*$", ErrorMessage = "Use alphabets only")]
        public string GuardianLastName { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("RelationShip")]
        [LookUp("LookUps.GuardianType")]
        public string GuardianStudentRelationShip { get; set; }
        public byte? GuardianStudentRelationShipID { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(25, ErrorMessage = "Maximum Length should be within 25!")]
        [CustomDisplay("Occupation")]
        public string GuardianOccupation { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        [CustomDisplay("CompanyName")]
        public string GuardianCompanyName { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("MobileNo.")]
        [RegularExpression(@"^[0-9*#+ ]+$", ErrorMessage = "Use  digits only")]
        public string GuardianMobileNumber { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, Attributes = "ng-change='FillGuardianWhatsappNoFromMobileNo(CRUDModel.ViewModel.GuardianDetails)'")]
        [CustomDisplay("SetMobileNo.asWhatsAppNo.")]
        public bool? IsWhatsAppNoSameAsMobile { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("WhatsAppNo")]
        [RegularExpression(@"^[0-9*#+ ]+$", ErrorMessage = "Use  digits only")]
        public string GuardianWhatsappMobileNo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [CustomDisplay("EmailID")]
        [RegularExpression("[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?\\.)+[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?", ErrorMessage = "InValid Email Address")]
        public string GuardianEmailID { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Nationality")]
        [LookUp("LookUps.Nationality")]
        public string GuardianNationality { get; set; }
        public int? GuardianNationalityID { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("NationalIDNo")]
        [RegularExpression(@"^[0-9*#+]+$", ErrorMessage = "Use  digits only")]
        [MaxLength(11, ErrorMessage = "Max 11 characters"), MinLength(11, ErrorMessage = "Min 11 characters")]
        public string GuardianNationalID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("NationalIDIssueDate")]
        public string GuardianNationalIDNoIssueDateString { get; set; }
        public DateTime? GuardianNationalIDNoIssueDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("NationalIDExpiryDate")]
        public string GuardianNationalIDNoExpiryDateString { get; set; }
        public DateTime? GuardianNationalIDNoExpiryDate { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(12, ErrorMessage = "Maximum Length should be within 12!")]
        [CustomDisplay("Passport No.")]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Use alphanumeric only")]
        public string GuardianPassportNumber { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Passport Issue Country")]
        [LookUp("LookUps.Country")]
        public string GuardianCountryofIssue { get; set; }

        public int? CountryofIssueID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Passport Issue Date")]
        public string GuardianPassportNoIssueString { get; set; }
        public DateTime? GuardianPassportNoIssueDate { get; set; }


        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Passport Expiry Date")]
        public string GuardianPassportNoExpiryString { get; set; }
        public DateTime? GuardianPassportNoExpiryDate { get; set; }

    }
}
