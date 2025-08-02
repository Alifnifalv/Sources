using Newtonsoft.Json;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Repository;
using Eduegate.Framework;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;

namespace Eduegate.Domain.Mappers.Accounts
{
    public class PVInvoiceAllocationMapper : DTOEntityDynamicMapper
    {
        public static PVInvoiceAllocationMapper Mapper(CallContext context)
        {
            var mapper = new PVInvoiceAllocationMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<PVInvoiceAllocationDTO>(entity);
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as PVInvoiceAllocationDTO;
            var entity = ToEntity(toDto);
            new AccountTransactionRepository().SavePayables(entity);
            return GetEntity(toDto.SupplierID);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            dto = dto as PVInvoiceAllocationDTO;
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            var head = new AccountTransactionRepository().GetSupplierPendingInvoices(IID);
            var dto = ToDTO(head);
            return ToDTOString(dto);
        }

        public PVInvoiceAllocationDTO ToDTO(List<Payable> entiies)
        {
            var dto = new PVInvoiceAllocationDTO();

            foreach (var detail in entiies)
            {
                dto.Payments.Add(new PayableDTO()
                {
                    Amount = detail.Amount,
                    AccountID = detail.AccountID,
                    Account = detail.Account == null ? new KeyValueDTO() : new KeyValueDTO() { Key = detail.Account.AccountID.ToString(), Value = detail.Account.AccountName },
                });
            }

            return dto;
        }

        public List<Payable> ToEntity(PVInvoiceAllocationDTO dto)
        {
            var payables = new List<Payable>();

            foreach (var detail in dto.Payments)
            {
                payables.Add(new Payable()
                {
                     
                });
            }

            return payables;
        }

        public override KeyValueDTO ValidateField(BaseMasterDTO dto, string field)
        {
            var toDto = dto as PVInvoiceAllocationDTO;
            var valueDTO = new KeyValueDTO();
            return valueDTO;
        }
    }
}
