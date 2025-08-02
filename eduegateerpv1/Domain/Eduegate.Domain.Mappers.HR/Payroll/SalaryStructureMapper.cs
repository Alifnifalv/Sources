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
                    .Include(i => i.SalaryStructureComponentMaps)
                    .Include(i => i.SalaryStructureComponentMaps).ThenInclude(i => i.SalaryComponent)
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

                structure.SalaryComponents = new List<SalaryStructureComponentDTO>();
                foreach (var salaryComponent in entity.SalaryStructureComponentMaps)
                {
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
                        }
                    });
                }

                return ToDTOString(structure);
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as SalaryStructureDTO;
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
                };

                var IIDs = toDto.SalaryComponents
                    .Select(a => a.SalaryStructureComponentMapIID).ToList();
                //delete maps
                var entities = dbContext.SalaryStructureComponentMaps.Where(x =>
                    x.SalaryStructureID == entity.SalaryStructureID &&
                    !IIDs.Contains(x.SalaryStructureComponentMapIID)).AsNoTracking().ToList();

                if (entities.IsNotNull())
                    dbContext.SalaryStructureComponentMaps.RemoveRange(entities);

                entity.SalaryStructureComponentMaps = new List<SalaryStructureComponentMap>();

                foreach (var salaryComp in toDto.SalaryComponents)
                {
                    // Date Different Check
                    if (salaryComp.MinAmount >= salaryComp.MaxAmount)
                    {
                        throw new Exception("Select Amount Properlly!!");
                    }
                    entity.SalaryStructureComponentMaps.Add(new SalaryStructureComponentMap()
                    {
                        SalaryStructureComponentMapIID = salaryComp.SalaryStructureComponentMapIID,
                        SalaryComponentID = salaryComp.SalaryComponentID,
                        MinAmount = salaryComp.MinAmount,
                        MaxAmount = salaryComp.MaxAmount
                    });
                }
                if (entity.SalaryStructureID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    foreach (var compnent in entity.SalaryStructureComponentMaps)
                    {
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