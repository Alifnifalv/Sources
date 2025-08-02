using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Services.Contracts.Accounts.Accounting;

namespace Eduegate.Domain.Mappers.Accounts
{
    public class GroupMapper : IDTOEntityMapper<AccountGroupDTO, Group>
    {
        private CallContext _context;

        public static GroupMapper Mapper(CallContext context)
        {
            var mapper = new GroupMapper();
            mapper._context = context;
            return mapper;
        }

        public AccountGroupDTO ToDTO(Group entity)
        {
            return new AccountGroupDTO(){
                 AccountGroupID = entity.GroupID,
                  GroupName = entity.GroupName,
            };
        }

        public Group ToEntity(AccountGroupDTO dto)
        {
            return new Group()
            {
                GroupID = dto.AccountGroupID.Value,
                GroupName = dto.GroupName,
            };
        }
    }
}
