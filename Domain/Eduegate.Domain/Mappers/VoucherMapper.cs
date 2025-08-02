using System;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts;

namespace Eduegate.Domain.Mappers
{
    public class VoucherMapper : IDTOEntityMapper<VouchersDTO, Voucher>
    {
        private CallContext _context;

        public static VoucherMapper Mapper(CallContext context)
        {
            var mapper = new VoucherMapper();
            mapper._context = context;
            return mapper;
        }

        public VouchersDTO ToDTO(Voucher entity)
        {
            if (entity != null)
            {
                return new VouchersDTO()
                {
                    VoucherIID = entity.VoucherIID,
                    VoucherNo = entity.VoucherNo,
                    VoucherPin = entity.VoucherPin,
                    VoucherTypeID = entity.VoucherTypeID,
                    IsSharable = entity.IsSharable,
                    CustomerID = entity.CustomerID,
                    Amount = entity.Amount,
                    ExpiryDate = entity.ExpiryDate,
                    MinimumAmount = entity.MinimumAmount,
                    CurrentBalance = entity.CurrentBalance,
                    Description = entity.Description,
                    StatusID = entity.StatusID,
                };
            }
            else return new VouchersDTO();
        }

        public Voucher ToEntity(VouchersDTO dto)
        {
            throw new NotImplementedException();
        }

    }
}
