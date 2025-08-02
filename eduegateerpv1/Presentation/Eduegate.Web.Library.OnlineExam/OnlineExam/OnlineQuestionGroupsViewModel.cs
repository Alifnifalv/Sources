using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Services.Contracts.OnlineExam.Exam;

namespace Eduegate.Web.Library.OnlineExam.OnlineExam
{
    public class OnlineQuestionGroupsViewModel : BaseMasterViewModel
    {
        public OnlineQuestionGroupsViewModel()
        {

        }

        public int QuestionGroupID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Group Name")]
        public string GroupName { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Subject", "Numeric", false, "")]
        [LookUp("LookUps.Subject")]
        [DisplayName("Subject")]
        public KeyValueViewModel Subject { get; set; }
        public long? SubjectID { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as OnlineQuestionGroupsDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<OnlineQuestionGroupsViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<OnlineQuestionGroupsDTO, OnlineQuestionGroupsViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var qDTO = dto as OnlineQuestionGroupsDTO;
            var vm = Mapper<OnlineQuestionGroupsDTO, OnlineQuestionGroupsViewModel>.Map(qDTO);

            vm.QuestionGroupID = qDTO.QuestionGroupID;
            vm.GroupName = qDTO.GroupName;
            vm.Subject = qDTO.SubjectID.HasValue ? new KeyValueViewModel() { Key = qDTO.SubjectID.ToString(), Value = qDTO.SubjectName } : new KeyValueViewModel();

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<OnlineQuestionGroupsViewModel, OnlineQuestionGroupsDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = Mapper<OnlineQuestionGroupsViewModel, OnlineQuestionGroupsDTO>.Map(this);

            dto.QuestionGroupID = this.QuestionGroupID;
            dto.GroupName = this.GroupName;
            dto.SubjectID = this.Subject == null || string.IsNullOrEmpty(this.Subject.Key) ? (int?)null : int.Parse(this.Subject.Key);

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<OnlineQuestionGroupsDTO>(jsonString);
        }

    }
}