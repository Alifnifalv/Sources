using Eduegate.Domain.Entity;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Mappers.Notification;
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

namespace Eduegate.Domain.Mappers.HR.Employment
{
    public class JobInterviewEvaluationMapper : DTOEntityDynamicMapper
    {
        public static JobInterviewEvaluationMapper Mapper(CallContext context)
        {
            var mapper = new JobInterviewEvaluationMapper();
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
                dto.Interview = entity.Job.JobDescription;
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

                dto.ShortListDTO = new List<JobInterviewMapDTO> ();

                var ratingEntity = dbContext.JobInterviewMarkMaps
                    .Where(m => m.InterviewID == IID)
                    .ToList();
                var ratingLookup = ratingEntity.ToDictionary(m => (m.ApplicantID, m.RoundID), m => new { m.Rating, m.HeldOnDate,m.Remarks });

                dto.ShortListDTO = entity.JobInterviewMaps.Select(x => new JobInterviewMapDTO
                {
                    InterviewID = entity.InterviewID,
                    ApplicantID = x.ApplicantID,
                    ApplicantName = $"{x.Applicant.FirstName} {x.Applicant.MiddleName} {x.Applicant.LastName}",
                    TotalRating = entity.JobInterviewRoundMaps?.Sum(y => y.Round?.MaximumRating) ?? 0,
                    TotalRatingGot = ratingEntity.Where(m => m.ApplicantID == x.ApplicantID).Sum(s => s.Rating),
                    RoundMapDTO = entity.JobInterviewRoundMaps.Select(y => new JobInterviewMapDTO.JobInterviewRoundMapDTO
                    {
                        RoundID = y.RoundID,
                        Round = y.Round.Description,
                        Rating = ratingLookup.TryGetValue((x.ApplicantID, y.RoundID), out var ratingInfo) ? ratingInfo.Rating : (int?)null,
                        Grade = null,
                        Remarks = ratingLookup.TryGetValue((x.ApplicantID, y.RoundID), out var remark) ? ratingInfo.Remarks : null,
                        MaximumRating = y.Round.MaximumRating,
                        HeldOnDate = ratingLookup.TryGetValue((x.ApplicantID, y.RoundID), out var heldOnDateInfo) ? heldOnDateInfo.HeldOnDate : (DateTime?)null,
                    }).ToList(),
                }).ToList();

                return ToDTOString(dto);
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as JobInterviewDTO;

            using (var dbContext = new dbEduegateERPContext())
            {
                if (toDto.ShortListDTO != null && toDto.ShortListDTO.Any(shortList =>
                        shortList.RoundMapDTO != null && shortList.RoundMapDTO.Any(x => x.Rating > x.MaximumRating)))
                {
                    throw new Exception("One or more ratings exceed their maximum allowed values.");
                }

                var oldData = dbContext.JobInterviewMarkMaps.Where(x => x.InterviewID == toDto.InterviewID).ToList();

                if (oldData.Any())
                {
                    dbContext.JobInterviewMarkMaps.RemoveRange(oldData);
                    dbContext.SaveChanges();
                }

                var toEntity = new List<JobInterviewMarkMap>();

                var mappedItems = toDto.ShortListDTO
                    .SelectMany(item => item.RoundMapDTO
                        .Where(tx => tx.Rating != null)
                        .Select(tx => new JobInterviewMarkMap
                        {
                            InterviewID = toDto.InterviewID,
                            ApplicantID = item.ApplicantID,
                            RoundID = tx.RoundID,
                            Rating = tx.Rating,
                            HeldOnDate = tx.HeldOnDate,
                            Remarks = tx.Remarks,
                            CreatedBy = _context.LoginID,
                            CreatedDate = DateTime.Now,
                            UpdatedBy = null,
                            UpdatedDate = null,
                        }));

                dbContext.JobInterviewMarkMaps.AddRange(mappedItems);
                dbContext.SaveChanges();

                return GetEntity(toDto.InterviewID);
            }
        }

    }
}