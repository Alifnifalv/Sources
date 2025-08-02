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
    public class TreatmentGroupViewModel : BaseMasterViewModel
    {
        public TreatmentGroupViewModel()
        {

        }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Treatment Group ID")]
        public long TreatmentGroupID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(500, ErrorMessage = "Maximum Length should be within 500!")]
        [DisplayName("Description")]
        public string Description { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as TreatmentGroupDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<TreatmentGroupViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<TreatmentGroupDTO, TreatmentGroupViewModel>.CreateMap();           
            return Mapper<TreatmentGroupDTO, TreatmentGroupViewModel>.Map(dto as TreatmentGroupDTO);            
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<TreatmentGroupViewModel, TreatmentGroupDTO>.CreateMap();
            return Mapper<TreatmentGroupViewModel, TreatmentGroupDTO>.Map(this);
        }
    }
}
