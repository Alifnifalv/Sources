using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Translator;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using Eduegate.Services.Contracts.OnlineExam.Exam;

namespace Eduegate.Web.Library.OnlineExam.OnlineExam
{
    public class CandidateResultEntryViewModel : BaseMasterViewModel
    {
        public CandidateResultEntryViewModel()
        {
            ResultSubjects = new List<CandidateResultSubjectViewModel>() { new CandidateResultSubjectViewModel() };
        }

        public decimal? MaxMark { get; set; }

        public string Remarks { get; set; }

        public long CandidateID { get; set; }

        public string CandidateName { get; set; }

        public List<CandidateResultSubjectViewModel> ResultSubjects { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as OnlineExamsDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<CandidateResultEntryViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<OnlineExamsDTO, CandidateResultEntryViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var onlineExamsDTO = dto as OnlineExamsDTO;
            var vm = Mapper<OnlineExamsDTO, CandidateResultEntryViewModel>.Map(onlineExamsDTO);
            
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<CandidateResultEntryViewModel, OnlineExamsDTO>.CreateMap();
            Mapper<KeyValueViewModel,KeyValueDTO>.CreateMap();
            var dto = Mapper<CandidateResultEntryViewModel, OnlineExamsDTO>.Map(this);

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<OnlineExamsDTO>(jsonString);
        }

    }
}