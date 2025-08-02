using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;
using Eduegate.Services.Contracts.OnlineExam.Exam;
using System.Collections.Generic;

namespace Eduegate.Web.Library.OnlineExam.OnlineExam
{
    public class OnlineExamResultViewModel : BaseMasterViewModel
    {
        public OnlineExamResultViewModel()
        {
            QuestionMapResults = new List<OnlineExamResultQuestionViewModel>() { new OnlineExamResultQuestionViewModel() };
            Candidate = new KeyValueViewModel();
            OnlineExam = new KeyValueViewModel();
            ResultStatus = new KeyValueViewModel();
            IsExpand = false;
            CandidateResultEntry = new List<CandidateResultEntryViewModel>() { new CandidateResultEntryViewModel() };
        }

        public long OnlineExamResultIID { get; set; }

        public decimal? MarksObtained { get; set; }

        public KeyValueViewModel Candidate { get; set; }
        public long? CandidateID { get; set; }

        public string Remarks { get; set; }

        public KeyValueViewModel OnlineExam { get; set; }
        public long? OnlineExamID { get; set; }

        public string SchoolName { get; set; }
        public byte? SchoolID { get; set; }

        public string AcademicYear { get; set; }
        public int? AcademicYearID { get; set; }

        public KeyValueViewModel ResultStatus { get; set; }
        public byte? ResultStatusID { get; set; }

        public bool IsExpand { get; set; }

        public decimal? TotalMarks { get; set; }

        public decimal? ObtainedMarksPercentage { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [DisplayName("Question map")]
        public List<OnlineExamResultQuestionViewModel> QuestionMapResults { get; set; }

        public List<CandidateResultEntryViewModel> CandidateResultEntry { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as OnlineQuestionsDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<OnlineExamResultViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<OnlineQuestionsDTO, OnlineExamResultViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var qnDto = dto as OnlineQuestionsDTO;
            var vm = Mapper<OnlineQuestionsDTO, OnlineExamResultViewModel>.Map(qnDto);

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<OnlineExamResultViewModel, OnlineQuestionsDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = Mapper<OnlineExamResultViewModel, OnlineQuestionsDTO>.Map(this);

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<OnlineQuestionsDTO>(jsonString);
        }

    }
}