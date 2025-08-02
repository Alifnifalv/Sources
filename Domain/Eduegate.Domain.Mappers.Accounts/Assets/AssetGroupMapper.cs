using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using System.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Eduegate.Services.Contracts.Accounts.Assets;
using Eduegate.Domain.Entity.Accounts;
using System;
using Eduegate.Domain.Entity.Accounts.Models.Assets;

namespace Eduegate.Domain.Mappers.Accounts.Assets
{
    public class AssetGroupMapper : DTOEntityDynamicMapper
    {
        public static AssetGroupMapper Mapper(CallContext context)
        {
            var mapper = new AssetGroupMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<AssetGroupDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private AssetGroupDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateAccountsContext())
            {
                var entity = dbContext.AssetGroups.Where(x => x.AssetGroupID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return ToDTO(entity);
            }
        }

        private AssetGroupDTO ToDTO(AssetGroup entity)
        {
            var groupDTO = new AssetGroupDTO()
            {
                AssetGroupID = entity.AssetGroupID,
                AssetGroupName = entity.AssetGroupName,
                IsActive = entity.IsActive,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedBy = entity.UpdatedBy,
                UpdatedDate = entity.UpdatedDate,
            };

            return groupDTO;
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as AssetGroupDTO;

            using (var dbContext = new dbEduegateAccountsContext())
            {
                var entity = new AssetGroup()
                {
                    AssetGroupID = toDto.AssetGroupID,
                    AssetGroupName = toDto.AssetGroupName,
                    IsActive = toDto.IsActive,
                    CreatedBy = toDto.AssetGroupID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                    UpdatedBy = toDto.AssetGroupID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                    CreatedDate = toDto.AssetGroupID == 0 ? DateTime.Now : dto.CreatedDate,
                    UpdatedDate = toDto.AssetGroupID > 0 ? DateTime.Now : dto.UpdatedDate,
                };

                dbContext.AssetGroups.Add(entity);
                if (entity.AssetGroupID == 0)
                {
                    var maxGroupID = dbContext.AssetGroups.Max(a => (int?)a.AssetGroupID);
                    entity.AssetGroupID = maxGroupID == null ? 1 : int.Parse(maxGroupID.ToString()) + 1;

                    dbContext.Entry(entity).State = EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = EntityState.Modified;
                }
                dbContext.SaveChanges();

                return ToDTOString(ToDTO(entity.AssetGroupID));
            }
        }

    }
}