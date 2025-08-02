using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.School.Academics;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Eduegate.Domain.Mappers.School.Academics
{
    public class AcademicClassMapMapper : DTOEntityDynamicMapper
    {

        public static AcademicClassMapMapper Mapper(CallContext context)
        {
            var mapper = new AcademicClassMapMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<AcademicClassMapDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private AcademicClassMapDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var academicClassMapDTO = new AcademicClassMapDTO();

                var fullDayMapList = new List<AcademicClassMapWorkingDayDTO>();
                var distinctDayMapList = new List<AcademicClassMapWorkingDayDTO>();
                var academicClassMapWorkingDayDTOList = new List<AcademicClassMapWorkingDayDTO>();

                var fullClassList = new List<KeyValueDTO>();
                var distinctClassList = new List<KeyValueDTO>();
                var classList = new List<KeyValueDTO>();

                var academicClassMapList = dbContext.AcademicClassMaps.Where(X => X.AcademicYearID == IID)
                    .Include(i => i.Class)
                    .Include(i => i.AcademicYear)
                    .OrderBy(y => y.Class.ORDERNO)
                    .AsNoTracking()
                    .ToList();

                foreach (var mapData in academicClassMapList)
                {
                    if (mapData.ClassID.HasValue)
                    {
                        fullClassList.Add(new KeyValueDTO()
                        {
                            Key = mapData.ClassID.ToString(),
                            Value = mapData.Class.ClassDescription
                        });
                    }

                    if (mapData.MonthID.HasValue)
                    {
                        fullDayMapList.Add(new AcademicClassMapWorkingDayDTO()
                        {
                            MonthID = mapData.MonthID,
                            YearID = mapData.YearID,
                            MonthName = new DateTime(Convert.ToInt32(mapData.YearID), Convert.ToInt32(mapData.MonthID), 1).ToString("MMMM", CultureInfo.InvariantCulture),
                            Description = mapData.Description,
                            TotalWorkingDays = mapData.TotalWorkingDays,
                        });
                    }
                }

                //To get working days distinct data
                if (fullDayMapList.Count > 0)
                {
                    var start = false;
                    var count = 0;

                    for (var x = 0; x < fullDayMapList.Count; x++)
                    {
                        for (var y = 0; y < distinctDayMapList.Count; y++)
                        {
                            if ((fullDayMapList[x].MonthID == distinctDayMapList[y].MonthID) && (fullDayMapList[x].YearID == distinctDayMapList[y].YearID))
                            {
                                start = true;
                            }
                        }
                        count++;
                        if (count == 1 && start == false)
                        {
                            distinctDayMapList.Add(fullDayMapList[x]);
                        }
                        start = false;
                        count = 0;
                    }

                    foreach (var data in distinctDayMapList)
                    {
                        academicClassMapWorkingDayDTOList.Add(new AcademicClassMapWorkingDayDTO()
                        {
                            MonthID = data.MonthID,
                            YearID = data.YearID,
                            MonthName = data.MonthName,
                            Description = data.Description,
                            TotalWorkingDays = data.TotalWorkingDays,
                        });
                    }
                }

                //To get class distinct data
                if (fullClassList.Count > 0)
                {
                    var start = false;
                    var count = 0;

                    for (var j = 0; j < fullClassList.Count; j++)
                    {
                        for (var k = 0; k < distinctClassList.Count; k++)
                        {
                            if (fullClassList[j].Key == distinctClassList[k].Key)
                            {
                                start = true;
                            }
                        }
                        count++;
                        if (count == 1 && start == false)
                        {
                            distinctClassList.Add(fullClassList[j]);
                        }
                        start = false;
                        count = 0;
                    }

                    foreach (var cls in distinctClassList)
                    {
                        classList.Add(new KeyValueDTO()
                        {
                            Key = cls.Key,
                            Value = cls.Value
                        });
                    }
                }

                foreach (var entity in academicClassMapList)
                {
                    academicClassMapDTO = new AcademicClassMapDTO()
                    {
                        AcademicClassMapIID = entity.AcademicClassMapIID,
                        AcademicYearID = entity.AcademicYearID,
                        AcademicYear = entity.AcademicYearID.HasValue ? new KeyValueDTO() { Key = entity.AcademicYear.AcademicYearID.ToString(), Value = entity.AcademicYear.Description + " (" + entity.AcademicYear.AcademicYearCode.ToString() + ")" } : new KeyValueDTO(),
                        Class = classList,
                        AcademicClassMapWorkingDayDTO = academicClassMapWorkingDayDTOList,
                        SchoolID = entity.SchoolID,
                        CreatedBy = entity.CreatedBy,
                        CreatedDate = entity.CreatedDate,
                        UpdatedBy = entity.UpdatedBy,
                        UpdatedDate = entity.UpdatedDate
                    };
                }

                return academicClassMapDTO;
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as AcademicClassMapDTO;

            if (toDto.AcademicYearID == null)
            {
                throw new Exception("Please select academic year!");
            }

            if (toDto.Class.Count == 0)
            {
                throw new Exception("Please select atleast one class!");
            }

            //convert the dto to entity and pass to the repository.
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = new AcademicClassMap();

                toDto.SchoolID = toDto.SchoolID.HasValue ? toDto.SchoolID : (byte)_context.SchoolID;

                foreach (KeyValueDTO cls in toDto.Class)
                {
                    var classID = int.Parse(cls.Key);
                    var mapEntity = dbContext.AcademicClassMaps.Where(x => x.AcademicYearID == toDto.AcademicYearID && x.SchoolID == toDto.SchoolID && x.ClassID == classID).AsNoTracking().ToList();

                    if (mapEntity != null || mapEntity.Count > 0)
                    {
                        dbContext.AcademicClassMaps.RemoveRange(mapEntity);
                    }

                    foreach (var gridmap in toDto.AcademicClassMapWorkingDayDTO)
                    {
                        entity = new AcademicClassMap()
                        {
                            AcademicClassMapIID = gridmap.AcademicClassMapIID,
                            ClassID = classID,
                            MonthID = gridmap.MonthID,
                            YearID = gridmap.YearID,
                            Description = gridmap.Description,
                            TotalWorkingDays = gridmap.TotalWorkingDays,
                            AcademicYearID = toDto.AcademicYearID,
                            SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                            CreatedBy = toDto.AcademicClassMapIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                            UpdatedBy = toDto.AcademicClassMapIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                            CreatedDate = toDto.AcademicClassMapIID == 0 ? DateTime.Now.Date : dto.CreatedDate,
                            UpdatedDate = toDto.AcademicClassMapIID > 0 ? DateTime.Now.Date : dto.UpdatedDate,
                            //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
                        };

                        dbContext.AcademicClassMaps.Add(entity);
                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        dbContext.SaveChanges();
                    }
                }
                return ToDTOString(ToDTO(Convert.ToInt64(entity.AcademicYearID)));
            }
        }

        public List<AcademicClassMapWorkingDayDTO> GetMonthAndYearByAcademicYearID(int? academicYearID)
        {
            var academicClassMapDTOList = new List<AcademicClassMapWorkingDayDTO>();

            using (dbEduegateSchoolContext dbContext = new dbEduegateSchoolContext())
            {
                var academicYearData = academicYearID.HasValue ? dbContext.AcademicYears.AsNoTracking().FirstOrDefault(x => x.AcademicYearID == academicYearID) : null;

                if (academicYearData != null)
                {
                    var startDateMonth = academicYearData.StartDate.Value.Month;
                    var startDateYear = academicYearData.StartDate.Value.Year;
                    var endDateMonth = academicYearData.EndDate.Value.Month;
                    var endDateYear = academicYearData.EndDate.Value.Year;

                    for (var yr = startDateYear; yr <= endDateYear; yr++)
                    {
                        for (var mnth = 1; mnth <= 12; mnth++)
                        {
                            if ((mnth >= startDateMonth && yr == startDateYear) || (mnth <= endDateMonth && yr == endDateYear))
                            {
                                academicClassMapDTOList.Add(new AcademicClassMapWorkingDayDTO()
                                {
                                    MonthID = Convert.ToByte(mnth),
                                    YearID = yr,
                                    MonthName = new DateTime(yr, mnth, 1).ToString("MMMM", CultureInfo.InvariantCulture),
                                });
                            }
                        }
                    }
                }
            }

            return academicClassMapDTOList;
        }

    }
}