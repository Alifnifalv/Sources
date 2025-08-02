using Newtonsoft.Json;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Repository;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Mappers.Accounts
{
    public class SalesVoucherMapper : DTOEntityDynamicMapper
    {
        public static SalesVoucherMapper Mapper(CallContext context)
        {
            var mapper = new SalesVoucherMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<AccountTransactionHeadDTO>(entity);
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as AccountTransactionHeadDTO;
            var entity = ToEntity(toDto);
            new AccountTransactionRepository().SaveAccountTransactionHead(entity);

            if (ConfigurationExtensions.GetAppConfigValue<TransactionProcessing>("TransactionProcessing") == TransactionProcessing.Immediate)
            {
                new TransactionEngineCore.AccountTransaction(null).StartProcess(0, 0, entity.AccountTransactionHeadIID);
            }

            return GetEntity(entity.AccountTransactionHeadIID);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            dto = dto as AccountTransactionHeadDTO;
            return JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {   
            var head = new AccountTransactionRepository().GetAccountTransactionHeadById(IID);
            var dto = ToDTO(head);
            return ToDTOString(dto);
        }

        public AccountTransactionHeadDTO ToDTO(AccountTransactionHead entity)
        {
            var dto = new AccountTransactionHeadDTO();

            dto.AccountTransactionHeadIID = entity.AccountTransactionHeadIID;
            dto.CurrencyID = entity.CurrencyID;
            dto.AccountID = entity.AccountID;
            dto.DocumentStatusID = entity.DocumentStatusID;
            dto.DocumentStatus = new KeyValueDTO() { Key = entity.DocumentStatus.DocumentStatusID.ToString(), Value = entity.DocumentStatus.StatusName };
            dto.TransactionStatus = new KeyValueDTO() { Key = entity.TransactionStatus.TransactionStatusID.ToString(), Value = entity.TransactionStatus.Description };
            dto.AccountTransactionDetails = new List<AccountTransactionDetailsDTO>();
            dto.Account = entity.Account == null ? new KeyValueDTO() : new KeyValueDTO() { Key = entity.Account.AccountID.ToString(), Value = entity.Account.AccountName };
            dto.Remarks = entity.Remarks;
            dto.Reference = entity.Reference;
            dto.TransactionDate = entity.TransactionDate;
            dto.TransactionNumber = entity.TransactionNumber;
            dto.DiscountAmount = entity.DiscountAmount;

            foreach (var detail in entity.AccountTransactionDetails)
            {
                dto.AccountTransactionDetails.Add(new AccountTransactionDetailsDTO()
                {
                    Description = detail.Remarks,
                    AccountTransactionDetailIID = detail.AccountTransactionDetailIID,
                    Amount = detail.Amount,
                    ReferenceRate = detail.ReferenceRate,
                    ReferenceQuantity = detail.ReferenceQuantity,
                    ExternalReference1 = detail.ExternalReference1,
                    ExternalReference2 = detail.ExternalReference2,
                    ExternalReference3 = detail.ExternalReference3,
                    AccountID = detail.AccountID,
                    Account = detail.Account == null ? new KeyValueDTO() : new KeyValueDTO() { Key = detail.Account.AccountID.ToString(), Value = detail.Account.AccountName },
                    DiscountAmount = detail.DiscountAmount,
                });
            }

            return dto;
        }

        public AccountTransactionHead ToEntity(AccountTransactionHeadDTO dto)
        {
            var entity = new AccountTransactionHead();

            if(dto.AccountTransactionHeadIID != 0)
            {
                entity = new AccountTransactionRepository().GetAccountTransactionHeadById(dto.AccountTransactionHeadIID);
            }

            entity.AccountTransactionHeadIID = dto.AccountTransactionHeadIID;
            entity.CurrencyID = dto.CurrencyID;
            entity.AccountID = dto.AccountID;
            entity.DocumentStatusID = dto.DocumentStatusID;
            entity.AccountTransactionDetails = new List<AccountTransactionDetail>();
            entity.Remarks = dto.Remarks;
            entity.Reference = dto.Reference;
            entity.TransactionNumber = dto.TransactionNumber;
            entity.TransactionStatusID = 1;
            entity.DocumentStatusID = dto.DocumentStatusID;
            entity.DiscountAmount = dto.DiscountAmount;

            foreach (var detail in dto.AccountTransactionDetails)
            {
                entity.AccountTransactionDetails.Add(new AccountTransactionDetail()
                {
                    Remarks = detail.Description,
                    AccountTransactionDetailIID = detail.AccountTransactionDetailIID,
                    Amount = Convert.ToDecimal(detail.Amount),
                    ReferenceRate = detail.ReferenceRate,
                    ReferenceQuantity = detail.ReferenceQuantity,
                    ExternalReference1 = detail.ExternalReference1,
                    ExternalReference2 = detail.ExternalReference2,
                    ExternalReference3 = detail.ExternalReference3,
                    AccountID = detail.AccountID,
                    DiscountAmount = detail.DiscountAmount
                });
            }

            return entity;
        }

        public override KeyValueDTO ValidateField(BaseMasterDTO dto, string field)
        {
            var toDto = dto as AccountTransactionHeadDTO;
            var valueDTO = new KeyValueDTO();
            return valueDTO;
        }
    }
}
