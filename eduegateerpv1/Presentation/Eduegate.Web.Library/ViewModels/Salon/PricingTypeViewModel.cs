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
    public class PricingTypeViewModel : BaseMasterViewModel
    {
        public PricingTypeViewModel(){

        }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Pricing Type ID")]
        public long PricingTypeID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(500, ErrorMessage = "Maximum Length should be within 500!")]
        [DisplayName("Description")]
        public string Description { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as PricingTypeDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<PricingTypeViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<PricingTypeDTO, PricingTypeViewModel>.CreateMap();
            return Mapper<PricingTypeDTO, PricingTypeViewModel>.Map(dto as PricingTypeDTO);
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<PricingTypeViewModel, PricingTypeDTO>.CreateMap();
            return Mapper<PricingTypeViewModel, PricingTypeDTO>.Map(this);
        }
    }
}
