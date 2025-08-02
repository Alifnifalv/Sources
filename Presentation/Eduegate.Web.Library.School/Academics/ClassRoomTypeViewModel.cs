using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.Academics;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Academics
{
    public class ClassRoomTypeViewModel : BaseMasterViewModel
    {
        public ClassRoomTypeViewModel()
        {
            IsShared = false;
        }

        public byte  ClassRoomTypeID { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("TypeDescription")]
        public string  TypeDescription { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsShared")]
        public bool?  IsShared { get; set; }
     
        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as ClassRoomTypeDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<ClassRoomTypeViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<ClassRoomTypeDTO, ClassRoomTypeViewModel>.CreateMap();
            var vm = Mapper<ClassRoomTypeDTO, ClassRoomTypeViewModel>.Map(dto as ClassRoomTypeDTO);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<ClassRoomTypeViewModel, ClassRoomTypeDTO>.CreateMap();
            var dto = Mapper<ClassRoomTypeViewModel, ClassRoomTypeDTO>.Map(this);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<ClassRoomTypeDTO>(jsonString);
        }
    }
}

