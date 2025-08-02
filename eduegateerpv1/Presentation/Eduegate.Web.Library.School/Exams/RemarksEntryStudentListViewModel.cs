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

namespace Eduegate.Web.Library.School.Exams
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "StudentList", "CRUDModel.ViewModel.StudentList")]
    [DisplayName("Student List")]
    public class RemarksEntryStudentListViewModel : BaseMasterViewModel
    {
        public RemarksEntryStudentListViewModel()
        {
            //ExamSubjectGrid = new List<RemarksEntryExamMapViewModel>() { new RemarksEntryExamMapViewModel() };
        }

        public long RemarksEntryStudentMapIID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [DisplayName("Sr.No")]
        public long SerialNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "alignleft")]
        [CustomDisplay("StudentName")]
        public string StudentName { get; set; }
        public long? StudentID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [CustomDisplay("Remarks")]
        public string Remarks1 { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextArea)]
        //[DisplayName("Remarks 2")]
        //public string Remarks2 { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.GridGroup, "onecol-header-left", Attributes4 = "colspan=4")]
        //[DisplayName("")]
        //public List<RemarksEntryExamMapViewModel> ExamSubjectGrid { get; set; }
    }
}
