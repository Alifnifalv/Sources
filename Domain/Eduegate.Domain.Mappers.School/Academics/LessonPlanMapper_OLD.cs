using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Framework;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts.School.Academics;
using Eduegate.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.School.Academics
{
    public class LessonPlanMapper_OLD : DTOEntityDynamicMapper
    {
        public static LessonPlanMapper_OLD Mapper(CallContext context)
        {
            var mapper = new LessonPlanMapper_OLD();
            mapper._context = context;
            return mapper;
        }
        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<LessonPlanDTO_OLD>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.LessonPlans.Where(X => X.LessonPlanIID == IID)
                    .Include(i => i.Subject)
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .Include(i => i.AcademicYear)
                    .Include(i => i.LessonPlanStatus)
                    .AsNoTracking()
                    .FirstOrDefault();

                var lessonPlan = new LessonPlanDTO_OLD()
                {
                    ClassID = entity.ClassID,
                    SubjectID = entity.SubjectID,
                    SectionID = entity.SectionID,
                    LessonPlanIID = entity.LessonPlanIID,
                    LessonPlanStatusID = entity.LessonPlanStatusID,
                    AcademicYearID = entity.AcademicYearID,
                    SchoolID = entity.SchoolID,
                    Class = entity.Class == null ? new KeyValueDTO() : new KeyValueDTO() { Key = entity.ClassID.ToString(), Value = entity.Class.ClassDescription },
                    Section = entity.Section == null ? new KeyValueDTO() : new KeyValueDTO() { Key = entity.SectionID.ToString(), Value = entity.Section.SectionName },
                    Subject = entity.Subject == null ? new KeyValueDTO() : new KeyValueDTO() { Key = entity.Subject.SubjectID.ToString(), Value = entity.Subject.SubjectName },
                    LessonPlanStatus = entity.LessonPlanStatus == null ? new KeyValueDTO() : new KeyValueDTO() { Key = entity.LessonPlanStatusID.ToString(), Value = entity.LessonPlanStatus.StatusName },
                    Date1 = entity.Date1,
                    Date2 = entity.Date2,
                    Date3 = entity.Date3,
                    TotalHours = entity.TotalHours,

                };
                var entityLessonPlanTopics = dbContext.LessonPlanTopicMaps.Where(X => X.LessonPlanID == IID)
                         .AsNoTracking()
                         .ToList();
                lessonPlan.LessonPlanTopicMap = new List<LessonPlanTopicMapDTO>();
                foreach (var lessonPlanMap in entity.LessonPlanTopicMaps)
                {
                    lessonPlan.LessonPlanTopicMap.Add(new LessonPlanTopicMapDTO()
                    {
                        Topic = lessonPlanMap.Topic,
                        LectureCode = lessonPlanMap.LectureCode,
                        LessonPlanID = lessonPlanMap.LessonPlanID,
                        LessonPlanTopicMapsIID = lessonPlanMap.LessonPlanTopicMapIID,
                    });
                }
                return ToDTOString(lessonPlan);
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as LessonPlanDTO_OLD;

            if (toDto.Class == null && toDto.Class.Key == null)
            {
                throw new Exception("Please Select Class!");
            }

            if (toDto.Section == null && toDto.Section.Key == null)
            {
                throw new Exception("Please Select Section!");
            }

            if (toDto.Subject == null && toDto.Subject.Key == null)
            {
                throw new Exception("Please Select Subject!");
            }

            if (toDto.LessonPlanStatus == null && toDto.LessonPlanStatus.Key == null)
            {
                throw new Exception("Please Select Plan Status!");
            }

            if (toDto.LessonPlanTopicMap == null || toDto.LessonPlanTopicMap.Count == 0)
            {
                throw new Exception("Please fill topics details!");
            }

            //convert the dto to entity and pass to the repository.
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = new LessonPlan()
                {
                    LessonPlanIID = toDto.LessonPlanIID,
                    Title = toDto.Title,
                    MonthID = toDto.MonthID,
                    NumberOfActivityCompleted = toDto.NumberOfActivityCompleted,
                    NumberOfClassTests = toDto.NumberOfClassTests,
                    NumberOfPeriods = toDto.NumberOfPeriods,
                    HomeWorks = toDto.HomeWorks,
                    LearningExperiences = toDto.LearningExperiences,
                    Activity = toDto.Activity,
                    SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                    AcademicYearID = toDto.AcademicYearID == null ? (int)_context.AcademicYearID : toDto.AcademicYearID,
                    ClassID = toDto.Class == null ? (int?)null : int.Parse(toDto.Class.Key),
                    SubjectID = toDto.Subject == null ? (int?)null : int.Parse(toDto.Subject.Key),
                    SectionID = toDto.Section == null ? (int?)null : int.Parse(toDto.Section.Key),
                    LessonPlanStatusID = toDto.LessonPlanStatus == null ? (byte?)null : byte.Parse(toDto.LessonPlanStatus.Key),
                    Date1 = toDto.Date1,
                    Date2 = toDto.Date2,
                    Date3 = toDto.Date3,
                    TotalHours = toDto.TotalHours,
                    CreatedBy = toDto.LessonPlanIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                    UpdatedBy = toDto.LessonPlanIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                    CreatedDate = toDto.LessonPlanIID == 0 ? DateTime.Now : dto.CreatedDate,
                    UpdatedDate = toDto.LessonPlanIID > 0 ? DateTime.Now : dto.UpdatedDate,
                    //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
                };

                var IIDs = toDto.LessonPlanTopicMap
                    .Select(a => a.LessonPlanTopicMapsIID).ToList();

                //delete maps
                var entities = dbContext.LessonPlanTopicMaps.Where(x =>
                    x.LessonPlanID == entity.LessonPlanIID &&
                    !IIDs.Contains(x.LessonPlanTopicMapIID)).ToList();

                if (entities.IsNotNull())
                    dbContext.LessonPlanTopicMaps.RemoveRange(entities);

                entity.LessonPlanTopicMaps = new List<LessonPlanTopicMap>();


                foreach (var map in toDto.LessonPlanTopicMap)
                {
                    List<LessonPlanTopicAttachmentMap> topicmapattach = new List<LessonPlanTopicAttachmentMap>();
                    if (map.LessonPlanTopicAttachments != null)
                    {
                        foreach (var tpcmapatt in map.LessonPlanTopicAttachments)
                        {
                            if (!string.IsNullOrEmpty(tpcmapatt.AttachmentName))
                            {
                                topicmapattach.Add(new LessonPlanTopicAttachmentMap()
                                {
                                    LessonPlanTopicAttachmentMapIID = tpcmapatt.LessonPlanTopicAttachmentMapIID,
                                    LessonPlanTopicMapID = tpcmapatt.LessonPlanTopicMapID,
                                    AttachmentReferenceID = tpcmapatt.AttachmentReferenceID,
                                    AttachmentName = tpcmapatt.AttachmentName,
                                    AttachmentDescription = tpcmapatt.AttachmentDescription,
                                    Notes = tpcmapatt.Notes,
                                    CreatedBy = tpcmapatt.LessonPlanTopicAttachmentMapIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                                    UpdatedBy = tpcmapatt.LessonPlanTopicAttachmentMapIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                                    CreatedDate = tpcmapatt.LessonPlanTopicAttachmentMapIID == 0 ? DateTime.Now : dto.CreatedDate,
                                    UpdatedDate = tpcmapatt.LessonPlanTopicAttachmentMapIID > 0 ? DateTime.Now : dto.UpdatedDate,
                                });
                            }
                        }
                    }
                    entity.LessonPlanTaskMaps = new List<LessonPlanTaskMap>();
                    if (map.LessonPlanTopicTask != null)
                    {
                        foreach (var tpctask in map.LessonPlanTopicTask)
                        {
                            List<LessonPlanTaskAttachmentMap> lessonPlanTopicTaskAtach = new List<LessonPlanTaskAttachmentMap>();
                            if (!string.IsNullOrEmpty(tpctask.Task))
                            {
                                foreach (var tpcmapatt in tpctask.LessonPlanTaskAttachment)
                                {
                                    if (!string.IsNullOrEmpty(tpcmapatt.AttachmentName))
                                    {
                                        lessonPlanTopicTaskAtach.Add(new LessonPlanTaskAttachmentMap()
                                        {
                                            LessonPlancTaskAttachmentMapIID = tpcmapatt.LessonPlancTaskAttachmentMapIID,
                                            LessonPlanTaskMapID = tpcmapatt.LessonPlanTaskMapID,
                                            AttachmentReferenceID = tpcmapatt.AttachmentReferenceID,
                                            AttachmentName = tpcmapatt.AttachmentName,
                                            AttachmentDescription = tpcmapatt.AttachmentDescription,
                                            Notes = tpcmapatt.Notes,
                                            CreatedBy = tpcmapatt.LessonPlancTaskAttachmentMapIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                                            UpdatedBy = tpcmapatt.LessonPlancTaskAttachmentMapIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                                            CreatedDate = tpcmapatt.LessonPlancTaskAttachmentMapIID == 0 ? DateTime.Now : dto.CreatedDate,
                                            UpdatedDate = tpcmapatt.LessonPlancTaskAttachmentMapIID > 0 ? DateTime.Now : dto.UpdatedDate,
                                        });
                                    }
                                }
                                entity.LessonPlanTaskMaps.Add(new LessonPlanTaskMap()
                                {
                                    Task = tpctask.Task,
                                    TaskTypeID = tpctask.TaskType == null ? (byte?)null : byte.Parse(tpctask.TaskType.Key),
                                    LessonPlanTaskMapIID = tpctask.LessonPlanTaskMapIID,
                                    LessonPlanID = tpctask.LessonPlanID,
                                    StartDate = tpctask.StartDate,
                                    EndDate = tpctask.EndDate,
                                    LessonPlanTaskAttachmentMaps = lessonPlanTopicTaskAtach,
                                });
                            }
                        }
                    }
                    entity.LessonPlanTopicMaps.Add(new LessonPlanTopicMap()
                    {
                        Topic = map.Topic,
                        LectureCode = map.LectureCode,
                        LessonPlanID = map.LessonPlanID,
                        LessonPlanTopicMapIID = map.LessonPlanTopicMapsIID,
                        CreatedBy = map.LessonPlanTopicMapsIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                        UpdatedBy = map.LessonPlanTopicMapsIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                        CreatedDate = map.LessonPlanTopicMapsIID == 0 ? DateTime.Now : dto.CreatedDate,
                        UpdatedDate = map.LessonPlanTopicMapsIID > 0 ? DateTime.Now : dto.UpdatedDate,
                        //TimeStamps = map.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
                        LessonPlanTopicAttachmentMaps = topicmapattach,
                    });
                }

                entity.LessonPlanTaskMaps = new List<LessonPlanTaskMap>();
                if (entity.LessonPlanTaskMaps != null)
                {
                    foreach (var Taskmap in toDto.LessonPlanTask)
                    {
                        if (!string.IsNullOrEmpty(Taskmap.Task))
                        {
                            List<LessonPlanTaskAttachmentMap> taskmapatt = new List<LessonPlanTaskAttachmentMap>();
                            foreach (var tskmapatt in Taskmap.LessonPlanTaskAttachment)
                            {
                                if (!string.IsNullOrEmpty(tskmapatt.AttachmentName))
                                {
                                    taskmapatt.Add(new LessonPlanTaskAttachmentMap()
                                    {
                                        LessonPlancTaskAttachmentMapIID = tskmapatt.LessonPlancTaskAttachmentMapIID,
                                        LessonPlanTaskMapID = tskmapatt.LessonPlanTaskMapID,
                                        AttachmentReferenceID = tskmapatt.AttachmentReferenceID,
                                        AttachmentName = tskmapatt.AttachmentName,
                                        AttachmentDescription = tskmapatt.AttachmentDescription,
                                        Notes = tskmapatt.Notes,
                                        CreatedBy = tskmapatt.LessonPlancTaskAttachmentMapIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                                        UpdatedBy = tskmapatt.LessonPlancTaskAttachmentMapIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                                        CreatedDate = tskmapatt.LessonPlancTaskAttachmentMapIID == 0 ? DateTime.Now : dto.CreatedDate,
                                        UpdatedDate = tskmapatt.LessonPlancTaskAttachmentMapIID > 0 ? DateTime.Now : dto.UpdatedDate,
                                    });
                                }
                            }
                            entity.LessonPlanTaskMaps.Add(new LessonPlanTaskMap()
                            {
                                Task = Taskmap.Task,
                                TaskTypeID = Taskmap.TaskType == null ? (byte?)null : byte.Parse(Taskmap.TaskType.Key),
                                LessonPlanTaskMapIID = Taskmap.LessonPlanTaskMapIID,
                                LessonPlanID = Taskmap.LessonPlanID,
                                StartDate = Taskmap.StartDate,
                                EndDate = Taskmap.EndDate,
                                LessonPlanTaskAttachmentMaps = taskmapatt,
                            });
                        }
                    }
                }

                if (entity.LessonPlanIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    foreach (var map in entity.LessonPlanTopicMaps)
                    {

                        if (map.LessonPlanTopicMapIID != 0)
                        {
                            dbContext.Entry(map).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                        else
                        {
                            dbContext.Entry(map).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                    }
                    foreach (var Taskmap in entity.LessonPlanTaskMaps)
                    {

                        if (Taskmap.LessonPlanTaskMapIID != 0)
                        {
                            dbContext.Entry(Taskmap).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                        else
                        {
                            dbContext.Entry(Taskmap).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                    }


                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();

                var lessonPlanWorkFlowID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("LESSON_PLAN_WORKFLOW_ID", 1, null);
                if (lessonPlanWorkFlowID == null)
                    throw new Exception("Please set 'LESSON_PLAN_WORKFLOW_ID' in settings");
                Workflows.Helpers.WorkflowGeneratorHelper.GenerateWorkflows(long.Parse(lessonPlanWorkFlowID), entity.LessonPlanIID);

                return GetEntity(entity.LessonPlanIID);
            }
        }
    }
}