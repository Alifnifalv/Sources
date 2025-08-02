using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Salon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.Salon
{
    public class ExtraTimeTypeViewModel : BaseMasterViewModel
    {
        public ExtraTimeTypeViewModel()
        {

        }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Extra time type id")]
        public long ExtraTimeTypeID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(500, ErrorMessage = "Maximum Length should be within 500!")]
        [DisplayName("Description")]
        public string Description { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as ExtraTimeTypeDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<ExtraTimeTypeViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<ExtraTimeTypeDTO, ExtraTimeTypeViewModel>.CreateMap();
            return Mapper<ExtraTimeTypeDTO, ExtraTimeTypeViewModel>.Map(dto as ExtraTimeTypeDTO);
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<ExtraTimeTypeViewModel, ExtraTimeTypeDTO>.CreateMap();
            return Mapper<ExtraTimeTypeViewModel, ExtraTimeTypeDTO>.Map(this);
        }
    }
}
