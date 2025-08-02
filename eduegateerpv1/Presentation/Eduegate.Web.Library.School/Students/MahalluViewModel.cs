using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.School.Students;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.School.Students
{
    public class MahalluViewModel : BaseMasterViewModel
    {
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("MahalluIID")]
        public long  MahalluIID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("MahalluName")]
        public string  MahalluName { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Place")]
        public string  Place { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Post")]
        public string  Post { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Pincode")]
        public string  Pincode { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("District")]
        public string  District { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("State")]
        public string  State { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Phone")]
        public string  Phone { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Email")]
        public string  Email { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Fax")]
        public string  Fax { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("WaqafNumber")]
        public string  WaqafNumber { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("EstablishedOn")]
        public System.DateTime?  EstablishedOn { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("MahalluArea")]
        public string  MahalluArea { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Description")]
        public string  Description { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Logo")]
        public string  Logo { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("CurrentDate")]
        public System.DateTime?  CurrentDate { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Extra1")]
        public string  Extra1 { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("ExtraDate")]
        public System.DateTime?  ExtraDate { get; set; }
     
        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as MahalluDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<MahalluViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<MahalluDTO, MahalluViewModel>.CreateMap();
            var vm = Mapper<MahalluDTO, MahalluViewModel>.Map(dto as MahalluDTO);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<MahalluViewModel, MahalluDTO>.CreateMap();
            var dto = Mapper<MahalluViewModel, MahalluDTO>.Map(this);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<MahalluDTO>(jsonString);
        }
    }
}

