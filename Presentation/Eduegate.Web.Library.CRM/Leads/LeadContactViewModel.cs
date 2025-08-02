using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Eduegate.Web.Library.Common;
using System.Globalization;
using Eduegate.Services.Contracts.Leads;
using Newtonsoft.Json;

namespace Eduegate.Web.Library.CRM.Leads
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "ContactDetails", "CRUDModel.ViewModel.ContactDetails")]
    [DisplayName("Contact Details")]
    public class LeadContactViewModel : BaseMasterViewModel
    {
        public LeadContactViewModel()
        {

        }

        public long? ContactID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("StudentName")]
        public string StudentName { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Gender")]
        [LookUp("LookUps.Gender")]
        public string Gender { get; set; }
        public byte? GenderID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("DateOfBirth")]
        public string DateOfBirthString { get; set; }
        public DateTime? DateOfBirth { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Academic Year")]
        [LookUp("LookUps.AcademicYear")]
        public string AcademicYear { get; set; }
        public int? AcademicYearID { get; set; }

        public string AcademicYearCode { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Curriculam")]
        [LookUp("LookUps.SchoolSyllabus")]

        public string CurriculamString { get; set; }
        public byte? CurriculamID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, attribs: "ng-change='ClassChanges($event, $element,CRUDModel.ViewModel)'")]
        [LookUp("LookUps.Classes")]
        [CustomDisplay("Class")]
        public string ClassName { get; set; }
        public int? ClassID { get; set; }
        public string Class { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("ParentName")]
        public string ParentName { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(13, ErrorMessage = "Maximum Length should be within 13!")]
        [CustomDisplay("MobileNumber")]
        public string MobileNumber { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Email")]
        public string EmailAddress { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("AdditionalDetails")]
        public string AdditionalDetails { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine4 { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("CivilIDNumber")]
        public string CivilIDNumber { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine5 { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("AddressName")]
        public string AddressName { get; set; }

        
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Flat")]
        public string Flat { get; set; }

        
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Block")]
        public string Block { get; set; }

        
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("TelephoneCode")]
        public string TelephoneCode { get; set; }

        
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("PhoneNo1")]
        public string PhoneNo1 { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("PhoneNo2")]
        public string PhoneNo2 { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("AlternateMobileNo1")]
        public string MobileNo1 { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("AlternateMobileNo2")]
        public string MobileNo2 { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("AlternateEmailID1")]
        public string AlternateEmailID1 { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("AlternateEmailID2")]
        public string AlternateEmailID2 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Nationality", "Numeric", false, "")]
        [LookUp("LookUps.Nationality")]
        [CustomDisplay("Nationality")]
        public KeyValueViewModel Nationality { get; set; }

    }
}
