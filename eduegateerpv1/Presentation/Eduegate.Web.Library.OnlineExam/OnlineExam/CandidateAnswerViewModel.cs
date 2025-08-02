using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Translator;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Services.Contracts.OnlineExam.Exam;
using System;
using Eduegate.Framework.Mvc.Attributes;
using System.ComponentModel;
using Eduegate.Web.Library.Common;
using System.Collections.Generic;

namespace Eduegate.Web.Library.OnlineExam.OnlineExam
{
    public class CandidateAnswerViewModel : BaseMasterViewModel
    {
        public CandidateAnswerViewModel()
        {
            AnswerDetails = new List<CandidateAnswerDetailViewModel>() { new CandidateAnswerDetailViewModel() };
            QuestionOptionMapIDs = new List<long?>();
        }

        public long CandidateAnswerIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Candidate")]
        public string Candidate { get; set; }
        public long? CandidateID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Online Exam")]
        public string OnlineExam { get; set; }
        public long? OnlineExamID { get; set; }

        public long? CandidateOnlineExamMapID { get; set; }

        public DateTime? DateOfAnswer { get; set; }

        public string Comments { get; set; }

        public long? QuestionOptionMapID { get; set; }

        public string OtherDetails { get; set; }

        public string OtherAnswers { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("Answers")]
        public List<CandidateAnswerDetailViewModel> AnswerDetails { get; set; }

        public long? OnlineExamQuestionID { get; set; }

        public string OnlineExamQuestion { get; set; }

        public List<long?> QuestionOptionMapIDs { get; set; }


        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as CandidateAnswerDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<CandidateAnswerViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<CandidateAnswerDTO, CandidateAnswerViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var ansDTO = dto as CandidateAnswerDTO;
            var vm = Mapper<CandidateAnswerDTO, CandidateAnswerViewModel>.Map(ansDTO);

            vm.CandidateAnswerIID = ansDTO.CandidateAnswerIID;
            vm.CandidateOnlineExamMapID = ansDTO.CandidateOnlineExamMapID;
            vm.CandidateID = ansDTO.CandidateID;
            vm.Candidate = ansDTO.CandidateName;
            vm.OnlineExamID = ansDTO.OnlineExamID;
            vm.OnlineExam = ansDTO.OnlineExamName;

            vm.AnswerDetails = new List<CandidateAnswerDetailViewModel>();

            foreach (var answr in ansDTO.AnswerList)
            {
                var questions = new List<AnswerQuestionsViewModel>();
                
                foreach (var qn in answr.OnlineExamQuestionList)
                {
                    var questionOptions = new List<OnlineQuestionOptionsViewModel>();

                    foreach (var optionMap in qn.QuestionOptionMaps)
                    {
                        questionOptions.Add(new OnlineQuestionOptionsViewModel()
                        {
                            OptionText = optionMap.OptionText,
                        });
                    }

                    questions.Add(new AnswerQuestionsViewModel()
                    {
                        Question = qn.Question,
                        QuestionOptions = questionOptions
                    });
                }

                vm.AnswerDetails.Add(new CandidateAnswerDetailViewModel()
                {
                    CandidateAnswerIID = answr.CandidateAnswerIID,
                    Comments = answr.Comments,
                    QuestionOptionMapID = answr.QuestionOptionMapID,
                    QuestionOptionMap = answr.QuestionOptionMap,
                    OtherDetails = answr.OtherDetails,
                    OtherAnswers = answr.OtherAnswers,
                    Questions = questions
                });
            }

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<CandidateAnswerViewModel, CandidateAnswerDTO>.CreateMap();
            Mapper<KeyValueViewModel,KeyValueDTO>.CreateMap();
            var dto = Mapper<CandidateAnswerViewModel, CandidateAnswerDTO>.Map(this);

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<CandidateAnswerDTO>(jsonString);
        }

    }
}