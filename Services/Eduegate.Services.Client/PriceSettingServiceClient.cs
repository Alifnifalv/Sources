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

namespace Eduegate.Service.Client
{
    public class PriceSettingServiceClient : BaseClient, IPriceSettings
    {
        private static string ServiceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        private static string Service = string.Concat(ServiceHost, Eduegate.Framework.Helper.Constants.PRICE_SETTING_SERVICE);

        public PriceSettingServiceClient(CallContext callContext = null, Action<string> logger = null)
            : base(callContext, logger)
        {

        }

        public BrandPriceDTO GetBrandPrice(long ID)
        {
            throw new NotImplementedException();
        }

        public BranchMapDTO GetBranchMaps(long ID)
        {
            throw new NotImplementedException(); 
        }

        public CategoryPriceDTO GetCategoryPrice(long ID)
        {
            throw new NotImplementedException();
        }

        public CustomerGroupPriceDTO GetCustomerGroupPrice(long ID)
        {
            throw new NotImplementedException();
        }

        public ProductPriceSettingDTO GetProductPrice(long ID)
        {
            throw new NotImplementedException();
        }

        public ProductPriceSKUDTO GetSKUPrice(long ID)
        {
            throw new NotImplementedException();
        }

        public List<BrandPriceDTO> SaveBrandPrice(List<BrandPriceDTO> brandPriceSettings)
        {
            throw new NotImplementedException();
        }

        public List<BranchMapDTO> SaveBranchMaps(List<BranchMapDTO> branchMapSettings) 
        {
            throw new NotImplementedException();
        }

        public List<CategoryPriceDTO> SaveCategoryPrice(List<CategoryPriceDTO> categoryPriceSettings)
        {
            throw new NotImplementedException();
        }

        public List<CustomerGroupPriceDTO> SaveCustomerGroupPrice(List<CustomerGroupPriceDTO> customerGroupPriceSettings,long IID)
        {
            return ServiceHelper.HttpPostGetRequest<List<CustomerGroupPriceDTO>>(string.Format("{0}/{1}", Service, "SaveCustomerGroupPrice?IID=" + IID), customerGroupPriceSettings, _callContext, _logger);
        }

        public List<ProductPriceSettingDTO> SaveProductPrice(List<ProductPriceSettingDTO> productPriceSettings, long productIID)
        {
            return ServiceHelper.HttpPostGetRequest<List<ProductPriceSettingDTO>>(string.Format("{0}/{1}", Service, "SaveProductPrice?productIID=" + productIID), productPriceSettings, _callContext, _logger);
        }

        public List<ProductPriceSKUDTO> SaveSKUPrice(List<ProductPriceSKUDTO> skuPriceSettings, long skuIID)
        {
            return ServiceHelper.HttpPostGetRequest<List<ProductPriceSKUDTO>>(string.Format("{0}/{1}", Service, "SaveSKUPrice?skuIID="+ skuIID), skuPriceSettings, _callContext, _logger);
        }
        public List<ProductPriceCategoryDTO> SaveCategoryPriceSettings(List<ProductPriceCategoryDTO> categoryPriceSettings,long categoryIID)
        {
            return ServiceHelper.HttpPostGetRequest<List<ProductPriceCategoryDTO>>(string.Format("{0}/{1}", Service, "SaveCategoryPriceSettings?categoryIID="+ categoryIID), categoryPriceSettings, _callContext, _logger);
        }

        public List<ProductPriceSettingDTO> GetProductPriceSettings(long productIID)
        {
            var uri = string.Format("{0}/GetProductPriceSettings?productIID={1}", Service, productIID);
            return ServiceHelper.HttpGetRequest<List<ProductPriceSettingDTO>>(uri, _callContext, _logger);
        }

        public List<ProductPriceSKUDTO> GetProductPriceListForSKU(long skuID)
        {
            var uri = string.Format("{0}/GetProductPriceListForSKU?skuID={1}", Service, skuID);
            return ServiceHelper.HttpGetRequest<List<ProductPriceSKUDTO>>(uri, _callContext, _logger);
        }

        public List<CustomerGroupDTO> GetCustomerGroupPriceSettingsForSKU(long IID)
        {
            var uri = string.Format("{0}/GetCustomerGroupPriceSettingsForSKU?IID={1}", Service, IID);
            return ServiceHelper.HttpGetRequest<List<CustomerGroupDTO>>(uri, _callContext, _logger);
        }

        public List<CustomerGroupDTO> GetCustomerGroupPriceSettingsForProduct(long IID)
        {
            var uri = string.Format("{0}/GetCustomerGroupPriceSettingsForProduct?IID={1}", Service, IID);
            return ServiceHelper.HttpGetRequest<List<CustomerGroupDTO>>(uri, _callContext, _logger);
        }

        public List<ProductPriceDTO> GetProductPriceLists()
        {
            var uri = string.Format("{0}/GetProductPriceLists", Service);
            return ServiceHelper.HttpGetRequest<List<ProductPriceDTO>>(uri, _callContext, _logger);
        }

        public List<BranchMapDTO> GetProductBranchLists()
        {
            var uri = string.Format("{0}/GetProductBranchLists", Service);
            return ServiceHelper.HttpGetRequest<List<BranchMapDTO>>(uri, _callContext, _logger);
        }

        public List<ProductPriceBrandDTO> GetBrandPriceSettings(long brandID)
        {
            var uri = string.Format("{0}/GetBrandpriceSettings?brandID={1}", Service, brandID);
            return ServiceHelper.HttpGetRequest<List<ProductPriceBrandDTO>>(uri, _callContext, _logger);
        }

        public List<ProductPriceCategoryDTO> GetCategoryPriceSettings(long categoryID)
        {
            var uri = string.Format("{0}/GetCategoryPriceSettings?categoryID={1}", Service, categoryID);
            return ServiceHelper.HttpGetRequest<List<ProductPriceCategoryDTO>>(uri, _callContext, _logger);
        }

        public List<ProductPriceBrandDTO> SaveBrandPriceSettings(List<ProductPriceBrandDTO> brandPriceSettings,long brandIID)
        {
            return ServiceHelper.HttpPostGetRequest<List<ProductPriceBrandDTO>>(string.Format("{0}/{1}", Service, "SaveBrandPriceSettings?brandIID="+ brandIID), brandPriceSettings, _callContext, _logger);
        }

        public List<CustomerGroupPriceDTO> SaveProductCustomerGroupPrice(List<CustomerGroupPriceDTO> customerGroupPriceSettings, long productID)
        {
            return ServiceHelper.HttpPostGetRequest<List<CustomerGroupPriceDTO>>(string.Format("{0}/{1}", Service, "SaveProductCustomerGroupPrice?productID=" + productID), customerGroupPriceSettings, _callContext, _logger);
        }

        public List<KeyValueDTO> GetProductPriceListStatus() 
        {
            var uri = string.Format("{0}/GetProductPriceListStatus", Service);
            return ServiceHelper.HttpGetRequest<List<KeyValueDTO>>(uri, _callContext, _logger);
        }

    }
}
