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
    public class HostelRoomViewModel : BaseMasterViewModel
    {
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Id")]
        public long  HostelRoomIID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(10, ErrorMessage = "Maximum Length should be within 10!")]
        [DisplayName("Room Number")]
        public string  RoomNumber { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [DisplayName("Hostel")]
        [LookUp("LookUps.Hostel")]
        public string Hostel { get; set; }

        public int?  HostelID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [DisplayName("Room Type")]
        [LookUp("LookUps.RoomType")]
        public string  RoomType { get; set; }
        public int? RoomTypeID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(5, ErrorMessage = "Maximum Length should be within 5!")]
        [DisplayName("Number of bed")]
        public int?  NumberOfBed { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(7, ErrorMessage = "Maximum Length should be within 7!")]
        [DisplayName("Cost per bed")]
        public decimal?  CostPerBed { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(500, ErrorMessage = "Maximum Length should be within 500!")]
        [DisplayName("Description")]
        public string  Description { get; set; }       
     
        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as HostelRoomDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<HostelRoomViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<HostelRoomDTO, HostelRoomViewModel>.CreateMap();
            var hostelDto = dto as HostelRoomDTO;
            var vm = Mapper<HostelRoomDTO, HostelRoomViewModel>.Map(hostelDto);
            vm.RoomType = hostelDto.RoomTypeID.ToString();
            vm.Hostel = hostelDto.HostelID.ToString();
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<HostelRoomViewModel, HostelRoomDTO>.CreateMap();
            var dto = Mapper<HostelRoomViewModel, HostelRoomDTO>.Map(this);
            dto.HostelID = Convert.ToInt32(this.Hostel);
            dto.RoomTypeID = Convert.ToInt32(this.RoomType);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<HostelRoomDTO>(jsonString);
        }
    }
}

