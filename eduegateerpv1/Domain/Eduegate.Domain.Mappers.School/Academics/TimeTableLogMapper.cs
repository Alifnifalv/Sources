using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Academics;
using Eduegate.Framework;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using System.Data.SqlClient;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Eduegate.Domain.Entity;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Framework.Extensions;

namespace Eduegate.Domain.Mappers.School.Academics
{
    public class TimeTableLogMapper : DTOEntityDynamicMapper
    {
        public static TimeTableLogMapper Mapper(CallContext context)
        {
            var mapper = new TimeTableLogMapper();
            mapper._context = context;
            return mapper;
        }
        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<TimeTableLogDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }


        public override string GetEntity(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.TimeTableLogs.Where(X => X.TimeTableLogID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                var entitydto = new TimeTableLogDTO()
                {
                    // TimeTableAllocationID = entity.TimeTableAllocationID,
                    TimeTableLogID = entity.TimeTableLogID,
                    StaffID = entity.StaffID,
                    WeekDayID = entity.WeekDayID,
                    SubjectID = entity.SubjectID,
                    SectionID = entity.SectionID,
                    ClassTimingID = entity.ClassTimingID,
                    ClassId = entity.ClassId,
                    Notes = null,
                    AllocatedDate = entity.AllocatedDate
                };

                return ToDTOString(entitydto);
            }
        }



        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as TimeTableLogDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new TimeTableLog()
            {
                ClassId = toDto.ClassId,
                SectionID = toDto.SectionID,
                WeekDayID = toDto.WeekDayID,
                ClassTimingID = toDto.ClassTimingID,
                StaffID = toDto.StaffID,
                SubjectID = toDto.SubjectID,
                TimeTableID = toDto.TimeTableID,
                TimeTableLogID = toDto.TimeTableLogID,
                AllocatedDate = toDto.AllocatedDate,
                SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                AcademicYearID = toDto.AcademicYearID == null ? (int)_context.AcademicYearID : toDto.AcademicYearID,
                CreatedBy = toDto.TimeTableLogID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.TimeTableLogID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.TimeTableLogID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.TimeTableLogID > 0 ? DateTime.Now : dto.UpdatedDate,
                //TimeStamps = toDto.TimeStamps == null ? null : toDto.TimeStamps,
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entityGetStaffId = dbContext.TimeTableLogs.Where(a => ((a.ClassId != entity.ClassId) || (a.ClassId == entity.ClassId && a.SectionID != entity.SectionID)) && a.TimeTableID == entity.TimeTableID && a.WeekDayID == entity.WeekDayID && a.ClassTimingID == entity.ClassTimingID && a.StaffID == entity.StaffID).AsNoTracking().FirstOrDefault();
                if (entityGetStaffId != null)
                {
                    return "#101";
                }
                var entityGetId = dbContext.TimeTableLogs.Where(a => a.TimeTableID == entity.TimeTableID && a.ClassId == entity.ClassId && a.SectionID == entity.SectionID && a.WeekDayID == entity.WeekDayID && a.ClassTimingID == entity.ClassTimingID).AsNoTracking().FirstOrDefault();
                if (entityGetId == null)
                {                  
                    var maxGroupID = dbContext.TimeTableLogs.Max(a => (long?)a.TimeTableLogID);
                    entity.TimeTableLogID = maxGroupID == null ? 1 : int.Parse(maxGroupID.ToString()) + 1;                    
                    dbContext.TimeTableLogs.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    entityGetId.StaffID = entity.StaffID;
                    entityGetId.SubjectID = entity.SubjectID;
                    entity.TimeTableLogID = entityGetId.TimeTableLogID;
                    dbContext.TimeTableLogs.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();
            }

            return (Convert.ToString(entity.TimeTableLogID));
        }

        private TimeTableLogDTO ToDTO(TimeTableLog entity)
        {
            return new TimeTableLogDTO()
            {
                ClassId = entity.ClassId,
                SectionID = entity.SectionID,
                StaffID = entity.StaffID,
                WeekDayID = entity.WeekDayID,
                SubjectID = entity.SubjectID,
                ClassTimingID = entity.ClassTimingID,
                AllocatedDate = entity.AllocatedDate,

                TimeTableLogID = entity.TimeTableLogID,
                Class = new KeyValueDTO()
                {
                    Key = entity.ClassId.ToString(),
                    Value = entity.Class.ClassDescription
                },
                Section = new KeyValueDTO()
                {
                    Key = entity.SectionID.ToString(),
                    Value = entity.Section.SectionName
                },
                TimeTable = new KeyValueDTO()
                {
                    Key = entity.TimeTableID.ToString(),
                    Value = entity.TimeTable.TimeTableDescription
                },
                //ClassTiming = new KeyValueDTO()
                //{
                //    Key = entity.ClassTimingID.ToString(),
                //    Value = entity.ClassTiming.TimingDescription
                //},
                //Employee = new KeyValueDTO()
                //{
                //    Key = entity.StaffID.ToString(),
                //    Value = entity.Employee.EmployeeName
                //}//,
                //CreatedBy = entity.CreatedBy,
                //UpdatedBy = entity.UpdatedBy,
                //CreatedDate = entity.CreatedDate,
                //UpdatedDate = entity.UpdatedDate
            };
        }

        public List<TimeTableAllocInfoHeaderDTO> GetTimeTableByDate(int classID, int tableMasterId, DateTime timeTableDate)
        {
            var timeTableInfoList = new List<TimeTableAllocInfoHeaderDTO>();
            var timeTableList = new List<TimeTableAllocationDTO>();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entitiesTableMaster = dbContext.TimeTables.Where(a => a.TimeTableID == tableMasterId)
                       .AsNoTracking()
                       .ToList();

                var entitiesClassSection = dbContext.ClassSectionMaps.Where(a => a.ClassID == classID && a.AcademicYearID == _context.AcademicYearID && a.SchoolID == _context.SchoolID).OrderBy(a => a.Section.SectionName)
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .AsNoTracking()
                    .ToList();

                foreach (var classSection in entitiesClassSection)
                {
                    var entitiesTimeTableAlloc = dbContext.TimeTableLogs.Where(a => a.ClassId == classID && a.TimeTableID == tableMasterId && a.SectionID == classSection.SectionID && a.AllocatedDate == timeTableDate && a.AcademicYearID == _context.AcademicYearID && a.SchoolID == _context.SchoolID)
                        .Include(i => i.ClassTiming)
                        .Include(i => i.Employee)
                        .Include(i => i.Subject)
                        .Include(i => i.WeekDay).ThenInclude(i => i.Day)
                        .AsNoTracking()
                        .ToList();

                    var detailList = new List<TimeTableAllocInfoDetailDTO>();
                    var breakTimeList = new List<TimeTableAllocInfoDetailDTO>();
                    foreach (var timeTableAlloc in entitiesTimeTableAlloc)
                    {
                        detailList.Add(new TimeTableAllocInfoDetailDTO()
                        {
                            TimeTableAllocationIID = timeTableAlloc.TimeTableLogID,
                            ClassTimingID = timeTableAlloc.ClassTimingID,
                            StaffID = timeTableAlloc.StaffID.HasValue ? timeTableAlloc.StaffID : (long?)null,
                            SubjectID = timeTableAlloc.SubjectID.HasValue ? timeTableAlloc.SubjectID : (int?)null,
                            WeekDayID = timeTableAlloc.WeekDayID,
                            ClassTiming = new KeyValueDTO()
                            {
                                Key = timeTableAlloc.ClassTiming.ClassTimingID.ToString(),
                                Value = timeTableAlloc.ClassTiming.TimingDescription
                            },
                            Employee = timeTableAlloc.StaffID.HasValue ? new KeyValueDTO()
                            {
                                Key = timeTableAlloc.Employee.EmployeeIID.ToString(),
                                Value = timeTableAlloc.Employee.FirstName + " " + timeTableAlloc.Employee.MiddleName + " " + timeTableAlloc.Employee.LastName
                            } : new KeyValueDTO(),
                            Subject = timeTableAlloc.SubjectID.HasValue ? new KeyValueDTO()
                            {
                                Key = timeTableAlloc.Subject.SubjectID.ToString(),
                                Value = timeTableAlloc.Subject.SubjectName
                            } : new KeyValueDTO(),
                            WeekDay = new KeyValueDTO()
                            {
                                Key = timeTableAlloc.WeekDay.WeekDayID.ToString(),
                                Value = timeTableAlloc.WeekDay.Day.DayName
                            },
                            IsBreakTime = false,
                            AllocatedDate = timeTableAlloc.AllocatedDate

                        });
                    }

                    var entitiesWeek = dbContext.WeekDays.Where(a => a.IsWeekDay == true)
                        .Include(x => x.Day)
                        .AsNoTracking()
                        .ToList();

                    foreach (var week in entitiesWeek)
                    {
                        var entitiesClassTiming = dbContext.ClassTimings.Where(a => a.IsBreakTime == true)
                            .Include(x=>x.BreakType)
                            .AsNoTracking()
                            .ToList();

                        foreach (var classTiming in entitiesClassTiming)
                        {
                            detailList.Add(new TimeTableAllocInfoDetailDTO()
                            {
                                TimeTableAllocationIID = 0,
                                ClassTimingID = classTiming.ClassTimingID,
                                StaffID = (long?)null,
                                SubjectID = (int?)null,
                                WeekDayID = week.WeekDayID,
                                ClassTiming = new KeyValueDTO() { Key = classTiming.ClassTimingID.ToString(), Value = classTiming.TimingDescription },
                                Employee = new KeyValueDTO() { Key = null, Value = null },
                                Subject = new KeyValueDTO() { Key = null, Value = classTiming.BreakType.BreakTypeName },
                                WeekDay = new KeyValueDTO() { Key = week.WeekDayID.ToString(), Value = week.Day.DayName },
                                IsBreakTime = true
                            });
                        }
                    }

                    timeTableInfoList.Add(new TimeTableAllocInfoHeaderDTO()
                    {
                        Class = new KeyValueDTO() { Key = classSection.Class.ClassID.ToString(), Value = classSection.Class.ClassDescription },
                        Section = new KeyValueDTO() { Key = classSection.Section.SectionID.ToString(), Value = classSection.Section.SectionName },
                        ClassId = classSection.ClassID,
                        SectionID = classSection.SectionID,
                        AllocInfoDetails = detailList,
                        TimeTableAllocationIID = detailList.Count() > 0 ? detailList[0].TimeTableAllocationIID : 0
                    });
                }
            }

            return timeTableInfoList;
        }

        public void DeleteEntity(long timeTableLogID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entitiesTimeTableAlloc = dbContext.TimeTableLogs.Where(a => a.TimeTableLogID == timeTableLogID).AsNoTracking().FirstOrDefault();
                if (entitiesTimeTableAlloc != null)
                {
                    dbContext.TimeTableLogs.Remove(entitiesTimeTableAlloc);
                    dbContext.SaveChanges();
                }
            }
        }

        public string GenerateTimeTable(TimeTableAllocationDTO timeTableInfo)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            // It throws Argument null exception  
            //var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

            String[] dataResult = null;
            using (SqlConnection conn = new SqlConnection(Infrastructure.ConfigHelper.GetDefaultConnectionString()))
            using (SqlCommand cmd = new SqlCommand("[schools].[SPS_TIMETABLE_LOG]", conn))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@ACADEMICYEARID", SqlDbType.Int));
                adapter.SelectCommand.Parameters["@ACADEMICYEARID"].Value = _context.AcademicYearID;

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@TIMETABLEMASTERID", SqlDbType.Int));
                adapter.SelectCommand.Parameters["@TIMETABLEMASTERID"].Value = timeTableInfo.TimeTableMasterID;

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@SCHOOLID", SqlDbType.Int));
                adapter.SelectCommand.Parameters["@SCHOOLID"].Value = _context.SchoolID;

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@LOGDATE", SqlDbType.DateTime));
                adapter.SelectCommand.Parameters["@LOGDATE"].Value = timeTableInfo.AllocatedDate;

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@LOGINID", SqlDbType.Int));
                adapter.SelectCommand.Parameters["@LOGINID"].Value = _context.LoginID;

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@CLASSID", SqlDbType.Int));
                adapter.SelectCommand.Parameters["@CLASSID"].Value = timeTableInfo.ClassId;

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@ISGENERATE", SqlDbType.Int));
                adapter.SelectCommand.Parameters["@ISGENERATE"].Value = timeTableInfo.IsGenerate;

                DataSet dt = new DataSet();

                adapter.Fill(dt);
                DataTable dataTable = null;

                if (dt.Tables.Count > 0)
                {
                    if (dt.Tables[0].Rows.Count > 0)
                    {
                        dataTable = dt.Tables[0];
                    }
                }
                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        string rowValue = row.ItemArray[0].ToString().TrimEnd('_');
                        dataResult = (rowValue.Split('_'));
                    }
                }


            }
            return "Successfully Generated";
        }

        public List<DaysDTO> GetWeekDays()
        {

            using (var db = new dbEduegateSchoolContext())
            {
                var Days = db.WeekDays
                    .Include(i => i.Day)
                    .AsNoTracking()
                    .ToList();
                var DaysDTOs = new List<DaysDTO>();


                foreach (var day in Days)
                {
                    DaysDTOs.Add(new DaysDTO()
                    {
                        DayID = day.DayID ?? 0,
                        DayName = day.Day.DayName,
                    });

                }
                return DaysDTOs;
            }


        }

        public List<TimeTableAllocInfoHeaderDTO> GetSmartTimeTableByDate(int tableMasterId, DateTime timeTableDate)
        {
            var timeTableInfoList = new List<TimeTableAllocInfoHeaderDTO>();
            var timeTableList = new List<TimeTableAllocationDTO>();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entitiesTableMaster = dbContext.TimeTables.Where(a => a.TimeTableID == tableMasterId)
                       .AsNoTracking()
                       .ToList();

                var entitiesClassSection = dbContext.ClassSectionMaps.Where(a => a.AcademicYearID == _context.AcademicYearID && a.SchoolID == _context.SchoolID).OrderBy(a => a.Class.ORDERNO)
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .AsNoTracking()
                    .ToList();

                foreach (var classSection in entitiesClassSection)
                {
                    var entitiesTimeTableAlloc = dbContext.TimeTableLogs.Where(a => a.ClassId == classSection.ClassID && a.TimeTableID == tableMasterId 
                    && a.SectionID == classSection.SectionID //&& a.AllocatedDate == timeTableDate 
                    && a.AcademicYearID == _context.AcademicYearID && a.SchoolID == _context.SchoolID)
                        .Include(i => i.ClassTiming)
                        .Include(i => i.Employee)
                        .Include(i => i.Subject)
                        .Include(i => i.WeekDay).ThenInclude(i => i.Day)
                        .AsNoTracking()
                        .ToList();

                    var detailList = new List<TimeTableAllocInfoDetailDTO>();
                    var breakTimeList = new List<TimeTableAllocInfoDetailDTO>();
                    foreach (var timeTableAlloc in entitiesTimeTableAlloc)
                    {
                        detailList.Add(new TimeTableAllocInfoDetailDTO()
                        {
                            TimeTableAllocationIID = timeTableAlloc.TimeTableLogID,
                            ClassTimingID = timeTableAlloc.ClassTimingID,
                            StaffID = timeTableAlloc.StaffID.HasValue ? timeTableAlloc.StaffID : (long?)null,
                            SubjectID = timeTableAlloc.SubjectID.HasValue ? timeTableAlloc.SubjectID : (int?)null,
                            WeekDayID = timeTableAlloc.WeekDayID,
                            ClassTiming = new KeyValueDTO()
                            {
                                Key = timeTableAlloc.ClassTiming.ClassTimingID.ToString(),
                                Value = timeTableAlloc.ClassTiming.TimingDescription
                            },
                            Employee = timeTableAlloc.StaffID.HasValue ? new KeyValueDTO()
                            {
                                Key = timeTableAlloc.Employee.EmployeeIID.ToString(),
                                Value = timeTableAlloc.Employee.FirstName + " " + timeTableAlloc.Employee.MiddleName + " " + timeTableAlloc.Employee.LastName
                            } : new KeyValueDTO(),
                            Subject = timeTableAlloc.SubjectID.HasValue ? new KeyValueDTO()
                            {
                                Key = timeTableAlloc.Subject.SubjectID.ToString(),
                                Value = timeTableAlloc.Subject.SubjectName
                            } : new KeyValueDTO(),
                            WeekDay = new KeyValueDTO()
                            {
                                Key = timeTableAlloc.WeekDay.WeekDayID.ToString(),
                                Value = timeTableAlloc.WeekDay.Day.DayName
                            },
                            IsBreakTime = false,
                            AllocatedDate = timeTableAlloc.AllocatedDate

                        });
                    }

                    var entitiesWeek = dbContext.WeekDays.Where(a => a.IsWeekDay == true)
                        .Include(x => x.Day)
                        .AsNoTracking()
                        .ToList();

                    foreach (var week in entitiesWeek)
                    {
                        var entitiesClassTiming = dbContext.ClassTimings.Where(a => a.IsBreakTime == true)
                            .Include(x => x.BreakType)
                            .AsNoTracking()
                            .ToList();

                        foreach (var classTiming in entitiesClassTiming)
                        {
                            detailList.Add(new TimeTableAllocInfoDetailDTO()
                            {
                                TimeTableAllocationIID = 0,
                                ClassTimingID = classTiming.ClassTimingID,
                                StaffID = (long?)null,
                                SubjectID = (int?)null,
                                WeekDayID = week.WeekDayID,
                                ClassTiming = new KeyValueDTO() { Key = classTiming.ClassTimingID.ToString(), Value = classTiming.TimingDescription },
                                Employee = new KeyValueDTO() { Key = null, Value = null },
                                Subject = new KeyValueDTO() { Key = null, Value = classTiming.BreakType.BreakTypeName },
                                WeekDay = new KeyValueDTO() { Key = week.WeekDayID.ToString(), Value = week.Day.DayName },
                                IsBreakTime = true
                            });
                        }
                    }

                    timeTableInfoList.Add(new TimeTableAllocInfoHeaderDTO()
                    {
                        Class = new KeyValueDTO() { Key = classSection.Class.ClassID.ToString(), Value = classSection.Class.ClassDescription },
                        Section = new KeyValueDTO() { Key = classSection.Section.SectionID.ToString(), Value = classSection.Section.SectionName },
                        ClassId = classSection.ClassID,
                        SectionID = classSection.SectionID,
                        AllocInfoDetails = detailList,
                        TimeTableAllocationIID = detailList.Count() > 0 ? detailList[0].TimeTableAllocationIID : 0
                    });
                }
            }

            return timeTableInfoList;
        }

        public List<TimeTableListDTO> GetTeacherSummary()
        {
            var data = new List<TimeTableListDTO>();
            var allData = new List<TimeTableListDTO>();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var timetableDetails = dbContext.TimeTableLogs
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .Include(i => i.Employee)
                    .Include(i => i.Subject)
                    .GroupBy(a => new { a.SubjectID, a.StaffID, a.ClassId, a.SectionID })
                    .Select(g => new
                    {
                        SubjectID = g.Key.SubjectID, 
                        SubjectName = g.FirstOrDefault().Subject.SubjectName,
                        StaffID = g.Key.StaffID,
                        EmployeeName = g.FirstOrDefault().Employee.EmployeeName,
                        WeekPeriods = g.Count(a => a.TimeTableLogID != null),
                        ClassID = g.FirstOrDefault().ClassId,
                        SectionID = g.FirstOrDefault().SectionID,
                    })
                    .AsNoTracking()
                    .ToList();

                var classIds = timetableDetails
                    .Select(td => td.ClassID).Distinct()
                    .ToList();

                var sectionIds = timetableDetails
                    .Select(td => td.SectionID).Distinct()
                    .ToList();

                var subjectIds = timetableDetails
                    .Select(td => td.SubjectID).Distinct()
                    .ToList();


                var allDetails = dbContext.ClassSectionSubjectPeriodMaps
                    .Where(a => classIds.Contains(a.ClassID) && sectionIds.Contains(a.SectionID) && subjectIds.Contains(a.SubjectID))
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .Include(i => i.Subject)
                    .ToList();

                var groupedDatas = allDetails.GroupBy(a => new { a.SubjectID, a.ClassID, a.SectionID }).ToList();

                if (groupedDatas.Any())
                {
                    foreach (var sectionGroup in groupedDatas)
                    {
                        var firstItem = sectionGroup.FirstOrDefault();
                        var weekPeriods = dbContext.TimeTableLogs.Where(a => a.ClassId == firstItem.ClassID && a.SectionID == firstItem.SectionID && a.SubjectID == firstItem.SubjectID)
                            .Include(a=>a.Employee)
                            .ToList();

                        //var secondLang = sectionGroup.Where(a => a.SubjectTypeID == 2).ToList();
                        //var subLang = sectionGroup.Where(a => new[] { 22, 23 }.Contains(a.SubjectID ?? 0)).ToList();

                        //var allSkippedPeriods = secondLang.Skip(1).Union(subLang.Skip(1)).ToList();
                        //var remainingPeriods = sectionGroup.Where(a => !allSkippedPeriods.Contains(a)).ToList();

                        if (firstItem != null && weekPeriods.Count() > 0)
                        {
                            allData.Add(new TimeTableListDTO
                            {
                                Employee = new KeyValueDTO()
                                {
                                    Key = weekPeriods.FirstOrDefault().StaffID.ToString(),
                                    Value = weekPeriods.FirstOrDefault().Employee.EmployeeName,
                                },
                                Class = new KeyValueDTO()
                                {
                                    Key = sectionGroup.Key.ToString(),
                                    Value = firstItem.Class.ClassDescription
                                },
                                Section = new KeyValueDTO()
                                {
                                    Key = firstItem.SectionID.ToString(),
                                    Value = firstItem.Section.SectionName,
                                },
                                Subject = new KeyValueDTO()
                                {
                                    Key = firstItem.SubjectID.ToString(),
                                    Value = firstItem.Subject.SubjectName,
                                },
                                AllocatedPeriods = weekPeriods.Count(),
                                WeekPeriods = sectionGroup.Select(a => a.WeekPeriods).Sum()
                            });
                        }
                    }
                }

                var grpdata = allData.GroupBy(a=> a.Employee.Key).ToList();

                foreach (var datas in grpdata)
                {
                    data.Add(new TimeTableListDTO
                    {
                        Employee = new KeyValueDTO()
                        {
                            Key = datas.FirstOrDefault().Employee.Key.ToString(),
                            Value = datas.FirstOrDefault().Employee.Value,
                        },
                        AllocatedPeriods = datas.Select(a => a.AllocatedPeriods).Sum(),
                        WeekPeriods = datas.Select(a => a.WeekPeriods).Sum()
                    });
                }
            }
            return data;
        }

        public List<TimeTableListDTO> GetClassSummary()
        {
            var data = new List<TimeTableListDTO>();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var timetableDetails = dbContext.TimeTableLogs
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .GroupBy(a => new { a.ClassId, a.SectionID })
                    .Select(g => new
                    {
                        ClassID = g.Key.ClassId,
                        SectionID = g.Key.SectionID,
                        ClassDescription = g.FirstOrDefault().Class.ClassDescription,
                        SectionName = g.FirstOrDefault().Section.SectionName,
                        WeekPeriods = g.Count(a => a.TimeTableLogID != null)
                    })
                    .AsNoTracking()
                    .ToList();

                var classIds = timetableDetails
                    .Select(td => td.ClassID).Distinct()
                    .ToList();

                var sectionIds = timetableDetails
                    .Select(td => td.SectionID).Distinct()
                    .ToList();

                
                var allDetails = dbContext.ClassSectionSubjectPeriodMaps
                    .Where(a => classIds.Contains(a.ClassID) && sectionIds.Contains(a.SectionID))
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .ToList();

                var groupedDatas = allDetails.GroupBy(a => a.SectionID).ToList();

                if (groupedDatas.Any())
                {
                    foreach (var sectionGroup in groupedDatas)
                    {
                        var firstItem = sectionGroup.FirstOrDefault();
                        var weekPeriods = dbContext.TimeTableLogs.Where(a => a.ClassId == firstItem.ClassID && a.SectionID == firstItem.SectionID).ToList();

                        var secondLang = sectionGroup.Where(a => a.SubjectTypeID == 2).ToList();
                        var subLang = sectionGroup.Where(a => new[] { 22, 23 }.Contains(a.SubjectID ?? 0)).ToList();

                        var allSkippedPeriods = secondLang.Skip(1).Union(subLang.Skip(1)).ToList();
                        var remainingPeriods = sectionGroup.Where(a => !allSkippedPeriods.Contains(a)).ToList();

                        if (firstItem != null)
                        {
                            data.Add(new TimeTableListDTO
                            {
                                Section = new KeyValueDTO()
                                {
                                    Key = firstItem.SectionID.ToString(),
                                    Value = firstItem.Section.SectionName,
                                },
                                Class = new KeyValueDTO()
                                {
                                    Key = firstItem.ClassID.ToString(),
                                    Value = firstItem.Class.ClassDescription
                                },
                                AllocatedPeriods = weekPeriods.Count(),
                                WeekPeriods = remainingPeriods.Select(a =>a.WeekPeriods).Sum()
                            });
                        }
                    }
                }
            }

            return data;
        }

        public List<TimeTableAllocationDTO> GetClassSectionTimeTableSummary()
        {
            var result = new List<TimeTableAllocationDTO>();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                // Group by ClassId and SectionID to get distinct combinations
                var groupedTimetable = dbContext.TimeTableLogs
                    .GroupBy(a => new { a.ClassId, a.SectionID, a.WeekDayID })
                    .Select(g => new
                    {
                        ClassID = g.Key.ClassId,
                        SectionID = g.Key.SectionID,
                        WeekDayID = g.Key.WeekDayID,
                    })
                    .AsNoTracking()
                    .ToList();

                foreach (var timetable in groupedTimetable)
                {
                    // Get all timetable details for the grouped class and section
                    var timeTableDetails = dbContext.TimeTableLogs
                        .Where(a => a.ClassId == timetable.ClassID && a.SectionID == timetable.SectionID && a.WeekDayID == timetable.WeekDayID)
                        .Include(i => i.Class)
                        .Include(i => i.Section)
                        .Include(i => i.Subject)
                        .Include(i => i.WeekDay).ThenInclude(i => i.Day)
                        .Include(i => i.ClassTiming)
                        .Include(i => i.Employee)
                        .AsNoTracking()
                        .ToList();

                    // Get all class timings for the school (filtering out break times)
                    var classTimings = dbContext.ClassTimings
                        .Where(a => a.IsBreakTime == false && a.SchoolID == _context.SchoolID && a.AcademicYearID == _context.AcademicYearID)
                        .AsNoTracking()
                        .ToList();

                    var weekDays = dbContext.WeekDays.ToList();
                    var mainDetails = new List<TimeTableAllocationListDTO>();

                    foreach (var weekDay in weekDays)
                    {
                        var details = new List<TimeTableAllocationListDTO>();

                        foreach (var timing in classTimings)
                        {
                            // Match class timing to a specific day and class timing
                            var classTimetable = timeTableDetails
                                .FirstOrDefault(a => a.ClassTimingID == timing.ClassTimingID && a.ClassId == timetable.ClassID && a.SectionID == timetable.SectionID && a.WeekDayID == weekDay.WeekDayID);

                            details.Add(new TimeTableAllocationListDTO
                            {
                                Class = new KeyValueDTO
                                {
                                    Key = classTimetable?.ClassId.ToString(),
                                    Value = classTimetable?.Class.ClassDescription,
                                },
                                Section = new KeyValueDTO
                                {
                                    Key = classTimetable?.SectionID.ToString(),
                                    Value = classTimetable?.Section.SectionName,
                                },
                                WeekDay = new KeyValueDTO
                                {
                                    Key = classTimetable?.WeekDayID.ToString(),
                                    Value = classTimetable?.WeekDay.Day.DayName,
                                },
                                Subject = new KeyValueDTO
                                {
                                    Key = classTimetable?.SubjectID.ToString(),
                                    Value = classTimetable?.Subject.SubjectName,
                                },
                                ClassTiming = new KeyValueDTO
                                {
                                    Key = timing.ClassTimingID.ToString(),
                                    Value = timing.TimingDescription,
                                },
                                Employee = new KeyValueDTO
                                {
                                    Key = classTimetable?.StaffID.ToString(),
                                    Value = classTimetable?.Employee.EmployeeName,
                                },
                                ClassId = classTimetable?.ClassId,
                                SectionID = classTimetable?.SectionID,
                                ClassTimingID = timing.ClassTimingID,
                                WeekDayID = classTimetable?.WeekDayID,
                                SubjectID = classTimetable?.SubjectID,
                            });
                        }
                        mainDetails.Add(new TimeTableAllocationListDTO
                        {
                            ClassId = timetable.ClassID,
                            SectionID = timetable.SectionID,
                            WeekDayID = weekDay.WeekDayID,
                            MapDetails = details
                        });

                    }

                    // Add the details of each section and class only once
                    if (!result.Any(x => x.ClassId == timetable.ClassID && x.SectionID == timetable.SectionID))
                    {
                        result.Add(new TimeTableAllocationDTO
                        {
                            ClassId = timetable.ClassID,
                            SectionID = timetable.SectionID,
                            MainDetails = mainDetails
                        });
                    }
                }
            }

            return result;
        }

        public List<TimeTableListDTO> GetTeacherSummaryByTeacherID(long teacherID)
        {
            var data = new List<TimeTableListDTO>();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var timetableDetails = dbContext.TimeTableLogs
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .Include(i => i.Employee)
                    .Include(i => i.Subject)
                    .Where(a => a.StaffID == teacherID)
                    .GroupBy(a => new { a.SubjectID, a.StaffID, a.ClassId, a.SectionID })
                    .Select(g => new
                    {
                        SubjectID = g.Key.SubjectID,
                        SubjectName = g.FirstOrDefault().Subject.SubjectName,
                        StaffID = g.Key.StaffID,
                        EmployeeName = g.FirstOrDefault().Employee.EmployeeName,
                        WeekPeriods = g.Count(a => a.TimeTableLogID != null),
                        ClassID = g.FirstOrDefault().ClassId,
                        SectionID = g.FirstOrDefault().SectionID,
                    })
                    .AsNoTracking()
                    .ToList();

                var classIds = timetableDetails
                    .Select(td => td.ClassID).Distinct()
                    .ToList();

                var sectionIds = timetableDetails
                    .Select(td => td.SectionID).Distinct()
                    .ToList();

                var subjectIds = timetableDetails
                    .Select(td => td.SubjectID).Distinct()
                    .ToList();


                var allDetails = dbContext.ClassSectionSubjectPeriodMaps
                    .Where(a => classIds.Contains(a.ClassID) && sectionIds.Contains(a.SectionID) && subjectIds.Contains(a.SubjectID))
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .Include(i => i.Subject)
                    .ToList();

                var groupedDatas = allDetails.GroupBy(a => new { a.SubjectID, a.ClassID, a.SectionID }).ToList();

                if (groupedDatas.Any())
                {
                    foreach (var sectionGroup in groupedDatas)
                    {
                        var firstItem = sectionGroup.FirstOrDefault();
                        var weekPeriods = dbContext.TimeTableLogs.Where(a => a.ClassId == firstItem.ClassID && a.SectionID == firstItem.SectionID && a.SubjectID == firstItem.SubjectID && a.StaffID == teacherID)
                            .Include(a => a.Employee)
                            .ToList();

                        //var secondLang = sectionGroup.Where(a => a.SubjectTypeID == 2).ToList();
                        //var subLang = sectionGroup.Where(a => new[] { 22, 23 }.Contains(a.SubjectID ?? 0)).ToList();

                        //var allSkippedPeriods = secondLang.Skip(1).Union(subLang.Skip(1)).ToList();
                        //var remainingPeriods = sectionGroup.Where(a => !allSkippedPeriods.Contains(a)).ToList();

                        if (firstItem != null && weekPeriods.Count() > 0)
                        {
                            data.Add(new TimeTableListDTO
                            {
                                Employee = new KeyValueDTO()
                                {
                                    Key = weekPeriods.FirstOrDefault().StaffID.ToString(),
                                    Value = weekPeriods.FirstOrDefault().Employee.EmployeeName,
                                },
                                Class = new KeyValueDTO()
                                {
                                    Key = sectionGroup.Key.ToString(),
                                    Value = firstItem.Class.ClassDescription
                                },
                                Section = new KeyValueDTO()
                                {
                                    Key = firstItem.SectionID.ToString(),
                                    Value = firstItem.Section.SectionName,
                                },
                                Subject = new KeyValueDTO()
                                {
                                    Key = firstItem.SubjectID.ToString(),
                                    Value = firstItem.Subject.SubjectName,
                                },
                                AllocatedPeriods = weekPeriods.Count(),
                                WeekPeriods = sectionGroup.Select(a => a.WeekPeriods).Sum()
                            });
                        }
                    }
                }
            }
            return data;
        }

        public List<TimeTableListDTO> GetClassSummaryDetails(long classID, long sectionID)
        {
            var data = new List<TimeTableListDTO>();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var timetableDetails = dbContext.TimeTableLogs
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .Where(a => a.ClassId == classID && a.SectionID == sectionID)
                    .GroupBy(a => new { a.ClassId, a.SectionID })
                    .Select(g => new
                    {
                        ClassID = g.Key.ClassId,
                        SectionID = g.Key.SectionID,
                        ClassDescription = g.FirstOrDefault().Class.ClassDescription,
                        SectionName = g.FirstOrDefault().Section.SectionName,
                        WeekPeriods = g.Count(a => a.TimeTableLogID != null)
                    })
                    .AsNoTracking()
                    .ToList();

                var classIds = timetableDetails
                    .Select(td => td.ClassID).Distinct()
                    .ToList();

                var sectionIds = timetableDetails
                    .Select(td => td.SectionID).Distinct()
                    .ToList();


                var allDetails = dbContext.ClassSectionSubjectPeriodMaps
                    .Where(a => classIds.Contains(a.ClassID) && sectionIds.Contains(a.SectionID))
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .Include(i => i.Subject)
                    .ToList();

                var groupedDatas = allDetails.GroupBy(a => a.SubjectID).ToList();

                if (groupedDatas.Any())
                {
                    foreach (var sectionGroup in groupedDatas)
                    {
                        var firstItem = sectionGroup.FirstOrDefault();
                        var weekPeriods = dbContext.TimeTableLogs.Where(a => a.ClassId == firstItem.ClassID && a.SectionID == firstItem.SectionID && a.SubjectID == firstItem.SubjectID).ToList();

                        //var secondLang = sectionGroup.Where(a => a.SubjectTypeID == 2).ToList();
                        //var subLang = sectionGroup.Where(a => new[] { 22, 23 }.Contains(a.SubjectID ?? 0)).ToList();

                        //var allSkippedPeriods = secondLang.Skip(1).Union(subLang.Skip(1)).ToList();
                        //var remainingPeriods = sectionGroup.Where(a => !allSkippedPeriods.Contains(a)).ToList();

                        if (firstItem != null)
                        {
                            data.Add(new TimeTableListDTO
                            {
                                Section = new KeyValueDTO()
                                {
                                    Key = firstItem.SectionID.ToString(),
                                    Value = firstItem.Section.SectionName,
                                },
                                Class = new KeyValueDTO()
                                {
                                    Key = firstItem.ClassID.ToString(),
                                    Value = firstItem.Class.ClassDescription
                                },
                                Subject = new KeyValueDTO()
                                {
                                    Key = sectionGroup.Key.ToString(),
                                    Value = firstItem.Subject.SubjectName
                                },
                                AllocatedPeriods = weekPeriods.Count(),
                                WeekPeriods = sectionGroup.Select(a => a.WeekPeriods).Sum()
                            });
                        }
                    }
                }
            }

            return data;
        }
    }
}