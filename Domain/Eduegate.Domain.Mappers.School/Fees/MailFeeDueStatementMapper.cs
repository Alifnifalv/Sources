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
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Eduegate.Domain.Mappers.Notification.Notifications;
using Eduegate.Domain.Mappers.Notification.Notifications.Helpers;
using Eduegate.Services.Contracts.School.Fees;

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
                var asOnDateString = "";

                long fromLoginID = _context?.LoginID != null ? (long)_context.LoginID : 2;

                if (DateTime.TryParseExact(gridData.AsOnDate, "MM/dd/yyyy", CultureInfo.InvariantCulture,
                       DateTimeStyles.None, out DateTime parsedDate))
                {
                    asOnDateString = parsedDate.ToString(dateFormat, CultureInfo.InvariantCulture);
                }
                else
                {
                    asOnDateString = gridData.AsOnDate;
                }

                var settings = NotificationSetting.GetParentAppSettings();
                var title = "Fee Due Statement as on : " + asOnDateString;
                var message = gridData.AdmissionNo + " - Fee due statement as on date " + asOnDateString;
                long toLoginID = (long)gridData.ParentLoginID;

                // check today's notification already send to same loginID
                var checkOldNotifications = dbContext.NotificationAlerts
                    .Where(c => c.Message == message && c.NotificationDate.Value.Day == todayDate.Day && c.NotificationDate.Value.Month == todayDate.Month && c.NotificationDate.Value.Year == todayDate.Year)
                    .AsNoTracking().FirstOrDefault();

                if (checkOldNotifications == null && toLoginID != 0)
                {
                    PushNotificationMapper.SendAndSavePushNotification(toLoginID, fromLoginID, message, title, settings);
                    gridData.IsError = false;
                }
                else
                {
                    gridData.IsError = true;
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

                var query = dbContext.StudentFeeDues
                        .Include(x => x.FeeDueFeeTypeMaps)
                        .Where(x => x.DueDate <= asOnDate &&
                        x.Student.IsActive == true &&
                        x.Student.ClassID == classID && x.Student.SectionID == sectionID && x.CollectionStatus == false && x.IsCancelled == false &&
                        x.FeeDueFeeTypeMaps.Any(f => f.Status == false));

                var className = dbContext.Classes.FirstOrDefault(c => c.ClassID == classID);

                var getFeeDues = query
                    .Include(i => i.Student).ThenInclude(i => i.Parent)
                    .Include(i => i.School)
                    .Include(i => i.Class)
                    .OrderBy(o => o.Student.AdmissionNumber)
                    .AsNoTracking()
                    .ToList();

                // Step 1: Get distinct StudentIds
                var studentIDs = getFeeDues.Select(x => x.StudentId).Distinct().ToHashSet();

                // Step 2: Identify students with pending fees
                var studentsWithPendingFees = new HashSet<long>();

                foreach (var studentId in studentIDs)
                {
                    var pendingFees = FeeCollectionMapper.Mapper(_context).FillPendingFees((long)studentId);
                    if (pendingFees != null)
                    {
                        var finalDue = pendingFees.Sum(a => a.Amount) - pendingFees.Sum(a => a.CrDrAmount ?? 0) - pendingFees.Sum(a => a.CollAmount ?? 0);
                        if (finalDue > 0)
                        {
                            // Add all student IDs from pending fees
                            foreach (var fee in pendingFees)
                            {
                                studentsWithPendingFees.Add((long)studentId);
                            }
                        }
                    }
                }

                var existingStudentIds = new HashSet<long?>(feeDueStudentList.Select(x => x.StudentID));

                var asOnDateStr = asOnDate.ToString("MM/dd/yyyy");

                // Step 3: Build the list, avoiding per-element checks
                foreach (var feeRecord in getFeeDues)
                {
                    if (feeRecord == null) continue;

                    var studentId = feeRecord.StudentId;

                    // Check if student has pending fees or already processed
                    if (studentsWithPendingFees.Contains((long)studentId) && !existingStudentIds.Contains(studentId))
                    {
                        var student = feeRecord.Student;
                        var parent = student?.Parent;

                        feeDueStudentList.Add(new MailFeeDueStatementReportDTO
                        {
                            IsSelected = true,
                            StudentID = student?.StudentIID,
                            AdmissionNo = feeRecord.Student?.AdmissionNumber,
                            FeeDueDate = feeRecord.DueDate.ToString(),
                            AsOnDate = asOnDateStr,
                            SchoolID = feeRecord.SchoolID,
                            SchoolName = feeRecord.School?.SchoolName,
                            Class = className.ClassDescription,
                            ClassID = classID,
                            StudentName = $"{student?.FirstName} {student?.MiddleName} {student?.LastName}".Trim(),
                            ParentContact = parent?.GuardianPhone ?? parent?.PhoneNumber,
                            ParentEmailID = parent?.GaurdianEmail ?? parent?.FatherEmailID,
                            ParentLoginID = parent?.LoginID,
                        });

                        // Add to existing set to avoid duplicates
                        existingStudentIds.Add(studentId);
                    }
                }


                return feeDueStudentList;
            }

        }

    }
}