using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Distributions;
using Eduegate.Framework.Extensions;

namespace Eduegate.Domain.Mappers.Distributions
{
    public class ProductDeliveryTypeMapMapper : IDTOEntityMapper<ProductDeliveryTypeDTO, ProductDeliveryTypeMap>
    {
        private CallContext _context;

        public static ProductDeliveryTypeMapMapper Mapper(CallContext context)
        {
            var mapper = new ProductDeliveryTypeMapMapper();
            mapper._context = context;
            return mapper;
        }

        public ProductDeliveryTypeDTO ToDTO(ProductDeliveryTypeMap entity)
        {
            if (entity.IsNotNull())
            {
                ProductDeliveryTypeDTO pdtDTO = new ProductDeliveryTypeDTO();

                pdtDTO.ProductDeliveryTypeMapIID = entity.ProductDeliveryTypeMapIID;
                pdtDTO.ProductID = entity.ProductID;
                pdtDTO.ProductSKUMapID = entity.ProductSKUMapID;
                pdtDTO.DeliveryTypeID = entity.DeliveryTypeID;
                pdtDTO.DeliveryCharge = entity.DeliveryCharge;
                pdtDTO.DeliveryChargePercentage = entity.DeliveryChargePercentage;
                pdtDTO.IsDeliveryAvailable = entity.IsSelected;
                //pdtDTO.CartTotalFrom = entity.CartTotalFrom;
                //pdtDTO.CartTotalTo = entity.CartTotalTo;
                pdtDTO.CreatedBy = entity.CreatedBy;
                pdtDTO.CreatedDate = entity.CreatedDate;
                pdtDTO.UpdatedBy = entity.UpdatedBy;
                pdtDTO.UpdatedDate = entity.UpdatedDate;
                //pdtDTO.TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps);
                pdtDTO.BranchID = entity.BranchID;
                return pdtDTO;
            }
            else
            {
                return new ProductDeliveryTypeDTO();
            }
        }

        public ProductDeliveryTypeMap ToEntity(ProductDeliveryTypeDTO dto)
        {
            if (dto.IsNotNull())
            {
                ProductDeliveryTypeMap entity = new ProductDeliveryTypeMap();

                entity.ProductDeliveryTypeMapIID = dto.ProductDeliveryTypeMapIID;
                entity.ProductID = dto.ProductID;
                entity.ProductSKUMapID = dto.ProductSKUMapID;
                entity.DeliveryTypeID = dto.DeliveryTypeID;
                entity.DeliveryCharge = dto.DeliveryCharge;
                entity.DeliveryChargePercentage = dto.DeliveryChargePercentage;
                entity.IsSelected = dto.IsDeliveryAvailable;
                //entity.CartTotalFrom = dto.CartTotalFrom;
                //entity.CartTotalTo = dto.CartTotalTo;
                entity.CreatedBy = dto.ProductDeliveryTypeMapIID > 0 ? dto.CreatedBy : (int)_context.LoginID;
                entity.UpdatedBy = dto.ProductDeliveryTypeMapIID > 0 ? (int)_context.LoginID : dto.UpdatedBy;
                entity.CreatedDate = dto.ProductDeliveryTypeMapIID > 0 ? dto.CreatedDate : DateTime.Now;
                entity.UpdatedDate = dto.ProductDeliveryTypeMapIID > 0 ? DateTime.Now : dto.UpdatedDate;
                //entity.TimeStamps = dto.TimeStamps.IsNotNull() ? Convert.FromBase64String(dto.TimeStamps) : null;
                entity.BranchID = dto.BranchID;

                return entity;
            }
            else
            {
                return new ProductDeliveryTypeMap();
            }
        }


    }
}
