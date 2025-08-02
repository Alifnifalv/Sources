using System.Collections.Generic;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Services.Contracts.PriceSettings
{
    public interface IPriceSettings
    {
        List<ProductPriceSettingDTO> SaveProductPrice(List<ProductPriceSettingDTO> productPriceSettings, long productIID);

        List<Catalog.ProductPriceSKUDTO> SaveSKUPrice(List<Catalog.ProductPriceSKUDTO> skuPriceSettings, long skuIID);

        List<Catalog.ProductPriceCategoryDTO> SaveCategoryPriceSettings(List<Catalog.ProductPriceCategoryDTO> categoryPriceSettings, long categoryIID);

        List<BrandPriceDTO> SaveBrandPrice(List<BrandPriceDTO> brandPriceSettings);

        List<CategoryPriceDTO> SaveCategoryPrice(List<CategoryPriceDTO> categoryPriceSettings);

        List<CustomerGroupPriceDTO> SaveCustomerGroupPrice(List<CustomerGroupPriceDTO> customerGroupPriceSettings, long IID);

        ProductPriceSettingDTO GetProductPrice(long ID);

        Catalog.ProductPriceSKUDTO GetSKUPrice(long ID);

        List<ProductPriceBrandDTO> GetBrandPriceSettings(long brandID);

        List<ProductPriceCategoryDTO> GetCategoryPriceSettings(long categoryID);

        BrandPriceDTO GetBrandPrice(long ID);

        CategoryPriceDTO GetCategoryPrice(long ID);

        CustomerGroupPriceDTO GetCustomerGroupPrice(long ID);

        List<ProductPriceSKUDTO> GetProductPriceListForSKU(long skuID);

        List<CustomerGroupDTO> GetCustomerGroupPriceSettingsForSKU(long IID);

        List<ProductPriceDTO> GetProductPriceLists();


        List<BranchMapDTO> GetProductBranchLists(); 

        List<Catalog.ProductPriceBrandDTO> SaveBrandPriceSettings(List<Catalog.ProductPriceBrandDTO> brandPriceSettings, long brandIID);

        List<ProductPriceSettingDTO> GetProductPriceSettings(long productIID);

        List<CustomerGroupDTO> GetCustomerGroupPriceSettingsForProduct(long IID);

        List<CustomerGroupPriceDTO> SaveProductCustomerGroupPrice(List<CustomerGroupPriceDTO> customerGroupPriceSettings, long productID);

        List<KeyValueDTO> GetProductPriceListStatus();
    }
}