using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Academics;
using Eduegate.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.School.Academics
{
    public class ClassTimingMapper : DTOEntityDynamicMapper
    {
        public static ClassTimingMapper Mapper(CallContext context)
        {
            var mapper = new ClassTimingMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<ClassTimingDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private ClassTimingDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.ClassTimings.Where(X => X.ClassTimingID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return new ClassTimingDTO()
                {
                    ClassTimingID = entity.ClassTimingID,
                    ClassTimingSetID = entity.ClassTimingSetID,
                    TimingDescription = entity.TimingDescription,
                    StartTime = entity.StartTime,
                    EndTime = entity.EndTime,
                    IsBreakTime = entity.IsBreakTime,
                    AcademicYearID = entity.AcademicYearID,
                    SchoolID = entity.SchoolID,
                    BreakTypeID = entity.BreakTypeID,
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedDate = entity.UpdatedDate,
                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as ClassTimingDTO;
            //convert the dto to entity and pass to the repository.
            if (toDto.StartTime >= toDto.EndTime)
            {
                throw new Exception("Select Time Properlly!!");
            }
            if (toDto.IsBreakTime == true && toDto.BreakTypeID == null)
            {
                throw new Exception("Select Time Properlly!!");
            }
            var entity = new ClassTiming()
            {
                ClassTimingID = toDto.ClassTimingID,
                ClassTimingSetID = toDto.ClassTimingSetID,
                TimingDescription = toDto.TimingDescription,
                StartTime = toDto.StartTime,
                EndTime = toDto.EndTime,
                IsBreakTime = toDto.IsBreakTime,
                BreakTypeID = toDto.IsBreakTime == true ? toDto.BreakTypeID : null,
                SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                AcademicYearID = toDto.AcademicYearID == null ? (int)_context.AcademicYearID : toDto.AcademicYearID,
                CreatedBy = toDto.ClassTimingID == 0 ? Convert.ToInt32(_context.LoginID) : toDto.CreatedBy,
                CreatedDate = toDto.ClassTimingID == 0 ? DateTime.Now : toDto.CreatedDate,
                UpdatedBy = toDto.ClassTimingID != 0 ? Convert.ToInt32(_context.LoginID) : toDto.UpdatedBy,
                UpdatedDate = toDto.ClassTimingID != 0 ? DateTime.Now : toDto.UpdatedDate,
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
                if (entity.ClassTimingID == 0)
                {                    
                    var maxGroupID = dbContext.ClassTimings.Max(a => (int?)a.ClassTimingID);                    
                    entity.ClassTimingID = Convert.ToByte(maxGroupID == null ? 1 : int.Parse(maxGroupID.ToString()) + 1);
                    dbContext.ClassTimings.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.ClassTimings.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
               
                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.ClassTimingID));
        }
        public List<ClassTimingDTO> GetClassTimingByClassSet(int classTimingSetID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var dtos = new List<ClassTimingDTO>();

                var classTimings = dbContext.ClassTimings.Where(x => x.ClassTimingSetID == classTimingSetID).AsNoTracking().ToList();

                foreach (var timing in classTimings)
                {
                    dtos.Add(new ClassTimingDTO()
                    {
                        TimingDescription = timing.TimingDescription,
                    });
                }

                return dtos;
            }
        }

    }
}