using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Collaboration;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.School.Collaboration
{
    public class AlbumTypeViewModel : BaseMasterViewModel
    {
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("AlbumTypeID")]
        public byte  AlbumTypeID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [DisplayName("AlbumTypeName")]
        public string  AlbumTypeName { get; set; }
     
        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as AlbumTypeDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<AlbumTypeViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<AlbumTypeDTO, AlbumTypeViewModel>.CreateMap();
            var vm = Mapper<AlbumTypeDTO, AlbumTypeViewModel>.Map(dto as AlbumTypeDTO);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<AlbumTypeViewModel, AlbumTypeDTO>.CreateMap();
            var dto = Mapper<AlbumTypeViewModel, AlbumTypeDTO>.Map(this);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<AlbumTypeDTO>(jsonString);
        }
    }
}

