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
  public class MarkListSubjectMapViewModel : BaseMasterViewModel
    {


        public long MarkRegisterSubjectMapIID { get; set; }
        public long? MarkRegisterStudentMapID { get; set; }
        public long? MarkRegisterID { get; set; }
        public long? MarksGradeMapID { get; set; }
        public int? MarksGradeID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Hidden)]
        //[DisplayName("SubjectID")]
        public int? SubjectID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Subject")]
        public string Subject { get; set; }



        [ControlType(Framework.Enums.ControlTypes.Label, "textright")]
        [DisplayName("Minimum Mark")]
        public decimal? MinimumMark { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "textright")]
        [DisplayName("Maximum Mark")]
        public decimal? MaximumMark { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "textright")]
        [DisplayName("Mark")]
        public decimal? Mark { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "textleft", "small-col-width", Attributes = "ng-bind=GetGrade(gridModel,1)")]
        [DisplayName("Grade")]
        public string Grade { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, "textleft", "small-col-width")]//{{gridModel.MinimumMark < gridModel.Mark}}
        [DisplayName("Is Passed")]
        public bool? IsPassed { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, "textleft", "small-col-width")]
        [DisplayName("Is Absent")]
        public bool? IsAbsent { get; set; }



    }
}
