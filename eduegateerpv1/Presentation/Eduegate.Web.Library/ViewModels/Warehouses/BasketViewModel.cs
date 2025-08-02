using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Services.Contracts.Warehouses;
using Eduegate.Framework.Extensions;
namespace Eduegate.Web.Library.ViewModels.Warehouses
{
    public class BasketViewModel : BaseMasterViewModel
    {
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Basket ID")]
        public long BasketID { get; set; }
        
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Basket Code")]
        [MaxLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        public string BasketCode { get; set; }
        
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Description")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string Description { get; set; }
        
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Bar Code")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string BarCode { get; set; }


        public static BasketViewModel FromDTOToVM(BasketDTO dto) //Translating job entry data from DTO to viewmodel
        {
            if (dto.IsNotNull())
            {
                return new BasketViewModel()
                {
                    BasketID = dto.BasketID,
                    BarCode = dto.BarCode,
                    BasketCode= dto.BasketCode,
                    Description = dto.Description,
                    CreatedBy = dto.CreatedBy,
                    CreatedDate = dto.CreatedDate,
                    TimeStamps = dto.TimeStamps,
                    UpdatedBy = dto.UpdatedBy,
                    UpdatedDate = dto.UpdatedDate
                };
            }

            else
                return new BasketViewModel();
        }

        public static BasketDTO FromVMToDTO(BasketViewModel vm) //Translating job entry data from DTO to viewmodel
        {
            if (vm.IsNotNull())
            {
                return new BasketDTO()
                {
                    BasketID = vm.BasketID,
                    BarCode = vm.BarCode,
                    BasketCode = vm.BasketCode,
                    Description = vm.Description,
                    CreatedBy = vm.CreatedBy,
                    CreatedDate = vm.CreatedDate,
                    TimeStamps = vm.TimeStamps,
                    UpdatedBy = vm.UpdatedBy,
                    UpdatedDate = vm.UpdatedDate
                };
            }

            else
                return new BasketDTO();
        }
    }
}
