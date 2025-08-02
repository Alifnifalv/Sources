using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.HR.Leaves;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Eduegate.Web.Library.HR.Leaves
{
    public class HolidayListViewModel : BaseMasterViewModel
    {
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("HolidayIID")]
        public long HolidayIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.HolidayList")]
        [DisplayName("HolidayList")]
        public string HolidayList { get; set; }
        public long? HolidayListID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DateTimePicker)]
        [DisplayName("HolidayDate")]
        public System.DateTime? HolidayDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(500, ErrorMessage = "Maximum Length should be within 500!")]
        [DisplayName("Description")]
        public string Description { get; set; }
        


        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as HolidayDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<HolidayListViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<HolidayDTO, HolidayListViewModel>.CreateMap();
            var hlDtO = dto as HolidayDTO;
            var vm = Mapper<HolidayDTO, HolidayListViewModel>.Map(dto as HolidayDTO);
            vm.HolidayList = hlDtO.HolidayListID.ToString();
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<HolidayListViewModel, HolidayDTO>.CreateMap();
            var dto = Mapper<HolidayListViewModel, HolidayDTO>.Map(this);
            dto.HolidayListID = string.IsNullOrEmpty(this.HolidayList) ? (long?)null : long.Parse(this.HolidayList);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<HolidayDTO>(jsonString);
        }

    }
}
