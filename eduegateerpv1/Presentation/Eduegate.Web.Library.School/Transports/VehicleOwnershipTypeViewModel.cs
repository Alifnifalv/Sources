using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Transports;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Transports
{
    public class VehicleOwnershipTypeViewModel : BaseMasterViewModel
    {
        public short  VehicleOwnershipTypeID { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("OwnershipTypeName")]
        public string  OwnershipTypeName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Description")]
        public string  Description { get; set; }       
     
        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as VehicleOwnershipTypeDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<VehicleOwnershipTypeViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<VehicleOwnershipTypeDTO, VehicleOwnershipTypeViewModel>.CreateMap();
            var vm = Mapper<VehicleOwnershipTypeDTO, VehicleOwnershipTypeViewModel>.Map(dto as VehicleOwnershipTypeDTO);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<VehicleOwnershipTypeViewModel, VehicleOwnershipTypeDTO>.CreateMap();
            var dto = Mapper<VehicleOwnershipTypeViewModel, VehicleOwnershipTypeDTO>.Map(this);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<VehicleOwnershipTypeDTO>(jsonString);
        }
    }
}

