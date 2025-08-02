using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.School.Exams;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace Eduegate.Domain.Mappers.School.Exams
{
    public class SkillMasterMapper : DTOEntityDynamicMapper
    {
        public static SkillMasterMapper Mapper(CallContext context)
        {
            var mapper = new SkillMasterMapper();
            mapper._context = context;
            return mapper;
        }
        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<SkillMasterDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }
        public SkillMasterDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.SkillMasters.Where(x => x.SkillMasterID == IID)
                    .Include(i => i.SkillGroupMaster)
                    .AsNoTracking()
                    .FirstOrDefault();

                return new SkillMasterDTO()
                {
                    SkillMasterID = entity.SkillMasterID,
                    SkillName = entity.SkillName,
                    SkillGroupMasterID = entity.SkillGroupMasterID,
                    SkillGroup = entity.SkillGroupMasterID.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.SkillGroupMasterID.ToString(),
                        Value = entity.SkillGroupMaster.SkillGroup,
                    } : new KeyValueDTO(),
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    //TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),
                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as SkillMasterDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new SkillMaster()
            {
                SkillMasterID = toDto.SkillMasterID,
                SkillName = toDto.SkillName,
                SkillGroupMasterID = toDto.SkillGroupMasterID,
                CreatedBy = toDto.SkillMasterID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.SkillMasterID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.SkillMasterID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.SkillMasterID > 0 ? DateTime.Now : dto.UpdatedDate,
                //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
                dbContext.SkillMasters.Add(entity);
                if (entity.SkillMasterID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.SkillMasterID));
        }
    }
}

