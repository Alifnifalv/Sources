using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts;

namespace Eduegate.Domain.Mappers
{
    public class WalletCustomerLogMapper : IDTOEntityMapper<WalletCustomerLogDTO, WalletCustomerLog>
    {
        public static WalletCustomerLogMapper Mapper()
        {
            var mapper = new WalletCustomerLogMapper();
            return mapper;
        }

        public WalletCustomerLogDTO ToDTO(WalletCustomerLog entity)
        {
            if (entity != null)
            {
                return new WalletCustomerLogDTO()
                {
                    LogId = entity.LogId,
                    CustomerId = entity.CustomerId,
                    CreatedDateTime = entity.CreatedDateTime,
                    Guid = entity.Guid,
                };
            }
            else
                return new WalletCustomerLogDTO();
        }

        public WalletCustomerLog ToEntity(WalletCustomerLogDTO dto)
        {
            if (dto != null)
            {
                return new WalletCustomerLog()
                {
                    LogId = dto.LogId,
                    CustomerId =Convert.ToInt64(dto.CustomerId),
                    CreatedDateTime = (dto.LogId.IsNull() || dto.LogId == 0) ? DateTime.Now : Convert.ToDateTime(dto.CreatedDateTime),
                    Guid = dto.Guid,
                };
            }
            else
                return new WalletCustomerLog();
        }
    }
}
