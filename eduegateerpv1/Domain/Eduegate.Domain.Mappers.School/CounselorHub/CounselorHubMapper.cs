using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Mappers.Notification;
using Eduegate.Domain.Mappers.Notification.Helpers;
using Eduegate.Domain.Mappers.School.School;
using Eduegate.Domain.Mappers.School.Students;
using Eduegate.Domain.Repository;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Extensions;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Enums.Notifications;
using Eduegate.Services.Contracts.Payroll;
using Eduegate.Services.Contracts.School.Circulars;
using Eduegate.Services.Contracts.School.CounselorHub;
using Eduegate.Services.Contracts.School.Exams;
using Eduegate.Services.Contracts.School.Students;
using EntityGenerator.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Eduegate.Domain.Mappers.School.CounselorHub
{
    public class CounselorHubMapper : DTOEntityDynamicMapper
    {
        public static CounselorHubMapper Mapper(CallContext context)
        {
            var mapper = new CounselorHubMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<CounselorHubDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private CounselorHubDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.CounselorHubs.Where(X => X.CounselorHubIID == IID)
                    .Include(i => i.AcademicYear)
                    .Include(i => i.CounselorHubMaps).ThenInclude(i => i.Class)
                    .Include(i => i.CounselorHubMaps).ThenInclude(i => i.Section)
                    .Include(i => i.CounselorHubMaps).ThenInclude(i => i.Class)
                    .Include(i => i.CounselorHubMaps).ThenInclude(i => i.Student)
                    .Include(i => i.CounselorHubAttachmentMaps)
                    .AsNoTracking()
                    .FirstOrDefault();

                var counsDTO = new CounselorHubDTO()
                {
                    CounselorHubIID = entity.CounselorHubIID,
                    SchoolID = entity.SchoolID,
                    AcademicYearID = entity.AcademicYearID,
                    Title = entity.Title,
                    ShortTitle = entity.ShortTitle,
                    Message = entity.Message,
                    CounselorHubEntryDate = entity.CounselorHubEntryDate,
                    CounselorHubExpiryDate = entity.CounselorHubExpiryDate,
                    CounselorHubStatusID = entity.CounselorHubStatusID,
                    AcademicYear = entity.AcademicYearID.HasValue ? new KeyValueDTO() { Key = entity.AcademicYearID.Value.ToString(), Value = entity.AcademicYear.AcademicYearCode + " ( " + entity.AcademicYear.Description + " )" } : new KeyValueDTO(),
                    IsActive = entity.IsActive,
                    IsSendNotification = entity.IsSendNotification,
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedDate = entity.UpdatedDate,
                };

                foreach (var map in entity.CounselorHubMaps)
                {
                    if (!counsDTO.CounselorHubMaps.Any(x => x.ClassID == map.ClassID &&
                        x.SectionID == map.SectionID && x.StudentID == map.StudentID
                        && x.AllClass == map.AllClass && x.AllStudent == map.AllStudent &&
                        x.AllSection == map.AllSection))
                    {
                        counsDTO.CounselorHubMaps.Add(new CounselorHubMapDTO()
                        {
                            CounselorHubMapIID = map.CounselorHubMapIID,
                            ClassID = map.ClassID,
                            AllClass = map.AllClass,
                            AllStudent = map.AllStudent,
                            AllSection = map.AllSection,
                            SectionID = map.SectionID,
                            StudentID = map.StudentID,
                            Class = map.ClassID.HasValue && map.Class != null ? new KeyValueDTO() { Key = map.ClassID.Value.ToString(), Value = map.Class.ClassDescription } : new KeyValueDTO() { Key = null, Value = "All Classes" },
                            Section = map.SectionID.HasValue && map.Section != null ? new KeyValueDTO() { Key = map.SectionID.Value.ToString(), Value = map.Section.SectionName } : new KeyValueDTO() { Key = null, Value = "All Section" },
                            Student = map.StudentID.HasValue && map.Student != null ? new KeyValueDTO() { Key = map.StudentID.Value.ToString(), Value = map.Student.AdmissionNumber + " - " + map.Student.FirstName + " " + map.Student.MiddleName + " " + map.Student.LastName } : new KeyValueDTO() { Key = null, Value = "All Student" },
                            CreatedBy = map.CreatedBy,
                            CreatedDate = map.CreatedDate,
                            UpdatedBy = map.UpdatedBy,
                            UpdatedDate = map.UpdatedDate,
                        });
                    }
                }

                counsDTO.CounselorHubAttachments = new List<CounselorHubAttachmentMapDTO>();
                if (entity.CounselorHubAttachmentMaps.Count > 0)
                {
                    foreach (var attachment in entity.CounselorHubAttachmentMaps)
                    {
                        if (attachment.AttachmentReferenceID.HasValue || !string.IsNullOrEmpty(attachment.AttachmentName))
                        {
                            counsDTO.CounselorHubAttachments.Add(new CounselorHubAttachmentMapDTO()
                            {
                                CounselorHubAttachmentMapIID = attachment.CounselorHubAttachmentMapIID,
                                CounselorHubID = attachment.CounselorHubID.HasValue ? attachment.CounselorHubID : null,
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

                //var lstStudentData = entity.CounselorHubMaps.Where(x => x.StudentID.HasValue).ToList();
                //if (lstStudentData.Count > 0)
                //{
                //    foreach (var studMap in lstStudentData)
                //    {
                //        counsDTO.CounselorHubMaps.Add(new CounselorHubMapDTO()
                //        {
                //            CounselorHubMapIID = studMap.CounselorHubMapIID,
                //            StudentID = studMap.StudentID.HasValue ? studMap.StudentID : null,
                //            StudentName = studMap.StudentID.HasValue || studMap.Student != null ? studMap.Student.AdmissionNumber + " - " + studMap.Student.FirstName + "  " + studMap.Student.MiddleName + "  " + studMap.Student.LastName : null,
                //        });
                //    }
                //}
                //else
                //{

                //}


                return counsDTO;
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as CounselorHubDTO;

            if (toDto.CounselorHubMaps.Count == 0)
            {
                throw new Exception("Please select atleast one Class or Section");
            }

            if (toDto.IsSelected == false)
            {
                throw new Exception("Kindly ensure that the class, section, and student are selected.");
            }

            if (toDto.Message == null || toDto.Message == "")
            {
                throw new Exception("Message cannot be left empty");
            }

            var publishStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<byte>("COUNSELOR_HUB_PUBLISH_STATUS_ID");

            using (var dbContext = new dbEduegateSchoolContext())
            {
                if (toDto.CounselorHubIID != 0)
                {
                    var counselor = dbContext.CounselorHubs.Where(X => X.CounselorHubIID == toDto.CounselorHubIID).AsNoTracking().FirstOrDefault();
                    if (counselor != null)
                    {
                        if (counselor.CounselorHubStatusID == 2)
                        {
                            throw new Exception("This  can't be edit, its already Published!");
                        }
                    }
                }
            }


            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = new Entity.School.Models.CounselorHub()
                {
                    CounselorHubIID = toDto.CounselorHubIID,
                    SchoolID = toDto.SchoolID.HasValue ? toDto.SchoolID : Convert.ToByte(_context.SchoolID),
                    AcademicYearID = toDto.AcademicYearID.HasValue ? toDto.AcademicYearID : _context.AcademicYearID,
                    Title = toDto.Title,
                    ShortTitle = toDto.ShortTitle,
                    Message = toDto.Message,
                    CounselorHubStatusID = toDto.CounselorHubStatusID,
                    IsSendNotification = toDto.IsSendNotification,
                    IsActive = toDto.IsActive,
                    CounselorHubEntryDate = toDto.CounselorHubEntryDate,
                    CounselorHubExpiryDate = toDto.CounselorHubExpiryDate
                };

                var IIDs = toDto.CounselorHubAttachments
                    .Select(a => a.CounselorHubAttachmentMapIID).ToList();

                //delete maps
                var entities = dbContext.CounselorHubAttachmentMaps.Where(x =>
                    x.CounselorHubID == entity.CounselorHubIID &&
                    !IIDs.Contains(x.CounselorHubAttachmentMapIID)).AsNoTracking().ToList();

                if (entities.IsNotNull())
                    dbContext.CounselorHubAttachmentMaps.RemoveRange(entities);


                entity.CounselorHubAttachmentMaps = new List<CounselorHubAttachmentMap>();

                if (toDto.CounselorHubAttachments.Count > 0)
                {
                    foreach (var attach in toDto.CounselorHubAttachments)
                    {
                        if (attach.AttachmentReferenceID.HasValue || !string.IsNullOrEmpty(attach.AttachmentName))
                        {
                            entity.CounselorHubAttachmentMaps.Add(new CounselorHubAttachmentMap()
                            {
                                CounselorHubAttachmentMapIID = attach.CounselorHubAttachmentMapIID,
                                CounselorHubID = attach.CounselorHubID.HasValue ? attach.CounselorHubID : null,
                                AttachmentReferenceID = attach.AttachmentReferenceID,
                                AttachmentName = attach.AttachmentName,
                                AttachmentDescription = attach.AttachmentDescription,

                            });
                        }
                    }
                }
                if (toDto.CounselorHubIID == 0)
                {
                    entity.CreatedBy = Convert.ToInt32(_context.LoginID);
                    entity.CreatedDate = DateTime.Now;
                }
                else
                {
                    entity.UpdatedBy = Convert.ToInt32(_context.LoginID);
                    entity.UpdatedDate = DateTime.Now;
                }


                foreach (var CounsMap in toDto.CounselorHubMaps)
                {
                    if (!entity.CounselorHubMaps.Any(x => x.ClassID == CounsMap.ClassID &&
                       x.SectionID == CounsMap.SectionID && x.StudentID == CounsMap.StudentID
                       && x.AllClass == CounsMap.AllClass && x.AllStudent == CounsMap.AllStudent &&
                       x.AllSection == CounsMap.AllSection))
                    {
                        entity.CounselorHubMaps.Add(new CounselorHubMap()
                        {
                            CounselorHubMapIID = CounsMap.CounselorHubMapIID,
                            ClassID = CounsMap.ClassID,
                            AllClass = CounsMap.AllClass,
                            AllStudent = CounsMap.AllStudent,
                            AllSection = CounsMap.AllSection,
                            SectionID = CounsMap.SectionID,
                            StudentID = CounsMap.StudentID,
                            CreatedBy = CounsMap.CounselorHubMapIID == 0 ? Convert.ToInt32(_context.LoginID) : CounsMap.CreatedBy,
                            CreatedDate = CounsMap.CounselorHubMapIID == 0 ? DateTime.Now : CounsMap.CreatedDate,
                            UpdatedBy = CounsMap.CounselorHubMapIID != 0 ? Convert.ToInt32(_context.LoginID) : CounsMap.UpdatedBy,
                            UpdatedDate = CounsMap.CounselorHubMapIID != 0 ? DateTime.Now : CounsMap.UpdatedDate,
                        });
                    }
                }

                using (var dbContext1 = new dbEduegateSchoolContext())
                {
                    dbContext.CounselorHubs.Add(entity);

                    if (entity.CounselorHubIID == 0)
                    {
                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    }
                    else
                    {
                        var mapEntities = dbContext1.CounselorHubMaps
                            .Where(x => x.CounselorHubID == toDto.CounselorHubIID).AsNoTracking().ToList();

                        if (mapEntities != null && mapEntities.Count > 0)
                        {
                            dbContext1.CounselorHubMaps.RemoveRange(mapEntities);
                        }

                        dbContext1.SaveChanges();

                        if (entity.CounselorHubMaps.Count > 0)
                        {
                            foreach (var counselorMap in entity.CounselorHubMaps)
                            {
                                if (counselorMap.CounselorHubMapIID == 0)
                                {
                                    dbContext.Entry(counselorMap).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                }
                                else
                                {
                                    dbContext.Entry(counselorMap).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                }
                            }
                        }


                        if (entity.CounselorHubAttachmentMaps.Count > 0)
                        {
                            foreach (var atch in entity.CounselorHubAttachmentMaps)
                            {
                                if (atch.CounselorHubAttachmentMapIID == 0)
                                {
                                    dbContext.Entry(atch).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                }
                                else
                                {
                                    dbContext.Entry(atch).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                }
                            }
                        }

                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }

                    dbContext.SaveChanges();

                    if (toDto.IsSendNotification == true && toDto.CounselorHubStatusID == publishStatusID)
                    {
                        SendEmailAndPushNotification(toDto);
                    }
                }
                return ToDTOString(ToDTO(entity.CounselorHubIID));
            }
        }

        private void SendEmailAndPushNotification(CounselorHubDTO counsHubDTO)
        {
            var parentEmails = new List<KeyValueDTO>();
            List<long?> loginIDs = new List<long?>();

            var message = "A counsellor note has been uploaded.";
            var title = "Counsellor Hub";

            var maps = counsHubDTO.CounselorHubMaps.Where(m => m.StudentID.HasValue).ToList();

            if (maps != null)
            {
                foreach (var map in maps)
                {
                    var emailID = ParentMapper.Mapper(_context).GetParentEmailIDByStudentID(map.StudentID);
                    var studentDetails = StudentMapper.Mapper(_context).GetStudentDetailsByStudentID(map.StudentID ?? 0);
                    var classDetails = new Eduegate.Domain.Setting.SettingBL(_context).GetClassDetailByClassID(studentDetails.ClassID.Value);

                    parentEmails.Add(new KeyValueDTO
                    {
                        Key= emailID,
                        Value = classDetails.Code,
                    });

                    var loginID = GetParentLoginIDs(map.StudentID);
                    loginIDs.AddRange(loginID);

                }
            }
            else
            {
                var mapAllStudents = counsHubDTO.CounselorHubMaps.FirstOrDefault(m => m.AllStudent == true);

                if (mapAllStudents != null)
                {
                    var classDetails = new Eduegate.Domain.Setting.SettingBL(_context).GetClassDetailByClassID(mapAllStudents.ClassID.Value);
                    if (mapAllStudents.SectionID.HasValue)
                    {
                        parentEmails.Add(new KeyValueDTO 
                        { 
                            Key = ParentMapper.Mapper(_context).GetParentsEmailIDByClassSection(mapAllStudents.ClassID, mapAllStudents.SectionID).ToString(),
                            Value = classDetails.Code,
                        });
                    }
                    else
                    {
                        parentEmails.Add(new KeyValueDTO
                        {
                            Key = ParentMapper.Mapper(_context).GetParentsEmailIDByClassSection(mapAllStudents.ClassID, mapAllStudents.SectionID).ToString()
                        });
                    }
                }
            }

            foreach (var emailID in parentEmails)
            {
                EmailProcess(emailID.Key, emailID.Value, counsHubDTO);
            }

            var settings = NotificationSetting.GetParentAppSettings();
            foreach (var login in loginIDs)
            {
                long toLoginID = Convert.ToInt32(login);
                long fromLoginID = _context?.LoginID != null ? (long)_context.LoginID : 2;

                PushNotificationMapper.SendAndSavePushNotification(toLoginID, fromLoginID, message, title, settings);
            }
        }

        public void EmailProcess(string toEmail, string classCode, CounselorHubDTO counsHubDTO)
        {
            var hostDet = new Domain.Setting.SettingBL(null).GetSettingValue<string>("HOST_NAME");

            string defaultMail = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DEFAULT_MAIL_ID");

            var emailTemplate = EmailTemplateMapper.Mapper(_context).GetEmailTemplateDetails(EmailTemplates.CounselingMail.ToString());

            var receiptBody = emailTemplate?.EmailTemplate;
            string emailSubject = emailTemplate?.Subject;

            receiptBody = receiptBody?.Replace("{MESSAGE_CONTENT}", counsHubDTO.Title);
            receiptBody = receiptBody?.Replace("{COUNSELLING_TITLE}", counsHubDTO.ShortTitle);


            string emailBody = receiptBody ?? null;


            if (string.IsNullOrEmpty(emailBody))
            {
                emailBody = @"<br />
               <p align='left'>
               Dear Parent/Guardian,<br /></p>
                The Counsellor hub has uploaded a message.<br />
                To view,  please login to your parent portal.<br /><br />
               <br/><br/><p style='font-size:0.7rem;'<b>Please Note: </b>do not reply to this email as it is a computer generated email</p>";
            }

            if (string.IsNullOrEmpty(emailSubject))
            {
                emailSubject = "Counseling";
            }

            try
            {
                classCode = classCode?.ToLower();
                string mailMessage = new Eduegate.Domain.Notification.EmailNotificationBL(_context).PopulateBody(toEmail, emailBody);

                var mailParameters = new Dictionary<string, string>()
                {
                    { "CLASS_CODE", classCode},
                };

                if (emailBody != "")
                {
                    if (hostDet.ToLower() == "live")
                    {
                        new Eduegate.Domain.Notification.EmailNotificationBL(_context).SendMail(toEmail, emailSubject, mailMessage, EmailTypes.CounselingMail, mailParameters);
                    }
                    else
                    {
                        new Eduegate.Domain.Notification.EmailNotificationBL(_context).SendMail(defaultMail, emailSubject, mailMessage, EmailTypes.CounselingMail, mailParameters);
                    }
                }
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception")
                    ? ex.InnerException?.Message : ex.Message;

                Eduegate.Logger.LogHelper<string>.Fatal($"Counseling Mailing failed. Error message: {errorMessage}", ex);
            }
        }

        public List<CounselorHubListDTO> GetCounselorList(long loginID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat", "dd/MM/yyyy");

                var studentDTO = (from s in dbContext.Students
                                  join p in dbContext.Parents on s.ParentID equals p.ParentIID
                                  where p.LoginID == loginID && s.IsActive == true
                                  orderby s.StudentIID ascending
                                  select new StudentDTO()
                                  {
                                      ClassID = s.ClassID,
                                      SectionID = s.SectionID,
                                      SchoolID = s.SchoolID,
                                      StudentIID = s.StudentIID,
                                      StudentFullName = s.AdmissionNumber + " - " + s.FirstName + "  " + s.MiddleName + "  " + s.LastName
                                  }).Distinct().AsNoTracking();

                List<long> allCounselorList = new List<long>();
                var counselorLists = new List<CounselorHubListDTO>();

                foreach (var sData in studentDTO)
                {
                    var counselorhubmaps = new List<CounselorHubMap>();

                    counselorhubmaps = (from n in dbContext.CounselorHubMaps
                                            join m in dbContext.CounselorHubs on n.CounselorHubID equals m.CounselorHubIID
                                            where m.CounselorHubStatusID == 2 && m.CounselorHubExpiryDate.Value.Date >= DateTime.Now.Date
                                                  && m.SchoolID == sData.SchoolID && (n.StudentID == sData.StudentIID)
                                            select n).Distinct()
                                            .Include(i => i.CounselorHub)
                                            .Include(i => i.Class)
                                            .Include(i => i.Section)
                                            .AsNoTracking().ToList();

                    var classSectionCounselorHubs = (from n in dbContext.CounselorHubMaps
                                                     join m in dbContext.CounselorHubs on n.CounselorHubID equals m.CounselorHubIID
                                                     where m.CounselorHubStatusID == 2 && m.CounselorHubExpiryDate.Value.Date >= DateTime.Now.Date
                                                           && m.SchoolID == sData.SchoolID && (n.AllStudent == true)
                                                     select n).Distinct()
                                            .Include(i => i.CounselorHub)
                                            .Include(i => i.Class)
                                            .Include(i => i.Section)
                                            .AsNoTracking().ToList();

                    var allDatas = dbContext.CounselorHubMaps.Where(a => classSectionCounselorHubs.Select(a => a.CounselorHubID).Contains(a.CounselorHubID)).ToList();
                    var classDatas = allDatas.Where(a => a.ClassID == sData.ClassID || a.AllClass == true).Select(a => a.CounselorHubID).ToList();
                    var filteredDatas = allDatas.Where(a => classDatas.Contains(a.CounselorHubID)).ToList();
                    var sectionDatas = filteredDatas.Where(a => a.SectionID == sData.SectionID || a.AllSection == true).ToList();

                    counselorhubmaps.AddRange(sectionDatas);

                    foreach (var maps in counselorhubmaps)
                    {
                        var counselorData = dbContext.CounselorHubs.Where(c => c.CounselorHubIID == maps.CounselorHubID)
                            .Include(i => i.CounselorHubMaps).ThenInclude(i => i.Student)
                            .Include(i => i.AcademicYear)
                            .Include(i => i.School)
                            .Include(i => i.CounselorHubAttachmentMaps)
                            .OrderByDescending(y => y.CounselorHubEntryDate)
                            .ThenByDescending(y => y.CounselorHubIID)
                            .AsNoTracking()
                            .ToList();

                        var counselorList = (from c in counselorData
                                             select new CounselorHubListDTO()
                                             {
                                                 Message = c.Message,
                                                 ShortTitle = c.ShortTitle,
                                                 Title = c.Title,
                                                 CounselorHubEntryDate = c.CounselorHubEntryDate.HasValue ? c.CounselorHubEntryDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                                                 CounselorHubExpiryDate = c.CounselorHubExpiryDate.HasValue ? c.CounselorHubExpiryDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                                                 //Classes = GetName(counselorClassSectionmaps, "Class", c.CounselorHubIID),
                                                 //Sections = GetName(counselorClassSectionmaps, "Section", c.CounselorHubIID),
                                                 //Student = c.CounselorHubMaps.FirstOrDefault(a => a.StudentID != null)?.Student.AdmissionNumber + " - "
                                                 //           + c.CounselorHubMaps.FirstOrDefault(a => a.StudentID != null)?.Student.FirstName + " "
                                                 //           + (c.CounselorHubMaps.FirstOrDefault(a => a.StudentID != null)?.Student.MiddleName ?? "") + " "
                                                 //           + (c.CounselorHubMaps.FirstOrDefault(a => a.StudentID != null)?.Student.LastName ?? ""),
                                                 Student = sData.StudentFullName,
                                                 CounselorHubIID = c.CounselorHubIID,
                                                 CounselorHubAttachments = (from aat in c.CounselorHubAttachmentMaps
                                                                            select new CounselorHubAttachmentMapDTO()
                                                                            {
                                                                                CounselorHubAttachmentMapIID = aat.CounselorHubAttachmentMapIID,
                                                                                CounselorHubID = aat.CounselorHubID,
                                                                                Notes = aat.Notes,
                                                                                AttachmentName = aat.AttachmentName,
                                                                                AttachmentDescription = aat.AttachmentDescription,
                                                                                AttachmentReferenceID = aat.AttachmentReferenceID,
                                                                                StudentID = aat.StudentID,
                                                                            }).ToList(),
                                             });
                        counselorLists.AddRange(counselorList);
                    }
                }

                return counselorLists.OrderByDescending(a => a.CounselorHubEntryDate).ToList();
            }
        }

        private string GetName(List<CounselorHubMap> CounselorMapList, string fieldType, long CounselorHubID)
        {
            string _sStudent_IDs = string.Empty;

            if (fieldType == "Class")
            {
                var classname = CounselorMapList.Where(y => y.CounselorHubID == CounselorHubID && y.ClassID != null);
                return classname.Count() > 0 ? string.Join(",", classname.Select(x => x.Class.ClassDescription)) : "All Class";


            }
            if (fieldType == "Section")
            {
                var section = CounselorMapList.Where(y => y.CounselorHubID == CounselorHubID && y.SectionID != null);
                return section.Count() > 0 ? string.Join(",", section.Select(x => x.Section.SectionName)) : "All Section";
            }
            return null;
        }

        public List<long?> GetParentLoginIDs(long? studentID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var loginIDs = dbContext.Students.Where(a => a.StudentIID == studentID)
                    .Include(i => i.Parent)
                    .Select(i => i.Parent.LoginID)
                    .ToList();

                return loginIDs;
            }

        }


    }
}
