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
    public class TreatmentTypeViewModel : BaseMasterViewModel
    {
        public TreatmentTypeViewModel()
        {

        }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Treatment Type ID")]
        public long TreatmentTypeID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [DisplayName("Treatment Name")]
        public string TreatmentName { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.TreatmentGroup")]
        [DisplayName("Treatment Group ID")]
        public string TreatmentGroupID { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as TreatmentTypeDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<TreatmentTypeViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<TreatmentTypeDTO, TreatmentTypeViewModel>.CreateMap();
            var mapper =  Mapper<TreatmentTypeDTO, TreatmentTypeViewModel>.Map(dto as TreatmentTypeDTO);
            mapper.TreatmentGroupID = this.TreatmentGroupID;
            return mapper;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<TreatmentTypeViewModel, TreatmentTypeDTO>.CreateMap();
            var mapper = Mapper<TreatmentTypeViewModel, TreatmentTypeDTO>.Map(this);
            mapper.TreatmentGroupID = int.Parse(this.TreatmentGroupID);
            return mapper;
        }
    }
}
