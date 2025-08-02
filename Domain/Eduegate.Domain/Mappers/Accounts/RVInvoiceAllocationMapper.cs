using Newtonsoft.Json;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Repository;
using Eduegate.Framework;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Services.Contracts.Accounts.Accounting;

namespace Eduegate.Domain.Mappers.Accounts
{
    public class RVInvoiceAllocationMapper : DTOEntityDynamicMapper
    {
        List<string> validationFields = new List<string>() { "Amount" };

        public static RVInvoiceAllocationMapper Mapper(CallContext context)
        {
            var mapper = new RVInvoiceAllocationMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<RVInvoiceAllocationDTO>(entity);
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            string errorMessage = "";
            //validate first
            foreach (var field in validationFields)
            {
                var isValid = ValidateField(dto, field);

                if (isValid.Key.Equals("true"))
                {
                    errorMessage = string.Concat(errorMessage, "-", isValid.Value, "<br>");
                }
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                throw new Exception(errorMessage);
            }

            var toDto = dto as RVInvoiceAllocationDTO;
            var entity = ToEntity(toDto);
            new AccountTransactionRepository().SaveReceivables(entity);
            return GetEntity(toDto.CustomerID);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            dto = dto as RVInvoiceAllocationDTO;
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            var head = new AccountTransactionRepository().GetCustomerPendingInvoices(IID);
            var dto = ToDTO(head);
            return ToDTOString(dto);
        }

        public RVInvoiceAllocationDTO ToDTO(List<Receivable> entities)
        {
            var dto = new RVInvoiceAllocationDTO();

            foreach (var detail in entities)
            {
                dto.Receipts.Add(new ReceivableDTO()
                {
                    AccountID = detail.AccountID,
                });
            }

            return dto;
        }

        public List<Receivable> ToEntity(RVInvoiceAllocationDTO dto)
        {
            var receiptIds = new List<long>();

            foreach(var receipt in dto.Receipts)
            {
                receiptIds.Add(receipt.ReceivableIID);
            }

            var allReceipts = new AccountTransactionRepository().GetReceivables(receiptIds, true);
            var newReceipts = new Dictionary<long,decimal>();
            var receiptID = allReceipts.Where(a => a.DocumentType.ReferenceTypeID != 51).FirstOrDefault();

            foreach (var detail in allReceipts)
            {
                var findEntity = dto.Receipts.Where(a => a.ReceivableIID == detail.ReceivableIID).FirstOrDefault();

                //split the receipt
                if(detail.Amount != findEntity.Amount)
                {
                    detail.PaidAmount = findEntity.Amount;
                    newReceipts.Add(detail.ReceivableIID, findEntity.Amount.Value);
                    detail.Amount = findEntity.Amount;
                }
                else
                {
                    detail.PaidAmount = findEntity.Amount;
                }

                if (detail.DocumentType.ReferenceTypeID == 51)
                {
                    if (receiptID != null)
                    {
                        detail.ReferenceReceivablesID = receiptID.ReceivableIID;
                    }

                    detail.PaidAmount = findEntity.Amount;
                }

                //detail.DocumentStatusID = 2;
                detail.Description = detail.Description + ";" + findEntity.Description;
            }

            var newAllReceipts = new AccountTransactionRepository().GetReceivables(newReceipts.Select(a=> a.Key).ToList(), false);

            foreach (var recNew in newAllReceipts)
            {
                var existingEntry = newReceipts.Where(a => a.Key == recNew.ReceivableIID).FirstOrDefault();

                recNew.ReferenceReceivablesID = recNew.ReceivableIID;
                recNew.ReceivableIID = 0;
                recNew.Amount = recNew.Amount - existingEntry.Value;

                if (recNew.Amount < 0)
                {
                    recNew.Amount = -1 * recNew.Amount;
                }

                allReceipts.Add(recNew);
            }

            return allReceipts;
        }

        public override KeyValueDTO ValidateField(BaseMasterDTO dto, string field)
        {
            var toDto = dto as RVInvoiceAllocationDTO;
            var valueDTO = new KeyValueDTO();

            switch (field)
            {
                case "Amount":
                    var allocatedAmount = toDto.Receipts.Where(a => a.DocumentReferenceTypeID != 51).Sum(a=> a.Amount);
                    var totalAmount = toDto.Receipts.Where(a => a.DocumentReferenceTypeID == 51).Sum(a => a.Amount);
                    if (totalAmount > allocatedAmount)
                    {
                        valueDTO.Key = "true";
                        valueDTO.Value = "Allocated amount cannot be more than the receipt amount.";
                    }
                    else
                    {
                        valueDTO.Key = "false";
                    }
                    break;
            }

            return valueDTO;
        }
    }
}
