using Eduegate.Framework.Mvc.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.ViewModels.Payroll
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "PassportDetails", "CRUDModel.ViewModel.PassportDetails")]
    [DisplayName("Employee Other Details")]
    public class EmployeePassportDetailViewModel : BaseMasterViewModel
    {
        public EmployeePassportDetailViewModel()
        {

        }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName(" ")]
        public string NewLine { get; set; }
        public long EmployeePassportDetailsIID { get; set; }
        public long PassportVisaIID { get; set; }
        public long? EmployeeID { get; set; }
        public long? ReferenceID { get; set; }

        [Required]
        [MaxLength(12, ErrorMessage = "Maximum Length should be within 12!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Passport No.")]
        public string PassportNo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Countries", "String", false)]
        [LookUp("LookUps.Countries")]
        [CustomDisplay("CountryofIssue")]
        public KeyValueViewModel CountryofIssue { get; set; }
        //public int? CountryofIssueID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        [CustomDisplay("PassportIssuePlace")]
        public string PlaceOfIssue { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Passport Expiry Date")]
        public string PassportExpiryDateString { get; set; }
        public DateTime? PassportNoExpiry { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [Required]
        [MaxLength(11, ErrorMessage = "Maximum Length should be within 11!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("NationalIDNo")]
        public string NationalIDNo { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("NationalIDExpiryDate")]
        public string NationalIDNoExpiryString { get; set; }
        public DateTime? NationalIDNoExpiry { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }

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

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.SponsoredBy")]
        [CustomDisplay("SponsoredBy")]
        public string Sponsor { get; set; }

        public string UserType { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.PassageType")]
        [CustomDisplay("Passage Type")]
        public string PassageType { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.AccomodationType")]
        [CustomDisplay("AccomodationType")]
        public string AccomodationType { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(11, ErrorMessage = "Maximum Length should be within 11!")]
        [CustomDisplay("MOI ID")]
        public string MOIID { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(10, ErrorMessage = "Maximum Length should be within 11!")]
        [CustomDisplay("Health Card No.")]
        public string HealthCardNo { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(11, ErrorMessage = "Maximum Length should be within 11!")]
        [CustomDisplay("Labour Card No.")]
        public string LabourCardNo { get; set; }

    }
}
