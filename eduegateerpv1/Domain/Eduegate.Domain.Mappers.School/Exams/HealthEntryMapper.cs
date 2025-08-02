using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.School.Exams;
using Eduegate.Services.Contracts.School.Students;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eduegate.Domain.Mappers.School.Exams
{
    public class HealthEntryMapper : DTOEntityDynamicMapper
    {
        public static HealthEntryMapper Mapper(CallContext context)
        {
            var mapper = new HealthEntryMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<HealthEntryDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }
        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private HealthEntryDTO ToDTO(long IID)
        {
            using (dbEduegateSchoolContext dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.HealthEntries.Where(a => a.HealthEntryIID == IID)
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .Include(i => i.AcademicYear)
                    .Include(i => i.ExamGroup)
                    .Include(i => i.HealthEntryStudentMaps).ThenInclude(i => i.Student)
                    .AsNoTracking()
                    .FirstOrDefault();

                var dto = new HealthEntryDTO()
                {
                    HealthEntryIID = entity.HealthEntryIID,
                    ClassID = entity.ClassID.HasValue ? entity.ClassID : null,
                    SectionID = entity.SectionID.HasValue ? entity.SectionID : null,
                    AcademicYearID = entity.AcademicYearID.HasValue ? entity.AcademicYearID : null,
                    ExamGroupID = entity.ExamGroupID.HasValue ? entity.ExamGroupID : null,
                    ExamGroupName = entity.ExamGroupID.HasValue || entity.ExamGroup != null ? entity.ExamGroup.ExamGroupName : null,
                    Class = entity.ClassID.HasValue ? new KeyValueDTO() { Key = entity.ClassID.ToString(), Value = entity.Class != null ? entity.Class.ClassDescription : null } : new KeyValueDTO(),
                    Section = entity.SectionID.HasValue ? new KeyValueDTO() { Key = entity.SectionID.ToString(), Value = entity.Section != null ? entity.Section.SectionName : null } : new KeyValueDTO(),
                    AcademicYear = entity.AcademicYearID.HasValue ? new KeyValueDTO() { Key = entity.AcademicYearID.ToString(), Value = entity.AcademicYear != null ? entity.AcademicYear.Description +" "+'('+entity.AcademicYear.AcademicYearCode+')' : null } : new KeyValueDTO(),
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedDate = entity.UpdatedDate,
                };

                foreach (var studMap in entity.HealthEntryStudentMaps)
                {
                    dto.HealthEntryStudentMap.Add(new HealthEntryStudentMapDTO()
                    {
                        HealthEntryID = studMap.HealthEntryID,
                        HealthEntryStudentMapIID = studMap.HealthEntryStudentMapIID,
                        StudentID = studMap.StudentID.HasValue ? studMap.StudentID : null,
                        StudentName = studMap.StudentID.HasValue || studMap.Student != null ? studMap.Student.AdmissionNumber + " - " + studMap.Student.FirstName + "  " + studMap.Student.MiddleName + "  " + studMap.Student.LastName : null,
                        Height = studMap.Height,
                        Weight = studMap.Weight,
                        BMS = studMap.BMS,
                        Remarks = studMap.Remarks,
                    });
                }

                return dto;
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as HealthEntryDTO;

            //convert the dto to entity and pass to the repository.
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = new HealthEntry()
                {
                    HealthEntryIID = toDto.HealthEntryIID,
                    ClassID = toDto.ClassID,
                    SectionID = toDto.SectionID,
                    ExamGroupID = toDto.ExamGroupID,
                    TeacherID = _context.EmployeeID.HasValue ? _context.EmployeeID : null,
                    SchoolID = _context.SchoolID.HasValue ? Convert.ToByte(_context.SchoolID) : (byte?)null,
                    AcademicYearID = toDto.AcademicYearID.HasValue ? toDto.AcademicYearID : _context.AcademicYearID,
                    CreatedBy = toDto.HealthEntryIID == 0 ? Convert.ToInt32(_context.LoginID) : toDto.CreatedBy,
                    CreatedDate = toDto.HealthEntryIID == 0 ? DateTime.Now : toDto.CreatedDate,
                    UpdatedBy = toDto.HealthEntryIID != 0 ? Convert.ToInt32(_context.LoginID) : toDto.UpdatedBy,
                    UpdatedDate = toDto.HealthEntryIID != 0 ? DateTime.Now : toDto.UpdatedDate,
                };

                entity.HealthEntryStudentMaps = new List<HealthEntryStudentMap>();
                foreach (var Det in toDto.HealthEntryStudentMap)
                {
                    entity.HealthEntryStudentMaps.Add(new HealthEntryStudentMap()
                    {
                        HealthEntryStudentMapIID = Det.HealthEntryStudentMapIID,
                        StudentID = Det.StudentID,
                        Height = Det.Height,
                        Weight = Det.Weight,
                        BMS = Det.BMS,
                        Remarks = Det.Remarks,
                    });

                }

                //Check already exist data when new create remark entry 
                if (entity.HealthEntryIID == 0)
                {
                    var getHealth = dbContext.HealthEntries
                        .Where(X => X.ClassID == toDto.ClassID && X.SectionID == toDto.SectionID && X.ExamGroupID == toDto.ExamGroupID && X.AcademicYearID == toDto.AcademicYearID)
                        .AsNoTracking().FirstOrDefault();

                    if (getHealth != null)
                    {
                        throw new Exception("For this Class,Section,Term data already exist. Please use Edit option!");
                    }
                }

                dbContext.HealthEntries.Add(entity);
                if (entity.HealthEntryIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                foreach (var studentMap in entity.HealthEntryStudentMaps)
                {
                    if (studentMap.HealthEntryStudentMapIID != 0)
                    {
                        dbContext.Entry(studentMap).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }
                    else
                    {
                        dbContext.Entry(studentMap).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    }
                }
                dbContext.SaveChanges();

                return ToDTOString(ToDTO(entity.HealthEntryIID));
            }
        }

        public List<HealthEntryStudentMapDTO> GetHealthEntryStudentData(int classId, int sectionId, int academicYearID, int examGroupID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var classStudentList = new List<HealthEntryStudentMapDTO>();
                var healthMapData = dbContext.HealthEntryStudentMaps
                    .Where(x => x.HealthEntry.ClassID == classId && x.HealthEntry.SectionID == sectionId && x.HealthEntry.AcademicYearID == academicYearID && x.HealthEntry.ExamGroupID == examGroupID)
                    .AsNoTracking().ToList();

                var students = dbContext.Students.Where(a => a.ClassID == classId && (sectionId == 0 || a.SectionID == sectionId) && a.IsActive == true && a.AcademicYearID == academicYearID)
                    .OrderBy(z => z.AdmissionNumber)
                    .AsNoTracking().ToList();

                foreach (var classStud in students)
                {
                    var filterDetails = healthMapData.Count > 0 ? healthMapData.Find(x => x.StudentID == classStud.StudentIID) : null;
                    classStudentList.Add(new HealthEntryStudentMapDTO
                    {
                        StudentID = classStud.StudentIID,
                        StudentName = classStud.AdmissionNumber + " - " + classStud.FirstName + " " + classStud.MiddleName + " " + classStud.LastName,
                        HealthEntryStudentMapIID = filterDetails != null ? filterDetails.HealthEntryStudentMapIID : 0,
                        Height = filterDetails != null ? filterDetails.Height : null,
                        Weight = filterDetails != null ? filterDetails.Weight : null,
                        BMS = filterDetails != null ? filterDetails.BMS : null,
                        Remarks = filterDetails != null ? filterDetails.Remarks : null,
                    });
                }

                return classStudentList;
            }
        }
    }
}