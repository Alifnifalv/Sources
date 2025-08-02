using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Distributions;
using Eduegate.Services.Contracts.Mutual;

namespace Eduegate.Service.Client
{
    public class DistributionServiceClient : BaseClient, IDistributionService
    {
        private static string _serviceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        private static string _service = string.Concat(_serviceHost, Eduegate.Framework.Helper.Constants.DISTRIBUTION_SERVICE_NAME);
        public DistributionServiceClient(CallContext callContext = null, Action<string> logger = null)
            : base(callContext, logger)
        {
        }

        public RouteDTO GetRoute(int routeID)
        {
            var uri = _service + "GetRoute?routeID=" + routeID;
            return ServiceHelper.HttpGetRequest<RouteDTO>(uri, _callContext);
        }

        public RouteDTO SaveRoute(RouteDTO dtoRoute)
        {
            var uri = _service + "SaveRoute";
            return ServiceHelper.HttpPostGetRequest<RouteDTO>(uri, dtoRoute, _callContext);
        }

        public ServiceProviderDTO SaveServiceProvider(ServiceProviderDTO providerDetail)
        {
            var uri = _service + "SaveServiceProvider";
            return ServiceHelper.HttpPostGetRequest<ServiceProviderDTO>(uri, providerDetail, _callContext);
        }

        public ServiceProviderDTO GetServiceProvider(int serviceProviderID)
        {
            var uri = _service + "GetServiceProvider?serviceProviderID=" + serviceProviderID;
            return ServiceHelper.HttpGetRequest<ServiceProviderDTO>(uri, _callContext);
        }


        public DeliverySettingDTO SaveDeliverySettings(DeliverySettingDTO setting)
        {
            var uri = _service + "SaveDeliverySettings";
            return ServiceHelper.HttpPostGetRequest<DeliverySettingDTO>(uri, setting, _callContext);
        }

        public DeliverySettingDTO GetDeliverySettings(int deliveryTypeID)
        {
            var uri = _service + "GetDeliverySettings?deliveryTypeID=" + deliveryTypeID;
            return ServiceHelper.HttpGetRequest<DeliverySettingDTO>(uri, _callContext);
        }

        #region Product and SKU Delivery Types

        public List<DeliveryTypeDTO> GetProductDeliveryTypeMaps(long IID, bool isProduct,long branchId)
        {
            var uri = _service + "GetProductDeliveryTypeMaps?IID=" + IID + "&isProduct="+ isProduct + "&branchId=" + branchId;
            return ServiceHelper.HttpGetRequest<List<DeliveryTypeDTO>>(uri, _callContext);
        }

        public bool SaveProductDeliveryTypeMaps(List<ProductDeliveryTypeDTO> productDeliveryTypes, long IID, bool isProduct)
        {
            var uri = _service + "SaveProductDeliveryTypeMaps?IID=" + IID + "&isProduct="+ isProduct;
            var result = ServiceHelper.HttpPostRequest(uri, productDeliveryTypes, _callContext);
            return JsonConvert.DeserializeObject<bool>(result);
        }

        #endregion

        #region ZoneDeliveryTypes

        public List<DeliveryTypeDTO> GetZoneDeliveryTypeMaps(short zoneID)
        {
            var uri = _service + "GetZoneDeliveryTypeMaps?zoneID=" + zoneID;
            return ServiceHelper.HttpGetRequest<List<DeliveryTypeDTO>>(uri, _callContext);
        }

        public bool SaveZoneDeliveryTypeMaps(List<ZoneDeliveryChargeDTO> zoneDeliveryTypes, short zoneID)
        {
            var uri = _service + "SaveZoneDeliveryTypeMaps?zoneID=" + zoneID;
            var result = ServiceHelper.HttpPostRequest(uri, zoneDeliveryTypes, _callContext);
            return JsonConvert.DeserializeObject<bool>(result);
        }

        #endregion

        #region AreaDeliveryTypes

        public List<DeliveryTypeDTO> GetAreaDeliveryTypeMaps(int areaID)
        {
            var uri = _service + "GetAreaDeliveryTypeMaps?areaID=" + areaID;
            return ServiceHelper.HttpGetRequest<List<DeliveryTypeDTO>>(uri, _callContext);
        }

        public bool SaveAreaDeliveryTypeMaps(List<AreaDeliveryChargeDTO> areaDeliveryTypes, int areaID)
        {
            var uri = _service + "SaveAreaDeliveryTypeMaps?areaID=" + areaID;
            var result = ServiceHelper.HttpPostRequest(uri, areaDeliveryTypes, _callContext);
            return JsonConvert.DeserializeObject<bool>(result);
        }

        #endregion

        #region CustomerGroup Delivery Types

        public List<DeliveryTypeDTO> GetCustomerGroupDeliveryTypeMaps(long customerGroupID)
        {
            var uri = _service + "GetCustomerGroupDeliveryTypeMaps?customerGroupID=" + customerGroupID;
            return ServiceHelper.HttpGetRequest<List<DeliveryTypeDTO>>(uri, _callContext);
        }

        public bool SaveCustomerGroupDeliveryTypeMaps(List<CustomerGroupDeliveryChargeDTO> customerGroupDeliveryTypes, long customerGroupID)
        {
            var uri = _service + "SaveCustomerGroupDeliveryTypeMaps?customerGroupID=" + customerGroupID;
            var result = ServiceHelper.HttpPostRequest(uri, customerGroupDeliveryTypes, _callContext);
            return JsonConvert.DeserializeObject<bool>(result);
        }

        #endregion

        public List<SKUBranchDeliveryTypeDTO> GetBranchDeliverySettings(long skuIID)
        {
            var uri = _service + "GetBranchDeliverySettings?skuIID=" + skuIID;
            return ServiceHelper.HttpGetRequest<List<SKUBranchDeliveryTypeDTO>>(uri, _callContext);
        }
    }
}
