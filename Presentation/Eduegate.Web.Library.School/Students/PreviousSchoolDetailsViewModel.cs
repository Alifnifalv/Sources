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
using Eduegate.Web.Library.CRM.Leads;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Students
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "PreviousSchoolDetails", "CRUDModel.ViewModel.PreviousSchoolDetails")]
    [DisplayName("Previous School Details")]
    public class PreviousSchoolDetailsViewModel : BaseMasterViewModel
    {
        public PreviousSchoolDetailsViewModel()
        {
            //PreviousSchoolClass = new KeyValueViewModel();
            IsStudentStudiedBefore = false;
            IsStudentStudiedBeforeForPortal = false;
        }
        public bool IsStudentStudiedBeforeForPortal { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsStudentStudiedBefore")]
        public bool? IsStudentStudiedBefore { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-disabled = CRUDModel.ViewModel.PreviousSchoolDetails.IsStudentStudiedBefore!=true")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [CustomDisplay("PreviousSchoolName")]
        //[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use alphabets only")]
        public string PreviousSchoolName { get; set; }


        [ControlType(Framework.Enums.ControlTypes.DropDown, attribs: "ng-disabled = CRUDModel.ViewModel.PreviousSchoolDetails.IsStudentStudiedBefore!=true")]
        [CustomDisplay("PreviousSchoolSyllabus")]
        [LookUp("LookUps.Syllabus")]

        public string PreviousSchoolSyllabus { get; set; }
        public byte? PreviousSchoolSyllabusID { get; set; }
        public byte? SyllabusID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-disabled = CRUDModel.ViewModel.PreviousSchoolDetails.IsStudentStudiedBefore!=true")]
        [MaxLength(9, ErrorMessage = "Maximum Length should be within 9!")]
        [CustomDisplay("PreviousSchoolAcademicYear")]
        //[RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Use alphanumeric only")]
        //public string PreviousSchoolAcademicYear { get; set; }
        public string PreviousSchoolAcademicYear { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Class", "Numeric", false, "", optionalAttribute1: "ng-disabled = CRUDModel.ViewModel.PreviousSchoolDetails.IsStudentStudiedBefore!=true")]
        [LookUp("LookUps.PreviousClassNames")]
        [CustomDisplay("PreviousSchoolClassCompleted")]
        public KeyValueViewModel PreviousSchoolClass { get; set; }
        public string PreviousSchoolClassClassKey { get; set; }
        public int? PreviousSchoolClassCompletedID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-disabled = CRUDModel.ViewModel.PreviousSchoolDetails.IsStudentStudiedBefore!=true")]
        [MaxLength(200, ErrorMessage = "Maximum Length should be within 200!")]
        [CustomDisplay("PreviousSchoolAddress")]
        public string PreviousSchoolAddress { get; set; }
    }
}
