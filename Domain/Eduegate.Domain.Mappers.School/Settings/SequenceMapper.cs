using Newtonsoft.Json;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Settings;
using System;
using Eduegate.Framework;
using Eduegate.Domain.Entity.School.Models.School;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.School.Settings
{
    public class SequenceMapper : DTOEntityDynamicMapper
    {   
        public static  SequenceMapper Mapper(CallContext context)
        {
            var mapper = new  SequenceMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<SequenceDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.Sequences.Where(a => a.SequenceID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return ToDTOString(new SequenceDTO()
                {
                    SequenceID = entity. SequenceID,
                    SequenceType = entity. SequenceType,
                    Prefix=entity.Prefix,
                    LastSequence=entity.LastSequence,
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    UpdatedBy = entity.UpdatedBy
                });
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as SequenceDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new Eduegate.Domain.Entity.School.Models.Sequence()
            {
                SequenceID = toDto. SequenceID,
                SequenceType = toDto. SequenceType,
                Prefix = toDto.Prefix,
                LastSequence = toDto.LastSequence,
                CreatedBy = toDto.SequenceID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.SequenceID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.SequenceID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.SequenceID > 0 ? DateTime.Now : dto.UpdatedDate,

            };

            using (var dbContext = new dbEduegateSchoolContext())
            {             
                if (entity.SequenceID == 0)
                {                   
                    var maxGroupID = dbContext.Sequences.Max(a => (int?)a.SequenceID);
                    entity.SequenceID = maxGroupID == null ? 1 : int.Parse(maxGroupID.ToString()) + 1;
                    dbContext.Sequences.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Sequences.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();
            }

            return ToDTOString(new SequenceDTO()
            {
                SequenceID = entity. SequenceID,
                SequenceType = entity. SequenceType,
                Prefix = entity.Prefix,
                LastSequence = entity.LastSequence,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate,
                UpdatedBy = entity.UpdatedBy
            });
        }

    }
}