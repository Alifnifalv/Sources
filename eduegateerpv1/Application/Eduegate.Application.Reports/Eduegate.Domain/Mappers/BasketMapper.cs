using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Services.Contracts.Warehouses;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Interfaces;

namespace Eduegate.Domain.Mappers
{
    public class BasketMapper : IDTOEntityMapper<BasketDTO, Basket>
    {
        private CallContext _context;

        public static BasketMapper Mapper(CallContext context)
        {
            var mapper = new BasketMapper();
            mapper._context = context;
            return mapper;
        }

        public Basket ToEntity(BasketDTO dto) 
        {
            return new Basket()
            {
                BasketID = (int)dto.BasketID,
                BasketCode = dto.BasketCode,
                Description = dto.Description,
                Barcode = dto.BarCode,
                CreatedBy = !dto.CreatedBy.HasValue ? int.Parse(_context.LoginID.ToString()) : dto.CreatedBy,
                CreatedDate = !dto.CreatedDate.HasValue ? DateTime.Now : dto.CreatedDate,
                UpdatedBy = int.Parse(_context.LoginID.ToString()),
                UpdatedDate = DateTime.Now,
                TimeStamps = dto.TimeStamps == null ? null : Convert.FromBase64String(dto.TimeStamps),
                CompanyID = dto.CompanyID.IsNotNull() ? dto.CompanyID : _context.CompanyID
            };
        }

        public BasketDTO ToDTO(Basket entity)
        {
            return new BasketDTO()
            {
                BasketID = entity.BasketID,
                BasketCode = entity.BasketCode,
                Description = entity.Description,
                BarCode = entity.Barcode,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedBy = entity.UpdatedBy,
                UpdatedDate= entity.UpdatedDate,
                TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),
                CompanyID = entity.CompanyID.IsNotNull()? entity.CompanyID : _context.CompanyID
            };
        }
    }
}
