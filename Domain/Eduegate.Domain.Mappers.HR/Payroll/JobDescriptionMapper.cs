using Eduegate.Domain.Entity.HR;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Extensions;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.HR.Payroll;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Eduegate.Domain.Mappers.HR.Payroll
{
    public class JobDescriptionMapper : DTOEntityDynamicMapper

    {
        public static JobDescriptionMapper Mapper(CallContext context)
        {
            var mapper = new JobDescriptionMapper();
            mapper._context = context;
            return mapper;
        }
        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<JobDescriptionDTO>(entity);
        }
  
        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }


        public override string GetEntity(long IID)
        {
            using (var dbContext = new dbEduegateHRContext())
            {
                var entity = dbContext.JobDescriptions.Where(X => X.JDMasterIID == IID)
                   .Include(i => i.ReportingToEmployee)
                   .Include(i => i.JobDescriptionDetails)
                   .AsNoTracking()
                   .FirstOrDefault();

                var jd = new JobDescriptionDTO()
                {
                    JDMasterIID = entity.JDMasterIID,
                    Title = entity.Title,

                    ReportingToEmployeeID = entity.ReportingToEmployeeID,
                    ReportingToEmployee = new KeyValueDTO()
                    {
                        Value = entity.ReportingToEmployee.EmployeeCode + " - " + entity.ReportingToEmployee.FirstName + " " + entity.ReportingToEmployee.MiddleName + " " + entity.ReportingToEmployee.LastName,
                        Key = entity.ReportingToEmployeeID.ToString()
                    },

                    JDDate = entity.JDDate,
                    JDReference = entity.JDReference,
                    RevDate = entity.RevDate,
                    RevReference = entity.RevReference,
                    DepartmentID = entity.DepartmentID,
                    DesignationID = entity.DesignationID,

                    RoleSummary = entity.RoleSummary,
                    Undertaking = entity.Undertaking,
                    Responsibilities = entity.Responsibilities,

                    CreatedBy = (int?)entity.CreatedBy,
                    UpdatedBy = (int?)entity.UpdatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate

                };

                jd.JDDetail = new List<JobDescriptionDTO.JobDescriptionDetailDTO>();
                foreach (var detail in entity.JobDescriptionDetails)
                {
                    if (detail.Description != null)
                    {
                        jd.JDDetail.Add(new JobDescriptionDTO.JobDescriptionDetailDTO()
                        {
                            JDMapID = detail.JDMapID,
                            JDMasterID = detail.JDMasterID,
                            Description = detail.Description,
                        });
                    }
                }

                return ToDTOString(jd);
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as JobDescriptionDTO;

            using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
            {
                var entity = new JobDescription()
                {
                    JDMasterIID = toDto.JDMasterIID,
                    Title = toDto.Title,
                    ReportingToEmployeeID = toDto.ReportingToEmployeeID,
                    JDReference = toDto.JDReference,
                    JDDate = toDto.JDDate,
                    RevReference = toDto.RevReference,
                    RevDate = toDto.RevDate,
                    RoleSummary = toDto.RoleSummary,
                    Undertaking = toDto.Undertaking,
                    Responsibilities = toDto.Responsibilities,
                    DepartmentID = toDto.DepartmentID,
                    DesignationID = toDto.DesignationID,
                    CreatedBy = toDto.JDMasterIID == 0 ? _context.LoginID : toDto.CreatedBy,
                    UpdatedBy = toDto.JDMasterIID != 0 ? _context.LoginID : toDto.UpdatedBy,
                    CreatedDate = toDto.JDMasterIID == 0 ? DateTime.Now : toDto.CreatedDate,
                    UpdatedDate = toDto.JDMasterIID != 0 ? DateTime.Now : toDto.UpdatedDate,
                };

                var IIDs = toDto.JDDetail
                .Select(a => a.JDMapID).ToList();

                //delete maps
                var entities = dbContext.JobDescriptionDetails.Where(x =>
                    x.JDMasterID == entity.JDMasterIID &&
                    !IIDs.Contains(x.JDMapID)).AsNoTracking().ToList();

                if (entities.IsNotNull())
                    dbContext.JobDescriptionDetails.RemoveRange(entities);

                foreach (var detail in toDto.JDDetail)
                {
                    entity.JobDescriptionDetails.Add(new JobDescriptionDetail()
                    {
                        JDMapID = detail.JDMapID,
                        JDMasterID = entity.JDMasterIID,
                        Description = detail.Description,
                    });
                }

                dbContext.JobDescriptions.Add(entity);

                if (entity.JDMasterIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    foreach (var detail in entity.JobDescriptionDetails)
                    {
                        if (detail.JDMapID != 0)
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

                return GetEntity(entity.JDMasterIID);
            }
        }


        public JobDescriptionDTO GetJobDescriptionByJDMasterID(long JDMasterID) 
        {
            var result = new JobDescriptionDTO();

            using (var db = new dbEduegateHRContext())
            {
                var jd = db.JobDescriptions
                           .Include(x => x.JobDescriptionDetails)
                           .FirstOrDefault(x => x.JDMasterIID == JDMasterID);

                if (jd == null)
                    return result;

                result.DepartmentID = jd.DepartmentID;
                result.DesignationID = jd.DesignationID;

                result.JDDetail = new List<JobDescriptionDTO.JobDescriptionDetailDTO>();

                foreach (var jdDetail in jd.JobDescriptionDetails)
                {
                    result.JDDetail.Add(new JobDescriptionDTO.JobDescriptionDetailDTO
                    {
                        Description = jdDetail.Description,
                    });
                }
            }

            return result;
        }
    }
}