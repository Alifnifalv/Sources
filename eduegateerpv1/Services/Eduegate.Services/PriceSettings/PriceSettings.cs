using System.ServiceModel;
using Eduegate.Domain;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.PriceSettings;

namespace Eduegate.Services.PriceSettings
{
    public class PriceSettings : BaseService, IPriceSettings
    {
        public BrandPriceDTO GetBrandPrice(long ID)
        {
            throw new NotImplementedException();
        }
        public BrandPriceDTO GetBranchMaps(long ID) 
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
            try 
            {
                List<CustomerGroupPriceDTO> customerGroupPrices = new PriceSettingsBL(CallContext).SaveCustomerGroupPrice(customerGroupPriceSettings, IID);
                Eduegate.Logger.LogHelper<List<CustomerGroupPriceDTO>>.Info("Service Result : " + customerGroupPrices.ToString());
                return customerGroupPrices;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<List<ProductPriceSKUDTO>>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<ProductPriceSettingDTO> SaveProductPrice(List<ProductPriceSettingDTO> productPriceSettings, long productIID)
        {
            try
            {
                List<ProductPriceSettingDTO> ppsDTOs = new PriceSettingsBL(CallContext).SaveProductPrice(productPriceSettings, productIID);
                Eduegate.Logger.LogHelper<List<ProductPriceSettingDTO>>.Info("Service Result : " + ppsDTOs.ToString());
                return ppsDTOs;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<List<ProductPriceSettingDTO>>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<ProductPriceSKUDTO> SaveSKUPrice(List<ProductPriceSKUDTO> skuPriceSettings, long skuIID)
        {
            try
            {
                List<ProductPriceSKUDTO> productPriceSKUs = new PriceSettingsBL(CallContext).SaveSKUPrice(skuPriceSettings, skuIID);
                Eduegate.Logger.LogHelper<List<ProductPriceSKUDTO>>.Info("Service Result : " + productPriceSKUs.ToString());
                return productPriceSKUs;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<List<ProductPriceSKUDTO>>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<ProductPriceCategoryDTO> SaveCategoryPriceSettings(List<ProductPriceCategoryDTO> categoryPriceSettings,long categoryIID)
        {
            try
            {
                List<ProductPriceCategoryDTO> categoryPrices = new PriceSettingsBL(CallContext).SaveCategoryPriceSettings(categoryPriceSettings, categoryIID);
                return categoryPrices;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<List<ProductPriceCategoryDTO>>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<ProductPriceSKUDTO> GetProductPriceListForSKU(long skuID)
        {
            try
            {
                List<ProductPriceSKUDTO> productPriceSKUs = new PriceSettingsBL(CallContext).GetProductPriceListForSKU(skuID);
                return productPriceSKUs;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ReferenceData>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<ProductPriceCategoryDTO> GetCategoryPriceSettings(long categoryID)
        {
            try
            {
                List<ProductPriceCategoryDTO> CategoryPrices = new PriceSettingsBL(CallContext).GetCategoryPriceList(categoryID);
                return CategoryPrices;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ReferenceData>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<ProductPriceBrandDTO> GetBrandPriceSettings(long brandID)
        {
            try
            {
                List<ProductPriceBrandDTO> brandPrices = new PriceSettingsBL(CallContext).GetBrandPriceList(brandID);
                return brandPrices;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ReferenceData>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<CustomerGroupDTO> GetCustomerGroupPriceSettingsForSKU(long IID)
        {
            try
            {
                return new PriceSettingsBL(CallContext).GetCustomerGroupPriceSettingsForSKU(IID);
            }
            catch(Exception ex)
            {
                Eduegate.Logger.LogHelper<PriceSettings>.Fatal(ex.Message, ex);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<ProductPriceDTO> GetProductPriceLists()
        {
            try
            {
                List<ProductPriceDTO> productLists = new PriceSettingsBL(CallContext).GetProductPriceLists((int)CallContext.CompanyID);
                return productLists;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<PriceSettings>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<BranchMapDTO> GetProductBranchLists()
        {
            try 
            {
                List<BranchMapDTO> productLists = new PriceSettingsBL(CallContext).GetProductBranchLists();
                return productLists;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<PriceSettings>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<ProductPriceBrandDTO> SaveBrandPriceSettings(List<ProductPriceBrandDTO> brandPriceSettings,long brandIID) 
        {
            try
            {
                List<ProductPriceBrandDTO> brandPrices = new PriceSettingsBL(CallContext).SaveBrandPriceSettings(brandPriceSettings, brandIID);
                return brandPrices;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<List<ProductPriceBrandDTO>>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<ProductPriceSettingDTO> GetProductPriceSettings(long productIID)
        {
            try
            {
                List<ProductPriceSettingDTO> priceSettings = new PriceSettingsBL(CallContext).GetProductPriceSettings(productIID);
                return priceSettings;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ReferenceData>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<CustomerGroupDTO> GetCustomerGroupPriceSettingsForProduct(long IID)
        {
            try
            {
                return new PriceSettingsBL(CallContext).GetCustomerGroupPriceSettingsForProduct(IID);
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<PriceSettings>.Fatal(ex.Message, ex);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<CustomerGroupPriceDTO> SaveProductCustomerGroupPrice(List<CustomerGroupPriceDTO> customerGroupPriceSettings, long productID)
        {
            try
            {
                List<CustomerGroupPriceDTO> customerGroupPrices = new PriceSettingsBL(CallContext).SaveProductCustomerGroupPrice(customerGroupPriceSettings, productID);
                Eduegate.Logger.LogHelper<List<CustomerGroupPriceDTO>>.Info("Service Result : " + customerGroupPrices.ToString());
                return customerGroupPrices;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<List<ProductPriceSKUDTO>>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<KeyValueDTO> GetProductPriceListStatus()
        {
            try 
            {
                List<KeyValueDTO> productLists = new ReferenceDataBL(CallContext).GetLookUpData(Contracts.Enums.LookUpTypes.PriceListStatuses);
                return productLists;
            } 
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<PriceSettings>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }       
    }
}
