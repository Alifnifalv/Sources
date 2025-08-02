using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Distributions;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Mappers;
using Eduegate.Domain.Repository;
using Eduegate.Framework;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Extensions;
using Eduegate.Domain.Mappers.Distributions;

namespace Eduegate.Domain
{
    public class DistributionBL
    {
        private CallContext _CallContext { get; set; }

        public DistributionBL(CallContext context)
        {
            _CallContext = context;
        }

        #region Route

        public RouteDTO SaveRoute(RouteDTO dtoRoute)
        {
            // Check param variable availability
            if (dtoRoute == null)
                return new RouteDTO();

            // Convert Route DTO to Route entity
            Route route = RouteMapper.Mapper(_CallContext).ToEntity(dtoRoute);
            route.CompanyID = _CallContext.CompanyID;

            // Call Repository
            route = new DistributionRepository().SaveRoute(route);

            // Convert Route entity to Route DTO and return
            return RouteMapper.Mapper(_CallContext).ToDTO(route);
        }

        public RouteDTO GetRoute(int routeID)
        {
            // Check param variable availability
            if (routeID <= 0)
                return new RouteDTO();

            // Call Repository
            Route route = new DistributionRepository().GetRoute(routeID);

            // Convert Route entity to Route DTO and return
            return RouteMapper.Mapper(_CallContext).ToDTO(route);
        }

        #endregion

        #region Service Provider

        public ServiceProviderDTO SaveServiceProvider(ServiceProviderDTO dto)
        {
            var serviceProvider = new DistributionRepository().SaveServiceProvider(ServiceProviderMapper.Mapper(_CallContext).ToEntity(dto));
            return ServiceProviderMapper.Mapper(_CallContext).ToDTO(serviceProvider);
        }

        public ServiceProviderDTO GetServiceProvider(int serviceProviderID)
        {
            var entity = new DistributionRepository().GetServiceProvider(serviceProviderID);
            return ServiceProviderMapper.Mapper(_CallContext).ToDTO(entity);
        }

        public DeliverySettingDTO GetDeliverySettings(int deliveryTypeID)
        {
            var entity = new DistributionRepository().GetDeliverySettings(deliveryTypeID);
            var deliveryType = Mappers.Distributions.DeliveryTypeMapper.Mapper(_CallContext).ToDTO(entity);
            return deliveryType;
        }

        public DeliverySettingDTO SaveDeliverySettings(DeliverySettingDTO dto)
        {
            var serviceProvider = new DistributionRepository().SaveDeliverySettings(Mappers.Distributions.DeliveryTypeMapper.Mapper(_CallContext).ToEntity(dto));
            return Mappers.Distributions.DeliveryTypeMapper.Mapper(_CallContext).ToDTO(serviceProvider);
        }

        public List<DeliveryTypeDTO> GetProductDeliveryTypeMaps(long IID, bool isProduct, long branchId)
        {
            List<DeliveryTypeDTO> productDeliveryTypes = new List<DeliveryTypeDTO>();
            DeliveryTypeDTO deliveryType = null;
            ProductDeliveryTypeDTO pdtDTO = null;
            List<ProductDeliveryTypeMap> productDeliveryTypeMaps = new List<ProductDeliveryTypeMap>();
            var skuID = default(long);
            var pdts = new DistributionRepository().GetProductDeliveryTypeMaps(IID, isProduct, branchId);

            // If this is SKU get productid first
            if (isProduct == false)
            {
                skuID = IID;
                IID = new ProductDetailBL(_CallContext).GetProductSKUDetails(IID).ProductID;
            }

            var productTypeDeliveryTypeMaps = new DistributionRepository().GetProductTypeDeliveryTypes(IID);

            if (productTypeDeliveryTypeMaps.IsNotNull() && productTypeDeliveryTypeMaps.Count > 0)
            {
                foreach (var dt in productTypeDeliveryTypeMaps)
                {
                    if (isProduct == true)
                        productDeliveryTypeMaps = pdts.Where(x => x.ProductID == IID && x.DeliveryTypeID == dt.DeliveryTypeID).ToList();
                    else
                        productDeliveryTypeMaps = pdts.Where(x => x.ProductSKUMapID == skuID && x.DeliveryTypeID == dt.DeliveryTypeID).ToList();

                    deliveryType = new DeliveryTypeDTO();
                    deliveryType.DeliveryDetails = new List<ProductDeliveryTypeDTO>();

                    deliveryType.DeliveryTypeID = Convert.ToInt16(dt.DeliveryTypeID);
                    deliveryType.Description = new DistributionRepository().GetDeliveryTypeName(Convert.ToInt16(dt.DeliveryTypeID));

                    if (productDeliveryTypeMaps.IsNotNull() && productDeliveryTypeMaps.Count > 0)
                    {
                        foreach (var pdt in productDeliveryTypeMaps)
                        {
                            pdtDTO = new ProductDeliveryTypeDTO();

                            pdtDTO.DeliveryTypeID = Convert.ToInt16(pdt.DeliveryTypeID);
                            pdtDTO.Description = new DistributionRepository().GetDeliveryTypeName(Convert.ToInt16(dt.DeliveryTypeID));
                            pdtDTO.ProductDeliveryTypeMapIID = pdt.ProductDeliveryTypeMapIID;

                            //pdtDTO.ProductID = isProduct ? pdt.ProductID.IsNotNull() ? pdt.ProductID : IID : (long?)null;
                            //pdtDTO.ProductSKUMapID = !isProduct ? pdt.ProductSKUMapID.IsNotNull() ? pdt.ProductSKUMapID : IID : (long?)null;

                            pdtDTO.CompanyID = pdt.CompanyID > 0 ? pdt.CompanyID : _CallContext.CompanyID;
                            pdtDTO.ProductID = pdt.ProductID > 0 ? pdt.ProductID : null;
                            pdtDTO.ProductSKUMapID = pdt.ProductSKUMapID> 0  ? pdt.ProductSKUMapID : null;
                            pdtDTO.DeliveryDays = pdt.DeliveryDays;

                            pdtDTO.DeliveryTypeID = pdt.DeliveryTypeID.IsNotNull() ? pdt.DeliveryTypeID : dt.DeliveryTypeID;
                            pdtDTO.CartTotalFrom = pdt.CartTotalFrom;
                            pdtDTO.CartTotalTo = pdt.CartTotalTo;
                            pdtDTO.DeliveryCharge = pdt.DeliveryCharge;
                            pdtDTO.IsDeliveryAvailable = pdt.IsSelected;
                            pdtDTO.DeliveryChargePercentage = pdt.DeliveryChargePercentage;
                            pdtDTO.CreatedBy = pdt.CreatedBy;
                            pdtDTO.CreatedDate = pdt.CreatedDate;
                            pdtDTO.UpdatedBy = pdt.UpdatedBy;
                            pdtDTO.UpdatedDate = pdt.UpdatedDate;
                            pdtDTO.TimeStamps = pdt.TimeStamps.IsNotNull() ? Convert.ToBase64String(pdt.TimeStamps) : null;
                            pdtDTO.BranchID = pdt.BranchID.IsNotNull() ? pdt.BranchID : null;
                            deliveryType.DeliveryDetails.Add(pdtDTO);
                        }
                    }
                    else
                    {
                        pdtDTO = new ProductDeliveryTypeDTO();
                        pdtDTO.DeliveryTypeID = dt.DeliveryTypeID;
                        pdtDTO.ProductID = IID;
                        pdtDTO.ProductSKUMapID = skuID;
                        pdtDTO.BranchID = branchId.IsNotNull() ? branchId :(long?)null;

                        deliveryType.DeliveryDetails.Add(pdtDTO);
                    }

                    productDeliveryTypes.Add(deliveryType);
                }
            }

            return productDeliveryTypes;
        }

        public bool SaveProductDeliveryTypeMaps(List<ProductDeliveryTypeDTO> productDeliveryTypes, long IID, bool isProduct)
        {
            List<ProductDeliveryTypeMap> pdtMaps = new List<ProductDeliveryTypeMap>();
            ProductDeliveryTypeMap entity = null;
            List<ProductDeliveryTypeMap> deleteEntities = new List<ProductDeliveryTypeMap>();

            foreach (var pdtMap in productDeliveryTypes)
            {
                entity = new ProductDeliveryTypeMap();

                entity.ProductDeliveryTypeMapIID = pdtMap.ProductDeliveryTypeMapIID.IsNotNull()? pdtMap.ProductDeliveryTypeMapIID : 0;
                entity.ProductID = pdtMap.ProductID.IsNotNull()? pdtMap.ProductID : 0;
                entity.CompanyID = pdtMap.CompanyID > 0 ? pdtMap.CompanyID : _CallContext.CompanyID;

                if (pdtMap.ProductSKUMapID.IsNotNull() && pdtMap.ProductSKUMapID > 0)
                {
                    entity.ProductSKUMapID = pdtMap.ProductSKUMapID;
                }

                entity.DeliveryDays = pdtMap.DeliveryDays;
                entity.DeliveryTypeID = pdtMap.DeliveryTypeID;
                entity.CartTotalFrom = pdtMap.CartTotalFrom;
                entity.CartTotalTo = pdtMap.CartTotalTo;
                entity.IsSelected = pdtMap.IsDeliveryAvailable;
                entity.DeliveryCharge = pdtMap.DeliveryCharge;
                entity.DeliveryChargePercentage = pdtMap.DeliveryChargePercentage;
                entity.CreatedBy = pdtMap.ProductDeliveryTypeMapIID > 0 ? pdtMap.CreatedBy : (int)_CallContext.LoginID;
                entity.CreatedDate = pdtMap.ProductDeliveryTypeMapIID > 0 ? pdtMap.CreatedDate : DateTime.Now;
                entity.UpdatedBy = pdtMap.ProductDeliveryTypeMapIID > 0 ? (int)_CallContext.LoginID : pdtMap.UpdatedBy;
                entity.UpdatedDate = pdtMap.ProductDeliveryTypeMapIID > 0 ? pdtMap.UpdatedDate : DateTime.Now;
                entity.TimeStamps = pdtMap.TimeStamps.IsNotNull() ? Convert.FromBase64String(pdtMap.TimeStamps) : null;
                entity.BranchID = pdtMap.BranchID;
                pdtMaps.Add(entity);

            }

            // Check if we are editing deliverytypes
            var currentData = productDeliveryTypes.Where(x => x.ProductDeliveryTypeMapIID >= 0).ToList();

            // Get stored deliverytype maps for current sku
            var dbData = new DistributionRepository().GetProductDeliveryTypeMaps(IID, isProduct,(int)_CallContext.CompanyID);

            // If current records exist in DB
            if (currentData.IsNotNull() && currentData.Count > 0)
            {
                deleteEntities = dbData;
            }
            else
            {
                foreach (var db in dbData)
                {
                    var item = currentData.Where(x => x.ProductDeliveryTypeMapIID == db.ProductDeliveryTypeMapIID).FirstOrDefault();

                    if (item == null)
                        deleteEntities.Add(db);
                }
            }

            return new DistributionRepository().SaveProductDeliveryTypeMaps(pdtMaps, IID, deleteEntities);
        }

        #endregion

        #region Zone Delivery Types

        public List<DeliveryTypeDTO> GetZoneDeliveryTypeMaps(short zoneID)
        {
            List<DeliveryTypeDTO> deliveryTypes = new List<DeliveryTypeDTO>();
            DeliveryTypeDTO deliveryType = null;
            ZoneDeliveryChargeDTO zoneDTO = null;
            List<DeliveryTypeAllowedZoneMap> zoneDeliveryTypeDetails = new List<DeliveryTypeAllowedZoneMap>();

            var zoneDeliveryDetails = new DistributionRepository().GetZoneDeliveryTypeMaps(zoneID);
            var zoneDeliveryTypes = new DistributionRepository().GetDeliveryTypes();

            if (zoneDeliveryTypes.IsNotNull() && zoneDeliveryTypes.Count > 0)
            {
                foreach (var dt in zoneDeliveryTypes)
                {
                    zoneDeliveryTypeDetails = zoneDeliveryDetails.Where(x => x.ZoneID == zoneID && x.DeliveryTypeID == dt.DeliveryTypeID).ToList();

                    deliveryType = new DeliveryTypeDTO();
                    deliveryType.ZoneDeliveryDetails = new List<ZoneDeliveryChargeDTO>();

                    deliveryType.DeliveryTypeID = Convert.ToInt16(dt.DeliveryTypeID);
                    deliveryType.Description = new DistributionRepository().GetDeliveryTypeName(Convert.ToInt16(dt.DeliveryTypeID));

                    if (zoneDeliveryTypeDetails.IsNotNull() && zoneDeliveryTypeDetails.Count > 0)
                    {
                        foreach (var pdt in zoneDeliveryTypeDetails)
                        {
                            deliveryType.ZoneDeliveryDetails.Add(DeliveryTypeAllowedZoneMapper.Mapper(_CallContext).ToDTO(pdt));
                        }
                    }
                    else
                    {
                        zoneDTO = new ZoneDeliveryChargeDTO();

                        zoneDTO.DeliveryTypeID = dt.DeliveryTypeID;
                        zoneDTO.ZoneID = zoneID;

                        deliveryType.ZoneDeliveryDetails.Add(zoneDTO);
                    }

                    deliveryTypes.Add(deliveryType);
                }
            }

            return deliveryTypes;
        }

        public bool SaveZoneDeliveryTypeMaps(List<ZoneDeliveryChargeDTO> zoneDeliveryDetails, short zoneID)
        {
            List<DeliveryTypeAllowedZoneMap> zoneMaps = new List<DeliveryTypeAllowedZoneMap>();
            List<DeliveryTypeAllowedZoneMap> deleteEntities = new List<DeliveryTypeAllowedZoneMap>();

            foreach (var zdt in zoneDeliveryDetails)
            {
                zoneMaps.Add(DeliveryTypeAllowedZoneMapper.Mapper(_CallContext).ToEntity(zdt));
            }

            var currentData = zoneDeliveryDetails.Where(x => x.ZoneDeliveryTypeMapIID > 0).ToList();
            var dbData = new DistributionRepository().GetZoneDeliveryTypeMaps(zoneID);

            if (currentData.IsNotNull() && currentData.Count == 0)
            {
                deleteEntities = dbData;
            }
            else
            {
                foreach (var db in dbData)
                {
                    var item = currentData.Where(x => x.ZoneDeliveryTypeMapIID == db.ZoneDeliveryTypeMapIID).FirstOrDefault();

                    if (item == null)
                        deleteEntities.Add(db);
                }
            }

            return new DistributionRepository().SaveZoneDeliveryTypeMaps(zoneMaps, zoneID, deleteEntities);
        }

        #endregion

        #region Area Delivery Types

        public List<DeliveryTypeDTO> GetAreaDeliveryTypeMaps(int areaID)
        {
            List<DeliveryTypeDTO> deliveryTypes = new List<DeliveryTypeDTO>();
            DeliveryTypeDTO deliveryType = null;
            AreaDeliveryChargeDTO areaDTO = null;
            List<DeliveryTypeAllowedAreaMap> areaDeliveryTypeDetails = new List<DeliveryTypeAllowedAreaMap>();

            var areaDeliveryDetails = new DistributionRepository().GetAreaDeliveryTypeMaps(areaID);
            var areaDeliveryTypes = new DistributionRepository().GetDeliveryTypes();

            if (areaDeliveryTypes.IsNotNull() && areaDeliveryTypes.Count > 0)
            {
                foreach (var dt in areaDeliveryTypes)
                {
                    areaDeliveryTypeDetails = areaDeliveryDetails.Where(x => x.AreaID == areaID && x.DeliveryTypeID == dt.DeliveryTypeID).ToList();

                    deliveryType = new DeliveryTypeDTO();
                    deliveryType.AreaDeliveryDetails = new List<AreaDeliveryChargeDTO>();

                    deliveryType.DeliveryTypeID = Convert.ToInt16(dt.DeliveryTypeID);
                    deliveryType.Description = new DistributionRepository().GetDeliveryTypeName(Convert.ToInt16(dt.DeliveryTypeID));

                    if (areaDeliveryTypeDetails.IsNotNull() && areaDeliveryTypeDetails.Count > 0)
                    {
                        foreach (var pdt in areaDeliveryTypeDetails)
                        {
                            deliveryType.AreaDeliveryDetails.Add(DeliveryTypeAllowedAreaMapper.Mapper(_CallContext).ToDTO(pdt));
                        }
                    }
                    else
                    {
                        areaDTO = new AreaDeliveryChargeDTO();

                        areaDTO.DeliveryTypeID = dt.DeliveryTypeID;
                        areaDTO.AreaID = areaID;

                        deliveryType.AreaDeliveryDetails.Add(areaDTO);
                    }

                    deliveryTypes.Add(deliveryType);
                }
            }

            return deliveryTypes;
        }

        public bool SaveAreaDeliveryTypeMaps(List<AreaDeliveryChargeDTO> areaDeliveryDetails, int areaID)
        {
            List<DeliveryTypeAllowedAreaMap> areaMaps = new List<DeliveryTypeAllowedAreaMap>();
            List<DeliveryTypeAllowedAreaMap> deleteEntities = new List<DeliveryTypeAllowedAreaMap>();

            foreach (var adt in areaDeliveryDetails)
            {
                areaMaps.Add(DeliveryTypeAllowedAreaMapper.Mapper(_CallContext).ToEntity(adt));
            }

            var currentData = areaDeliveryDetails.Where(x => x.AreaDeliveryTypeMapIID > 0).ToList();
            var dbData = new DistributionRepository().GetAreaDeliveryTypeMaps(areaID);

            if (currentData.IsNotNull() && currentData.Count == 0)
            {
                deleteEntities = dbData;
            }
            else
            {
                foreach (var db in dbData)
                {
                    var item = currentData.Where(x => x.AreaDeliveryTypeMapIID == db.AreaDeliveryTypeMapIID).FirstOrDefault();

                    if (item == null)
                        deleteEntities.Add(db);
                }
            }

            return new DistributionRepository().SaveAreaDeliveryTypeMaps(areaMaps, areaID, deleteEntities);
        }

        #endregion

        #region CustomerGroup Delivery Types

        public List<DeliveryTypeDTO> GetCustomerGroupDeliveryTypeMaps(long customerGroupID)
        {
            List<DeliveryTypeDTO> deliveryTypes = new List<DeliveryTypeDTO>();
            DeliveryTypeDTO deliveryType = null;
            CustomerGroupDeliveryChargeDTO cgDTO = null;
            List<CustomerGroupDeliveryTypeMap> cgDeliveryTypeDetails = new List<CustomerGroupDeliveryTypeMap>();

            var cgDeliveryDetails = new DistributionRepository().GetCustomerGroupDeliveryTypeMaps(customerGroupID);
            var cgDeliveryTypes = new DistributionRepository().GetDeliveryTypes();

            if (cgDeliveryTypes.IsNotNull() && cgDeliveryTypes.Count > 0)
            {
                foreach (var dt in cgDeliveryTypes)
                {
                    cgDeliveryTypeDetails = cgDeliveryDetails.Where(x => x.CustomerGroupID == customerGroupID && x.DeliveryTypeID == dt.DeliveryTypeID).ToList();

                    deliveryType = new DeliveryTypeDTO();
                    deliveryType.CustomerGroupDeliveryDetails = new List<CustomerGroupDeliveryChargeDTO>();

                    deliveryType.DeliveryTypeID = Convert.ToInt16(dt.DeliveryTypeID);
                    deliveryType.Description = new DistributionRepository().GetDeliveryTypeName(Convert.ToInt16(dt.DeliveryTypeID));

                    if (cgDeliveryTypeDetails.IsNotNull() && cgDeliveryTypeDetails.Count > 0)
                    {
                        foreach (var pdt in cgDeliveryTypeDetails)
                        {
                            deliveryType.CustomerGroupDeliveryDetails.Add(CustomerGroupDeliveryTypeMapper.Mapper(_CallContext).ToDTO(pdt));
                        }
                    }
                    else
                    {
                        cgDTO = new CustomerGroupDeliveryChargeDTO();

                        cgDTO.DeliveryTypeID = dt.DeliveryTypeID;
                        cgDTO.CustomerGroupID = customerGroupID;

                        deliveryType.CustomerGroupDeliveryDetails.Add(cgDTO);
                    }

                    deliveryTypes.Add(deliveryType);
                }
            }

            return deliveryTypes;
        }

        public bool SaveCustomerGroupDeliveryTypeMaps(List<CustomerGroupDeliveryChargeDTO> cgDeliveryDetails, long customerGroupID)
        {
            List<CustomerGroupDeliveryTypeMap> cgMaps = new List<CustomerGroupDeliveryTypeMap>();
            List<CustomerGroupDeliveryTypeMap> deleteEntities = new List<CustomerGroupDeliveryTypeMap>();

            foreach (var cg in cgDeliveryDetails)
            {
                cgMaps.Add(CustomerGroupDeliveryTypeMapper.Mapper(_CallContext).ToEntity(cg));
            }

            var currentData = cgDeliveryDetails.Where(x => x.CustomerGroupDeliveryTypeMapIID > 0).ToList();
            var dbData = new DistributionRepository().GetCustomerGroupDeliveryTypeMaps(customerGroupID);

            if (currentData.IsNotNull() && currentData.Count == 0)
            {
                deleteEntities = dbData;
            }
            else
            {
                foreach (var db in dbData)
                {
                    var item = currentData.Where(x => x.CustomerGroupDeliveryTypeMapIID == db.CustomerGroupDeliveryTypeMapIID).FirstOrDefault();

                    if (item == null)
                        deleteEntities.Add(db);
                }
            }

            return new DistributionRepository().SaveCustomerGroupDeliveryTypeMaps(cgMaps, customerGroupID, deleteEntities);
        }

        #endregion

        public List<SKUBranchDeliveryTypeDTO> GetBranchDeliverySettings(long skuIID)
        {
            var sKUBranchDeliveryTypeDTOs = new List<SKUBranchDeliveryTypeDTO>();

            // get selected delivery types for that skuIID
            var deliveryDetails = new DistributionRepository().GetBranchDeliveryTypeBySkuID(skuIID, _CallContext.CompanyID.HasValue? (int)_CallContext.CompanyID : default(int));
            // Note: here we have Delivery Type Id

            //if (deliveryDetails.Count == 0)
            //{
            //    return null;
            //}

            // To Get All Delivery Types based on SKU Type (phy or digi)
            var productTypeDeliveryTypeMaps = new DistributionRepository().GetProductSKUTypeDeliveryTypes(skuIID);
            // Note: here also we have Delivery Type Id

            // get BranchId from deliveryDetails
            var branchIds = deliveryDetails.Select(x => x.BranchID).Distinct().ToList();

                foreach (var branch in branchIds)
                {
                    var sKUBranchDeliveryTypeDTO = new SKUBranchDeliveryTypeDTO();

                    sKUBranchDeliveryTypeDTO.BranchID = branch.IsNotNull()?branch : null;
                    if (sKUBranchDeliveryTypeDTO.BranchID.IsNotNull())
                    {
                      sKUBranchDeliveryTypeDTO.BranchName = new DistributionRepository().GetBranchName((long)branch);
                    }
                    sKUBranchDeliveryTypeDTO.DeliveryDetails = new List<ProductDeliveryTypeDTO>();

                    foreach (var productTypeDeliveryTypeMap in productTypeDeliveryTypeMaps)
                    {
                        // get delivery detail based on branchId
                        var delivery = (from d in deliveryDetails
                                        where d.BranchID == branch && d.DeliveryTypeID == productTypeDeliveryTypeMap.DeliveryTypeID
                                        select d).FirstOrDefault();

                        var productDeliveryTypeDTO = new ProductDeliveryTypeDTO();

                        if (delivery.IsNotNull())
                        {
                            productDeliveryTypeDTO.DeliveryTypeID = Convert.ToInt16(delivery.DeliveryTypeID);
                            productDeliveryTypeDTO.Description = new DistributionRepository().GetDeliveryTypeName(Convert.ToInt16(delivery.DeliveryTypeID));
                            productDeliveryTypeDTO.ProductDeliveryTypeMapIID = delivery.ProductDeliveryTypeMapIID.IsNotNull()? delivery.ProductDeliveryTypeMapIID : productTypeDeliveryTypeMap.ProductTypeDeliveryTypeMapIID;
                            productDeliveryTypeDTO.CompanyID = delivery.CompanyID > 0 ? delivery.CompanyID : _CallContext.CompanyID;
                            productDeliveryTypeDTO.ProductID = delivery.ProductID > 0 ? delivery.ProductID : null;
                            productDeliveryTypeDTO.ProductSKUMapID = delivery.ProductSKUMapID > 0 ? delivery.ProductSKUMapID : null;
                            productDeliveryTypeDTO.DeliveryDays = delivery.DeliveryDays;
                            productDeliveryTypeDTO.DeliveryTypeID = delivery.DeliveryTypeID.IsNotNull() ? delivery.DeliveryTypeID : delivery.DeliveryTypeID;
                            productDeliveryTypeDTO.CartTotalFrom = delivery.CartTotalFrom;
                            productDeliveryTypeDTO.CartTotalTo = delivery.CartTotalTo;
                            productDeliveryTypeDTO.DeliveryCharge = delivery.DeliveryCharge;
                            productDeliveryTypeDTO.IsDeliveryAvailable = delivery.IsSelected;
                            productDeliveryTypeDTO.DeliveryChargePercentage = delivery.DeliveryChargePercentage;
                            productDeliveryTypeDTO.BranchID = delivery.BranchID.IsNotNull() ? delivery.BranchID : null;
                            productDeliveryTypeDTO.BranchName = productDeliveryTypeDTO.BranchID.IsNotNull()? sKUBranchDeliveryTypeDTO.BranchName : string.Empty;
                        }
                        else
                        {
                            productDeliveryTypeDTO.DeliveryTypeID = Convert.ToInt16(productTypeDeliveryTypeMap.DeliveryTypeID);
                            productDeliveryTypeDTO.Description = new DistributionRepository().GetDeliveryTypeName(Convert.ToInt16(productTypeDeliveryTypeMap.DeliveryTypeID));
                            productDeliveryTypeDTO.ProductSKUMapID = skuIID;
                            productDeliveryTypeDTO.BranchID = branch;
                            var prod = new ProductDetailRepository().GetProductBySKUID(skuIID);
                            productDeliveryTypeDTO.ProductID = prod.ProductIID;
                        }

                        // Add into DeliveryDetails
                        sKUBranchDeliveryTypeDTO.DeliveryDetails.Add(productDeliveryTypeDTO);
                    }

                    // Add into SKUBranchDeliveryTypeDTOs
                    sKUBranchDeliveryTypeDTOs.Add(sKUBranchDeliveryTypeDTO);
                }


            // if branch IDs does not contain NULL then add "productTypeDeliveryTypeMaps" with NULL branch ID

             if(!branchIds.Any(s => s == null)) 
               {
                    var sKUBranchDeliveryTypeDTO = new SKUBranchDeliveryTypeDTO();

                    sKUBranchDeliveryTypeDTO.BranchID = null;
                    sKUBranchDeliveryTypeDTO.DeliveryDetails = new List<ProductDeliveryTypeDTO>();

                    foreach (var productTypeDeliveryTypeMap in productTypeDeliveryTypeMaps)
                    {
                        // get delivery detail based on branchId
                        var delivery = (from d in deliveryDetails
                                        where d.BranchID == null && d.DeliveryTypeID == productTypeDeliveryTypeMap.DeliveryTypeID
                                        select d).FirstOrDefault();

                        var productDeliveryTypeDTO = new ProductDeliveryTypeDTO();

                        if (delivery.IsNotNull())
                        {
                            productDeliveryTypeDTO.DeliveryTypeID = Convert.ToInt16(delivery.DeliveryTypeID);
                            productDeliveryTypeDTO.Description = new DistributionRepository().GetDeliveryTypeName(Convert.ToInt16(delivery.DeliveryTypeID));
                            productDeliveryTypeDTO.ProductDeliveryTypeMapIID = delivery.ProductDeliveryTypeMapIID.IsNotNull() ? delivery.ProductDeliveryTypeMapIID : productTypeDeliveryTypeMap.ProductTypeDeliveryTypeMapIID;
                            productDeliveryTypeDTO.CompanyID = delivery.CompanyID > 0 ? delivery.CompanyID : _CallContext.CompanyID;
                            productDeliveryTypeDTO.ProductID = delivery.ProductID > 0 ? delivery.ProductID : null;
                            productDeliveryTypeDTO.ProductSKUMapID = delivery.ProductSKUMapID > 0 ? delivery.ProductSKUMapID : null;
                            productDeliveryTypeDTO.DeliveryDays = delivery.DeliveryDays;
                            productDeliveryTypeDTO.DeliveryTypeID = delivery.DeliveryTypeID.IsNotNull() ? delivery.DeliveryTypeID : delivery.DeliveryTypeID;
                            productDeliveryTypeDTO.CartTotalFrom = delivery.CartTotalFrom;
                            productDeliveryTypeDTO.CartTotalTo = delivery.CartTotalTo;
                            productDeliveryTypeDTO.DeliveryCharge = delivery.DeliveryCharge;
                            productDeliveryTypeDTO.IsDeliveryAvailable = delivery.IsSelected;
                            productDeliveryTypeDTO.DeliveryChargePercentage = delivery.DeliveryChargePercentage;
                            productDeliveryTypeDTO.BranchID = null;
                            productDeliveryTypeDTO.BranchName = string.Empty;
                        }
                        else
                        {
                            productDeliveryTypeDTO.DeliveryTypeID = Convert.ToInt16(productTypeDeliveryTypeMap.DeliveryTypeID);
                            productDeliveryTypeDTO.Description = new DistributionRepository().GetDeliveryTypeName(Convert.ToInt16(productTypeDeliveryTypeMap.DeliveryTypeID));
                            productDeliveryTypeDTO.ProductSKUMapID = skuIID;
                            var prod = new ProductDetailRepository().GetProductBySKUID(skuIID);
                            productDeliveryTypeDTO.ProductID = prod.ProductIID;
                        }

                        // Add into DeliveryDetails
                        sKUBranchDeliveryTypeDTO.DeliveryDetails.Add(productDeliveryTypeDTO);
                    }

                    // Add into SKUBranchDeliveryTypeDTOs
                    sKUBranchDeliveryTypeDTOs.Add(sKUBranchDeliveryTypeDTO);
                }


            return sKUBranchDeliveryTypeDTOs;
        }

    }
}
