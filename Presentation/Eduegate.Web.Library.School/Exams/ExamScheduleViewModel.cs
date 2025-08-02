using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.Exams;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.School.Exams
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "ExamSchedule", "CRUDModel.ViewModel.ExamSchedule")]
    [DisplayName("Exam Schedule")]
    public class ExamScheduleViewModel : BaseMasterViewModel
    {

        public long ExamScheduleIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        public long? ExamID { get; set; }

        //[Required]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Room")]
        public string Room { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.DatePicker)]
        //[DisplayName("Schedule Date")]

        //public string ScheduleDateString { get; set; }
        //public DateTime? Date { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [DisplayName("Exam Start Date")]
        public string ExamStartDateString { get; set; }
        public DateTime? ExamStartDate { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [DisplayName("Exam End Date")]
        public string ExamEndDateString { get; set; }
        public DateTime? ExamEndDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine4 { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TimePicker)]
        [DisplayName("Start Time")]
        public string Starttime { get; set; }
        public System.DateTime? StartTime { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TimePicker)]
        [DisplayName("End Time")]
        public string Endtime { get; set; }
        public System.DateTime? EndTime { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine6 { get; set; }

        [Required]
        [MaxLength(3, ErrorMessage = "Maximum Length should be within 3!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Full Marks")]
        public double? FullMarks { get; set; }

        //[Required]
        [MaxLength(5, ErrorMessage = "Maximum Length should be within 5!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Passing Marks")]
        public double? PassingMarks { get; set; }
    }
}

