using Eduegate.Domain.Entity;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Extensions;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.Payroll;
using Eduegate.Services.Contracts.School.Students;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Eduegate.Domain.Mappers.School.Students
{
    public class ClassSubjectPeriodMapMapper : DTOEntityDynamicMapper
    {
        List<string> validationFields = new List<string>() { "ClassID" };

        public static ClassSubjectPeriodMapMapper Mapper(CallContext context)
        {
            var mapper = new ClassSubjectPeriodMapMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<ClassSectionSubjectPeriodMapDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private ClassSectionSubjectPeriodMapDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var mainData = dbContext.ClassSectionSubjectPeriodMaps.Where(x => x.PeriodMapIID == IID)
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .Include(i => i.AcademicYear)
                    .AsNoTracking().FirstOrDefault();

                var entity = dbContext.ClassSectionSubjectPeriodMaps.Where(x => x.ClassID == mainData.ClassID && x.SectionID == mainData.SectionID && x.AcademicYearID == _context.AcademicYearID)
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .Include(i => i.Subject)
                    .AsNoTracking()
                    .ToList();

                var periodMaps = new ClassSectionSubjectPeriodMapDTO()
                {
                    PeriodMapIID = mainData.PeriodMapIID,
                    ClassID = mainData.ClassID,
                    ClassName = mainData.ClassID.HasValue ? mainData.Class?.ClassDescription : null,
                    SectionID = mainData.SectionID,
                    SectionName = mainData.SectionID.HasValue ? mainData.Section?.SectionName : null,
                    SchoolID = mainData.SchoolID,
                    AcademicYearID = mainData.AcademicYearID,
                    AcademicYearName = mainData.AcademicYearID.HasValue ? mainData.AcademicYear?.Description : null,
                    CreatedBy = mainData.CreatedBy,
                    UpdatedBy = mainData.UpdatedBy,
                    CreatedDate = mainData.CreatedDate,
                    UpdatedDate = mainData.UpdatedDate,
                };

                periodMaps.SubjectMapDetails = new List<ClassSectionSubjectPeriodMapMapDTO>();

                foreach (var subject in entity)
                {
                    periodMaps.SubjectMapDetails.Add(new ClassSectionSubjectPeriodMapMapDTO()
                    {
                        PeriodMapIID = subject.PeriodMapIID,
                        Subject = new KeyValueDTO()
                        {
                            Key = subject.SubjectID.ToString(),
                            Value = subject.SubjectID.HasValue ? subject.Subject?.SubjectName : null,
                        },
                        WeekPeriods = subject.WeekPeriods,
                        TotalPeriods = subject.TotalPeriods,
                        MinimumPeriods = subject.MinimumPeriods,
                        MaximumPeriods = subject.MaximumPeriods,
                    });
                }

                return periodMaps;
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as ClassSectionSubjectPeriodMapDTO;
            var entity = new ClassSectionSubjectPeriodMap();

            //var toDto = dto as ClassAssociateTeacherMapDTO;
            using (var dbContext = new dbEduegateSchoolContext())
            {
                if (toDto.ClassID == null || toDto.ClassID == 0)
                {
                    throw new Exception("Please Select class");
                }

                if (toDto.SectionID == null || toDto.SectionID == 0)
                {
                    throw new Exception("Please Select section");
                }

                var existing = dbContext.ClassSectionSubjectPeriodMaps.Where(a => a.ClassID == toDto.ClassID && a.SectionID == toDto.SectionID 
                && a.AcademicYearID == (toDto.AcademicYearID.IsNotNull() ? toDto.AcademicYearID : _context.AcademicYearID)).AsNoTracking().ToList();

                if (toDto.PeriodMapIID == 0 && existing.Count() > 0)
                {
                    throw new Exception("Please Select section");
                }

                var IIDs = toDto.SubjectMapDetails
                    .Select(a => a.PeriodMapIID).ToList();

                //delete maps
                var entities = dbContext.ClassSectionSubjectPeriodMaps.Where(x =>
                    x.ClassID == toDto.ClassID && x.SectionID == toDto.SectionID && x.AcademicYearID == (toDto.AcademicYearID.IsNotNull() ? toDto.AcademicYearID : _context.AcademicYearID) 
                    && !IIDs.Contains(x.PeriodMapIID)).AsNoTracking().ToList();

                if (entities.IsNotNull())
                    dbContext.ClassSectionSubjectPeriodMaps.RemoveRange(entities);

                foreach (var map in toDto.SubjectMapDetails)
                {
                    var subjectDetails = dbContext.Subjects.Where(x => x.SubjectID == map.SubjectID).FirstOrDefault();

                    entity = new ClassSectionSubjectPeriodMap()
                    {
                        PeriodMapIID = map.PeriodMapIID,
                        ClassID = toDto.ClassID,
                        SectionID = toDto.SectionID,
                        SchoolID = toDto.SchoolID.HasValue ? toDto.SchoolID : Convert.ToByte(_context.SchoolID),
                        AcademicYearID = toDto.AcademicYearID.HasValue ? toDto.AcademicYearID : _context.AcademicYearID,

                        SubjectID = map.SubjectID,
                        SubjectTypeID = subjectDetails?.SubjectTypeID,
                        WeekPeriods = map.WeekPeriods,
                        TotalPeriods = map.TotalPeriods,
                        MinimumPeriods = map.MinimumPeriods,
                        MaximumPeriods = map.MaximumPeriods,

                        CreatedBy = toDto.PeriodMapIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                        UpdatedBy = toDto.PeriodMapIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                        CreatedDate = toDto.PeriodMapIID == 0 ? DateTime.Now.Date : dto.CreatedDate,
                        UpdatedDate = toDto.PeriodMapIID > 0 ? DateTime.Now.Date : dto.UpdatedDate,
                        //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
                    };

                    dbContext.ClassSectionSubjectPeriodMaps.Add(entity);

                    if (entity.PeriodMapIID == 0)
                    {
                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    }
                    else
                    {
                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }

                    dbContext.SaveChanges();

                }

                #region OLD_CODE
                //if (entity.PeriodMapIID == 0)
                //{
                //    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                //}
                //else
                //{
                //using (var dbContext1 = new dbEduegateSchoolContext())
                //{
                //    var classAssociateTeacherMaps = dbContext1.ClassAssociateTeacherMaps.Where(x => x.ClassClassTeacherMapID == entity.ClassClassTeacherMapIID).AsNoTracking().ToList();
                //    if (classAssociateTeacherMaps != null && classAssociateTeacherMaps.Count() > 0)
                //    {
                //        dbContext1.ClassAssociateTeacherMaps.RemoveRange(classAssociateTeacherMaps);
                //        dbContext1.SaveChanges();
                //    }
                //}

                //if (entity.ClassTeacherMaps.Count > 0)
                //{
                //    foreach (var teacher in entity.ClassTeacherMaps)
                //    {
                //        if (teacher.ClassTeacherMapIID == 0)
                //        {
                //            dbContext.Entry(teacher).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                //        }
                //        else
                //        {
                //            dbContext.Entry(teacher).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                //        }
                //    }
                //}

                //if (entity.ClassAssociateTeacherMaps.Count > 0)
                //{
                //    foreach (var associateteacher in entity.ClassAssociateTeacherMaps)
                //    {
                //        if (associateteacher.ClassAssociateTeacherMapIID == 0)
                //        {
                //            dbContext.Entry(associateteacher).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                //        }
                //        else
                //        {
                //            dbContext.Entry(associateteacher).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                //        }
                //    }
                //}

                //    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                //}
                #endregion


                return ToDTOString(ToDTO(entity.PeriodMapIID));
            }
        }

    }
}