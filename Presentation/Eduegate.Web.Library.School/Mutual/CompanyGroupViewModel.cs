using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Mutual;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.School.Mutual
{
    public class CompanyGroupViewModel : BaseMasterViewModel
    {
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Company group id")]
        public int  CompanyGroupID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(500, ErrorMessage = "Maximum Length should be within 500!")]
        [DisplayName("Description")]
        public string  GroupName { get; set; }
     
        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as CompanyGroupDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<CompanyGroupViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<CompanyGroupDTO, CompanyGroupViewModel>.CreateMap();
            var vm = Mapper<CompanyGroupDTO, CompanyGroupViewModel>.Map(dto as CompanyGroupDTO);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<CompanyGroupViewModel, CompanyGroupDTO>.CreateMap();
            var dto = Mapper<CompanyGroupViewModel, CompanyGroupDTO>.Map(this);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<CompanyGroupDTO>(jsonString);
        }
    }
}

