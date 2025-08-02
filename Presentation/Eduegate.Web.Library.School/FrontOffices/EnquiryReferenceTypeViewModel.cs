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
    public class EnquiryReferenceTypeViewModel : BaseMasterViewModel
    {
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        [DisplayName("EnquiryReferenceTypeID")]
        public byte  EnquiryReferenceTypeID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [DisplayName("ReferenceName")]
        public string  ReferenceName { get; set; }
     
        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as EnquiryReferenceTypeDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<EnquiryReferenceTypeViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<EnquiryReferenceTypeDTO, EnquiryReferenceTypeViewModel>.CreateMap();
            var vm = Mapper<EnquiryReferenceTypeDTO, EnquiryReferenceTypeViewModel>.Map(dto as EnquiryReferenceTypeDTO);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<EnquiryReferenceTypeViewModel, EnquiryReferenceTypeDTO>.CreateMap();
            var dto = Mapper<EnquiryReferenceTypeViewModel, EnquiryReferenceTypeDTO>.Map(this);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<EnquiryReferenceTypeDTO>(jsonString);
        }
    }
}

