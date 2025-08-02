using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Extensions;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.School.Students;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Eduegate.Domain.Mappers.School.Academics
{
    public class TimeTableExtensionMapper : DTOEntityDynamicMapper
    {
        List<string> validationFields = new List<string>() { "ClassID" };

        public static TimeTableExtensionMapper Mapper(CallContext context)
        {
            var mapper = new TimeTableExtensionMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<TimeTableExtensionDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private TimeTableExtensionDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.TimeTableExtensions.Where(x => x.TimeTableExtIID == IID)
                    .Include(i => i.SubjectType)
                    .Include(i => i.TimeTableExtClasses).ThenInclude(i => i.Class)
                    .Include(i => i.TimeTableExtSections).ThenInclude(i => i.Section)
                    .Include(i => i.TimeTableExtSubjects).ThenInclude(i => i.Subject)
                    .Include(i => i.TimeTableExtTeachers).ThenInclude(i => i.Teacher)
                    .Include(i => i.TimeTableExtWeekDays).ThenInclude(i => i.WeekDay).ThenInclude(i => i.Day)
                    .Include(i => i.TimeTableExtClassTimings).ThenInclude(i => i.ClassTiming)
                    .Include(i => i.TimeTableExtWeekDays).ThenInclude(i => i.LogicalOperator)
                    .Include(i => i.TimeTableExtClassTimings).ThenInclude(i => i.LogicalOperator)
                    .Include(i => i.AcademicYear)
                    .Include(i => i.School)
                    .AsNoTracking().FirstOrDefault();

                var periodMaps = new TimeTableExtensionDTO()
                {
                    TimeTableExtIID = entity.TimeTableExtIID,
                    TimeTableID = entity.TimeTableID,
                    TimeTableExtName = entity.TimeTableExtName,
                    SubjectTypeID = entity.SubjectTypeID,
                    SubjectTypeName = entity.SubjectType?.TypeName,
                    PeriodCountWeek = entity.PeriodCountWeek.ToString(),
                    MaxPeriodCountDay = entity.MaxPeriodCountDay.ToString(),
                    MinPeriodCountDay = entity.MinPeriodCountDay.ToString(),
                    IsActive = entity.IsActive,
                    IsPeriodContinues = entity.IsPeriodContinues,

                    SchoolID = entity.SchoolID,
                    AcademicYearID = entity.AcademicYearID,

                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                };

                periodMaps.Class = new List<KeyValueDTO>();
                foreach (var classes in entity.TimeTableExtClasses)
                {
                    periodMaps.Class.Add(new KeyValueDTO()
                    {
                        Key = classes.ClassID.ToString(),
                        Value = classes.Class?.ClassDescription,
                    });
                }

                periodMaps.Section = new List<KeyValueDTO>();
                foreach (var section in entity.TimeTableExtSections)
                {
                    periodMaps.Section.Add(new KeyValueDTO()
                    {
                        Key = section.SectionID.ToString(),
                        Value = section.Section?.SectionName,
                    });
                }

                periodMaps.Subject = new List<KeyValueDTO>();
                foreach (var subject in entity.TimeTableExtSubjects)
                {
                    periodMaps.Subject.Add(new KeyValueDTO()
                    {
                        Key = subject.SubjectID.ToString(),
                        Value = subject.Subject?.SubjectName,
                    });
                }

                periodMaps.Teacher = new List<KeyValueDTO>();
                foreach (var teacher in entity.TimeTableExtTeachers)
                {
                    periodMaps.Teacher.Add(new KeyValueDTO()
                    {
                        Key = teacher.TeacherID.ToString(),
                        Value = teacher.Teacher?.EmployeeCode + " - " + teacher.Teacher?.FirstName + " " + teacher.Teacher?.MiddleName + " " + teacher.Teacher?.LastName,
                    });
                }

                periodMaps.ClassTiming = new List<KeyValueDTO>();
                foreach (var clssTime in entity.TimeTableExtClassTimings)
                {
                    periodMaps.ClassTiming.Add(new KeyValueDTO()
                    {
                        Key = clssTime.ClassTimingID.ToString(),
                        Value = clssTime.ClassTiming?.StartTime.HasValue == true && clssTime.ClassTiming?.EndTime.HasValue == true ? DateTime.Today.Add(clssTime.ClassTiming.StartTime.Value).ToString("hh:mm tt") + " - " + DateTime.Today.Add(clssTime.ClassTiming.EndTime.Value).ToString("hh:mm tt") : null,
                    });
                }

                var clsOptr = entity.TimeTableExtClassTimings?.FirstOrDefault()?.LogicalOperator;
                if (clsOptr.IsNotNull())
                {
                    periodMaps.ClassTimingOperator = new KeyValueDTO()
                    {
                        Key = clsOptr?.LogicalOperatorID.ToString(),
                        Value = clsOptr?.LogicalOperatorName,
                    };
                }

                periodMaps.WeekDay = new List<KeyValueDTO>();
                foreach (var weekDay in entity.TimeTableExtWeekDays)
                {
                    periodMaps.WeekDay.Add(new KeyValueDTO()
                    {
                        Key = weekDay.WeekDayID.ToString(),
                        Value = weekDay.WeekDay?.Day?.DayName,
                    });
                }

                var wkDyOptr = entity.TimeTableExtWeekDays?.FirstOrDefault()?.LogicalOperator;
                if (wkDyOptr.IsNotNull())
                {
                    periodMaps.WeekDayOperator = new KeyValueDTO()
                    {
                        Key = wkDyOptr.LogicalOperatorID.ToString(),
                        Value = wkDyOptr.LogicalOperatorName,
                    };
                }

                return periodMaps;
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as TimeTableExtensionDTO;
            var entity = new TimeTableExtension();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                //if (toDto.TimeTableID)
                //{
                //    throw new Exception("Please Select section");
                //}
                var classIds = toDto.Class.Select(x => int.Parse(x.Key)).ToList();
                var sectionIds = toDto.Section.Select(x => int.Parse(x.Key)).ToList();
                var subjectIds = toDto.Subject.Select(x => int.Parse(x.Key)).ToList();
                var teacherIds = toDto.Teacher.Select(x => int.Parse(x.Key)).ToList();

                var weekDayIds = toDto.WeekDay.Select(x => int.Parse(x.Key)).ToList();
                var classTimigIds = toDto.ClassTiming.Select(x => int.Parse(x.Key)).ToList();

                if (classIds.Count() <= 0 || subjectIds.Count() <= 0)
                {
                    throw new Exception("Select atleast one class, subject and teacher!");
                }

                if (classIds.Count() > sectionIds.Count())
                {
                    throw new Exception("Selected classes counts and sections should be equal!");
                }

                if (sectionIds.Count() < subjectIds.Count())
                {
                    throw new Exception("Selected subjects count should not be greater than sections counts!");
                }

                if (weekDayIds.Count() > 0 && toDto.WeekDayOperator.Key.IsNull())
                {
                    throw new Exception("Please select WeekDay Operator!");
                }

                if (classTimigIds.Count() > 0 && toDto.ClassTimingOperator.Key.IsNull())
                {
                    throw new Exception("Please select Class Timing Operator!");
                }

                if (toDto.TimeTableExtIID == 0)
                {
                    var existingExtension = dbContext.TimeTableExtensions
                    .Where(ext => ext.SubjectTypeID == toDto.SubjectTypeID && ext.TimeTableID == toDto.TimeTableID
                                  && ext.TimeTableExtClasses.Any(cls => classIds.Contains(cls.ClassID))
                                  && ext.TimeTableExtSections.Any(sct => sectionIds.Contains(sct.SectionID))
                                  && ext.TimeTableExtSubjects.Any(sub => subjectIds.Contains(sub.SubjectID))).ToList();

                    if (existingExtension.Any() && existingExtension != null)
                    {
                        throw new Exception("This mapping against this Subject Type and Time Table is already exists!");
                    }
                }

                //delete class maps
                if (toDto.TimeTableExtIID != 0)
                {
                    //class removal
                    var classIDs = toDto.Class
                        .Select(a => int.Parse(a.Key)).ToList();

                    var classEntities = dbContext.TimeTableExtClasses.Where(x => x.TimeTableExtID == toDto.TimeTableExtIID && !classIDs.Contains(x.ClassID)).AsNoTracking().ToList();

                    if (classEntities.IsNotNull())
                        dbContext.TimeTableExtClasses.RemoveRange(classEntities);

                    //section removal
                    var sectionIDs = toDto.Section
                        .Select(a => int.Parse(a.Key)).ToList();

                    var sectionEntities = dbContext.TimeTableExtSections.Where(x => x.TimeTableExtID == toDto.TimeTableExtIID && !sectionIDs.Contains(x.SectionID)).AsNoTracking().ToList();

                    if (sectionEntities.IsNotNull())
                        dbContext.TimeTableExtSections.RemoveRange(sectionEntities);

                    //subject removal
                    var subjectIDs = toDto.Subject.Select(s => int.Parse(s.Key)).ToList();

                    var subjectEntities = dbContext.TimeTableExtSubjects
                        .Where(x => x.TimeTableExtID == toDto.TimeTableExtIID && !subjectIDs.Contains(x.SubjectID))
                        .AsNoTracking()
                        .ToList();

                    if (subjectEntities != null && subjectEntities.Any())
                    {
                        dbContext.TimeTableExtSubjects.RemoveRange(subjectEntities);
                    }

                    //teacher removal
                    var teacherIDs = toDto.Teacher.Select(t => long.Parse(t.Key)).ToList();

                    var teacherEntities = dbContext.TimeTableExtTeachers
                        .Where(x => x.TimeTableExtID == toDto.TimeTableExtIID && !teacherIDs.Contains(x.TeacherID))
                        .AsNoTracking()
                        .ToList();

                    if (teacherEntities != null && teacherEntities.Any())
                    {
                        dbContext.TimeTableExtTeachers.RemoveRange(teacherEntities);
                    }

                    //weekday removal
                    var weekDayIDs = toDto.WeekDay.Select(w => int.Parse(w.Key)).ToList();
                    //var operatorIDs = toDto.WeekDayOperator.Select(o => int.Parse(o.Key)).ToList();

                    var weekDayEntities = dbContext.TimeTableExtWeekDays
                        .Where(x => x.TimeTableExtID == toDto.TimeTableExtIID && (!weekDayIDs.Contains(x.WeekDayID ?? 0) /*|| !operatorIDs.Contains(x.LogicalOperatorID ?? 0)*/)).AsNoTracking().ToList();

                    if (weekDayEntities != null && weekDayEntities.Any())
                    {
                        dbContext.TimeTableExtWeekDays.RemoveRange(weekDayEntities);
                    }

                    //classtiming removal
                    var classTimingIDs = toDto.ClassTiming.Select(c => int.Parse(c.Key)).ToList();
                    //var timingOperatorIDs = toDto.ClassTimingOperator.Select(o => int.Parse(o.Key)).ToList();

                    var classTimingEntities = dbContext.TimeTableExtClassTimings
                        .Where(x => x.TimeTableExtID == toDto.TimeTableExtIID && (!classTimingIDs.Contains(x.ClassTimingID ?? 0) /*|| !timingOperatorIDs.Contains(x.LogicalOperatorID ?? 0)*/)).AsNoTracking().ToList();

                    if (classTimingEntities != null && classTimingEntities.Any())
                    {
                        dbContext.TimeTableExtClassTimings.RemoveRange(classTimingEntities);
                    }

                }

                entity = new TimeTableExtension()
                {
                    TimeTableExtIID = toDto.TimeTableExtIID,
                    TimeTableExtName = toDto.TimeTableExtName,
                    TimeTableID = toDto.TimeTableID,
                    SubjectTypeID = toDto.SubjectTypeID,
                    MaxPeriodCountDay = toDto.MaxPeriodCountDay.IsNotNullOrEmpty() ? int.Parse(toDto.MaxPeriodCountDay) : null,
                    MinPeriodCountDay = toDto.MinPeriodCountDay.IsNotNullOrEmpty() ? int.Parse(toDto.MinPeriodCountDay) : null,
                    PeriodCountWeek = toDto.PeriodCountWeek.IsNotNullOrEmpty() ? int.Parse(toDto.PeriodCountWeek) : null,
                    IsActive = toDto.IsActive,
                    IsPeriodContinues = toDto.IsPeriodContinues,

                    SchoolID = toDto.SchoolID.HasValue ? toDto.SchoolID : Convert.ToByte(_context.SchoolID),
                    AcademicYearID = toDto.AcademicYearID.HasValue ? toDto.AcademicYearID : _context.AcademicYearID,

                    CreatedBy = toDto.TimeTableExtIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                    UpdatedBy = toDto.TimeTableExtIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                    CreatedDate = toDto.TimeTableExtIID == 0 ? DateTime.Now.Date : dto.CreatedDate,
                    UpdatedDate = toDto.TimeTableExtIID > 0 ? DateTime.Now.Date : dto.UpdatedDate,
                };

                // Classes
                entity.TimeTableExtClasses = new List<TimeTableExtClass>();
                foreach (var clss in toDto.Class)
                {
                    var alreadyExist = dbContext.TimeTableExtClasses.Where(x => x.TimeTableExtID == toDto.TimeTableExtIID && x.ClassID == int.Parse(clss.Key)).AsNoTracking().FirstOrDefault();

                    var timeTableExtClass = new TimeTableExtClass
                    {
                        TimeTableExtClassIID = alreadyExist != null ? alreadyExist.TimeTableExtClassIID : 0,
                        ClassID = int.Parse(clss.Key),
                        IsActive = toDto.IsActive
                    };

                    entity.TimeTableExtClasses.Add(timeTableExtClass);

                    if (timeTableExtClass.TimeTableExtClassIID == 0)
                    {
                        dbContext.Entry(timeTableExtClass).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    }
                    else
                    {
                        dbContext.Entry(timeTableExtClass).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }
                }

                // Sections
                entity.TimeTableExtSections = new List<TimeTableExtSection>();
                foreach (var section in toDto.Section)
                {
                    var alreadyExist = dbContext.TimeTableExtSections.Where(x => x.TimeTableExtID == toDto.TimeTableExtIID && x.SectionID == int.Parse(section.Key)).AsNoTracking().FirstOrDefault();

                    var timeTableExtSection = new TimeTableExtSection
                    {
                        TimeTableExtSectionIID = alreadyExist != null ? alreadyExist.TimeTableExtSectionIID : 0,
                        SectionID = int.Parse(section.Key),
                        IsActive = toDto.IsActive
                    };

                    entity.TimeTableExtSections.Add(timeTableExtSection);

                    if (timeTableExtSection.TimeTableExtSectionIID == 0)
                    {
                        dbContext.Entry(timeTableExtSection).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    }
                    else
                    {
                        dbContext.Entry(timeTableExtSection).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }
                }

                // Subjects
                entity.TimeTableExtSubjects = new List<TimeTableExtSubject>();

                foreach (var subject in toDto.Subject)
                {
                    var alreadyExist = dbContext.TimeTableExtSubjects.Where(x => x.TimeTableExtID == toDto.TimeTableExtIID && x.SubjectID == int.Parse(subject.Key)).AsNoTracking().FirstOrDefault();

                    var timeTableExtSubject = new TimeTableExtSubject
                    {
                        TimeTableExtSubjectIID = alreadyExist != null ? alreadyExist.TimeTableExtSubjectIID : 0,
                        SubjectID = int.Parse(subject.Key),
                        IsActive = toDto.IsActive
                    };

                    entity.TimeTableExtSubjects.Add(timeTableExtSubject);

                    if (timeTableExtSubject.TimeTableExtSubjectIID == 0)
                    {
                        dbContext.Entry(timeTableExtSubject).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    }
                    else
                    {
                        dbContext.Entry(timeTableExtSubject).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }
                }

                // Teachers
                entity.TimeTableExtTeachers = new List<TimeTableExtTeacher>();

                foreach (var teacher in toDto.Teacher)
                {
                    var alreadyExist = dbContext.TimeTableExtTeachers.Where(x => x.TimeTableExtID == toDto.TimeTableExtIID && x.TeacherID == int.Parse(teacher.Key)).AsNoTracking().FirstOrDefault();

                    var timeTableExtTeacher = new TimeTableExtTeacher
                    {
                        TimeTableExtTeacherIID = alreadyExist != null ? alreadyExist.TimeTableExtTeacherIID : 0,
                        TeacherID = int.Parse(teacher.Key),
                        IsActive = toDto.IsActive
                    };

                    entity.TimeTableExtTeachers.Add(timeTableExtTeacher);

                    if (timeTableExtTeacher.TimeTableExtTeacherIID == 0)
                    {
                        dbContext.Entry(timeTableExtTeacher).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    }
                    else
                    {
                        dbContext.Entry(timeTableExtTeacher).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }
                }

                // WeekDays
                entity.TimeTableExtWeekDays = new List<TimeTableExtWeekDay>();

                foreach (var weekDay in toDto.WeekDay)
                {
                    var oprtr = toDto.WeekDayOperator;
                    var alreadyExist = dbContext.TimeTableExtWeekDays.Where(x => x.TimeTableExtID == toDto.TimeTableExtIID && x.WeekDayID == int.Parse(weekDay.Key) && x.LogicalOperatorID == int.Parse(oprtr.Key)).AsNoTracking().FirstOrDefault();

                    var timeTableExtWeekDay = new TimeTableExtWeekDay
                    {
                        TimeTableExtWeekDayIID = alreadyExist.IsNotNull() ? alreadyExist.TimeTableExtWeekDayIID : 0,
                        WeekDayID = int.Parse(weekDay.Key),
                        LogicalOperatorID = int.Parse(oprtr.Key),
                        IsActive = toDto.IsActive
                    };

                    entity.TimeTableExtWeekDays.Add(timeTableExtWeekDay);
                    if (timeTableExtWeekDay.TimeTableExtWeekDayIID == 0)
                    {
                        dbContext.Entry(timeTableExtWeekDay).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    }
                    else
                    {
                        dbContext.Entry(timeTableExtWeekDay).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }
                }

                // ClassTiming
                entity.TimeTableExtClassTimings = new List<TimeTableExtClassTiming>();

                foreach (var classTiming in toDto.ClassTiming)
                {
                    var oprtr = toDto.ClassTimingOperator;
                    var alreadyExist = dbContext.TimeTableExtClassTimings.Where(x => x.TimeTableExtID == toDto.TimeTableExtIID && x.ClassTimingID == int.Parse(classTiming.Key) && x.LogicalOperatorID == int.Parse(oprtr.Key)).AsNoTracking().FirstOrDefault();

                    var timeTableExtClassTiming = new TimeTableExtClassTiming
                    {
                        TimeTableExtClassTimingIID = alreadyExist.IsNotNull() ? alreadyExist.TimeTableExtClassTimingIID : 0,
                        ClassTimingID = int.Parse(classTiming.Key),
                        LogicalOperatorID = int.Parse(oprtr.Key),
                        IsActive = toDto.IsActive
                    };

                    entity.TimeTableExtClassTimings.Add(timeTableExtClassTiming);

                    if (timeTableExtClassTiming.TimeTableExtClassTimingIID == 0)
                    {
                        dbContext.Entry(timeTableExtClassTiming).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    }
                    else
                    {
                        dbContext.Entry(timeTableExtClassTiming).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }
                }


                dbContext.TimeTableExtensions.Add(entity);

                if (entity.TimeTableExtIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();

                return ToDTOString(ToDTO(entity.TimeTableExtIID));
            }
        }

    }
}