using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Mutual;

namespace Eduegate.Services.Contracts.Distributions
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IDistributionService" in both code and config file together.
    [ServiceContract]
    public interface IDistributionService
    {
        
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveRoute")]
        RouteDTO SaveRoute(RouteDTO dtoRoute);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetRoute?routeID={routeID}")]
        RouteDTO GetRoute(int routeID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveServiceProvider")]
        ServiceProviderDTO SaveServiceProvider(ServiceProviderDTO providerDetail);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetServiceProvider?serviceProviderID={serviceProviderID}")]
        ServiceProviderDTO GetServiceProvider(int serviceProviderID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveDeliverySettings")]
        DeliverySettingDTO SaveDeliverySettings(DeliverySettingDTO setting);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetDeliverySettings?deliveryTypeID={deliveryTypeID}")]
        DeliverySettingDTO GetDeliverySettings(int deliveryTypeID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetProductDeliveryTypeMaps?IID={IID}&isProduct={isProduct}&branchId={branchId}")]
        List<DeliveryTypeDTO> GetProductDeliveryTypeMaps(long IID, bool isProduct,long branchId);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveProductDeliveryTypeMaps?IID={IID}&isProduct={isProduct}")]
        bool SaveProductDeliveryTypeMaps(List<ProductDeliveryTypeDTO> productDeliveryTypes, long IID, bool isProduct);

        #region Zone Delivery Types

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetZoneDeliveryTypeMaps?zoneID={zoneID}")]
        List<DeliveryTypeDTO> GetZoneDeliveryTypeMaps(short zoneID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveZoneDeliveryTypeMaps?zoneID={zoneID}")]
        bool SaveZoneDeliveryTypeMaps(List<ZoneDeliveryChargeDTO> productDeliveryTypes, short zoneID);

        #endregion

        #region Area Delivery Types

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetAreaDeliveryTypeMaps?areaID={areaID}")]
        List<DeliveryTypeDTO> GetAreaDeliveryTypeMaps(int areaID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveAreaDeliveryTypeMaps?areaID={areaID}")]
        bool SaveAreaDeliveryTypeMaps(List<AreaDeliveryChargeDTO> areaDeliveryTypes, int areaID);

        #endregion

        #region CustomerGroup Delivery Types

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetCustomerGroupDeliveryTypeMaps?customerGroupID={customerGroupID}")]
        List<DeliveryTypeDTO> GetCustomerGroupDeliveryTypeMaps(long customerGroupID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveCustomerGroupDeliveryTypeMaps?customerGroupID={customerGroupID}")]
        bool SaveCustomerGroupDeliveryTypeMaps(List<CustomerGroupDeliveryChargeDTO> cgDeliveryTypes, long customerGroupID);

        #endregion

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetBranchDeliverySettings?skuIID={skuIID}")]
        List<SKUBranchDeliveryTypeDTO> GetBranchDeliverySettings(long skuIID);

    }
}
