using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework;
using Eduegate.Services.Contracts.School.Accounts;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.School.Accounts
{
    public class AccountGroupMapper : DTOEntityDynamicMapper
    {
        public static AccountGroupMapper Mapper(CallContext context)
        {
            var mapper = new AccountGroupMapper();
            mapper._context = context;
            return mapper;
        }
        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<AccountsGroupDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private AccountsGroupDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.Groups.Where(a => a.GroupID == IID)
                    .Include(i => i.Accounts)
                    .AsNoTracking()
                    .FirstOrDefault();

                var parentGroup = dbContext.Groups.AsNoTracking().FirstOrDefault(g => g.GroupID == entity.Parent_ID);

                return new AccountsGroupDTO()
                {
                    GroupID = entity.GroupID,
                    GroupName = entity.GroupName,
                    Parent_ID = entity.Parent_ID,
                    GroupCode = entity.GroupCode,
                    ParentGroup = entity.Parent_ID.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.Parent_ID.ToString(),
                        Value = parentGroup.GroupName,
                    } : new KeyValueDTO(),
                    Affect_ID = entity.Affect_ID,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    //TimeStamps = Convert.ToBase64String(entity.TimeStamps),
                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as AccountsGroupDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new Group()
            {
                GroupID = toDto.GroupID,
                GroupName = toDto.GroupName,
                Parent_ID = toDto.Parent_ID,
                Affect_ID = 0,//toDto.Affect_ID,
                GroupCode = toDto.GroupCode,
                CreatedBy = toDto.GroupID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.GroupID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.GroupID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.GroupID > 0 ? DateTime.Now : dto.UpdatedDate,
                //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
                dbContext.Groups.Add(entity);
                if (entity.GroupID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.GroupID));
        }

        public AccountsGroupDTO GetGroupCodeByParentGroup(long parentGroupID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var dtos = new AccountsGroupDTO();

                //Get Application details
                var emp = dbContext.Groups.FromSqlRaw($@"SELECT  GroupID, account.FNS_ACCOUNT_GROUP_CODE(GroupID) AS GroupCode, GroupName, Parent_ID, Affect_ID, Default_Side, CreatedBy, UpdatedBy, CreatedDate, UpdatedDate, TimeStamps, CompanyID, IsSystemDefined, AllowUserDelete, AllowUserEdit, 
                         AllowAddSubGroup, AllowUserRename, IsHidden FROM  account.Groups WHERE GroupID={parentGroupID}")
                    .AsNoTracking().FirstOrDefault();

                if (emp != null)
                {
                    dtos = new AccountsGroupDTO()
                    {
                        GroupID = emp.GroupID,
                        GroupCode = emp.GroupCode,
                    };
                }

                return dtos;
            }
        }

    }
}