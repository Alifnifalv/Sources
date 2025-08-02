using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Mappers.Accounts
{
    public class PayableMapper : IDTOEntityMapper<PayableDTO, Payable>
    {
        private CallContext _context;

        public static PayableMapper Mapper(CallContext context)
        {
            var mapper = new PayableMapper();
            mapper._context = context;
            return mapper;
        }

        public PayableDTO ToDTO(Payable entity)
        {
            return new PayableDTO()
            {
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
                PayableIID = entity.PayableIID,
                DocumentStatusID = entity.DocumentStatusID,
                DocumentTypeID = entity.DocumentTypeID,
                DocumentTypeName = entity.DocumentType != null ? entity.DocumentType.TransactionTypeName : string.Empty,
                TransactionNumber = entity.TransactionNumber,
                InvoiceNumber = entity.TransactionNumber,
                DebitOrCredit = entity.DebitOrCredit,
                DiscountAmount = entity.DiscountAmount,
            };
        }

        public Payable ToEntity(PayableDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
