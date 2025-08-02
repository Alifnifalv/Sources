using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Exams;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Services.Contracts.School.Academics;
using System;

namespace Eduegate.Web.Library.School.Exams
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, " MarkList", "CRUDModel.ViewModel. MarkList")]
    [DisplayName("MarkList")]
    public class MarkListViewModel : BaseMasterViewModel
    {
        public MarkListViewModel()
        {
            Exam = new KeyValueViewModel();
            MarkRegisterDetailsSplit = new List<MarkListSubjectMapViewModel>() { new MarkListSubjectMapViewModel() };
        }

        [ControlType(Framework.Enums.ControlTypes.ExpandCollapseButton, "textleft", "small-col-width")]
        [DisplayName(" ")]
        public bool IsExpand { get; set; }

        public long MarkRegisterIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [DisplayName("Exam")]
        [Select2("Exams", "Numeric", false, "", optionalAttribute1: "ng-disabled=false")]
        [LookUp("LookUps.Exams")]
        public KeyValueViewModel Exam { get; set; }
        public long? ExamID { get; set; }
        
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [DisplayName("Mark")]
        public List<MarkListSubjectMapViewModel> MarkRegisterDetailsSplit { get; set; }

    }
}
