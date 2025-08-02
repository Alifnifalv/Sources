using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.Inventory;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.School.Inventory
{
    public class RackViewModel : BaseMasterViewModel
    {
       /// [Required]
        ///[ControlType(Eduegate.Framework.Enums.ControlTypes.Label)]
      ///  [DisplayName("Rack ID")]
        public long RackID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public int NewLine1 { get; set; }

        [Required]
        [ControlType(Eduegate.Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [CustomDisplay("RackName")]
        public string RackName { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as RackDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<RackViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<RackDTO, RackViewModel>.CreateMap();
            var vm = Mapper<RackDTO, RackViewModel>.Map(dto as RackDTO);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<RackViewModel, RackDTO>.CreateMap();
            var dto = Mapper<RackViewModel, RackDTO>.Map(this);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<RackDTO>(jsonString);
        }
    }
}
