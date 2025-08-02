using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Checkout;

namespace Eduegate.Domain.Mappers.Payments
{
    public class PaymentMethodMapper : IDTOEntityMapper<PaymentMethodDTO, PaymentMethod>
    {
        private CallContext _context;

        public static PaymentMethodMapper Mapper(CallContext context)
        {
            var mapper = new PaymentMethodMapper();
            mapper._context = context;
            return mapper;
        }

        public List<PaymentMethodDTO> ToDTO(List<PaymentMethod> entities)
        {
            var dtos = new List<PaymentMethodDTO>();

            foreach (var entity in entities)
            {
                dtos.Add(new PaymentMethodDTO() { PaymentMethodID = entity.PaymentMethodID, PaymentMethodName = entity.PaymentMethod1, ImageName = entity.ImageName, Description = entity.Description });
            }

            return dtos;
        }

        public PaymentMethodDTO ToDTO(PaymentMethod entity)
        {
            return entity == null ? null : new PaymentMethodDTO
            {
                PaymentMethodID = entity.PaymentMethodID,
                PaymentMethodName = entity.PaymentMethod1,
                ImageName = entity.ImageName,
                Description = entity.Description,
            };
        }

        public PaymentMethod ToEntity(PaymentMethodDTO dto)
        {
            return dto == null ? null : new PaymentMethod
           {
               PaymentMethodID = dto.PaymentMethodID,
               PaymentMethod1 = dto.PaymentMethodName,
               ImageName = dto.ImageName
           };
        }
    }
}
