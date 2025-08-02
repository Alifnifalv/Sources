using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Mappers.Notification;
using Eduegate.Domain.Mappers.Notification.Helpers;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Extensions;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.School.Academics;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;

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
                    .Include(i => i.LessonPlanStatus)
                    .Include(i => i.LessonPlanAttachmentMaps)
                    .Include(i => i.LessonPlanClassSectionMaps).ThenInclude(i => i.Class)
                    .Include(i => i.LessonPlanClassSectionMaps).ThenInclude(i => i.Section)
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
                ActionPlan = entity.ActionPlan
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
                    ActionPlan = toDto.ActionPlan
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

                foreach (var data in toDto.LessonPlanClassSectionMaps)
                {
                    entity.LessonPlanClassSectionMaps.Add(new LessonPlanClassSectionMap()
                    {
                        ClassID = data.ClassID,
                        SectionID = data.SectionID,
                        LessonPlanID = toDto.LessonPlanIID,
                    });
                }

                if (toDto.LessonPlanIID == 0)
                {
                    entity.CreatedBy = Convert.ToInt32(_context.LoginID);
                    entity.CreatedDate = DateTime.Now;
                }
                else
                {
                    entity.CreatedBy = toDto.CreatedBy;
                    entity.CreatedDate = toDto.CreatedDate;
                    entity.UpdatedBy = Convert.ToInt32(_context.LoginID);
                    entity.UpdatedDate = DateTime.Now;
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

                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();

                #region unwanted field hide

                //NumberOfActivityCompleted = toDto.NumberOfActivityCompleted.HasValue ? toDto.NumberOfActivityCompleted : null,
                //    NumberOfClassTests = toDto.NumberOfClassTests.HasValue ? toDto.NumberOfClassTests : null,
                //    NumberOfPeriods = toDto.NumberOfPeriods.HasValue ? toDto.NumberOfPeriods : null,
                //    HomeWorks = toDto.HomeWorks == null ? null : toDto.HomeWorks,
                //    LearningExperiences = toDto.LearningExperiences == null ? null : toDto.LearningExperiences,
                //    Activity = toDto.Activity == null ? null : toDto.Activity,
                //    //TeachingAidID = toDto.TeachingAidID,
                //    Resourses = toDto.Resourses,
                //    SkillFocused = toDto.SkillFocused,
                //    AllignmentToVisionAndMission = toDto.AllignmentToVisionAndMission,
                //    Connectivity = toDto.Connectivity,
                //    CrossDisciplinaryConnection = toDto.CrossDisciplinaryConnection,
                //    Introduction = toDto.Introduction,
                //    TeachingMethodology = toDto.TeachingMethodology,
                //    Closure = toDto.Closure,
                //    SEN = toDto.SEN,
                //    HighAchievers = toDto.HighAchievers,
                //    StudentsWhoNeedImprovement = toDto.StudentsWhoNeedImprovement,
                //    PostLessonEvaluation = toDto.PostLessonEvaluation,
                //    Reflections = toDto.Reflections,
                //    ExpectedLearningOutcomeID = toDto.ExpectedLearningOutcomeID.HasValue ? toDto.ExpectedLearningOutcomeID : null,
                //    Date3 = toDto.Date3.HasValue ? toDto.Date3 : (DateTime?)null,
                //    TotalHours = toDto.TotalHours.HasValue ? toDto.TotalHours : null,
                //if (toDto.ClassID == null)
                //{
                //    throw new Exception("Please Select Class!");
                //}

                //if (toDto.SectionID == null)
                //{
                //    throw new Exception("Please Select Section!");
                //}


                #endregion
                //
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
                var currentAcademicYearStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<int?>("CURRENT_ACADEMIC_YEAR_STATUSID", 2);

                var lessonPlans = dbContext.LessonPlans.Where(x => x.Class.ClassTeacherMaps.Any(b => b.TeacherID == employeeID && b.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID && x.SubjectID == b.SubjectID
                    && x.LessonPlanClassSectionMaps.Any(c => c.ClassID == b.ClassID && c.SectionID == b.SectionID)))
                    .Include(i => i.Class).ThenInclude(i => i.ClassTeacherMaps)
                    .Include(i => i.Section)
                    .Include(i => i.Subject)
                    .Include(i => i.LessonPlanStatus)
                    .Include(i => i.LessonPlanAttachmentMaps)
                    .Include(i => i.LessonPlanTaskMaps)
                    .Include(i => i.LessonPlanTopicMaps)
                    .Include(i => i.LessonPlanClassSectionMaps).ThenInclude(i => i.Class)
                    .Include(i => i.LessonPlanClassSectionMaps).ThenInclude(i => i.Section)
                    .AsNoTracking()
                    .ToList();

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
                    .Include(i => i.LessonPlanStatus)
                    .Include(i => i.LessonPlanAttachmentMaps)
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

    }
}