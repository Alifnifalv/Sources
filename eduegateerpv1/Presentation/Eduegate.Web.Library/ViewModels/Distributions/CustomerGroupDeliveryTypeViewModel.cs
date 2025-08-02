using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Distributions;
using Eduegate.Framework.Extensions;

namespace Eduegate.Web.Library.ViewModels
{
    public class CustomerGroupDeliveryTypeViewModel : BaseMasterViewModel
    {
        public long CustomerGroupDeliveryTypeMapIID { get; set; }
        public Nullable<int> DeliveryTypeID { get; set; }
        public Nullable<long> CustomerGroupID { get; set; }
        public Nullable<bool> IsDeliveryAvailable { get; set; }
        public Nullable<decimal> CartTotalFrom { get; set; }
        public Nullable<decimal> CartTotalTo { get; set; }
        public Nullable<decimal> DeliveryCharge { get; set; }
        public Nullable<decimal> DeliveryChargePercentage { get; set; }


        public static CustomerGroupDeliveryTypeViewModel FromDTO(CustomerGroupDeliveryChargeDTO dto)
        {
            if (dto.IsNotNull())
            {
                CustomerGroupDeliveryTypeViewModel cgdtVM = new CustomerGroupDeliveryTypeViewModel();

                cgdtVM.CustomerGroupDeliveryTypeMapIID = dto.CustomerGroupDeliveryTypeMapIID;
                cgdtVM.DeliveryTypeID = dto.DeliveryTypeID;
                cgdtVM.CustomerGroupID = dto.CustomerGroupID;
                cgdtVM.CartTotalFrom = dto.CartTotalFrom;
                cgdtVM.CartTotalTo = dto.CartTotalTo;
                cgdtVM.DeliveryCharge = dto.DeliveryCharge;
                cgdtVM.DeliveryChargePercentage = dto.DeliveryChargePercentage;
                cgdtVM.IsDeliveryAvailable = dto.IsDeliveryAvailable;
                cgdtVM.CreatedBy = dto.CreatedBy;
                cgdtVM.CreatedDate = dto.CreatedDate;
                cgdtVM.UpdatedBy = dto.UpdatedBy;
                cgdtVM.UpdatedDate = dto.UpdatedDate;
                cgdtVM.TimeStamps = dto.TimeStamps;

                return cgdtVM;
            }
            else
            {
                return new CustomerGroupDeliveryTypeViewModel();
            }
        }

        public static CustomerGroupDeliveryChargeDTO ToDTO(CustomerGroupDeliveryTypeViewModel vm)
        {
            if (vm.IsNotNull())
            {
                CustomerGroupDeliveryChargeDTO cgdtDTO = new CustomerGroupDeliveryChargeDTO();

                cgdtDTO.CustomerGroupDeliveryTypeMapIID = vm.CustomerGroupDeliveryTypeMapIID;
                cgdtDTO.DeliveryTypeID = vm.DeliveryTypeID;
                cgdtDTO.CustomerGroupID = vm.CustomerGroupID;
                cgdtDTO.CartTotalFrom = vm.CartTotalFrom;
                cgdtDTO.CartTotalTo = vm.CartTotalTo;
                cgdtDTO.DeliveryCharge = vm.DeliveryCharge;
                cgdtDTO.DeliveryChargePercentage = vm.DeliveryChargePercentage;
                cgdtDTO.IsDeliveryAvailable = vm.IsDeliveryAvailable;
                cgdtDTO.CreatedBy = vm.CreatedBy;
                cgdtDTO.CreatedDate = vm.CreatedDate;
                cgdtDTO.UpdatedBy = vm.UpdatedBy;
                cgdtDTO.UpdatedDate = vm.UpdatedDate;
                cgdtDTO.TimeStamps = vm.TimeStamps;

                return cgdtDTO;
            }
            else
            {
                return new CustomerGroupDeliveryChargeDTO();
            }
        }

    }
}
