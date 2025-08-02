using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.School.Exams
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "ExamSubjectGrid", "gridModel.ExamSubjectGrid", "", "", "", "examGrid")]
    [DisplayName("Remark Exam Subjects")]
    public class RemarksEntryExamMapViewModel : BaseMasterViewModel
    {
        public RemarksEntryExamMapViewModel()
        {
            Exam = new KeyValueViewModel();
            Subject = new KeyValueViewModel();
        }

        public long RemarksEntryExamMapIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Exam", "Numeric", false, "")]
        [LookUp("LookUps.Exams")]
        [DisplayName("Exam")]
        public KeyValueViewModel Exam { get; set; }
        public long? ExamID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Subject", "Numeric", false, "")]
        [LookUp("LookUps.Subject")]
        [DisplayName("Subject")]
        public KeyValueViewModel Subject { get; set; }

        public int? SubjectID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [DisplayName("Remarks")]
        public string Remarks { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index, ModelStructure.StudentList[0], gridModel.ExamSubjectGrid)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, ModelStructure.StudentList[0], gridModel.ExamSubjectGrid)'")]
        [DisplayName("-")]
        public string Remove { get; set; }

    }
}
