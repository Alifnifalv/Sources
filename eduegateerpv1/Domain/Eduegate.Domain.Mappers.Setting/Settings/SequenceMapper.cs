using Newtonsoft.Json;
using Eduegate.Domain.Entity.Setting.Models;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.Setting.Settings;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.Setting.Settings
{
    public class SequenceMapper : DTOEntityDynamicMapper
    {
        public static SequenceMapper Mapper(CallContext context)
        {
            var mapper = new SequenceMapper();
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
            return ToDTOString(ToDTO(IID));
        }

        private SequenceDTO ToDTO(long IID)
        {
            using (dbEduegateSettingContext dbContext = new dbEduegateSettingContext())
            {
                var entity = dbContext.Sequences.Where(x => x.SquenceID == IID).AsNoTracking().FirstOrDefault();

                return new SequenceDTO()
                {
                    SquenceID = entity.SquenceID,
                    SequenceType = entity.SequenceType,
                    Prefix = entity.Prefix,
                    Format = entity.Format,
                    LastSequence = entity.LastSequence,
                    IsAuto = entity.IsAuto,
                    ZeroPadding = entity.ZeroPadding,
                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as SequenceDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new Sequence()
            {
                SquenceID = toDto.SquenceID,
                SequenceType = toDto.SequenceType,
                Prefix = toDto.Prefix,
                Format = toDto.Format,
                LastSequence = toDto.LastSequence,
                IsAuto = toDto.IsAuto,
                ZeroPadding = toDto.ZeroPadding,
            };

            using (dbEduegateSettingContext dbContext = new dbEduegateSettingContext())
            {
                if (entity.SquenceID == 0)
                {
                    var maxGroupID = dbContext.Sequences.Max(a => (int?)a.SquenceID);
                    entity.SquenceID = maxGroupID == null ? 1 : int.Parse(maxGroupID.ToString()) + 1;
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

            return ToDTOString(ToDTO(entity.SquenceID));
        }

    }
}