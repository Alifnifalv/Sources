using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Academics;
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
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "SubjectType", "CRUDModel.ViewModel")]
    [DisplayName("Details")]
    public class SubjectTypeViewModel : BaseMasterViewModel
    {
       /// [Required]
       /// [ControlType(Framework.Enums.ControlTypes.Label)]
        /// [DisplayName("Id")]
        public byte  SubjectTypeID { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Description")]
        public string  TypeName { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as SubjectTypeDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<SubjectTypeViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<SubjectTypeDTO, SubjectTypeViewModel>.CreateMap();
            return Mapper<SubjectTypeDTO, SubjectTypeViewModel>.Map(dto as SubjectTypeDTO);
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<SubjectTypeViewModel, SubjectTypeDTO>.CreateMap();
            return Mapper<SubjectTypeViewModel, SubjectTypeDTO>.Map(this);
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<SubjectTypeDTO>(jsonString);
        }
    }
}

