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
using Eduegate.Services;

namespace Eduegate.Service.Client.Direct
{
    public class DistributionServiceClient : BaseClient, IDistributionService
    {
        DistributionService distService = new DistributionService();
        
        public DistributionServiceClient(CallContext callContext = null, Action<string> logger = null)
            : base(callContext, logger)
        {
            distService.CallContext = callContext;
        }

        public RouteDTO GetRoute(int routeID)
        {
            return distService.GetRoute(routeID);
        }

        public RouteDTO SaveRoute(RouteDTO dtoRoute)
        {
            return distService.SaveRoute(dtoRoute);
        }

        public ServiceProviderDTO SaveServiceProvider(ServiceProviderDTO providerDetail)
        {
            return distService.SaveServiceProvider(providerDetail);
        }

        public ServiceProviderDTO GetServiceProvider(int serviceProviderID)
        {
            return distService.GetServiceProvider(serviceProviderID);
        }

        public DeliverySettingDTO SaveDeliverySettings(DeliverySettingDTO setting)
        {
            return distService.SaveDeliverySettings(setting);
        }

        public DeliverySettingDTO GetDeliverySettings(int deliveryTypeID)
        {
            return distService.GetDeliverySettings(deliveryTypeID);
        }

        #region Product and SKU Delivery Types

        public List<DeliveryTypeDTO> GetProductDeliveryTypeMaps(long IID, bool isProduct,long branchId)
        {
            return distService.GetProductDeliveryTypeMaps(IID, isProduct, branchId);
        }

        public bool SaveProductDeliveryTypeMaps(List<ProductDeliveryTypeDTO> productDeliveryTypes, long IID, bool isProduct)
        {
            return distService.SaveProductDeliveryTypeMaps(productDeliveryTypes, IID, isProduct);
        }

        #endregion

        #region ZoneDeliveryTypes

        public List<DeliveryTypeDTO> GetZoneDeliveryTypeMaps(short zoneID)
        {
            return distService.GetZoneDeliveryTypeMaps(zoneID);
        }

        public bool SaveZoneDeliveryTypeMaps(List<ZoneDeliveryChargeDTO> zoneDeliveryTypes, short zoneID)
        {
            return distService.SaveZoneDeliveryTypeMaps(zoneDeliveryTypes, zoneID);
        }

        #endregion

        #region AreaDeliveryTypes

        public List<DeliveryTypeDTO> GetAreaDeliveryTypeMaps(int areaID)
        {
            return distService.GetAreaDeliveryTypeMaps(areaID);
        }

        public bool SaveAreaDeliveryTypeMaps(List<AreaDeliveryChargeDTO> areaDeliveryTypes, int areaID)
        {
            return distService.SaveAreaDeliveryTypeMaps(areaDeliveryTypes, areaID);
        }

        #endregion

        #region CustomerGroup Delivery Types

        public List<DeliveryTypeDTO> GetCustomerGroupDeliveryTypeMaps(long customerGroupID)
        {
            return distService.GetCustomerGroupDeliveryTypeMaps(customerGroupID);
        }

        public bool SaveCustomerGroupDeliveryTypeMaps(List<CustomerGroupDeliveryChargeDTO> customerGroupDeliveryTypes, long customerGroupID)
        {
            return distService.SaveCustomerGroupDeliveryTypeMaps(customerGroupDeliveryTypes, customerGroupID);
        }

        #endregion

        public List<SKUBranchDeliveryTypeDTO> GetBranchDeliverySettings(long skuIID)
        {
            return distService.GetBranchDeliverySettings(skuIID);
        }
    }
}
