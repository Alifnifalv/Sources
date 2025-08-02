using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.FrontOffices;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.School.FrontOffices
{
    public class VisitorBookViewModel : BaseMasterViewModel
    {
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        [DisplayName("VisitorBookIID")]
        public long  VisitorBookIID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        [DisplayName("VisitingPurposeID")]
        public byte?  VisitingPurposeID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [DisplayName("VisitorName")]
        public string  VisitorName { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(13, ErrorMessage = "Maximum Length should be within 13!")]
        [DisplayName("PhoneNumber")]
        public string  PhoneNumber { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        [DisplayName("IDCard")]
        public string  IDCard { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(5, ErrorMessage = "Maximum Length should be within 5!")]
        [DisplayName("NumberOfPerson")]
        public string  NumberOfPerson { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(10, ErrorMessage = "Maximum Length should be within 10!")]
        [DisplayName("Date")]
        public System.DateTime?  Date { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(5, ErrorMessage = "Maximum Length should be within 5!")]
        [DisplayName("InTime")]
        public System.DateTime?  InTime { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(5, ErrorMessage = "Maximum Length should be within 5!")]
        [DisplayName("OutTime")]
        public System.DateTime?  OutTime { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(250, ErrorMessage = "Maximum Length should be within 250!")]
        [DisplayName("Note")]
        public string  Note { get; set; }

     
        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as VisitorBookDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<VisitorBookViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<VisitorBookDTO, VisitorBookViewModel>.CreateMap();
            var vm = Mapper<VisitorBookDTO, VisitorBookViewModel>.Map(dto as VisitorBookDTO);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<VisitorBookViewModel, VisitorBookDTO>.CreateMap();
            var dto = Mapper<VisitorBookViewModel, VisitorBookDTO>.Map(this);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<VisitorBookDTO>(jsonString);
        }
    }
}

