using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Repository.Payroll;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Catalog;

namespace Eduegate.Domain.Mappers.Catalog
{
    public class SKUMapper : IDTOEntityMapper<SKUDTO, ProductSKUMap>
    {
        private CallContext _context;
        public static SKUMapper Mapper(CallContext context)
        {
            var mapper = new SKUMapper();
            mapper._context = context;
            return mapper;
        }
        public SKUDTO ToDTO(ProductSKUMap entity)
        {
            var skuDTO = new SKUDTO();
            //skuDTO.ProductInventoryConfigDTOs = new ProductInventoryConfigDTO();
            skuDTO.ProductInventorySKUConfigMaps = new List<ProductInventorySKUConfigMapDTO>();

            skuDTO.ProductID = Convert.ToInt64(entity.ProductID);
            skuDTO.ProductSKUCode = entity.ProductSKUCode;
            skuDTO.Sequence = entity.Sequence;
            skuDTO.ProductPrice = entity.ProductPrice;
            skuDTO.PartNumber = entity.PartNo;
            skuDTO.BarCode = entity.BarCode;
            skuDTO.ProductSKUMapID = entity.ProductSKUMapIID;
            skuDTO.StatusID = entity.StatusID;
            skuDTO.IsHiddenFromList =  Convert.ToBoolean(entity.IsHiddenFromList) ;
            skuDTO.HideSKU = Convert.ToBoolean(entity.HideSKU);
            skuDTO.SkuName = entity.SKUName;
            skuDTO.VariantsMap = entity.VariantsMap;
            skuDTO.CreatedBy = entity.CreatedBy;
            skuDTO.UpdatedBy = entity.UpdatedBy;
            skuDTO.CreatedDate = entity.CreatedDate;
            skuDTO.UpdatedDate = entity.UpdatedDate;
            skuDTO.SeoMetadataID = entity.SeoMetadataID;
            //skuDTO.//TimeStamps = Convert.ToBase64String(entity.TimeStamps);
            skuDTO.CreatedDate = entity.CreatedDate;
            skuDTO.CreatedBy = entity.CreatedBy;
            skuDTO.UpdatedDate = entity.UpdatedDate;
            skuDTO.UpdatedBy = entity.UpdatedBy;
            //skuDTO.ImageMaps = entity.;
            //skuDTO.ProductInventoryConfigDTOs = entity.;
            //skuDTO.ProductVideoMaps = entity.;
            //skuDTO.Properties = entity.;
            if (entity.ProductInventorySKUConfigMaps.IsNotNull() && entity.ProductInventorySKUConfigMaps.Count > 0)
            {
                foreach (var skuconfigMap in entity.ProductInventorySKUConfigMaps)
                {
                    
                    var skuConfigMapDTO = new ProductInventorySKUConfigMapDTO()
                    {
                        ProductInventoryConfigID = skuconfigMap.ProductInventoryConfigID,
                        ProductSKUMapID = skuconfigMap.ProductSKUMapID,
                        CreatedBy = skuconfigMap.CreatedBy,
                        CreatedDate = skuconfigMap.CreatedDate,
                        UpdatedBy = skuconfigMap.UpdatedBy,
                        UpdatedDate = skuconfigMap.UpdatedDate,
                        //TimeStamps = Convert.ToBase64String(skuconfigMap.TimeStamps),

                    };

                    if (skuconfigMap.ProductInventoryConfig.IsNotNull())
                    {
                        skuConfigMapDTO.ProductInventoryConfig = new ProductInventoryConfigDTO();
                        skuConfigMapDTO.ProductInventoryConfig = ProductInventoryConfigMapper.ToDto(skuconfigMap.ProductInventoryConfig);
                        if (skuconfigMap.ProductInventoryConfig.EmployeeID.HasValue && skuconfigMap.ProductInventoryConfig.EmployeeID.Value > 0)
                        { 
                            skuConfigMapDTO.ProductInventoryConfig.EmployeeName = new EmployeeRepository().GetEmployeeName(skuconfigMap.ProductInventoryConfig.EmployeeID.Value);
                        }
                         
                    }
                    skuDTO.ProductInventorySKUConfigMaps.Add(skuConfigMapDTO);

                }
            }


            return skuDTO;

        }

        public ProductSKUMap ToEntity(SKUDTO dto)
        {
            var entity = new ProductSKUMap();
            //skuDTO.ProductInventoryConfigDTOs = new ProductInventoryConfigDTO();

            entity.ProductID = Convert.ToInt64(dto.ProductID);
            entity.ProductSKUCode = dto.ProductSKUCode;
            entity.Sequence = dto.Sequence;
            entity.ProductPrice = dto.ProductPrice;
            entity.PartNo = dto.PartNumber;
            entity.BarCode = dto.BarCode;
            entity.ProductSKUMapIID = dto.ProductSKUMapID;
            entity.StatusID = dto.StatusID;
            entity.IsHiddenFromList = Convert.ToBoolean(dto.IsHiddenFromList);
            entity.HideSKU = Convert.ToBoolean(dto.HideSKU);
            entity.SKUName = dto.SKU;
            entity.VariantsMap = dto.VariantsMap;
            entity.CreatedBy = dto.CreatedBy;
            entity.UpdatedBy = dto.UpdatedBy;
            entity.CreatedDate = dto.CreatedDate;
            entity.UpdatedDate = dto.UpdatedDate;
            entity.SeoMetadataID = dto.SeoMetadataID;
            //entity.TimeStamps = Convert.FromBase64String(dto.TimeStamps);
            entity.CreatedDate = dto.CreatedDate;
            entity.CreatedBy = dto.CreatedBy;
            entity.UpdatedDate = DateTime.Now;
            entity.UpdatedBy = dto.UpdatedBy;

             
            //entity.ImageMaps = dto.;
            //entity.ProductInventoryConfigDTOs = dto.;
            //entity.ProductVideoMaps = dto.;
            //entity.Properties = dto.;
            return entity;
        }

        public ProductSKUMap FromDTOToEntity(long productID,long skuID)
        { 
            var entity = new ProductSKUMap();
            entity.ProductID = productID;
            entity.UpdatedDate = DateTime.Now;
            entity.UpdatedBy = (int)_context.LoginID;
            entity.ProductSKUMapIID = skuID;
            return entity;
        }
    }
}
