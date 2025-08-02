using Eduegate.Domain.Entity;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.Jobs;
using EntityGenerator.Core.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Eduegate.Domain.Mappers.HR.Employment
{
    public class JobApplicationManagementMapper : DTOEntityDynamicMapper
    {
        public static JobApplicationManagementMapper Mapper(CallContext context)
        {
            var mapper = new JobApplicationManagementMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<JobsDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var entity = dbContext.JobApplications
                    .Include(x => x.Job)
                    .Include(x => x.Applicant)
                    .Where(x => x.JobID == IID && x.IsShortListed == true).ToList();

                var dto = new JobsDTO();

                dto.JobIID = IID;
                dto.JobTitle = entity.FirstOrDefault().Job?.JobTitle;

                foreach (var data in entity)
                {
                    dto.JobApplicationDTO.Add(new JobApplicationDTO()
                    {
                        ApplicationIID = data.ApplicationIID,
                        JobID = data.JobID,
                        ApplicantID = data.ApplicantID,
                        ApplicantName = data.Applicant?.FirstName+" "+data.Applicant?.LastName,
                        AppliedDate = data.AppliedDate,
                        Education = data.Applicant?.Education,
                        TotalYearOfExperience = data.TotalYearOfExperience,
                        CVContentID = data.CVContentID,
                    });
                }

                return ToDTOString(dto);
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as JobsDTO;

            using (var dbContext = new dbEduegateERPContext())
            {
                foreach (var fromDTO in toDto.JobApplicationDTO)
                { 
                    var existingEntity = dbContext.JobApplications.Find(fromDTO.ApplicationIID);
                    if (existingEntity != null)
                    {
                        existingEntity.ShortListDate = DateTime.Now;
                        existingEntity.IsShortListed = true; 
                    }
                }
                dbContext.SaveChanges();
            }

            return GetEntity(toDto.JobIID);
        }


        public List<JobsDTO> GetAvailableJobList()
        {
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

            var jobList = new List<JobsDTO>();
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var statusIDsSetting = new Domain.Setting.SettingBL(null).GetSettingValue<string>("JOBLIST_VISIBLE_STATUS_IDS");

                var statusIDs = statusIDsSetting?.Split(',')
                                     .Select(id => byte.TryParse(id.Trim(), out var result) ? result : (byte?)null)
                                     .Where(id => id.HasValue)
                                     .Select(id => id.Value)
                                     .ToList() ?? new List<byte>();

                var data = db.AvailableJobs
                    .Where(x => statusIDs.Contains((byte)x.StatusID))
                    .Include(x => x.School)
                    .Include(x => x.StatusNavigation)
                    .Include(x => x.JobType)
                    .Include(x => x.Department)
                    .Include(x => x.AvailableJobCriteriaMaps).ThenInclude(y => y.Qualification)
                    .Include(x => x.AvailableJobSkillMaps)
                    .Where(x => (!x.ClosingDate.HasValue || x.ClosingDate.Value.Date >= DateTime.Now.Date) &&
                            (!x.PublishDate.HasValue || x.PublishDate.Value.Date <= DateTime.Now.Date))
                    .ToList();

                if (data.Count > 0)
                {
                    jobList = data.Select(job => new JobsDTO()
                    {
                        JobIID = job.JobIID,
                        JobTitle = job.JobTitle,
                        TypeOfJob = job.JobType?.JobTypeName,
                        JobDescription = job.JobDescription,
                        JobDetails = job.JobDetails,
                        Status = job.StatusNavigation?.Description,
                        School = job.School.SchoolName,
                        TotalYearOfExperience = job.TotalYearOfExperience,
                        DepartmentID = job.DepartmentId,
                        DesignationID = job.DesignationID,
                        PostedDate = job.PublishDate.HasValue ? job.PublishDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                        CloseDate = job.ClosingDate.HasValue ? job.ClosingDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                        JobTitleInitial = GetInitials(job.JobTitle),
                        SkillList = job.AvailableJobSkillMaps.Select(x => new Framework.Contracts.Common.KeyValueDTO() { Key = x.SkillID.ToString(), Value = x.Skill.ToString() }).ToList(),
                        MonthlySalary = job.MonthlySalary,
                        AvailableJobCriteriaMapDTO = job.AvailableJobCriteriaMaps.Select(x => new AvailableJobCriteriaMapDTO()
                        {
                            JobID = x.JobID,
                            TypeID = x.TypeID,
                            QualificationID = x.QualificationID,
                            Qualification = new Framework.Contracts.Common.KeyValueDTO() { Key = x.QualificationID.ToString(), Value = x.Qualification?.QualificationName.ToString() },
                            FieldOfStudy = x.FieldOfStudy,
                        }).ToList(),
                        IsJobAlreadyApplied = db.JobApplications.Any(c => c.JobID == job.JobIID && c.Applicant.LoginID == _context.LoginID) ? (bool?)true : (bool?)false,

                    }).OrderByDescending(x => x.JobIID).ToList();
                }
            }

            return jobList;
        }

        public List<JobApplicationDTO> GetAppliedJobList()
        {
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

            var appliedList = new List<JobApplicationDTO>();
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {

                var data = db.JobApplications
                    .Include(x => x.Job).ThenInclude(x => x.JobType)
                    .Include(x => x.Job).ThenInclude(x => x.StatusNavigation)
                    .Include(x => x.Job).ThenInclude(x => x.School)
                    .Where(x => x.ApplicantID == _context.LoginID).ToList();

                if (data.Count > 0)
                {
                    appliedList = data.Select(job => new JobApplicationDTO()
                    {
                        JobID = (long)job.JobID,
                        JobTitle = job.Job?.JobTitle,
                        TypeOfJob = job.Job?.JobType?.JobTypeName,
                        JobDescription = job.Job?.JobDescription,
                        JobDetails = job.Job?.JobDetails,
                        Status = job.Job?.StatusNavigation?.Description,
                        School = job.Job?.School.SchoolName,
                        IsShortListed = job.IsShortListed.HasValue ? job.IsShortListed : false,
                        AppliedDateString = job.AppliedDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture),
                        ShortListDateString = job.ShortListDate.HasValue ? job.ShortListDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                    }).ToList();
                }
            }

            return appliedList;
        }

        public static string GetInitials(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                return string.Empty;

            var words = description.Split(' ');
            var initials = new StringBuilder();

            foreach (var word in words)
            {
                if (!string.IsNullOrEmpty(word))
                {
                    initials.Append(char.ToUpper(word[0]));
                }
            }

            return initials.ToString().PadRight(2, ' ').Substring(0, 2);
        }

        //ERP
        public List<JobApplicationDTO> GetApplicantsForShortList(long ID, bool? isShortListed)
        {
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

            var applicantList = new List<JobApplicationDTO>();
            List<long?> applicantIDsToExclude = new List<long?>();

            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var jobApplicationsForJob = db.JobApplications
                    .Include(x => x.Job)
                    .Include(x => x.Applicant).ThenInclude(x => x.Qualification)
                    .Include(x => x.Applicant).ThenInclude(x => x.JobInterviewMaps)
                    .Where(x => x.JobID == ID).ToList();

                if (jobApplicationsForJob.Any())
                {
                    var mapIDsFromJobApplicants = jobApplicationsForJob
                        .Where(app => app.Applicant?.JobInterviewMaps != null)
                        .SelectMany(app => app.Applicant.JobInterviewMaps.Select(map => map.MapID))
                        .Distinct().ToList();

                    if (mapIDsFromJobApplicants.Any())
                    {
                        var mapIDsLinkedToActiveEmployees = db.Employees
                            .AsNoTracking()
                            .Where(e => e.IsActive == true && mapIDsFromJobApplicants.Contains((long)e.JobInterviewMapID))
                            .Select(e => e.JobInterviewMapID).Distinct().ToList();

                        if (mapIDsLinkedToActiveEmployees.Any())
                        {
                            applicantIDsToExclude = db.JobInterviewMaps
                                .AsNoTracking()
                                .Where(jim => mapIDsLinkedToActiveEmployees.Contains(jim.MapID))
                                .Select(jim => jim.ApplicantID).Distinct().ToList();
                        }
                    }
                }

                if (jobApplicationsForJob.Any())
                {
                    var query = jobApplicationsForJob.AsQueryable();
                    query = query.Where(x => isShortListed == true ? x.IsShortListed == true : (x.IsShortListed == null || true || false));

                    if (applicantIDsToExclude.Any())
                    {
                        query = query.Where(x => !applicantIDsToExclude.Contains(x.ApplicantID));
                    }

                    applicantList = query.Select(job => new JobApplicationDTO()
                    {
                        ApplicationIID = job.ApplicationIID,
                        ApplicantID = job.ApplicantID,
                        IsShortListed = job.IsShortListed.HasValue ? job.IsShortListed : false,
                        JobID = job.JobID,
                        AppliedDateString = job.AppliedDate.HasValue ? job.AppliedDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                        ApplicantName = job.Applicant != null ? job.Applicant.FirstName + " " + job.Applicant.LastName : null,
                        Education = job.Applicant != null ? job.Applicant.Qualification != null ? job.Applicant.Qualification.QualificationName : null : null,
                        TotalYearOfExperience = job.TotalYearOfExperience,
                        CVContentID = job.CVContentID,
                        JobTitle = job.Job != null ? job.Job.JobTitle : null,
                        ApplicantMailID = job.Applicant != null ? job.Applicant.EmailID : null,
                        Remarks = job.IsShortListed == true ? "Shortlisted on " + job.ShortListDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : "",
                    }).ToList();
                }
            }

            return applicantList;
        }
    }
}