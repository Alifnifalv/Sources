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
    public class HolidaysViewModel : BaseMasterViewModel
    {
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("HolidayIID")]
        public long  HolidayIID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("HolidayListID")]
        public long?  HolidayListID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("HolidayDate")]
        public System.DateTime?  HolidayDate { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Description")]
        public string  Description { get; set; }
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
     
        public static List<HolidaysViewModel> FromDTO(List<HolidaysDTO> dtos)
        {
            var vms = new List<HolidaysViewModel>();

            foreach (var dto in dtos)
            {
                vms.Add(FromDTO(dto));
            }

            return vms;
        }

        public static HolidaysViewModel FromDTO(HolidaysDTO dto)
        {
            Mapper<HolidaysDTO, HolidaysViewModel>.CreateMap();
            var mapper = Mapper<HolidaysDTO, HolidaysViewModel>.Map(dto);           
            return mapper;
        }

        public static List<HolidaysDTO> ToDTO(List<HolidaysViewModel> vms)
        {
            var dtos = new List<HolidaysDTO>();

            foreach (var vm in vms)
            {
                dtos.Add(ToDTO(vm));
            }

            return dtos;
        }

        public static HolidaysDTO ToDTO(HolidaysViewModel vm)
        {
            Mapper<HolidaysViewModel, HolidaysDTO>.CreateMap();
            var mapper = Mapper<HolidaysViewModel, HolidaysDTO>.Map(vm);         
            return mapper;
        }
    }
}

