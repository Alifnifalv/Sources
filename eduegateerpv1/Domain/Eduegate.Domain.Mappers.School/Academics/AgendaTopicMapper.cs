using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Framework;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Services.Contracts.School.Academics;
using Eduegate.Services.Contracts.School.Students;
using Eduegate.Framework.Extensions;
using Eduegate.Domain.Mappers.Notification.Helpers;
using Eduegate.Domain.Mappers.Notification;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Eduegate.Domain.Mappers.School.Academics
{
    public class AgendaTopicMapper : DTOEntityDynamicMapper
    {
        public static AgendaTopicMapper Mapper(CallContext context)
        {
            var mapper = new AgendaTopicMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<AgendaDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private AgendaDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.Agendas.Where(x => x.AgendaIID == IID)
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .Include(i => i.Subject)
                    .Include(i => i.AgendaStatus)
                    .Include(i => i.AcademicYear)
                    .Include(i => i.AgendaTopicMaps)
                    .Include(i => i.AgendaSectionMaps).ThenInclude(i => i.Section)
                    .Include(i => i.AgendaTaskMaps).ThenInclude(i => i.TaskType)
                    .Include(i => i.AgendaTaskMaps).ThenInclude(i => i.AgendaTaskAttachmentMaps)
                    .AsNoTracking()
                    .FirstOrDefault();

                return ToDTO(entity);
            }
        }

        private AgendaDTO ToDTO(Agenda entity)
        {
            var topic = new AgendaDTO();
            if (entity != null)
            {
                var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

                topic = new AgendaDTO()
                {
                    ClassID = entity.ClassID,
                    SubjectID = entity.SubjectID,
                    SectionID = entity.SectionID,
                    AgendaIID = entity.AgendaIID,
                    AcademicYearID = entity.AcademicYearID,
                    SchoolID = entity.SchoolID,
                    Title = entity.Title,
                    Date1 = entity.Date1,
                    Date1String = entity.Date1.Value.ToString(dateFormat, CultureInfo.InvariantCulture),
                    Class = entity.Class == null ? new KeyValueDTO() : new KeyValueDTO() { Key = entity.ClassID.ToString(), Value = entity.Class.ClassDescription },
                    Section = entity.Section == null ? new KeyValueDTO() : new KeyValueDTO() { Key = entity.SectionID.ToString(), Value = entity.Section.SectionName },
                    Subject = entity.Subject == null ? new KeyValueDTO() : new KeyValueDTO() { Key = entity.Subject.SubjectID.ToString(), Value = entity.Subject.SubjectName },
                    AgendaStatus = entity.AgendaStatusID.HasValue ? new KeyValueDTO() { Key = entity.AgendaStatusID.ToString(), Value = entity.AgendaStatus.StatusName } : new KeyValueDTO(),
                    AcademicYear = entity.AcademicYearID != null ? new KeyValueDTO() { Key = entity.AcademicYear.AcademicYearID.ToString(), Value = (string.IsNullOrEmpty(entity.AcademicYear.AcademicYearCode) ? " " : entity.AcademicYear.Description + " " + '(' + entity.AcademicYear.AcademicYearCode + ')') } : null,
                    IsSendPushNotification = false,
                };

                topic.SectionList = new List<KeyValueDTO>();
                foreach (var map in entity.AgendaSectionMaps)
                {
                    topic.SectionList.Add(new KeyValueDTO()
                    {
                        Key = map.SectionID.ToString(),
                        Value = map.Section.SectionName
                    });
                }

                topic.AgendaTopicMap = new List<AgendaTopicMapDTO>();
                foreach (var lectureMap in entity.AgendaTopicMaps)
                {
                    topic.AgendaTopicMap.Add(new AgendaTopicMapDTO()
                    {
                        Topic = lectureMap.Topic,
                        LectureCode = lectureMap.LectureCode,
                        AgendaID = lectureMap.AgendaID,
                        AgendaTopicMapIID = lectureMap.AgendaTopicMapIID,
                    });
                }

                topic.AgendaTaskMap = new List<AgendaTaskMapDTO>();
                foreach (var taskMap in entity.AgendaTaskMaps)
                {
                    foreach (var taskAttach in taskMap.AgendaTaskAttachmentMaps)
                    {
                        if (taskMap.TaskType != null)
                        {
                            topic.AgendaTaskMap.Add(new AgendaTaskMapDTO()
                            {
                                AgendaTaskMapIID = taskMap.AgendaTaskMapIID,
                                AgendaTopicMapID = taskMap.AgendaTopicMapID,
                                AgendaID = taskMap.AgendaID,
                                Task = taskMap.Task,
                                Date1String = taskMap.StartDate.HasValue ? taskMap.StartDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                                Date2String = taskMap.EndDate.HasValue ? taskMap.EndDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                                TaskTypeID = taskMap.TaskTypeID.HasValue ? taskMap.TaskTypeID : null,
                                TaskType = taskMap.TaskTypeID.HasValue ? new KeyValueDTO()
                                {
                                    Key = taskMap.TaskTypeID.ToString(),
                                    Value = taskMap.TaskType != null ? taskMap.TaskType.TaskType1 : GetTaskTypeByTask(taskMap.TaskTypeID).TaskType.Value
                                } : new KeyValueDTO(),
                                StartDate = taskMap.StartDate,
                                EndDate = taskMap.EndDate,
                                AttachmentReferenceID = taskAttach.AttachmentReferenceID.HasValue ? taskAttach.AttachmentReferenceID : null,
                                AgendacTaskAttachmentMapIID = taskAttach.AgendacTaskAttachmentMapIID,
                                AgendaTaskMapID = taskAttach.AgendaTaskMapID,
                                AttachmentName = taskAttach.AttachmentName,
                            });
                        }
                    }
                }
            }

            return topic;
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as AgendaDTO;

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

            //convert the dto to entity and pass to the repository.
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var ReferenceID = new Domain.Setting.SettingBL(null).GetSettingValue<int?>("AGENDA_TOPIC_REF_ID", 1, 2);

                var publishStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<byte>("AGENDA_PUBLISH_STATUS_ID", 2);

                var entity = new Agenda()
                {
                    AgendaIID = toDto.AgendaIID,
                    Title = toDto.Title,
                    Date1 = toDto.Date1,
                    ReferenceID = ReferenceID.HasValue ? ReferenceID : (int?)null,
                    SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                    AcademicYearID = toDto.AcademicYearID,
                    AgendaStatusID = toDto.AgendaStatusID,
                    ClassID = toDto.ClassID,
                    SubjectID = toDto.SubjectID,
                    CreatedBy = toDto.AgendaIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                    UpdatedBy = toDto.AgendaIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                    CreatedDate = toDto.AgendaIID == 0 ? DateTime.Now : dto.CreatedDate,
                    UpdatedDate = toDto.AgendaIID > 0 ? DateTime.Now : dto.UpdatedDate,
                    //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
                };

                entity.AgendaSectionMaps = new List<AgendaSectionMap>();
                foreach (var sec in toDto.SectionList)
                {
                    entity.AgendaSectionMaps.Add(new AgendaSectionMap()
                    {
                        AgendaID = entity.AgendaIID,
                        SectionID = int.Parse(sec.Key),
                        CreatedBy = entity.CreatedBy,
                        CreatedDate = entity.CreatedDate,
                        UpdatedBy = entity.UpdatedBy,
                        UpdatedDate = entity.UpdatedDate,
                    });
                }

                var IIDs = toDto.AgendaTopicMap
                .Select(a => a.AgendaTopicMapIID).ToList();

                //delete maps
                var entities = dbContext.AgendaTopicMaps.Where(x =>
                    x.AgendaID == entity.AgendaIID &&
                    !IIDs.Contains(x.AgendaTopicMapIID)).AsNoTracking().ToList();

                if (entities.IsNotNull() && entities.Count > 0)
                    dbContext.AgendaTopicMaps.RemoveRange(entities);

                ////delete task maps
                //if (toDto.AgendaTaskMap.Count > 0)
                //{
                //    var taskmapEntities = dbContext.AgendaTaskMaps
                //        .Where(x => x.AgendaID == toDto.AgendaIID)
                //        .AsNoTracking().ToList();

                //    if (taskmapEntities != null && taskmapEntities.Count > 0)
                //    {
                //        dbContext.AgendaTaskMaps.RemoveRange(taskmapEntities);
                //    }
                //}

                ////delete attach maps
                //foreach (var map in toDto.AgendaTaskMap)
                //{
                //    var mapEntities = dbContext.AgendaTaskAttachmentMaps
                //        .Where(x => x.AgendaTaskMapID == map.AgendaTaskMapID)
                //        .AsNoTracking().ToList();

                //    if (mapEntities != null && mapEntities.Count > 0)
                //    {
                //        dbContext.AgendaTaskAttachmentMaps.RemoveRange(mapEntities);
                //    }
                //}

                entity.AgendaTopicMaps = new List<AgendaTopicMap>();
                foreach (var map in toDto.AgendaTopicMap)
                {
                    entity.AgendaTopicMaps.Add(new AgendaTopicMap()
                    {
                        AgendaTopicMapIID = map.AgendaTopicMapIID,
                        AgendaID = toDto.AgendaIID,
                        Topic = map.Topic,
                        LectureCode = map.LectureCode,
                        CreatedBy = map.AgendaTopicMapIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                        UpdatedBy = map.AgendaTopicMapIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                        CreatedDate = map.AgendaTopicMapIID == 0 ? DateTime.Now : dto.CreatedDate,
                        UpdatedDate = map.AgendaTopicMapIID > 0 ? DateTime.Now : dto.UpdatedDate,
                    });
                }

                var taskMapIIDs = toDto.AgendaTaskMap
                .Select(a => a.AgendaTaskMapIID).ToList();

                //delete maps
                var taskMapEntities = dbContext.AgendaTaskMaps.Where(x =>
                    x.AgendaID == entity.AgendaIID &&
                    !taskMapIIDs.Contains(x.AgendaTaskMapIID)).AsNoTracking().ToList();

                if (taskMapEntities.IsNotNull() && taskMapEntities.Count > 0)
                {
                    foreach (var map in taskMapEntities)
                    {
                        var attachmentMaps = dbContext.AgendaTaskAttachmentMaps.Where(a => a.AgendaTaskMapID == map.AgendaTaskMapIID).AsNoTracking().ToList();
                        if (attachmentMaps != null && attachmentMaps.Count > 0)
                        {
                            dbContext.AgendaTaskAttachmentMaps.RemoveRange(attachmentMaps);
                        }
                    }

                    dbContext.AgendaTaskMaps.RemoveRange(taskMapEntities);
                }

                entity.AgendaTaskMaps = new List<AgendaTaskMap>();
                foreach (var taskmap in toDto.AgendaTaskMap)
                {
                    List<AgendaTaskAttachmentMap> taskAttachMap = new List<AgendaTaskAttachmentMap>();
                    if (taskmap.TaskTypeID != null)
                    {
                        taskAttachMap.Add(new AgendaTaskAttachmentMap()
                        {
                            AgendacTaskAttachmentMapIID = taskmap.AgendacTaskAttachmentMapIID,
                            AgendaTaskMapID = taskmap.AgendaTopicMapID,
                            CreatedBy = taskmap.AgendacTaskAttachmentMapIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                            UpdatedBy = taskmap.AgendacTaskAttachmentMapIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                            CreatedDate = taskmap.AgendacTaskAttachmentMapIID == 0 ? DateTime.Now : dto.CreatedDate,
                            UpdatedDate = taskmap.AgendacTaskAttachmentMapIID > 0 ? DateTime.Now : dto.UpdatedDate,
                            //TimeStamps = taskmap.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
                            AttachmentReferenceID = taskmap.AttachmentReferenceID,
                            AttachmentName = taskmap.AttachmentName,
                        });
                    }

                    if (taskmap.TaskTypeID != null)
                    {
                        entity.AgendaTaskMaps.Add(new AgendaTaskMap()
                        {
                            AgendaTaskMapIID = taskmap.AgendaTaskMapIID,
                            AgendaTopicMapID = taskmap.AgendaTopicMapID,
                            CreatedBy = taskmap.AgendaTaskMapIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                            UpdatedBy = taskmap.AgendaTaskMapIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                            CreatedDate = taskmap.AgendaTaskMapIID == 0 ? DateTime.Now : dto.CreatedDate,
                            UpdatedDate = taskmap.AgendaTaskMapIID > 0 ? DateTime.Now : dto.UpdatedDate,
                            //TimeStamps = taskmap.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
                            Task = taskmap.Task,
                            TaskTypeID = taskmap.TaskTypeID,
                            StartDate = taskmap.StartDate,
                            EndDate = taskmap.EndDate,
                            AgendaTaskAttachmentMaps = taskAttachMap,
                        });
                    }
                }

                dbContext.Agendas.Add(entity);
                if (entity.AgendaIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    if (entity.AgendaSectionMaps.Count > 0)
                    {
                        using (var dbContext1 = new dbEduegateSchoolContext())
                        {
                            var oldSectionMaps = dbContext1.AgendaSectionMaps.Where(s => s.AgendaID == entity.AgendaIID).AsNoTracking().ToList();

                            if (oldSectionMaps != null && oldSectionMaps.Count > 0)
                            {
                                dbContext1.AgendaSectionMaps.RemoveRange(oldSectionMaps);

                                dbContext1.SaveChanges();
                            }
                        }

                        foreach (var secMap in entity.AgendaSectionMaps)
                        {
                            if (secMap.AgendaSectionMapIID == 0)
                            {
                                dbContext.Entry(secMap).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            }
                            else
                            {
                                dbContext.Entry(secMap).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }
                        }
                    }

                    foreach (var map in entity.AgendaTopicMaps)
                    {
                        if (map.AgendaTopicMapIID == 0)
                        {
                            dbContext.Entry(map).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                        else
                        {
                            dbContext.Entry(map).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                    }

                    foreach (var tmap in entity.AgendaTaskMaps)
                    {
                        if (tmap.AgendaTaskMapIID == 0)
                        {
                            dbContext.Entry(tmap).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                        else
                        {
                            foreach (var atch in tmap.AgendaTaskAttachmentMaps)
                            {
                                if (atch.AgendacTaskAttachmentMapIID == 0)
                                {
                                    dbContext.Entry(atch).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                }
                                else
                                {
                                    dbContext.Entry(atch).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                }
                            }

                            dbContext.Entry(tmap).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                    }

                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();

                if (entity.AgendaIID != 0)
                {
                    if (toDto.IsSendPushNotification == true && entity.AgendaStatusID.HasValue)
                    {
                        if (entity.AgendaStatusID == publishStatusID)
                        {
                            SendAndSavePushNotification(entity);
                        }
                    }
                }

                return ToDTOString(ToDTO(entity.AgendaIID));
            }
        }

        private void SendAndSavePushNotification(Agenda entity)
        {
            var loginIDs = GetParentLoginIDs(entity);

            var message = "A Note has been uploaded.";
            var title = "New Note";
            var settings = NotificationSetting.GetParentAppSettings();

            foreach (var login in loginIDs)
            {
                long toLoginID = Convert.ToInt32(login);
                long fromLoginID = _context?.LoginID != null ? (long)_context.LoginID : 2;

                PushNotificationMapper.SendAndSavePushNotification(toLoginID, fromLoginID, message, title, settings);
            }
        }

        private List<long?> GetParentLoginIDs(Agenda entity)
        {
            List<long?> loginIDs = new List<long?>();
            List<int?> sectionIDs = entity.AgendaSectionMaps.Select(a=>a.SectionID).ToList();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                #region old code using for push notification
                //if (entity.ClassID.HasValue && entity.SectionID.HasValue)
                //{
                //    loginIDs = dbContext.Parents.Where(p => p.Students.Any(s => s.ClassID == entity.ClassID && s.SectionID == entity.SectionID && s.IsActive == true) && p.LoginID.HasValue)
                //        .Include(i => i.Students)
                //        .AsNoTracking()
                //        .Select(l => l.LoginID)
                //        .ToList();
                //}
                //if (entity.ClassID.HasValue && !entity.SectionID.HasValue)
                //{
                //    loginIDs = dbContext.Parents.Where(p => p.Students.Any(s => s.ClassID == entity.ClassID) && p.LoginID.HasValue)
                //        .Include(i => i.Students)
                //        .AsNoTracking()
                //        .Select(l => l.LoginID)
                //        .ToList();
                //}
                //if (!entity.ClassID.HasValue && entity.SectionID.HasValue)
                //{
                //    loginIDs = dbContext.Parents.Where(p => p.Students.Any(s => s.SectionID == entity.SectionID) && p.LoginID.HasValue)
                //        .Include(i => i.Students)
                //        .AsNoTracking()
                //        .Select(l => l.LoginID)
                //        .ToList();
                //}
                //if (!entity.ClassID.HasValue && !entity.SectionID.HasValue)
                //{
                //    loginIDs = dbContext.Parents.Where(p => p.LoginID.HasValue)
                //        .AsNoTracking()
                //        .Select(l => l.LoginID)
                //        .ToList();
                //}
                #endregion

                loginIDs = dbContext.Parents.Where(p => p.Students.Any(s => s.ClassID == entity.ClassID && sectionIDs.Contains(s.SectionID) && s.IsActive == true && s.AcademicYearID == entity.AcademicYearID)
                            && p.LoginID.HasValue)
                            .Include(i => i.Students)
                            .AsNoTracking()
                            .Select(l => l.LoginID)
                            .ToList();
            }

            return loginIDs;
        }

        public AgendaTaskMapDTO GetTaskTypeByTask(byte? taskTypeID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var taskType = new AgendaTaskMapDTO();

                var taskDetail = dbContext.TaskTypes.Where(X => X.TaskTypeID == taskTypeID).AsNoTracking().FirstOrDefault();
                var type = taskDetail != null ? dbContext.TaskTypes.Where(X => X.TaskTypeID == taskDetail.TaskTypeID)
                    .Include(i => i.TaskType1)
                    .AsNoTracking().FirstOrDefault() : null;

                if (type != null)
                {
                    taskType.TaskType = new KeyValueDTO()
                    {
                        Key = type.TaskTypeID.ToString(),
                        Value = type.TaskType1
                    };
                }

                return taskType;
            }
        }

        public int GetAgendaCount(long loginID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var publishStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<byte>("AGENDA_PUBLISH_STATUS_ID", 2);

                var studentList = dbContext.Students.Where(s => s.Parent.LoginID == loginID).AsNoTracking().ToList();

                var agendaList = new List<Agenda>();

                foreach (var studentDet in studentList)
                {
                    var agendas = dbContext.Agendas
                        .Include(i => i.AgendaTaskMaps)
                        .Include(i => i.Class)
                        .Include(i => i.AgendaSectionMaps)
                        .Include(i => i.AgendaTopicMaps)
                        .Where(X => X.AcademicYearID == studentDet.AcademicYearID && X.AgendaStatusID == publishStatusID &&
                        X.ClassID == studentDet.ClassID && X.AgendaSectionMaps.Any(s => s.SectionID == studentDet.SectionID))
                        .AsNoTracking().ToList();

                    foreach (var agenda in agendas)
                    {
                        agendaList.Add(agenda);
                    }
                }

                return agendaList.Count();
            }
        }

        #region old function GetAgendaList
        //public List<AgendaDTO> GetAgendaList(long studentID)
        //{
        //    var agendaList = new List<AgendaDTO>();
        //    using (var dbContext = new dbEduegateSchoolContext())
        //    {
        //        var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");
        //        var studentDTO = (from s in dbContext.Students

        //                          where (s.StudentIID == studentID && s.IsActive == true)

        //                          select new StudentDTO()
        //                          {
        //                              ClassID = s.ClassID,
        //                              SectionID = s.SectionID,
        //                              SchoolID = s.SchoolID
        //                          });
        //        var agendas = dbContext.Agendas.Where(y => y.AgendaStatusID == 2
        //                          && studentDTO.Select(z => z.SchoolID).Contains(y.SchoolID)
        //                          && ((studentDTO.Select(z => z.ClassID).Contains(y.ClassID)
        //                          && (studentDTO.Select(z => z.SectionID).Contains(y.SectionID) || y.SectionID == null)))).OrderByDescending(x=>x.Date1).ToList();

        //        if (agendas.Count > 0)
        //            agendaList = (from lp in agendas
        //                          select new AgendaDTO()
        //                          {
        //                              ClassID = lp.ClassID,
        //                              SubjectID = lp.SubjectID,
        //                              SectionID = lp.SectionID,
        //                              AgendaIID = lp.AgendaIID,
        //                              AcademicYearID = lp.AcademicYearID,
        //                              SchoolID = lp.SchoolID,
        //                              Title = lp.Title,
        //                              Date1=lp.Date1,
        //                              Class = lp.Class == null ? new KeyValueDTO() : new KeyValueDTO() { Key = lp.ClassID.ToString(), Value = lp.Class.ClassDescription },
        //                              Section = lp.Section == null ? new KeyValueDTO() : new KeyValueDTO() { Key = lp.SectionID.ToString(), Value = lp.Section.SectionName },
        //                              Subject = lp.Subject == null ? new KeyValueDTO() : new KeyValueDTO() { Key = lp.Subject.SubjectID.ToString(), Value = lp.Subject.SubjectName },
        //                              AcademicYear = lp.AcademicYear == null ? new KeyValueDTO() : new KeyValueDTO() { Key = lp.AcademicYear.AcademicYearCode.ToString(), Value = lp.AcademicYear.Description + ' ' + '(' + lp.AcademicYear.AcademicYearCode + ')' },
        //                              School = lp.School.SchoolName,
        //                              AgendaTaskMap = (from lpTask in lp.AgendaTaskMaps
        //                                               select new AgendaTaskMapDTO()
        //                                               {
        //                                                   AgendaTaskMapIID = lpTask.AgendaTaskMapIID,
        //                                                   AgendaTopicMapID = lpTask.AgendaTopicMapID,
        //                                                   Task = lpTask.Task,
        //                                                   TaskTypeID = lpTask.TaskTypeID.HasValue ? lpTask.TaskTypeID : null,
        //                                                   TaskType = lpTask.TaskTypeID.HasValue ? GetTaskTypeByTask(lpTask.TaskTypeID).TaskType : null,
        //                                                   StartDate = lpTask.StartDate,
        //                                                   EndDate = lpTask.EndDate,
        //                                                   AgendaTaskAttachmentMaps = (from aat in lpTask.AgendaTaskAttachmentMaps//.AsEnumerable().AsQueryable()

        //                                                                               select new AgendaTaskAttachmentMapDTO()
        //                                                                               {
        //                                                                                   AgendacTaskAttachmentMapIID = aat.AgendacTaskAttachmentMapIID,
        //                                                                                   AgendaTaskMapID = aat.AgendaTaskMapID,
        //                                                                                   Notes = aat.Notes,
        //                                                                                   AttachmentName = aat.AttachmentName,
        //                                                                                   AttachmentDescription = aat.AttachmentDescription,
        //                                                                                   AttachmentReferenceID = aat.AttachmentReferenceID,
        //                                                                               }).ToList(),
        //                                               }).ToList(),
        //                              Topic = lp.AgendaTopicMaps.Select(x => x.Topic).FirstOrDefault(),
        //                              LectureCode = lp.AgendaTopicMaps.Select(x => x.LectureCode).FirstOrDefault()

        //                          }).ToList();
        //        return agendaList;
        //    }

        //}
        #endregion

        public List<AgendaDTO> GetAgendaList(long studentID)
        {
            var agendaList = new List<AgendaDTO>();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

                var publishStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<byte>("AGENDA_PUBLISH_STATUS_ID", 2);

                var studentDet = dbContext.Students.Where(x => x.StudentIID == studentID).AsNoTracking().FirstOrDefault();

                var agendas = dbContext.Agendas
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .Include(i => i.Subject)
                    .Include(i => i.AgendaStatus)
                    .Include(i => i.AcademicYear)
                    .Include(i => i.AgendaTopicMaps)
                    .Include(i => i.AgendaSectionMaps).ThenInclude(i => i.Section)
                    .Include(i => i.AgendaTaskMaps).ThenInclude(i => i.TaskType)
                    .Include(i => i.AgendaTaskMaps).ThenInclude(i => i.AgendaTaskAttachmentMaps)
                    .Where(X => X.AcademicYearID == studentDet.AcademicYearID && X.AgendaStatusID == publishStatusID &&
                    X.ClassID == studentDet.ClassID && X.AgendaSectionMaps.Any(s => s.SectionID == studentDet.SectionID))
                    .AsNoTracking().ToList().OrderByDescending(i => i.Date1);

                foreach (var agenda in agendas)
                {
                    agendaList.Add(ToDTO(agenda));
                }

                return agendaList;
            }
        }

    }
}