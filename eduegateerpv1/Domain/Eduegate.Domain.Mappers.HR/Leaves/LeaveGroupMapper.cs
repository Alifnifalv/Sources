using Newtonsoft.Json;
using Eduegate.Domain.Entity.HR;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.HR.Leaves;
using Eduegate.Framework;
using System;
using Eduegate.Domain.Entity.HR.Models.Leaves;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.HR.Leaves
{
    public class LeaveGroupMapper : DTOEntityDynamicMapper
    {

        public static LeaveGroupMapper Mapper(CallContext context)
        {
            var mapper = new LeaveGroupMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<LeaveTypeDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
            {
                var entity = dbContext.LeaveGroups.Where(X => X.LeaveGroupID == IID)
                 .AsNoTracking()
                 .FirstOrDefault();

                return ToDTOString(new LeaveGroupDTO()
                {
                    LeaveGroupID = entity.LeaveGroupID
                });
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as LeaveGroupDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new LeaveGroup()
            {
                LeaveGroupName = toDto.LeaveGroupName,
                LeaveGroupID = toDto.LeaveGroupID,

            };

            using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
            {               
                if (entity.LeaveGroupID == 0)
                {
                    var maxGroupID = dbContext.LeaveGroups.Max(a => (int?)a.LeaveGroupID);
                    entity.LeaveGroupID = Convert.ToInt32(maxGroupID == null ? 1 : int.Parse(maxGroupID.ToString()) + 1);
                    dbContext.LeaveGroups.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.LeaveGroups.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return ToDTOString(new LeaveGroupDTO()
            {
                LeaveGroupID = toDto.LeaveGroupID,
                LeaveGroupName = toDto.LeaveGroupName,

            });
        }

        public List<LeaveGroupTypeMapDTO> LeaveGroupChanges(int? leaveGroupID)
        {
            using (var dbContext = new dbEduegateHRContext())
            {
                var leaveGroupTypeList = new List<LeaveGroupTypeMapDTO>();

                try
                {
                    leaveGroupTypeList = dbContext.LeaveAllocations.Where(X => X.LeaveGroupID == leaveGroupID).AsNoTracking()
                        .Select(x => new LeaveGroupTypeMapDTO
                        {
                            LeaveTypeID = x.LeaveTypeID,
                            LeaveGroupID = x.LeaveGroupID,
                            NoofLeaves = (int?)(x.AllocatedLeaves),
                            LeaveType = new KeyValueDTO()
                            {
                                Key = x.LeaveTypeID.ToString(),
                                Value = x.LeaveType.Description
                            }
                        }).ToList();
                }
                catch(Exception ex)
                {
                    Eduegate.Logger.LogHelper<LeaveGroupMapper>.Fatal(ex.Message, ex);
                }

                return leaveGroupTypeList;
            }
        }

    }
}