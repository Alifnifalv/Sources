using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Distributions;
using Eduegate.Framework.Extensions;

namespace Eduegate.Web.Library.ViewModels
{
    public class ProductDeliveryTypeViewModel : BaseMasterViewModel
    {
        public long ProductDeliveryTypeMapIID { get; set; }
        public Nullable<long> ProductID { get; set; }
        public Nullable<long> ProductSKUMapID { get; set; }
        public Nullable<int> DeliveryTypeID { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> DeliveryCharge { get; set; }
        public Nullable<decimal> DeliveryChargePercentage { get; set; }
        public Nullable<bool> IsDeliveryAvailable { get; set; }
        public Nullable<decimal> CartTotalFrom { get; set; }
        public Nullable<decimal> CartTotalTo { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<byte> DeliveryDays { get; set; }
        public Nullable<long> BranchID { get; set; }
        public string BranchName { get; set; } 

        public static ProductDeliveryTypeViewModel FromDTO(ProductDeliveryTypeDTO dto)
        {
            if (dto.IsNotNull())
            {
                ProductDeliveryTypeViewModel pdtVM = new ProductDeliveryTypeViewModel();

                pdtVM.ProductDeliveryTypeMapIID = dto.ProductDeliveryTypeMapIID;
                pdtVM.ProductID = dto.ProductID;
                pdtVM.ProductSKUMapID = dto.ProductSKUMapID;
                pdtVM.DeliveryTypeID = dto.DeliveryTypeID;
                pdtVM.DeliveryCharge = dto.DeliveryCharge;
                pdtVM.DeliveryChargePercentage = dto.DeliveryChargePercentage;
                pdtVM.IsDeliveryAvailable = dto.IsDeliveryAvailable;
                pdtVM.Description = dto.Description;
                pdtVM.CartTotalFrom = dto.CartTotalFrom;
                pdtVM.CartTotalTo = dto.CartTotalTo;
                pdtVM.CreatedBy = dto.CreatedBy;
                pdtVM.CreatedDate = dto.CreatedDate;
                pdtVM.UpdatedBy = dto.UpdatedBy;
                pdtVM.UpdatedDate = dto.UpdatedDate;
                pdtVM.TimeStamps = dto.TimeStamps;
                pdtVM.CompanyID = dto.CompanyID;
                pdtVM.DeliveryDays = dto.DeliveryDays;
                pdtVM.BranchID = dto.BranchID;

                return pdtVM;
            }
            else
            {
                return new ProductDeliveryTypeViewModel();
            }
        }

        public static ProductDeliveryTypeDTO ToDTO(ProductDeliveryTypeViewModel vm)
        {
            if (vm.IsNotNull())
            {
                ProductDeliveryTypeDTO pdtDTO = new ProductDeliveryTypeDTO();

                pdtDTO.ProductDeliveryTypeMapIID = vm.ProductDeliveryTypeMapIID;
                pdtDTO.ProductID = vm.ProductID;
                pdtDTO.ProductSKUMapID = vm.ProductSKUMapID;
                pdtDTO.DeliveryTypeID = vm.DeliveryTypeID;
                pdtDTO.DeliveryCharge = vm.DeliveryCharge;
                pdtDTO.Description = vm.Description;
                pdtDTO.DeliveryChargePercentage = vm.DeliveryChargePercentage;
                pdtDTO.IsDeliveryAvailable = vm.IsDeliveryAvailable;
                pdtDTO.CartTotalFrom = vm.CartTotalFrom;
                pdtDTO.CartTotalTo = vm.CartTotalTo;
                pdtDTO.CreatedBy = vm.CreatedBy;
                pdtDTO.CreatedDate = vm.CreatedDate;
                pdtDTO.UpdatedBy = vm.UpdatedBy;
                pdtDTO.UpdatedDate = vm.UpdatedDate;
                pdtDTO.TimeStamps = vm.TimeStamps;
                pdtDTO.CompanyID = vm.CompanyID > 0 ? vm.CompanyID : null;
                pdtDTO.DeliveryDays = vm.DeliveryDays;
                pdtDTO.BranchID = vm.BranchID;

                return pdtDTO;
            }
            else
            {
                return new ProductDeliveryTypeDTO();
            }
        }

    }
}
