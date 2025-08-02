using Newtonsoft.Json;
using Eduegate.Domain.Entity.HR;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.HR.Leaves;
using System;
using Eduegate.Framework;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.HR.Payroll
{
    public class HolidayMapper : DTOEntityDynamicMapper
    {
        public static HolidayMapper Mapper(CallContext context)
        {
            var mapper = new HolidayMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<HolidayDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
            {
                var entity = dbContext.Holidays.Where(X => X.HolidayIID == IID)
                .AsNoTracking()
                .FirstOrDefault();

                return ToDTOString(new HolidayDTO()
                {
                    HolidayIID = entity.HolidayIID,
                    HolidayListID = entity.HolidayListID,
                    HolidayDate = entity.HolidayDate,
                    Description = entity.Description,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    //TimeStamps = entity.TimeStamps,

                });
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as HolidayDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new Holiday()
            {
                HolidayIID = toDto.HolidayIID,
                HolidayListID = toDto.HolidayListID,
                HolidayDate = toDto.HolidayDate,
                Description = toDto.Description,
                CreatedBy = toDto.HolidayIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.HolidayIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.HolidayIID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.HolidayIID > 0 ? DateTime.Now : dto.UpdatedDate,
                //TimeStamps = toDto.TimeStamps == null ? null : toDto.TimeStamps,

            };

            using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
            {
                if (entity.HolidayIID == 0)
                {
                    var maxGroupID = dbContext.Holidays.Max(a => (long?)a.HolidayIID);
                    entity.HolidayIID = maxGroupID == null ? 1 : long.Parse(maxGroupID.ToString()) + 1;

                    dbContext.Holidays.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Holidays.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();
            }

            return ToDTOString(new HolidayDTO()
            {
                HolidayIID = entity.HolidayIID,
                HolidayListID = entity.HolidayListID,
                HolidayDate = entity.HolidayDate,
                Description = entity.Description,
                CreatedBy = entity.HolidayIID == 0 ? (int)_context.LoginID : entity.CreatedBy,
                UpdatedBy = entity.HolidayIID > 0 ? (int)_context.LoginID : entity.UpdatedBy,
                CreatedDate = entity.HolidayIID == 0 ? DateTime.Now : entity.CreatedDate,
                UpdatedDate = entity.HolidayIID > 0 ? DateTime.Now : entity.UpdatedDate,
                //TimeStamps = entity.TimeStamps == null ? null : entity.TimeStamps,

            });
        }

    }
}