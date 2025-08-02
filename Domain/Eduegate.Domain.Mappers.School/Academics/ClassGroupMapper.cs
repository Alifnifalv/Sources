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
using Eduegate.Services.Contracts.School.Students;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Eduegate.Domain.Mappers.School.Academics
{
    public class ClassGroupMapper : DTOEntityDynamicMapper
    {   
        public static  ClassGroupMapper Mapper(CallContext context)
        {
            var mapper = new  ClassGroupMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<ClassGroupDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
             return ToDTOString(ToDTO(IID));
        }

        private ClassGroupDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.ClassGroups.Where(X => X.ClassGroupID == IID)
                    .Include(i => i.Employee)
                    .Include(i => i.Subject)
                    .Include(i => i.ClassGroupTeacherMaps).ThenInclude(i => i.Employee)
                    .AsNoTracking()
                    .FirstOrDefault();
           
                var classGroupDetails = new ClassGroupDTO()
                {
                    ClassGroupID = entity.ClassGroupID,
                    GroupDescription = entity.GroupDescription,
                    AcademicYearID = entity.AcademicYearID,
                    SchoolID = entity.SchoolID,
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    UpdatedBy = entity.UpdatedBy,
                    HeadTeacherID = entity.HeadTeacherID,
                    HeadTeacher = entity.HeadTeacherID.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.HeadTeacherID.ToString(),
                        Value = entity.Employee != null ? entity.Employee.EmployeeCode + "-" + entity.Employee.FirstName + " " + entity.Employee.MiddleName + " " + entity.Employee.LastName : null
                    } : new KeyValueDTO(),
                    SubjectID = entity.SubjectID,
                    Subject = entity.SubjectID.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.SubjectID.ToString(),
                        Value = entity.Subject?.SubjectName
                    } : new KeyValueDTO(),
                };

                classGroupDetails.ClassTeacher = new List<KeyValueDTO>();
                foreach (var teacher in entity.ClassGroupTeacherMaps)
                {
                    classGroupDetails.ClassTeacher.Add(new KeyValueDTO()
                    {
                        Key = Convert.ToString(teacher.TeacherID),
                        Value = teacher.Employee != null ? teacher.Employee.EmployeeCode +"-"+ teacher.Employee.FirstName + " " + teacher.Employee.MiddleName + " " + teacher.Employee.LastName : null
                    });
                }

                return classGroupDetails;
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as ClassGroupDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new ClassGroup()
            {
                ClassGroupID = toDto.ClassGroupID,
                GroupDescription = toDto.GroupDescription,
                SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                AcademicYearID = toDto.AcademicYearID == null ? (int)_context.AcademicYearID : toDto.AcademicYearID,
                CreatedBy = toDto.ClassGroupID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.ClassGroupID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.ClassGroupID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.ClassGroupID > 0 ? DateTime.Now : dto.UpdatedDate,
                HeadTeacherID = toDto.HeadTeacherID,
                SubjectID = toDto.SubjectID,
            };

            entity.ClassGroupTeacherMaps = new List<ClassGroupTeacherMap>();
            foreach (var teacherMap in toDto.ClassTeacher)
            {
                entity.ClassGroupTeacherMaps.Add(new ClassGroupTeacherMap()
                {
                    ClassGroupID = toDto.ClassGroupID,
                    TeacherID =long.Parse(teacherMap.Key),
                    SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                    AcademicYearID = toDto.AcademicYearID == null ? (int)_context.AcademicYearID : toDto.AcademicYearID,
                });
            }

            using (var dbContext = new dbEduegateSchoolContext())
            {
                if (entity.ClassGroupID == 0)
                {                  
                    var maxGroupID = dbContext.ClassGroups.Max(a => (long?)a.ClassGroupID);                   
                    entity.ClassGroupID = Convert.ToInt64(maxGroupID == null ? 1 : long.Parse(maxGroupID.ToString()) + 1);
                    dbContext.ClassGroups.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.ClassGroups.Add(entity);                   
                    foreach (var teacher in entity.ClassGroupTeacherMaps)
                    {
                        if (teacher.ClassGroupTeacherMapIID != 0)
                        {
                            dbContext.Entry(teacher).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                        else
                        {
                            dbContext.Entry(teacher).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                    }
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
               
                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.ClassGroupID ));
        }       

    }
}