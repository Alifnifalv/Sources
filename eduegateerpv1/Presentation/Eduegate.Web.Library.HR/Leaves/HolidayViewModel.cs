using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.HR.Leaves;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.HR.Payroll
{
    public class HolidayViewModel : BaseMasterViewModel
    {
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        [DisplayName("HolidayIID")]
        public long  HolidayIID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        [DisplayName("HolidayListID")]
        public long?  HolidayListID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(10, ErrorMessage = "Maximum Length should be within 10!")]
        [DisplayName("HolidayDate")]
        public System.DateTime?  HolidayDate { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(500, ErrorMessage = "Maximum Length should be within 500!")]
        [DisplayName("Description")]
        public string  Description { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("CreatedBy")]
        public int? CreatedBy { get; set; }
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
     
        public static List<HolidayViewModel> FromDTO(List<HolidayDTO> dtos)
        {
            var vms = new List<HolidayViewModel>();

            foreach (var dto in dtos)
            {
                vms.Add(FromDTO(dto));
            }

            return vms;
        }

        public static HolidayViewModel FromDTO(HolidayDTO dto)
        {
            Mapper<HolidayDTO, HolidayViewModel>.CreateMap();
            var mapper = Mapper<HolidayDTO, HolidayViewModel>.Map(dto);           
            return mapper;
        }

        public static List<HolidayDTO> ToDTO(List<HolidayViewModel> vms)
        {
            var dtos = new List<HolidayDTO>();

            foreach (var vm in vms)
            {
                dtos.Add(ToDTO(vm));
            }

            return dtos;
        }

        public static HolidayDTO ToDTO(HolidayViewModel vm)
        {
            Mapper<HolidayViewModel, HolidayDTO>.CreateMap();
            var mapper = Mapper<HolidayViewModel, HolidayDTO>.Map(vm);         
            return mapper;
        }
    }
}

