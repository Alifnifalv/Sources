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
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "FatherMotherDetails", "CRUDModel.ViewModel.FatherMotherDetails")]
    [DisplayName("Father Mother Details")]
    public class FatherMotherDetailsViewModel : BaseMasterViewModel
    {
        public FatherMotherDetailsViewModel()
        {
            //PreviousSchoolClass = new KeyValueViewModel();
            IsMotherWhatsAppNoSameAsMobile = false;
            IsFatherWhatsAppNoSameAsMobile = false;
        }

        [ControlType(Framework.Enums.ControlTypes.Label, "fullwidth alignleft")]
        [CustomDisplay("FatherDetails:")]
        public bool? Father { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine5 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("First Name")]
        [StringLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        [RegularExpression(@"^[a-zA-Z_ ]*$", ErrorMessage = "Use alphabets only")]
        public string FatherFirstName { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Middle Name")]
        [StringLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        [RegularExpression(@"^[a-zA-Z_ ]*$", ErrorMessage = "Use alphabets only")]
        public string FatherMiddleName { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Last Name")]
        [StringLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        [RegularExpression(@"^[a-zA-Z_ ]*$", ErrorMessage = "Use alphabets only")]
        public string FatherLastName { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.DropDown)]
        //[DisplayName("Guardian Student RelationShip")]
        //[LookUp("LookUps.GuardianType")]
        public string FatherStudentRelationShip { get; set; }
        public byte? FatherStudentRelationShipID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(25, ErrorMessage = "Maximum Length should be within 25!")]
        [CustomDisplay("Occupation")]
        //[RegularExpression(@"^[a-zA-Z_ ]*$", ErrorMessage = "Use alphabets only")]
        public string FatherOccupation { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        [CustomDisplay("CompanyName")]
        //[RegularExpression(@"^[a-zA-Z_ ]*$", ErrorMessage = "Use alphabets only")]
        public string FatherCompanyName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("MobileNo.")]
        //[MaxLength(13, ErrorMessage = "Max 13 characters"), MinLength(13, ErrorMessage = "Min 13 characters")]
        [RegularExpression(@"^[0-9*#+ ]+$", ErrorMessage = "Use  digits only")]
        public string MobileNumber { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, Attributes = "ng-change='FillFatherWhatsappNoFromMobileNo(CRUDModel.ViewModel.FatherMotherDetails)'")]
        [CustomDisplay("SetMobileNo.asWhatsAppNo.")]
        public bool? IsFatherWhatsAppNoSameAsMobile { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("WhatsApp No.")]
        //[MaxLength(13, ErrorMessage = "Max 13 characters"), MinLength(13, ErrorMessage = "Min 13 characters")]
        [RegularExpression(@"^[0-9*#+ ]+$", ErrorMessage = "Use  digits only")]
        public string FatherWhatsappMobileNo { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("OtherMobileNo.")]
        //[MaxLength(13, ErrorMessage = "Max 13 characters"), MinLength(13, ErrorMessage = "Min 13 characters")]
        [RegularExpression(@"^[0-9*#+ ]+$", ErrorMessage = "Use  digits only")]
        public string FatherMobileNumberTwo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [CustomDisplay("EmailID")]
        [RegularExpression("[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?\\.)+[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?", ErrorMessage = "InValid Email Address")]
        public string EmailID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Nationality")]
        [LookUp("LookUps.Nationality")]
        public string FatherCountry { get; set; }
        public int? FatherCountryID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("NationalIDNo")]
        [RegularExpression(@"^[0-9*#+]+$", ErrorMessage = "Use  digits only")]
        [MaxLength(11, ErrorMessage = "Max 11 characters"), MinLength(11, ErrorMessage = "Min 11 characters")]
        public string FatherNationalID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("NationalIDIssueDate")]
        public string FatherNationalDNoIssueDateString { get; set; }
        public DateTime? FatherNationalDNoIssueDate { get; set; }


        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("NationalIDExpiryDate")]
        public string FatherNationalDNoExpiryDateString { get; set; }
        public DateTime? FatherNationalDNoExpiryDate { get; set; }
        public long? FatherPassportDetailNoID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(12, ErrorMessage = "Maximum Length should be within 12!")]
        [CustomDisplay("Passport No.")]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Use alphanumeric only")]
        public string FatherPassportNumber { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Passport Issue Country")]
        [LookUp("LookUps.Country")]
        public string FatherCountryofIssue { get; set; }

        public int? FatherCountryofIssueID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Passport Issue Date")]
        public string FatherPassportNoIssueString { get; set; }


        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Passport Expiry Date")]
        public string FatherPassportNoExpiryString { get; set; }

        public long? FatherVisaDetailNoID { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[MaxLength(11, ErrorMessage = "Maximum Length should be within 11!")]
        //[DisplayName("Visa No.")]
        //[RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Use alphanumeric only")]
        //public string FatherVisaNo { get; set; }


        //[ControlType(Framework.Enums.ControlTypes.DatePicker)]
        //[DisplayName("Visa Issue Date")]
        //public string FatherVisaIssueDateString { get; set; }



        //[ControlType(Framework.Enums.ControlTypes.DatePicker)]
        //[DisplayName("Visa Expiry Date")]
        //public string FatherVisaExpiryDateString { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("InWhichSphereofSchoollifeCanYouVolunteerToHelp?")]
        [LookUp("LookUps.VolunteerType")]

        public string CanYouVolunteerToHelpOneString { get; set; }
        public int? CanYouVolunteerToHelpOneID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "fullwidth alignleft")]
        [CustomDisplay("MotherDetails:")]
        public bool? Mother { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine6 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("MotherFirstName")]
        [StringLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        [RegularExpression(@"^[a-zA-Z_ ]*$", ErrorMessage = "Use alphabets only")]
        public string MotherFirstName { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("MotherMiddleName")]
        [StringLength(20, ErrorMessage = "Maximum Length should be 20!")]
        [RegularExpression(@"^[a-zA-Z_ ]*$", ErrorMessage = "Use alphabets only")]
        public string MotherMiddleName { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("MotherLastName")]
        [StringLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        [RegularExpression(@"^[a-zA-Z_ ]*$", ErrorMessage = "Use alphabets only")]
        public string MotherLastName { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.DropDown)]
        //[DisplayName("Student RelationShip")]
        //[LookUp("LookUps.GuardianType")]
        //public string MotherStudentRelationShip { get; set; }
        //public byte? MotherStudentRelationShipID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Nationality")]
        [LookUp("LookUps.Nationality")]
        public string MotherCountry { get; set; }
        public int? MotherCountryID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("NationalIDNo")]
        [MaxLength(11, ErrorMessage = "Max 11 characters"), MinLength(11, ErrorMessage = "Min 11 characters")]
        [RegularExpression(@"^[0-9*#+]+$", ErrorMessage = "Use  digits only")]
        public string MotherNationalID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("NationalIDIssueDate")]
        public string MotherNationalDNoIssueDateString { get; set; }
        public DateTime? MotherNationalDNoIssueDate { get; set; }


        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("NationalIDExpiryDate")]
        public string MotherNationaIDNoExpiryDateString { get; set; }
        public DateTime? MotherNationaIDNoExpiryDate { get; set; }
        public long? MotherPassportDetailNoID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(12, ErrorMessage = "Maximum Length should be within 12!")]
        [CustomDisplay("Passport No.")]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Use alphanumeric only")]
        public string MotherPassportNumber { get; set; }


        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Passport Issue Country")]
        [LookUp("LookUps.Country")]
        public string MotherCountryofIssue { get; set; }

        public int? MotherCountryofIssueID { get; set; }
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Passport Issue Date")]
        public string MotherPassportNoIssueString { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Passport Expiry Date")]
        public string MotherPassportNoExpiryString { get; set; }

        public long? MotherVisaDetailNoID { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[MaxLength(11, ErrorMessage = "Maximum Length should be within 11!")]
        //[DisplayName("Visa No.")]
        //[RegularExpression(@"^[0-9*#+]+$", ErrorMessage = "Use  digits only")]
        //public string MotherVisaNo { get; set; }


        //[ControlType(Framework.Enums.ControlTypes.DatePicker)]
        //[DisplayName("Visa Issue Date")]
        //public string MotherVisaIssueDateString { get; set; }



        //[ControlType(Framework.Enums.ControlTypes.DatePicker)]
        //[DisplayName("Visa Expiry Date")]
        //public string MotherVisaExpiryDateString { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine4 { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("MobileNo.")]
        //[MaxLength(13, ErrorMessage = "Max 13 characters"), MinLength(13, ErrorMessage = "Min 13 characters")]
        [RegularExpression(@"^[0-9*#+ ]+$", ErrorMessage = "Use numbers only")]
        public string MotherMobileNumber { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, Attributes = "ng-change='FillMotherWhatsappNoFromMobileNo(CRUDModel.ViewModel.FatherMotherDetails)'")]
        [CustomDisplay("SetMobileNo.asWhatsAppNo.")]
        public bool? IsMotherWhatsAppNoSameAsMobile { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("WhatsApp No.")]
        //[MaxLength(13, ErrorMessage = "Max 13 characters"), MinLength(13, ErrorMessage = "Min 13 characters")]
        [RegularExpression(@"^[0-9*#+ ]+$", ErrorMessage = "Use  digits only")]
        public string MotherWhatsappMobileNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine7 { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(25, ErrorMessage = "Maximum Length should be within 25!")]
        [CustomDisplay("Occupation")]
        //[RegularExpression(@"^[a-zA-Z_ ]*$", ErrorMessage = "Use alphabets only")]
        public string MotherOccupation { get; set; }
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        [CustomDisplay("CompanyName")]
        //[RegularExpression(@"^[a-zA-Z_ ]*$", ErrorMessage = "Use alphabets only")]
        public string MotherCompanyName { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [CustomDisplay("EmailID")]
        [RegularExpression("[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?\\.)+[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?", ErrorMessage = "InValid Email Address")]
        public string MotherEmailID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("InWhichSphereofSchoollifeCanYouVolunteerToHelp?")]
        [LookUp("LookUps.VolunteerType")]

        public string CanYouVolunteerToHelpTwoString { get; set; }
        public int? CanYouVolunteerToHelpTwoID { get; set; }
    }
}
