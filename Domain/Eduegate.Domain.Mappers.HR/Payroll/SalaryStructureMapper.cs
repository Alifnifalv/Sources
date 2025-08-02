using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Domain.Entity.HR;
using Eduegate.Domain.Entity.HR.Payroll;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.HR.Payroll;
using Newtonsoft.Json;
using Eduegate.Framework.Extensions;
using Eduegate.Framework;
using Microsoft.EntityFrameworkCore;
using Eduegate.Domain.Entity.HR.Models;

namespace Eduegate.Domain.Mappers.HR.Payroll
{
    public class SalaryStructureMapper : DTOEntityDynamicMapper
    {
        public static SalaryStructureMapper Mapper(CallContext context)
        {
            var mapper = new SalaryStructureMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<SalaryStructureDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
            {
                var entity = dbContext.SalaryStructures.Where(s => s.SalaryStructureID == IID)
                    .Include(i => i.Account)
                    .Include(i => i.PaymentMode)
                    .Include(i => i.PayrollFrequency)
                    .Include(i => i.SalaryStructureScaleMaps)
                    .Include(i => i.SalaryStructureComponentMaps)
                    .Include(i => i.SalaryStructureComponentMaps).ThenInclude(i => i.SalaryComponent)
                    .Include(i => i.SalaryStructureComponentMaps).ThenInclude(i => i.SalaryComponentVariableMap)
                    .Include(i => i.TimeSheetSalaryComponent)
                    .AsNoTracking()
                    .FirstOrDefault();

                var structure = new SalaryStructureDTO()
                {
                    SalaryStructureID = entity.SalaryStructureID,
                    AccountID = entity.AccountID,
                    IsSalaryBasedOnTimeSheet = entity.IsSalaryBasedOnTimeSheet,
                    PaymentModeID = entity.PaymentModeID,
                    IsActive = entity.IsActive,
                    PayrollFrequencyID = entity.PayrollFrequencyID,
                    TimeSheetSalaryComponentID = entity.TimeSheetSalaryComponentID,
                    StructureName = entity.StructureName,
                    TimeSheetHourRate = entity.TimeSheetHourRate,
                    TimeSheetLeaveEncashmentPerDay = entity.TimeSheetLeaveEncashmentPerDay,
                    TimeSheetMaximumBenefits = entity.TimeSheetMaximumBenefits,
                    StructureCode = entity.StructureCode,
                    PaymentMode = new KeyValueDTO()
                    {
                        Key = entity.PaymentModeID.ToString(),
                        Value = entity.PaymentMode.PaymentName
                    },
                    PayrollFrequency = new KeyValueDTO()
                    {
                        Key = entity.PayrollFrequencyID.ToString(),
                        Value = entity.PayrollFrequency.FrequencyName
                    },
                    TimeSheetSalaryComponent = entity.TimeSheetSalaryComponentID.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.TimeSheetSalaryComponentID.HasValue ? Convert.ToString(entity.TimeSheetSalaryComponentID) : null,
                        Value = entity.TimeSheetSalaryComponent.Description
                    } : new KeyValueDTO(),

                    Account = new KeyValueDTO()
                    {
                        Key = entity.AccountID.ToString(),
                        Value = entity.Account.AccountName
                    }
                };

                structure.SalaryStructureScale = new List<SalaryStructureScaleDTO>();
                structure.SalaryStructureScale = entity.SalaryStructureScaleMaps.Select(x => new SalaryStructureScaleDTO()
                {
                    StructureScaleID = x.StructureScaleID,
                    IsSponsored = x.IsSponsored,
                    MinAmount = x.MinAmount,
                    MaxAmount = x.MaxAmount,
                    MaritalStatusID = x.MaritalStatusID,
                    AccomodationTypeID = x.AccomodationTypeID,
                    LeaveTicket = x.LeaveTicket,
                    IncrementNote = x.IncrementNote,
                    SalaryStructureID = x.SalaryStructureID,
                }).ToList();

                structure.SalaryComponents = new List<SalaryStructureComponentDTO>();

                foreach (var salaryComponent in entity.SalaryStructureComponentMaps)
                {
                    var listcomponentVariableMapDTO = new List<SalaryComponentVariableMapDTO>();
                    foreach (var variablemap in salaryComponent.SalaryComponentVariableMap)
                    {
                        var componentVariableMapDTO = new SalaryComponentVariableMapDTO()
                        {
                            SalaryComponentVariableMapIID = variablemap.SalaryComponentVariableMapIID,
                            SalaryStructureComponentMapID = salaryComponent.SalaryStructureComponentMapIID,
                            VariableValue = variablemap.VariableValue,
                            VariableKey = variablemap.VariableKey
                        };
                        listcomponentVariableMapDTO.Add(componentVariableMapDTO);
                    }

                    structure.SalaryComponents.Add(new SalaryStructureComponentDTO()
                    {
                        SalaryStructureComponentMapIID = salaryComponent.SalaryStructureComponentMapIID,
                        MinAmount = salaryComponent.MinAmount,
                        MaxAmount = salaryComponent.MaxAmount,
                        Formula = salaryComponent.Formula,
                        SalaryComponent = new KeyValueDTO()
                        {
                            Key = salaryComponent.SalaryComponentID.ToString(),
                            Value = salaryComponent.SalaryComponent.Description
                        },
                        SalaryComponentVariableMap = listcomponentVariableMapDTO
                    });
                }

                return ToDTOString(structure);
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as SalaryStructureDTO;
            foreach (var salaryComp in toDto.SalaryComponents)
            {
                // Date Different Check
                if (salaryComp.MinAmount >= salaryComp.MaxAmount)
                {
                    throw new Exception("Select Amount Properlly!!");
                }
            }
            //convert the dto to entity and pass to the repository.
            using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
            {
                var entity = new SalaryStructure()
                {
                    SalaryStructureID = toDto.SalaryStructureID,
                    AccountID = toDto.AccountID,
                    IsSalaryBasedOnTimeSheet = toDto.IsSalaryBasedOnTimeSheet,
                    PaymentModeID = toDto.PaymentModeID,
                    IsActive = toDto.IsActive,
                    PayrollFrequencyID = toDto.PayrollFrequencyID,
                    TimeSheetSalaryComponentID = toDto.TimeSheetSalaryComponentID,
                    StructureName = toDto.StructureName,
                    TimeSheetHourRate = toDto.TimeSheetHourRate,
                    TimeSheetLeaveEncashmentPerDay = toDto.TimeSheetLeaveEncashmentPerDay,
                    TimeSheetMaximumBenefits = toDto.TimeSheetMaximumBenefits,
                    StructureCode = toDto.StructureCode
                };

                //ScaleTable
                var scaleIDs = toDto.SalaryStructureScale
                            .Select(a => a.StructureScaleID).ToList();
                //delete maps
                var scaleEntities = dbContext.SalaryStructureScaleMaps.Where(x =>
                    x.SalaryStructureID == entity.SalaryStructureID &&
                    !scaleIDs.Contains(x.StructureScaleID)).AsNoTracking().ToList();

                if (scaleIDs.IsNotNull())
                    dbContext.SalaryStructureScaleMaps.RemoveRange(scaleEntities);

                var IIDs = toDto.SalaryComponents
                    .Select(a => a.SalaryStructureComponentMapIID).ToList();
                //delete maps
                var entities = dbContext.SalaryStructureComponentMaps.Where(x =>
                    x.SalaryStructureID == entity.SalaryStructureID &&
                    !IIDs.Contains(x.SalaryStructureComponentMapIID)).AsNoTracking().ToList();

                if (entities.IsNotNull())
                    dbContext.SalaryStructureComponentMaps.RemoveRange(entities);


                var variableIIDs = toDto.SalaryComponents
                   .SelectMany(a => a.SalaryComponentVariableMap).Select(x => x.SalaryComponentVariableMapIID).ToList();
                var varEntities = dbContext.SalaryComponentVariableMaps.Where(x =>
                 x.SalaryStructureComponentMapID != null && IIDs.Contains(x.SalaryStructureComponentMapID.Value) &&
                   !variableIIDs.Contains(x.SalaryComponentVariableMapIID)).AsNoTracking().ToList();

                if (entities.IsNotNull())
                    dbContext.SalaryComponentVariableMaps.RemoveRange(varEntities);

                entity.SalaryStructureScaleMaps = new List<SalaryStructureScaleMap>();

                entity.SalaryStructureScaleMaps = toDto.SalaryStructureScale.Select(x => new SalaryStructureScaleMap()
                {
                    StructureScaleID = x.StructureScaleID,
                    IsSponsored = x.IsSponsored,
                    MinAmount = x.MinAmount,
                    MaxAmount = x.MaxAmount,
                    MaritalStatusID = x.MaritalStatusID,
                    AccomodationTypeID = x.AccomodationTypeID,
                    LeaveTicket = x.LeaveTicket,
                    IncrementNote = x.IncrementNote,
                    SalaryStructureID = x.SalaryStructureID,
                }).ToList();


                entity.SalaryStructureComponentMaps = new List<SalaryStructureComponentMap>();

                foreach (var salaryComp in toDto.SalaryComponents)
                {

                    var listcomponentVariableMap = new List<SalaryComponentVariableMap>();
                    foreach (var variablemap in salaryComp.SalaryComponentVariableMap)
                    {
                        var componentVariableMap = new SalaryComponentVariableMap()
                        {
                            SalaryComponentVariableMapIID = variablemap.SalaryComponentVariableMapIID,
                            SalaryStructureComponentMapID = salaryComp.SalaryStructureComponentMapIID,
                            VariableValue = variablemap.VariableValue,
                            VariableKey = variablemap.VariableKey
                        };
                        listcomponentVariableMap.Add(componentVariableMap);
                    }
                    entity.SalaryStructureComponentMaps.Add(new SalaryStructureComponentMap()
                    {
                        SalaryStructureComponentMapIID = salaryComp.SalaryStructureComponentMapIID,
                        SalaryComponentID = salaryComp.SalaryComponentID,
                        MinAmount = salaryComp.MinAmount,
                        MaxAmount = salaryComp.MaxAmount,
                        SalaryComponentVariableMap = listcomponentVariableMap
                    });
                }
                if (entity.SalaryStructureID == 0)
                {
                    foreach (var scale in entity.SalaryStructureScaleMaps)
                    {
                        dbContext.Entry(scale).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    }

                    foreach (var compnent in entity.SalaryStructureComponentMaps)
                    {
                        foreach (var variableMap in compnent.SalaryComponentVariableMap)
                        {

                            dbContext.Entry(variableMap).State = Microsoft.EntityFrameworkCore.EntityState.Added;

                        }

                        dbContext.Entry(compnent).State = Microsoft.EntityFrameworkCore.EntityState.Added;

                    }

                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    foreach (var scaleMap in entity.SalaryStructureScaleMaps)
                    {
                        if (scaleMap.StructureScaleID == 0)
                        {
                            dbContext.Entry(scaleMap).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                        else
                        {
                            dbContext.Entry(scaleMap).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                    }


                    foreach (var compnent in entity.SalaryStructureComponentMaps)
                    {
                        foreach (var variableMap in compnent.SalaryComponentVariableMap)
                        {
                            if (variableMap.SalaryComponentVariableMapIID == 0)
                            {
                                dbContext.Entry(variableMap).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            }
                            else
                            {
                                dbContext.Entry(variableMap).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }
                        }
                        if (compnent.SalaryStructureComponentMapIID != 0)
                        {
                            dbContext.Entry(compnent).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                        else
                        {
                            dbContext.Entry(compnent).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                    }

                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();

                return GetEntity(entity.SalaryStructureID);
            }
        }
    }
}