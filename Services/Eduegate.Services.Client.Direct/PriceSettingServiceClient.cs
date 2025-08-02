using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.PriceSettings;
using Eduegate.Services.PriceSettings;

namespace Eduegate.Service.Client.Direct
{
    public class PriceSettingServiceClient : BaseClient, IPriceSettings
    {
        PriceSettings service = new PriceSettings();

        public PriceSettingServiceClient(CallContext callContext = null, Action<string> logger = null)
            : base(callContext, logger)
        {
            service.CallContext = callContext;
        }

        public BrandPriceDTO GetBrandPrice(long ID)
        {
            return service.GetBrandPrice(ID);
        }

        public BrandPriceDTO GetBranchMaps(long ID)
        {
            return service.GetBranchMaps(ID);
        }

        public CategoryPriceDTO GetCategoryPrice(long ID)
        {
            return service.GetCategoryPrice(ID);
        }

        public CustomerGroupPriceDTO GetCustomerGroupPrice(long ID)
        {
            return service.GetCustomerGroupPrice(ID);
        }

        public ProductPriceSettingDTO GetProductPrice(long ID)
        {
            return service.GetProductPrice(ID);
        }

        public ProductPriceSKUDTO GetSKUPrice(long ID)
        {
            return service.GetSKUPrice(ID);
        }

        public List<BrandPriceDTO> SaveBrandPrice(List<BrandPriceDTO> brandPriceSettings)
        {
            return service.SaveBrandPrice(brandPriceSettings);
        }

        public List<BranchMapDTO> SaveBranchMaps(List<BranchMapDTO> branchMapSettings) 
        {
            return service.SaveBranchMaps(branchMapSettings);
        }

        public List<CategoryPriceDTO> SaveCategoryPrice(List<CategoryPriceDTO> categoryPriceSettings)
        {
            return service.SaveCategoryPrice(categoryPriceSettings);
        }

        public List<CustomerGroupPriceDTO> SaveCustomerGroupPrice(List<CustomerGroupPriceDTO> customerGroupPriceSettings,long IID)
        {
            return service.SaveCustomerGroupPrice(customerGroupPriceSettings, IID);
        }

        public List<ProductPriceSettingDTO> SaveProductPrice(List<ProductPriceSettingDTO> productPriceSettings, long productIID)
        {
            return service.SaveProductPrice(productPriceSettings, productIID);
        }

        public List<ProductPriceSKUDTO> SaveSKUPrice(List<ProductPriceSKUDTO> skuPriceSettings, long skuIID)
        {
            return service.SaveSKUPrice(skuPriceSettings, skuIID);
        }
        public List<ProductPriceCategoryDTO> SaveCategoryPriceSettings(List<ProductPriceCategoryDTO> categoryPriceSettings,long categoryIID)
        {
            return service.SaveCategoryPriceSettings(categoryPriceSettings, categoryIID);
        }

        public List<ProductPriceSettingDTO> GetProductPriceSettings(long productIID)
        {
            return service.GetProductPriceSettings(productIID);
        }

        public List<ProductPriceSKUDTO> GetProductPriceListForSKU(long skuID)
        {
            return service.GetProductPriceListForSKU(skuID);
        }

        public List<CustomerGroupDTO> GetCustomerGroupPriceSettingsForSKU(long IID)
        {
            return service.GetCustomerGroupPriceSettingsForSKU(IID);
        }

        public List<CustomerGroupDTO> GetCustomerGroupPriceSettingsForProduct(long IID)
        {
            return service.GetCustomerGroupPriceSettingsForProduct(IID);
        }

        public List<ProductPriceDTO> GetProductPriceLists()
        {
            return service.GetProductPriceLists();
        }

        public List<BranchMapDTO> GetProductBranchLists()
        {
            return service.GetProductBranchLists();
        }

        public List<ProductPriceBrandDTO> GetBrandPriceSettings(long brandID)
        {
            return service.GetBrandPriceSettings(brandID);
        }

        public List<ProductPriceCategoryDTO> GetCategoryPriceSettings(long categoryID)
        {
            return service.GetCategoryPriceSettings(categoryID);
        }

        public List<ProductPriceBrandDTO> SaveBrandPriceSettings(List<ProductPriceBrandDTO> brandPriceSettings,long brandIID)
        {
            return service.SaveBrandPriceSettings(brandPriceSettings, brandIID);
        }

        public List<CustomerGroupPriceDTO> SaveProductCustomerGroupPrice(List<CustomerGroupPriceDTO> customerGroupPriceSettings, long productID)
        {
            return service.SaveProductCustomerGroupPrice(customerGroupPriceSettings, productID);
        }

        public List<KeyValueDTO> GetProductPriceListStatus() 
        {
            return service.GetProductPriceListStatus();
        }
    }
}
