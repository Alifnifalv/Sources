using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework;

namespace Eduegate.Domain.Mappers
{
    public class CustomerProductReferenceMapper : IDTOEntityMapper<CustomerProductReferenceDTO, CustomerProductReference>
    {
        private CallContext _context;

        public static CustomerProductReferenceMapper Mapper(CallContext context)
        {
            var mapper = new CustomerProductReferenceMapper();
            mapper._context = context;
            return mapper;
        }


        public CustomerProductReferenceDTO ToDTO(CustomerProductReference entity)
        {
            if (entity != null)
            {
                return new CustomerProductReferenceDTO()
                {
                    CustomerProductReferenceID = entity.CustomerProductReferenceIID,
                    CustomerID = entity.CustomerID,
                    ProductSKUMapID = entity.ProductSKUMapID,
                    BarCode = entity.BarCode,
                    //TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),
                };
            }
            else return new CustomerProductReferenceDTO();
        }

        public CustomerProductReference ToEntity(CustomerProductReferenceDTO dto)
        {
            if (dto != null)
            {
                return new CustomerProductReference()
                {
                    CustomerProductReferenceIID = dto.CustomerProductReferenceID,
                    CustomerID = dto.CustomerID,
                    ProductSKUMapID = dto.ProductSKUMapID,
                    BarCode = dto.BarCode,
                    UpdatedDate = DateTime.Now,
                    UpdatedBy = (int)_context.LoginID,
                    CreatedDate = dto.CustomerProductReferenceID == 0 ? DateTime.Now : dto.CreatedDate,
                    CreatedBy = dto.CustomerProductReferenceID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                    //TimeStamps = dto.TimeStamps == null ? null : Convert.FromBase64String(dto.TimeStamps),
                    
                };
            }
            else return new CustomerProductReference();
        }
    }
}
