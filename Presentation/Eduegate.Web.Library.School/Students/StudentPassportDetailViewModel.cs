using Eduegate.Domain;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.School.Students
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "StudentPassportDetails", "CRUDModel.ViewModel.StudentPassportDetails")]
    [DisplayName("Student Passport Details")]
    public class StudentPassportDetailViewModel : BaseMasterViewModel
    {
        public StudentPassportDetailViewModel()
        {
            IndianNationalityID = int.Parse(new Domain.Setting.SettingBL(null).GetSettingValue<string>("NATIONALITY_ID_INDIAN"));
            //CountryofIssue = new KeyValueViewModel();
            //Nationality = new KeyValueViewModel();
            //CountryofBirth = new KeyValueViewModel();
        }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine { get; set; }
        public long StudentPassportDetailsIID { get; set; }

        public long? StudentID { get; set; }

        //[Required]
        [MaxLength(12, ErrorMessage = "Maximum Length should be within 12!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Passport No.")]
        public string PassportNo { get; set; }

        //[Required] 06/APR/2022
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Countries", "Numeric", false, "")]
        [LookUp("LookUps.Countries")]
        [CustomDisplay("CountryofIssue")]
        public KeyValueViewModel CountryofIssue { get; set; }
        public int? CountryofIssueID { get; set; }

        //[Required] 06/APR/2022
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Passport Expiry Date")]
        public string PassportNoString { get; set; }
        public DateTime? PassportNoExpiry { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        //[Required] 06/APR/2022
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Countries", "Numeric", false, "")]
        [LookUp("LookUps.Countries")]
        [CustomDisplay("Country Of Birth")]
        public KeyValueViewModel CountryofBirth { get; set; }
        public int? CountryofBirthID { get; set; }

        //[Required] 06/APR/2022
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Countries", "Numeric", false, "")]
        [LookUp("LookUps.Nationality")]
        [CustomDisplay("Nationality")]
        public KeyValueViewModel Nationality { get; set; }
        public int? NationalityID { get; set; }

        public int? IndianNationalityID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-disabled = CRUDModel.ViewModel.StudentPassportDetails.Nationality.Key!=CRUDModel.ViewModel.StudentPassportDetails.IndianNationalityID")]
        [CustomDisplay("Adhaar No")]
        [MaxLength(12, ErrorMessage = "Max 12 characters"), MinLength(12, ErrorMessage = "Min 12 characters")]
        [RegularExpression(@"^[0-9*#+]+$", ErrorMessage = "Use  digits only")]
        public string AdhaarCardNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(11, ErrorMessage = "Maximum Length should be within 11!")]
        [CustomDisplay("Visa No")]
        public string VisaNo { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("VisaExpiryDate")]
        public string VisaExpiryString { get; set; }
        public DateTime? VisaExpiry { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }
        //[Required] 06/APR/2022
        [MaxLength(11, ErrorMessage = "Maximum Length should be within 11!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("NationalIDNo")]
        public string NationalIDNo { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("NationalIDExpiryDate")]
        public string NationalIDNoExpiryString { get; set; }
        public DateTime? NationalIDNoExpiry { get; set; }
    }
}
