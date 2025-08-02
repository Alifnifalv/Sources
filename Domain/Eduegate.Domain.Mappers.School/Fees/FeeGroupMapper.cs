using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Fees;
using Eduegate.Framework;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Eduegate.Domain.Mappers.School.Fees
{
    public class FeeGroupMapper : DTOEntityDynamicMapper
    {
        public static FeeGroupMapper Mapper(CallContext context)
        {
            var mapper = new FeeGroupMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<FeeGroupDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.FeeGroups.Where(x => x.FeeGroupID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return ToDTOString(new FeeGroupDTO()
                {
                    FeeGroupID = entity.FeeGroupID,
                    Description = entity.Description,
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    UpdatedBy = entity.UpdatedBy
                });
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as FeeGroupDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new FeeGroup()
            {
                FeeGroupID = toDto.FeeGroupID,
                Description = toDto.Description,
                CreatedBy = toDto.FeeGroupID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.FeeGroupID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.FeeGroupID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.FeeGroupID > 0 ? DateTime.Now : dto.UpdatedDate,

            };

            using (var dbContext = new dbEduegateSchoolContext())
            {               
                if (entity.FeeGroupID == 0)
                {                   
                    var maxGroupID = dbContext.FeeGroups.Max(a => (int?)a.FeeGroupID);
                    entity.FeeGroupID = maxGroupID == null ? 1 : int.Parse(maxGroupID.ToString()) + 1;
                    dbContext.FeeGroups.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.FeeGroups.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return ToDTOString(new FeeGroupDTO()
            {
                FeeGroupID = entity.FeeGroupID,
                Description = entity.Description,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate,
                UpdatedBy = entity.UpdatedBy

            });
        }
    }
}