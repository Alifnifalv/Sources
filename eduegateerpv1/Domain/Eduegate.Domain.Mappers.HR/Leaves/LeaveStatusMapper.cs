using Newtonsoft.Json;
using Eduegate.Domain.Entity.HR;
using Eduegate.Domain.Entity.HR.Leaves;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.HR.Leaves;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace Eduegate.Domain.Mappers.HR.Leaves
{
    public class LeaveStatusMapper : DTOEntityDynamicMapper
    {
        public static LeaveStatusMapper Mapper(CallContext context)
        {
            var mapper = new LeaveStatusMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<LeaveStatusDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
            {
                var entity = dbContext.LeaveStatuses.Where(X => X.LeaveStatusID == IID)
                       .AsNoTracking()
                       .FirstOrDefault();

                return ToDTOString(new LeaveStatusDTO()
                {
                    LeaveStatusID = entity.LeaveStatusID,
                    StatusName = entity.StatusName
                });
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as LeaveStatusDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new LeaveStatus()
            {
                LeaveStatusID = toDto.LeaveStatusID,
                StatusName = toDto.StatusName,
            };

            using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
            {                
                if (entity.LeaveStatusID == 0)
                {                   
                    var maxGroupID = dbContext.LeaveStatuses.Max(a => (byte?)a.LeaveStatusID);                   
                    entity.LeaveStatusID = Convert.ToByte(maxGroupID == null ? 1 : byte.Parse((byte.Parse(maxGroupID.ToString()) + 1).ToString()));

                    dbContext.LeaveStatuses.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.LeaveStatuses.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();
            }

            return ToDTOString(new LeaveStatusDTO()
            {
                StatusName = toDto.StatusName,
                LeaveStatusID = toDto.LeaveStatusID
            });
        }
    }
}