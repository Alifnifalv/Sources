using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts;

namespace Eduegate.Web.Library.ViewModels
{
    public class ProductDeliveryCountrySettingViewModel : BaseMasterViewModel
    {
        public long ProductDeliveryCountrySettingsIID { get; set; }
        public Nullable<long> ProductID { get; set; }
        public Nullable<long> ProductSKUMapID { get; set; }
        public Nullable<long> CountryID { get; set; }
        public string CountryName { get; set; }

        public static ProductDeliveryCountrySettingViewModel ToViewModel(ProductDeliveryCountrySettingDTO pdcsDTO)
        {
            if (pdcsDTO.IsNotNull())
            {
                ProductDeliveryCountrySettingViewModel pdcsVM = new ProductDeliveryCountrySettingViewModel();

                pdcsVM.ProductDeliveryCountrySettingsIID = pdcsDTO.ProductDeliveryCountrySettingsIID;
                pdcsVM.ProductID = pdcsDTO.ProductID;
                pdcsVM.ProductSKUMapID = pdcsDTO.ProductSKUMapID;
                pdcsVM.CountryID = pdcsDTO.CountryID;
                pdcsVM.CountryName = pdcsDTO.CountryName;
                pdcsVM.CreatedDate = pdcsDTO.CreatedDate;
                pdcsVM.CreatedBy = pdcsDTO.CreatedBy;
                pdcsVM.UpdatedDate = pdcsDTO.UpdatedDate;
                pdcsVM.UpdatedBy = Convert.ToInt32(pdcsDTO.UpdatedBy);

                return pdcsVM;
            }
            else
            {
                return new ProductDeliveryCountrySettingViewModel();
            }
        }

        public static ProductDeliveryCountrySettingDTO ToDTO(ProductDeliveryCountrySettingViewModel pdcsVM, SKUViewModel sku)
        {
            if (pdcsVM.IsNotNull())
            {
                ProductDeliveryCountrySettingDTO pdcsDTO = new ProductDeliveryCountrySettingDTO();

                pdcsDTO.ProductDeliveryCountrySettingsIID = pdcsVM.ProductDeliveryCountrySettingsIID;
                pdcsDTO.ProductID = pdcsVM.ProductID.IsNotNull() ? pdcsVM.ProductID : Convert.ToInt32(sku.ProductID);
                pdcsDTO.ProductSKUMapID = pdcsVM.ProductSKUMapID.IsNotNull() ? pdcsVM.ProductSKUMapID : (sku.ProductSKUMapID != default(long) ? sku.ProductSKUMapID : (long?)null);
                pdcsDTO.CountryID = pdcsVM.CountryID;
                pdcsDTO.CountryName = pdcsVM.CountryName;
                pdcsDTO.CreatedDate = pdcsVM.CreatedDate;
                pdcsDTO.CreatedBy = pdcsVM.CreatedBy;
                pdcsDTO.UpdatedDate = pdcsVM.UpdatedDate;
                pdcsDTO.UpdatedBy = Convert.ToInt32(pdcsVM.UpdatedBy);

                return pdcsDTO;
            }
            else
            {
                return new ProductDeliveryCountrySettingDTO();
            }
        }

        public static ProductDeliveryCountrySettingViewModel ToSelect2DeliverySetting(CountryDTO dto)
        {
            if (dto != null)
            {
                return new ProductDeliveryCountrySettingViewModel()
                {
                    CountryID = Convert.ToInt32(dto.CountryID),
                    CountryName = dto.CountryName
                };
            }
            else return new ProductDeliveryCountrySettingViewModel();
        }
    }
}
