using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
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
    public class SubjectTeacherMapMapper : DTOEntityDynamicMapper
    {
        public static SubjectTeacherMapMapper Mapper(CallContext context)
        {
            var mapper = new SubjectTeacherMapMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<SubjectTeacherMapDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private SubjectTeacherMapDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.SubjectTeacherMaps.Where(x => x.SubjectTeacherMapIID == IID)
                    .Include(i => i.Subject)
                    .Include(i => i.Employee)
                    .AsNoTracking().FirstOrDefault();

                var getTeacherDatas = dbContext.SubjectTeacherMaps.Where(y => y.SubjectID == entity.SubjectID && y.AcademicYearID == entity.AcademicYearID)
                    .Include(i => i.Employee)
                    .AsNoTracking()
                    .ToList();

                var teachers = new List<KeyValueDTO>();

                foreach (var emp in getTeacherDatas)
                {
                    teachers.Add(new KeyValueDTO()
                    {
                        Value = emp.Employee.EmployeeCode + " - " + emp.Employee.FirstName + " " + emp.Employee.MiddleName + " " + emp.Employee.LastName,
                        Key = emp.EmployeeID.ToString()
                    });
                }

                return new SubjectTeacherMapDTO()
                {
                    SubjectTeacherMapIID = entity.SubjectTeacherMapIID,
                    SubjectID = entity.SubjectID,
                    SubjectName = new KeyValueDTO() { Key = entity.Subject.SubjectID.ToString(), Value = entity.Subject.SubjectName },
                    Teachers = teachers,
                    TeacherName = entity.Employee != null ? entity.Employee.EmployeeCode + " - " + entity.Employee.FirstName + " " + entity.Employee.MiddleName + " " + entity.Employee.LastName : null,
                    AcademicYearID = entity.AcademicYearID,
                    SchoolID = entity.SchoolID,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    //TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),
                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as SubjectTeacherMapDTO;
            if (toDto.SubjectID == null || toDto.SubjectID == 0)
            {
                throw new Exception("Please Select Subject");
            }
            if (toDto.Teachers.Count < 0)
            {
                throw new Exception("Please Select Employee");
            }


            using (var dbContext = new dbEduegateSchoolContext())
            {
                var getData = dbContext.SubjectTeacherMaps.Where(x => x.SubjectID == toDto.SubjectID && x.AcademicYearID == _context.AcademicYearID).AsNoTracking().ToList();
                if (toDto.SubjectTeacherMapIID == 0)
                {
                    foreach (var duplicate in getData)
                    {
                        if (toDto.SubjectID == duplicate.SubjectID && _context.AcademicYearID == duplicate.AcademicYearID)
                        {
                            throw new Exception("Teachers are already assigned to this Subject Please check!");
                        }
                    }
                }
                else
                {
                    //delete all mapping and recreate
                    if (getData != null && getData.Count > 0)
                    {
                        dbContext.SubjectTeacherMaps.RemoveRange(getData);
                    }
                }

                SubjectTeacherMap teacherMap = null;
                if (toDto.Teachers.Count > 0)
                {
                    foreach (KeyValueDTO keyval in toDto.Teachers)
                    {
                        var entity = new SubjectTeacherMap()
                        {
                            //SubjectTeacherMapIID = toDto.SubjectTeacherMapIID,
                            SubjectID = toDto.SubjectID,
                            EmployeeID = long.Parse(keyval.Key),
                            SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                            AcademicYearID = toDto.AcademicYearID == null ? (int)_context.AcademicYearID : toDto.AcademicYearID,
                            CreatedBy = toDto.SubjectTeacherMapIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                            UpdatedBy = toDto.SubjectTeacherMapIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                            CreatedDate = toDto.SubjectTeacherMapIID == 0 ? DateTime.Now.Date : dto.CreatedDate,
                            UpdatedDate = toDto.SubjectTeacherMapIID > 0 ? DateTime.Now.Date : dto.UpdatedDate,
                            //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
                        };

                        teacherMap = entity;
                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    }
                }
                dbContext.SaveChanges();

                return GetEntity(teacherMap.SubjectTeacherMapIID);
            }
        }

        public List<SubjectTeacherMapDTO> GetTeacherBySubject(int classId, int sectionId, int subjectId)
        {
            var detailList = new List<SubjectTeacherMapDTO>();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entitiesSubjectList = dbContext.SubjectTeacherMaps.Where(X => X.SubjectID == subjectId && X.SchoolID == _context.SchoolID && X.AcademicYearID == _context.AcademicYearID)
                    .Include(x => x.Subject)
                    .Include(x => x.Employee)
                    .AsNoTracking()
                    .ToList();

                foreach (var data in entitiesSubjectList)
                {
                    detailList.Add(new SubjectTeacherMapDTO()
                    {
                        SubjectTeacherMapIID = data.SubjectTeacherMapIID,
                        EmployeeID = data.EmployeeID,
                        SubjectID = data.SubjectID,
                        TeacherName = data.Employee != null ? data.Employee.EmployeeCode + " - " + data.Employee.FirstName + " " + data.Employee.MiddleName + " " + data.Employee.LastName : null,
                        SubjectName = new KeyValueDTO() { Key = data.Subject.SubjectID.ToString(), Value = data.Subject.SubjectName },
                    });
                }
            }

            return detailList;
        }

    }
}