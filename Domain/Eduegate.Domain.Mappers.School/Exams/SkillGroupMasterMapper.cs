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
    public class SkillGroupMasterMapper : DTOEntityDynamicMapper
    {
        public static SkillGroupMasterMapper Mapper(CallContext context)
        {
            var mapper = new SkillGroupMasterMapper();
            mapper._context = context;
            return mapper;
        }
        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<SkillGroupMasterDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }
        public SkillGroupMasterDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.SkillGroupMasters.Where(a => a.SkillGroupMasterID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return new SkillGroupMasterDTO()
                {
                    SkillGroupMasterID = entity.SkillGroupMasterID,
                    SkillGroup = entity.SkillGroup,
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
            var toDto = dto as SkillGroupMasterDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new SkillGroupMaster()
            {
                SkillGroupMasterID = toDto.SkillGroupMasterID,
                SkillGroup = toDto.SkillGroup,
                CreatedBy = toDto.SkillGroupMasterID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.SkillGroupMasterID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.SkillGroupMasterID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.SkillGroupMasterID > 0 ? DateTime.Now : dto.UpdatedDate,
                //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
               
                if (entity.SkillGroupMasterID == 0)
                {
                    var maxGroupID = dbContext.SkillGroupMasters.Max(a => (long?)a.SkillGroupMasterID);
                    //entity.RackID = maxGroupID == null ? 1 : long.Parse(maxGroupID.ToString()) + 1;
                    entity.SkillGroupMasterID = Convert.ToByte(maxGroupID == null ? 1 : long.Parse(maxGroupID.ToString()) + 1);
                    dbContext.SkillGroupMasters.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.SkillGroupMasters.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return ToDTOString(new SkillGroupMasterDTO()
            {
                SkillGroupMasterID = entity.SkillGroupMasterID,
                SkillGroup = entity.SkillGroup,
            });
        }
    }
}

