using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Setting.Settings;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Web.Library.ViewModels;

namespace Eduegate.Web.Library.Setting.Settings
{
    public class SequenceViewModel : BaseMasterViewModel
    {
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBoxEditable)]
        [DisplayName("SquenceID")]
        public int SquenceID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("SequenceType")]
        public string SequenceType { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Prefix")]
        public string Prefix { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Format")]
        public string Format { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("LastSequence")]
        public long? LastSequence { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("IsAuto")]
        public bool? IsAuto { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("ZeroPadding")]
        public int? ZeroPadding { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as SequenceDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<SequenceViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<SequenceDTO, SequenceViewModel>.CreateMap();
            var vm = Mapper<SequenceDTO, SequenceViewModel>.Map(dto as SequenceDTO);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<SequenceViewModel, SequenceDTO>.CreateMap();
            var dto = Mapper<SequenceViewModel, SequenceDTO>.Map(this);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<SequenceDTO>(jsonString);
        }

    }
}