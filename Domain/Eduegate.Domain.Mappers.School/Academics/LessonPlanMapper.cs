using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Mappers.Notification.Notifications;
using Eduegate.Domain.Mappers.Notification.Notifications.Helpers;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Extensions;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.Lms;
using Eduegate.Services.Contracts.School.Academics;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Eduegate.Domain.Mappers.School.Academics
{
    public class LessonPlanMapper : DTOEntityDynamicMapper
    {
        public static LessonPlanMapper Mapper(CallContext context)
        {
            var mapper = new LessonPlanMapper();
            mapper._context = context;
            return mapper;
        }
        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<LessonPlanDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private LessonPlanDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.LessonPlans.Where(x => x.LessonPlanIID == IID)
                    .Include(i => i.Class)
                   
                    .Include(i => i.Section)
                    .Include(i => i.Subject)
                    .Include(i => i.Unit)
                    .Include(i => i.LessonPlanStatus)
                    .Include(i => i.LessonPlanAttachmentMaps)
                    //.Include(i => i.LessonPlanLearningOutcomeMaps).ThenInclude(i => i.LessonLearningOutcome)
                    //.Include(i => i.LessonPlanLearningObjectiveMaps).ThenInclude(i => i.LessonLearningObjective)
                    .Include(i => i.LessonPlanLearningOutcomeMaps)
                    .Include(i => i.LessonPlanLearningObjectiveMaps)
                    .Include(i => i.LessonPlanClassSectionMaps).ThenInclude(i => i.Class)
                    .Include(i => i.LessonPlanClassSectionMaps).ThenInclude(i => i.Section)
                    .Include(i => i.LessonPlanTopicMaps)
                    .ThenInclude(tp => tp.LessonPlanTopicAttachmentMaps)
                    .Include(i => i.LessonPlanTopicMaps)
                    .ThenInclude(tp => tp.LessonPlanTaskMaps)
                    .Include(i => i.LessonPlanTaskMaps)
                    .ThenInclude(tsk => tsk.LessonPlanTaskAttachmentMaps)
                    .Include(i => i.LessonPlanTaskMaps)
                     .ThenInclude(i => i.TaskType)
                    .AsNoTracking()
                    .FirstOrDefault();

                return ToDTO(entity);
            }
        }

        private LessonPlanDTO ToDTO(LessonPlan entity)
        {
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");
            var lessonPlanDTO = new LessonPlanDTO()
            {
                LessonPlanIID = entity.LessonPlanIID,
                Title = entity.Title,
                ClassID = entity.ClassID,
                SectionID = entity.SectionID,
                SubjectID = entity.SubjectID,
                LessonPlanStatusID = entity.LessonPlanStatusID,
                AcademicYearID = entity.AcademicYearID,
                SchoolID = entity.SchoolID,
                Date1 = entity.Date1,
                Date2 = entity.Date2,
                StartDate = entity.Date1.HasValue ? entity.Date1.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                EndDate = entity.Date2.HasValue ? entity.Date2.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                MonthID = entity.MonthID,
                IsSendPushNotification = false,
                Subject = entity.SubjectID.HasValue ? new KeyValueDTO() { Key = entity.Subject?.SubjectID.ToString(), Value = entity.Subject?.SubjectName } : new KeyValueDTO(),
                LessonPlanStatus = entity.LessonPlanStatusID.HasValue ? new KeyValueDTO() { Key = entity.LessonPlanStatusID.ToString(), Value = entity.LessonPlanStatus?.StatusName } : new KeyValueDTO(),
                Content = entity.Content,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                IsSyllabusCompleted = entity.IsSyllabusCompleted == true ? true : false,
                ActionPlan = entity.ActionPlan,
                TotalHours = entity.TotalHours,
                NumberOfPeriods = entity.NumberOfPeriods,
                NumberOfClassTests = entity.NumberOfClassTests,
                LearningExperiences = entity.LearningExperiences,
                HomeWorks = entity.HomeWorks,
                CrossDisciplinaryConnection = entity.CrossDisciplinaryConnection,
                AllignmentToVisionAndMission = entity.AllignmentToVisionAndMission,
                SEN = entity.SEN,
                HighAchievers = entity.HighAchievers,
                StudentsWhoNeedImprovement = entity.StudentsWhoNeedImprovement,
                PostLessonEvaluation = entity.PostLessonEvaluation,
                Activity = entity.Activity,
                UnitID = entity.UnitID,
                SubjectUnit = entity.UnitID.HasValue ? new KeyValueDTO() { Key = entity.Unit.UnitIID.ToString(), Value = entity.Unit?.UnitTitle } : new KeyValueDTO(),
            };
            GetExpectedLearningOutComeString(entity, lessonPlanDTO);

            lessonPlanDTO.LessonPlanAttachmentMap = new List<LessonPlanAttachmentMapDTO>();
            if (entity.LessonPlanAttachmentMaps.Count > 0)
            {
                foreach (var attachment in entity.LessonPlanAttachmentMaps)
                {
                    if (attachment.AttachmentReferenceID.HasValue || !string.IsNullOrEmpty(attachment.AttachmentName))
                    {
                        lessonPlanDTO.LessonPlanAttachmentMap.Add(new LessonPlanAttachmentMapDTO()
                        {
                            LessonPlanAttachmentMapIID = attachment.LessonPlanAttachmentMapIID,
                            LessonPlanID = attachment.LessonPlanID.HasValue ? attachment.LessonPlanID : null,
                            AttachmentReferenceID = attachment.AttachmentReferenceID,
                            AttachmentName = attachment.AttachmentName,
                            AttachmentDescription = attachment.AttachmentDescription,
                            Notes = attachment.Notes,
                            CreatedBy = attachment.CreatedBy,
                            CreatedDate = attachment.CreatedDate,
                        });
                    }
                }
            }

            lessonPlanDTO.LessonPlanClassSectionMaps = new List<LessonPlanClassSectionMapDTO>();
            foreach (var map in entity.LessonPlanClassSectionMaps)
            {
                if (!lessonPlanDTO.LessonPlanClassSectionMaps.Any(x => x.ClassID == map.ClassID && x.SectionID == map.SectionID))
                {
                    lessonPlanDTO.LessonPlanClassSectionMaps.Add(new LessonPlanClassSectionMapDTO()
                    {
                        LessonPlanClassSectionMapIID = map.LessonPlanClassSectionMapIID,
                        LessonPlanID = map.LessonPlanID,
                        ClassID = map.ClassID,
                        SectionID = map.SectionID,
                        Class = map.ClassID.HasValue ? new KeyValueDTO() { Key = map.ClassID.Value.ToString(), Value = map.Class?.ClassDescription } : new KeyValueDTO(),
                        Section = map.SectionID.HasValue ? new KeyValueDTO() { Key = map.SectionID.Value.ToString(), Value = map.Section?.SectionName } : new KeyValueDTO(),
                        CreatedBy = map.CreatedBy,
                        CreatedDate = map.CreatedDate,
                        UpdatedBy = map.UpdatedBy,
                        UpdatedDate = map.UpdatedDate,
                    });
                }
            }

            lessonPlanDTO.LessonPlanTopicMap = new List<LessonPlanTopicMapDTO>();
            foreach (var lessonPlanMap in entity.LessonPlanTopicMaps)
            {
                lessonPlanDTO.LessonPlanTopicMap.Add(new LessonPlanTopicMapDTO()
                {
                    LessonPlanID = lessonPlanMap.LessonPlanID,
                    LessonPlanTopicMapsIID = lessonPlanMap.LessonPlanTopicMapIID,
                    Topic = lessonPlanMap.Topic,
                    LectureCode = lessonPlanMap.LectureCode,
                    Period = lessonPlanMap.Period,

                    // Map the LessonPlanTaskMaps to DTO
                    LessonPlanTopicTask = lessonPlanMap.LessonPlanTaskMaps?.Select(task => new LessonPlanTaskMapDTO()
                    {
                        LessonPlanTaskMapIID = task.LessonPlanTaskMapIID,
                        LessonPlanID = task.LessonPlanID,
                        TimeDuration = task.TimeDuration,
                        Task = task.Task,
                        StartDate = task.StartDate,
                        EndDate = task.EndDate,
                        TaskType = task.TaskType != null
                        ? new KeyValueDTO { Key = task.TaskTypeID.ToString(), Value = task.TaskType.TaskType1 }
                        : new KeyValueDTO(),

                        // Map the attachments if available
                        LessonPlanTaskAttachment = task.LessonPlanTaskAttachmentMaps?.Select(att => new LessonPlanTaskAttachmentMapDTO()
                        {
                            LessonPlancTaskAttachmentMapIID = att.LessonPlancTaskAttachmentMapIID,
                            LessonPlanTaskMapID = att.LessonPlanTaskMapID,
                            AttachmentReferenceID = att.AttachmentReferenceID,
                            AttachmentName = att.AttachmentName,
                            AttachmentDescription = att.AttachmentDescription,
                            Notes = att.Notes
                        }).ToList()

                    }).ToList()
                });
            }

            lessonPlanDTO.LessonPlanTask = new List<LessonPlanTaskMapDTO>();
            foreach (var taskMap in entity.LessonPlanTaskMaps)
            {
                lessonPlanDTO.LessonPlanTask.Add(new LessonPlanTaskMapDTO()
                {
                    Task = taskMap.Task,
                    LessonPlanID = taskMap.LessonPlanID,
                    TaskType = taskMap.TaskType != null
                        ? new KeyValueDTO { Key = taskMap.TaskTypeID.ToString(), Value = taskMap.TaskType.TaskType1 }
                        : new KeyValueDTO(),
                    TimeDuration = taskMap.TimeDuration,
                    LessonPlanTopicMapID = taskMap.LessonPlanTopicMapID,
                    LessonPlanTaskMapIID = taskMap.LessonPlanTaskMapIID,
                });
            }

            FillLessonPlanLearningOutcomeMap(lessonPlanDTO, entity);
            FillLessonPlanLearningObjectiveMap(lessonPlanDTO, entity);

            #region unwanted fields hide

            //TotalHours = entity.TotalHours,
            //Resourses = entity.Resourses,
            //NumberOfPeriods = entity.NumberOfPeriods,
            //NumberOfClassTests = entity.NumberOfClassTests,
            //NumberOfActivityCompleted = entity.NumberOfActivityCompleted,
            //LearningExperiences = entity.LearningExperiences,
            //HomeWorks = entity.HomeWorks,
            //Activity = entity.Activity,
            //Description = entity.Description,
            //LessonPlanCode = entity.LessonPlanCode,
            //Date3 = entity.Date3,
            //SkillFocused = entity.SkillFocused,
            //AllignmentToVisionAndMission = entity.AllignmentToVisionAndMission,
            //Connectivity = entity.Connectivity,
            //CrossDisciplinaryConnection = entity.CrossDisciplinaryConnection,
            //Introduction = entity.Introduction,
            //TeachingMethodology = entity.TeachingMethodology,
            //Closure = entity.Closure,
            //SEN = entity.SEN,
            //HighAchievers = entity.HighAchievers,
            //StudentsWhoNeedImprovement = entity.StudentsWhoNeedImprovement,
            //PostLessonEvaluation = entity.PostLessonEvaluation,
            //Reflections = entity.Reflections,
            //ExpectedLearningOutcomeID = entity.ExpectedLearningOutcomeID,
            //TeachingAidID = entity.TeachingAidID,

            //Class = entity.ClassID.HasValue ? new KeyValueDTO() { Key = entity.ClassID.ToString(), Value = entity.Class.ClassDescription } : new KeyValueDTO(),
            //Section = entity.SectionID.HasValue ? new KeyValueDTO() { Key = entity.SectionID.ToString(), Value = entity.Section.SectionName } : new KeyValueDTO(),

            #endregion

            return lessonPlanDTO;
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as LessonPlanDTO;
            using (var dbContext = new dbEduegateSchoolContext())
            {
                if (toDto.SubjectID == null)
                {
                    throw new Exception("Please Select Subject!");
                }

                if (toDto.LessonPlanStatusID == null)
                {
                    throw new Exception("Please Select Plan Status!");
                }

                int? approvedStsID = new Domain.Setting.SettingBL(null).GetSettingValue<int?>("Lesson_Approved_StatusID", 5);

                //edit disable when status is approved and Syllabus is Completed
                if (toDto.LessonPlanStatusID == approvedStsID && toDto.IsSyllabusCompleted == true)
                {
                    throw new Exception("The lesson plan has been already approved, could not edit !!!");
                }

                var entity = new LessonPlan()
                {
                    LessonPlanIID = toDto.LessonPlanIID,
                    Title = toDto.Title == null ? null : toDto.Title,
                    MonthID = toDto.MonthID.HasValue ? toDto.MonthID : null,
                    SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                    AcademicYearID = toDto.AcademicYearID.HasValue ? toDto.AcademicYearID : null,
                    ClassID = toDto.ClassID,
                    SubjectID = toDto.SubjectID,
                    SectionID = toDto.SectionID,
                    LessonPlanStatusID = toDto.LessonPlanStatusID,
                    Date1 = toDto.Date1.HasValue ? toDto.Date1 : (DateTime?)null,
                    Date2 = toDto.Date2.HasValue ? toDto.Date2 : (DateTime?)null,
                    Content = toDto.Content,
                    IsSyllabusCompleted = toDto.IsSyllabusCompleted == true ? true : false,
                    ActionPlan = toDto.ActionPlan,
                    TotalHours = toDto.TotalHours,
                    NumberOfPeriods = toDto.NumberOfPeriods,
                    NumberOfClassTests = toDto.NumberOfClassTests,
                    LearningExperiences = toDto.LearningExperiences,
                    HomeWorks = toDto.HomeWorks,
                    CrossDisciplinaryConnection = toDto.CrossDisciplinaryConnection,
                    AllignmentToVisionAndMission = toDto.AllignmentToVisionAndMission,
                    SEN = toDto.SEN,
                    HighAchievers = toDto.HighAchievers,
                    StudentsWhoNeedImprovement = toDto.StudentsWhoNeedImprovement,
                    PostLessonEvaluation = toDto.PostLessonEvaluation,
                    Activity = toDto.Activity,
                    UnitID = toDto.UnitID,
                    CreatedBy = toDto.LessonPlanIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                    UpdatedBy = toDto.LessonPlanIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                    CreatedDate = toDto.LessonPlanIID == 0 ? DateTime.Now : dto.CreatedDate,
                    UpdatedDate = toDto.LessonPlanIID > 0 ? DateTime.Now : dto.UpdatedDate,
                };

                var IIDs = toDto.LessonPlanAttachmentMap
                    .Select(a => a.LessonPlanAttachmentMapIID).ToList();

                //delete maps
                var entities = dbContext.LessonPlanAttachmentMaps.Where(x =>
                    x.LessonPlanID == entity.LessonPlanIID &&
                    !IIDs.Contains(x.LessonPlanAttachmentMapIID)).AsNoTracking().ToList();

                if (entities.IsNotNull())
                    dbContext.LessonPlanAttachmentMaps.RemoveRange(entities);

                entity.LessonPlanAttachmentMaps = new List<LessonPlanAttachmentMap>();
                if (toDto.LessonPlanAttachmentMap.Count > 0)
                {
                    foreach (var attach in toDto.LessonPlanAttachmentMap)
                    {
                        if (attach.AttachmentReferenceID.HasValue || !string.IsNullOrEmpty(attach.AttachmentName))
                        {
                            entity.LessonPlanAttachmentMaps.Add(new LessonPlanAttachmentMap()
                            {
                                LessonPlanAttachmentMapIID = attach.LessonPlanAttachmentMapIID,
                                LessonPlanID = attach.LessonPlanID.HasValue ? attach.LessonPlanID : null,
                                AttachmentReferenceID = attach.AttachmentReferenceID,
                                AttachmentName = attach.AttachmentName,
                                AttachmentDescription = attach.AttachmentDescription,
                                Notes = attach.Notes,
                                CreatedBy = attach.LessonPlanAttachmentMapIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                                UpdatedBy = attach.LessonPlanAttachmentMapIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                                CreatedDate = attach.LessonPlanAttachmentMapIID == 0 ? DateTime.Now : dto.CreatedDate,
                                UpdatedDate = attach.LessonPlanAttachmentMapIID > 0 ? DateTime.Now : dto.UpdatedDate,
                            });
                        }
                    }
                }

                entity.LessonPlanClassSectionMaps = new List<LessonPlanClassSectionMap>();

                if (toDto.LessonPlanClassSectionMaps.Count > 0)
                {
                    foreach (var data in toDto.LessonPlanClassSectionMaps)
                    {
                        entity.LessonPlanClassSectionMaps.Add(new LessonPlanClassSectionMap()
                        {
                            ClassID = data.ClassID,
                            SectionID = data.SectionID,
                            LessonPlanID = toDto.LessonPlanIID,
                        });
                    }
                }

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

                    List<LessonPlanTaskMap> taskList = new List<LessonPlanTaskMap>(); // ✅ Initialize task list here
                    if (map.LessonPlanTopicTask != null)
                    {
                        foreach (var tpctask in map.LessonPlanTopicTask)
                        {
                            List<LessonPlanTaskAttachmentMap> lessonPlanTopicTaskAtach = new List<LessonPlanTaskAttachmentMap>();

                            if (!string.IsNullOrEmpty(tpctask.Task))
                            {
                                if (tpctask.LessonPlanTaskAttachment != null) // ✅ Check for null before looping
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
                                }

                                // ✅ Use taskList.Add(...) instead of entity.LessonPlanTaskMaps.Add(...)
                                taskList.Add(new LessonPlanTaskMap()
                                {
                                    Task = tpctask.Task,
                                    TaskTypeID = tpctask.TaskTypeID,
                                    LessonPlanTaskMapIID = tpctask.LessonPlanTaskMapIID,
                                    LessonPlanID = tpctask.LessonPlanID,
                                    TimeDuration = tpctask.TimeDuration,
                                    StartDate = tpctask.StartDate,
                                    EndDate = tpctask.EndDate,
                                    LessonPlanTaskAttachmentMaps = lessonPlanTopicTaskAtach,
                                });
                            }
                        }
                    }

                    // ✅ Assign taskList to LessonPlanTopicMap.LessonPlanTaskMaps
                    entity.LessonPlanTopicMaps.Add(new LessonPlanTopicMap()
                    {
                        Topic = map.Topic,
                        LectureCode = map.LectureCode,
                        Period = map.Period,
                        LessonPlanID = map.LessonPlanID,
                        LessonPlanTopicMapIID = map.LessonPlanTopicMapsIID,
                        CreatedBy = map.LessonPlanTopicMapsIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                        UpdatedBy = map.LessonPlanTopicMapsIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                        CreatedDate = map.LessonPlanTopicMapsIID == 0 ? DateTime.Now : dto.CreatedDate,
                        UpdatedDate = map.LessonPlanTopicMapsIID > 0 ? DateTime.Now : dto.UpdatedDate,
                        LessonPlanTaskMaps = taskList, // ✅ Assign taskList here
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
                            List<LessonPlanTaskAttachmentMap> taskMapAttach = new List<LessonPlanTaskAttachmentMap>();
                            foreach (var tskmapatt in Taskmap.LessonPlanTaskAttachment)
                            {
                                if (!string.IsNullOrEmpty(tskmapatt.AttachmentName))
                                {
                                    taskMapAttach.Add(new LessonPlanTaskAttachmentMap()
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
                                TaskTypeID = Taskmap.TaskTypeID,
                                LessonPlanTaskMapIID = Taskmap.LessonPlanTaskMapIID,
                                LessonPlanID = Taskmap.LessonPlanID,
                                TimeDuration = Taskmap.TimeDuration,
                                LessonPlanTaskAttachmentMaps = taskMapAttach,
                            });
                        }
                    }
                }
                entity.LessonPlanLearningOutcomeMaps = new List<LessonPlanLearningOutcomeMap>();
                if (entity.LessonPlanLearningOutcomeMaps != null)
                {
                    foreach (var LearningOutcome in toDto.LessonPlanLearningOutcomeMap)
                    {
                        entity.LessonPlanLearningOutcomeMaps.Add(new LessonPlanLearningOutcomeMap()
                        {
                            LessonLearningOutcomeID = LearningOutcome.LessonLearningOutcomeID,
                            LessonPlanID = LearningOutcome.LessonPlanID
                        });
                    }
                }

                entity.LessonPlanLearningObjectiveMaps = new List<LessonPlanLearningObjectiveMap>();
                if (entity.LessonPlanLearningObjectiveMaps != null)
                {
                    foreach (var LearningObjective in toDto.LessonPlanLearningObjectiveMap)
                    {
                        entity.LessonPlanLearningObjectiveMaps.Add(new LessonPlanLearningObjectiveMap()
                        {
                            LessonLearningObjectiveID = LearningObjective.LessonLearningObjectiveID,
                            LessonPlanID = LearningObjective.LessonPlanID
                        });
                    }
                }

                dbContext.LessonPlans.Add(entity);

                if (entity.LessonPlanIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    if (entity.LessonPlanAttachmentMaps.Count > 0)
                    {
                        foreach (var atch in entity.LessonPlanAttachmentMaps)
                        {
                            if (atch.LessonPlanAttachmentMapIID == 0)
                            {
                                dbContext.Entry(atch).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            }
                            else
                            {
                                dbContext.Entry(atch).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }
                        }
                    }

                    if (entity.LessonPlanClassSectionMaps.Count > 0)
                    {
                        using (var dbContext1 = new dbEduegateSchoolContext())
                        {
                            //Remove old Data and Add new Ones
                            var getFullLIst = dbContext1.LessonPlanClassSectionMaps.Where(y => y.LessonPlanID == entity.LessonPlanIID).AsNoTracking().ToList();
                            if (getFullLIst != null && getFullLIst.Count > 0)
                            {
                                dbContext1.LessonPlanClassSectionMaps.RemoveRange(getFullLIst);
                            }
                            dbContext1.SaveChanges();
                        }

                        foreach (var clsSecMap in entity.LessonPlanClassSectionMaps)
                        {
                            if (clsSecMap.LessonPlanClassSectionMapIID == 0)
                            {
                                dbContext.Entry(clsSecMap).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            }
                            else
                            {
                                dbContext.Entry(clsSecMap).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            }
                        }
                    }

                    foreach (var map in entity.LessonPlanTopicMaps)
                    {
                        if (map.LessonPlanTopicMapIID != 0)
                        {
                            dbContext.Entry(map).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                        else
                        {
                            // Ensure tasks are correctly saved under the topic map
                            foreach (var taskMap in map.LessonPlanTaskMaps)
                            {
                                if (taskMap.LessonPlanTaskMapIID != 0)
                                {
                                    dbContext.Entry(taskMap).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                }
                                else
                                {
                                    dbContext.Entry(taskMap).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                }
                            }
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

                    if (entity.LessonPlanLearningOutcomeMaps.Count > 0)
                    {
                        using (var dbContext1 = new dbEduegateSchoolContext())
                        {
                            //Remove old Data and Add new Ones
                            var getFullLIst = dbContext1.LessonPlanLearningOutcomeMaps.Where(y => y.LessonPlanID == entity.LessonPlanIID).AsNoTracking().ToList();
                            if (getFullLIst != null && getFullLIst.Count > 0)
                            {
                                dbContext1.LessonPlanLearningOutcomeMaps.RemoveRange(getFullLIst);
                            }
                            dbContext1.SaveChanges();
                        }

                        foreach (var Taskmap in entity.LessonPlanLearningOutcomeMaps)
                        {

                            if (Taskmap.LessonPlanLearningOutcomeMapIID != 0)
                            {
                                dbContext.Entry(Taskmap).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }
                            else
                            {
                                dbContext.Entry(Taskmap).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            }
                        }
                    }

                    if (entity.LessonPlanLearningObjectiveMaps.Count > 0)
                    {
                        using (var dbContext1 = new dbEduegateSchoolContext())
                        {
                            //Remove old Data and Add new Ones
                            var getFullLIst = dbContext1.LessonPlanLearningObjectiveMaps.Where(y => y.LessonPlanID == entity.LessonPlanIID).AsNoTracking().ToList();
                            if (getFullLIst != null && getFullLIst.Count > 0)
                            {
                                dbContext1.LessonPlanLearningObjectiveMaps.RemoveRange(getFullLIst);
                            }
                            dbContext1.SaveChanges();
                        }

                        foreach (var Taskmap in entity.LessonPlanLearningObjectiveMaps)
                        {

                            if (Taskmap.LessonPlanLearningObjectiveMapIID != 0)
                            {
                                dbContext.Entry(Taskmap).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }
                            else
                            {
                                dbContext.Entry(Taskmap).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            }
                        }
                    }

                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();

                long? lessonPlanWorkFlowID = null;
                int? lessonPlanWorkFlowEntity = new Domain.Setting.SettingBL(null).GetSettingValue<int?>("LessonPlanWorkFlowEntity", 1, 1);
                if (lessonPlanWorkFlowEntity != 0)
                {
                    var lessonPlanWorkFlow = (from sub in dbContext.ClassSubjectWorkflowEntityMaps
                                            join wentity in dbContext.ClassSubjectMaps on sub.ClassSubjectMapID equals wentity.ClassSubjectMapIID
                                            where sub.SubjectID == entity.SubjectID && sub.WorkflowEntityID == lessonPlanWorkFlowEntity && wentity.ClassID == entity.ClassID
                                            && wentity.AcademicYearID == entity.AcademicYearID
                                            select sub).AsNoTracking().FirstOrDefault();

                    lessonPlanWorkFlowID = lessonPlanWorkFlow?.workflowID;
                }
                if (lessonPlanWorkFlowID == null)
                {
                    lessonPlanWorkFlowID = new Domain.Setting.SettingBL(null).GetSettingValue<long?>("LESSON_PLAN_WORKFLOW_ID", 1, null);
                    if (lessonPlanWorkFlowID == null)
                        throw new Exception("Please set 'LESSON_PLAN_WORKFLOW_ID' in settings");
                }
                Workflows.Helpers.WorkflowGeneratorHelper.GenerateWorkflows(lessonPlanWorkFlowID.Value, entity.LessonPlanIID);

                if (toDto.IsSendPushNotification == true)
                {
                    SendAndSavePushNotification(entity, toDto);
                }

                return ToDTOString(ToDTO(entity.LessonPlanIID));
            }
        }

        private void FillLessonPlanLearningOutcomeMap(LessonPlanDTO lessonPlanDTO, LessonPlan entity)
        {            
            lessonPlanDTO.LessonPlanLearningOutcomeMap = new List<LessonPlanOutcomeMapDTO>();
            foreach (var learningOutcomeMap in entity.LessonPlanLearningOutcomeMaps)
            {
                if (learningOutcomeMap.LessonLearningOutcomeID.HasValue)
                {
                    using (var dbContext = new dbEduegateSchoolContext())
                    {
                        var learningOutcome = dbContext.LessonLearningOutcomes.Where(x => x.LessonLearningOutcomeID == learningOutcomeMap.LessonLearningOutcomeID).FirstOrDefault();

                        lessonPlanDTO.LessonPlanLearningOutcomeMap.Add(new LessonPlanOutcomeMapDTO()
                        {
                            LessonPlanID = learningOutcomeMap.LessonPlanID,
                            LessonLearningOutcomeID = learningOutcomeMap.LessonLearningOutcomeID,
                            //LessonLearningOutcomeName = learningOutcomeMap.LessonLearningOutcome.LessonLearningOutcomeName,
                            LessonLearningOutcomeName = learningOutcome?.LessonLearningOutcomeName,
                        });
                    }
                }
            }
        }

        private void FillLessonPlanLearningObjectiveMap(LessonPlanDTO lessonPlanDTO, LessonPlan entity)
        {
            lessonPlanDTO.LessonPlanLearningObjectiveMap = new List<LessonPlanObjectiveMapDTO>();
            foreach (var learningObjectiveMap in entity.LessonPlanLearningObjectiveMaps)
            {
                if (learningObjectiveMap.LessonLearningObjectiveID.HasValue)
                {
                    using (var dbContext = new dbEduegateSchoolContext())
                    {
                        var learningOutcome = dbContext.LessonLearningObjectives.Where(x => x.LessonLearningObjectiveID == learningObjectiveMap.LessonLearningObjectiveID).FirstOrDefault();

                        lessonPlanDTO.LessonPlanLearningObjectiveMap.Add(new LessonPlanObjectiveMapDTO()
                        {
                            LessonPlanID = learningObjectiveMap.LessonPlanID,
                            LessonLearningObjectiveID = learningObjectiveMap.LessonLearningObjectiveID,
                            //LessonLearningObjectiveName = learningObjectiveMap.LessonLearningObjective.LessonLearningObjectiveName,
                            LessonLearningObjectiveName = learningOutcome?.LessonLearningObjectiveName,
                        });
                    }
                }
            }
        }

        private void SendAndSavePushNotification(LessonPlan entity, LessonPlanDTO toDto)
        {
            if (entity.LessonPlanStatusID.HasValue)
            {
                if (entity.LessonPlanStatusID == 2)
                {
                    var loginIDs = GetParentLoginIDs(entity, toDto);

                    var message = "Lesson plan has been uploaded.";
                    var title = "New Lesson Plan";
                    var settings = NotificationSetting.GetParentAppSettings();

                    foreach (var login in loginIDs)
                    {
                        long toLoginID = Convert.ToInt32(login);
                        long fromLoginID = _context?.LoginID != null ? (long)_context.LoginID : 2;

                        PushNotificationMapper.SendAndSavePushNotification(toLoginID, fromLoginID, message, title, settings);
                    }
                }
            }
        }

        private List<long?> GetParentLoginIDs(LessonPlan entity, LessonPlanDTO toDto)
        {
            List<long?> _sRetData = new List<long?>();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

                List<int> _sLstClassIDs = dbContext.Classes.AsNoTracking().Select(x => x.ClassID).ToList<int>();
                List<int> _sLstSectionIDs = dbContext.Sections.AsNoTracking().Select(x => x.SectionID).ToList<int>();

                if (toDto.LessonPlanClassSectionMaps.Any(w => w.ClassID > 0))
                {
                    _sLstClassIDs.RemoveAll(w => !toDto.LessonPlanClassSectionMaps.Any(x => x.ClassID == w));
                }
                if (toDto.LessonPlanClassSectionMaps.Any(w => w.SectionID > 0))
                {
                    _sLstSectionIDs.RemoveAll(w => !toDto.LessonPlanClassSectionMaps.Any(x => x.SectionID == w));
                }

                var dParents = (from stud in dbContext.Students
                                join prnt in dbContext.Parents on stud.ParentID equals prnt.ParentIID
                                join ayr in dbContext.AcademicYears on stud.SchoolAcademicyearID equals ayr.AcademicYearID
                                where stud.SchoolID == entity.SchoolID && ayr.AcademicYearStatusID != 3
                                && _sLstClassIDs.Any(x => x == stud.ClassID)
                                && _sLstSectionIDs.Any(x => x == stud.SectionID)
                                && (stud.IsActive ?? false == true) && prnt.LoginID.HasValue
                                select prnt.LoginID).Distinct();
                if (dParents.Any())
                    _sRetData.AddRange(dParents);
            }
            return _sRetData;
        }

        public int GetMyLessonPlanCount(long employeeID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var currentAcademicYearStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<int?>("CURRENT_ACADEMIC_YEAR_STATUSID", 2);

                var lessonPlans = dbContext.LessonPlans
                    .Include(i => i.LessonPlanClassSectionMaps)
                    .Include(i => i.AcademicYear)
                    .Include(i => i.Class).ThenInclude(i => i.ClassTeacherMaps)
                    .OrderByDescending(d => d.Date1)
                    .Where(x => x.Class.ClassTeacherMaps.Any(b => b.TeacherID == employeeID && b.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID && x.SubjectID == b.SubjectID
                    && x.LessonPlanClassSectionMaps.Any(c => c.ClassID == b.ClassID && c.SectionID == b.SectionID))).AsNoTracking().ToList();

                var lessonPlanCount = lessonPlans != null ? lessonPlans.Count() : 0;

                return lessonPlanCount;
            }
        }

        public List<LessonPlanDTO> GetMyLessonPlans(long employeeID)
        {
            var dtos = new List<LessonPlanDTO>();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var currentAcademicYearStatusID = new Domain.Setting.SettingBL(null)
                    .GetSettingValue<int?>("CURRENT_ACADEMIC_YEAR_STATUSID", 2);

                if (currentAcademicYearStatusID == null)
                    throw new Exception("CURRENT_ACADEMIC_YEAR_STATUSID is null");

                var classSectionMap = dbContext.ClassTeacherMaps
                    .Where(b => b.TeacherID == employeeID && b.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID).Distinct().ToList();

                var classSectionLessonPlans = dbContext.LessonPlanClassSectionMaps
                    .Where(a => classSectionMap.Select(a => a.ClassID).Contains(a.ClassID) && classSectionMap.Select(a => a.SectionID).Contains(a.SectionID))
                    .ToList();

                var lessonPlans = dbContext.LessonPlans.Where(a => classSectionLessonPlans.Select(a => a.LessonPlanID ?? 0).Contains(a.LessonPlanIID) && a.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID && classSectionMap.Select(a => a.SubjectID).Contains(a.SubjectID)) 
                                        .Include(i => i.Class)
                                        .Include(i => i.Section)
                                        .Include(i => i.Subject)
                                        .Include(i => i.Unit)
                                        .Include(i => i.LessonPlanStatus)
                                        .Include(i => i.LessonPlanAttachmentMaps)
                                        .Include(i => i.LessonPlanTaskMaps)
                                        .Include(i => i.LessonPlanTopicMaps)
                                        .Include(i => i.LessonPlanClassSectionMaps).ThenInclude(i => i.Class)
                                        .Include(i => i.LessonPlanClassSectionMaps).ThenInclude(i => i.Section)
                    .ToList();


                // Map the filtered results to DTOs
                foreach (var lessonPlan in lessonPlans)
                {
                    dtos.Add(ToDTO(lessonPlan));
                }
            }

            return dtos;
        }

        public List<LessonPlanDTO> GetLessonPlanList(long studentID)
        {
            var lessonPlanList = new List<LessonPlanDTO>();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");
                var approvedStsID = new Domain.Setting.SettingBL(null).GetSettingValue<byte?>("Lesson_Approved_StatusID", 5);

                var studentDet = dbContext.Students.AsNoTracking().FirstOrDefault(x => x.StudentIID == studentID);
                
                var lessonPlans = dbContext.LessonPlans.Where(x => x.LessonPlanClassSectionMaps.Any(y => x.LessonPlanStatusID == approvedStsID && y.ClassID == studentDet.ClassID && y.SectionID == studentDet.SectionID))
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .Include(i => i.Subject)
                    .Include(i => i.Unit)
                    .Include(i => i.LessonPlanStatus)
                    .Include(i => i.LessonPlanAttachmentMaps)
                    .Include(i => i.LessonPlanTaskMaps)
                    .Include(i => i.LessonPlanTopicMaps)
                    .Include(i => i.LessonPlanClassSectionMaps).ThenInclude(i => i.Class)
                    .Include(i => i.LessonPlanClassSectionMaps).ThenInclude(i => i.Section)
                    .OrderByDescending(lp => lp.CreatedDate) 
                    .AsNoTracking()
                    .ToList();

                if (lessonPlans != null)
                {
                    foreach (var lesson in lessonPlans)
                    {
                        lessonPlanList.Add(ToDTO(lesson));
                    }
                }

                return lessonPlanList;
            }
        }

        private void GetExpectedLearningOutComeString(LessonPlan entity, LessonPlanDTO dto)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var getData = dbContext.ExamTypes.AsNoTracking().FirstOrDefault(x => x.ExamTypeID == entity.ExpectedLearningOutcomeID);
                var getDataMonth = dbContext.Months.AsNoTracking().FirstOrDefault(x => x.MonthID == entity.MonthID);
                dto.ExpectedLearningOutcomeString = getData != null ? getData.ExamTypeDescription : null;
                dto.MonthNameString = getDataMonth != null ? getDataMonth.Description : null;
            }
        }


        public LessonPlanDTO GetLessonPlanByLessonID(long LessonPlanID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {

                // Fetch the specific lesson plan by its ID
                var lessonPlan = dbContext.LessonPlans
                    .Where(lp => lp.LessonPlanIID == LessonPlanID)
                    .Include(lp => lp.LessonPlanStatus)
                    .Include(lp => lp.Unit)
                    .Include(lp => lp.LessonPlanAttachmentMaps)
                    .Include(lp => lp.LessonPlanTaskMaps)
                    .Include(lp => lp.LessonPlanTopicMaps)
                    .AsNoTracking()
                    .FirstOrDefault();

                // Return the DTO if a lesson plan is found
                if (lessonPlan != null)
                {
                    return ToDTO(lessonPlan);
                }

                // Return null if no lesson plan is found
                return null;
            }
        }

        public List<SubjectWiseLessonPlanDTO> GetLessonPlanListBySubject(long studentID)
        {
            var subjectWiseLessonPlanList = new List<SubjectWiseLessonPlanDTO>();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var approvedStsID = new Domain.Setting.SettingBL(null).GetSettingValue<byte?>("Lesson_Approved_StatusID", 5);

                var studentDet = dbContext.Students.AsNoTracking().FirstOrDefault(x => x.StudentIID == studentID);

                if (studentDet == null)
                    return subjectWiseLessonPlanList; // Return empty list if student not found

                var lessonPlans = dbContext.LessonPlans
                    .Where(x => x.LessonPlanClassSectionMaps.Any(y => x.LessonPlanStatusID == approvedStsID && y.ClassID == studentDet.ClassID && y.SectionID == studentDet.SectionID))
                    .Include(i => i.Subject)
                    .Include(i => i.Unit)
                    .Include(i => i.LessonPlanStatus)
                    .Include(i => i.LessonPlanAttachmentMaps)
                    .Include(i => i.LessonPlanClassSectionMaps).ThenInclude(i => i.Class)
                    .Include(i => i.LessonPlanClassSectionMaps).ThenInclude(i => i.Section)
                    .AsNoTracking()
                    .ToList();

                if (lessonPlans.Any())
                {
                    var groupedBySubject = lessonPlans
                        .GroupBy(lp => lp.Subject)
                        .ToList();

                    foreach (var group in groupedBySubject)
                    {
                        var subjectWiseDTO = new SubjectWiseLessonPlanDTO
                        {
                            Subject = group.Key != null ? new KeyValuePair<string, string>(group.Key.SubjectID.ToString(), group.Key.SubjectName) : null,
                            LessonPlans = group.Select(ToDTO).ToList()
                        };

                        subjectWiseLessonPlanList.Add(subjectWiseDTO);
                    }
                }
            }

            return subjectWiseLessonPlanList;
        }
        public List<LessonPlanDTO> ExtractUploadedFiles()
        {
            var lessonPlan = new LessonPlanDTO
            {
                LessonPlanIID = 27515,
                Title = "English Lesson Plan",
                TotalHours = 2
            };

            // Returning as a List (correcting the return type)
            return new List<LessonPlanDTO> { lessonPlan };
        }

     
    }
}