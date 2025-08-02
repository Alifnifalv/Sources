using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.Students;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Students
{
    public class CastViewModel : BaseMasterViewModel

    {
       /// [Required]
       /// [ControlType(Eduegate.Framework.Enums.ControlTypes.Label)]
       /// [DisplayName("CastID")]
        public byte CastID { get; set; }
               
        [Required]
        [ControlType(Eduegate.Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [CustomDisplay("CasteDescription")]
        public string CastDescription { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as CastDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<CastViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<CastDTO, CastViewModel>.CreateMap();
            var vm = Mapper<CastDTO, CastViewModel>.Map(dto as CastDTO);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<CastViewModel, CastDTO>.CreateMap();
            var dto = Mapper<CastViewModel, CastDTO>.Map(this);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<CastDTO>(jsonString);
        }

    }
}