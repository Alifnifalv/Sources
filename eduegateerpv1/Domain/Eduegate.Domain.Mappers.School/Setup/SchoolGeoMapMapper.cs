using Newtonsoft.Json;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Settings;
using Eduegate.Domain.Entity.Models.Settings;
using System;
using Eduegate.Framework;
using Eduegate.Services.Contracts.School.Setup;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.School.Setup
{
    public class SchoolGeoMapMapper : DTOEntityDynamicMapper
    {

        public static SchoolGeoMapMapper Mapper(CallContext context)
        {
            var mapper = new SchoolGeoMapMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<SchoolGeoMapDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            using (dbEduegateSchoolContext dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.SchoolGeoMaps.Where(a => a.SchoolID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return ToDTOString(new SchoolGeoMapDTO()
                {
                    SchoolID = entity.SchoolID,
                    Longitude = entity.Longitude,
                    Latitude = entity.Latitude,
                    AreaID = entity.AreaID
                });
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as SchoolGeoMapDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new SchoolGeoMap()
            {

                SchoolID = toDto.SchoolID,
                Longitude = toDto.Longitude,
                Latitude = toDto.Latitude,
                AreaID = toDto.AreaID
            };

            using (dbEduegateSchoolContext dbContext = new dbEduegateSchoolContext())
            {
                dbContext.SchoolGeoMaps.Add(entity);

                if (entity.SchoolGeoMapIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();
            }

            return ToDTOString(new SchoolGeoMapDTO()
            {
                SchoolID = entity.SchoolID,
                Longitude = entity.Longitude,
                Latitude = entity.Latitude,
                AreaID = entity.AreaID

            });
        }
    }
}
