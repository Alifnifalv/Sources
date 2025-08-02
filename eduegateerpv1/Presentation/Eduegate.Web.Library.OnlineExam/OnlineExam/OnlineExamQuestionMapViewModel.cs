using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Services.Contracts.OnlineExam.Exam;
using Eduegate.Web.Library.Common;
using System.Collections.Generic;

namespace Eduegate.Web.Library.OnlineExam.OnlineExam
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "OnlineExamQuestionMap", "CRUDModel.ViewModel")]
    [DisplayName("Online Exam Question Map")]
    public class OnlineExamQuestionMapViewModel : BaseMasterViewModel
    {
        public OnlineExamQuestionMapViewModel()
        {
            OnlineExamQuestionMapDetail = new List<OnlineExamQuestionMapDetailViewModel>() { new OnlineExamQuestionMapDetailViewModel() };
            Questions = new List<KeyValueViewModel>();
        }

        public long OnlineExamQuestionIID { get; set; }

        public int? AcademicYearID { get; set; }

        public byte? SchoolID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("OnlineExams", "Numeric", false, "OnlineExamChanges(CRUDModel.ViewModel)", false, optionalAttribute1: "ng-disabled='CRUDModel.ViewModel.OnlineExamQuestionIID !=0'")]
        [LookUp("LookUps.OnlineExams")]
        [CustomDisplay("OnlineExam")]
        public KeyValueViewModel OnlineExam { get; set; }
        public long? OnlineExamID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Exam Name")]
        public string ExamName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Exam Description")]
        public string ExamDescription { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Candidate", "Numeric", false)]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Candidate", "LookUps.Candidate")]
        //[LookUp("LookUps.Candidate")]
        [CustomDisplay("Candidate")]
        public KeyValueViewModel Candidate { get; set; }
        public long? CandidateID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "fullwidth alignleft")]
        [Select2("Questions", "Numeric", true, "QuestionSelects($event, $element, CRUDModel.ViewModel)")]
        [LookUp("LookUps.Questions")]
        [CustomDisplay("Question")]
        public List<KeyValueViewModel> Questions { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [DisplayName("Question Details")]
        public List<OnlineExamQuestionMapDetailViewModel> OnlineExamQuestionMapDetail { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as OnlineExamQuestionDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<OnlineExamQuestionMapViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<OnlineExamQuestionDTO, OnlineExamQuestionMapViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var qDTO = dto as OnlineExamQuestionDTO;
            var vm = Mapper<OnlineExamQuestionDTO, OnlineExamQuestionMapViewModel>.Map(qDTO);

            vm.OnlineExamQuestionIID = qDTO.OnlineExamQuestionIID;
            vm.OnlineExamID = qDTO.OnlineExamID;
            vm.AcademicYearID = qDTO.AcademicYearID;
            vm.ExamName = qDTO.ExamName;
            vm.ExamDescription = qDTO.ExamDescription;
            vm.SchoolID = qDTO.SchoolID;
            vm.OnlineExam = qDTO.OnlineExamID.HasValue ? new KeyValueViewModel() { Key = qDTO.OnlineExamID.ToString(), Value = qDTO.ExamName } : new KeyValueViewModel();
            vm.Candidate = qDTO.CandidateID.HasValue ? new KeyValueViewModel() { Key = qDTO.CandidateID.ToString(), Value = qDTO.CandidateName } : new KeyValueViewModel();

            vm.Questions = new List<KeyValueViewModel>();
            vm.OnlineExamQuestionMapDetail = new List<OnlineExamQuestionMapDetailViewModel>();

            foreach (var mapDatas in qDTO.QuestionListMap)
            {
                vm.Questions.Add(new KeyValueViewModel()
                {
                    Key = mapDatas.QuestionID.HasValue ? mapDatas.QuestionID.ToString() : null,
                    Value = mapDatas.Question != null ? mapDatas.Question : null,
                });

                vm.OnlineExamQuestionMapDetail.Add(new OnlineExamQuestionMapDetailViewModel()
                {
                    QuestionID = mapDatas.QuestionID.HasValue ? mapDatas.QuestionID : null,
                    Question = mapDatas.Question != null ? mapDatas.Question : null,
                    AnswerType = mapDatas.AnswerType != null ? mapDatas.AnswerType : null,
                    QuestionOptionCount = mapDatas.QuestionOptionCount != null ? mapDatas.QuestionOptionCount : 0,
                    Points=mapDatas.Points != null ? mapDatas.Points : null,
                });
            }
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<OnlineExamQuestionMapViewModel, OnlineExamQuestionDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = Mapper<OnlineExamQuestionMapViewModel, OnlineExamQuestionDTO>.Map(this);

            dto.OnlineExamQuestionIID = this.OnlineExamQuestionIID;
            dto.OnlineExamID = this.OnlineExam == null || string.IsNullOrEmpty(this.OnlineExam.Key) ? (int?)null : int.Parse(this.OnlineExam.Key);
            dto.ExamName = this.ExamName != null ? this.ExamName : null;
            dto.ExamDescription = this.ExamDescription != null ? this.ExamDescription : null;
            dto.AcademicYearID = this.AcademicYearID.HasValue ? this.AcademicYearID : null;
            dto.SchoolID = this.SchoolID.HasValue ? this.SchoolID : null;
            dto.CandidateID = this.Candidate == null || string.IsNullOrEmpty(this.Candidate.Key) ? (long?)null : long.Parse(this.Candidate.Key);

            foreach (var map in this.OnlineExamQuestionMapDetail)
            {
                if (map.QuestionID.HasValue)
                {
                    dto.QuestionListMap.Add(new OnlineExamQuestionDTO()
                    {
                        QuestionID = map.QuestionID.HasValue ? map.QuestionID : null,
                        Question = map.Question != null ? map.Question : null,
                        AnswerType = map.AnswerType != null ? map.AnswerType : null,
                        QuestionOptionCount = map.QuestionOptionCount != null ? map.QuestionOptionCount : 0,
                    });
                }
            }

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<OnlineExamQuestionDTO>(jsonString);
        }

    }
}