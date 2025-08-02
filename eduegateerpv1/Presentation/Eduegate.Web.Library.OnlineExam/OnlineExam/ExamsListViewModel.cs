using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Translator;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Services.Contracts.OnlineExam.Exam;
using System;

using Eduegate.Domain;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Eduegate.Web.Library.OnlineExam.OnlineExam
{
    public class ExamsListViewModel : BaseMasterViewModel
    {
        public ExamsListViewModel()
        {

        }

        public long? CandidateID { get; set; }

        public string CandidateName { get; set; }

        public long? OnlineExamID { get; set; }

        public double? Duration { get; set; }

        public double? AdditionalTime { get; set; }

        public double? TotalExamDuration { get; set; }

        public byte? OnlineExamStatusID { get; set; }

        public byte? OnlineExamOperationStatusID { get; set; }

        public string OnlineExamName { get; set; }

        public string OnlineExamDescription { get; set; }

        public string OnlineExamStatusName { get; set; }

        public string OnlineExamOperationStatusName { get; set; }

        public string Email { get; set; }

        public string Telephone { get; set; }

        public string MobileNumber { get; set; }

        public string Notes { get; set; }

        public string ExamName { get; set; }

        public string ExamDescription { get; set; }

        public double? MinimumDuration { get; set; }

        public double? MaximumDuration { get; set; }

        public double? PassPercentage { get; set; }

        public int? PassNos { get; set; }

        public byte QuestionSelectionID { get; set; }

        public string QuestionSelectionName { get; set; }

        public decimal? MaximumMarks { get; set; }

        public decimal? MinimumMarks { get; set; }

        public int? ClassID { get; set; }

        public long? CandidateOnlinExamMapID { get; set; }

        public bool? IsCandidateConductedExam { get; set; }

        public DateTime? ExamStartTime { get; set; }

        public DateTime? ExamEndTime { get; set; }

        public decimal? CandidateExamQuestionsTotalMarks { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as OnlineExamsDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<ExamsListViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<OnlineExamsDTO, ExamsListViewModel>.CreateMap();
            var onlineExamsDTO = dto as OnlineExamsDTO;
            var vm = Mapper<OnlineExamsDTO, ExamsListViewModel>.Map(onlineExamsDTO);

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<ExamsListViewModel, OnlineExamsDTO>.CreateMap();
            var dto = Mapper<ExamsListViewModel, OnlineExamsDTO>.Map(this);

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<OnlineExamsDTO>(jsonString);
        }
    }
}