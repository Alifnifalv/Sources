using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.FrontOffices;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.School.FrontOffices
{
    public class EnquirySourceViewModel : BaseMasterViewModel
    {
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        [DisplayName("EnquirySourceID")]
        public byte  EnquirySourceID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [DisplayName("SourceName")]
        public string  SourceName { get; set; }
     
        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as EnquirySourceDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<EnquirySourceViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<EnquirySourceDTO, EnquirySourceViewModel>.CreateMap();
            var vm = Mapper<EnquirySourceDTO, EnquirySourceViewModel>.Map(dto as EnquirySourceDTO);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<EnquirySourceViewModel, EnquirySourceDTO>.CreateMap();
            var dto = Mapper<EnquirySourceViewModel, EnquirySourceDTO>.Map(this);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<EnquirySourceDTO>(jsonString);
        }
    }
}

