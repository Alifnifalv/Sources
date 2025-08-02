using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Settings;
using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Entity.School.Models;
using System;
using Eduegate.Framework;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.School.Settings
{
    public class BuildingMapper : DTOEntityDynamicMapper
    {
        public static BuildingMapper Mapper(CallContext context)
        {
            var mapper = new BuildingMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<BuildingDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.Buildings.Where(a => a.BuildingID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return ToDTOString(new BuildingDTO()
                {
                    BuildingID = entity.BuildingID,
                    BuildingName = entity.BuildingName,
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    UpdatedBy = entity.UpdatedBy
                });
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as BuildingDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new Building()
            {
                BuildingID = toDto.BuildingID,
                BuildingName = toDto.BuildingName,
                CreatedBy = toDto.BuildingID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.BuildingID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.BuildingID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.BuildingID > 0 ? DateTime.Now : dto.UpdatedDate,

            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
              
                if (entity.BuildingID == 0)
                {
                    var maxGroupID = dbContext.Buildings.Max(a => (int?)a.BuildingID);
                    entity.BuildingID = maxGroupID == null ? 1 : int.Parse(maxGroupID.ToString()) + 1;
                    dbContext.Buildings.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Buildings.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return ToDTOString(new BuildingDTO()
            {
                BuildingID = entity.BuildingID,
                BuildingName = entity.BuildingName,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate,
                UpdatedBy = entity.UpdatedBy


            });
        }

    }
}