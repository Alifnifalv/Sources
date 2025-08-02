using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.School.Students
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "Guardians", "CRUDModel.ViewModel.Guardians")]
    [DisplayName("Guardian/Parent Details")]
    public class GaurdianViewModel : BaseMasterViewModel
    {
        public GaurdianViewModel()
        {

        }

        [ControlType(Framework.Enums.ControlTypes.DataPicker)]
        [DisplayName("Parent Pick")]
        //[CustomDisplay("FromStudentApplication")]
        [DataPicker("ParentAdvancedSearch")]
        public string ReferenceParentNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("ParentCode.")]
        public string ParentCode { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }
       
        public long ParentStudentMapIID { get; set; }

        public long ParentIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine9 { get; set; }

        #region Father details

        [ControlType(Framework.Enums.ControlTypes.Label, "fullwidth alignleft")]
        [CustomDisplay("FatherDetails:")]
        public bool? FatherDetails { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.FileUpload)]
        //[FileUploadInfo("Content/UploadContents", EduegateImageTypes.UserProfile, "PictureUrl")]
        //[DisplayName("Guardian Photo")]
        //public string FatherProfile { get; set; }
        //public string FatherProfileUrl { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("FatherFirstName")]
        [StringLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        [RegularExpression(@"^[a-zA-Z_ ]*$", ErrorMessage = "Use alphabets only")]
        public string FatherFirstName { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("FatherMiddleName")]
        [StringLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        [RegularExpression(@"^[a-zA-Z_ ]*$", ErrorMessage = "Use alphabets only")]
        public string FatherMiddleName { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("FatherLastName")]
        [StringLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        [RegularExpression(@"^[a-zA-Z_ ]*$", ErrorMessage = "Use alphabets only")]
        public string FatherLastName { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[DisplayName("Father Occupation")]
        ////[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use alphabets only")]
        //public string FatherOccupation { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [CustomDisplay("Occupation")]
        public string FatherOccupation { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        [CustomDisplay("CompanyName")]
        //[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use alphabets only")]
        public string FatherCompanyName { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[DisplayName("Father Phone Number")]
        //[RegularExpression(@"^[0-9]*$", ErrorMessage = "Use numbers only")]
        //public string PhoneNumber { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(13, ErrorMessage = "Maximum Length should be within 13!")]
        [CustomDisplay("MobileNo.")]
        //[RegularExpression(@"^[0-9]*$", ErrorMessage = "Use numbers only")]
        public string FatherPhoneNumber { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, Attributes = "ng-change='FillFatherWhatsappNoFromMobileNo(CRUDModel.ViewModel.Guardians)'")]
        [CustomDisplay("Set Mobile No. As WhatsApp No.")]
        public bool? IsFatherWhatsAppNoSameAsMobile { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(13, ErrorMessage = "Maximum Length should be within 13!")]
        [CustomDisplay("WhatsAppNo")]
        //[RegularExpression(@"^[0-9]*$", ErrorMessage = "Use numbers only")]
        public string FatherWhatsappMobileNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("OtherMobileNo.")]
        //[MaxLength(13,ErrorMessage = "Maximum 13 characters"), MinLength(13,ErrorMessage = "Minimum 13 characteGaurdianEmailrs")]
        //[RegularExpression(@"^[0-9*#+]+$", ErrorMessage = "Use  digits only")]
        public string FatherMobileNumberTwo { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[DisplayName(" Father Email ID")]
        //[RegularExpression("[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", ErrorMessage = "InValid Email Address")]
        //public string EmailID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [CustomDisplay("EmailID")]
        public string FatherEmailID { get; set; }

        //[Required] 06/APR/2022
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Nationality")]
        [LookUp("LookUps.Nationality")]
        public string FatherCountry { get; set; }
        public int? FatherCountryID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("NationalIDNo")]
        [RegularExpression(@"^[0-9*#+]+$", ErrorMessage = "Use  digits only")]
        [MaxLength(11,ErrorMessage = "Maximum 11 characters"), MinLength(11,ErrorMessage = "Minimum 11 characters")]
        public string FatherNationalID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("NationalIDIssueDate")]
        public string FatherNationalDNoIssueDateString { get; set; }
        public DateTime? FatherNationalDNoIssueDate { get; set; }


        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("NationalIDExpiryDate")]
        public string FatherNationalDNoExpiryDateString { get; set; }
        public DateTime? FatherNationalDNoExpiryDate { get; set; }

        //[Required] 06/APR/2022
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(12, ErrorMessage = "Maximum Length should be within 12!")]
        [CustomDisplay("Passport No.")]
        //[RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Use alphanumeric only")]
        public string FatherPassportNumber { get; set; }

        //[Required] 06/APR/2022
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Passport Issue Country")]
        [LookUp("LookUps.Countries")]
        public string FatherCountryofIssue { get; set; }
        public int? FatherCountryofIssueID { get; set; }

        public int? FatherPassportCountryofIssueID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Passport Issue Date")]
        public string FatherPassportNoIssueString { get; set; }
        public DateTime? FatherPassportNoIssueDate { get; set; }


        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Passport Expiry Date")]
        public string FatherPassportNoExpiryString { get; set; }
        public DateTime? FatherPassportNoExpiryDate { get; set; }


        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("CanYouVolunteerToHelp")]
        [LookUp("LookUps.VolunteerType")]
        public string CanYouVolunteerToHelpOneString { get; set; }
        public int? CanYouVolunteerToHelpOneID { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.DropDown)]
        //[DisplayName("RelationShip")]
        //[LookUp("LookUps.GaurdianType")]
        //public string GuardianType { get; set; }

        //public byte? GuardianTypeID { get; set; }

        #endregion Father details

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine { get; set; }

        #region Mother details

        [ControlType(Framework.Enums.ControlTypes.Label, "fullwidth alignleft")]
        [CustomDisplay("MotherDetails:")]
        public bool? MotherDetails { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.FileUpload)]
        //[FileUploadInfo("Content/UploadContents", EduegateImageTypes.UserProfile, "PictureUrl1")]
        //[DisplayName("Mother Photo")]
        //public string MotherPofile { get; set; }
        //public string MotherPofileUrl { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        //[DisplayName("")]
        //public string NewLine3 { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[DisplayName("Mother Name")]
        //public string MotherName { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("MotherFirstName")]
        [StringLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        [RegularExpression(@"^[a-zA-Z_ ]*$", ErrorMessage = "Use alphabets only")]
        public string MotherFirstName { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("MotherMiddleName")]
        [StringLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        [RegularExpression(@"^[a-zA-Z_ ]*$", ErrorMessage = "Use alphabets only")]
        public string MotherMiddleName { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("MotherLastName")]
        [StringLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        [RegularExpression(@"^[a-zA-Z_ ]*$", ErrorMessage = "Use alphabets only")]
        public string MotherLastName { get; set; }

        //[Required] 06/APR/2022
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Nationality")]
        [LookUp("LookUps.Nationality")]
        public string MotherCountry { get; set; }
        public int? MotherCountryID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("NationalIDNo")]
        [MaxLength(11,ErrorMessage = "Maximum 11 characters"), MinLength(11,ErrorMessage = "Minimum 11 characters")]
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

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(12, ErrorMessage = "Maximum Length should be within 12!")]
        [CustomDisplay("Passport No.")]
        //[RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Use alphanumeric only")]
        public string MotherPassportNumber { get; set; }


        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Passport Issue Country")]
        [LookUp("LookUps.Countries")]
        public string MotherCountryofIssue { get; set; }

        public int? MotherCountryofIssueID { get; set; }
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Passport Issue Date")]
        public string MotherPassportNoIssueString { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Passport Expiry Date")]
        public string MotherPassportNoExpiryString { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(13, ErrorMessage = "Maximum Length should be within 13!")]
        [CustomDisplay("MobileNo.")]
        //[RegularExpression(@"^[0-9]*$", ErrorMessage = "Use numbers only")]
        public string MotherPhone { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, Attributes = "ng-change='FillMotherWhatsappNoFromMobileNo(CRUDModel.ViewModel.Guardians)'")]
        [CustomDisplay("Set Mobile No. As WhatsApp No.")]
        public bool? IsMotherWhatsAppNoSameAsMobile { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(13, ErrorMessage = "Maximum Length should be within 13!")]
        [CustomDisplay("WhatsAppNo")]
        //[RegularExpression(@"^[0-9]*$", ErrorMessage = "Use numbers only")]
        public string MotherWhatsappMobileNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        [CustomDisplay("CompanyName")]
        //[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use alphabets only")]
        public string MotherCompanyName { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [CustomDisplay("Occupation")]
        public string MotherOccupation { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [CustomDisplay("EmailID")]
        //[RegularExpression("[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", ErrorMessage = "InValid Email Address")]
        public string MotherEmailID { get; set; }
        //public List<KeyValueViewModel> Countries { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("CanYouVolunteerToHelp")]
        [LookUp("LookUps.VolunteerType")]

        public string CanYouVolunteerToHelpTwoString { get; set; }
        public int? CanYouVolunteerToHelpTwoID { get; set; }

        #endregion Mother details

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine4 { get; set; }

        #region Guardian details

        //public string GuardianPhotoUrl { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.FileUpload)]
        //[FileUploadInfo("Content/UploadContents", EduegateImageTypes.UserProfile, "PictureUrl2")]
        //[DisplayName("Guardian Photo")]
        //public string GuardianPhoto { get; set; }


        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[DisplayName("Guardian Name")]
        //public string GuardianName { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[DisplayName("Guardian Relation")]
        // public string GuardianRelation { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[DisplayName("Guardian Occupation")]
        //public string GuardianOccupation { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.TextArea)]
        //[DisplayName("Guardian Address")]
        //public string GuardianAddress { get; set; }

        public long? GuardianVisaDetailNoID { get; set; }

        public long? GuardianPassportDetailNoID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "fullwidth alignleft")]
        [CustomDisplay("GuardianDetails")]
        public bool? GuardianDetails { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, Attributes = "ng-change='FillFatherDetailstoGuardian($event, $element,CRUDModel.ViewModel)'")]
        [CustomDisplay("FatherDetailsasGuardianDetails")]
        public bool? IsFatherDetailsAsGuardianDetail { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, Attributes = "ng-change='FillMotherDetailstoGuardian($event, $element,CRUDModel.ViewModel)'")]
        [CustomDisplay("MotherDetailsasGuardianDetails")]
        public bool? IsMotherDetailsAsGuardianDetail { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine6 { get; set; }

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

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("RelationShip")]
        [LookUp("LookUps.GuardianType")]
        public string GuardianType { get; set; }
        public byte? GuardianTypeID { get; set; }

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

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("MobileNo.")]
        [RegularExpression(@"^[0-9*#+ ]+$", ErrorMessage = "Use  digits only")]
        public string GuardianPhone { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, Attributes = "ng-change='FillGuardianWhatsappNoFromMobileNo(CRUDModel.ViewModel.Guardians)'")]
        [CustomDisplay("Set Mobile No. As WhatsApp No.")]
        public bool? IsGuardianWhatsAppNoSameAsMobile { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("WhatsAppNo")]
        [RegularExpression(@"^[0-9*#+ ]+$", ErrorMessage = "Use  digits only")]
        public string GuardianWhatsappMobileNo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [CustomDisplay("EmailID")]
        [RegularExpression("[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", ErrorMessage = "InValid Email Address")]
        public string GaurdianEmail { get; set; }

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
        [LookUp("LookUps.Countries")]
        public string GuardianCountryofIssue { get; set; }

        public int? GuardianCountryofIssueID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Passport Issue Date")]
        public string GuardianPassportNoIssueString { get; set; }


        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Passport Expiry Date")]
        public string GuardianPassportNoExpiryString { get; set; }

        #endregion Guardian details

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine5 { get; set; }

        #region Address details

        [ControlType(Framework.Enums.ControlTypes.Label, "fullwidth alignleft")]
        [CustomDisplay("AddressDetails")]
        public bool? GuardianAddressDetails { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        [CustomDisplay("BuildingNo")]
        public string BuildingNo { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        [CustomDisplay("Flat/UnitNo")]
        public string FlatNo { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        [CustomDisplay("StreetNo")]
        public string StreetNo { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        [CustomDisplay("StreetName")]
        public string StreetName { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        [CustomDisplay("Zone/LocationNo")]
        public string LocationNo { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        [CustomDisplay("Zone/LocationName")]
        public string LocationName { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(8, ErrorMessage = "Maximum Length should be within 8!")]
        [CustomDisplay("ZipNo")]
        [RegularExpression(@"^[0-9*#+]+$", ErrorMessage = "Use  digits only")]
        public string ZipNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(8, ErrorMessage = "Maximum Length should be within 8!")]
        [CustomDisplay("PostBoxNo")]
        [RegularExpression(@"^[0-9*#+]+$", ErrorMessage = "Use  digits only")]
        public string PostBoxNo { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        [CustomDisplay("City")]
        public string City { get; set; }


        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Country")]
        [LookUp("LookUps.Countries")]
        public string Country { get; set; }
        public int? CountryID { get; set; }

        #endregion Address details

    }
}