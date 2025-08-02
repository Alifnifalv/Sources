using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework;
using System;
using Eduegate.Services.Contracts.School.Students;
using System.Linq;
using System.Collections.Generic;
using Eduegate.Domain.Repository;
using Eduegate.Domain.Mappers.Notification.Helpers;
using Eduegate.Domain.Mappers.Notification;
using System.Net.Mail;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.School.Fees
{
    public class MailFeeDueStatementMapper : DTOEntityDynamicMapper
    {
        public static MailFeeDueStatementMapper Mapper(CallContext context)
        {
            var mapper = new MailFeeDueStatementMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<FeeDueCancellationDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }


        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private FeeDueCancellationDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.StudentFeeConcessions
                    .Where(X => X.StudentFeeConcessionIID == IID)
                    .AsNoTracking().FirstOrDefault();

                var student = dbContext.Students.Where(a => a.StudentIID == entity.StudentID).AsNoTracking().FirstOrDefault();
                var studentName = student.AdmissionNumber + "-" + student.FirstName + " " + student.MiddleName + " " + student.LastName;

                var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

                var FeeDueCancellationDTO = new FeeDueCancellationDTO()
                {
                };

                return FeeDueCancellationDTO;
            }
        }

        #region Send feedue Mail and PushNotification to parents
        public MailFeeDueStatementReportDTO SendFeeDueMailReportToParent(MailFeeDueStatementReportDTO gridData)
        {
            MutualRepository mutualRepository = new MutualRepository();
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var todayDate = DateTime.Now;

                long fromLoginID = _context?.LoginID != null ? (long)_context.LoginID : 2;

                var settings = NotificationSetting.GetParentAppSettings();
                var title = "Fee Due Statement as on : " + gridData.AsOnDate.ToString();
                var message = gridData.AdmissionNo + " - Fee due statement as on date " + gridData.AsOnDate;
                long toLoginID = (long)gridData.ParentLoginID;

                // check today's notification already send to same loginID
                var checkOldNotifications = dbContext.NotificationAlerts
                    .Where(c => c.Message == message && c.NotificationDate.Value.Day == todayDate.Day && c.NotificationDate.Value.Month == todayDate.Month && c.NotificationDate.Value.Year == todayDate.Year)
                    .AsNoTracking().FirstOrDefault();

                if (checkOldNotifications == null && toLoginID != 0)
                {
                    PushNotificationMapper.SendAndSavePushNotification(toLoginID, fromLoginID, message, title, settings);
                }
                else
                {
                    gridData.returnMessage = "Fee due statement already send to " + gridData.StudentName;
                }

                return gridData;
            }

        }
        #endregion

        public List<MailFeeDueStatementReportDTO> GetFeeDueDatasForReportMail(DateTime asOnDate, int classID, int? sectionID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var feeDueStudentList = new List<MailFeeDueStatementReportDTO>();

                var getFeeDues = sectionID == null ?
                    //sectionID not have value
                    dbContext.StudentFeeDues.Where(x => x.DueDate <= asOnDate
                && x.Student.IsActive == true && x.Student.ClassID == classID && x.CollectionStatus == false && x.IsCancelled == false)
                    .Include(i => i.Student).ThenInclude(i => i.Parent)
                    .Include(i => i.School)
                    .Include(i => i.Class)
                    .OrderBy(o => o.Student.AdmissionNumber).AsNoTracking().ToList()
                    //when classID and SectionID have
                    : dbContext.StudentFeeDues.Where(x => x.DueDate <= asOnDate &&
                    x.Student.IsActive == true && x.Student.ClassID == classID && x.Student.SectionID == sectionID &&
                    x.CollectionStatus == false && x.IsCancelled == false)
                    .Include(i => i.Student).ThenInclude(i => i.Parent)
                    .Include(i => i.School)
                    .Include(i => i.Class)
                    .OrderBy(o => o.Student.AdmissionNumber).AsNoTracking().ToList();

                foreach (var studs in getFeeDues)
                {
                    if (studs != null)
                    {
                        if (!feeDueStudentList.Any(x => x.StudentID == studs.StudentId))
                        {
                            feeDueStudentList.Add(new MailFeeDueStatementReportDTO
                            {
                                IsSelected = true,
                                StudentID = studs.Student?.StudentIID,
                                AdmissionNo = studs.Student?.AdmissionNumber,
                                FeeDueDate = studs.DueDate.ToString(),
                                AsOnDate = asOnDate.ToString("MM/dd/yyyy"),
                                SchoolID = studs?.SchoolID,
                                SchoolName = studs?.School?.SchoolName,
                                Class = studs.Class?.ClassDescription,
                                StudentName = studs.Student.FirstName + " " + studs.Student?.MiddleName + " " + studs.Student?.LastName,
                                ParentContact = studs.Student?.Parent?.GuardianPhone != null ? studs.Student?.Parent?.GuardianPhone :
                                                studs.Student?.Parent?.PhoneNumber,
                                ParentEmailID = studs.Student?.Parent?.GaurdianEmail != null ? studs.Student?.Parent?.GaurdianEmail :
                                                studs.Student?.Parent?.FatherEmailID,
                                ParentLoginID = studs.Student?.Parent?.LoginID,
                            });
                        }
                    }
                }

                return feeDueStudentList;
            }

        }

    }
}