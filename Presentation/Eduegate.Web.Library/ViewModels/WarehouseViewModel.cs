using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Mutual;

namespace Eduegate.Web.Library.ViewModels
{
    public class WarehouseViewModel : BaseMasterViewModel
    {
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        [DisplayName("Warehouse ID")]
        public long WareHouseID { get; set; }
        [ControlType(Framework.Enums.ControlTypes.TextBox)]

        [Required]
        [DisplayName("Warehouse Name")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string WarehouseName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.WarehouseStatus")]
        [DisplayName("Status")]
        public string WarehouseStatus { get; set; }
        public byte StatusID { get; set; } 
        public Nullable<int> CompanyID { get; set; }

        public static List<WarehouseViewModel> FromDTO(List<WarehouseDTO> dtos)
        {
            var vms = new List<WarehouseViewModel>();

            foreach (var dto in dtos)
            {
                vms.Add(FromDTO(dto));
            }

            return vms;
        }

        public static WarehouseViewModel FromDTO(WarehouseDTO dto)
        {
            Mapper<WarehouseDTO, WarehouseViewModel>.CreateMap();
            var mapper = Mapper<WarehouseDTO, WarehouseViewModel>.Map(dto);
            mapper.WarehouseStatus = dto.StatusID.ToString();
            mapper.CompanyID = dto.CompanyID;
            return mapper;
        }

        public static List<WarehouseDTO> ToDTO(List<WarehouseViewModel> vms)
        {
            var dtos = new List<WarehouseDTO>();

            foreach (var vm in vms)
            {
                dtos.Add(ToDTO(vm));
            }

            return dtos;
        }

        public static WarehouseDTO ToDTO(WarehouseViewModel vm)
        {
            Mapper<WarehouseViewModel, WarehouseDTO>.CreateMap();
            var mapper = Mapper<WarehouseViewModel, WarehouseDTO>.Map(vm);
            mapper.StatusID =Convert.ToByte(vm.WarehouseStatus);
            mapper.CompanyID = vm.CompanyID;
            return mapper;
        }
    }
}
