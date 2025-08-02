using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.OnlineExam.OnlineExam
{

    [ContainerType(Framework.Enums.ContainerTypes.Grid, "CandidateExamDetails", "CRUDModel.ViewModel.CandidateExamDetails")]
    [DisplayName("Exam Details")]
    public class CandidateExamDetailViewModel : BaseMasterViewModel
    {

        public CandidateExamDetailViewModel()
        {
            OnlineExam = new KeyValueViewModel();
        }

        public long CandidateOnlinExamMapIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("OnlineExams", "Numeric", false, "OnlineExamChanges($event, $element, gridModel)")]
        [LookUp("LookUps.OnlineExams")]
        [CustomDisplay("OnlineExam")]
        public KeyValueViewModel OnlineExam { get; set; }
        public long? OnlineExamID { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("DurationInMinutes")]
        public double? Duration { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("AdditionalTimeInMinutes")]
        public double? AdditionalTime { get; set; }
        public double? OldAdditionalTime { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.OnlineExamStatus")]
        [CustomDisplay("OnlineExamStatus")]
        public string OnlineExamStatus { get; set; }
        public byte? OnlineExamStatusID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.OnlineExamOperationStatus")]
        [CustomDisplay("OnlineExamOperationStatus")]
        public string OnlineExamOperationStatus { get; set; }
        public byte? OnlineExamOperationStatusID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("StartTime")]
        public string StartTimeString { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("EndTime")]
        public string EndTimeString { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index,ModelStructure.CandidateExamDetails[0], CRUDModel.ViewModel.CandidateExamDetails)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, ModelStructure.CandidateExamDetails[0],CRUDModel.ViewModel.CandidateExamDetails)'")]
        [DisplayName("-")]
        public string Remove { get; set; }

    }
}
