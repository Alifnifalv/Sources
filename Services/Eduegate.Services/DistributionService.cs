using System.ServiceModel;
using Eduegate.Services.Contracts.Distributions;
using Eduegate.Domain;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts.Catalog;

namespace Eduegate.Services
{
    public class DistributionService : BaseService, IDistributionService
    {
        public RouteDTO SaveRoute(RouteDTO dtoRoute)
        {
            return new DistributionBL(CallContext).SaveRoute(dtoRoute);
        }

        public RouteDTO GetRoute(int routeID)
        {
            return new DistributionBL(CallContext).GetRoute(routeID);
        }

        public ServiceProviderDTO SaveServiceProvider(ServiceProviderDTO dto)
        {
            try
            {
                return new DistributionBL(CallContext).SaveServiceProvider(dto);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ServiceProviderDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public ServiceProviderDTO GetServiceProvider(int serviceProviderID)
        {
            try
            {
                return new DistributionBL(CallContext).GetServiceProvider(serviceProviderID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ServiceProviderDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public DeliverySettingDTO SaveDeliverySettings(DeliverySettingDTO setting)
        {
            try
            {
                return new DistributionBL(CallContext).SaveDeliverySettings(setting);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<DeliverySettingDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public DeliverySettingDTO GetDeliverySettings(int deliveryTypeID)
        {
            try
            {
                return new DistributionBL(CallContext).GetDeliverySettings(deliveryTypeID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<DeliverySettingDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<DeliveryTypeDTO> GetProductDeliveryTypeMaps(long IID, bool isProduct,long branchId)
        {
            try
            {
                return new DistributionBL(CallContext).GetProductDeliveryTypeMaps(IID, isProduct,branchId);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<DistributionService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public bool SaveProductDeliveryTypeMaps(List<ProductDeliveryTypeDTO> productDeliveryTypes, long IID, bool isProduct)
        {
            try
            {
                return new DistributionBL(CallContext).SaveProductDeliveryTypeMaps(productDeliveryTypes, IID, isProduct);
            }
            catch(Exception exception)
            {
                Eduegate.Logger.LogHelper<DistributionService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        #region Zone Delivery Types

        public List<DeliveryTypeDTO> GetZoneDeliveryTypeMaps(short zoneID)
        {
            try
            {
                return new DistributionBL(CallContext).GetZoneDeliveryTypeMaps(zoneID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<DistributionService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public bool SaveZoneDeliveryTypeMaps(List<ZoneDeliveryChargeDTO> zoneDeliveryTypes, short zoneID)
        {
            try
            {
                return new DistributionBL(CallContext).SaveZoneDeliveryTypeMaps(zoneDeliveryTypes, zoneID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<DistributionService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        #endregion

        #region Area Delivery Types

        public List<DeliveryTypeDTO> GetAreaDeliveryTypeMaps(int areaID)
        {
            try
            {
                return new DistributionBL(CallContext).GetAreaDeliveryTypeMaps(areaID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<DistributionService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public bool SaveAreaDeliveryTypeMaps(List<AreaDeliveryChargeDTO> areaDeliveryTypes, int areaID)
        {
            try
            {
                return new DistributionBL(CallContext).SaveAreaDeliveryTypeMaps(areaDeliveryTypes, areaID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<DistributionService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        #endregion

        #region CustomerGroup Delivery Types

        public List<DeliveryTypeDTO> GetCustomerGroupDeliveryTypeMaps(long customerGroupID)
        {
            try
            {
                return new DistributionBL(CallContext).GetCustomerGroupDeliveryTypeMaps(customerGroupID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<DistributionService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public bool SaveCustomerGroupDeliveryTypeMaps(List<CustomerGroupDeliveryChargeDTO> cgDeliveryTypes, long customerGroupID)
        {
            try
            {
                return new DistributionBL(CallContext).SaveCustomerGroupDeliveryTypeMaps(cgDeliveryTypes, customerGroupID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<DistributionService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        #endregion

        public List<SKUBranchDeliveryTypeDTO> GetBranchDeliverySettings(long skuIID)
        {
            try
            {
                List<SKUBranchDeliveryTypeDTO> branchSKUs = new DistributionBL(CallContext).GetBranchDeliverySettings(skuIID);
                return branchSKUs;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ReferenceData>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

    }
}
