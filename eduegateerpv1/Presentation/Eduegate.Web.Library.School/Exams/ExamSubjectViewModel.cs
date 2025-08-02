using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.School.Exams
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "ExamSubject", "CRUDModel.ViewModel.ExamSubject")]
    [DisplayName("Subject")]
    public class ExamSubjectViewModel : BaseMasterViewModel
    {
        public ExamSubjectViewModel()
        {
            ExamSubjects = new List<ExamSubjectMapsViewModel>() { new ExamSubjectMapsViewModel() };
        }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [DisplayName(" ")]
        public List<ExamSubjectMapsViewModel> ExamSubjects { get; set; }
    }
}
