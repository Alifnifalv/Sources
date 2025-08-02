using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Framework;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Fees;
using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Framework.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.School.Fees
{
    public class FeeMasterClassMapMapper : DTOEntityDynamicMapper
    {
        public static FeeMasterClassMapMapper Mapper(CallContext context)
        {
            var mapper = new FeeMasterClassMapMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<ClassFeeMasterDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.ClassFeeMasters.Where(x => x.ClassFeeMasterIID == IID)
                    .Include(i => i.AcademicYear)
                    .Include(x => x.FeeMasterClassMaps).ThenInclude(i => i.FeeMaster).ThenInclude(i => i.FeeCycle)
                    .Include(x => x.FeeMasterClassMaps).ThenInclude(i => i.FeePeriod)
                    .Include(x => x.FeeMasterClassMaps).ThenInclude(x => x.FeeMasterClassMontlySplitMaps)
                    .AsNoTracking()
                    .FirstOrDefault();

                var feemaster = new ClassFeeMasterDTO()
                {
                    ClassFeeMasterIID = entity.ClassFeeMasterIID,
                    //Class = new KeyValueDTO() { Key = entity.ClassID.ToString(), Value = entity.Class.ClassDescription },
                   // Package= new KeyValueDTO() { Key = entity.PackageConfigID.ToString(), Value = entity.PackageConfig.Name },
                    Academic = new KeyValueDTO()
                    {
                        Key = entity.AcadamicYearID.ToString(),
                        Value = entity.AcademicYear.Description
                        + " ( " + (Convert.ToDateTime(entity.AcademicYear.StartDate).ToString("dd-MM-yyyy")
                        + " - " + Convert.ToDateTime(entity.AcademicYear.EndDate).ToString("dd-MM-yyyy") + ")")
                    },
                    Description = entity.Description,
                    //Amount = entity.Amount,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                };

                foreach (var feemasterMonthly in entity.FeeMasterClassMaps)
                {
                    if (feemasterMonthly.Amount.HasValue)
                    {
                        feemaster.FeeMasterClassMaps = new List<FeeMasterClassFeePeriodDTO>();

                        foreach (var feeMasterMap in entity.FeeMasterClassMaps)
                        {
                            var montlySplitDTO = new List<FeeMasterClassMonthlySplitDTO>();

                            foreach (var monthlySplit in feeMasterMap.FeeMasterClassMontlySplitMaps)
                            {
                                if (monthlySplit.Amount.HasValue)
                                {
                                    if (monthlySplit.Amount.HasValue)
                                    {
                                        var entityChild = new FeeMasterClassMonthlySplitDTO()
                                        {
                                            FeeMasterClassMontlySplitMapIID = monthlySplit.FeeMasterClassMontlySplitMapIID,
                                            FeeMasterClassMapID=monthlySplit.FeeMasterClassMapID,
                                            Year = monthlySplit.Year.HasValue ? int.Parse(monthlySplit.Year.ToString()) : 0,
                                            MonthID = monthlySplit.MonthID.HasValue ? int.Parse(monthlySplit.MonthID.ToString()) : 0,
                                            FeePeriodID = monthlySplit.FeePeriodID.HasValue ? monthlySplit.FeePeriodID : (int?)null,
                                            Amount = monthlySplit.Amount.HasValue ? monthlySplit.Amount : (decimal?)null,
                                            Tax = monthlySplit.TaxAmount.HasValue ? monthlySplit.TaxAmount : (decimal?)null,
                                            TaxPercentage = monthlySplit.TaxPercentage.HasValue ? monthlySplit.TaxPercentage : (decimal?)null
                                        };

                                        montlySplitDTO.Add(entityChild);
                                    }
                                }
                            }

                            feemaster.FeeMasterClassMaps.Add(new FeeMasterClassFeePeriodDTO()
                            {
                                FeeMasterClassMapIID = feeMasterMap.FeeMasterClassMapIID,
                                ClassFeeMasterID=feeMasterMap.ClassFeeMasterID,
                                Amount = feeMasterMap.Amount,
                                TaxPercentage = feeMasterMap.TaxPercentage,
                                TaxAmount = feeMasterMap.TaxAmount,
                                FeeMaster = new KeyValueDTO()
                                {
                                    Key = feeMasterMap.FeeMasterID.ToString(),
                                    Value = feeMasterMap.FeeMaster.Description
                                },
                                FeePeriod = feeMasterMap.FeePeriodID.HasValue ? new KeyValueDTO()
                                {
                                    Key = feeMasterMap.FeePeriodID.HasValue ? Convert.ToString(feeMasterMap.FeePeriodID) : null,
                                    Value = !feeMasterMap.FeePeriodID.HasValue ? null : feeMasterMap.FeePeriod.Description + " ( " + feeMasterMap.FeePeriod.PeriodFrom.ToShortDateString() + '-'
                                        + feeMasterMap.FeePeriod.PeriodTo.ToShortDateString() + " ) "
                                } : new KeyValueDTO(),
                                FeeMasterClassMontlySplitMaps = montlySplitDTO,
                                FeeCycleID = feeMasterMap.FeeMaster.FeeCycleID,
                                IsFeePeriodDisabled = feeMasterMap.FeeMaster.FeeCycleID.HasValue ? feeMasterMap.FeeMaster.FeeCycleID.Value != 3 : true,
                            }); ;

                        }

                        
                    }
                }

                return ToDTOString(feemaster);
            }
          
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as ClassFeeMasterDTO;
            if (toDto.ClassID == null)
            {
                throw new Exception("Please select class");
            }
            if (!toDto.AcadamicYearID.HasValue)
            {
                throw new Exception("Please select acadamic year");
            }
            if (toDto.FeeMasterClassMaps == null || toDto.FeeMasterClassMaps.Count == 0)
            {
                throw new Exception("Fee details needs to be filled!");
            }
            else if (toDto.FeeMasterClassMaps.Where(i => i.FeeMasterID != null && (i.Amount == null || i.Amount <= 0)).Count() > 0)
            {
                throw new Exception("Amount cannot be empty ,zero or negative value!");
            }
            else if (toDto.FeeMasterClassMaps.Where(i => i.FeePeriodID != null && i.FeeMasterClassMontlySplitMaps.Count == 0).Count() > 0)
            {
                throw new Exception("Monthly split up not provided for the given fee period.Please check!");
            }
            
            //convert the dto to entity and pass to the repository.
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = new ClassFeeMaster()
                {
                    ClassFeeMasterIID = toDto.ClassFeeMasterIID,
                    ClassID = toDto.ClassID,
                    //PackageConfigID = toDto.PackageConfigID.HasValue ? toDto.PackageConfigID.Value : (long?)null,
                    AcadamicYearID = toDto.AcadamicYearID,
                    Description = toDto.Description,
                    CreatedBy = toDto.ClassFeeMasterIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                    UpdatedBy = toDto.ClassFeeMasterIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                    CreatedDate = toDto.ClassFeeMasterIID == 0 ? DateTime.Now : dto.CreatedDate,
                    UpdatedDate = toDto.ClassFeeMasterIID > 0 ? DateTime.Now : dto.UpdatedDate,
                    //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
                };

            
                //get existing parent iids
                var parentRowIIDs = toDto.FeeMasterClassMaps.Select(s => s.FeeMasterClassMapIID).ToList();
                //get existing parent entities
                var parentRowEntities = dbContext.FeeMasterClassMaps.Where(x =>
                    x.ClassFeeMasterID == entity.ClassFeeMasterIID &&
                    !parentRowIIDs.Contains(x.FeeMasterClassMapIID)).AsNoTracking().ToList();

                //delete parent maps
                if (parentRowEntities != null && parentRowEntities.Count > 0)
                {
                    //delete child entities with parent
                    foreach (var classMaps in parentRowEntities)
                    {
                        dbContext.FeeMasterClassMontlySplitMaps.RemoveRange(classMaps.FeeMasterClassMontlySplitMaps);
                    }
                    dbContext.FeeMasterClassMaps.RemoveRange(parentRowEntities);
                }

                entity.FeeMasterClassMaps = new List<FeeMasterClassMap>();

                foreach (var feemasterMonthly in toDto.FeeMasterClassMaps)
                {
                    if (feemasterMonthly.Amount.HasValue)
                    {
                        //get existing split iids
                        var splitIIDs = feemasterMonthly.FeeMasterClassMontlySplitMaps
                            .Select(a => a.FeeMasterClassMontlySplitMapIID).ToList();

                        //delete split maps
                        var splitEntities = dbContext.FeeMasterClassMontlySplitMaps.Where(x =>
                            x.FeeMasterClassMapID == feemasterMonthly.FeeMasterClassMapIID &&
                            !splitIIDs.Contains(x.FeeMasterClassMontlySplitMapIID)).AsNoTracking().ToList();

                        if (splitEntities.IsNotNull())
                            dbContext.FeeMasterClassMontlySplitMaps.RemoveRange(splitEntities);

                        var monthlySplit = new List<FeeMasterClassMontlySplitMap>();
                  
                        foreach (var feeMasterMonthlyDto in feemasterMonthly.FeeMasterClassMontlySplitMaps)
                        {
                            if (feeMasterMonthlyDto.Amount.HasValue)
                            {
                                var entityChild = new FeeMasterClassMontlySplitMap()
                                {
                                    FeeMasterClassMontlySplitMapIID = feeMasterMonthlyDto.FeeMasterClassMontlySplitMapIID,
                                    FeeMasterClassMapID = feemasterMonthly.FeeMasterClassMapIID,
                                    MonthID = byte.Parse(feeMasterMonthlyDto.MonthID.ToString()),
                                    Year = feeMasterMonthlyDto.Year,
                                    FeePeriodID = feemasterMonthly.FeePeriodID,
                                    Amount = feeMasterMonthlyDto.Amount.HasValue ? feeMasterMonthlyDto.Amount : (decimal?)null,
                                    TaxAmount = feeMasterMonthlyDto.Tax.HasValue ? feeMasterMonthlyDto.Tax : (decimal?)null,
                                    TaxPercentage = feeMasterMonthlyDto.TaxPercentage.HasValue ? feeMasterMonthlyDto.TaxPercentage : (decimal?)null
                                };

                                monthlySplit.Add(entityChild);
                            }
                        }

                        entity.FeeMasterClassMaps.Add(new FeeMasterClassMap()
                        {
                            FeeMasterClassMapIID = feemasterMonthly.FeeMasterClassMapIID,
                            ClassFeeMasterID = toDto.ClassFeeMasterIID,
                            FeeMasterID = feemasterMonthly.FeeMasterID,
                            FeePeriodID = feemasterMonthly.FeePeriodID,
                            Amount = feemasterMonthly.Amount,
                            TaxPercentage = feemasterMonthly.TaxPercentage,
                            TaxAmount = feemasterMonthly.TaxAmount,
                            FeeMasterClassMontlySplitMaps = monthlySplit,
                        });
                    }                    
                }

                dbContext.ClassFeeMasters.Add(entity);
                if (entity.ClassFeeMasterIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    foreach (var classMap in entity.FeeMasterClassMaps)
                    {
                        foreach (var monthlySplit in classMap.FeeMasterClassMontlySplitMaps)
                        {
                            if (monthlySplit.FeeMasterClassMontlySplitMapIID != 0)
                            {
                                dbContext.Entry(monthlySplit).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }
                            else
                            {
                                dbContext.Entry(monthlySplit).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            }
                        }

                        if (classMap.FeeMasterClassMapIID != 0)
                        {
                            dbContext.Entry(classMap).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                        else
                        {
                            dbContext.Entry(classMap).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                    }

                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();

                return GetEntity(entity.ClassFeeMasterIID);
            }
        }

    }
}