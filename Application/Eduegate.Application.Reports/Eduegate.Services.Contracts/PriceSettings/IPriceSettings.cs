using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Services.Contracts.PriceSettings
{
    [ServiceContract]
    public interface IPriceSettings
    {
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveProductPrice?productIID={productIID}")]
        List<ProductPriceSettingDTO> SaveProductPrice(List<ProductPriceSettingDTO> productPriceSettings, long productIID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveSKUPrice?skuIID={skuIID}")]
        List<Catalog.ProductPriceSKUDTO> SaveSKUPrice(List<Catalog.ProductPriceSKUDTO> skuPriceSettings, long skuIID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveCategoryPriceSettings?categoryIID={categoryIID}")] 
        List<Catalog.ProductPriceCategoryDTO> SaveCategoryPriceSettings(List<Catalog.ProductPriceCategoryDTO> categoryPriceSettings,long categoryIID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
              BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveBrandPrice")]
        List<BrandPriceDTO> SaveBrandPrice(List<BrandPriceDTO> brandPriceSettings);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveCategoryPrice")]
        List<CategoryPriceDTO> SaveCategoryPrice(List<CategoryPriceDTO> categoryPriceSettings);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveCustomerGroupPrice?IID={IID}")]
        List<CustomerGroupPriceDTO> SaveCustomerGroupPrice(List<CustomerGroupPriceDTO> customerGroupPriceSettings,long IID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetProductPrice?ID={ID}")]
        ProductPriceSettingDTO GetProductPrice(long ID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetSKUPrice?ID={ID}")]
        Catalog.ProductPriceSKUDTO GetSKUPrice(long ID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetBrandPriceSettings?brandID={brandID}")]
        List<ProductPriceBrandDTO> GetBrandPriceSettings(long brandID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, 
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetCategoryPriceSettings?categoryID={categoryID}")]
        List<ProductPriceCategoryDTO> GetCategoryPriceSettings(long categoryID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
             BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetBrandPrice?ID={ID}")]
        BrandPriceDTO GetBrandPrice(long ID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetCategoryPrice?ID={ID}")]
        CategoryPriceDTO GetCategoryPrice(long ID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetCustomerGroupPrice?ID={ID}")]
        CustomerGroupPriceDTO GetCustomerGroupPrice(long ID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetProductPriceListForSKU?skuID={skuID}")]
        List<ProductPriceSKUDTO> GetProductPriceListForSKU(long skuID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetCustomerGroupPriceSettingsForSKU?IID={IID}")]
        List<CustomerGroupDTO> GetCustomerGroupPriceSettingsForSKU(long IID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetProductPriceLists")]
        List<ProductPriceDTO> GetProductPriceLists();


        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetProductBranchLists")]
        List<BranchMapDTO> GetProductBranchLists(); 

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, 
         BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveBrandPriceSettings?brandIID={brandIID}")] 
        List<Catalog.ProductPriceBrandDTO> SaveBrandPriceSettings(List<Catalog.ProductPriceBrandDTO> brandPriceSettings,long brandIID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetProductPriceSettings?productIID={productIID}")]
        List<ProductPriceSettingDTO> GetProductPriceSettings(long productIID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetCustomerGroupPriceSettingsForProduct?IID={IID}")]
        List<CustomerGroupDTO> GetCustomerGroupPriceSettingsForProduct(long IID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveProductCustomerGroupPrice?productID={productID}")]
        List<CustomerGroupPriceDTO> SaveProductCustomerGroupPrice(List<CustomerGroupPriceDTO> customerGroupPriceSettings, long productID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetProductPriceListStatus")]
        List<KeyValueDTO> GetProductPriceListStatus();
    }
}
