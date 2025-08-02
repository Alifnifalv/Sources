using Eduegate.Domain.Entity.Models.Inventory;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Accounts.Taxes;
using System.Collections.Generic;

namespace Eduegate.Domain.Mappers.InventoryTransactions
{
    public class TaxTransactionMapper : IDTOEntityMapper<TaxDetailsDTO, TaxTransaction>
    {
        private CallContext _context;

        public static TaxTransactionMapper Mapper(CallContext context)
        {
            var mapper = new TaxTransactionMapper();
            mapper._context = context;
            return mapper;
        }

        public TaxDetailsDTO ToDTO(TaxTransaction entity)
        {
            return new TaxDetailsDTO()
            {
                AccountID = entity.AccoundID,
                Amount = entity.Amount,
                Percentage = entity.Percentage,
                TaxTypeID = entity.TaxTypeID,
                TaxID = entity.TaxTransactionIID,
                TaxName = entity.TaxType == null ? null : entity.TaxType.Description,
                TaxTemplateID = entity.TaxTemplateID,
                TaxTemplateItemID = entity.TaxTemplateItemID,
            };
        }

        public List<TaxTransaction> ToEntity(List<TaxDetailsDTO> dtos, long headID)
        {
            var entities = new List<TaxTransaction>();

            foreach (var dto in dtos)
            {
                var taxEntity = ToEntity(dto);
                taxEntity.HeadID = headID;
                entities.Add(taxEntity);
            }

            return entities;
        }

        public TaxTransaction ToEntity(TaxDetailsDTO dto)
        {
            return new TaxTransaction()
            {
                AccoundID = dto.AccountID,
                Amount = dto.Amount,
                Percentage = dto.Percentage,
                TaxTemplateID = dto.TaxTemplateID,
                TaxTemplateItemID = dto.TaxTemplateItemID,
                TaxTypeID = dto.TaxTypeID,
                TaxTransactionIID = dto.TaxID,
                ExclusiveAmount = dto.ExclusiveTaxAmount,
                InclusiveAmount = dto.InclusiveTaxAmount,
                HasTaxInclusive = dto.HasTaxInclusive,
            };
        }
    }
}
