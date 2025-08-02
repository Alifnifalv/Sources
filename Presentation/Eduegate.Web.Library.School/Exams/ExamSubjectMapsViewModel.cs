using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;
using System;
using System.ComponentModel.DataAnnotations;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Exams
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "ExamSubject", "CRUDModel.ViewModel.ExamSubject.ExamSubjects")]
    [DisplayName("ExamSubject")]
    public class ExamSubjectMapsViewModel:BaseMasterViewModel
    {   
        public ExamSubjectMapsViewModel()
        {
            //Subject = new KeyValueViewModel();

        }
        public long ExamSubjectMapIID { get; set; }
       
        public long? ExamID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "onecol-header-left")]
        [Select2("SubjectType", "String", false, "SubjectTypeChange($event, $element, gridModel)")]
        [CustomDisplay("SubjectType")]
        [LookUp("LookUps.SubjectType")]
        public KeyValueViewModel SubjectType { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.DatePicker)]
        //[DisplayName("Exam Date")]
        //public string ExamDateString { get; set; }

        //public DateTime? ExamDate { get; set; }        

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("Subject")]
        [Select2("Subject", "Numeric", false, "")]
        [LookUp("LookUps.Subject")]
        public KeyValueViewModel Subject { get; set; }

        public int? SubjectID { get; set; }
       
       
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("MarkGrade", "Numeric", false)]
        [LookUp("LookUps.MarkGrade")]
        [CustomDisplay("MarkGrade")]
        public KeyValueViewModel MarkGrade { get; set; }
        public int? MarkGradeID { get; set; }

        //[Required]
        [MaxLength(5, ErrorMessage = "Maximum Length should be within 5!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("MinimumMarks")]

        public decimal? MinimumMarks { get; set; }

        //[Required]
        [MaxLength(5, ErrorMessage = "Maximum Length should be within 5!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("TotalMarks")]

        public decimal? MaximumMarks { get; set; }


        //[Required]
        [MaxLength(5, ErrorMessage = "Maximum Length should be within 5!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("ConversionFactor")]

        public decimal? ConversionFactor { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.TimePicker)]
        //[DisplayName("Start Time")]
        //public string StartTimeString { get; set; }
        //public System.TimeSpan? StartTime { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.TimePicker)]
        //[DisplayName("End Time")]
        //public string EndTimeString { get; set; }
        //public System.TimeSpan? EndTime { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index, ModelStructure.ExamSubject.ExamSubjects[0], CRUDModel.ViewModel.ExamSubject.ExamSubjects)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, ModelStructure.ExamSubject.ExamSubjects[0],CRUDModel.ViewModel.ExamSubject.ExamSubjects)'")]
        [DisplayName("-")]
        public string Remove { get; set; }
    }
}
