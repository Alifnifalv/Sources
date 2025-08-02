using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Services.Contracts.OnlineExam.Exam;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.OnlineExam.OnlineExam
{
    public class OnlinePassageQuestionsViewModel : BaseMasterViewModel
    {
        public OnlinePassageQuestionsViewModel()
        {

        }

        public long PassageQuestionIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.RichTextEditor)]
        [CustomDisplay("PassageQuestion")]
        public string PassageQuestion { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("ShortDescription")]
        public string ShortDescription { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("MinimumMarks")]
        public decimal? MinimumMark { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("MaximumMarks")]
        public decimal? MaximumMark { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as PassageQuestionDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<OnlinePassageQuestionsViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<PassageQuestionDTO, OnlinePassageQuestionsViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var qDTO = dto as PassageQuestionDTO;
            var vm = Mapper<PassageQuestionDTO, OnlinePassageQuestionsViewModel>.Map(qDTO);

            vm.PassageQuestionIID = qDTO.PassageQuestionIID;
            vm.PassageQuestion = qDTO.PassageQuestion;
            vm.ShortDescription = qDTO.ShortDescription;
            vm.MinimumMark = qDTO.MinimumMark;
            vm.MaximumMark = qDTO.MaximumMark;

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<OnlinePassageQuestionsViewModel, PassageQuestionDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = Mapper<OnlinePassageQuestionsViewModel, PassageQuestionDTO>.Map(this);

            dto.PassageQuestionIID = this.PassageQuestionIID;
            dto.PassageQuestion = this.PassageQuestion;
            dto.ShortDescription = this.ShortDescription;
            dto.MinimumMark = this.MinimumMark;
            dto.MaximumMark = this.MaximumMark;

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<PassageQuestionDTO>(jsonString);
        }

    }
}