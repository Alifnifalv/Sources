using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts;

namespace Eduegate.Domain.Mappers
{
    class EntitlementPriceListMapMapper : IDTOEntityMapper<EntitlementPriceListMapDTO, ProductPriceListCustomerMap>
    {
        //public static EntitlementPriceListMapMapper Mapper { get { return new EntitlementPriceListMapMapper(); } }

        private CallContext _context;
        public static EntitlementPriceListMapMapper Mapper(CallContext context)
        {
            var mapper = new EntitlementPriceListMapMapper();
            mapper._context = context;
            return mapper;
        }

        public EntitlementPriceListMapDTO ToDTO(ProductPriceListCustomerMap entity)
        {
            if (entity != null)
            {
                return new EntitlementPriceListMapDTO()
                {
                    ProductPriceListCustomerMapIID = entity.ProductPriceListCustomerMapIID,
                    ProductPriceListIID = entity.ProductPriceListID,
                    CustomerID = entity.CustomerID,
                    EntitlementID = entity.EntitlementID,
                    TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),
                    //TimeStamps =Convert.ToString(entity.TimeStamps),
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedDate = entity.UpdatedDate,
                };
            }
            else {
                return new EntitlementPriceListMapDTO() ;
            }
        }

        public ProductPriceListCustomerMap ToEntity(EntitlementPriceListMapDTO dto)
        {
            if (dto != null)
            {
                return new ProductPriceListCustomerMap()
                {
                    ProductPriceListCustomerMapIID = dto.ProductPriceListCustomerMapIID,
                    ProductPriceListID = dto.ProductPriceListIID,
                    CustomerID = dto.CustomerID,
                    EntitlementID = dto.EntitlementID,
                    CreatedBy = dto.ProductPriceListCustomerMapIID > 0 ? dto.CreatedBy : Convert.ToInt32(_context.LoginID),
                    CreatedDate = dto.ProductPriceListCustomerMapIID > 0 ? dto.CreatedDate : DateTime.Now,
                    UpdatedBy = Convert.ToInt32(_context.LoginID),
                    UpdatedDate = DateTime.Now,
                    TimeStamps = string.IsNullOrEmpty(dto.TimeStamps) ? null : Convert.FromBase64String(dto.TimeStamps),
                };
            }
            else
            {
                return new ProductPriceListCustomerMap();
            }
        }
    }
}

