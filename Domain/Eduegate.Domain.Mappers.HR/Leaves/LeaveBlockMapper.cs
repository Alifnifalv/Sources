using Newtonsoft.Json;
using Eduegate.Domain.Entity.HR;
using Eduegate.Domain.Entity.HR.Leaves;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.HR.Leaves;
using System;
using Eduegate.Framework;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.HR.Leaves
{
    public class LeaveBlockMapper : DTOEntityDynamicMapper
    {
        public static LeaveBlockMapper Mapper(CallContext context)
        {
            var mapper = new LeaveBlockMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<LeaveBlockDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private LeaveBlockDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateHRContext())
            {
                var entity = dbContext.LeaveBlockLists.Where(X => X.LeaveBlockListIID == IID)
                   .AsNoTracking()
                   .FirstOrDefault();

                return new LeaveBlockDTO()
                {
                    LeaveBlockListIID = entity.LeaveBlockListIID,
                    DepartmentID = entity.DepartmentID,
                    Description = entity.Description,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as LeaveBlockDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new LeaveBlockList()
            {
                LeaveBlockListIID = toDto.LeaveBlockListIID,
                DepartmentID = toDto.DepartmentID,
                Description = toDto.Description,
                CreatedBy = toDto.LeaveBlockListIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.LeaveBlockListIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.LeaveBlockListIID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.LeaveBlockListIID > 0 ? DateTime.Now : dto.UpdatedDate,
            };

            using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
            {
                dbContext.LeaveBlockLists.Add(entity);
                if (entity.LeaveBlockListIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.LeaveBlockListIID));
        }

    }
}