using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Exams
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "StudentList", "CRUDModel.ViewModel.StudentList")]
    [DisplayName("Student List")]
    public class HealthEntryStudentListViewModel : BaseMasterViewModel
    {
        public HealthEntryStudentListViewModel()
        {
            //ExamSubjectGrid = new List<RemarksEntryExamMapViewModel>() { new RemarksEntryExamMapViewModel() };
        }

        public long HealthEntryStudentMapIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [DisplayName("Sr.No")]
        public long SerialNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "alignleft")]
        [CustomDisplay("StudentName")]
        public string StudentName { get; set; }
        public long? StudentID { get; set; }

        //[MaxLength(7, ErrorMessage = "Maximum Length should be within 7!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width", Attributes3 = "ng-blur=StudentBMI(gridModel)")]
        [CustomDisplay("Height")]
        public decimal? Height { get; set; }

        //[MaxLength(7, ErrorMessage = "Maximum Length should be within 7!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width", Attributes3 = "ng-blur=StudentBMI(gridModel)")]
        [CustomDisplay("Weight")]
        public decimal? Weight { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("BMI")]
        public decimal? BMS { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Remarks")]
        public string Remarks { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.GridGroup, "onecol-header-left", Attributes4 = "colspan=4")]
        //[DisplayName("")]
        //public List<RemarksEntryExamMapViewModel> ExamSubjectGrid { get; set; }

    }
}
