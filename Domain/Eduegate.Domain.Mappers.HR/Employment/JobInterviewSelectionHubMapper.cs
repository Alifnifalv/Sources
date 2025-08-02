using Eduegate.Domain.Entity;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.Jobs;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace Eduegate.Domain.Mappers.HR.Employment
{
    public class JobInterviewSelectionHubMapper : DTOEntityDynamicMapper
    {
        public static JobInterviewSelectionHubMapper Mapper(CallContext context)
        {
            var mapper = new JobInterviewSelectionHubMapper();
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
                            .Include(x => x.JobInterviewRoundMaps).ThenInclude(y => y.Round)
                            .FirstOrDefault(x => x.InterviewID == IID);

                var listEntity = dbContext.JobInterviewMarkMaps
                                .Where(a => a.InterviewID == IID)
                                .GroupBy(x => x.ApplicantID)
                                .ToList();

                var interviewRound = "";
                int serialNumber = 1; 

                foreach (var val in entity.JobInterviewRoundMaps)
                {
                    interviewRound = string.Concat(interviewRound, "<b>" + "Round " + serialNumber + ": " + "</b>" + val.Round.Description + "</br>");
                    serialNumber++;   
                }

                var dto = new JobInterviewDTO
                {
                    InterviewID = entity.InterviewID,
                    Interview = entity.Job?.JobDescription,
                    JobID = entity.JobID,
                    JobTitle = entity.Job?.JobTitle,
                    InterviewerID = entity.InterviewerID,
                    StartDate = entity.StartDate,
                    EndDate = entity.EndDate,
                    Interviewer = entity.InterviewerID != null
                        ? new KeyValueDTO
                        {
                            Key = entity.InterviewerID.ToString(),
                            Value = $"{entity.Interviewer.EmployeeCode} - {entity.Interviewer.FirstName} {entity.Interviewer.MiddleName} {entity.Interviewer.LastName}"
                        }
                        : new KeyValueDTO(),
                    Rounds = interviewRound,
                    ShortListDTO = listEntity.Select(group =>
                    {
                        var applicantId = group.Key; 
                        var applicant = dbContext.JobSeekers.FirstOrDefault(js => js.SeekerID == applicantId);
                        var isSelected = dbContext.JobInterviewMaps.FirstOrDefault(js => js.InterviewID == IID && js.ApplicantID == applicantId).IsSelected;
                        var ratingsGot = dbContext.JobInterviewMarkMaps
                                                .Where(m => m.InterviewID == IID && m.ApplicantID == applicantId).Sum(s => s.Rating) ?? 0;

                        return new JobInterviewMapDTO
                        {
                            InterviewID = entity.InterviewID,
                            ApplicantID = applicantId,
                            ApplicantName = applicant != null ?
                                $"{applicant.FirstName} {applicant.MiddleName} {applicant.LastName}" : null, 
                            TotalRating = entity.JobInterviewRoundMaps?.Sum(y => y.Round?.MaximumRating) ?? 0,
                            TotalRounds = entity.JobInterviewRoundMaps?.Count() ?? 0,
                            TotalRatingGot = ratingsGot,
                            TotalRatingEarned = ratingsGot + "/" + entity.JobInterviewRoundMaps.Sum(y => y.Round?.MaximumRating),
                            RoundsCompleted = dbContext.JobInterviewMarkMaps
                                .Where(m => m.InterviewID == IID && m.ApplicantID == applicantId).Count(),
                            IsSelected = isSelected,
                        };
                    }).OrderByDescending(d => d.TotalRatingGot).ToList(),
                };

                return ToDTOString(dto);
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as JobInterviewDTO;

            using (var dbContext = new dbEduegateERPContext())
            { 
                foreach (var shortlistItem in toDto.ShortListDTO)
                {
                    var existingEntity = dbContext.JobInterviewMaps
                        .FirstOrDefault(j => j.InterviewID == toDto.InterviewID && j.ApplicantID == shortlistItem.ApplicantID);

                    if (existingEntity != null)
                    {
                        existingEntity.IsSelected = shortlistItem.IsSelected;
                        existingEntity.IsSelectedByLoginID = shortlistItem.IsSelected == true ? _context.LoginID : null;
                    }
                }
 
                dbContext.SaveChanges();

                return GetEntity(toDto.InterviewID);
            }
        }

    }
}