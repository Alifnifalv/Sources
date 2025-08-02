using System;
using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework;
using Eduegate.Services.Contracts.School.Students;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.School.Students
{
    public class CastMapper : DTOEntityDynamicMapper
    {
        public static CastMapper Mapper(CallContext context)
        {
            var mapper = new CastMapper();
            mapper._context = context;
            return mapper;
        }
        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<CastDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        public CastDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.Casts.Where(a => a.CastID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return new CastDTO()
                {
                    CastID = entity.CastID,
                    CastDescription = entity.CastDescription,
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    UpdatedBy = entity.UpdatedBy
                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as CastDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new Cast()
            {
                CastID = toDto.CastID,
                CastDescription = toDto.CastDescription,
                CreatedBy = toDto.CastID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.CastID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.CastID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.CastID > 0 ? DateTime.Now : dto.UpdatedDate,
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
               
                if (entity.CastID == 0)
                {                   
                    var maxGroupID = dbContext.Casts.Max(a => (byte?)a.CastID);
                    entity.CastID = Convert.ToByte(maxGroupID == null ? 1 : byte.Parse(maxGroupID.ToString()) + 1);
                    dbContext.Casts.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Casts.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return ToDTOString(new CastDTO()
            {
                CastID = entity.CastID,
                CastDescription = entity.CastDescription,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate,
                UpdatedBy = entity.UpdatedBy

            });
        }
    }
}