using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Students;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Academics
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "Sections", "CRUDModel.ViewModel")]
    [DisplayName("Details")]
    public class SectionViewModel : BaseMasterViewModel
    {
       /// [Required]
       /// [ControlType(Framework.Enums.ControlTypes.Label)]
       /// [DisplayName("Id")]
        public int  SectionID { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Description")]
        public string  SectionName { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as SectionDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<SectionViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<SectionDTO, SectionViewModel>.CreateMap();
            return Mapper<SectionDTO, SectionViewModel>.Map(dto as SectionDTO);
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<SectionViewModel, SectionDTO>.CreateMap();
            return Mapper<SectionViewModel, SectionDTO>.Map(this);
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<SectionDTO>(jsonString);
        }
    }
}

