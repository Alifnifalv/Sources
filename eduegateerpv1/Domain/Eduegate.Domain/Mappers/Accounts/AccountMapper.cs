using System;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Accounting;

namespace Eduegate.Domain.Mappers.Accounts
{
    public class AccountMapper : IDTOEntityMapper<AccountDTO, Account>
    {
        private CallContext _context;

        public static AccountMapper Mapper(CallContext context)
        {
            var mapper = new AccountMapper();
            mapper._context = context;
            return mapper;
        }

        public AccountDTO ToDTO(Account entity)
        {
            if (entity == null) return new AccountDTO();

            return new AccountDTO()
            {
                AccountID = entity.AccountID,
                AccountName = entity.AccountName,
                Alias = entity.Alias,
                AccountCode = entity.AccountCode,
                //ParentAccount = entity.ParentAccountID.HasValue ? new AccountDTO() { AccountID = entity.ParentAccountID.Value, AccountName = entity.Account1.AccountName } : null,
                AccountGroup = entity.Group != null ? new AccountGroupDTO() { AccountGroupID = entity.Group.GroupID, GroupName = entity.Group.GroupName } : null,
                AccountBehavior = entity.AccountBehavoirID.HasValue ? (Eduegate.Services.Contracts.Enums.Accounting.AccountBehavior)Enum.Parse(typeof(Eduegate.Services.Contracts.Enums.Accounting.AccountBehavior), entity.AccountBehavoirID.ToString()) : Services.Contracts.Enums.Accounting.AccountBehavior.Both,
                //TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedBy = entity.UpdatedBy,
                UpdatedDate = entity.UpdatedDate,
            };
        }

        public Account ToEntity(AccountDTO dto)
        {
            return new Account()
            {
                AccountID = dto.AccountID.Value,
                AccountName = dto.AccountName,
                Alias = dto.Alias,
                AccountCode = dto.AccountCode,
                GroupID = dto.AccountGroup.AccountGroupID,
                //ParentAccountID = dto.ParentAccount == null ? null : (long?)dto.ParentAccount.AccountID,
                AccountBehavoirID = (byte)dto.AccountBehavior,
                //TimeStamps = dto.TimeStamps == null ? null : Convert.FromBase64String(dto.TimeStamps),
                CreatedBy = dto.CreatedBy,
                CreatedDate = dto.CreatedDate,
                UpdatedBy = dto.UpdatedBy,
                UpdatedDate = dto.UpdatedDate,
            };
        }
    }
}
