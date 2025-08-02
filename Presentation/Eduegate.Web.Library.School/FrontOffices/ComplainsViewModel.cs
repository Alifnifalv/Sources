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
    public class ComplainsViewModel : BaseMasterViewModel
    {
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        [DisplayName("ComplainIID")]
        public long  ComplainIID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        [DisplayName("ComplainTypeID")]
        public byte?  ComplainTypeID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        [DisplayName("SourceID")]
        public byte?  SourceID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [DisplayName("ComplainType")]
        public string  ComplainType { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(13, ErrorMessage = "Maximum Length should be within 13!")]
        [DisplayName("Phone")]
        public string  Phone { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(10, ErrorMessage = "Maximum Length should be within 10!")]
        [DisplayName("Date")]
        public System.DateTime?  Date { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(500, ErrorMessage = "Maximum Length should be within 500!")]
        [DisplayName("Description")]
        public string  Description { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [DisplayName("ActionTaken")]
        public string  ActionTaken { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [DisplayName("Assigned")]
        public string  Assigned { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(250, ErrorMessage = "Maximum Length should be within 250!")]
        [DisplayName("Note")]
        public string  Note { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("CreatedBy")]
        public int?  CreatedBy { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("UpdatedBy")]
        public int?  UpdatedBy { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("CreatedDate")]
        public System.DateTime?  CreatedDate { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("UpdatedDate")]
        public System.DateTime?  UpdatedDate { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("TimeStamps")]
        public byte[]  TimeStamps { get; set; }
     
        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as ComplainsDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<ComplainsViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<ComplainsDTO, ComplainsViewModel>.CreateMap();
            var vm = Mapper<ComplainsDTO, ComplainsViewModel>.Map(dto as ComplainsDTO);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<ComplainsViewModel, ComplainsDTO>.CreateMap();
            var dto = Mapper<ComplainsViewModel, ComplainsDTO>.Map(this);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<ComplainsDTO>(jsonString);
        }
    }
}

