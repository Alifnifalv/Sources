using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Academics;
using Eduegate.Framework;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.School.Academics
{
    public class ShiftMapper : DTOEntityDynamicMapper
    {   
        public static  ShiftMapper Mapper(CallContext context)
        {
            var mapper = new  ShiftMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<ShiftDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
             return ToDTOString(ToDTO(IID));
        }

        private ShiftDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {              
                var entity = dbContext.Shifts.Where(X => X.ClassShiftID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return new ShiftDTO()
                {
                    ClassShiftID = entity.ClassShiftID,
                    ShiftDescription = entity.ShiftDescription,
                    AcademicYearID = entity.AcademicYearID,
                    SchoolID = entity.SchoolID,
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    UpdatedBy = entity.UpdatedBy
                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as ShiftDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new Shift()
            {
                ClassShiftID = toDto.ClassShiftID,
                ShiftDescription = toDto.ShiftDescription,
                SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                AcademicYearID = toDto.AcademicYearID == null ? (int)_context.AcademicYearID : toDto.AcademicYearID,
                CreatedBy = toDto.ClassShiftID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.ClassShiftID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.ClassShiftID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.ClassShiftID > 0 ? DateTime.Now : dto.UpdatedDate,
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
                if (entity.ClassShiftID == 0)
                {                  
                    var maxGroupID = dbContext.Shifts.Max(a => (byte?)a.ClassShiftID);
                    entity.ClassShiftID = Convert.ToByte(maxGroupID == null ? 1 : int.Parse(maxGroupID.ToString()) + 1);
                    dbContext.Shifts.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Shifts.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.ClassShiftID));
        }

    }
}