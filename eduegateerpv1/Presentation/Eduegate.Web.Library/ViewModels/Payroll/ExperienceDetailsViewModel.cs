using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Eduegate.Web.Library.ViewModels.Payroll
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "ExperienceDetails", "CRUDModel.ViewModel.ExperienceDetails")]
    [DisplayName("ExperienceDetails")]
    public class ExperienceDetailsViewModel : BaseMasterViewModel
    {
        public ExperienceDetailsViewModel()
        {
        }
        public long EmployeeExperienceDetailIID { get; set; }

        public long? EmployeeID { get; set; }




        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("From Date")]
        public string FromDateString { get; set; }
        public DateTime? FromDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("To Date ")]
        public string ToDateString { get; set; }

        public DateTime? ToDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Name of the School/Organization")]
        public string NameOfOraganizationtName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Curriculum/Industry")]
        public string CurriculamOrIndustry { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Grade/Designation")]

        public string Designation { get; set; }
        

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay(" Subjects Taught")]
        public string SubjectTaught { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Class Taught")]
        public string ClassTaught { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index, ModelStructure.ExperienceDetails[0], CRUDModel.ViewModel.ExperienceDetails)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, ModelStructure.ExperienceDetails[0],CRUDModel.ViewModel.ExperienceDetails)'")]
        [DisplayName("-")]
        public string Remove { get; set; }

    }
}








       