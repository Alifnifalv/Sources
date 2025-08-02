using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Extensions;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.School.Fees;
using Eduegate.Services.Contracts.School.Students;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eduegate.Domain.Mappers.School.Students
{
    public class StudentGroupFeeMasterMapper : DTOEntityDynamicMapper
    {
        public static StudentGroupFeeMasterMapper Mapper(CallContext context)
        {
            var mapper = new StudentGroupFeeMasterMapper();
            mapper._context = context;
            return mapper;
        }
        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<StudentGroupFeeMasterDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.StudentGroupFeeMasters.Where(a => a.StudentGroupFeeMasterIID == IID)
                    .Include(a => a.StudentGroup)
                    .Include(a => a.AcademicYear)
                    .Include(a => a.StudentGroupFeeTypeMaps).ThenInclude(a => a.FeeMaster)
                    .Include(a => a.StudentGroupFeeTypeMaps).ThenInclude(a => a.FeePeriod)
                    .AsNoTracking()
                    .FirstOrDefault();

                var feemaster = new StudentGroupFeeMasterDTO()
                {
                    StudentGroupFeeMasterIID = entity.StudentGroupFeeMasterIID,
                    StudentGroup = new KeyValueDTO()
                    {
                        Key = entity.StudentGroupID.ToString(),
                        Value = entity.StudentGroup.GroupName
                    },
                    AcadamicYear = new KeyValueDTO()
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
                    //TimeStamps = Convert.ToBase64String(entity.TimeStamps),
                };

                foreach (var feemasterMonthly in entity.StudentGroupFeeTypeMaps)
                {
                    if (feemasterMonthly.PercentageAmount.HasValue)
                    {
                        feemaster.FeeMasterClassMaps = new List<StudentGroupFeeTypeMapDTO>();

                        foreach (var feeMasterMap in entity.StudentGroupFeeTypeMaps)
                        {
                            var montlySplitDTO = new List<FeeMasterClassMonthlySplitDTO>();

                            //foreach (var monthlySplit in feeMasterMap.StudentGroupFeeMaster.StudentGroupFeeTypeMaps)
                            //{
                            //    if (monthlySplit.Amount.HasValue)
                            //    {
                            //        if (monthlySplit.Amount.HasValue)
                            //        {
                            //            var entityChild = new FeeMasterClassMonthlySplitDTO()
                            //            {
                            //                FeeMasterClassMontlySplitMapIID = monthlySplit.StudentGroupFeeTypeMapIID,
                            //                FeeMasterClassMapID = monthlySplit.StudentGroupFeeMasterID,
                            //                //MonthID = monthlySplit.MonthID.HasValue ? int.Parse(monthlySplit.MonthID.ToString()) : 0,
                            //                Amount = monthlySplit.Amount.HasValue ? monthlySplit.Amount : (decimal?)null,
                            //                Tax = monthlySplit.TaxAmount.HasValue ? monthlySplit.TaxAmount : (decimal?)null,
                            //                TaxPercentage = monthlySplit.TaxPercentage.HasValue ? monthlySplit.TaxPercentage : (decimal?)null
                            //            };

                            //            montlySplitDTO.Add(entityChild);
                            //        }
                            //    }
                            //}

                            feemaster.FeeMasterClassMaps.Add(new StudentGroupFeeTypeMapDTO()
                            {
                                StudentGroupFeeTypeMapIID = feeMasterMap.StudentGroupFeeTypeMapIID,
                                StudentGroupFeeMasterID = feeMasterMap.StudentGroupFeeMasterID,
                                PercentageAmount = feeMasterMap.PercentageAmount,
                                TaxPercentage = feeMasterMap.TaxPercentage,
                                TaxAmount = feeMasterMap.TaxAmount,
                                //IsDiscount = feeMasterMap.IsDiscount,
                                IsPercentage = feeMasterMap.IsPercentage,
                                FeeMaster = new KeyValueDTO()
                                {
                                    Key = feeMasterMap.FeeMasterID.ToString(),
                                    Value = feeMasterMap.FeeMaster.FeeType.TypeName
                                },
                                FeePeriod = feeMasterMap.FeePeriodID.HasValue ? new KeyValueDTO()
                                {
                                    Key = feeMasterMap.FeePeriodID.HasValue ? Convert.ToString(feeMasterMap.FeePeriodID) : null,
                                    Value = !feeMasterMap.FeePeriodID.HasValue ? null : feeMasterMap.FeePeriod.Description + " ( " + feeMasterMap.FeePeriod.PeriodFrom.ToShortDateString() + '-'
                                        + feeMasterMap.FeePeriod.PeriodTo.ToShortDateString() + " ) "
                                } : new KeyValueDTO(),
                                //FeeMasterClassMontlySplitMaps = montlySplitDTO,
                                //FeeCycleID = feeMasterMap.FeeMaster.FeeCycleID,
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
            var toDto = dto as StudentGroupFeeMasterDTO;
            if (toDto.AcadamicYearID == null || toDto.AcadamicYearID == 0)
            {
                throw new Exception("Please Select Academic Year");
            }
            if (toDto.StudentGroupID == null || toDto.StudentGroupID == 0)
            {
                throw new Exception("Please Select Student Group");
            }

            //convert the dto to entity and pass to the repository.
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = new StudentGroupFeeMaster()
                {
                    StudentGroupFeeMasterIID = toDto.StudentGroupFeeMasterIID,
                    StudentGroupID = toDto.StudentGroupID,
                    AcadamicYearID = toDto.AcadamicYearID,
                    Description = toDto.Description,
                    Amount = toDto.Amount,
                    CreatedBy = toDto.StudentGroupFeeMasterIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                    UpdatedBy = toDto.StudentGroupFeeMasterIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                    CreatedDate = toDto.StudentGroupFeeMasterIID == 0 ? DateTime.Now : dto.CreatedDate,
                    UpdatedDate = toDto.StudentGroupFeeMasterIID > 0 ? DateTime.Now : dto.UpdatedDate,
                    //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
                };

                //get fee type maps
                var IIDs = toDto.FeeMasterClassMaps
                    .Select(a => a.StudentGroupFeeTypeMapIID).ToList();

                //delete maps
                var entities = dbContext.StudentGroupFeeTypeMaps.Where(x =>
                    x.StudentGroupFeeMasterID == entity.StudentGroupFeeMasterIID &&
                    !IIDs.Contains(x.StudentGroupFeeTypeMapIID)).AsNoTracking().ToList();

                if (entities.IsNotNull())
                    dbContext.StudentGroupFeeTypeMaps.RemoveRange(entities);

                entity.StudentGroupFeeTypeMaps = new List<StudentGroupFeeTypeMap>();

                foreach (var feemasterMonthly in toDto.FeeMasterClassMaps)
                {
                    if (feemasterMonthly.PercentageAmount <= 100)
                    {
                        //get existing split iids
                        var splitIIDs = feemasterMonthly.FeeMasterClassMontlySplitMaps
                            .Select(a => a.FeeMasterClassMontlySplitMapIID).ToList();

                        //delete split maps
                        var splitEntities = dbContext.FeeMasterClassMontlySplitMaps.Where(x =>
                            x.FeeMasterClassMapID == feemasterMonthly.StudentGroupFeeTypeMapIID &&
                            !splitIIDs.Contains(x.FeeMasterClassMontlySplitMapIID)).AsNoTracking().ToList();

                        if (splitEntities.IsNotNull())
                            dbContext.FeeMasterClassMontlySplitMaps.RemoveRange(splitEntities);

                        var monthlySplit = new List<FeeMasterClassMontlySplitMap>();

                        foreach (var feeMasterMonthlyDto in feemasterMonthly.FeeMasterClassMontlySplitMaps)
                        {
                            if (feeMasterMonthlyDto.Amount.HasValue)
                            {
                                var entityChild = new FeeMasterClassMontlySplitMap();
                                //{
                                //    FeeMasterClassMontlySplitMapIID = feeMasterMonthlyDto.FeeMasterClassMontlySplitMapIID,
                                //    FeeMasterClassMapID = feemasterMonthly.StudentGroupFeeTypeMapIID,
                                //    MonthID = byte.Parse(feeMasterMonthlyDto.MonthID.ToString()),
                                //    Amount = feeMasterMonthlyDto.Amount.HasValue ? feeMasterMonthlyDto.Amount : (decimal?)null,
                                //    TaxAmount = feeMasterMonthlyDto.Tax.HasValue ? feeMasterMonthlyDto.Tax : (decimal?)null,
                                //    TaxPercentage = feeMasterMonthlyDto.TaxPercentage.HasValue ? feeMasterMonthlyDto.TaxPercentage : (decimal?)null
                                //};

                                monthlySplit.Add(entityChild);
                            }
                        }

                        entity.StudentGroupFeeTypeMaps.Add(new StudentGroupFeeTypeMap()
                        {
                            StudentGroupFeeTypeMapIID = feemasterMonthly.StudentGroupFeeTypeMapIID,
                            StudentGroupFeeMasterID = feemasterMonthly.StudentGroupFeeMasterID,
                            FeeMasterID = feemasterMonthly.FeeMasterID,
                            FeePeriodID = feemasterMonthly.FeePeriodID,
                            PercentageAmount = feemasterMonthly.PercentageAmount,
                            TaxPercentage = feemasterMonthly.TaxPercentage,
                            TaxAmount = feemasterMonthly.TaxAmount,
                            //IsDiscount = feemasterMonthly.IsDiscount,
                            IsPercentage = feemasterMonthly.IsPercentage,
                            //FeeMasterClassMontlySplitMaps = monthlySplit,
                        });
                    }
                    else
                    {
                        throw new Exception("Percentage  Max 100 ");
                    }
                }

                dbContext.StudentGroupFeeMasters.Add(entity);
                if (entity.StudentGroupFeeMasterIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    foreach (var classMap in entity.StudentGroupFeeTypeMaps)
                    {
                        //foreach (var monthlySplit in classMap.FeeMasterClassMontlySplitMaps)
                        //{
                        //    if (monthlySplit.FeeMasterClassMontlySplitMapIID != 0)
                        //    {
                        //        dbContext.Entry(monthlySplit).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        //    }
                        //    else
                        //    {
                        //        dbContext.Entry(monthlySplit).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        //    }
                        //}

                        if (classMap.StudentGroupFeeTypeMapIID != 0)
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

                return GetEntity(entity.StudentGroupFeeMasterIID);
            }
        }

    }
}