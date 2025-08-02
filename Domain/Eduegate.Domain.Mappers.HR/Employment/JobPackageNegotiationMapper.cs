using Eduegate.Domain.Entity;
using Eduegate.Domain.Entity.HR;
using Eduegate.Domain.Entity.HR.Payroll;
using Eduegate.Domain.Mappers.Notification;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Extensions;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.Enums.Notifications;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.HR.Payroll;
using Eduegate.Services.Contracts.Jobs;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic;

namespace Eduegate.Domain.Mappers.HR.Employment
{
    public class JobPackageNegotiationMapper : DTOEntityDynamicMapper
    {
        public static JobPackageNegotiationMapper Mapper(CallContext context)
        {
            var mapper = new JobPackageNegotiationMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<EmployeeSalaryStructureDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            var dto = new EmployeeSalaryStructureDTO();

            using (dbEduegateHRContext dbHRContext = new dbEduegateHRContext())
            {
                var structEntity = dbHRContext.EmployeeSalaryStructures
                                .Include(x => x.Employee)
                                .Include(x => x.SalaryStructure)
                                //.Include(x => x.SalaryPaymentMode)
                                //.Include(x => x.PayrollFrequency)
                                .Include(x => x.SalaryComponent)
                                //.Include(x => x.Account)
                                .Include(x => x.EmployeeSalaryStructureComponentMaps).ThenInclude(x => x.SalaryComponent)
                                .Include(x => x.EmployeeSalaryStructureComponentMaps).ThenInclude(x => x.EmployeeSalaryStructureVariableMaps)
                                .Include(x => x.EmployeeSalaryStructureLeaveSalaryMaps).ThenInclude(x => x.SalaryComponent)
                                .Include(x => x.LeaveSalaryStructure)
                                .FirstOrDefault(a => a.JobInterviewMapID == IID);

                if (structEntity != null)
                {
                    dto = new EmployeeSalaryStructureDTO()
                    {
                        EmployeeSalaryStructureIID = structEntity.EmployeeSalaryStructureIID,
                        SalaryStructureID = structEntity.SalaryStructureID,
                        EmployeeSalaryStructure = new KeyValueDTO()
                        {
                            Key = structEntity.SalaryStructureID.ToString(),
                            Value = structEntity.SalaryStructure.StructureName
                        },
                        FromDate = structEntity.FromDate,
                        IsActive = structEntity.IsActive,
                        Amount = structEntity.Amount,
                        //AccountID = structEntity.AccountID,
                        //PayrollFrequencyID = structEntity.PayrollFrequencyID,
                        IsSalaryBasedOnTimeSheet = structEntity.IsSalaryBasedOnTimeSheet,
                        //PaymentModeID = structEntity.PaymentModeID,
                        TimeSheetSalaryComponentID = structEntity.TimeSheetSalaryComponentID,
                        TimeSheetHourRate = structEntity.TimeSheetHourRate,
                        TimeSheetLeaveEncashmentPerDay = structEntity.TimeSheetLeaveEncashmentPerDay,
                        TimeSheetMaximumBenefits = structEntity.TimeSheetMaximumBenefits,
                        CreatedBy = structEntity.CreatedBy,
                        CreatedDate = structEntity.CreatedDate,
                        //PaymentMode = new KeyValueDTO()
                        //{
                        //    Key = structEntity.PaymentModeID.ToString(),
                        //    Value = structEntity.SalaryPaymentMode.PaymentName
                        //},
                        //PayrollFrequency = new KeyValueDTO()
                        //{

                        //    Key = structEntity.PayrollFrequencyID.ToString(),
                        //    Value = structEntity.PayrollFrequency.FrequencyName
                        //},
                        TimeSheetSalaryComponent = structEntity.TimeSheetSalaryComponentID.HasValue ? new KeyValueDTO()
                        {
                            Key = structEntity.TimeSheetSalaryComponentID.HasValue ? Convert.ToString(structEntity.TimeSheetSalaryComponentID) : null,
                            Value = structEntity.SalaryComponent.Description
                        } : new KeyValueDTO(),

                        //Account = new KeyValueDTO()
                        //{
                        //    Key = structEntity.AccountID.ToString(),
                        //    Value = structEntity.Account.AccountName
                        //},
                        LeaveSalaryStructureID = structEntity.LeaveSalaryStructureID,
                        EmployeeLeaveSalaryStructure = structEntity.LeaveSalaryStructureID.HasValue ? new KeyValueDTO()
                        {
                            Key = structEntity.LeaveSalaryStructureID.ToString(),
                            Value = structEntity.LeaveSalaryStructure.StructureName
                        } : new KeyValueDTO(),
                    };

                    dto.SalaryComponents = new List<EmployeeSalaryStructureComponentMapDTO>();

                    foreach (var salaryComponent in structEntity.EmployeeSalaryStructureComponentMaps)
                    {
                        var listcomponentVariableMapDTO = new List<EmployeeSalaryStructureVariableDTO>();
                        foreach (var variablemap in salaryComponent.EmployeeSalaryStructureVariableMaps)
                        {
                            var componentVariableMapDTO = new EmployeeSalaryStructureVariableDTO()
                            {
                                EmployeeSalaryStructureVariableMapIID = variablemap.EmployeeSalaryStructureVariableMapIID,
                                EmployeeSalaryStructureComponentMapID = salaryComponent.EmployeeSalaryStructureComponentMapIID,
                                VariableValue = variablemap.VariableValue,
                                VariableKey = variablemap.VariableKey
                            };
                            listcomponentVariableMapDTO.Add(componentVariableMapDTO);
                        }

                        dto.SalaryComponents.Add(new EmployeeSalaryStructureComponentMapDTO()
                        {
                            EmployeeSalaryStructureComponentMapIID = salaryComponent.EmployeeSalaryStructureComponentMapIID,
                            Amount = salaryComponent.Amount,
                            EmployeeSalaryStructureID = salaryComponent.EmployeeSalaryStructureID,
                            SalaryComponentID = salaryComponent.SalaryComponentID,
                            Formula = salaryComponent.Formula,
                            SalaryComponent = new KeyValueDTO()
                            {
                                Key = salaryComponent.SalaryComponentID.ToString(),
                                Value = salaryComponent.SalaryComponent.Description
                            },
                            EmployeeSalaryStructureVariableMap = listcomponentVariableMapDTO
                        });
                    }

                    foreach (var leaveSalaryComponent in structEntity.EmployeeSalaryStructureLeaveSalaryMaps)
                    {
                        dto.LeaveSalaryComponents.Add(new EmployeeSalaryStructureComponentMapDTO()
                        {
                            EmployeeSalaryStructureComponentMapIID = leaveSalaryComponent.EmployeeSalaryStructureLeaveSalaryMapIID,
                            Amount = leaveSalaryComponent.Amount,
                            EmployeeSalaryStructureID = leaveSalaryComponent.EmployeeSalaryStructureID,
                            SalaryComponentID = leaveSalaryComponent.SalaryComponentID,
                            Formula = leaveSalaryComponent.Formula,
                            SalaryComponent = new KeyValueDTO()
                            {
                                Key = leaveSalaryComponent.SalaryComponentID.ToString(),
                                Value = leaveSalaryComponent.SalaryComponent.Description
                            }
                        });
                    }
                }
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    var entity = dbContext.JobInterviewMaps
                        .Include(x => x.Applicant)
                        .Include(x => x.Interview).ThenInclude(o => o.Job).ThenInclude(p => p.Designation)
                        .Include(x => x.Interview).ThenInclude(o => o.Job).ThenInclude(p => p.Department)
                        .Include(x => x.Interview).ThenInclude(o => o.Job).ThenInclude(p => p.School)
                        .FirstOrDefault(x => x.MapID == IID);

                    dto.JobInterviewMapID = IID;
                    dto.ApplicantName = entity.Applicant?.FirstName + " " + entity.Applicant?.MiddleName + " " + entity.Applicant?.LastName;
                    dto.ApplicantID = entity.ApplicantID;
                    dto.InterviewID = entity.InterviewID;
                    dto.Designation = entity.Interview?.Job?.Designation?.DesignationName;
                    dto.Department = entity.Interview?.Job?.Department?.DepartmentName;
                    dto.CandidateMailID = entity.Applicant?.EmailID;
                    dto.CompanyName = entity.Interview?.Job?.School?.SchoolName;
                    dto.SchoolID = entity.Interview?.Job?.SchoolID;
                    dto.IsOfferLetterSent = entity.IsOfferLetterSent;
                }
            }
            return ToDTOString(dto);

        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as EmployeeSalaryStructureDTO;

            if (toDto.SalaryStructureID == null || toDto.SalaryStructureID == 0)
            {
                throw new Exception("Please Select a Salary Structure Name!");
            }
            if (toDto.SalaryComponents.Count == 0)
            {
                throw new Exception("Please Select any Salary Component!");
            }

            using (var dbContext = new dbEduegateHRContext())
            {
                var salStruct = dbContext.SalaryStructureScaleMaps.Where(x => (x.IsSponsored.HasValue ? x.IsSponsored : false ) == toDto.IsSponsored && x.SalaryStructureID == toDto.SalaryStructureID).ToList();

                if (salStruct.Count > 0)
                {
                    decimal? totalMinAmount = salStruct.Sum(x => x.MinAmount);
                    decimal? totalMaxAmount = salStruct.Sum(x => x.MaxAmount);

                    if (toDto.Amount < totalMinAmount || toDto.Amount > totalMaxAmount)
                    {
                        throw new Exception($"Amount must be between {totalMinAmount} and {totalMaxAmount}. Provided amount: {toDto.Amount}");
                    }
                }

                EmployeeSalaryStructure employeeMap = null;

                var entity = new EmployeeSalaryStructure()
                {
                    EmployeeSalaryStructureIID = toDto.EmployeeSalaryStructureIID,
                    //EmployeeID = int.Parse(keyval.Key),
                    SalaryStructureID = toDto.SalaryStructureID,
                    FromDate = toDto.FromDate,
                    Amount = toDto.Amount,
                    //AccountID = toDto.AccountID,
                    IsSalaryBasedOnTimeSheet = toDto.IsSalaryBasedOnTimeSheet,
                    //PaymentModeID = toDto.PaymentModeID,
                    //PayrollFrequencyID = toDto.PayrollFrequencyID,
                    TimeSheetSalaryComponentID = toDto.TimeSheetSalaryComponentID,
                    TimeSheetHourRate = toDto.TimeSheetHourRate,
                    TimeSheetLeaveEncashmentPerDay = toDto.TimeSheetLeaveEncashmentPerDay,
                    TimeSheetMaximumBenefits = toDto.TimeSheetMaximumBenefits,
                    LeaveSalaryStructureID = toDto.LeaveSalaryStructureID,
                    JobInterviewMapID = toDto.JobInterviewMapID,
                };
                if (toDto.EmployeeSalaryStructureIID == 0)
                {
                    entity.IsActive = toDto.IsActive;
                    entity.CreatedBy = Convert.ToInt32(_context.LoginID);
                    entity.CreatedDate = DateTime.Now;
                }
                else
                {
                    entity.IsActive = toDto.IsActive;
                    entity.CreatedBy = toDto.CreatedBy;
                    entity.CreatedDate = toDto.CreatedDate;
                    entity.UpdatedBy = Convert.ToInt32(_context.LoginID);
                    entity.UpdatedDate = DateTime.Now;
                }
                employeeMap = entity;

                var IIDs = toDto.SalaryComponents
                .Select(a => a.EmployeeSalaryStructureComponentMapIID).ToList();
                //delete maps
                var entities = dbContext.EmployeeSalaryStructureComponentMaps.Where(x =>
                    x.EmployeeSalaryStructureID == entity.EmployeeSalaryStructureIID &&
                    !IIDs.Contains(x.EmployeeSalaryStructureComponentMapIID)).AsNoTracking().ToList();

                if (entities.IsNotNull())
                    dbContext.EmployeeSalaryStructureComponentMaps.RemoveRange(entities);

                entity.EmployeeSalaryStructureComponentMaps = new List<EmployeeSalaryStructureComponentMap>();

                foreach (var salaryComp in toDto.SalaryComponents)
                {
                    var listcomponentVariableMap = new List<EmployeeSalaryStructureVariableMap>();
                    foreach (var variablemap in salaryComp.EmployeeSalaryStructureVariableMap)
                    {
                        var componentVariableMap = new EmployeeSalaryStructureVariableMap()
                        {
                            EmployeeSalaryStructureVariableMapIID = variablemap.EmployeeSalaryStructureVariableMapIID,
                            EmployeeSalaryStructureComponentMapID = salaryComp.EmployeeSalaryStructureComponentMapIID,
                            VariableValue = variablemap.VariableValue,
                            VariableKey = variablemap.VariableKey
                        };
                        listcomponentVariableMap.Add(componentVariableMap);
                    }


                    entity.EmployeeSalaryStructureComponentMaps.Add(new EmployeeSalaryStructureComponentMap()
                    {
                        EmployeeSalaryStructureComponentMapIID = salaryComp.EmployeeSalaryStructureComponentMapIID,
                        EmployeeSalaryStructureID = salaryComp.EmployeeSalaryStructureID,
                        SalaryComponentID = salaryComp.SalaryComponentID,
                        Amount = salaryComp.Amount,
                        EmployeeSalaryStructureVariableMaps = listcomponentVariableMap
                    });
                }

                var leaveIIDs = toDto.LeaveSalaryComponents
                .Select(a => a.EmployeeSalaryStructureComponentMapIID).ToList();
                //delete maps
                var leaveEntities = dbContext.EmployeeSalaryStructureLeaveSalaryMaps.Where(x =>
                    x.EmployeeSalaryStructureID == entity.EmployeeSalaryStructureIID &&
                    !leaveIIDs.Contains(x.EmployeeSalaryStructureLeaveSalaryMapIID)).AsNoTracking().ToList();

                if (leaveEntities.IsNotNull())
                    dbContext.EmployeeSalaryStructureLeaveSalaryMaps.RemoveRange(leaveEntities);

                entity.EmployeeSalaryStructureLeaveSalaryMaps = new List<EmployeeSalaryStructureLeaveSalaryMap>();

                foreach (var salaryComp in toDto.LeaveSalaryComponents)
                {
                    entity.EmployeeSalaryStructureLeaveSalaryMaps.Add(new EmployeeSalaryStructureLeaveSalaryMap()
                    {
                        EmployeeSalaryStructureLeaveSalaryMapIID = salaryComp.EmployeeSalaryStructureComponentMapIID,
                        EmployeeSalaryStructureID = salaryComp.EmployeeSalaryStructureID,
                        SalaryComponentID = salaryComp.SalaryComponentID,
                        Amount = salaryComp.Amount,
                    });
                }

                if (toDto.IsActive == true)
                {
                    var existingSalaryStructure = dbContext.EmployeeSalaryStructures.Where(s => s.JobInterviewMapID == toDto.ApplicantID && s.IsActive == true && s.EmployeeSalaryStructureIID != toDto.EmployeeSalaryStructureIID)
                    .AsNoTracking().ToList();

                    foreach (var salary in existingSalaryStructure)
                    {
                        salary.IsActive = false;
                        dbContext.Entry(salary).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }
                }


                dbContext.EmployeeSalaryStructures.Add(entity);

                if (entity.EmployeeSalaryStructureIID == 0)
                {
                    foreach (var compnent in entity.EmployeeSalaryStructureComponentMaps)
                    {
                        foreach (var variableMap in compnent.EmployeeSalaryStructureVariableMaps)
                        {

                            dbContext.Entry(variableMap).State = Microsoft.EntityFrameworkCore.EntityState.Added;

                        }

                        dbContext.Entry(compnent).State = Microsoft.EntityFrameworkCore.EntityState.Added;

                    }
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    foreach (var compnent in entity.EmployeeSalaryStructureComponentMaps)
                    {
                        foreach (var variableMap in compnent.EmployeeSalaryStructureVariableMaps)
                        {
                            if (variableMap.EmployeeSalaryStructureVariableMapIID == 0)
                            {
                                dbContext.Entry(variableMap).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            }
                            else
                            {
                                dbContext.Entry(variableMap).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }
                        }

                        if (compnent.EmployeeSalaryStructureComponentMapIID == 0)
                        {
                            dbContext.Entry(compnent).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                        else
                        {
                            dbContext.Entry(compnent).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                    }

                    foreach (var compnent in entity.EmployeeSalaryStructureLeaveSalaryMaps)
                    {
                        if (compnent.EmployeeSalaryStructureLeaveSalaryMapIID == 0)
                        {
                            dbContext.Entry(compnent).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                        else
                        {
                            dbContext.Entry(compnent).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                    }

                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();

                return GetEntity((long)toDto.JobInterviewMapID);
            }
        }

        public string CheckAndUpdateEmployeeStructure(long? employeeID, long? interviewMapID)
        {
            using (var dbContext = new dbEduegateHRContext())
            {
                var structure = dbContext.EmployeeSalaryStructures.FirstOrDefault(x => x.JobInterviewMapID == interviewMapID);
                if (structure != null)
                {
                    structure.EmployeeID = employeeID;
                }

                dbContext.SaveChanges();

                return null;
            }
        }

    }
}