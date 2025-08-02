using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Mappers.School.Students;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Extensions;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.School.Exams;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eduegate.Domain.Mappers.School.Exams
{
    public class ExamMapper : DTOEntityDynamicMapper
    {
        public static ExamMapper Mapper(CallContext context)
        {
            var mapper = new ExamMapper();
            mapper._context = context;
            return mapper;
        }
        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<ExamDTO>(entity);
        }
        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }
        public override string GetEntity(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.Exams.Where(a => a.ExamIID == IID)
                    .Include(i => i.ExamType)
                    .Include(i => i.ExamGroup)
                    .Include(i => i.School)
                    .Include(i => i.AcademicYear)
                    .Include(i => i.ExamSubjectMaps).ThenInclude(i => i.MarkGrade)
                    .Include(i => i.ExamSubjectMaps).ThenInclude(i => i.Subject).ThenInclude(i => i.SubjectType)
                    .Include(i => i.ExamClassMaps).ThenInclude(i => i.Class)
                    .Include(i => i.ExamClassMaps).ThenInclude(i => i.Section)
                    .Include(i => i.ExamSkillMaps).ThenInclude(i => i.ClassSubjectSkillGroupMap)
                    .AsNoTracking()
                    .FirstOrDefault();

                var examdetail = new ExamDTO()
                {
                    ExamIID = entity.ExamIID,
                    ExamDescription = entity.ExamDescription != null ? entity.ExamDescription : null,
                    ProgressCardHeader = entity.ProgressCardHeader,
                    IsActive = entity.IsActive,
                    ExamTypeID = entity.ExamTypeID.HasValue ? entity.ExamTypeID : null,
                    //MarkGradeID = entity.MarkGradeID,
                    //ExamTypeName=entity.ExamTypeName != null ? entity.ExamTypeName.Description : null,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    AcademicYearID = entity.AcademicYearID,
                    SchoolID = entity.SchoolID,
                    //TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),
                    ExamGroupID = entity.ExamGroupID != null ? entity.ExamGroupID : null,
                };

                //foreach (var examsch in entity.ExamSchedules)                   //ExamScheduleContainer
                //{
                //    examdetail.ExamSchedules = new ExamScheduleDTO()
                //    {
                //        ExamScheduleIID = examsch.ExamScheduleIID,
                //        ExamID = examsch.ExamID.HasValue ? examsch.ExamID : null,
                //        //Date = examsch.Date,
                //        ExamStartDate = examsch.ExamStartDate.HasValue ? examsch.ExamStartDate : null,
                //        ExamEndDate = examsch.ExamEndDate.HasValue ? examsch.ExamEndDate : null,
                //        StartTime = examsch.StartTime.HasValue ? examsch.StartTime : null,
                //        EndTime = examsch.EndTime.HasValue ? examsch.EndTime : null,
                //        Room = examsch.Room != null ? examsch.Room : null,
                //        FullMarks = examsch.FullMarks != null ? examsch.FullMarks : null,
                //        PassingMarks = examsch.PassingMarks != null ? examsch.PassingMarks : null,
                //        CreatedBy = examsch.CreatedBy,
                //        UpdatedBy = examsch.UpdatedBy,
                //        CreatedDate = examsch.CreatedDate,
                //        UpdatedDate = examsch.UpdatedDate,
                //    };
                //}

                examdetail.ExamSubjects = new List<ExamSubjectDTO>();

                foreach (var exmEntity in entity.ExamSubjectMaps)
                {
                    examdetail.ExamSubjects.Add(new ExamSubjectDTO()
                    {
                        ExamSubjectMapIID = exmEntity.ExamSubjectMapIID,
                        ExamID = exmEntity.ExamID.HasValue ? exmEntity.ExamID : null,
                        ExamDate = exmEntity.ExamDate.HasValue ? exmEntity.ExamDate : null,
                        SubjectID = exmEntity.SubjectID.HasValue ? exmEntity.SubjectID : null,
                        Subject = exmEntity.Subject != null ? exmEntity.Subject.SubjectName : null,
                        MarkGradeID = exmEntity.MarkGradeID.HasValue ? exmEntity.MarkGradeID : null,
                        MarkGrade = exmEntity.MarkGrade != null ? exmEntity.MarkGrade.Description : null,
                        MinimumMarks = exmEntity.MinimumMarks != null ? exmEntity.MinimumMarks : null,
                        MaximumMarks = exmEntity.MaximumMarks != null ? exmEntity.MaximumMarks : null,
                        StartTime = exmEntity.StartTime.HasValue ? exmEntity.StartTime : null,
                        EndTime = exmEntity.EndTime.HasValue ? exmEntity.EndTime : null,
                        SubjectType = exmEntity.SubjectID.HasValue ? exmEntity.Subject != null && exmEntity.Subject.SubjectType != null ? new KeyValueDTO()
                        {
                            Key = exmEntity.Subject.SubjectType.SubjectTypeID.ToString(),
                            Value = exmEntity.Subject.SubjectType.TypeName
                        } : GetSubjectTypeBySubject(exmEntity.SubjectID).SubjectType : new KeyValueDTO(),
                        CreatedBy = exmEntity.CreatedBy,
                        UpdatedBy = exmEntity.UpdatedBy,
                        CreatedDate = exmEntity.CreatedDate,
                        UpdatedDate = exmEntity.UpdatedDate,
                        ConversionFactor = exmEntity.ConversionFactor,
                    });
                }

                examdetail.ExamSkillMaps = new List<ExamSkillMapDTO>();

                foreach (var map in entity.ExamSkillMaps)
                {
                    examdetail.ExamSkillMaps.Add(new ExamSkillMapDTO()
                    {
                        ExamSkillMapIID = map.ExamSkillMapIID,
                        ExamID = map.ExamID,
                        ClassSubjectSkillGroupMapID = map.ClassSubjectSkillGroupMapID,
                        //SkillGroupMasterID = map.SkillGroupMasterID,
                        //SkillGroup = map.SkillGroupMasterID.HasValue ? new KeyValueDTO() { Key = map.SkillGroupMasterID.Value.ToString(), Value = map.SkillGroupMaster.SkillGroup } : null,
                        SkillSet = map.ClassSubjectSkillGroupMapID.HasValue ? new KeyValueDTO() { Key = map.ClassSubjectSkillGroupMapID.Value.ToString(), Value = map.ClassSubjectSkillGroupMap.Description } : null,
                        CreatedBy = map.CreatedBy,
                        CreatedDate = map.CreatedDate,
                        UpdatedBy = map.UpdatedBy,
                        UpdatedDate = map.UpdatedDate,
                    });
                }

                examdetail.ExamClasses = new List<ExamClassDTO>();

                foreach (var examClass in entity.ExamClassMaps)
                {
                    examdetail.ExamClasses.Add(new ExamClassDTO()
                    {
                        ExamID = examClass.ExamID.HasValue ? examClass.ExamID : null,
                        ClassID = examClass.ClassID.HasValue ? examClass.ClassID : null,
                        SectionID = examClass.SectionID.HasValue ? examClass.SectionID : null,
                        //Section = examClass.Section.SectionName,
                        Section = examClass.Section != null ? examClass.Section.SectionName : null,
                        Class = examClass.Class.ClassDescription != null ? examClass.Class.ClassDescription : null,
                        ExamClassMapIID = examClass.ExamClassMapIID,
                        CreatedBy = examClass.CreatedBy,
                        UpdatedBy = examClass.UpdatedBy,
                        CreatedDate = examClass.CreatedDate,
                        UpdatedDate = examClass.UpdatedDate,
                    });
                }

                return ToDTOString(examdetail);
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as ExamDTO;
            // Date Different Check
            //if (toDto.ExamSchedules.ExamStartDate >= toDto.ExamSchedules.ExamEndDate)
            //{
            //    throw new Exception("Select Date Properlly!!");
            //}

            // Time Different Check
            //if (toDto.ExamSchedules.StartTime >= toDto.ExamSchedules.EndTime)
            //{
            //    throw new Exception("Select Time Properlly!!");
            //}

            //convert the dto to entity and pass to the repository.
            using (var dbContext = new dbEduegateSchoolContext())
            {
                if (toDto.ExamClasses.Count == 0)
                {
                    throw new Exception("Please Select atleast one Class");
                }
                //if (toDto.ExamSubjects.Count == 0)
                //{
                //    throw new Exception("Please Fill atleast one Exam Date");
                //}

                var entity = new Exam()
                {
                    ExamIID = toDto.ExamIID,
                    IsActive = toDto.IsActive,
                    ExamTypeID = toDto.ExamTypeID,
                    ExamGroupID = toDto.ExamGroupID,
                    //MarkGradeID = toDto.MarkGradeID,
                    ExamDescription = toDto.ExamDescription,
                    ProgressCardHeader = toDto.ProgressCardHeader,
                    SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                    AcademicYearID = toDto.AcademicYearID == null ? (int)_context.AcademicYearID : toDto.AcademicYearID,
                    CreatedBy = toDto.ExamIID == 0 ? (int)_context.LoginID : toDto.CreatedBy,
                    UpdatedBy = toDto.ExamIID > 0 ? (int)_context.LoginID : toDto.UpdatedBy,
                    CreatedDate = toDto.ExamIID == 0 ? DateTime.Now : toDto.CreatedDate,
                    UpdatedDate = toDto.ExamIID > 0 ? DateTime.Now : toDto.UpdatedDate,
                    //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),

                    //ExamGroupID = toDto.ExamGroupID != null ? toDto.ExamGroupID : 0,
                    //ExamGroupName = toDto.ExamGroupName
                };

                entity.ExamSkillMaps = new List<ExamSkillMap>();

                foreach (var skillMap in toDto.ExamSkillMaps)
                {
                    entity.ExamSkillMaps.Add(new ExamSkillMap()
                    {
                        ExamSkillMapIID = skillMap.ExamSkillMapIID,
                        //SkillGroupMasterID = skillMap.SkillGroupMasterID,
                        ClassSubjectSkillGroupMapID = skillMap.ClassSubjectSkillGroupMapID,
                        ExamID = toDto.ExamIID,
                        CreatedBy = skillMap.ExamSkillMapIID == 0 ? Convert.ToInt32(_context.LoginID) : skillMap.CreatedBy,
                        CreatedDate = skillMap.ExamSkillMapIID == 0 ? DateTime.Now : skillMap.CreatedDate,
                        UpdatedBy = skillMap.ExamSkillMapIID != 0 ? Convert.ToInt32(_context.LoginID) : skillMap.UpdatedBy,
                        UpdatedDate = skillMap.ExamSkillMapIID != 0 ? DateTime.Now : skillMap.UpdatedDate,
                    });
                }

                //entity.ExamSchedules = new List<ExamSchedule>();

                //entity.ExamSchedules.Add(new ExamSchedule()
                //{
                //    ExamScheduleIID = toDto.ExamSchedules.ExamScheduleIID,
                //    ExamID = toDto.ExamIID,
                //    //Date = toDto.ExamSchedules.Date,
                //    ExamStartDate = toDto.ExamSchedules.ExamStartDate,
                //    ExamEndDate = toDto.ExamSchedules.ExamEndDate,
                //    StartTime = toDto.ExamSchedules.StartTime,
                //    EndTime = toDto.ExamSchedules.EndTime,
                //    Room = toDto.ExamSchedules.Room,
                //    FullMarks = toDto.ExamSchedules.FullMarks,
                //    PassingMarks = toDto.ExamSchedules.PassingMarks,
                //    CreatedBy = toDto.ExamSchedules.ExamScheduleIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                //    UpdatedBy = toDto.ExamSchedules.ExamScheduleIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                //    CreatedDate = toDto.ExamSchedules.ExamScheduleIID == 0 ? DateTime.Now : dto.CreatedDate,
                //    UpdatedDate = toDto.ExamSchedules.ExamScheduleIID > 0 ? DateTime.Now : dto.UpdatedDate,
                //});
                // grid remove
                var IIDs = toDto.ExamSubjects
                .Select(a => a.ExamSubjectMapIID).ToList();
                //delete maps
                var entities = dbContext.ExamSubjectMaps.Where(x =>
                    x.ExamID == entity.ExamIID &&
                    !IIDs.Contains(x.ExamSubjectMapIID)).AsNoTracking().ToList();

                if (entities.IsNotNull())
                    dbContext.ExamSubjectMaps.RemoveRange(entities);

                entity.ExamSubjectMaps = new List<ExamSubjectMap>();

                foreach (var exmsub in toDto.ExamSubjects)
                {
                    //if (exmsub.ExamDate == null)
                    //{
                    //    throw new Exception("Please Select a Exam Date");
                    //}

                    // Time Different Check
                    //if (exmsub.StartTime >= exmsub.EndTime)
                    //{
                    //    throw new Exception("Select Time Properlly!!");
                    //}

                    if (exmsub.SubjectID == null || exmsub.SubjectID == 0)
                    {
                        throw new Exception("Please Select a Subject");
                    }

                    if (exmsub.MarkGradeID == null || exmsub.MarkGradeID == 0)
                    {
                        throw new Exception("Please Select a Mark Grade");
                    }

                    //if (exmsub.StartTime == null)
                    //{
                    //    throw new Exception("Please Select Exam Start Time in Subject");
                    //}

                    //if (exmsub.EndTime == null)
                    //{
                    //    throw new Exception("Please Select Exam End Time in Subject");
                    //}
                    //entity.ExamSubjectMaps = new List<ExamSubjectMap>();

                    entity.ExamSubjectMaps.Add(new ExamSubjectMap()
                    {
                        ExamSubjectMapIID = exmsub.ExamSubjectMapIID,
                        ExamID = toDto.ExamIID,
                        ExamDate = exmsub.ExamDate,
                        SubjectID = exmsub.SubjectID,
                        MarkGradeID = exmsub.MarkGradeID,
                        MinimumMarks = exmsub.MinimumMarks,
                        MaximumMarks = exmsub.MaximumMarks,
                        StartTime = exmsub.StartTime,
                        EndTime = exmsub.EndTime,
                        CreatedBy = exmsub.ExamSubjectMapIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                        UpdatedBy = exmsub.ExamSubjectMapIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                        CreatedDate = exmsub.ExamSubjectMapIID == 0 ? DateTime.Now : dto.CreatedDate,
                        UpdatedDate = exmsub.ExamSubjectMapIID > 0 ? DateTime.Now : dto.UpdatedDate,
                        //TimeStamps = exmsub.TimeStamps == null ? null : Convert.FromBase64String(exmsub.TimeStamps),
                        ConversionFactor = exmsub.ConversionFactor,
                    });

                }

                // grid remove
                var CIIDs = toDto.ExamClasses
                .Select(a => a.ExamClassMapIID).ToList();
                //delete maps
                var clsentities = dbContext.ExamClassMaps.Where(x =>
                    x.ExamID == entity.ExamIID &&
                    !CIIDs.Contains(x.ExamClassMapIID)).AsNoTracking().ToList();

                if (clsentities.IsNotNull())
                    dbContext.ExamClassMaps.RemoveRange(clsentities);

                entity.ExamClassMaps = new List<ExamClassMap>();

                foreach (var examClass in toDto.ExamClasses)
                {
                    if (examClass.ClassID == null || examClass.ClassID == 0)
                    {
                        throw new Exception("Please Select a Class");
                    }
                    entity.ExamClassMaps.Add(new ExamClassMap()
                    {
                        ExamID = toDto.ExamIID,
                        ClassID = examClass.ClassID,
                        SectionID = examClass.SectionID,
                        ExamClassMapIID = examClass.ExamClassMapIID,
                        //ExamScheduleID = toDto.ExamSchedules.ExamScheduleIID,
                        CreatedBy = examClass.ExamClassMapIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                        UpdatedBy = examClass.ExamClassMapIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                        CreatedDate = examClass.ExamClassMapIID == 0 ? DateTime.Now : dto.CreatedDate,
                        UpdatedDate = examClass.ExamClassMapIID > 0 ? DateTime.Now : dto.UpdatedDate,
                    });
                }

                dbContext.Exams.Add(entity);

                if (entity.ExamIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    var mapEntity = dbContext.ExamSkillMaps.Where(X => X.ExamID == toDto.ExamIID).AsNoTracking().ToList();
                    if (mapEntity != null || mapEntity.Count > 0)
                    {
                        dbContext.ExamSkillMaps.RemoveRange(mapEntity);
                    }

                    //foreach (var examschedulemap in entity.ExamSchedules)
                    //{
                    //    if (examschedulemap.ExamScheduleIID != 0)
                    //    {
                    //        dbContext.Entry(examschedulemap).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    //    }
                    //    else
                    //    {
                    //        dbContext.Entry(examschedulemap).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    //    }
                    //}

                    foreach (var examsubjemap in entity.ExamSubjectMaps)
                    {
                        if (examsubjemap.ExamSubjectMapIID != 0)
                        {
                            dbContext.Entry(examsubjemap).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                        else
                        {
                            dbContext.Entry(examsubjemap).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                    }

                    foreach (var examClass in entity.ExamClassMaps)
                    {
                        if (examClass.ExamClassMapIID != 0)
                        {
                            dbContext.Entry(examClass).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                        else
                        {
                            dbContext.Entry(examClass).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                    }

                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                }
                dbContext.SaveChanges();

                return GetEntity(entity.ExamIID);
            }
        }

        public List<ExamDTO> GetExamLists(long studentId)
        {
            var examList = new List<ExamDTO>();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var examDTO = (from exmcls in dbContext.ExamClassMaps
                               join exm in dbContext.Exams on exmcls.ExamID equals exm.ExamIID
                               join stud in dbContext.Students on exmcls.ClassID equals stud.ClassID
                               where stud.StudentIID == studentId && exmcls.SectionID == stud.SectionID && exmcls.ClassID == stud.ClassID && exm.IsActive == true
                               orderby exmcls.ExamID descending
                               select exmcls).AsNoTracking().ToList();

                examList = examDTO.Select(examListGroup => new ExamDTO()
                {
                    ExamIID = examListGroup.Exam.ExamIID,
                    ExamDescription = examListGroup.Exam.ExamDescription,
                    ExamTypeID = examListGroup.Exam.ExamTypeID,
                    ExamTypeName = examListGroup.Exam.ExamType.ExamTypeDescription,
                    ExamSubjects = (from sub in examListGroup.Exam.ExamSubjectMaps

                                    select new ExamSubjectDTO()
                                    {
                                        ExamSubjectMapIID = sub.ExamSubjectMapIID,
                                        ExamID = sub.ExamID,
                                        ExamDate = sub.ExamDate,
                                        SubjectID = sub.SubjectID,
                                        MinimumMarks = sub.MinimumMarks,
                                        MaximumMarks = sub.MaximumMarks,
                                        StartTime = sub.StartTime,
                                        EndTime = sub.EndTime,
                                        Subject = sub.Subject.SubjectName,
                                    }).ToList(),
                }).ToList();
            }

            return examList;
        }

        public decimal GetExamCount(long parentID)
        {
            var examList = new List<ExamDTO>();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var examDatas = (from exmcls in dbContext.ExamClassMaps
                                 join exm in dbContext.Exams on exmcls.ExamID equals exm.ExamIID
                                 join stud in dbContext.Students on exmcls.ClassID equals stud.ClassID
                                 //join exmsh in dbContext.ExamSchedules on exmcls.ExamID equals exmsh.ExamID
                                 where stud.ParentID == parentID && exmcls.SectionID == stud.SectionID && exmcls.ClassID == stud.ClassID && exm.IsActive == true
                                 select exmcls).AsNoTracking().ToList();

                return examDatas != null ? examDatas.Count() : 0;
            }
        }

        public ExamDTO GetClassByExam(int examId)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.Exams.Where(x => x.ExamIID == examId)
                    .Include(i => i.ExamClassMaps).ThenInclude(i => i.Class)
                    .Include(i => i.ExamClassMaps).ThenInclude(i => i.Section)
                    .Include(i => i.ExamSubjectMaps).ThenInclude(i => i.Subject)
                    .Include(i => i.ExamSubjectMaps).ThenInclude(i => i.MarkGrade)
                    .Include(i => i.ExamSkillMaps)
                    .AsNoTracking()
                    .FirstOrDefault();

                var examdetail = new ExamDTO()
                {
                    ExamDescription = entity.ExamDescription
                };

                examdetail.ExamSubjects = new List<ExamSubjectDTO>();
                foreach (var exmEntity in entity.ExamSubjectMaps)
                {
                    examdetail.ExamSubjects.Add(new ExamSubjectDTO()
                    {
                        ExamID = exmEntity.ExamID,
                        ExamDate = exmEntity.ExamDate,
                        SubjectID = exmEntity.SubjectID,
                        MaximumMarks = exmEntity.MaximumMarks,
                        MinimumMarks = exmEntity.MinimumMarks,
                        Subject = exmEntity.Subject.SubjectName,
                        MarkGrade = exmEntity.MarkGrade.Description,
                        MarkGradeID = exmEntity.MarkGrade.MarkGradeIID,
                    });
                }

                examdetail.ExamClasses = new List<ExamClassDTO>();
                foreach (var examClass in entity.ExamClassMaps)
                {
                    if (!examdetail.ExamClassList.Exists(x => x.Key.ToString() == examClass.ClassID.ToString()))
                    {
                        examdetail.ExamClassList.Add(new KeyValueDTO()
                        {
                            Key = examClass.ClassID.ToString(),
                            Value = examClass.Class.ClassDescription,

                        });
                    }
                }

                foreach (var examClass in entity.ExamClassMaps)
                {
                    examdetail.ExamClasses.Add(new ExamClassDTO()
                    {
                        ClassID = examClass.ClassID,
                        Class = examClass.Class.ClassDescription,
                        SectionID = examClass.SectionID,
                        Section = examClass.Section.SectionName
                    });
                }

                foreach (var examClass in entity.ExamClassMaps)
                {
                    examdetail.ExamSectionList.Add(new KeyValueDTO()
                    {
                        Key = examClass.SectionID.ToString(),
                        Value = examClass.Section.SectionName
                    });
                }

                foreach (var examsub in entity.ExamSubjectMaps)
                {
                    examdetail.ExamSubjectList.Add(new KeyValueDTO()
                    {
                        Key = examsub.SubjectID.ToString(),
                        Value = examsub.Subject.SubjectName
                    });
                }

                examdetail.ExamStudentList = new StudentMapper().GetStudentByExamClass(examdetail.ExamClasses);

                return examdetail;
            }
        }

        public List<KeyValueDTO> GetExamsByTermID(MarkEntrySearchArgsDTO argsDTO)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var examList = new List<KeyValueDTO>();
                var includeTExamType = 0;
                var examType = 0;
                if (argsDTO.tabType == "Scholastic")
                {
                    includeTExamType = 4;
                    examType = 1;
                }
                else if (argsDTO.tabType == "Co-Scholastic")
                {
                    includeTExamType = 2;
                    examType = 2;
                }
                else if (argsDTO.tabType == "Scholastic Internals")
                {
                    includeTExamType = 3;
                    examType = 3;
                }
                examList = (from s in dbContext.Exams
                            join ec in dbContext.ExamClassMaps on s.ExamIID equals ec.ExamID
                            where s.IsActive == true && s.AcademicYearID == argsDTO.AcademicYearID
                            && s.ExamGroupID == argsDTO.TermID && ec.ClassID == argsDTO.ClassID
                            && ec.SectionID == argsDTO.SectionID
                            && (ec.Exam.ExamType.ExamTypeID == examType ||
                            ec.Exam.ExamType.ExamTypeID == includeTExamType
                            )
                            select new KeyValueDTO
                            {
                                Key = s.ExamIID.ToString(),
                                Value = s.ExamDescription
                            }).AsNoTracking().ToList();

                return examList;
            }
        }

        public List<KeyValueDTO> GetSubjectsByClassID(MarkEntrySearchArgsDTO argsDTO)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var subjectList = new List<KeyValueDTO>();
                subjectList = (from s in dbContext.ExamSubjectMaps
                               join c in dbContext.ExamClassMaps on s.ExamID equals c.ExamID
                               where s.Exam.AcademicYearID == argsDTO.AcademicYearID
                               && c.ClassID == argsDTO.ClassID && s.ExamID == argsDTO.ExamID
                               && (argsDTO.SectionID == null || c.SectionID == argsDTO.SectionID)
                               select new KeyValueDTO
                               {
                                   Key = s.SubjectID.ToString(),
                                   Value = s.Subject.SubjectName
                               }).AsNoTracking().ToList();

                return subjectList;
            }
        }

        public List<KeyValueDTO> GetSubjectsBySubjectType(MarkEntrySearchArgsDTO argsDTO)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var subjectList = new List<KeyValueDTO>();
                subjectList = (from s in dbContext.ExamSubjectMaps
                               join c in dbContext.ExamClassMaps on s.ExamID equals c.ExamID
                               where s.Exam.AcademicYearID == argsDTO.AcademicYearID
                               && c.ClassID == argsDTO.ClassID && s.ExamID == argsDTO.ExamID
                               && ((argsDTO.LanguageTypeID == null && s.Subject.SubjectTypeID != 2 && s.Subject.SubjectTypeID != 3) || (argsDTO.LanguageTypeID != null && s.Subject.SubjectTypeID == argsDTO.LanguageTypeID))
                               && (argsDTO.SectionID == null || c.SectionID == argsDTO.SectionID)
                               select new KeyValueDTO
                               {
                                   Key = s.SubjectID.ToString(),
                                   Value = s.Subject.SubjectName
                               }).AsNoTracking().ToList();

                return subjectList;
            }
        }

        public ExamSubjectDTO GetSubjectTypeBySubject(int? subjectID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var subject = new ExamSubjectDTO();

                //var subjectDetail = dbContext.Subjects.Where(X => X.SubjectID == subjectID).AsNoTracking().FirstOrDefault();
                //var subjectType = subjectDetail != null || subjectDetail.SubjectTypeID.HasValue ?
                //    dbContext.SubjectTypes.Where(X => X.SubjectTypeID == subjectDetail.SubjectTypeID).AsNoTracking().FirstOrDefault() : null;

                var subjectType = dbContext.SubjectTypes.Where(x => x.Subjects.Any(y => y.SubjectID == subjectID)).AsNoTracking().FirstOrDefault();

                if (subjectType != null)
                {
                    subject.SubjectType = new KeyValueDTO()
                    {
                        Key = subjectType.SubjectTypeID.ToString(),
                        Value = subjectType.TypeName
                    };
                }

                return subject;
            }
        }

        public override long Clone(long screenID, long IID)
        {
            var data = CopyAndInsertRelatedTableDatas(IID);
            return data;
        }

        public long CopyAndInsertRelatedTableDatas(long examID)
        {
            var ExamEntity = new Exam();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                ExamEntity = GetExamData(examID);

                if (ExamEntity != null)
                {

                    dbContext.Exams.Add(ExamEntity);

                    dbContext.Entry(ExamEntity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    dbContext.SaveChanges();
                }
            }

            return ExamEntity != null ? ExamEntity.ExamIID : 0;
        }

        #region Bind tables data
        public Exam GetExamData(long examID)
        {
            var Exam = new Exam();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var examData = dbContext.Exams
                    .Include(i => i.ExamClassMaps)//.ThenInclude(i => i.examStopMaps)
                    .Include(i => i.ExamSkillMaps)
                    .Include(i => i.ExamSubjectMaps)
                    .Include(i => i.ExamGroup)
                    .AsNoTracking()
                    .FirstOrDefault(g => g.ExamIID == examID);

                var currentAcademicYearStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<byte>("CURRENT_ACADEMIC_YEAR_STATUSID");
                var currentAcademicYearID = dbContext.AcademicYears.Where(a => a.SchoolID == examData.SchoolID && a.AcademicYearStatusID == currentAcademicYearStatusID).FirstOrDefault().AcademicYearID;

                var examExists = dbContext.Exams.Where(a => a.SchoolID == examData.SchoolID && a.AcademicYearID == currentAcademicYearID && a.ExamDescription == examData.ExamDescription).FirstOrDefault();

                var examGroup = dbContext.ExamGroups.Where(a => a.AcademicYearID == currentAcademicYearID && a.ExamGroupName == examData.ExamGroup.ExamGroupName && a.IsActive == true).FirstOrDefault();
                var examGroupID = examGroup?.ExamGroupID;

                if (examData != null && examExists == null)
                {
                    var examClassMap = GetExamClassMap(examData);
                    var examSkillMaps = GetExamSkillMapsData(examData);
                    var examSubjectMaps = GetExamSubjectMapsData(examData);

                    Exam = new Exam()
                    {
                        ExamDescription = examData.ExamDescription,
                        SchoolID = examData.SchoolID,
                        AcademicYearID = currentAcademicYearID,
                        ExamGroupID = examGroupID.IsNotNull() ? examGroupID : examData.ExamGroupID,
                        ExamTypeID = examData.ExamTypeID,
                        ProgressCardHeader = examData.ProgressCardHeader,
                        IsActive = true,
                        CreatedBy = Convert.ToInt32(_context.LoginID),
                        CreatedDate = DateTime.Now,
                        ExamClassMaps = examClassMap,
                        ExamSkillMaps = examSkillMaps,
                        ExamSubjectMaps = examSubjectMaps,
                    };
                }
                else
                {
                    Exam = null;
                }
            }

            return Exam;
        }

        public List<ExamClassMap> GetExamClassMap(Exam exam)
        {
            var examClassMaps = new List<ExamClassMap>();

            if (exam.ExamClassMaps != null && exam.ExamClassMaps.Count > 0)
            {
                var filterExamClassMap = exam.ExamClassMaps;

                foreach (var examData in filterExamClassMap)
                {
                    examClassMaps.Add(new ExamClassMap()
                    {
                        //ExamID = examData.ExamID,
                        ExamScheduleID = examData.ExamScheduleID,
                        ClassID = examData.ClassID,
                        SectionID = examData.SectionID,
                        CreatedBy = Convert.ToInt32(_context.LoginID),
                        CreatedDate = DateTime.Now,
                    });
                }
            }

            return examClassMaps;
        }

        public List<ExamSkillMap> GetExamSkillMapsData(Exam exam)
        {
            var examSkillMaps = new List<ExamSkillMap>();

            if (exam.ExamSkillMaps != null && exam.ExamSkillMaps.Count > 0)
            {
                var examSkillDatas = exam.ExamSkillMaps;

                foreach (var skillDatas in examSkillDatas)
                {
                    examSkillMaps.Add(new ExamSkillMap()
                    {
                        SkillGroupMasterID = skillDatas.SkillGroupMasterID,
                        ClassSubjectSkillGroupMapID = skillDatas.ClassSubjectSkillGroupMapID,
                        CreatedBy = Convert.ToInt32(_context.LoginID),
                        CreatedDate = DateTime.Now,
                    });
                }
            }

            return examSkillMaps;
        }

        public List<ExamSubjectMap> GetExamSubjectMapsData(Exam exam)
        {
            var examSubjectMaps = new List<ExamSubjectMap>();

            if (exam.ExamSubjectMaps != null && exam.ExamSubjectMaps.Count > 0)
            {
                var examSubjectMapDatas = exam.ExamSubjectMaps;

                foreach (var mapData in examSubjectMapDatas)
                {
                    examSubjectMaps.Add(new ExamSubjectMap()
                    {
                        SubjectID = mapData.SubjectID,
                        MinimumMarks = mapData.MinimumMarks,
                        MaximumMarks = mapData.MaximumMarks,
                        MarkGradeID = mapData.MarkGradeID,
                        ConversionFactor = mapData.ConversionFactor,
                        CreatedBy = Convert.ToInt32(_context.LoginID),
                        CreatedDate = DateTime.Now,
                    });
                }
            }

            return examSubjectMaps;
        }

        #endregion

    }
}