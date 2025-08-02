using Eduegate.Domain.Entity;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Mappers.Notification;
using Eduegate.Domain.Mappers.Notification.Notifications;
using Eduegate.Domain.Repository;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Enums.Notifications;
using Eduegate.Services.Contracts.Jobs;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UglyToad.PdfPig.Graphics.Operations.PathConstruction;

namespace Eduegate.Domain.Mappers.HR.Employment
{
    public class JobInterviewManagementMapper : DTOEntityDynamicMapper
    {
        public static JobInterviewManagementMapper Mapper(CallContext context)
        {
            var mapper = new JobInterviewManagementMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<JobInterviewDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var entity = dbContext.JobInterviews
                    .Include(x => x.Interviewer)
                    .Include(x => x.Job)
                    .Include(x => x.JobInterviewMaps).ThenInclude(z => z.Applicant).ThenInclude(e => e.Qualification)
                    .Include(x => x.JobInterviewRoundMaps).ThenInclude(y => y.Round)
                    .FirstOrDefault(x => x.InterviewID == IID);

                var dto = new JobInterviewDTO();

                dto.InterviewID = entity.InterviewID;
                dto.JobID = entity.JobID;
                dto.JobTitle = entity.Job.JobTitle;
                dto.InterviewerID = entity.InterviewerID;
                dto.Duration = entity.Duration;
                dto.StartDate = entity.StartDate;
                dto.EndDate = entity.EndDate;
                dto.CreatedDate = entity.CreatedDate;
                dto.UpdatedDate = entity.UpdatedDate;
                dto.CreatedBy = (int?)entity.CreatedBy;
                dto.UpdatedBy = (int?)entity.UpdatedBy;
                dto.MeetingLink = entity.MeetingLink;
                dto.Remarks = entity.Remarks;

                dto.Interviewer = new KeyValueDTO();
                dto.Interviewer = entity.InterviewerID != null ? new KeyValueDTO() { Key = entity.InterviewerID.ToString(), Value = entity.Interviewer.EmployeeCode + " - " + entity.Interviewer.FirstName + " " + entity.Interviewer.MiddleName + " " + entity.Interviewer.LastName } : new KeyValueDTO();

                dto.InterviewRounds = new List<KeyValueDTO> ();
                dto.InterviewRounds = entity.JobInterviewRoundMaps.Select(x => new KeyValueDTO()
                {
                    Key = x.RoundID.ToString(),
                    Value = x.Round.Description,

                }).ToList();

                dto.ShortListDTO = new List<JobInterviewMapDTO> ();
                dto.ShortListDTO = entity.JobInterviewMaps.Select(x => new JobInterviewMapDTO()
                {
                    InterviewID = entity.InterviewID,
                    StartTime = x.StartTime,
                    EndTime = x.EndTime,
                    Education = x.Applicant?.Qualification?.QualificationName,
                    ApplicantName = x.Applicant.FirstName+' '+x.Applicant.MiddleName+' '+x.Applicant.LastName,
                }).ToList();

                return ToDTOString(dto);
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as JobInterviewDTO;

            if(toDto.InterviewID != 0)
            {
                throw new Exception("Sorry, this screen is not editable !");
            }

            using (var dbContext = new dbEduegateERPContext())
            {
                var entity = new JobInterview()
                {
                    InterviewID = toDto.InterviewID,
                    JobID = toDto.JobID,
                    StartDate = toDto.StartDate,
                    EndDate = toDto.EndDate,
                    InterviewerID = toDto.InterviewerID,
                    MeetingLink = toDto.MeetingLink,
                    Duration = toDto.Duration,
                    Remarks = toDto.Remarks,

                    CreatedBy = toDto.InterviewID == 0 ? _context.LoginID : toDto.CreatedBy,
                    UpdatedBy = toDto.InterviewID != 0 ? _context.LoginID : toDto.UpdatedBy,
                    CreatedDate = toDto.InterviewID == 0 ? DateTime.Now : toDto.CreatedDate,
                    UpdatedDate = toDto.InterviewID != 0 ? DateTime.Now : toDto.UpdatedDate,
                };

                dbContext.Add(entity);

                entity.JobInterviewMaps = toDto.ShortListDTO.Select(x => new JobInterviewMap()
                {
                    InterviewID = x.InterviewID,
                    StartTime = x.StartTime,
                    EndTime = x.EndTime,
                    ApplicantID = x.ApplicantID,

                }).Where(x => x.ApplicantID != 0).ToList();

                entity.JobInterviewRoundMaps = toDto.InterviewRounds.Select(x => new JobInterviewRoundMap()
                {
                    InterviewID = entity.InterviewID,
                    RoundID = int.Parse(x.Key)

                }).ToList();

                dbContext.SaveChanges();

                if(toDto.InterviewID == 0 && entity.InterviewID != 0)
                {
                    toDto.InterviewID = entity.InterviewID;
                    SendInterviewMail(toDto);
                    SaveNotificationAlerts(toDto);
                }

                return GetEntity(entity.InterviewID);
            }
        }

        //Response from applicant from mail
        public string JobInterviewApplicantResponse(bool? response, long? interviewID, long? applicantID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var data = db.JobInterviewMaps.FirstOrDefault(x => x.InterviewID == interviewID && x.ApplicantID == applicantID);

                if (data != null)
                {
                    data.InterviewAccepted = response;
                    db.SaveChanges();
                }
            }

            return null;
        }

        #region  SendMail to applicants when interview scheduled
        public string SendInterviewMail(JobInterviewDTO dto)
        {
            string classCode = string.Empty;

            var mailParameters = new Dictionary<string, string>()
            {
                { "CLASS_CODE", classCode},
            };

            var hostDet = new Domain.Setting.SettingBL(null).GetSettingValue<string>("HOST_NAME");
            var defaultMail = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DEFAULT_MAIL_ID");
            var rooturl = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CareerRootUrl");

            var emailTemplate = EmailTemplateMapper.Mapper(_context).GetEmailTemplateDetails(EmailTemplates.JobInterviewMail.ToString());

            var emailSubject = string.Empty;
            var emailBody = string.Empty;

            foreach (var toApplicant in dto.ShortListDTO)
            {
                if (emailTemplate != null && !string.IsNullOrEmpty(emailTemplate.EmailTemplate))
                {
                    emailSubject = emailTemplate.Subject;

                    emailBody = emailTemplate.EmailTemplate;

                    emailBody = emailBody.Replace("{ApplicantName}", toApplicant.ApplicantName);
                    emailBody = emailBody.Replace("{JobTitle}", dto.JobTitle);
                    emailBody = emailBody.Replace("{MeetingLink}", dto.MeetingLink);
                    emailSubject = emailSubject.Replace("{JobTitle}", dto.JobTitle);
                    emailBody = emailBody.Replace("{ResponseLink}", rooturl+ "Account/JobInterviewApplicantResponse");
                    emailBody = emailBody.Replace("{Interviewer}", dto.Interviewer.Value.ToString());
                    emailBody = emailBody.Replace("{InterviewID}", dto.InterviewID.ToString());
                    emailBody = emailBody.Replace("{ApplicantID}", toApplicant.ApplicantID.ToString());
                }

                string mailMessage = new Eduegate.Domain.Notification.EmailNotificationBL(_context).PopulateBody(toApplicant.ApplicantMailID, emailBody);

                if (emailBody != "")
                {
                    if (hostDet.ToLower() == "live")
                    {
                        new Eduegate.Domain.Notification.EmailNotificationBL(_context).SendMail(toApplicant.ApplicantMailID, emailSubject, mailMessage, EmailTypes.RecruitmentPortal, mailParameters);
                    }
                    else
                    {
                        new Eduegate.Domain.Notification.EmailNotificationBL(_context).SendMail(defaultMail, emailSubject, mailMessage, EmailTypes.RecruitmentPortal, mailParameters);
                    }
                }
            }
            return null;
        }

        #endregion

        #region Save Notification Alert when Interview scheduled
        public string SaveNotificationAlerts (JobInterviewDTO dto)
        { 
            var alertstatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DEFAULTALERTSTATUSID");

            var notification = new List<Eduegate.Domain.Entity.Models.CareerNotificationAlert>();
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                notification = dto.ShortListDTO.Select(toEntity => new Entity.Models.CareerNotificationAlert()
                {
                    NotificationAlertIID = 0,
                    Message = "You’ve been shortlisted, and an interview has been scheduled for the " + dto.JobTitle + " position! For more details, please check your email. Good luck!",
                    FromLoginID = _context.LoginID,
                    ToLoginID = db.JobSeekers.Include(y => y.Login).FirstOrDefault(y => y.SeekerID == toEntity.ApplicantID).Login.LoginID,
                    NotificationDate = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    AlertStatusID = int.Parse(alertstatus),
                }).ToList();

                db.CareerNotificationAlerts.AddRange(notification);
                db.SaveChanges();
            }

            return null;
        }
        #endregion

        #region Get Interview List by LoginID
        public List<JobInterviewMapDTO> GetMyInterviewList()
        {
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

            var interviewList = new List<JobInterviewMapDTO>();
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var data = db.JobInterviewMaps
                    .Include(x => x.Interview).ThenInclude(y => y.Job).ThenInclude(z => z.Department)
                    .Include(x => x.Interview).ThenInclude(y => y.Interviewer)
                    .Include(x => x.Interview).ThenInclude(o => o.JobInterviewRoundMaps).ThenInclude(p => p.Round)
                    .Include(y => y.Applicant)
                    .Where(x => x.Applicant.LoginID == _context.LoginID).ToList();

                if (data.Count > 0)
                {
                    interviewList = data.Select(xy => new JobInterviewMapDTO()
                    {
                        InterviewID = (long)xy.InterviewID,
                        JobTitle = xy.Interview.Job.JobTitle,
                        Department = xy.Interview.Job?.Department?.DepartmentName,
                        InterviewDateString = xy.Interview.StartDate.HasValue ? xy.Interview.StartDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                        StartTimeString = xy.StartTime.HasValue ? DateTime.Today.Add(xy.StartTime.Value).ToString("hh:mm tt") : null,
                        EndTimeString = xy.EndTime.HasValue ? DateTime.Today.Add(xy.EndTime.Value).ToString("hh:mm tt") : null,
                        Interviewer = xy.Interview.Interviewer?.FirstName+" "+ xy.Interview.Interviewer?.MiddleName + " " + xy.Interview.Interviewer?.LastName,
                        CompletedRoundID = xy.CompletedRoundID,
                        NextRound = xy.Interview?.JobInterviewRoundMaps.FirstOrDefault(x => x.RoundID > (db.JobInterviewMarkMaps.Where(x => x.InterviewID == xy.InterviewID)?.ToList().LastOrDefault()?.RoundID))?.Round?.Description,
                    }).ToList();
                }
            }

            return interviewList;
        }

        #endregion
    }
}