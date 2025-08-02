using System.Collections.Generic;
using Eduegate.Services.Contracts.Catalog;

namespace Eduegate.Services.Contracts.Distributions
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IDistributionService" in both code and config file together.
    public interface IDistributionService
    {     
        RouteDTO SaveRoute(RouteDTO dtoRoute);

        RouteDTO GetRoute(int routeID);

        ServiceProviderDTO SaveServiceProvider(ServiceProviderDTO providerDetail);

        ServiceProviderDTO GetServiceProvider(int serviceProviderID);

        DeliverySettingDTO SaveDeliverySettings(DeliverySettingDTO setting);

        DeliverySettingDTO GetDeliverySettings(int deliveryTypeID);

        List<DeliveryTypeDTO> GetProductDeliveryTypeMaps(long IID, bool isProduct,long branchId);

        bool SaveProductDeliveryTypeMaps(List<ProductDeliveryTypeDTO> productDeliveryTypes, long IID, bool isProduct);

        #region Zone Delivery Types

        List<DeliveryTypeDTO> GetZoneDeliveryTypeMaps(short zoneID);

        bool SaveZoneDeliveryTypeMaps(List<ZoneDeliveryChargeDTO> productDeliveryTypes, short zoneID);

        #endregion

        #region Area Delivery Types

        List<DeliveryTypeDTO> GetAreaDeliveryTypeMaps(int areaID);

        bool SaveAreaDeliveryTypeMaps(List<AreaDeliveryChargeDTO> areaDeliveryTypes, int areaID);

        #endregion

        #region CustomerGroup Delivery Types

        List<DeliveryTypeDTO> GetCustomerGroupDeliveryTypeMaps(long customerGroupID);

        bool SaveCustomerGroupDeliveryTypeMaps(List<CustomerGroupDeliveryChargeDTO> cgDeliveryTypes, long customerGroupID);

        #endregion

        List<SKUBranchDeliveryTypeDTO> GetBranchDeliverySettings(long skuIID);

    }
}
