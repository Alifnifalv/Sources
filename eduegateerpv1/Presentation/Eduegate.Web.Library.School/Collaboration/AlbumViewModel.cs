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
    public class AlbumViewModel : BaseMasterViewModel
    {
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("AlbumID")]
        public int  AlbumID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [DisplayName("AlbumName")]
        public string  AlbumName { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [DisplayName("Album Type")]
        [LookUp("LookUps.AlbumType")]
        public string AlbumTypeName { get; set; }
        public byte? AlbumTypeID { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as AlbumDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<AlbumViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<AlbumDTO, AlbumViewModel>.CreateMap();
            var vm = Mapper<AlbumDTO, AlbumViewModel>.Map(dto as AlbumDTO);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<AlbumViewModel, AlbumDTO>.CreateMap();
            var dto = Mapper<AlbumViewModel, AlbumDTO>.Map(this);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<AlbumDTO>(jsonString);
        }
    }
}

