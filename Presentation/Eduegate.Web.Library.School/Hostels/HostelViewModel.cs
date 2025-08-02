using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Hostel;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.School.Hostel
{
    public class HostelViewModel : BaseMasterViewModel
    {
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("HostelID")]
        public int  HostelID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [DisplayName("Name")]
        public string HostelName { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [DisplayName("Hostel type")]
        [LookUp("LookUps.HostelType")]
        public string  HostelType { get; set; }

        public byte? HostelTypeID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        [DisplayName("Address")]
        public string  Address { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [DisplayName("InTake")]
        public int?  InTake { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        [DisplayName("Description")]
        public string  Description { get; set; }
     
        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as HostelDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<HostelViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<HostelDTO, HostelViewModel>.CreateMap();
            var vm = Mapper<HostelDTO, HostelViewModel>.Map(dto as HostelDTO);
            vm.HostelType = (dto as HostelDTO).HostelTypeID.ToString();
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<HostelViewModel, HostelDTO>.CreateMap();
            var dto = Mapper<HostelViewModel, HostelDTO>.Map(this);
            dto.HostelTypeID = Convert.ToByte(this.HostelType);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<HostelDTO>(jsonString);
        }
    }
}

