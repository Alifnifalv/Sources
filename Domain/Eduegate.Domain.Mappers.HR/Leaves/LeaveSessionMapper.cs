using Newtonsoft.Json;
using Eduegate.Domain.Entity.HR;
using Eduegate.Domain.Entity.HR.Leaves;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.HR.Leaves;
using Eduegate.Framework;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.HR.Leaves
{
    public class LeaveSessionMapper : DTOEntityDynamicMapper
    {
        public static LeaveSessionMapper Mapper(CallContext context)
        {
            var mapper = new LeaveSessionMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<LeaveSessionDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private LeaveSessionDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateHRContext())
            {
                var entity = dbContext.LeaveSessions.Where(X => X.LeaveSessionID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return new LeaveSessionDTO()
                {
                    LeaveSessionID = entity.LeaveSessionID,
                    SesionName = entity.SesionName,
                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as LeaveSessionDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new LeaveSession()
            {
                LeaveSessionID = toDto.LeaveSessionID,
                SesionName = toDto.SesionName,
            };

            using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
            {
                if (entity.LeaveSessionID == 0)
                {                   
                    var maxGroupID = dbContext.LeaveSessions.Max(a => (byte?)a.LeaveSessionID);                  
                    entity.LeaveSessionID = (byte)(maxGroupID == null ? 1 : byte.Parse(maxGroupID.ToString()) + 1);

                    dbContext.LeaveSessions.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.LeaveSessions.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.LeaveSessionID));
        }
    }
}