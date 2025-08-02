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
    public class VouchersMapper : IDTOEntityMapper<VouchersDTO, Voucher>
    {
        private CallContext _context;
        public static VouchersMapper Mapper(CallContext context)
        {
            var mapper = new VouchersMapper();
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
