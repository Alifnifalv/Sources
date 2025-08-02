using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Eduegate.Web.Library.ViewModels.Payroll
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "AcademicDetails", "CRUDModel.ViewModel.AcademicDetails")]
    [DisplayName("AcademicDetails")]
    public class AcademicDetailsViewModel : BaseMasterViewModel
    {
        public AcademicDetailsViewModel()
        {
        }
        public long EmployeeQualificationMapIID { get; set; }

        public long? EmployeeID { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.Qualifications")]
        [CustomDisplay("Qualifications")]
        public string Qualification { get; set; }
        public byte? QualificationID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("TitleoftheProgramme")]
        public string TitleOfProgramme { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("ModeofProgramme")]
        public string ModeOfProgramme { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("University/Institution/Board")]
        public string University { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("CoreSubjects")]
        public string Subject { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.Months")]
        [CustomDisplay("Graduation Month")]

        public string Months { get; set; }
        public int? GraduationMonth { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay(" YearofGraduation")]
        public int? GraduationYear { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Marksinpercentage")]
        public decimal? MarksInPercentage { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index, ModelStructure.AcademicDetails[0], CRUDModel.ViewModel.AcademicDetails)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, ModelStructure.AcademicDetails[0],CRUDModel.ViewModel.AcademicDetails)'")]
        [DisplayName("-")]
        public string Remove { get; set; }

    }
}








       