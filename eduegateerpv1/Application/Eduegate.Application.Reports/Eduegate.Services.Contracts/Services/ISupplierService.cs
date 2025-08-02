using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Mutual;

namespace Eduegate.Services.Contracts.Services
{
    [ServiceContract]
    public interface ISupplierService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetSupplier/{SupplierID}/")]
        SupplierDTO GetSupplier(string supplierID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetSuppliers?searchText={searchText}&dataSize={dataSize}")]
        List<SupplierDTO> GetSuppliers(string searchText, int dataSize);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveSupplier")]
        SupplierDTO SaveSupplier(SupplierDTO supplierDTO);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
    BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetSupplierBySupplierIdAndCR?searchText={searchText}")]
        List<SupplierDTO> GetSupplierBySupplierIdAndCR(string searchText);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetSupplierStatuses")]
        List<SupplierStatusDTO> GetSupplierStatuses();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
                   BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetSKUPriceDetailByBranch?branchID={branchID}&skuMapID={skuMapID}")]
        ProductPriceListSKUMapDTO GetSKUPriceDetailByBranch(long branchID, long skuMapID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveSupplierAccountMaps")]
        List<Eduegate.Services.Contracts.Accounting.SupplierAccountEntitlmentMapsDTO> SaveSupplierAccountMaps(List<Eduegate.Services.Contracts.Accounting.SupplierAccountEntitlmentMapsDTO> SupplierAccountMapsDTOs);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
                   BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetSupplierByLoginID?loginID={loginID}")]
        SupplierDTO GetSupplierByLoginID(long loginID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "UpdateSupplierSKUInventory")]
        SKUDTO UpdateSupplierSKUInventory(SKUDTO sku);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "UpdateMarketPlaceOrderStatus")]
        bool UpdateMarketPlaceOrderStatus(MarketPlaceTransactionActionDTO OrderDetail);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
                   BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetSupplierProductPriceListSKUMapDetails?SKUID={SKUID}")]
        SKUDTO GetSupplierProductPriceListSKUMapDetails(long SKUID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetSupplierDeliveryMethod?supplierID={supplierID}")]
        KeyValueDTO GetSupplierDeliveryMethod(long supplierID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetSupplierReturnMethod?supplierID={supplierID}")]
        KeyValueDTO GetSupplierReturnMethod(long supplierID); 
    }
} 
