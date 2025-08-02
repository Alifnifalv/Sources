using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models.ValueObjects;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.ShoppingCart;

namespace Eduegate.Domain.Mappers.ShoppingCartMapper
{
    public class CartPaymentDetailsMapper : IDTOEntityMapper<CartPaymentDetailsDTO, CartPaymentGateWayDetails>
    {
        private CallContext _context;

        public static CartPaymentDetailsMapper Mapper(CallContext context)
        {
            var mapper = new CartPaymentDetailsMapper();
            mapper._context = context;
            return mapper;
        }

        public CartPaymentDetailsDTO ToDTO(CartPaymentGateWayDetails entity)
        {
            return new CartPaymentDetailsDTO()
            {
               PaymentGateWayID = entity.PaymentGateWayID,
               CartIID = entity.CartIID,
               CartStatusID = entity.CartStatusID,
               TrackKey = entity.TrackKey,
               PaymentGateWay = entity.PaymentGateWay
            };
        }
        public CartPaymentGateWayDetails ToEntity(CartPaymentDetailsDTO dto)
        {
            return null;
        }
    }
}
