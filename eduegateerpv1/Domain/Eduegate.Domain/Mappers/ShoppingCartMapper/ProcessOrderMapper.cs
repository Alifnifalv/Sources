using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Entity.Models.ValueObjects;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.ShoppingCart;

namespace Eduegate.Domain.Mappers.ShoppingCartMapper
{
    public class ProcessOrderMapper : IDTOEntityMapper<ProcessOrderDTO, ShoppingCart>
    {
        private CallContext _context;

        public static ProcessOrderMapper Mapper(CallContext context)
        {
            var mapper = new ProcessOrderMapper();
            mapper._context = context;
            return mapper;
        }

        public ProcessOrderDTO ToDTO(ShoppingCart entity)
        {
            return new ProcessOrderDTO()
            {
                CartID = Convert.ToInt64(entity.CartID),
                CartStatus = entity.CartStatusID.HasValue?entity.CartStatusID.Value:0,
                Description = entity.Description,
                PaymentMethod = entity.PaymentMethod,
                ShoppingCartIID = entity.ShoppingCartIID,
                PaymentgateWayID = entity.PaymentGateWayID.HasValue ? entity.PaymentGateWayID.Value : (short)0
            };
        }
        public ShoppingCart ToEntity(ProcessOrderDTO dto)
        {
            return new ShoppingCart()
            {
                CartID = dto.CartID.ToString(),
                CartStatusID = dto.CartStatus,
                Description = dto.Description,
                PaymentMethod = dto.PaymentMethod,
                ShoppingCartIID = dto.ShoppingCartIID,
                PaymentGateWayID = dto.PaymentgateWayID
            };
        }
    }
}
