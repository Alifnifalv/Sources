using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Fees;
using Eduegate.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Framework.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Eduegate.Domain.Entity.HR.Payroll;
using Eduegate.Services.Contracts.Enums.Synchronizer;

namespace Eduegate.Domain.Mappers.School.Fees
{
    public class FeeStructureMapper : DTOEntityDynamicMapper
    {

        public static FeeStructureMapper Mapper(CallContext context)
        {
            var mapper = new FeeStructureMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<FeeStructureDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

                var entity = dbContext.FeeStructures.Where(x => x.FeeStructureIID == IID)
                    .Include(i => i.AcademicYear)
                    .Include(x => x.FeeStructureFeeMaps)
                    .ThenInclude(x => x.FeeMaster)
                    .Include(x => x.FeeStructureFeeMaps)
                    .ThenInclude(x => x.FeePeriod)
                    .Include(x => x.FeeStructureFeeMaps)
                    .ThenInclude(x => x.FeeStructureMontlySplitMaps)
                    .AsNoTracking()
                    .FirstOrDefault();

                var feemaster = new FeeStructureDTO()
                {
                    FeeStructureIID = entity.FeeStructureIID,
                    Name = entity.Name,
                    IsActive = entity.IsActive,
                    AcademicYear = new KeyValueDTO()
                    {
                        Key = entity.AcadamicYearID.ToString(),
                        Value = entity.AcademicYear.Description
                        + " ( " + (Convert.ToDateTime(entity.AcademicYear.StartDate).ToString(dateFormat, CultureInfo.InvariantCulture)
                        + " - " + Convert.ToDateTime(entity.AcademicYear.EndDate).ToString(dateFormat, CultureInfo.InvariantCulture) + ")")
                    },
                    Description = entity.Description,
                    //Amount = entity.Amount,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    SchoolID = entity.SchoolID,
                };

                foreach (var feemasterMonthly in entity.FeeStructureFeeMaps)
                {
                    if (feemasterMonthly.Amount.HasValue)
                    {
                        feemaster.FeeStructureFeeMaps = new List<FeeStructureFeeMapDTO>();

                        foreach (var feeMasterMap in entity.FeeStructureFeeMaps)
                        {
                            var montlySplitDTO = new List<FeeStructureMontlySplitMapDTO>();

                            foreach (var monthlySplit in feeMasterMap.FeeStructureMontlySplitMaps)
                            {
                                if (monthlySplit.Amount.HasValue)
                                {
                                    if (monthlySplit.Amount.HasValue)
                                    {
                                        var entityChild = new FeeStructureMontlySplitMapDTO()
                                        {
                                            FeeStructureMontlySplitMapIID = monthlySplit.FeeStructureMontlySplitMapIID,
                                            FeeStructureFeeMapID = monthlySplit.FeeStructureFeeMapID,
                                            Year = monthlySplit.Year.HasValue ? int.Parse(monthlySplit.Year.ToString()) : 0,
                                            MonthID = monthlySplit.MonthID.HasValue ? int.Parse(monthlySplit.MonthID.ToString()) : 0,
                                            Amount = monthlySplit.Amount.Value
                                        };

                                        montlySplitDTO.Add(entityChild);
                                    }
                                }
                            }

                            var orderedList = montlySplitDTO.OrderBy(x => x.Year).ToList();
                            feemaster.FeeStructureFeeMaps.Add(new FeeStructureFeeMapDTO()
                            {
                                FeeStructureFeeMapIID = feeMasterMap.FeeStructureFeeMapIID,
                                FeeStructureID = feeMasterMap.FeeStructureID,
                                Amount = feeMasterMap.Amount,
                                FeeMaster = new KeyValueDTO()
                                {
                                    Key = feeMasterMap.FeeMasterID.ToString(),
                                    Value = feeMasterMap.FeeMaster.Description
                                },
                                FeePeriod = feeMasterMap.FeePeriodID.HasValue ? new KeyValueDTO()
                                {
                                    Key = feeMasterMap.FeePeriodID.HasValue ? Convert.ToString(feeMasterMap.FeePeriodID) : null,
                                    Value = !feeMasterMap.FeePeriodID.HasValue ? null : feeMasterMap.FeePeriod.Description + " ( " + feeMasterMap.FeePeriod.PeriodFrom.ToString(dateFormat, CultureInfo.InvariantCulture) + '-'
                                        + feeMasterMap.FeePeriod.PeriodTo.ToString(dateFormat, CultureInfo.InvariantCulture) + " ) "
                                } : new KeyValueDTO(),
                                FeeStructureMontlySplitMaps = orderedList,
                                FeeCycleID = feeMasterMap.FeeMaster.FeeCycleID,
                                IsFeePeriodDisabled = feeMasterMap.FeeMaster.FeeCycleID.HasValue ? feeMasterMap.FeeMaster.FeeCycleID.Value != 1 : true,
                            }); ;

                        }
                    }
                }

                return ToDTOString(feemaster);
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as FeeStructureDTO;

            if (!toDto.AcadamicYearID.HasValue)
            {
                throw new Exception("Please select acadamic year");
            }
            if (toDto.FeeStructureFeeMaps == null || toDto.FeeStructureFeeMaps.Count == 0)
            {
                throw new Exception("Fee details needs to be filled!");
            }
            else if (toDto.FeeStructureFeeMaps.Where(i => i.FeeMasterID != null && (i.Amount == null || i.Amount <= 0)).Count() > 0)
            {
                throw new Exception("Amount cannot be empty ,zero or negative value!");
            }
            else if (toDto.FeeStructureFeeMaps.Where(i => i.FeePeriodID != null && i.FeeStructureMontlySplitMaps.Count == 0).Count() > 0)
            {
                throw new Exception("Monthly split up not provided for the given fee period.Please check!");
            }

            //convert the dto to entity and pass to the repository.
            using (var dbContext = new dbEduegateSchoolContext())
            {
                //get existing parent iids
                var parentRowIIDs = toDto.FeeStructureFeeMaps.Select(s => s.FeeStructureFeeMapIID).ToList();

                var queryFeeStructureExist = dbContext.FeeDueFeeTypeMaps.AsNoTracking().Any(x =>
                parentRowIIDs.Contains(x.FeeStructureFeeMapID.Value));
                if (queryFeeStructureExist == true)
                {
                    throw new Exception("Fee Structure already used, cannot be edited!");
                }

                var entity = new FeeStructure()
                {
                    FeeStructureIID = toDto.FeeStructureIID,
                    Name = toDto.Name,
                    IsActive = toDto.IsActive,
                    AcadamicYearID = toDto.AcadamicYearID,
                    Description = toDto.Description,
                    SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                    CreatedBy = toDto.FeeStructureIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                    UpdatedBy = toDto.FeeStructureIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                    CreatedDate = toDto.FeeStructureIID == 0 ? DateTime.Now : dto.CreatedDate,
                    UpdatedDate = toDto.FeeStructureIID > 0 ? DateTime.Now : dto.UpdatedDate,
                };

                //get existing parent entities
                var parentRowEntities = dbContext.FeeStructureFeeMaps.Where(x =>
                    x.FeeStructureID == entity.FeeStructureIID &&
                    !parentRowIIDs.Contains(x.FeeStructureFeeMapIID)).AsNoTracking().ToList();

                //delete parent maps
                if (parentRowEntities != null && parentRowEntities.Count > 0)
                {
                    //delete child entities with parent
                    foreach (var classMaps in parentRowEntities)
                    {
                        dbContext.FeeStructureMontlySplitMaps.RemoveRange(classMaps.FeeStructureMontlySplitMaps);
                    }
                    dbContext.FeeStructureFeeMaps.RemoveRange(parentRowEntities);
                }

                entity.FeeStructureFeeMaps = new List<FeeStructureFeeMap>();

                foreach (var feemasterMonthly in toDto.FeeStructureFeeMaps)
                {
                    if (feemasterMonthly.Amount.HasValue)
                    {
                        //get existing split iids
                        var splitIIDs = feemasterMonthly.FeeStructureMontlySplitMaps
                            .Select(a => a.FeeStructureMontlySplitMapIID).ToList();

                        //delete split maps
                        var splitEntities = dbContext.FeeStructureMontlySplitMaps.Where(x =>
                            x.FeeStructureFeeMapID == feemasterMonthly.FeeStructureFeeMapIID &&
                            !splitIIDs.Contains(x.FeeStructureMontlySplitMapIID)).AsNoTracking().ToList();

                        if (splitEntities.IsNotNull())
                            dbContext.FeeStructureMontlySplitMaps.RemoveRange(splitEntities);

                        var monthlySplit = new List<FeeStructureMontlySplitMap>();

                        foreach (var feeMasterMonthlyDto in feemasterMonthly.FeeStructureMontlySplitMaps)
                        {
                            if (feeMasterMonthlyDto.Amount.HasValue)
                            {
                                var entityChild = new FeeStructureMontlySplitMap()
                                {
                                    FeeStructureMontlySplitMapIID = feeMasterMonthlyDto.FeeStructureMontlySplitMapIID,
                                    FeeStructureFeeMapID = feemasterMonthly.FeeStructureFeeMapIID,
                                    MonthID = byte.Parse(feeMasterMonthlyDto.MonthID.ToString()),
                                    Year = feeMasterMonthlyDto.Year,
                                    Amount = feeMasterMonthlyDto.Amount.HasValue ? feeMasterMonthlyDto.Amount : (decimal?)null
                                };

                                monthlySplit.Add(entityChild);
                            }
                        }

                        entity.FeeStructureFeeMaps.Add(new FeeStructureFeeMap()
                        {
                            FeeStructureFeeMapIID = feemasterMonthly.FeeStructureFeeMapIID,
                            FeeStructureID = toDto.FeeStructureIID,
                            FeeMasterID = feemasterMonthly.FeeMasterID,
                            FeePeriodID = feemasterMonthly.FeePeriodID,
                            Amount = feemasterMonthly.Amount,
                            FeeStructureMontlySplitMaps = monthlySplit,
                        });
                    }
                }

                dbContext.FeeStructures.Add(entity);
                if (entity.FeeStructureIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    foreach (var classMap in entity.FeeStructureFeeMaps)
                    {
                        foreach (var monthlySplit in classMap.FeeStructureMontlySplitMaps)
                        {
                            if (monthlySplit.FeeStructureMontlySplitMapIID != 0)
                            {
                                dbContext.Entry(monthlySplit).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }
                            else
                            {
                                dbContext.Entry(monthlySplit).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            }
                        }

                        if (classMap.FeeStructureFeeMapIID != 0)
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

                return GetEntity(entity.FeeStructureIID);
            }
        }

        public List<KeyValueDTO> GetFeeStructure(int academicYearID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var feeStructureList = new List<KeyValueDTO>();

                var entities = dbContext.FeeStructures.Where(x => x.AcadamicYearID == academicYearID && x.SchoolID == _context.SchoolID).AsNoTracking().ToList();

                foreach (var structure in entities)
                {
                    feeStructureList.Add(new KeyValueDTO
                    {
                        Key = structure.FeeStructureIID.ToString(),
                        Value = structure.Name
                    });
                }

                return feeStructureList;
            }
        }

        public List<KeyValueDTO> GetFeePeriod(int academicYearID, long studentID, int? feeMasterID)
        {
            var acdmYrId = (academicYearID == 0 ? _context.AcademicYearID : academicYearID);
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

                var feePeriodList = new List<KeyValueDTO>();

                if (studentID != 0)
                {
                    var getData = dbContext.StudentFeeDues.Where(x => x.StudentId == studentID && x.IsCancelled != true && x.CollectionStatus != true && x.FeeDueFeeTypeMaps.Any(u => u.FeeMasterID == feeMasterID))
                        .Include(i => i.FeeDueFeeTypeMaps).ThenInclude(i => i.FeePeriod)
                        .Include(i => i.FeeDueFeeTypeMaps).ThenInclude(i => i.FeeMaster)
                        .Include(i => i.FeeDueCancellations)
                        .AsNoTracking().ToList();

                    foreach (var dat in getData)
                    {
                        var feeDueFeeTyp = dat.FeeDueFeeTypeMaps.FirstOrDefault();

                        if (feeDueFeeTyp?.FeePeriodID != null && !feePeriodList.Any(d => d.Key == feeDueFeeTyp.FeePeriodID.ToString()))
                        {
                            feePeriodList.Add(new KeyValueDTO
                            {
                                Key = feeDueFeeTyp.FeePeriodID.ToString(),
                                Value = feeDueFeeTyp.FeePeriod.Description + " (" + feeDueFeeTyp.FeePeriod.PeriodFrom.ToString(dateFormat, CultureInfo.InvariantCulture) + " - " + feeDueFeeTyp.FeePeriod.PeriodTo.ToString(dateFormat, CultureInfo.InvariantCulture) + ")",
                            });
                        }
                    }
                }
                else if (feeMasterID != 0)
                {
                    var getPeriods = dbContext.FeeStructureFeeMaps
                                    .Where(x => x.FeeMasterID == feeMasterID && x.FeePeriod.AcademicYearId == acdmYrId)
                                    .Include(x => x.FeePeriod)
                                    .Select(x => x.FeePeriod)
                                    .GroupBy(x => x.FeePeriodID)
                                    .Select(group => group.First())
                                    .ToList();

                    foreach (var period in getPeriods)
                    {
                        feePeriodList.Add(new KeyValueDTO
                        {
                            Key = period.FeePeriodID.ToString(),
                            Value = period.Description + " (" + period.PeriodFrom.ToString(dateFormat, CultureInfo.InvariantCulture) + " - " + period.PeriodTo.ToString(dateFormat, CultureInfo.InvariantCulture) + ")"
                        });
                    }
                }
                else
                {
                    var entities = dbContext.FeePeriods.Where(x => x.AcademicYearId == acdmYrId).AsNoTracking().ToList();

                    foreach (var period in entities)
                    {
                        feePeriodList.Add(new KeyValueDTO
                        {
                            Key = period.FeePeriodID.ToString(),
                            Value = period.Description + " (" + period.PeriodFrom.ToString(dateFormat, CultureInfo.InvariantCulture) + " - " + period.PeriodTo.ToString(dateFormat, CultureInfo.InvariantCulture) + ")"
                        });
                    }
                }

                return feePeriodList;
            }
        }

    }
}
