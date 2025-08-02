using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Translator;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Services.Contracts.OnlineExam.Exam;
using System.Collections.Generic;

namespace Eduegate.Web.Library.OnlineExam.OnlineExam
{
    public class ExamQuestionViewModel : BaseMasterViewModel
    {
        public ExamQuestionViewModel()
        {

        }

        public long OnlineExamQuestionIID { get; set; }

        public long? CandidateID { get; set; }

        public long? OnlineExamIID { get; set; }

        public string ExamName { get; set; }

        public string ExamDescription { get; set; }

        public string GroupName { get; set; }

        public long? QuestionIID { get; set; }

        public string Question { get; set; }

        public string AnswerType { get; set; }

        public long? QuestionOptionCount { get; set; }

        public List<OnlineQuestionOptionsViewModel> QuestionOptions { get; set; }

        public double? ExamMaximumDuration { get; set; }

        public string QuestionAnswer { get; set; }

        public long? CandidateOnlinExamMapID { get; set; }

        public string PassageQuestion { get; set; }

        public long? PassageQuestionID { get; set; }

        public bool IsPassageQn { get; set; }

        public string DocFile { get; set; }

        public long? SelectedOption { get; set; }

        public List<ExamQuestionViewModel> PassageQuestions { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as OnlineQuestionsDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<ExamQuestionViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<OnlineQuestionsDTO, ExamQuestionViewModel>.CreateMap();
            var onlineExamsDTO = dto as OnlineQuestionsDTO;
            var vm = Mapper<OnlineQuestionsDTO, ExamQuestionViewModel>.Map(onlineExamsDTO);

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<ExamQuestionViewModel, OnlineQuestionsDTO>.CreateMap();
            var dto = Mapper<ExamQuestionViewModel, OnlineQuestionsDTO>.Map(this);

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<OnlineQuestionsDTO>(jsonString);
        }

    }
}