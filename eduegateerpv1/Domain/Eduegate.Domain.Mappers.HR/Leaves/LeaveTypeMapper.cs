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
    public class LeaveTypeMapper : DTOEntityDynamicMapper
    {

        public static LeaveTypeMapper Mapper(CallContext context)
        {
            var mapper = new LeaveTypeMapper();
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
                var entity = dbContext.LeaveTypes.Where(X => X.LeaveTypeID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return ToDTOString(new LeaveTypeDTO()
                {
                    AllowNegativeBalance = entity.AllowNegativeBalance,
                    Description = entity.Description,
                    IncludeHolidayWithinLeaves = entity.IncludeHolidayWithinLeaves,
                    IsCarryForward = entity.IsCarryForward,
                    IsLeaveWithoutPay = entity.IsLeaveWithoutPay,
                    MaxDaysAllowed = entity.MaxDaysAllowed,
                    LeaveTypeID = entity.LeaveTypeID,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                });
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as LeaveTypeDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new LeaveType()
            {
                AllowNegativeBalance = toDto.AllowNegativeBalance,
                Description = toDto.Description,
                IncludeHolidayWithinLeaves = toDto.IncludeHolidayWithinLeaves,
                IsCarryForward = toDto.IsCarryForward,
                IsLeaveWithoutPay = toDto.IsLeaveWithoutPay,
                MaxDaysAllowed = toDto.MaxDaysAllowed,
                LeaveTypeID = toDto.LeaveTypeID,
                CreatedBy = toDto.LeaveTypeID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.LeaveTypeID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.LeaveTypeID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.LeaveTypeID > 0 ? DateTime.Now : dto.UpdatedDate,
            };

            using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
            {               
                if (entity.LeaveTypeID == 0)
                {
                    var maxGroupID = dbContext.LeaveTypes.Max(a => (int?)a.LeaveTypeID);                  
                    entity.LeaveTypeID = Convert.ToInt32(maxGroupID == null ? 1 : int.Parse(maxGroupID.ToString()) + 1);
                    dbContext.LeaveTypes.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.LeaveTypes.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();
            }

            return GetEntity(entity.LeaveTypeID);
        }
    }
}