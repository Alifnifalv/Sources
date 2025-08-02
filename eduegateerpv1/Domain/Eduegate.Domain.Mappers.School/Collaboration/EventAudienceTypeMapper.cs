using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Collaboration;
using Eduegate.Framework;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.School.Collaboration
{
    public class EventAudienceTypeMapper : DTOEntityDynamicMapper
    {   
        public static  EventAudienceTypeMapper Mapper(CallContext context)
        {
            var mapper = new  EventAudienceTypeMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<EventAudienceTypeDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
             return ToDTOString(ToDTO(IID));
        }

        private EventAudienceTypeDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.EventAudienceTypes.Where(a => a.EventAudienceTypeID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return new EventAudienceTypeDTO()
                {
                    EventAudienceTypeID = entity.EventAudienceTypeID,
                    AudienceTypeName = entity.AudienceTypeName,
                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as EventAudienceTypeDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new EventAudienceType()
            {
                EventAudienceTypeID = toDto.EventAudienceTypeID,
                AudienceTypeName = toDto.AudienceTypeName,
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
               
                if (entity.EventAudienceTypeID == 0)
                {                 
                    var maxGroupID = dbContext.EventAudienceTypes.Max(a => (byte?)a.EventAudienceTypeID);
                    entity.EventAudienceTypeID = Convert.ToByte(maxGroupID == null ? 1 : Convert.ToByte(maxGroupID.ToString()) + 1);
                    dbContext.EventAudienceTypes.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.EventAudienceTypes.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.EventAudienceTypeID ));
        }       
    }
}




