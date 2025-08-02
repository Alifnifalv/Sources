using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Framework;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Fees;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Eduegate.Domain.Mappers.School.Fees
{
    public class FeePeriodsMapper : DTOEntityDynamicMapper
    {
        public static FeePeriodsMapper Mapper(CallContext context)
        {
            var mapper = new FeePeriodsMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<FeePeriodsDTO>(entity);
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

                var entity = dbContext.FeePeriods.Where(x => x.FeePeriodID == IID)
                    .Include(x => x.AcademicYear)
                    .AsNoTracking()
                    .FirstOrDefault();

                var feeperiod = new FeePeriodsDTO()
                {
                    FeePeriodID = entity.FeePeriodID,
                    Description = entity.Description,
                    PeriodFrom = entity.PeriodFrom,
                    PeriodTo = entity.PeriodTo,
                    IsMandatoryToPay=entity.IsMandatoryToPay,
                    IsTransport = entity.IsTransport,
                    AcademicYearID = entity.AcademicYearId.HasValue ? Convert.ToInt32(entity.AcademicYearId) : 0,
                    AcademicYear = entity.AcademicYearId.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.AcademicYearId.ToString(),
                        Value = entity.AcademicYear.Description
                        + " ( " + entity.AcademicYear.StartDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture)
                        + " - " + entity.AcademicYear.EndDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) + ")"
                    } : new KeyValueDTO(),
                    NumberOfPeriods = entity.NumberOfPeriods,
                };

                return ToDTOString(feeperiod);
            }

        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as FeePeriodsDTO;
            if (toDto.PeriodFrom >= toDto.PeriodTo)
            {
                throw new Exception("Select Date Properlly!!");
            }

            using (var dbContext = new dbEduegateSchoolContext())
            {

                //Check duplicate 
                var oldPeriods = dbContext.FeePeriods.Where(x => x.AcademicYearId == toDto.AcademicYearID && x.PeriodFrom.Date == toDto.PeriodFrom.Date && x.IsTransport == toDto.IsTransport).ToList();

                if(oldPeriods.Count > 0 && toDto.FeePeriodID == 0)
                {
                    throw new Exception("The Period '" + toDto.Description + "' already exists; please check!");
                }

                //Check Dates based on academic year
                var academicYearDetail = dbContext.AcademicYears.Where(a => a.AcademicYearID == toDto.AcademicYearID).AsNoTracking().FirstOrDefault();

                if (academicYearDetail != null)
                {
                    if (academicYearDetail.StartDate > toDto.PeriodFrom || academicYearDetail.EndDate < toDto.PeriodFrom)
                    {
                        throw new Exception("Selected From Date is out of range!");
                    }

                    if (academicYearDetail.EndDate < toDto.PeriodTo)
                    {
                        throw new Exception("Selected To Date is out of range!");
                    }
                }
            }

            //convert the dto to entity and pass to the repository.
            var entity = new FeePeriod()
            {
                FeePeriodID = toDto.FeePeriodID,
                Description = toDto.Description,
                PeriodFrom = toDto.PeriodFrom,
                PeriodTo = toDto.PeriodTo,
                IsMandatoryToPay= toDto.IsMandatoryToPay,
                IsTransport = toDto.IsTransport,
                AcademicYearId = toDto.AcademicYearID,
                NumberOfPeriods = toDto.NumberOfPeriods,
                SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                CreatedBy = toDto.FeePeriodID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.FeePeriodID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.FeePeriodID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.FeePeriodID > 0 ? DateTime.Now : dto.UpdatedDate,
                //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
                if (entity.FeePeriodID == 0)
                {
                    var maxGroupID = dbContext.FeePeriods.Max(a => (int?)a.FeePeriodID);
                    entity.FeePeriodID = maxGroupID == null ? 1 : int.Parse(maxGroupID.ToString()) + 1;
                    dbContext.FeePeriods.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.FeePeriods.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return GetEntity(entity.FeePeriodID);
        }

        public List<FeePeriodsDTO> GetFeePeriodMonthlySplit(List<int> feePeriodIds)
        {
            var feeperiodList = new List<FeePeriodsDTO>();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                feeperiodList = (from fp in dbContext.FeePeriods
                                 where feePeriodIds.Contains(fp.FeePeriodID)
                                 select new FeePeriodsDTO()
                                 {
                                     NumberOfPeriods = fp.NumberOfPeriods,
                                     FeePeriodID = fp.FeePeriodID,
                                     PeriodFrom = fp.PeriodFrom,
                                     PeriodTo = fp.PeriodTo
                                 }).AsNoTracking().ToList();
            }

            return feeperiodList;
        }

        public List<KeyValueDTO> GetTransportFeePeriod(int academicYearID)
        {
            var acdmYrId = (academicYearID == 0 ? _context.AcademicYearID : academicYearID);
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

                var feePeriodList = new List<KeyValueDTO>();

                var entities = dbContext.FeePeriods.Where(a => a.AcademicYearId == acdmYrId && a.IsTransport == true).AsNoTracking().ToList();

                foreach (var period in entities)
                {
                    feePeriodList.Add(new KeyValueDTO
                    {
                        Key = period.FeePeriodID.ToString(),
                        Value = period.Description + " (" + period.PeriodFrom.ToString(dateFormat, CultureInfo.InvariantCulture) + " - " + period.PeriodTo.ToString(dateFormat, CultureInfo.InvariantCulture) + ")"
                    });
                }

                return feePeriodList;
            }
        }

    }
}