using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Distributions;

namespace Eduegate.Web.Library.ViewModels
{
    public class ProductSKUBranchSettingViewModel : BaseMasterViewModel
    {
        public ProductSKUBranchSettingViewModel()
        {
            ProductSKUTypeDeliveryTypes = new List<ProductDeliveryTypeViewModel>();
        }
        public Nullable<int> DeliveryTypeID { get; set; }
        public Nullable<long> ProductSKUMapID { get; set; }
        public Nullable<long> BranchID { get; set; } 
        public string BranchName { get; set; }

        public List<ProductDeliveryTypeViewModel> ProductSKUTypeDeliveryTypes { get; set; }

        public static ProductSKUBranchSettingViewModel FromDTO(SKUBranchDeliveryTypeDTO dto)
        {
            if (dto.IsNotNull())
            {
                ProductSKUBranchSettingViewModel psbVM = new ProductSKUBranchSettingViewModel();
                psbVM.ProductSKUTypeDeliveryTypes = new List<ProductDeliveryTypeViewModel>();
                ProductDeliveryTypeViewModel pdtVM = null;

                psbVM.BranchID = dto.BranchID.IsNotNull() ? dto.BranchID : null;
                psbVM.BranchName = dto.BranchName.IsNotNull () ? dto.BranchName : string.Empty;

                if (dto.DeliveryDetails.IsNotNull() && dto.DeliveryDetails.Count > 0)
                {
                    foreach (var pdt in dto.DeliveryDetails)
                    {
                        pdtVM = new ProductDeliveryTypeViewModel();

                        pdtVM.ProductSKUMapID = pdt.ProductSKUMapID;
                        pdtVM.ProductID = pdt.ProductID;
                        pdtVM.DeliveryTypeID = pdt.DeliveryTypeID;
                        pdtVM.CartTotalFrom = pdt.CartTotalFrom;
                        pdtVM.CartTotalTo = pdt.CartTotalTo;
                        pdtVM.DeliveryCharge = pdt.DeliveryCharge;
                        pdtVM.DeliveryChargePercentage = pdt.DeliveryChargePercentage;
                        pdtVM.DeliveryDays = pdt.DeliveryDays;
                        pdtVM.CompanyID = pdt.CompanyID;
                        pdtVM.IsDeliveryAvailable = pdt.IsDeliveryAvailable;
                        pdtVM.CreatedBy = pdt.CreatedBy;
                        pdtVM.CreatedDate = pdt.CreatedDate;
                        pdtVM.UpdatedBy = pdt.UpdatedBy;
                        pdtVM.UpdatedDate = pdt.UpdatedDate;
                        pdtVM.TimeStamps = pdt.TimeStamps;
                        pdtVM.BranchID = pdt.BranchID;
                        pdtVM.BranchName = pdt.BranchName;
                        pdtVM.Description = pdt.Description;
                        psbVM.ProductSKUTypeDeliveryTypes.Add(pdtVM);
                    }
                }

                return psbVM;
            }
            else
            {
                return new ProductSKUBranchSettingViewModel();
            }
        }


    }
}
