using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Extensions;

namespace Eduegate.Domain.Mappers
{
    public class TransactionHeadEntitlementMapMapper : IDTOEntityMapper<TransactionHeadEntitlementMapDTO, TransactionHeadEntitlementMap>
    {
        private CallContext _context;

        public static TransactionHeadEntitlementMapMapper Mapper(CallContext context)
        {
            var mapper = new TransactionHeadEntitlementMapMapper();
            mapper._context = context;
            return mapper;
        }

        public List<TransactionHeadEntitlementMapDTO> ToDTO(List<TransactionHeadEntitlementMap> entities)
        {
            var maps = new List<TransactionHeadEntitlementMapDTO>();

            foreach (var entity in entities)
            {
                maps.Add(ToDTO(entity));
            }

            return maps;
        }

        public TransactionHeadEntitlementMapDTO ToDTO(TransactionHeadEntitlementMap entity)
        {
            if (entity != null)
            {
                return new TransactionHeadEntitlementMapDTO()
                {
                    TransactionHeadEntitlementMapID = entity.TransactionHeadEntitlementMapIID,
                    TransactionHeadID = entity.TransactionHeadID,
                    EntitlementID = entity.EntitlementID,
                    Amount = entity.Amount,
                    ReferenceNo= entity.ReferenceNo,
                    EntitlementName = entity.EntityTypeEntitlement.IsNotNull() ? entity.EntityTypeEntitlement.EntitlementName : string.Empty,
                    PaymentTrackID = entity.PaymentTrackID,
                    PaymentTransactionNumber = entity.PaymentTransactionNumber,
                };
            }
            else return new TransactionHeadEntitlementMapDTO();
        }

        public TransactionHeadEntitlementMap ToEntity(TransactionHeadEntitlementMapDTO dto)
        {
            if (dto != null)
            {
                return new TransactionHeadEntitlementMap()
                {
                    TransactionHeadEntitlementMapIID = dto.TransactionHeadEntitlementMapID,
                    TransactionHeadID = dto.TransactionHeadID,
                    EntitlementID = dto.EntitlementID,
                    ReferenceNo = dto.ReferenceNo,
                    Amount = dto.Amount,
                    PaymentTrackID= dto.PaymentTrackID,
                    PaymentTransactionNumber = dto.PaymentTransactionNumber,
                };
            }
            else return new TransactionHeadEntitlementMap();
        }

    }
}
