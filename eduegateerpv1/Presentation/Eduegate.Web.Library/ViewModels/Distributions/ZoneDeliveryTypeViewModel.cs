using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Distributions;
using Eduegate.Framework.Extensions;

namespace Eduegate.Web.Library.ViewModels
{
    public class ZoneDeliveryTypeViewModel : BaseMasterViewModel
    {
        public long ZoneDeliveryTypeMapIID { get; set; }
        public Nullable<int> DeliveryTypeID { get; set; }
        public Nullable<short> ZoneID { get; set; }
        public int CountryID { get; set; }
        public Nullable<bool> IsDeliveryAvailable { get; set; }
        public Nullable<decimal> CartTotalFrom { get; set; }
        public Nullable<decimal> CartTotalTo { get; set; }
        public Nullable<decimal> DeliveryCharge { get; set; }
        public Nullable<decimal> DeliveryChargePercentage { get; set; }

        public static ZoneDeliveryTypeViewModel FromDTO(ZoneDeliveryChargeDTO dto)
        {
            if (dto.IsNotNull())
            {
                ZoneDeliveryTypeViewModel zdtVM = new ZoneDeliveryTypeViewModel();
                var  CountryID = 1;

                zdtVM.ZoneDeliveryTypeMapIID = dto.ZoneDeliveryTypeMapIID;
                zdtVM.DeliveryTypeID = dto.DeliveryTypeID;
                zdtVM.ZoneID = dto.ZoneID;
                zdtVM.CountryID = CountryID;
                zdtVM.CartTotalFrom = dto.CartTotalFrom;
                zdtVM.CartTotalTo = dto.CartTotalTo;
                zdtVM.DeliveryCharge = dto.DeliveryCharge;
                zdtVM.DeliveryChargePercentage = dto.DeliveryChargePercentage;
                zdtVM.IsDeliveryAvailable = dto.IsDeliveryAvailable;
                zdtVM.CreatedBy = dto.CreatedBy;
                zdtVM.CreatedDate = dto.CreatedDate;
                zdtVM.UpdatedBy = dto.UpdatedBy;
                zdtVM.UpdatedDate = dto.UpdatedDate;
                zdtVM.TimeStamps = dto.TimeStamps;

                return zdtVM;
            }
            else
            {
                return new ZoneDeliveryTypeViewModel();
            }
        }

        public static ZoneDeliveryChargeDTO ToDTO(ZoneDeliveryTypeViewModel vm)
        {
            if (vm.IsNotNull())
            {
                ZoneDeliveryChargeDTO zdcDTO = new ZoneDeliveryChargeDTO();

                zdcDTO.ZoneDeliveryTypeMapIID = vm.ZoneDeliveryTypeMapIID;
                zdcDTO.DeliveryTypeID = vm.DeliveryTypeID;
                zdcDTO.ZoneID = vm.ZoneID;
                zdcDTO.CountryID = 1;
                zdcDTO.CartTotalFrom = vm.CartTotalFrom;
                zdcDTO.CartTotalTo = vm.CartTotalTo;
                zdcDTO.DeliveryCharge = vm.DeliveryCharge;
                zdcDTO.DeliveryChargePercentage = vm.DeliveryChargePercentage;
                zdcDTO.IsDeliveryAvailable = vm.IsDeliveryAvailable;
                zdcDTO.CreatedBy = vm.CreatedBy;
                zdcDTO.CreatedDate = vm.CreatedDate;
                zdcDTO.UpdatedBy = vm.UpdatedBy;
                zdcDTO.UpdatedDate = vm.UpdatedDate;
                zdcDTO.TimeStamps = vm.TimeStamps;

                return zdcDTO;
            }
            else
            {
                return new ZoneDeliveryChargeDTO();
            }
        }

    }
}
