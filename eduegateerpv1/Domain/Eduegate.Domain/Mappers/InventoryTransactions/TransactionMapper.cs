using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Catalog;

namespace Eduegate.Domain.Mappers.InventoryTransactions
{
    public class TransactionMapper : IDTOEntityMapper<TransactionHeadDTO, TransactionHead>
    {
        private CallContext _context;

        public static TransactionMapper Mapper(CallContext context)
        {
            var mapper = new TransactionMapper();
            mapper._context = context;
            return mapper;
        }

        public TransactionHeadDTO ToDTO(TransactionHead entity)
        {
            throw new NotImplementedException();
        }

        public TransactionHead ToEntity(TransactionHeadDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
