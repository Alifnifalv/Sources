using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Accounting;
using System;

namespace Eduegate.Domain.Mappers.Accounts
{
    public class ReceivableMapper : IDTOEntityMapper<ReceivableDTO, Receivable>
    {
        private CallContext _context;

        public static ReceivableMapper Mapper(CallContext context)
        {
            var mapper = new ReceivableMapper();
            mapper._context = context;
            return mapper;
        }

        public ReceivableDTO ToDTO(Receivable entity)
        {
            return new ReceivableDTO()
            {
                Account = new Eduegate.Framework.Contracts.Common.KeyValueDTO() { Key = entity.Account.AccountID.ToString(), Value = entity.Account.AccountName },
                AccountID = entity.AccountID,
                AccountPostingDate = entity.AccountPostingDate,
                Amount = entity.Amount,
                CurrencyID = entity.CurrencyID,
                CurrencyName = entity.Currency != null ? entity.Currency.Name : string.Empty,
                Description = entity.Description,
                InvoiceAmount = entity.Amount,
                PaidAmount = entity.PaidAmount,
                TransactionDate = entity.TransactionDate,
                SerialNumber = entity.SerialNumber,
                ReceivableIID = entity.ReceivableIID,
                DocumentStatusID = entity.DocumentStatusID,
                DocumentTypeID = entity.DocumentTypeID,
                DocumentTypeName = entity.DocumentType != null ? entity.DocumentType.TransactionTypeName : string.Empty,
                TransactionNumber = entity.TransactionNumber,
                InvoiceNumber = entity.TransactionNumber,
                DocumentReferenceTypeID = entity.DocumentType.ReferenceTypeID,
                DebitOrCredit = entity.DebitOrCredit,
                DiscountAmount = entity.DiscountAmount,
            };
        }

        public Receivable ToEntity(ReceivableDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
