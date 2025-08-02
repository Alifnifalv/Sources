using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.School.Exams
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "ExamClass", "CRUDModel.ViewModel.ExamClass")]
    [DisplayName("Class")]
    public class ExamClassViewModel : BaseMasterViewModel
    {
        public ExamClassViewModel()
        {
            ExamClasses = new List<ExamClassMapsViewModel>() { new ExamClassMapsViewModel() };
        }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("ExamClass")]
        public List<ExamClassMapsViewModel> ExamClasses { get; set; }
    }
}
