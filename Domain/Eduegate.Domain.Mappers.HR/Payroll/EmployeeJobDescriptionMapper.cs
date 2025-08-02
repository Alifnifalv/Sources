using Eduegate.Domain.Entity;
using Eduegate.Domain.Entity.HR.Payroll;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Extensions;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.HR.Payroll;
using Eduegate.Services.Contracts.Jobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Reporting.Map.WebForms.BingMaps;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;

namespace Eduegate.Domain.Mappers.HR.Payroll
{
    public class EmployeeJobDescriptionMapper : DTOEntityDynamicMapper

    {
        public static EmployeeJobDescriptionMapper Mapper(CallContext context)
        {
            var mapper = new EmployeeJobDescriptionMapper();
            mapper._context = context;
            return mapper;
        }
        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<EmployeeJobDescriptionDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }


        public override string GetEntity(long IID)
        {

            var toDto = new EmployeeJobDescriptionDTO();

            var empJDentity = new EmployeeJobDescription();
            var jdEntity = new JobDescription();

            using (var dbContext = new dbEduegateERPContext())
            {
                var applicantDetails = dbContext.JobApplications
                        .Include(x => x.Applicant)
                        .Include(x => x.Job)
                        .Where(x => x.ApplicationIID == IID)
                        .AsNoTracking().FirstOrDefault();

                toDto.ApplicantName = applicantDetails?.Applicant?.FirstName + " " + applicantDetails?.Applicant?.MiddleName + " " + applicantDetails?.Applicant?.LastName;
                toDto.JobApplicationID = applicantDetails.ApplicationIID;


                empJDentity = dbContext.EmployeeJobDescriptions
                               .Where(X => X.JobApplicationID == IID)
                               .OrderByDescending(x => x.JobDescriptionIID)
                               .Include(i => i.ReportingToEmployee)
                               .Include(i => i.EmployeeJobDescriptionDetails)
                               .AsNoTracking()
                               .FirstOrDefault();


                if (empJDentity == null)
                {
                    var JDMasterID = dbContext.AvailableJobs.FirstOrDefault(x => x.JobIID == applicantDetails.JobID).JDReferenceID;

                    jdEntity = dbContext.JobDescriptions.Where(X => X.JDMasterIID == JDMasterID)
                                  .Include(i => i.ReportingToEmployee)
                                  .Include(i => i.JobDescriptionDetails)
                                  .AsNoTracking()
                                  .FirstOrDefault();
                }


                if (empJDentity != null)
                {
                    toDto.JobDescriptionIID = empJDentity.JobDescriptionIID;
                    toDto.ReportingToEmployeeID = empJDentity.ReportingToEmployeeID;
                    toDto.ReportingToEmployee = new KeyValueDTO()
                    {
                        Value = empJDentity.ReportingToEmployee.EmployeeCode + " - " + empJDentity.ReportingToEmployee.FirstName + " " + empJDentity.ReportingToEmployee.MiddleName + " " + empJDentity.ReportingToEmployee.LastName,
                        Key = empJDentity.ReportingToEmployeeID.ToString()
                    };

                    toDto.JDDate = empJDentity.JDDate;
                    toDto.JDReference = empJDentity.JDReference;
                    toDto.RevDate = empJDentity.RevDate;
                    toDto.RevReference = empJDentity.RevReference;
                    toDto.IsActive = empJDentity.IsActive;
                    toDto.RoleSummary = empJDentity.RoleSummary;
                    toDto.Undertaking = empJDentity.Undertaking;
                    toDto.CreatedBy = (int?)empJDentity.CreatedBy;
                    toDto.UpdatedBy = (int?)empJDentity.UpdatedBy;
                    toDto.CreatedDate = empJDentity.CreatedDate;
                    toDto.UpdatedDate = empJDentity.UpdatedDate;
                    toDto.IsAgreementSigned = empJDentity.IsAgreementSigned == true ? true : false;
                    toDto.AgreementSignedDate = empJDentity.AgreementSignedDate;
                    toDto.EmpJobDescriptionDetail = new List<EmployeeJobDescriptionDTO.EmployeeJobDescriptionDetailDTO>();
                    foreach (var detail in empJDentity.EmployeeJobDescriptionDetails)
                    {
                        if (detail.Description != null)
                        {
                            toDto.EmpJobDescriptionDetail.Add(new EmployeeJobDescriptionDTO.EmployeeJobDescriptionDetailDTO()
                            {
                                JobDescriptionMapID = detail.JobDescriptionMapID,
                                JobDescriptionID = detail.JobDescriptionID,
                                Description = detail.Description,
                            });
                        }
                    }
                }
                else
                {
                    toDto.ReportingToEmployeeID = jdEntity.ReportingToEmployeeID;
                    toDto.ReportingToEmployee = new KeyValueDTO()
                    {
                        Value = jdEntity.ReportingToEmployee.EmployeeCode + " - " + jdEntity.ReportingToEmployee.FirstName + " " + jdEntity.ReportingToEmployee.MiddleName + " " + jdEntity.ReportingToEmployee.LastName,
                        Key = jdEntity.ReportingToEmployeeID.ToString()
                    };

                    toDto.JDDate = jdEntity.JDDate;
                    toDto.JDReference = jdEntity.JDReference;
                    toDto.RevDate = jdEntity.RevDate;
                    toDto.RevReference = jdEntity.RevReference;
                    toDto.IsAgreementSigned = false;
                    toDto.RoleSummary = jdEntity.RoleSummary;
                    toDto.Undertaking = jdEntity.Undertaking;
                    toDto.CreatedBy = (int?)jdEntity.CreatedBy;
                    toDto.UpdatedBy = (int?)jdEntity.UpdatedBy;
                    toDto.CreatedDate = jdEntity.CreatedDate;
                    toDto.UpdatedDate = jdEntity.UpdatedDate;
                    toDto.IsActive = true;

                    toDto.EmpJobDescriptionDetail = new List<EmployeeJobDescriptionDTO.EmployeeJobDescriptionDetailDTO>();
                    foreach (var detail in jdEntity.JobDescriptionDetails)
                    {
                        if (detail.Description != null)
                        {
                            toDto.EmpJobDescriptionDetail.Add(new EmployeeJobDescriptionDTO.EmployeeJobDescriptionDetailDTO()
                            {
                                JobDescriptionMapID = 0,
                                JobDescriptionID = 0,
                                Description = detail.Description,
                            });
                        }
                    }
                }


                return ToDTOString(toDto);
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as EmployeeJobDescriptionDTO;

            if (toDto.JobApplicationID == null || toDto.JobApplicationID == 0)
            {
                throw new Exception("Something went wrong. Please contact the administrator for assistance. !");
            }

            if (toDto.EmpJobDescriptionDetail.Count <= 0)
            {
                throw new Exception("Please enter the job description");
            }

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {


                if (toDto.IsAgreementSigned == true)
                {
                    throw new Exception("Sorry! The applicant has signed the agreement, so the screen can’t be edited.");
                }
                else
                {
                    var IIDs = toDto.EmpJobDescriptionDetail.Select(a => a.JobDescriptionMapID).ToList();

                    //delete maps
                    var entities = dbContext.EmployeeJobDescriptionDetails.Where(x =>
                        x.JobDescriptionID == toDto.JobDescriptionIID &&
                        !IIDs.Contains(x.JobDescriptionMapID)).AsNoTracking().ToList();

                    if (entities.IsNotNull())
                        dbContext.EmployeeJobDescriptionDetails.RemoveRange(entities);
                }


                var entity = new EmployeeJobDescription()
                {
                    JobDescriptionIID = toDto.JobDescriptionIID,
                    EmployeeID = toDto.EmployeeID,
                    JobApplicationID = toDto.JobApplicationID,
                    IsAgreementSigned = toDto.IsAgreementSigned,
                    AgreementSignedDate = toDto.AgreementSignedDate,
                    ReportingToEmployeeID = toDto.ReportingToEmployeeID,
                    JDReference = toDto.JDReference,
                    JDDate = toDto.JDDate,
                    RevReference = toDto.RevReference,
                    RevDate = toDto.RevDate,
                    RoleSummary = toDto.RoleSummary,
                    Undertaking = toDto.Undertaking,
                    IsActive = toDto.IsActive,
                    CreatedBy = toDto.JobDescriptionIID == 0 ? _context.LoginID : toDto.CreatedBy,
                    CreatedDate = toDto.JobDescriptionIID == 0 ? DateTime.Now : toDto.CreatedDate,
                    UpdatedDate = toDto.JobDescriptionIID != 0 ? DateTime.Now : toDto.UpdatedDate,
                    UpdatedBy = toDto.JobDescriptionIID != 0 ? _context.LoginID : toDto.UpdatedBy,
                };


                foreach (var detail in toDto.EmpJobDescriptionDetail)
                {
                    entity.EmployeeJobDescriptionDetails.Add(new EmployeeJobDescriptionDetail()
                    {
                        JobDescriptionMapID = detail.JobDescriptionMapID,
                        JobDescriptionID = entity.JobDescriptionIID,
                        Description = detail.Description,
                    });
                }

                dbContext.EmployeeJobDescriptions.Add(entity);

                if (entity.JobDescriptionIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    foreach (var detail in entity.EmployeeJobDescriptionDetails)
                    {
                        if (detail.JobDescriptionMapID != 0)
                        {
                            dbContext.Entry(detail).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                        else
                        {
                            dbContext.Entry(detail).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                    }

                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();

                return GetEntity((long)entity.JobApplicationID);
            }
        }

        #region Get JD List by LoginID
        public List<EmployeeJobDescriptionDTO> GetJDListByLoginID()
        {
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

            var jdList = new List<EmployeeJobDescriptionDTO>();
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var data = db.EmployeeJobDescriptions
                    .Include(x => x.EmployeeJobDescriptionDetails)
                    .Include(x => x.ReportingToEmployee)
                    .Include(x => x.JobApplication).ThenInclude(z => z.Applicant)
                    .Include(x => x.JobApplication).ThenInclude(z => z.Job)
                    .Where(x => x.IsActive == true && x.JobApplication.Applicant.LoginID == _context.LoginID)
                    .ToList();

                if (data.Count > 0)
                {
                    jdList = data.Select(x => new EmployeeJobDescriptionDTO()
                    {
                        JobDescriptionIID = x.JobDescriptionIID,
                        JobTitle = x.JobApplication?.Job?.JobTitle,
                        IsAgreementSigned = x.IsAgreementSigned,
                        ReportingToEmployee = new KeyValueDTO()
                        {
                            Value = $"{x.ReportingToEmployee?.FirstName} {x.ReportingToEmployee?.MiddleName} {x.ReportingToEmployee?.LastName}",
                            Key = x.ReportingToEmployeeID?.ToString()
                        },
                        SignedDateString = x.AgreementSignedDate.HasValue ?
                                          x.AgreementSignedDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                        EmpJobDescriptionDetail = x.EmployeeJobDescriptionDetails.Select(y =>
                            new EmployeeJobDescriptionDTO.EmployeeJobDescriptionDetailDTO()
                            {   
                                JobDescriptionID = y.JobDescriptionID,
                                JobDescriptionMapID = y.JobDescriptionMapID,
                                Description = y.Description  
                            }).ToList(),
                    }).ToList();
                }
            }

            return jdList;
        }

        #endregion

        //Mark as agreed by Applicant through Career portal
        public OperationResultDTO MarkJDasAgreed(long iid)
        {
            var result = new OperationResultDTO();

            using (var dbContext = new dbEduegateERPContext())
            {
                var data = dbContext.EmployeeJobDescriptions
                    .Where(a => a.JobDescriptionIID == iid)
                    .AsNoTracking()
                    .FirstOrDefault();

                try
                {
                    if (data != null)
                    {
                        data.IsAgreementSigned = true;
                        data.AgreementSignedDate = DateTime.Now;

                        dbContext.Entry(data).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        dbContext.SaveChanges();
                    }

                    result = new OperationResultDTO()
                    {
                        operationResult = Framework.Contracts.Common.Enums.OperationResult.Success,
                        Message = "Confirmation saved successfully."
                    };
                }
                catch (Exception ex)
                {
                    result = new OperationResultDTO()
                    {
                        operationResult = Framework.Contracts.Common.Enums.OperationResult.Error,
                        Message = ex.Message
                    };
                }
            }

            return result;
        }
    }
}