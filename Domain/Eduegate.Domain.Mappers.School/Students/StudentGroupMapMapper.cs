using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.School.Students;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace Eduegate.Domain.Mappers.School.Students
{
    class StudentGroupMapMapper : DTOEntityDynamicMapper
    {
        public static StudentGroupMapMapper Mapper(CallContext context)
        {
            var mapper = new StudentGroupMapMapper();
            mapper._context = context;
            return mapper;
        }
        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<StudentGroupMapDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private StudentGroupMapDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.StudentGroupMaps.Where(a => a.StudentGroupMapIID == IID)
                    .Include(i => i.Student)
                    .Include(i => i.StudentGroup)
                    .AsNoTracking()
                    .FirstOrDefault();

                return new StudentGroupMapDTO()
                {
                    StudentGroupMapIID = entity.StudentGroupMapIID,
                    StudentID = entity.StudentID,
                    StudentName = entity.Student != null ? entity.Student.FirstName : null,
                    StudentGroupID = entity.StudentGroupID,
                    StudentGroup = entity.StudentGroup != null ? entity.StudentGroup.GroupName : null,
                    AcademicYearID = entity.AcademicYearID,
                    SchoolID = entity.SchoolID,
                    IsActive = entity.IsActive,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    ////TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),
                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as StudentGroupMapDTO;
            if (toDto.StudentID == 0 || toDto.StudentID == null)
            {
                throw new Exception("Please Select Student!");
            }
            if (toDto.StudentGroupID == 0 || toDto.StudentGroupID == null)
            {
                throw new Exception("Please Select StudentGroup!");
            }

            //convert the dto to entity and pass to the repository.
            var entity = new StudentGroupMap()
            {
                StudentGroupMapIID = toDto.StudentGroupMapIID,
                StudentID = toDto.StudentID,
                StudentGroupID = toDto.StudentGroupID,
                IsActive = toDto.IsActive,
                SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                AcademicYearID = toDto.AcademicYearID == null ? (int)_context.AcademicYearID : toDto.AcademicYearID,
                CreatedBy = toDto.StudentGroupMapIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.StudentGroupMapIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.StudentGroupMapIID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.StudentGroupMapIID > 0 ? DateTime.Now : dto.UpdatedDate,
                ////TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
                dbContext.StudentGroupMaps.Add(entity);
                if (entity.StudentGroupMapIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.StudentGroupMapIID));
        }

    }
}