using Newtonsoft.Json;
using Eduegate.Domain.Entity.HR;
using Eduegate.Domain.Entity.HR.Leaves;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.HR.Leaves;
using System;
using Eduegate.Framework;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.HR.Leaves
{
    public class LeaveAllocationMapper : DTOEntityDynamicMapper
    {
        public static LeaveAllocationMapper Mapper(CallContext context)
        {
            var mapper = new LeaveAllocationMapper();
            mapper._context = context;
            return mapper;
        }



        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<LeaveAllocationDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
            {
                var entity = dbContext.LeaveAllocations.Where(X => X.LeaveAllocationIID == IID)
                   .AsNoTracking()
                   .FirstOrDefault();

                return ToDTOString(new LeaveAllocationDTO()
                {
                    LeaveAllocationIID = entity.LeaveAllocationIID,
                    LeaveGroupID = entity.LeaveGroupID,
                    LeaveTypeID = entity.LeaveTypeID,
                    DateFrom = entity.DateFrom,
                    DateTo = entity.DateTo,
                    Description = entity.Description,
                    AllocatedLeaves = entity.AllocatedLeaves,

                });
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as LeaveAllocationDTO;

            if (toDto.DateFrom.HasValue && toDto.DateTo.HasValue && toDto.DateTo.Value <= toDto.DateFrom.Value)
            {
                throw new Exception("DateTo should be greater than DateFrom");
            }



            //convert the dto to entity and pass to the repository.
            var entity = new LeaveAllocation()




            {
                LeaveAllocationIID = toDto.LeaveAllocationIID,
                LeaveGroupID = toDto.LeaveGroupID,
                LeaveTypeID = toDto.LeaveTypeID,
                DateFrom = toDto.DateFrom,
                DateTo = toDto.DateTo,
                Description = toDto.Description,
                AllocatedLeaves = toDto.AllocatedLeaves,
                CreatedBy = toDto.LeaveAllocationIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.LeaveAllocationIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.LeaveAllocationIID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.LeaveAllocationIID > 0 ? DateTime.Now : dto.UpdatedDate,
                //TimeStamps = toDto.TimeStamps == null ? null : toDto.TimeStamps,
            };


            using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
            {

                if (entity.LeaveAllocationIID == 0)
                {
                    dbContext.LeaveAllocations.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();
            }

            return GetEntity(entity.LeaveAllocationIID);
        }
    }
}