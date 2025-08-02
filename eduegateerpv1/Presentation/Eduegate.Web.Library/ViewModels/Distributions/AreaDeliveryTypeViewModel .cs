using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Distributions;
using Eduegate.Framework.Extensions;

namespace Eduegate.Web.Library.ViewModels
{
    public class AreaDeliveryTypeViewModel : BaseMasterViewModel
    {
        public long AreaDeliveryTypeMapIID { get; set; }
        public Nullable<int> DeliveryTypeID { get; set; }
        public Nullable<int> AreaID { get; set; }
        public Nullable<bool> IsDeliveryAvailable { get; set; }
        public Nullable<decimal> CartTotalFrom { get; set; }
        public Nullable<decimal> CartTotalTo { get; set; }
        public Nullable<decimal> DeliveryCharge { get; set; }
        public Nullable<decimal> DeliveryChargePercentage { get; set; }


        public static AreaDeliveryTypeViewModel FromDTO(AreaDeliveryChargeDTO dto)
        {
            if (dto.IsNotNull())
            {
                AreaDeliveryTypeViewModel adtVM = new AreaDeliveryTypeViewModel();
                adtVM.AreaDeliveryTypeMapIID = dto.AreaDeliveryTypeMapIID;
                adtVM.DeliveryTypeID = dto.DeliveryTypeID;
                adtVM.AreaID = dto.AreaID;
                adtVM.CartTotalFrom = dto.CartTotalFrom;
                adtVM.CartTotalTo = dto.CartTotalTo;
                adtVM.DeliveryCharge = dto.DeliveryCharge;
                adtVM.DeliveryChargePercentage = dto.DeliveryChargePercentage;
                adtVM.IsDeliveryAvailable = dto.IsDeliveryAvailable;
                adtVM.CreatedBy = dto.CreatedBy;
                adtVM.CreatedDate = dto.CreatedDate;
                adtVM.UpdatedBy = dto.UpdatedBy;
                adtVM.UpdatedDate = dto.UpdatedDate;
                adtVM.TimeStamps = dto.TimeStamps;

                return adtVM;
            }
            else
            {
                return new AreaDeliveryTypeViewModel();
            }
        }

        public static AreaDeliveryChargeDTO ToDTO(AreaDeliveryTypeViewModel vm)
        {
            if (vm.IsNotNull())
            {
                AreaDeliveryChargeDTO adcDTO = new AreaDeliveryChargeDTO();

                adcDTO.AreaDeliveryTypeMapIID = vm.AreaDeliveryTypeMapIID;
                adcDTO.DeliveryTypeID = vm.DeliveryTypeID;
                adcDTO.AreaID = vm.AreaID;
                //adcDTO.CountryID = 1;
                adcDTO.CartTotalFrom = vm.CartTotalFrom;
                adcDTO.CartTotalTo = vm.CartTotalTo;
                adcDTO.DeliveryCharge = vm.DeliveryCharge;
                adcDTO.DeliveryChargePercentage = vm.DeliveryChargePercentage;
                adcDTO.IsDeliveryAvailable = vm.IsDeliveryAvailable;
                adcDTO.CreatedBy = vm.CreatedBy;
                adcDTO.CreatedDate = vm.CreatedDate;
                adcDTO.UpdatedBy = vm.UpdatedBy;
                adcDTO.UpdatedDate = vm.UpdatedDate;
                adcDTO.TimeStamps = vm.TimeStamps;

                return adcDTO;
            }
            else
            {
                return new AreaDeliveryChargeDTO();
            }
        }

    }
}
